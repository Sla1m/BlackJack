using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Card
    {
        public int Face { get; set; }
        public string Suit { get; set; }

        public Card(int face, string suit)
        {
            this.Face = face;
            this.Suit = suit;
        }
    }
}
