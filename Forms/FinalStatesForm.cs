using System;

namespace Maszyna.Forms
{
    public partial class FinalStatesForm : SymbolsGeneralForm
    {
        public FinalStatesForm(String[] actualElements) : base (actualElements, "Stany końcowe", 
            Models.TuringMachineModifiedElements.FinalStates, "Stany końcowe", 32) {}
    }
}
