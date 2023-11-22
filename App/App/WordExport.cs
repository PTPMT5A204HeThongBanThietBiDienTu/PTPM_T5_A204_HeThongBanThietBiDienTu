using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Tables;

namespace App
{
    public class WordExport
    {
        public Document doc;

        public WordExport() 
        {
            doc = new Document("CellphoneS.docx");
        }

        public void PutValue(string billKey, string name, string createTime, string total)
        {
            string[] fields = new string[] { "MaHD", "TenNV", "NgayLap", "TongTien" };

            string[] values = new string[] { billKey, name, createTime, total };

            doc.MailMerge.Execute(fields, values);
        }

        public void InsertRows(Table table, int index_source_row, int position, int rowNumber)
        {
            try
            {
                var sourceRow = table.Rows[index_source_row];
                if (sourceRow != null)
                    for (int i = 0; i < rowNumber - 1; i++)
                        table.Rows.Insert(position, sourceRow.Clone(true));
            }
            catch { }
        }

        public void PutValueToRow(Table table, int row, int column, string text)
        {
            Row r = table.Rows[row];
            if (r == null || text == null)
                return;

            Cell cell = r.Cells[column];
            if (cell == null)
                return;

            var paragraph = cell.Paragraphs[0];
            if (paragraph == null)
                cell.Paragraphs.Add(new Paragraph(cell.Document));
            paragraph = cell.Paragraphs[0];

            var run = paragraph.Runs[0];
            if (run == null)
                paragraph.Runs.Add(new Run(paragraph.Document));
            run = paragraph.Runs[0];

            run.Text = text;
        }

        public bool SaveAndOpenFile(DateTime dateTime)
        {
            string folder = "Bills";
            int d = dateTime.Day, m = dateTime.Month, y = dateTime.Year;
            int s = dateTime.Second, mi = dateTime.Minute, h = dateTime.Hour;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            try
            {
                string fileName = $"{folder}\\{d}-{m}-{y}-{h}-{m}-{s}.docx";
                doc.Save(fileName);
                Process.Start(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
