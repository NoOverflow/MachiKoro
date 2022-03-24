using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Coffee : ICard
    {
        public string Name { get; set; } = "Coffee";
        public List<int> Activations { get; set; } = new List<int>()
        {
            3
        };
        public int Price { get; set; } = 2;
        public CardType Type { get; set; } = CardType.Coffee;

        public int Gain(Player player) => 2;
    }
}
