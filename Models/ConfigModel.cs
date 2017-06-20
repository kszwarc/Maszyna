using System;
using System.Collections.Generic;
using System.Text;

namespace Maszyna.Models
{
    class ConfigModel
    {
        private static String beginningTextForSimulationAlert = "Aby rozpocząć symulację musisz wprowadzić: ";
        private static String textForInvalidStateTable = "tabelę stanów";

        public static String GenerateFormalSymbols(TuringMachine machine)
        {
            String sigmaSymbols = String.Join(", ", machine.Symbols);
            String delimeterForEverySymbolsFormulation = sigmaSymbols.Length != 0 && machine.EmptySymbol!=' ' ? ", " : "";
            String statesFormulation = "Q={" + GenerateStatesListForFormulation(machine.NumberOfStates) + "}";
            String symbolsSigmaFormulation = "Σ={" + sigmaSymbols + "}";
            String everySymbolsFormulation = "Γ={" + machine.EmptySymbol + delimeterForEverySymbolsFormulation + 
                sigmaSymbols + "}";
            String transitionFormulation = "δ:ΓxQ -> QxΓx{L,P,-}";
            String firstStateFormulation = "q0={q" + machine.FirstStateIndex + "}";
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
            StringBuilder text = new StringBuilder(beginningTextForSimulationAlert);
            if (machine.EmptySymbol == ' ')
                text.Append("symbol pusty");
            if (machine.Symbols.Count == 0)
                text.Append(GenerateTextWithDelimeterIfNeeded(text, "symbole wejściowe"));
            if (machine.FinalStates.Count == 0)
                text.Append(GenerateTextWithDelimeterIfNeeded(text, "stany końcowe"));
            if (machine.PotentialTransitions.Count == 0 || !AreTransitionsValid(machine))
                text.Append(GenerateTextWithDelimeterIfNeeded(text, textForInvalidStateTable));
            String result = text.ToString();
            return result == beginningTextForSimulationAlert ? "" : result+".";
        }

        public static Boolean ShouldSimulationTabBeVisible(TuringMachine machine)
        {
            return GenerateConditionsToShowSimulationTab(machine) == "";
        }

        public static Boolean IsStateTableInvalid(String textForSimulationAlert)
        {
            return textForSimulationAlert.Contains(textForInvalidStateTable);
        }

        public static List<int> FindInvalidTransitionsIndex(TuringMachine turingMachine)
        {
            List<int> indexes = new List<int>();
            List<PotentialTransition> potentialTransitions = turingMachine.PotentialTransitions;
            for (int i = 0; i < potentialTransitions.Count; i++)
                if (!Validator.IsTransitionInstructionValid(potentialTransitions[i].Instruction, turingMachine))
                    indexes.Add(i);
            return indexes;
        }

        private static String GenerateTextWithDelimeterIfNeeded(StringBuilder actualText, String text)
        {
            return beginningTextForSimulationAlert == actualText.ToString() ? text : ", " + text;
        }

        private static String GenerateStatesListForFormulation(int numberOfStates)
        {
            const byte maximumNumberOfStatesToShow = 10;
            if (numberOfStates == 1)
                return "q" + (numberOfStates - 1);
            else if (numberOfStates < 1)
                return "";
            else if (numberOfStates < maximumNumberOfStatesToShow)
                return GenerateStatesListForMultipleStates(numberOfStates);
            else
                return "q0, q1, ..., " + "q" + (numberOfStates - 1);
        }

        private static String GenerateStatesListForMultipleStates(int numberOfStates)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numberOfStates; i++)
                sb.Append("q" + i + ", ");
            return sb.Remove(sb.Length - 2, 2).ToString();
        }

        private static Boolean AreTransitionsValid(TuringMachine turingMachine)
        {
            foreach (PotentialTransition transition in turingMachine.PotentialTransitions)
                if (!Validator.IsTransitionInstructionValid(transition.Instruction, turingMachine))
                    return false;
            return true;
        }
    }
}
