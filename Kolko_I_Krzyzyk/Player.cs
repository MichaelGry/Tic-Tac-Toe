using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolko_I_Krzyzyk
{
    class Player
    {
        static int amount = 0;
        int id;
        string name;
        public int points = 0;
        bool twójRuch = false;

        public Player()
        {
            amount++;
            id = amount;
        }
        public string Name
        {
            get => name;
            set
            {
                if (value == "") name = "Player " + id;
                else name = value;
            }
        }
        public bool TwójRuch
        {
            get => twójRuch;
            set => twójRuch = value;
        }
    }
}
