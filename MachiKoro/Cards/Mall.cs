using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Mall : ICard
    {
        public string Name { get; set; } = "Mall";
        public List<int> Activations { get; set; } = new List<int>()
        {
            
        };
        public int Price { get; set; } = 10;
        public CardType Type { get; set; } = CardType.Monument;

        public int Gain(Player _) => 0;
    }
}
