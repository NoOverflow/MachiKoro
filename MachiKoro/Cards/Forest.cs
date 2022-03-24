using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Forest : ICard
    {
        public string Name { get; set; } = "Forest";
        public List<int> Activations { get; set; } = new List<int>()
        {
            5
        };
        public int Price { get; set; } = 3;
        public CardType Type { get; set; } = CardType.Resource;

        public int Gain(Player _) => 1;
    }
}
