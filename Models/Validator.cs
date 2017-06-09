using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maszyna.Models
{
    class Validator
    {
        public static Boolean areEntryDataForMachineValid(String text, TuringMachine turingMachine)
        {
            foreach (char symbol in text)
                if (turingMachine.EmptySymbol != symbol && !turingMachine.Symbols.Contains(symbol.ToString()))
                    return false;
            return true;
        }
    }
}
