using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class WheatField : ICard
    {
        public string Name { get; set; } = "Wheat Field";
        public List<int> Activations { get; set; } = new List<int>()
        {
            1
        };
        public int Price { get; set; } = 1;
        public CardType Type { get; set; } = CardType.Field;

        public int Gain(Player player) => 1;
    }
}
