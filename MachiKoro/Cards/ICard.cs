using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal interface ICard
    {
        /// <summary>
        /// The name of the card
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of the dice's results that will activate this card effects (we only focus on the gains)
        /// </summary>
        public List<int> Activations { get; set; }

        /// <summary>
        /// The price of the card (in coins)
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// The type of the card, used to calculate gain for specific cards such as the Dairy
        /// </summary>
        public CardType Type { get; set; }

        /// <summary>
        /// The gain of a card when it is activated (in coins)
        /// </summary>
        public int Gain(Player player);
    }
}
