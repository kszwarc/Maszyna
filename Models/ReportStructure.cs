using System;

namespace Maszyna.Models
{
    public struct ReportStructure
    {
        public TuringMachine TuringMachine { get; set; }
        public String EntryTape { get; set; }
        public String ExitTape { get; set; }
        public String ExecutionTimeText { get; set; }
        public String LastState { get; set; }
    }
}
