using System;
using System.Collections.Generic;
using System.Text;

namespace Maszyna.Models
{
    public class TuringMachine
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
        private StringBuilder _tape;
        private String _finishedStateSymbol;

        public ProgramResult ExecuteProgram(String tape)
        {
            _tape = new StringBuilder(tape);
            _actualState = FirstStateIndex;
            SetHeadBeginningPosition();
            Boolean finished = false;
            while (!finished)
                finished = ExecuteStep();
            return new ProgramResult(_finishedStateSymbol, _tape.ToString());
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
            Boolean isFinishedState = FinalStates.Contains(actualTransition.ExitSymbol);
            if (isFinishedState)
            {
                _finishedStateSymbol = actualTransition.ExitSymbol;
                return true;
            }
            _tape[_actualCharIndex] = actualTransition.ExitSymbol[0];
            _actualState = actualTransition.NextStateNumber;
            UpdateActualCharIndex(actualTransition.Movement);
            UpdateTapeIfNeeded();
            return false;
        }

        private void UpdateTapeIfNeeded()
        {
            if (_actualCharIndex < 0)
            {
                _tape.Insert(0, EmptySymbol);
                _actualCharIndex = 0;
            }
            else if (_actualCharIndex >= _tape.Length)
                _tape.Append(EmptySymbol);
        }

        private void UpdateActualCharIndex(Movement movement)
        {
            if (movement == Movement.Left)
                _actualCharIndex--;
            else if (movement == Movement.Right)
                _actualCharIndex++;
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
