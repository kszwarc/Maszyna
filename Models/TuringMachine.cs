using System;

namespace Maszyna.Models
{
    class TuringMachine
    {
        public char EmptySymbol { get; set; }
        public String[] Symbols { get; set; } = new String[0];
        public int NumberOfStates { get; set; }
        public int FirstStateIndex { get; set; }
        public String[] FinalStates { get; set; } = new String[0];
        public TuringHeadPosition HeadPosition { get; set; }
    }
}
