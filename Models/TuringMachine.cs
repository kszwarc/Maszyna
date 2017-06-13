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
        public int ActualCharIndex { get; set; } = -1;
        private int _actualState;
        private StringBuilder _tape;
        private String _finishedStateSymbol;

        public ProgramResult ExecuteProgram(String tape)
        {
            BeginningTuringSetUp(tape);
            Boolean finished = false;
            while (!finished)
                finished = ExecuteStep();
            return new ProgramResult(_finishedStateSymbol, _tape.ToString());
        }

        public ProgramResult ExecuteStepNext(String tape)
        {
            BeginningTuringSetUp(tape);
            return ExecuteStepNext();
        }

        public ProgramResult ExecuteStepNext()
        {
            ExecuteStep();
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

        public bool isActualCharIndexLaterThanTape()
        {
            return ActualCharIndex >= _tape.Length;
        }

        private void BeginningTuringSetUp(String tape)
        {
            _tape = new StringBuilder(tape);
            _actualState = FirstStateIndex;
            SetHeadBeginningPosition();
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
            _tape[ActualCharIndex] = actualTransition.ExitSymbol[0];
            _actualState = actualTransition.NextStateNumber;
            UpdateActualCharIndex(actualTransition.Movement);
            UpdateTapeIfNeeded();
            return false;
        }

        private void UpdateTapeIfNeeded()
        {
            if (ActualCharIndex < 0)
            {
                _tape.Insert(0, EmptySymbol);
                ActualCharIndex = 0;
            }
            else if (ActualCharIndex >= _tape.Length)
                _tape.Append(EmptySymbol);
        }

        private void UpdateActualCharIndex(Movement movement)
        {
            if (movement == Movement.Left)
                ActualCharIndex--;
            else if (movement == Movement.Right)
                ActualCharIndex++;
        }

        private Transition FindActualTransition()
        {
            char actualEntrySymbol = _tape[ActualCharIndex];
            Transition actualTransition = Transitions.Find(t => t.EntrySymbol == actualEntrySymbol &&
            t.StateNumber == _actualState);
            return actualTransition;
        }

        private void SetHeadBeginningPosition()
        {
            if (HeadPosition == TuringHeadPosition.FirstSymbolFromLeft)
                ActualCharIndex = 0;
            else
                ActualCharIndex = _tape.Length - 1;
        }
    }
}
