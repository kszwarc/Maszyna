using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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
        public Color ActualSymbolColor { get; set; } = Color.Red;
        public Color ActualStateColor { get; set; } = Color.LightBlue;
        private int _actualState;
        private Transition _lastTransition;
        private bool _forceToFinish = false;
        private StringBuilder _tape;
        private String _finishedStateSymbol;

        public ProgramResult ExecuteProgram(String tape)
        {
            BeginningTuringSetUp(tape);
            Boolean finished = false;
            while (!finished && !_forceToFinish)
                finished = ExecuteStep();
            return new ProgramResult(_finishedStateSymbol, _tape.ToString(), _actualState, 
                GetSymbolIndex(new String(_lastTransition.EntrySymbol, 1)));
        }

        public ProgramResult ExecuteStepNext(String tape)
        {
            BeginningTuringSetUp(tape);
            return ExecuteStepNext();
        }

        public ProgramResult ExecuteStepNext()
        {
            ExecuteStep();
            return new ProgramResult(_finishedStateSymbol, _tape.ToString(), _actualState, 
                GetSymbolIndex(new String(_lastTransition.EntrySymbol, 1)));
        }

        public void GenerateTransitionsFromPotential()
        {
            Transitions.Clear();
            foreach (PotentialTransition potentialTransition in PotentialTransitions)
            {
                Transition transitionToAdd = GenerateTransitionToAdd(potentialTransition);
                Transitions.Add(transitionToAdd);
            }
        }

        private Transition GenerateTransitionToAdd(PotentialTransition potentialTransition)
        {
            if (potentialTransition.IsFinalState)
                return new Transition(potentialTransition.StateNumber,
                    0, potentialTransition.EntrySymbol, potentialTransition.Instruction, Movement.None);
            else
                return new Transition(potentialTransition.StateNumber,
                    potentialTransition.GetNextStateNumberFromInstruction(), potentialTransition.EntrySymbol,
                    potentialTransition.GetNewSymbolFromInstruction(), potentialTransition.GetMovementFromInstruction());
        }

        public void ForceToFinishExecution()
        {
            _forceToFinish = true;
        }

        public bool IsActualCharIndexLaterThanTape()
        {
            return ActualCharIndex >= _tape.Length;
        }

        private void BeginningTuringSetUp(String tape)
        {
            _forceToFinish = false;
            _tape = new StringBuilder(tape);
            _finishedStateSymbol = "";
            _actualState = FirstStateIndex;
            SetHeadBeginningPosition();
        }

        private Boolean ExecuteStep()
        {
            _lastTransition = FindActualTransition();
            Boolean isFinishedState = FinalStates.Contains(_lastTransition.ExitSymbol);
            if (isFinishedState)
            {
                _finishedStateSymbol = _lastTransition.ExitSymbol;
                return true;
            }
            _tape[ActualCharIndex] = _lastTransition.ExitSymbol[0];
            _actualState = _lastTransition.NextStateNumber;
            UpdateActualCharIndex(_lastTransition.Movement);
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

        private int GetSymbolIndex(String symbol)
        {
            const byte EmptySymbolRowIndex = 0;
            const byte RowReservedForEmptySymbols = 1;
            if (EmptySymbol == symbol[0])
                return EmptySymbolRowIndex;
            else
                return Symbols.IndexOf(symbol) + RowReservedForEmptySymbols;
        }
    }
}
