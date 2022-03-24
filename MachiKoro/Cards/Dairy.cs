using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Dairy : ICard
    {
        public string Name { get; set; } = "Dairy";
        public List<int> Activations { get; set; } = new List<int>()
        {
            7
        };
        public int Price { get; set; } = 5;
        public CardType Type { get; set; } = CardType.Factory;

        public int Gain(Player player)
        {
            int animalCardsCount = 0;
            var animalCards = player.Deck.Where(x => x.Key.Type == CardType.Animal);

            foreach (var card in animalCards)
                animalCardsCount += card.Value;
            return 3 * animalCardsCount;
        }
    }
}
