using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class SymbolsGeneralForm : Window, IObservable<TuringElement<String[]>>
    {
        private List<IObserver<TuringElement<string[]>>> _observers = new List<IObserver<TuringElement<string[]>>>();
        protected TuringMachineModifiedElements _modifiedElements;

        public SymbolsGeneralForm(String[] actualElements, String name, 
            TuringMachineModifiedElements modifiedElements, String header, 
            int maxInputLengthForElement) : base(name)
        {
            InitializeComponent();
            _modifiedElements = modifiedElements;
            dataGridView.Columns[0].HeaderText = header;
            DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn)dataGridView.Columns[0];
            column.MaxInputLength = maxInputLengthForElement;
            SetDataGridViewRowsValues(actualElements);
        }

        public IDisposable Subscribe(IObserver<TuringElement<string[]>> observer)
        {
            _observers.Add(observer);
            return null;
        }

        private void buttonReady_Click(object sender, EventArgs e)
        {
            TuringElement<String[]> element = new TuringElement<String[]>();
            element.Element = _modifiedElements;
            element.Values = GetValuesFromDataGridView();
            _observers.ForEach(o => o.OnNext(element));
            this.Close();
        }

        private String[] GetValuesFromDataGridView()
        {
            HashSet<String> values = new HashSet<String>();
            foreach (DataGridViewRow row in dataGridView.Rows)
                if (row.Cells.Count>0 && row.Cells[0].Value!=null)
                    values.Add(row.Cells[0].Value.ToString());
            String[] stringArray = new String[values.Count];
            values.CopyTo(stringArray);
            return stringArray;
        }

        private void SetDataGridViewRowsValues(String[] actualElements)
        {
            foreach (String element in actualElements)
                dataGridView.Rows.Add(element);
        }
    }
}
