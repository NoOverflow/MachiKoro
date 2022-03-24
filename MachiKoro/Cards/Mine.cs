using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class Mine : ICard
    {
        public string Name { get; set; } = "Mine";
        public List<int> Activations { get; set; } = new List<int>()
        {
            9
        };
        public int Price { get; set; } = 6;
        public CardType Type { get; set; } = CardType.Resource;

        public int Gain(Player _) => 5;
    }
}
