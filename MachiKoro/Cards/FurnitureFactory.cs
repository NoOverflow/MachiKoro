using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class FurnitureFactory : ICard
    {
        public string Name { get; set; } = "Furniture Factory";
        public List<int> Activations { get; set; } = new List<int>()
        {
            8
        };
        public int Price { get; set; } = 3;
        public CardType Type { get; set; } = CardType.Factory;

        public int Gain(Player player)
        {
            int resourceCardsCount = 0;
            var resourceCards = player.Deck.Where(x => x.Key.Type == CardType.Resource);

            foreach (var card in resourceCards)
                resourceCardsCount += card.Value;
            return 3 * resourceCardsCount;
        }
    }
}
