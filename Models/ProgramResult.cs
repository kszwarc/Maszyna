using System;

namespace Maszyna.Models
{
    public struct ProgramResult
    {
        public String FinishedStateSymbol { get; }
        public String Tape { get; }
        public int StateIndex { get; }
        public int SymbolIndex { get; }

        public ProgramResult(String finishedStateSymbol, String tape, int stateIndex, int symbolIndex)
        {
            FinishedStateSymbol = finishedStateSymbol;
            Tape = tape;
            StateIndex = stateIndex;
            SymbolIndex = symbolIndex;
        }
    }
}
