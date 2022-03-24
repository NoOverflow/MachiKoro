using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Restaurant : ICard
    {
        public string Name { get; set; } = "Restaurant";
        public List<int> Activations { get; set; } = new List<int>()
        {
            9,
            10
        };
        public int Price { get; set; } = 3;
        public CardType Type { get; set; } = CardType.Coffee;

        public int Gain(Player _) => 2;
    }
}
