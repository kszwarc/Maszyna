using System;

namespace Maszyna.Models
{
    public class PotentialTransition
    {
        public String Instruction { get; }
        public int StateNumber { get; }
        public char EntrySymbol { get; }

        public PotentialTransition(String instruction, int stateNumber, char entrySymbol)
        {
            Instruction = instruction;
            StateNumber = stateNumber;
            EntrySymbol = entrySymbol;
        }

        public int GetNextStateNumberFromInstruction()
        {
            return int.Parse(Instruction.Split('/')[0].Substring(1));
        }

        public String GetNewSymbolFromInstruction()
        {
            return Instruction.Split('/')[1];
        }

        public Movement GetMovementFromInstruction()
        {
            char movementSymbol = Instruction.Split('/')[2][0];
            return SimpleFactoryForMovement(movementSymbol);
        }

        private Movement SimpleFactoryForMovement(char movementSymbol)
        {
            if (movementSymbol == 'L')
                return Movement.Left;
            if (movementSymbol == 'P')
                return Movement.Right;
            else
                return Movement.None;
        }
    }
}
