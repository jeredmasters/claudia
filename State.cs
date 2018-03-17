using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum Piece
    {
        Empty = 0,
        Naught = 1,
        Cross = 2,
        Draw = -1
    }

    public class State
    {
        Piece[] _board;


        public State()
        {
            _board = new Piece[9];
            for (int i = 0; i < 9; i++)
            {
                _board[i] = Piece.Empty;
            }
        }

        public State(string board)
        {
            _board = new Piece[9];
            for (int i = 0; i < 9; i++)
            {
                _board[i] = Piece.Empty;
            }

            for (int i = 0; i < board.Length; i++)
            {
                int j = 8 - i;
                switch (board[i])
                {
                    case '0':
                        _board[j] = Piece.Empty;
                        break;
                    case '1':
                        _board[j] = Piece.Naught;
                        break;
                    case '2':
                        _board[j] = Piece.Cross;
                        break;
                    default:
                        _board[j] = Piece.Empty;
                        break;
                }
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
            throw new Exception("Cannot place at: " + position);
        }

        public bool Valid()
        {
            int o = 0;
            int x = 0;

            for(int i = 0; i < 9; i++)
            {
                switch (_board[i])
                {
                    case Piece.Cross:
                        o++;
                        break;
                    case Piece.Naught:
                        x++;
                        break;
                }
            }

            return Math.Abs(o - x) <= 1;
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

            // If there's any empty squares then the game is still going
            for (int i = 0; i < 9; i++)
            {
                if (_board[i] == Piece.Empty)
                {
                    return Piece.Empty;
                }
            }

            // If there's no empty squares, then it's a draw
            return Piece.Draw;
        }

        public State RotateRight()
        {
            Piece[] temp = new Piece[9];

            for(int i = 0; i < 9; i++)
            {
                temp[Util.RotateRight(i)] = _board[i];
            }

            return new State(temp);
        }

        public State FlipHorizontal()
        {
            Piece[] temp = new Piece[9];

            for (int i = 0; i < 9; i++)
            {
                temp[i] = _board[Util.Flip(i)];
            }

            return new State(temp);
        }

        public State Clone()
        {
            Piece[] temp = new Piece[9];

            for (int i = 0; i < 9; i++)
            {
                temp[i] = _board[i];
            }

            return new State(temp);
        }

        public State Invert()
        {
            Piece[] temp = new Piece[9];

            for (int i = 0; i < 9; i++)
            {
                switch (_board[i])
                {
                    case Piece.Naught:
                        temp[i] = Piece.Cross;
                        break;
                    case Piece.Cross:
                        temp[i] = Piece.Naught;
                        break;
                    default:
                        temp[i] = _board[i];
                        break;
                }
            }

            return new State(temp);
        }

        public override string ToString()
        {
            return String.Join("", _board.Select(t => (int)t));
        }


        public static Transform? GetTransform(State a, State b)
        {
            State c = b.Clone();
            for (int r = 0; r < 4; r++)
            {
                if (a == c)
                    return new Transform() { Rotate = r, Flip = false };
                c = c.RotateRight();
            }
            c = b.FlipHorizontal();
            for (int r = 0; r < 4; r++)
            {
                if (a == c)
                    return new Transform() { Rotate = r, Flip = true };
                c = c.RotateRight();
            }
            return null;
        }

        public static bool operator ==(State a, State b)
        {
            for (int i = 0; i < 9; i++)
            {
                if (a._board[i] != b._board[i])
                    return false;
            }
            return true;
        }
        public static bool operator !=(State a, State b)
        {
            return !(a == b);
        }
    }
}
