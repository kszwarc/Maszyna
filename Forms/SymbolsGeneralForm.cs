using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class SymbolsGeneralForm : Window, IObservable<TuringElement<List<String>>>
    {
        private List<IObserver<TuringElement<List<String>>>> _observers = new List<IObserver<TuringElement<List<String>>>>();
        protected TuringMachineModifiedElements _modifiedElements;

        public SymbolsGeneralForm(List<String> actualElements, String name, 
            TuringMachineModifiedElements modifiedElements, String header, 
            int maxInputLengthForElement) : base(name)
        {
            InitializeComponent();
            this.Text = name;
            _modifiedElements = modifiedElements;
            dataGridView.Columns[0].HeaderText = header;
            DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn)dataGridView.Columns[0];
            column.MaxInputLength = maxInputLengthForElement;
            SetDataGridViewRowsValues(actualElements);
        }

        public IDisposable Subscribe(IObserver<TuringElement<List<String>>> observer)
        {
            _observers.Add(observer);
            return null;
        }

        private void buttonReady_Click(object sender, EventArgs e)
        {
            TuringElement<List<String>> element = new TuringElement<List<String>>();
            element.Element = _modifiedElements;
            element.Values = GetValuesFromDataGridView();
            _observers.ForEach(o => o.OnNext(element));
            this.Close();
        }

        private List<String> GetValuesFromDataGridView()
        {
            HashSet<String> values = new HashSet<String>();
            foreach (DataGridViewRow row in dataGridView.Rows)
                if (row.Cells.Count>0 && row.Cells[0].Value!=null)
                    values.Add(row.Cells[0].Value.ToString());
            return values.ToList();
        }

        private void SetDataGridViewRowsValues(List<String> actualElements)
        {
            foreach (String element in actualElements)
                dataGridView.Rows.Add(element);
        }
    }
}
