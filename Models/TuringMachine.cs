using System;
using System.Collections.Generic;

namespace Maszyna.Models
{
    class TuringMachine
    {
        public char EmptySymbol { get; set; }
        public List<String> Symbols { get; set; } = new List<String>();
        public int NumberOfStates { get; set; }
        public int FirstStateIndex { get; set; }
        public List<String> FinalStates { get; set; } = new List<String>();
        public TuringHeadPosition HeadPosition { get; set; }
        public List<Transition> Transitions { get; set; }
        public List<String> PotentialTransitions { get; set; } = new List<String>();
    }
}
