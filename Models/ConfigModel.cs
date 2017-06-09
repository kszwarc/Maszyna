using System;
using System.Text;

namespace Maszyna.Models
{
    class ConfigModel
    {
        public static String GenerateFormalSymbols(TuringMachine machine)
        {
            String sigmaSymbols = String.Join(", ", machine.Symbols);
            String delimeterForEverySymbolsFormulation = sigmaSymbols.Length != 0 ? ", " : "";
            String statesFormulation = "Q={" + GenerateStatesListForFormulation(machine.NumberOfStates) + "}";
            String symbolsSigmaFormulation = "Σ={" + sigmaSymbols + "}";
            String everySymbolsFormulation = "Γ={" + machine.EmptySymbol + delimeterForEverySymbolsFormulation + 
                sigmaSymbols + "}";
            String transitionFormulation = "δ:ΓxQ -> QxΓx{L,P,-}";
            String firstStateFormulation = "q={q" + machine.FirstStateIndex + "}";
            String emptySymbolFormulation = "B={" + machine.EmptySymbol + "}";
            String finalStatesFormulation = "F={" + String.Join(", ", machine.FinalStates) +"}";
            return statesFormulation + Environment.NewLine + symbolsSigmaFormulation +
                Environment.NewLine + everySymbolsFormulation + Environment.NewLine +
                transitionFormulation + Environment.NewLine + firstStateFormulation +
                Environment.NewLine + emptySymbolFormulation + Environment.NewLine +
                finalStatesFormulation;
        }

        public static String GenerateConditionsToShowSimulationTab(TuringMachine machine)
        {
            String beginningText = "Aby rozpocząć symulację musisz uzupełnić następujące pola: ";
            StringBuilder text = new StringBuilder(beginningText);

            String result = text.ToString();
            return result == beginningText ? "" : result;
        }

        private static String GenerateStatesListForFormulation(int numberOfStates)
        {
            const byte maximumNumberOfStatesToShow = 10;
            if (numberOfStates == 1)
                return "q" + (numberOfStates - 1);
            else if (numberOfStates < maximumNumberOfStatesToShow)
                return GenerateStatesListForMultipleStates(numberOfStates);
            else
                return "q0, q1, ..., "+ "q" + (numberOfStates - 1);
        }

        private static String GenerateStatesListForMultipleStates(int numberOfStates)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numberOfStates; i++)
                sb.Append("q" + i + ", ");
            return sb.Remove(sb.Length - 2, 2).ToString();
        }
    }
}
