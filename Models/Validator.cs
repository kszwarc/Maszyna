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
            if (parts[0].Length < 2 || !IsNextStateValid(parts[0], turingMachine))
                return false;
            if (!IsNewSymbolValid(parts[1], turingMachine))
                return false;
            if (parts[2].Length != 1 || !IsTransitionSymbolValid(parts[2][0]))
                return false;
            return true;
        }

        private static Boolean IsNextStateValid(String nextState, TuringMachine turingMachine)
        {
            int nextStateNumber;
            String stateNumber = nextState.Substring(1);
            if (nextState[0] != 'q')
                return false;
            else if (!int.TryParse(stateNumber, out nextStateNumber))
                return false;
            else if (nextStateNumber >= turingMachine.NumberOfStates)
                return false;
            return true;
        }

        private static Boolean IsTransitionSymbolValid(char symbol)
        {
            return symbol == 'P' || symbol == 'L' || symbol == '-';
        }

        private static Boolean IsNewSymbolValid(String newText, TuringMachine turingMachine)
        {
            Boolean isEmptySymbol = turingMachine.EmptySymbol == newText[0];
            Boolean isInAlphabet = turingMachine.Symbols.Contains(newText);
            Boolean isInFinalStates = turingMachine.FinalStates.Contains(newText);
            return  isEmptySymbol || isInAlphabet || isInFinalStates;
        }
        
    }
}
