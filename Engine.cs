using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Engine
    {
        public Engine()
        {
            //GenerateMatchboxes();
        }
        List<Matchbox> _matchboxes = new List<Matchbox>();
        List<Decision> _decisions = new List<Decision>();
        public Matchbox _lastMatchbox;
        public Transform? _lastTransform;
        void GenerateMatchboxes()
        {
            int total = (int)Math.Pow(3, 9);
            
            for (int i = 0; i < total; i++)
            {
                string preamble = Int32ToString(i, 3);
                int ones = preamble.Count(t => t == '1');
                int twos = preamble.Count(t => t == '2');
                if (Math.Abs(ones - twos) <= 1)
                {
                    var state = new State(preamble);
                    if (!_matchboxes.Any(t => t.GetTransform(state) != null))
                        _matchboxes.Add(new Matchbox(state));
                }
            }
            //_matchboxes.Sort((a, b) => b.Token.CompareTo(a.Token));
        }

        public static string Int32ToString(int value, int toBase)
        {
            string result = string.Empty;
            while (value > 0)
            {
                result = "012"[value % toBase] + result;
                value /= toBase;
            }

            while(result.Length < 9)
            {
                result = '0' + result;
            }
            

            return result;
        }

        public int GetDecision(State state, int rand, bool record = true)
        {
            int position, i;
            for (i = 0; i < _matchboxes.Count; i++)
            {
                _lastTransform = _matchboxes[i].GetTransform(state);
                if (_lastTransform != null)
                {
                    _lastMatchbox = _matchboxes[i];
                    position = _matchboxes[i].MakeDecision(rand);
                    if (record) _decisions.Add(new Decision(){ MatchboxIndex = i, Position = position});
                    return Util.ApplyTransform(position, (Transform)_lastTransform);
                }
            }
            i = _matchboxes.Count;
            _matchboxes.Add(new Matchbox(state.Clone()));
            _lastMatchbox = _matchboxes[i];
            position = _matchboxes[i].MakeDecision(rand);
            if (record) _decisions.Add(new Decision() { MatchboxIndex = i, Position = position });
            return position;
        }

        public void RewardDecisions(int reward)
        {
            foreach(var decision in _decisions)
            {
                _matchboxes[decision.MatchboxIndex].RewardDecision(decision.Position, reward);
            }
            _decisions.Clear();
        }
    }
}
