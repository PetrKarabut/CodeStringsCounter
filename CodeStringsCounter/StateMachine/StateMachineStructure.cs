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
        public int startStateIndex { get; set; }
        public int defaultWordIndex { get; set; }
        public int newLineWordIndex { get; set; }

        public void AddTransition(string from, string word, string to, bool count)
        {
            Transition transition = new Transition();
            transition.DepartureIndex = States.IndexOf(from);
            transition.WordIndex = Words.IndexOf(word);
            transition.DestinationIndex = States.IndexOf(to);
            transition.CountAfterTransition = count;

            Transitions.Add(transition);
        }
    }
}
