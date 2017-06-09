using System;
using System.Collections.Generic;

namespace Maszyna.Models
{
    class TuringMachine
    {
        public char EmptySymbol { get; set; }
        public int NumberOfStates { get; set; }
        public int FirstStateIndex { get; set; }
        public TuringHeadPosition HeadPosition { get; set; }
        public List<String> Symbols { get; set; } = new List<String>();
        public List<String> FinalStates { get; set; } = new List<String>();
        public List<Transition> Transitions { get; set; } = new List<Transition>();
        public List<PotentialTransition> PotentialTransitions { get; set; } = new List<PotentialTransition>();
        public String actualTape { get; set; }
        private static long MaximumExecutionTimeInMs = 10000;
        private int _actualState;

        public void executeProgram()
        {

        }

        public void GenerateTransitionsFromPotential()
        {
            foreach (PotentialTransition potentialTransition in PotentialTransitions)
            {
                Transition transitionToAdd = new Transition(potentialTransition.StateNumber, 
                    potentialTransition.GetNextStateNumberFromInstruction(), potentialTransition.EntrySymbol,
                    potentialTransition.GetNewSymbolFromInstruction(), potentialTransition.GetMovementFromInstruction());
                Transitions.Add(transitionToAdd);
            }
        }
    }
}
