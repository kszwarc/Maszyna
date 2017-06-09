using System;
using System.Text;

namespace Maszyna.Models
{
    class ConfigModel
    {
        public static String GenerateFormalSymbols(TuringMachine machine)
        {
            String sigmaSymbols = String.Join(", ", machine.Symbols);
            String delimeterForEverySymbolsFormulation = sigmaSymbols.Length != 0 && machine.EmptySymbol!=' ' ? ", " : "";
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
            String beginningText = "Aby rozpocząć symulację musisz wprowadzić: ";
            StringBuilder text = new StringBuilder(beginningText);
            if (machine.EmptySymbol == ' ')
                text.Append("symbol pusty");
            if (machine.Symbols.Count == 0)
                text.Append(GenerateTextWithDelimeterIfNeeded(beginningText, text, "symbole wejściowe"));
            if (machine.FinalStates.Count == 0)
                text.Append(GenerateTextWithDelimeterIfNeeded(beginningText, text, "stany końcowe"));
            String result = text.ToString();
            return result == beginningText ? "" : result+".";
        }

        public static Boolean ShouldSimulationTabBeVisible(TuringMachine machine)
        {
            return !(machine.EmptySymbol == ' ' || machine.Symbols.Count == 0 || machine.FinalStates.Count == 0);
        }

        private static String GenerateTextWithDelimeterIfNeeded(String beginningText, StringBuilder actualText, String text)
        {
            return beginningText == actualText.ToString() ? text : ", " + text;
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
