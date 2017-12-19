using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStringsCounter
{
   public struct Transition
    {
        public int DepartureIndex { get; set; }
        public int WordIndex { get; set; }
        public int DestinationIndex { get; set; }
        public bool CountAfterTransition { get; set; }
    }
}
