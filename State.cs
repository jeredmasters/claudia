using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    enum Piece
    {
        Empty = 0,
        Naught = 1,
        Cross = 2
    }

    internal class State
    {
        Piece[] _board;

        public State(string board)
        {
            _board = new Piece[9];
            for (int i = 0; i < 9; i++)
            {
                _board[i] = Piece.Empty;
            }

            for (int i = 0; i < board.Length; i++)
            {
                switch (board[i])
                {
                    case '0':
                        _board[i] = Piece.Empty;
                        break;
                    case '1':
                        _board[i] = Piece.Naught;
                        break;
                    case '2':
                        _board[i] = Piece.Cross;
                        break;
                    default:
                        _board[i] = Piece.Empty;
                        break;
                }
            }
        }

        public State()
        {
            _board = new Piece[9];
            for (int i = 0; i < 9; i++)
            {
                _board[i] = Piece.Empty;
            }
        }

        public State(Piece[] board)
        {
            _board = board;
        }

        public Piece Get(int x, int y)
        {
            return _board[x + 3 * y];
        }

        public Piece Get(int i)
        {
            return _board[i];
        }

        public bool CanGo(int position)
        {
            return _board[position] == Piece.Empty;
        }

        public bool Place(int position, Piece piece)
        {
            if(CanGo(position))
            {
                _board[position] = piece;
                return true;
            }
            return false;
        }

        public Piece HasWinner()
        {
            for(int x = 0; x < 3; x++)
            {
                if (Get(x, 0) == Get(x, 1) && Get(x, 0) == Get(x, 2))
                {
                    return Get(x, 0);
                }
            }

            for (int y = 0; y < 3; y++)
            {
                if (Get(0, y) == Get(1, y) && Get(0, y) == Get(2, y))
                {
                    return Get(0, y);
                }
            }

            if (Get(0) == Get(4) && Get(0) == Get(8))
            {
                return Get(0);
            }

            if (Get(2) == Get(4) && Get(2) == Get(6))
            {
                return Get(2);
            }

            return Piece.Empty;
        }

        public State Rotate()
        {
            Piece[] temp = new Piece[9];

            temp[0] = _board[6];
            temp[1] = _board[3];
            temp[2] = _board[0];
            temp[3] = _board[7];
            temp[4] = _board[4];
            temp[5] = _board[1];
            temp[6] = _board[8];
            temp[7] = _board[5];
            temp[8] = _board[2];

            return new State(temp);
        }

        public State FlipHorizontal()
        {
            Piece[] temp = new Piece[9];

            temp[0] = _board[2];
            temp[1] = _board[1];
            temp[2] = _board[0];
            temp[3] = _board[5];
            temp[4] = _board[4];
            temp[5] = _board[3];
            temp[6] = _board[8];
            temp[7] = _board[7];
            temp[8] = _board[6];

            return new State(temp);
        }

        public State FlipVertical()
        {
            Piece[] temp = new Piece[9];

            temp[0] = _board[6];
            temp[1] = _board[7];
            temp[2] = _board[8];
            temp[3] = _board[3];
            temp[4] = _board[4];
            temp[5] = _board[5];
            temp[6] = _board[0];
            temp[7] = _board[1];
            temp[8] = _board[2];

            return new State(temp);
        }

        public override string ToString()
        {
            return String.Join("", _board.Select(t => (int)t));
        }

        private static bool Compare(State a, State b)
        {
            for (int r = 0; r < 3; r++)
            {
                bool same = true;
                for (int i = 0; i < 3; i++)
                {
                    if (a._board[i] != b._board[i])
                    {
                        same = false;
                        break;
                    }
                }
                if (same)
                    return true;
                b = b.Rotate();
            }
            return false;
        }

        public static bool operator ==(State a, State b)
        {
            if (Compare(a, b))
                return true;
            b = b.FlipHorizontal();
            if (Compare(a, b))
                return true;
            b = b.FlipVertical();
            if (Compare(a, b))
                return true;

            return false;
        }
        public static bool operator !=(State a, State b)
        {
            return !(a == b);
        }
    }
}
