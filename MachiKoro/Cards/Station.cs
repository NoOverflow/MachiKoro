using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Station : ICard
    {
        public string Name { get; set; } = "Station";
        public List<int> Activations { get; set; } = new List<int>()
        {
            
        };
        public int Price { get; set; } = 4;
        public CardType Type { get; set; } = CardType.Monument;

        public int Gain(Player _) => 0;
    }
}
