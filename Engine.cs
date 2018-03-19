using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Engine
    {
        public Engine(Random rnd)
        {
            _rnd = rnd;
        }
        Random _rnd;
        List<Matchbox> _matchboxes = new List<Matchbox>();
        List<Decision> _decisions = new List<Decision>();
        List<Decision> _decisions_adversary = new List<Decision>();
        public Matchbox _lastMatchbox;
        public Transform? _lastTransform;
        void GenerateMatchboxes()
        {
            int total = (int)Math.Pow(3, 9);
            
            for (int i = 0; i < total; i++)
            {
                string preamble = Util.Int32ToString(i, 3);
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


        public int GetDecision(State state, bool record = true)
        {
            int position, i;
            for (i = 0; i < _matchboxes.Count; i++)
            {
                _lastTransform = _matchboxes[i].GetTransform(state);
                if (_lastTransform != null)
                {
                    _lastMatchbox = _matchboxes[i];
                    position = _matchboxes[i].MakeDecision(_rnd);
                    if (record) _decisions.Add(new Decision(){ MatchboxIndex = i, Position = position});
                    else _decisions_adversary.Add(new Decision() { MatchboxIndex = i, Position = position });
                    return Util.ApplyTransform(position, (Transform)_lastTransform);
                }
            }
            i = _matchboxes.Count;
            _matchboxes.Add(new Matchbox(state.Clone()));
            _lastMatchbox = _matchboxes[i];
            position = _matchboxes[i].MakeDecision(_rnd);
            if (record) _decisions.Add(new Decision() { MatchboxIndex = i, Position = position });
            else _decisions_adversary.Add(new Decision() { MatchboxIndex = i, Position = position });
            return position;
        }

        public void RewardDecisions(int reward)
        {
            foreach(var decision in _decisions)
            {
                _matchboxes[decision.MatchboxIndex].RewardDecision(decision.Position, reward);
            }
            _decisions.Clear();

            foreach (var decision in _decisions_adversary)
            {
                _matchboxes[decision.MatchboxIndex].RewardDecision(decision.Position, reward * -1);
            }
            _decisions_adversary.Clear();
        }
    }
}
