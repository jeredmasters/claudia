using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Matchbox
    {
        string _name;
        State _state;
        List<int> _choices;

        public Matchbox(State state)
        {
            _state = state;
            _name = state.ToString();
            _choices = new List<int>();

            for(int i = 0; i < 9; i++)
            {
                if (_state.CanGo(i))
                {
                    _choices.Add(i);
                    _choices.Add(i);
                }
            }
        }

        public bool IsState(State state)
        {
            return state == _state;
        }

        public int MakeDecision(int rand)
        {
            return _choices[rand % _choices.Count];
        }

        public void RewardDecision(int decision, bool good)
        {
            if (good)
            {
                _choices.Add(decision);
                _choices.Add(decision);
                _choices.Add(decision);
            }
            else
            {
                for(int i = 0; i < _choices.Count; i++)
                {
                    if (_choices[i] == decision)
                    {
                        _choices.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
