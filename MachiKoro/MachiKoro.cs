using MachiKoro.Cards;
using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro
{
    public enum Monuments
    {
        Station, 
        Mall,
        RadioTower,
        AmusementPark
    }

    public enum CardType
    {
        Field,
        Animal,
        Shop,
        Coffee,
        Resource,
        Business,
        Factory,
        FruitMarket,
        Monument
    }

    public enum MoveType
    {
        Skip,
        Buy,
        Construct
    }

    internal class MachiKoro : ICloneable
    {
        private Player AIPlayer;

        /// <summary>
        /// The available cards on the table
        /// </summary>
        public Dictionary<ICard, int> AvailableCards = new Dictionary<ICard, int>();

        private Random Random = new Random();

        /// <summary>
        /// Helper function used to count the cards in a player's deck
        /// </summary>
        /// <param name="player"></param>
        /// <param name="searchCard"></param>
        /// <returns></returns>
        public static int CountCards(Player player, ICard searchCard)
        {
            var cards = player.Deck.Where(x => x.Key.Name == searchCard.Name);
            int count = 0;

            foreach (var card in cards)
            {
                count += card.Value;
            }
            return count;
        }

        public int RollDice(Player player)
        {
            if (CountCards(player, new Station()) > 0)
            {
                int result = Random.Next(1, 13);

                Console.WriteLine("Player rolls two dices and get [ {0} ]", result);
                return result;
            } 
            else
            {
                int result = Random.Next(1, 7);

                Console.WriteLine("Player rolls a dice and get [ {0} ]", result);
                return result;
            }
        }

        public void PerformEffects(Player player, int diceResult)
        {
            foreach (var card in player.Deck)
            {
                if (card.Value == 0)
                    continue;
                if (card.Key.Activations.Contains(diceResult))
                {
                    // This is where you should perform the effects properly, since we're only testing the AI i'll just add money
                    Console.WriteLine("Your property {0} gets you {1}$", card.Key.Name, card.Key.Gain(player));
                    player.Money += card.Key.Gain(player);
                }
            }
        }

        public object Clone()
        {
            MachiKoro mk = new MachiKoro();

            mk.AvailableCards = new Dictionary<ICard, int>(this.AvailableCards);
            return mk;
        }

        public MachiKoro()
        {

        }

        public void Start()
        {
            this.AIPlayer = new AIPlayer();

            this.AIPlayer.Deck.Add(new Station(), 1);
            this.AIPlayer.Deck.Add(new Mall(), 1);
            this.AIPlayer.Deck.Add(new RadioTower(), 1);

            this.AIPlayer.Money = 14;

            this.AvailableCards = CardFactory.GenerateDeck(84);

            // Simulate 5 rounds with only AI playing
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("[ Available Cards ]");
                foreach (var card in this.AvailableCards)
                {
                    Console.WriteLine("\t- {0}: {1}", card.Key.Name, card.Value);
                }

                this.AIPlayer.DoTurn(this);

                if (CountCards(AIPlayer, new AmusementPark()) > 0
                && CountCards(AIPlayer, new Mall()) > 0
                && CountCards(AIPlayer, new Station()) > 0
                && CountCards(AIPlayer, new RadioTower()) > 0
                )
                {
                    Console.WriteLine("We won at round {0}!", i);
                    break;
                }
            }
                
        }
    }
}
