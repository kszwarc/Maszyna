using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Maszyna.Models
{
    class DataExportQuery
    {
        public static bool LoadFile(String filePath, ref TuringMachine turingMachine)
        {
            FileStream readStream = null;
            try
            {
                readStream = new FileStream(filePath, FileMode.Open);
                BinaryReader readBinary = new BinaryReader(readStream);
                turingMachine.EmptySymbol = readBinary.ReadChar();
                turingMachine.HeadPosition = (TuringHeadPosition)readBinary.ReadInt32();
                turingMachine.FirstStateIndex = readBinary.ReadInt32();
                turingMachine.NumberOfStates = readBinary.ReadInt32();
                int numberOfFinalStates = readBinary.ReadInt32();
                turingMachine.FinalStates = ReadList(readBinary, numberOfFinalStates);
                int numberOfSymbols = readBinary.ReadInt32();
                turingMachine.Symbols = ReadList(readBinary, numberOfSymbols);
                int numberOfPotentialTransitions = readBinary.ReadInt32();
                turingMachine.PotentialTransitions = ReadList(readBinary, numberOfPotentialTransitions).
                    Select(p=>new PotentialTransition(p,0,' ')).ToList();
                turingMachine.ActualStateColor = Color.FromArgb(readBinary.ReadInt32());
                turingMachine.ActualSymbolColor = Color.FromArgb(readBinary.ReadInt32());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (readStream != null)
                    readStream.Close();
            }
        }

        private static List<String> ReadList(BinaryReader readBinary, int size)
        {
            List<String> list = new List<String>();
            for (int i = 0; i < size; i++)
                list.Add(readBinary.ReadString());
            return list;
        }
    }
}
