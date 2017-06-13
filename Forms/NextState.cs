using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class NextState : Window
    {
        private DataGridViewCell _cellToEdit;
        private TuringMachine _turingMachine;

        public NextState(DataGridViewCell cellToEdit, TuringMachine turingMachine)
        {
            _cellToEdit = cellToEdit;
            _turingMachine = turingMachine;
            InitializeComponent();
            SetComboBoxesValues();
            this.Text = "Wartość przejścia";
        }

        private void SetComboBoxesValues()
        {
            SetStatesValues();
            SetSymbolsValues();
            SelectFirstItemIndex(comboBoxMovement);
        }

        private void SelectFirstItemIndex(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }

        private void SetSymbolsValues()
        {
            if (_turingMachine.EmptySymbol!=' ')
                comboBoxSymbol.Items.Add(_turingMachine.EmptySymbol);
            foreach (String symbol in _turingMachine.Symbols)
                comboBoxSymbol.Items.Add(symbol);
            foreach (String states in _turingMachine.FinalStates)
                comboBoxSymbol.Items.Add(states);
            SelectFirstItemIndex(comboBoxSymbol);
        }

        private void SetStatesValues()
        {
            for (int i = 0; i < _turingMachine.NumberOfStates; i++)
                comboBoxState.Items.Add("q" + i);
             SelectFirstItemIndex(comboBoxState);
        }

        private void buttonReady_Click(object sender, EventArgs e)
        {
            _cellToEdit.Value = comboBoxState.SelectedItem + "/" + comboBoxSymbol.SelectedItem + 
                "/" + comboBoxMovement.SelectedItem;
            this.Close();
        }
    }
}
