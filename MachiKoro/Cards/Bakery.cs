using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Bakery : ICard
    {
        public string Name { get; set; } = "Bakery";
        public List<int> Activations { get; set; } = new List<int>()
        {
            2,
            3
        };
        public int Price { get; set; } = 1;
        public CardType Type { get; set; } = CardType.Shop;

        public int Gain(Player player) => 1;
    }
}
