using System;

namespace Maszyna.Forms
{
    public partial class EntrySymbolForm : SymbolsGeneralForm
    {
        public EntrySymbolForm(String[] actualElements) : base(actualElements, "Symbole wejściowe", 
            Models.TuringMachineModifiedElements.EntrySymbols, "Symbol wejściowy", 1) {}
    }
}
