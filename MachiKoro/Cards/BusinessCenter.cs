using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class BusinessCenter : ICard
    {
        public string Name { get; set; } = "Business Center";
        public List<int> Activations { get; set; } = new List<int>()
        {
            6
        };
        public int Price { get; set; } = 8;
        public CardType Type { get; set; } = CardType.Business;

        public int Gain(Player player) => 0;
    }
}
