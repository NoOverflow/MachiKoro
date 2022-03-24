using MachiKoro.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachiKoro.Cards
{
    internal class TvChannel : ICard
    {
        public string Name { get; set; } = "TV Channel";
        public List<int> Activations { get; set; } = new List<int>()
        {
            6
        };
        public int Price { get; set; } = 7;
        public CardType Type { get; set; } = CardType.Business;

        public int Gain(Player player) => 5;
    }
}
