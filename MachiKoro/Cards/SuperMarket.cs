using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class SuperMarket : ICard
    {
        public string Name { get; set; } = "Super Market";
        public List<int> Activations { get; set; } = new List<int>()
        {
            4
        };
        public int Price { get; set; } = 2;
        public CardType Type { get; set; } = CardType.Shop;

        public int Gain(Player _) => 3;
    }
}
