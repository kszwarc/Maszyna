using System;

namespace Maszyna.Models
{
    public struct ProgramResult
    {
        public String FinishedStateSymbol { get; }
        public String Tape { get; }

        public ProgramResult(String finishedStateSymbol, String tape)
        {
            FinishedStateSymbol = finishedStateSymbol;
            Tape = tape;
        }
    }
}
