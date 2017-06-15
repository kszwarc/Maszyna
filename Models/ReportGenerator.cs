using System;
using System.Drawing;
using MigraDoc.DocumentObjectModel;

namespace Maszyna.Models
{
    class ReportGenerator
    {
        private const String ProgramName = "Symulator Maszyny Turinga";
        private Document _document;
        private String _filePath;
        private ReportStructure _reportStructure;
        private int MaxColumnsOnTraditionalPage = 8;

        public ReportGenerator(String filePath, ReportStructure reportStructure)
        {
            CreateNewDocument();
            _filePath = filePath;
            _reportStructure = reportStructure;
        }

        public bool GenerateReport()
        {
            try
            {
                AddTitlePage();
                AddInfo();
                AddStates();
                SaveDocument();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void AddInfo()
        {
            Section section = _document.AddSection();
            AddHeader(section, "Konfiguracja maszyny i wykonanie programu");
            Paragraph paragraph = GenerateFormulationParagraphForMachine();
            section.Add(paragraph);
            paragraph = GenerateParagraphWithResults();
            section.Add(paragraph);
            AddFooter(section, true);
        }

        private Paragraph GenerateParagraphWithResults()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.AddText("Taśma wejściowa: " + _reportStructure.EntryTape + Environment.NewLine);
            paragraph.AddText("Taśma wyjściowa: " + _reportStructure.ExitTape + Environment.NewLine);
            paragraph.AddText("Stan końcowy: " + _reportStructure.LastState + Environment.NewLine);
            paragraph.AddText(_reportStructure.ExecutionTimeText);
            return paragraph;
        }

        private Paragraph GenerateFormulationParagraphForMachine()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.AddText(ConfigModel.GenerateFormalSymbols(_reportStructure.TuringMachine) + Environment.NewLine);
            return paragraph;
        }

        private void AddStates()
        {
            Section section = _document.AddSection();
            AddHeader(section, "Tablica przejść stanów");
            section.PageSetup.Orientation = Orientation.Landscape;
            AddFooter(section, true);
            MigraDoc.DocumentObjectModel.Tables.Table tablica = GenerateTable(section);
            AddColumns(tablica, _reportStructure.TuringMachine.NumberOfStates + 1, "3cm");
            AddHeadersToTable(tablica, GenerateHeadersForTable());
            AddRows(tablica);
            AdjustPageToDimensions(_reportStructure.TuringMachine.NumberOfStates, section);
        }

        private String[] GenerateHeadersForTable()
        {
            String[] headers = new String[_reportStructure.TuringMachine.NumberOfStates + 1];
            headers[0] = "Σ\\Q";
            for (int i = 1; i < headers.Length; i++)
                headers[i] = "q" + (i - 1);
            return headers;
        }

        private void AddRows(MigraDoc.DocumentObjectModel.Tables.Table table)
        {
            TuringMachine turingMachine = _reportStructure.TuringMachine;
            MigraDoc.DocumentObjectModel.Tables.Row row = AddRow(table);
            row.Cells[0].AddParagraph(new String(turingMachine.EmptySymbol,1));
            AddTransitionsToTable(row, 0);
            for (int i=0; i< turingMachine.Symbols.Count; i++)
            {
                row = AddRow(table);
                row.Cells[0].AddParagraph(turingMachine.Symbols[i]);
                AddTransitionsToTable(row, i+1);
            }
        }

        private void AddTransitionsToTable(MigraDoc.DocumentObjectModel.Tables.Row row, int rowIndex)
        {
            TuringMachine turingMachine = _reportStructure.TuringMachine;
            for (int j = 0; j < turingMachine.NumberOfStates; j++)
                row.Cells[j + 1].AddParagraph(turingMachine.PotentialTransitions[rowIndex * turingMachine.NumberOfStates + j].Instruction);
        }

