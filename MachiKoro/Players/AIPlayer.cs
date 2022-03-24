using MachiKoro.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MachiKoro.MachiKoro;

namespace MachiKoro.Players
{
    internal class AIPlayer : Player
    {
        public override Dictionary<ICard, int> Deck { get; set; } = new Dictionary<ICard, int>()
        {
            { new WheatField(), 1 },
            { new Bakery(), 1 }
        };
        public override int Money { get; set; } = 3;

        /// <summary>
        /// Get the "weight" of a card, (probability of it being activated * gain) * (1 or (1 + 1/36)) 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        private float GetWeight(Player player, ICard card)
        {
            int maxDiceValue = CountCards(player, new Station()) > 0 ? 12 : 6;
            bool canDouble = CountCards(player, new Station()) > 0 && CountCards(player, new AmusementPark()) > 0;

            // Can this card be activated using only one dice ?
            if (card.Activations.Count(x => x <= maxDiceValue) == 0)
                return 0.0f;
            return card.Gain(player) * (card.Activations.Count / (float)maxDiceValue) * (canDouble ? (1 + 1 / 36) : 1);
        }

        /// <summary>
        /// Evaluate the total value of a tree's node based on the player state
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private float EvaluateNode(Player player)
        {
            float value = 0;
            float inf = float.MaxValue;

            if (CountCards(player, new AmusementPark()) > 0
                && CountCards(player, new Mall()) > 0
                && CountCards(player, new Station()) > 0
                && CountCards(player, new RadioTower()) > 0
            )
                return inf;

            // Arbitrary values used for this part
            if (CountCards(player, new AmusementPark()) > 0)
                value += 5;
            if (CountCards(player, new Mall()) > 0)
                value += 5;
            if (CountCards(player, new Station()) > 0)
                value += 5;
            if (CountCards(player, new RadioTower()) > 0)
                value += 5;
            //

            foreach (var card in player.Deck)
                value += GetWeight(player, card.Key) * (float)card.Value;
            return value;
        }

        /// <summary>
        /// Get the most efficient card to buy based on our current monetary situation
        /// TODO: Optimise, we should not really always buy, sometimes saving would be the best option
        /// </summary>
        /// <param name="player">The player</param>
        /// <returns>The best card to buy</returns>
        private ICard? GetMostEfficientCard(MachiKoro state, Player player)
        {
            ICard? ret = null;
            float bestWeight = 0;

            foreach (var card in state.AvailableCards)
            {
                if (card.Value == 0)
                    continue;
                var weight = GetWeight(player, card.Key);

                if (weight > bestWeight && player.Money >= card.Key.Price)
                {
                    ret = card.Key;
                    bestWeight = weight;
                }
            }
            return ret;
        }

        private ICard? GetMostEfficientMonument(Player player)
        {
            if (CountCards(player, new Station()) == 0 && new Station().Price <= player.Money)
                return new Station();
            if (CountCards(player, new Mall()) == 0 && new Mall().Price <= player.Money)
                return new Mall();
            if (CountCards(player, new AmusementPark()) == 0 && new AmusementPark().Price <= player.Money)
                return new AmusementPark();
            if (CountCards(player, new RadioTower()) == 0 && new RadioTower().Price <= player.Money)
                return new RadioTower();
            return null;
        }
        private void TestComputeNextSteps(MachiKoro state, Player player)
        {
            var skipValue = EvaluateNode(player);
            ICard? effCard = GetMostEfficientCard(state, player);
            ICard? effMon = GetMostEfficientMonument(player);
            float buyValue = float.MinValue;
            float constructValue = float.MinValue;

            if (effCard != null)
            {
                Player playerCopy = (Player)player.Clone();

                playerCopy.Buy(ref state, effCard);
                buyValue = EvaluateNode(playerCopy);
            }
            if (effMon != null)
            {
                Player playerCopy = (Player)player.Clone();

                playerCopy.Buy(ref state, effMon);
                constructValue = EvaluateNode(playerCopy);
            }
            Console.WriteLine("[ {0} ] (Skip)         [ {1} ] (Buy)         [ {2} ] (Construct)", skipValue, buyValue, constructValue);
        }

