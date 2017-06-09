using System;

namespace Maszyna.Models
{
    class Validator
    {
        public static Boolean AreEntryDataForMachineValid(String text, TuringMachine turingMachine)
        {
            foreach (char symbol in text)
                if (turingMachine.EmptySymbol != symbol && !turingMachine.Symbols.Contains(symbol.ToString()))
                    return false;
            return true;
        }

        public static Boolean IsTransitionValid(String text, TuringMachine turingMachine)
        {
            String[] parts = text.Split('/');
            if (parts.Length != 3)
                return false;
            if (parts[0].Length != 1 || !IsEntrySymbolValid(parts[0][0], turingMachine))
                return false;
            if (!IsNewSymbolValid(parts[1], turingMachine))
                return false;
            if (parts[2].Length != 1 || !IsTransitionSymbolValid(parts[2][0]))
                return false;
            return true;
        }

        private static Boolean IsTransitionSymbolValid(char symbol)
        {
            return symbol == 'P' || symbol == 'L' || symbol == '-';
        }

        private static Boolean IsNewSymbolValid(String newText, TuringMachine turingMachine)
        {
            Boolean isInFinalStates = turingMachine.FinalStates.Contains(newText);
            return isInFinalStates || IsEntrySymbolValid(newText[0], turingMachine);
        }

        private static Boolean IsEntrySymbolValid(char symbol, TuringMachine turingMachine)
        {
            Boolean isEmptySymbol = turingMachine.EmptySymbol == symbol;
            Boolean isInAlphabet = turingMachine.Symbols.Contains(symbol.ToString());
            return isEmptySymbol || isInAlphabet;
        }
        
    }
}
