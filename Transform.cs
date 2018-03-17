using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public struct Transform
    {
        public bool Flip;
        public int Rotate;

        public override string ToString()
        {
            return (Flip ? 'F' : '_') + " " + Rotate.ToString();
        }
    }
}
