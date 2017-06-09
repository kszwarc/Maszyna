using System;
using System.Collections.Generic;

namespace Maszyna.Forms
{
    public partial class FinalStatesForm : SymbolsGeneralForm
    {
        public FinalStatesForm(List<String> actualElements) : base (actualElements, "Stany końcowe", 
            Models.TuringMachineModifiedElements.FinalStates, "Stany końcowe", 32) {}
    }
}
