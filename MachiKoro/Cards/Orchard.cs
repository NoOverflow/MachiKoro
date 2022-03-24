using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Orchard : ICard
    {
        public string Name { get; set; } = "Orchard";
        public List<int> Activations { get; set; } = new List<int>()
        {
            10
        };
        public int Price { get; set; } = 3;
        public CardType Type { get; set; } = CardType.Field;

        public int Gain(Player _) => 3;
    }
}
