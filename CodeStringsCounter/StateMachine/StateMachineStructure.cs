using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStringsCounter
{
    public class StateMachineStructure
    {
        public List<string> States { get; set; }
        public List<string> Words { get; set; }
        public List<Transition> Transitions { get; set; }
        public int StartStateIndex { get; set; }
        public int DefaultWordIndex { get; set; }
        public int NewLineWordIndex { get; set; }

        public void AddTransition(string from, string word, string to, bool count)
        {
            var transition = new Transition()
            {
                DepartureIndex = States.IndexOf(from),
                WordIndex = Words.IndexOf(word),
                DestinationIndex = States.IndexOf(to),
                CountAfterTransition = count
            };

            Transitions.Add(transition);
        }
    }
}
