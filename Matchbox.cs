using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Matchbox
    {
        string _token;
        State _state;
        List<int> _choices;

        public Matchbox(State state)
        {
            _state = state;
            _token = state.ToString();
            _choices = new List<int>();

            InitChoices();
        }

        public string Token
        {
            get
            {
                return _token;
            }
        }

        private void InitChoices()
        {
            for (int i = 0; i < 9; i++)
            {
                if (_state.CanGo(i))
                {
                    _choices.Add(i);
                    _choices.Add(i);
                }
            }
        }

        public Transform? GetTransform(State state)
        {
            return State.GetTransform(state, _state);
        }

        public int MakeDecision(int rand)
        {
            return _choices[rand % _choices.Count];
        }

        public void RewardDecision(int decision, int reward)
        {
            switch (reward)
            {
                case -1:
                    for (int i = 0; i < _choices.Count; i++)
                    {
                        if (_choices[i] == decision)
                        {
                            _choices.RemoveAt(i);
                            break;
                        }
                    }
                    break;
                case 0:
                    _choices.Add(decision);
                    break;
                case 1:
                    _choices.Add(decision);
                    _choices.Add(decision);
                    _choices.Add(decision);
                    break;
            }

            if(_choices.Count == 0)
            {
                InitChoices();
            }
        }

        public override string ToString()
        {
            return _state.ToString();
        }
    }
}
