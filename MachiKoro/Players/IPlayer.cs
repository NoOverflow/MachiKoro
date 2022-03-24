using MachiKoro.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Players
{
    internal abstract class Player : ICloneable
    {
        /// <summary>
        /// The player's deck  [Card, NumberOfCardsOwned]
        /// </summary>
        public abstract Dictionary<ICard, int> Deck { get; set; }

        /// <summary>
        /// The player's available money
        /// </summary>
        public abstract float Money { get; set; }

        /// <summary>
        /// Perform the Player turn
        /// </summary>
        public abstract void DoTurn(MachiKoro state);

        
        /// <summary>
        /// Buy a card
        /// </summary>
        /// <param name="card">The card to buy</param>
        /// <returns>True if purchase was succesfull</returns>
        public abstract bool Buy(ref MachiKoro gameState, ICard card);

        public abstract object Clone();
    }
}
