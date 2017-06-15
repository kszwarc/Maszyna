using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Maszyna.Models
{
    class DataExportCommand
    {
        public static bool SaveFile(String filePath, TuringMachine turingMachine)
        {
            FileStream writeStream = null;
            try
            {
                writeStream = new FileStream(filePath, FileMode.Create);
                BinaryWriter writeBinary = new BinaryWriter(writeStream);
                writeBinary.Write(turingMachine.EmptySymbol);
                writeBinary.Write((Int32)turingMachine.HeadPosition);
                writeBinary.Write((Int32)turingMachine.FirstStateIndex);
                writeBinary.Write((Int32)turingMachine.NumberOfStates);
                writeBinary.Write((Int32)turingMachine.FinalStates.Count);
                SaveList(writeBinary, turingMachine.FinalStates);
                writeBinary.Write((Int32)turingMachine.Symbols.Count);
                SaveList(writeBinary, turingMachine.Symbols);
                writeBinary.Write((Int32)turingMachine.PotentialTransitions.Count);
                SaveList(writeBinary, turingMachine.PotentialTransitions.Select(p=>p.Instruction).ToList());
                writeBinary.Write((Int32)turingMachine.ActualStateColor.ToArgb());
                writeBinary.Write((Int32)turingMachine.ActualSymbolColor.ToArgb());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (writeStream!=null)
                    writeStream.Close();
            }
        }

        private static void SaveList(BinaryWriter writeBinary, List<String> list)
        {
            foreach (var element in list)
                writeBinary.Write(element);
        }
    }
}
