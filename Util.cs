using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Util
    {


        public static int ApplyTransform(int position, Transform transform)
        {
            if (transform.Flip)
            {
                position = Flip(position);
            }

            for (int i = 0; i < transform.Rotate; i++)
            {
                position = RotateRight(position);
            }
            return position;
        }

        public static int RotateLeft(int position)
        {
            switch (position)
            {
                case 0: return 6;
                case 1: return 3;
                case 2: return 0;
                case 3: return 7;
                case 4: return 4;
                case 5: return 1;
                case 6: return 8;
                case 7: return 5;
                case 8: return 2;
            }
            return -1;
        }

        public static int RotateRight(int position)
        {
            switch (position)
            {
                case 0: return 2;
                case 1: return 5;
                case 2: return 8;
                case 3: return 1;
                case 4: return 4;
                case 5: return 7;
                case 6: return 0;
                case 7: return 3;
                case 8: return 6;
            }
            return -1;
        }

        public static int Flip(int position)
        {
            switch (position)
            {
                case 0: return 2;
                case 1: return 1;
                case 2: return 0;
                case 3: return 5;
                case 4: return 4;
                case 5: return 3;
                case 6: return 8;
                case 7: return 7;
                case 8: return 6;
            }
            return -1;
        }
    }


}
