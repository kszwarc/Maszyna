using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class MainForm
    {
        private void ResetFontForHeaders()
        {
            foreach (DataGridViewColumn column in dataGridViewTable.Columns)
                column.HeaderCell.Style.Font = dataGridViewTable.ColumnHeadersDefaultCellStyle.Font;
        }

        private void UpdateFirstStateColor()
        {
            int columnIndexToChange = ((int)numericUpDownFirstStateNumber.Value) + ReservedColumns;
            if (columnIndexToChange < dataGridViewTable.Columns.Count)
            {
                Font actualFont = dataGridViewTable.ColumnHeadersDefaultCellStyle.Font;
                dataGridViewTable.Columns[columnIndexToChange].HeaderCell.Style.Font = new Font(actualFont, FontStyle.Bold);
            }
        }

        private void UpdateTable()
        {
            UpdateTableColumns();
            UpdateTableRows();
        }

        private void UpdateTableRows()
        {
            dataGridViewTable.Rows.Clear();
            if (_turingMachine.EmptySymbol != ' ')
                AddRowWithSymbol(_turingMachine.EmptySymbol);
            AddAvailableSymbolRows();
            dataGridViewTable.Refresh();
        }

        private void AddAvailableSymbolRows()
        {
            foreach (var symbol in _turingMachine.Symbols)
                AddRowWithSymbol(symbol[0]);
        }

        private void AddRowWithSymbol(char symbol)
        {
            DataGridViewRow row = new DataGridViewRow();
            dataGridViewTable.Rows.Add(row);
            int lastRowIndex = dataGridViewTable.Rows.Count - 1;
            dataGridViewTable.Rows[lastRowIndex].Cells[0].Value = symbol;
        }

        private void UpdateTableColumns()
        {
            int demandedNumberOfColumns = (int)numericUpDownStateNumbers.Value;
            int columnsToRemove = dataGridViewTable.Columns.Count - ReservedColumns - demandedNumberOfColumns;
            int columnsToAdd = demandedNumberOfColumns - (dataGridViewTable.Columns.Count - ReservedColumns);
            RemoveColumns(columnsToRemove);
            AddColumns(columnsToAdd);
            FirstStateChanges(null, null);
            dataGridViewTable.Refresh();
            UpdateStateTable(null, null);
        }

        private void AddColumns(int columnsToAdd)
        {
            const byte MaxInputLengthForElement = 32;
            for (int i = 0; i < columnsToAdd; i++)
            {
                DataGridViewTextBoxColumn columnToAdd = new DataGridViewTextBoxColumn();
                columnToAdd.HeaderText = "q" + (dataGridViewTable.Columns.Count - ReservedColumns);
                columnToAdd.MaxInputLength = MaxInputLengthForElement;
                columnToAdd.ReadOnly = !checkBoxManualTable.Checked;
                dataGridViewTable.Columns.Add(columnToAdd);
            }
        }

        private void RemoveColumns(int columnsToRemove)
        {
            const byte OffsetForCount = 1;
            for (int i = 0; i < columnsToRemove; i++)
                dataGridViewTable.Columns.RemoveAt(dataGridViewTable.Columns.Count - OffsetForCount);
        }

        private List<PotentialTransition> GeneratePotentialTransitions()
        {
            List<PotentialTransition> potentialTransitions = new List<PotentialTransition>();
            foreach (DataGridViewRow row in dataGridViewTable.Rows)
            {
                for (int i = ReservedColumns; i < dataGridViewTable.Columns.Count; i++)
                {
                    String cellValue = GetCellValue(row.Cells[i]);
                    String entrySymbol = GetCellValue(row.Cells[0]);
                    char entrySymbolToPass = entrySymbol.Length == 1 ? entrySymbol[0] : ' ';
                    int actualStateNumber = i - ReservedColumns;
                    PotentialTransition potentialTransition = new PotentialTransition(cellValue, actualStateNumber,
                        entrySymbolToPass);
                    potentialTransitions.Add(potentialTransition);
                }
            }
            return potentialTransitions;
        }

        private String GetCellValue(DataGridViewCell cell)
        {
            return cell.Value == null ? "" : cell.Value.ToString();
        }

        private void UpdateStateTable(object sender, DataGridViewCellEventArgs e)
        {
            _turingMachine.PotentialTransitions = GeneratePotentialTransitions();
            SetConfigurationStatus();
            UnlockOrLockTabWithSimulation();
        }

        private void CopyDataGridViewToActualTuringState()
        {
            dataGridViewActualTuring.Columns.Clear();
            CopyColumnsBetweenDataGridViews();
            CopyRowsBetweenDataGridViews();
            CopyCellsBetweenDataGridViews();
        }

        private void CopyCellsBetweenDataGridViews()
        {
            for (int i = 0; i < dataGridViewTable.Rows.Count; i++)
                for (int j = 0; j < dataGridViewTable.Columns.Count; j++)
                    dataGridViewActualTuring.Rows[i].Cells[j].Value = dataGridViewTable.Rows[i].Cells[j].Value;
        }

        private void CopyRowsBetweenDataGridViews()
        {
            foreach (DataGridViewRow row in dataGridViewTable.Rows)
            {
                DataGridViewRow rowToAdd = (DataGridViewRow)row.Clone();
                dataGridViewActualTuring.Rows.Add(rowToAdd);
            }
        }

        private void CopyColumnsBetweenDataGridViews()
        {
            foreach (DataGridViewColumn column in dataGridViewTable.Columns)
            {
                DataGridViewColumn columnToAdd = (DataGridViewColumn)column.Clone();
                columnToAdd.HeaderCell.Style.Font = dataGridViewTable.ColumnHeadersDefaultCellStyle.Font;
                columnToAdd.ReadOnly = true;
                dataGridViewActualTuring.Columns.Add(columnToAdd);
            }
        }

        private void ColorActualCell(ProgramResult result)
        {
            DataGridViewCell cellToColor = dataGridViewActualTuring.Rows[result.SymbolIndex].
                Cells[result.StateIndex + ReservedColumns];
            cellToColor.Style.BackColor = _turingMachine.ActualStateColor;
        }

        private void dataGridViewTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            const byte FirstFreeColumnIndex = ReservedColumns - 1;
            if (!checkBoxManualTable.Checked && e.ColumnIndex > FirstFreeColumnIndex && e.RowIndex >= 0)
                OpenNewNextStateWindow(((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex]);
        }

        private void RemoveBackgroundColorFromCells()
        {
            for (int i = 0; i < dataGridViewActualTuring.Rows.Count; i++)
                for (int j = 0; j < dataGridViewActualTuring.Columns.Count; j++)
                    dataGridViewActualTuring.Rows[i].Cells[j].Style.BackColor = dataGridViewTable.Rows[0].Cells[0].
                        Style.BackColor;
        }
    }
}
