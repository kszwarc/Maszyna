using System;
using System.Windows.Forms;
using System.Drawing;

namespace Maszyna.Forms
{
    class DataGridViewWithPaste : DataGridView
    {
        private bool addedRow = false;

        public DataGridViewWithPaste()
        {
            this.MouseClick += ShowMenu;
            this.RowsAdded += AdjustWidthForFirstRow;
            this.Resize += AdjustColumnsWidth;
            this.KeyDown += new KeyEventHandler(this.dataGridView_KeyDown);
        }

        public void AdjustColumnsWidth(object sender, EventArgs e)
        {
            AdjustColumns((DataGridView)sender);
        }

        private void AdjustWidthForFirstRow(object sender, EventArgs e)
        {
            if (!this.addedRow)
            {
                this.addedRow = true;
                AdjustColumnsWidth(sender, e);
            }
        }

        private void ShowMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem menuItem = new MenuItem("Wklej dane do kolumny z zaznaczoną komórką", PasteFromClipboard);
                menu.MenuItems.Add(menuItem);
                int currentMouseOverRow = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;
                menu.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
                PasteFromClipboard((DataGridView)sender);
        }

        private void PasteFromClipboard(object sender, EventArgs e)
        {
            PasteFromClipboard(((DataGridView)((ContextMenu)((MenuItem)sender).Parent).SourceControl));
        }

        public static void PasteFromClipboard(DataGridView dataGridView)
        {
            String text = Clipboard.GetText();
            PasteText(text, '\t', dataGridView);
        }

        public static void PasteText(String tekst, char separator, DataGridView dataGridView)
        {
            try
            {
                String[] lines = tekst.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;
                DataGridViewCell cell;
                foreach (string line in lines)
                {
                    if (row < dataGridView.RowCount && line.Length > 0)
                    {
                        String[] cellValues = line.Split(separator);
                        for (int i = 0; i < cellValues.GetLength(0); i++)
                        {
                            cell = dataGridView[column + i, row];
                            if (!cell.ReadOnly && cell.GetType() == typeof(DataGridViewTextBoxCell))
                                cell.Value = cellValues[i];
                        }
                        row++;
                    }
                    else
                        break;
                }
            }
            catch (Exception) { }
        }

        public static void AdjustColumns(DataGridView dataGridView)
        {
            int width = dataGridView.RowHeadersWidth;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                width += column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true);
            }
            if (width < dataGridView.Width && dataGridView.Columns.Count > 0)
                SetEveryColumnsOnFill(dataGridView);
        }

        private static void SetEveryColumnsOnFill(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}