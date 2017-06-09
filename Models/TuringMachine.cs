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
        private int _actualCharIndex, _actualState;
        private String _tape, _finishedStateSymbol;

        public ProgramResult ExecuteProgram(String tape)
        {
            _tape = tape;
            _actualState = FirstStateIndex;
            SetHeadBeginningPosition();
            Boolean finished = false;
            while (!finished)
                finished = ExecuteStep();
            return new ProgramResult(_finishedStateSymbol, _tape);
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

        private Boolean ExecuteStep()
        {
            Transition actualTransition = FindActualTransition();
            return false;
        }

        private Transition FindActualTransition()
        {
            char actualEntrySymbol = _tape[_actualCharIndex];
            Transition actualTransition = Transitions.Find(t => t.EntrySymbol == actualEntrySymbol &&
            t.StateNumber == _actualState);
            return actualTransition;
        }

        private void SetHeadBeginningPosition()
        {
            if (HeadPosition == TuringHeadPosition.FirstSymbolFromLeft)
                _actualCharIndex = 0;
            else
                _actualCharIndex = _tape.Length - 1;
        }
    }
}
