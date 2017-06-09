using System;

namespace Maszyna.Models
{
    public struct Transition
    {
        public int StateNumber { get; }
        public int NextStateNumber { get; }
        public char EntrySymbol { get; }
        public String ExitSymbol { get; }
        public Movement Movement { get; }

        public Transition(int stateNumber, int nextStateNumber, char entrySymbol, String exitSymbol, Movement movement)
        {
            StateNumber = stateNumber;
            NextStateNumber = nextStateNumber;
            EntrySymbol = entrySymbol;
            ExitSymbol = exitSymbol;
            Movement = movement;
        }
    }
}