        private MigraDoc.DocumentObjectModel.Tables.Table GenerateTable(Section section)
        {
            MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.Alignment = MigraDoc.DocumentObjectModel.Tables.RowAlignment.Center;
            return table;
        }

        private void AddHeader(Section section, String text)
        {
            Paragraph paragraf = section.AddParagraph();
            paragraf.Format.Alignment = ParagraphAlignment.Center;
            paragraf.AddFormattedText(text + "\n\n");
        }

        private void AddHeadersToTable(MigraDoc.DocumentObjectModel.Tables.Table table, String[] headers)
        {
            MigraDoc.DocumentObjectModel.Tables.Row row = AddRow(table);
            for (int i = 0; i < headers.Length; i++)
                row.Cells[i].AddParagraph(headers[i]);
        }

        private void AddColumns(MigraDoc.DocumentObjectModel.Tables.Table table, int numberOfColumns, String columnWidth)
        {
            for (int i = 0; i < numberOfColumns; i++)
            {
                table.AddColumn(columnWidth);
                table.Columns[i].Format.Alignment = ParagraphAlignment.Center;
            }
        }

        private MigraDoc.DocumentObjectModel.Tables.Row AddRow(MigraDoc.DocumentObjectModel.Tables.Table table)
        {
            MigraDoc.DocumentObjectModel.Tables.Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Format.Font.Bold = false;
            return row;
        }

        private void AddTitlePage()
        {
            Section section = _document.AddSection();
            AddFooter(section, false);
            section.AddParagraph();
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddFormattedText("\n\n\n\n\n\n\n\n\n");
            AddLogo(paragraph);
            paragraph.AddFormattedText("\n\n");
            paragraph.Format.Font.Size = 16;
            paragraph.AddFormattedText(ProgramName+"\n");
            paragraph = section.AddParagraph();
        }

        private void SaveDocument()
        {
            MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer = new MigraDoc.Rendering.PdfDocumentRenderer(true);
            pdfRenderer.Document = _document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(_filePath);
        }

        private void AddFooter(Section section, bool addPageNumber)
        {
            Paragraph footer = section.Footers.Primary.AddParagraph();
            if (addPageNumber)
                AddPageNumber(ref footer);
            footer.AddDateField();
            footer.Format.Font.Size = 10;
            footer.Format.Alignment = ParagraphAlignment.Center;
        }

        private void CreateNewDocument()
        {
            _document = new Document();
            _document.Info.Title = ProgramName;
            _document.Info.Subject = ProgramName;
            _document.Info.Author = ProgramName;
        }

        private Style CreateStyle()
        {
            Style style = _document.Styles["Normal"];
            style.Font.Name = "Verdana";
            style.Font.Size = 12;
            return style;
        }

        private byte[] GetLogo()
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(Properties.Resources.Logo, typeof(byte[]));
        }

        private void AddPageNumber(ref Paragraph footer)
        {
            footer.AddText("Strona ");
            footer.AddPageField();
            footer.AddText(" z ");
            footer.AddNumPagesField();
            footer.AddLineBreak();
        }

        private void AddLogo(Paragraph paragraph)
        {
            MigraDoc.DocumentObjectModel.Shapes.Image logo = paragraph.AddImage("base64:" + Convert.ToBase64String(GetLogo()));
            logo.Width = "4cm";
            logo.LockAspectRatio = true;
        }

        private void AdjustPageToDimensions(int numberOfStates, Section section)
        {
            bool shouldPageBeWider = numberOfStates + 1 > MaxColumnsOnTraditionalPage;
            if (shouldPageBeWider)
                MakePageWider(section, numberOfStates);
        }

        private void MakePageWider(Section section, int numberOfStates)
        {
            section.PageSetup.PageHeight = section.Document.DefaultPageSetup.PageWidth + (numberOfStates + 1 - MaxColumnsOnTraditionalPage) * 100 + 10;
            section.PageSetup.PageWidth = section.Document.DefaultPageSetup.PageHeight;
        }
    }
}
