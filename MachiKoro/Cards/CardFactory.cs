using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal static class CardFactory
    {
        private static Random Random = new Random();

        private static List<ICard> ICards = new List<ICard>()
        {
            new Bakery(),
            new BusinessCenter(),
            new Coffee(),
            new Dairy(),
            new Farm(),
            new Forest(),
            new FruitMarket(),
            new FurnitureFactory(),
            new Mine(),
            new Orchard(),
            new Restaurant(),
            new Stadium(),
            new SuperMarket(),
            new TvChannel(),
            new WheatField()
        };

        public static Dictionary<ICard, int> GenerateDeck(int count)
        {
            Dictionary<ICard, int> retDeck = new Dictionary<ICard, int>();

            for (int i = 0; i < count; i++)
            {
                ICard newCard = ICards[Random.Next(ICards.Count)];

                if (retDeck.ContainsKey(newCard))
                    retDeck[newCard] = retDeck[newCard] + 1;
                else
                    retDeck.Add(newCard, 1);
            }
            return retDeck;
        }
    }
}
