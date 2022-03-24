using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class FruitMarket : ICard
    {
        public string Name { get; set; } = "Fruit Market";
        public List<int> Activations { get; set; } = new List<int>()
        {
            11,
            12
        };
        public int Price { get; set; } = 2;
        public CardType Type { get; set; } = CardType.FruitMarket;

        public int Gain(Player player)
        {
            int resourceCardsCount = 0;
            var resourceCards = player.Deck.Where(x => x.Key.Type == CardType.Field);

            foreach (var card in resourceCards)
                resourceCardsCount += card.Value;
            return 2 * resourceCardsCount;
        }
    }
}