        private bool FutureBuy(ref MachiKoro state, ref Player player)
        {
            ICard? effCard = GetMostEfficientCard(state, player);

            if (effCard != null)
            {
                player.Buy(ref state, effCard);
                return true;
            }
            return false;
        }

        private bool FutureConstruct(ref MachiKoro state, ref Player player)
        {
            ICard? effMon = GetMostEfficientMonument(player);

            if (effMon != null)
            {
                player.Buy(ref state, effMon);
                return true;
            }
            return false;
        }

        private static MoveType FirstMiniMaxMove;
        /// <summary>
        /// A really dodgy implementation of minimax, right now the AI only plays for itself and not actively trying to minimize the opponent chances (Effectively just a "max" algorithm) 
        /// since the game really does not need a minimax as there is no effective "way" to counter the opponent (you cannot kill one of its pawns, you cannot block it etc...) 
        /// (The only way to block an opponent is to buy a card so that it is no longer available to him but it rarely happens so I don't see why we would need the additional compute time just for that unless i'm mistaken)
        ///
        /// TODO: Improve this by simulating the game state on every node recursion ? Probably a bad idea though since so much of this game depends on randomness (dices)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="playerState"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private float MiniMax(MachiKoro state, Player playerState, int depth)
        {
            float value = float.MinValue;

            if (depth == 0)
                return EvaluateNode(playerState);

            var skipPlayerState = (Player)playerState.Clone();
            var skipGameState = (MachiKoro)state.Clone(); 
            var sim = MiniMax(skipGameState, skipPlayerState, depth - 1);
            if (sim > value)
                FirstMiniMaxMove = MoveType.Skip;
            value = Math.Max(value, sim);

            var buyPlayerState = (Player)playerState.Clone();
            var buyGameState = (MachiKoro)state.Clone();
            if (FutureBuy(ref state, ref buyPlayerState))
            {
                var buySim = MiniMax(buyGameState, buyPlayerState, depth - 1);

                if (buySim >= value)
                    FirstMiniMaxMove = MoveType.Buy;
                value = Math.Max(value, buySim);
            }

            var buyConstrPlayerState = (Player)playerState.Clone();
            var buyConstrGameState = (MachiKoro)state.Clone();
            if (FutureConstruct(ref state, ref buyConstrPlayerState))
            {
                var constrSim = MiniMax(buyConstrGameState, buyConstrPlayerState, depth - 1);

                if (constrSim >= value)
                    FirstMiniMaxMove = MoveType.Construct;
                value = Math.Max(value, constrSim);
            }
            // Console.WriteLine("Move at depth {0} = {1} ({2})", depth, FirstMiniMaxMove, value);
            return value;
        }

        public override void DoTurn(MachiKoro state)
        {
            Console.WriteLine("[-----   AI TURN (Money: {0}) -----]" + Environment.NewLine, this.Money);
            int result = state.RollDice(this);

            state.PerformEffects(this, result);

            MachiKoro shallowGameState = (MachiKoro)state.Clone();
            MiniMax(shallowGameState, this, 8);
            switch (FirstMiniMaxMove)
            {
                case MoveType.Skip:
                    Console.WriteLine("The AI choses to skip this turn.");
                    break;
                case MoveType.Buy:
                    var effCard = GetMostEfficientCard(state, this);

                    this.Buy(ref state, effCard);
                    Console.WriteLine("The AI choses to buy a {0}", effCard.Name);
                    break;
                case MoveType.Construct:
                    var effMonCard = GetMostEfficientMonument(this);

                    this.Buy(ref state, effMonCard);
                    Console.WriteLine("The AI choses to construct a {0}", effMonCard.Name);
                    break;
            }
        }

        public override bool Buy(ref MachiKoro gameState, ICard card)
        {
            if (this.Money < card.Price)
                return false;
            this.Money -= card.Price;
            if (this.Deck.ContainsKey(card))
                this.Deck[card] = this.Deck[card] + 1;
            else
                this.Deck.Add(card, 1);
            if (card.Type != CardType.Monument)
                gameState.AvailableCards[card] = gameState.AvailableCards[card] - 1;
            return true;
        }

        public override object Clone()
        {
            Player clone = new AIPlayer();

            clone.Deck = new Dictionary<ICard, int>(this.Deck);
            clone.Money = this.Money;
            return clone;
        }
    }
}
