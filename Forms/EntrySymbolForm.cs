using System;
using System.Collections.Generic;

namespace Maszyna.Forms
{
    public partial class EntrySymbolForm : SymbolsGeneralForm
    {
        public EntrySymbolForm(List<String> actualElements) : base(actualElements, "Symbole wejściowe", 
            Models.TuringMachineModifiedElements.EntrySymbols, "Symbol wejściowy", 1) {}
    }
}
