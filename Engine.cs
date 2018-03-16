using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Engine
    {
        public Engine()
        {
            GenerateMatchboxes();
        }
        List<Matchbox> _matchboxes = new List<Matchbox>();

        void GenerateMatchboxes()
        {
            int total = (int)Math.Pow(3, 9);
            
            for (int i = 0; i < total; i++)
            {
                string preamble = Int32ToString(i, 3);
                if (Math.Abs(preamble.Count(t => t == '1') - preamble.Count(t => t == '2')) <= 1)
                {
                    var state = new State(preamble);
                    // if (!_matchboxes.Any(t => t.IsState(state)))
                        _matchboxes.Add(new Matchbox(state));
                }
            }
        }

        public static string Int32ToString(int value, int toBase)
        {
            string result = string.Empty;
            do
            {
                result = "012"[value % toBase] + result;
                value /= toBase;
            }
            while (value > 0);

            return result;
        }

        public int GetDecision(State state, int rand)
        {
            foreach(var matchbox in _matchboxes)
            {
                if (matchbox.IsState(state))
                {
                    return matchbox.MakeDecision(rand);
                }
            }
            return -1;
        }
    }
}
