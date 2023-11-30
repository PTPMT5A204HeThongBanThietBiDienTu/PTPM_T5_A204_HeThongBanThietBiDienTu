using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BLL_DAL;
using Excel = Microsoft.Office.Interop.Excel;
using DTO;

namespace RenderUI
{
    public class SaleUI
    {
        Control ctr;
        BLL_DAL_Bill bdb = new BLL_DAL_Bill();
        BLL_DAL_BillProduct bdbp = new BLL_DAL_BillProduct();
        public static List<Object> bills = new List<Object>() { };
        DataGridView dtgvBillProduct = new DataGridView();

        public SaleUI(Control ctr)
        {
            this.ctr = ctr;
        }
        private void dtgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgvBill = (DataGridView)sender;
            dtgvBillProduct = (DataGridView)ctr.Controls.Find("dtgvBillProduct", false)[0];

            string billId = dtgvBill.CurrentRow.Cells[0].Value.ToString();

            loadDataBillProduct(dtgvBillProduct, billId);
        }
        public void renderTextBoxSearch(string name, int leftPos, int topPos)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;
            txt.Width = 410;
            txt.Font = new Font("Arial", 16);
            txt.TextChanged += searchTextBoxChanged;
            ctr.Controls.Add(txt);
        }
        public void searchTextBoxChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            DataGridView dtgvBill = (DataGridView)ctr.Controls.Find("dtgvBill", false)[0];
            bills = bdb.getBillBySearching(txt.Text.Trim());
            dtgvBill.DataSource = bills;
            if (bills.Count != 0)
            {
                dtgvBill.ColumnHeadersHeight = 50;

                dtgvBill.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvBill.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvBill.Columns[0].Width = 190;
                dtgvBill.Columns[0].HeaderText = "Mã hóa đơn";

                dtgvBill.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvBill.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvBill.Columns[1].Width = 138;
                dtgvBill.Columns[1].HeaderText = "Tổng cộng";

                dtgvBill.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvBill.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvBill.Columns[2].Width = 130;
                dtgvBill.Columns[2].HeaderText = "Trạng thái";

                dtgvBill.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvBill.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvBill.Columns[3].Width = 130;
                dtgvBill.Columns[3].HeaderText = "Ngày lập";

                dtgvBill.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvBill.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvBill.Columns[4].Width = 180;
                dtgvBill.Columns[4].HeaderText = "Khách hàng";

                dtgvBill.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvBill.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvBill.Columns[5].Width = 125;
                dtgvBill.Columns[5].HeaderText = "SĐT";
            }

        }
        public void renderDataGridView(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Left = leftPos;
            dtgv.Top = topPos;

            dtgv.Width = ctr.Width - 600;
            dtgv.Height = 200;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            dtgv.CellMouseClick += dtgv_CellMouseClick;
            ctr.Controls.Add(dtgv);

            loadData(dtgv);
        }

        private void loadData(DataGridView dtgv)
        {
            bills = bdb.getAllPaid();
            if (bills.Count == 0)
            {
                renderLabelReport("lblReport", 500, 350);
                ctr.Controls.RemoveByKey("dtgvBill");
                ctr.Controls.RemoveByKey("dtgvBillProduct");
            }
            else
            {
                dtgv.DataSource = bills;

                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 190;
                dtgv.Columns[0].HeaderText = "Mã hóa đơn";

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 138;
                dtgv.Columns[1].HeaderText = "Tổng cộng";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 130;
                dtgv.Columns[2].HeaderText = "Trạng thái";

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 130;
                dtgv.Columns[3].HeaderText = "Ngày lập";

                dtgv.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[4].Width = 180;
                dtgv.Columns[4].HeaderText = "Khách hàng";

                dtgv.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[5].Width = 125;
                dtgv.Columns[5].HeaderText = "SĐT";



            }
        }
        public void renderLabelReport(string name, int leftPos, int topPos)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = "Không có hóa đơn nào đã được thanh toán!";
            lbl.Left = leftPos;
            lbl.Top = topPos;

            lbl.Font = new Font("Arial", 25, FontStyle.Bold);
            lbl.Width = 650;
            lbl.Height = 50;
            ctr.Controls.Add(lbl);
        }
        public void renderDTGVBillProduct(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Top = topPos;
            dtgv.Left = leftPos;
            dtgv.Width = ctr.Width - 600;
            dtgv.Height = 200;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            dtgv.Visible = true;
            ctr.Controls.Add(dtgv);


        }

        private void loadDataBillProduct(DataGridView dtgv, string billId)
        {
            dtgv.DataSource = bdbp.getAllByBillId(billId);
            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 190;
            dtgv.Columns[0].HeaderText = "Mã sản phẩm";

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 351;
            dtgv.Columns[1].HeaderText = "Tên sản phẩm";

            dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[2].Width = 200;
            dtgv.Columns[2].HeaderText = "Giá sản phẩm";

            dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[3].Width = 150;
            dtgv.Columns[3].HeaderText = "Số lượng";
        }
        public void renderButtonExport(string name, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = name;
            btn.Name = name;
            btn.Click += btnExport_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 30;
            ctr.Controls.Add(btn);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DateTimePicker picker1 = new DateTimePicker();
            DateTimePicker picker2 = new DateTimePicker();
            ComboBox cb = (ComboBox)ctr.Controls.Find("typeOfDateExport", false)[0];
            string dinhdang = cb.SelectedItem.ToString();
            if (dinhdang == "Ngày")
            {
                picker1 = (DateTimePicker)ctr.Controls.Find("startDate", false)[0];
                if (picker1.Value.Date > DateTime.Now)
                    MessageBox.Show("Ngày thống kế không thể lớn hơn ngày hiện tại!");
                if (picker1.Value == null)
                    MessageBox.Show("Vui lòng chọn một ngày để thống kê doanh thu!");
            }
            else if (dinhdang == "Nhiều ngày")
            {
                picker1 = (DateTimePicker)ctr.Controls.Find("begin", false)[0];
                picker2 = (DateTimePicker)ctr.Controls.Find("end", false)[0];
            }
            else if (dinhdang == "Tháng")
            {
                picker1 = (DateTimePicker)ctr.Controls.Find("Month", false)[0];
            }
            exportExcel(dinhdang, picker1.Value.Date, picker2.Value.Date);
            BLL_DAL_Recommend bdr = new BLL_DAL_Recommend();
            bdr.runAlgorithm();

        }
        private void exportExcel(string datetype, DateTime date, DateTime date2)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //add data
            double[] hours = new double[24];
            int row = 2;
            List<BillInfo> billList = bdb.getAllPaidForExcel();
            xlWorkSheet.Cells[1, 1] = "";
            xlWorkSheet.Cells[1, 2] = "Tổng Doanh Thu";
            if (datetype == "Ngày")
            {
                double[] gio = new double[24];
                foreach (BillInfo item in billList)
                {
                    if (item.CreatedAt.Date == date)
                        gio[item.CreatedAt.Hour] += item.Total;
                }
                for (int i = 0; i < gio.Length; i++)
                {

                    xlWorkSheet.Cells[row, 1] = "'" + i.ToString().PadLeft(2, '0') + ":00";
                    xlWorkSheet.Cells[row, 2] = gio[i].ToString();
                    row++;
                }
            }
            else if (datetype == "Nhiều ngày")
            {
                List<TotalInDate> ngay = new List<TotalInDate>();
                for (var i = date; i <= date2; i = i.AddDays(1))
                {
                    TotalInDate t = new TotalInDate();
                    t.Ngay = i.Date.ToString("dd-MM-yyyy");
                    t.Total = 0;
                    ngay.Add(t);
                }
                foreach (BillInfo item in billList)
                {
                    if (item.CreatedAt.Date >= date && item.CreatedAt.Date <= date2)
                    {
                        if (ngay.Exists(t => t.Ngay == item.CreatedAt.Date.ToString("dd-MM-yyyy")))
                        {
                            TotalInDate t = ngay.FirstOrDefault(i => i.Ngay == item.CreatedAt.Date.ToString("dd-MM-yyyy"));
                            t.Total += item.Total;
                        }
                    }
                }
                foreach (var n in ngay)
                {
                    xlWorkSheet.Cells[row, 1] = n.Ngay;
                    xlWorkSheet.Cells[row, 2] = n.Total;
                    row++;
                }
            }
            else if (datetype == "Tháng")
            {
                date = new DateTime(date.Year, date.Month, 1);
                int month = date.Month;
                List<TotalInDate> ngay = new List<TotalInDate>();
                for (var i = date; i.Month <= month; i = i.AddDays(1))
                {
                    TotalInDate t = new TotalInDate();
                    t.Ngay = i.Date.ToString("dd-MM-yyyy");
                    t.Total = 0;
                    ngay.Add(t);
                }
                foreach (BillInfo item in billList)
                {
                    if (item.CreatedAt.Date >= date && item.CreatedAt.Date <= date2)
                    {
                        if (ngay.Exists(t => t.Ngay == item.CreatedAt.Date.ToString("dd-MM-yyyy")))
                        {
                            TotalInDate t = ngay.FirstOrDefault(i => i.Ngay == item.CreatedAt.Date.ToString("dd-MM-yyyy"));
                            t.Total += item.Total;
                        }
                    }
                }
                foreach (var n in ngay)
                {
                    xlWorkSheet.Cells[row, 1] = n.Ngay;
                    xlWorkSheet.Cells[row, 2] = n.Total;
                    row++;
                }
            }
            Excel.Range chartRange;
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(200, 80, 500, 250);
            Excel.Chart chartPage = myChart.Chart;
            chartRange = xlWorkSheet.get_Range("A1", "B" + (row - 1));
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xlLine;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + " Export.xls";
            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            Excel.Application xlapp;
            xlapp = new Excel.Application();

            xlWorkBook = xlapp.Workbooks.Open(path, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            xlapp.Visible = true;

            MessageBox.Show("Excel file created!");

        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        public void renderDatePicker(string name, int width, int leftPos, int topPos)
        {
            DateTimePicker picker = new DateTimePicker();
            picker.Name = name;
            picker.Width = width;
            picker.Left = leftPos;
            picker.Top = topPos;
            picker.Height = 80;
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "dd-MM-yyyy";
            picker.Font = new Font("Arial", 16);
            ctr.Controls.Add(picker);

        }
        public void DatePicker_Changed(object sender, EventArgs e)
        {
            DateTimePicker end = (DateTimePicker)ctr.Controls.Find("end", false)[0];
            DateTimePicker start = (DateTimePicker)ctr.Controls.Find("begin", false)[0];
            Button Export = (Button)ctr.Controls.Find("Export", false)[0];
            if (start.Value.Date >= end.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
                Export.Enabled = false;
            }
            else
                Export.Enabled = true;


        }
        public void renderDatePickerEnd(string name, int width, int leftPos, int topPos)
        {
            DateTimePicker picker = new DateTimePicker();
            picker.Name = name;
            picker.Width = width;
            picker.Left = leftPos;
            picker.Top = topPos;
            picker.Height = 80;
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "dd-MM-yyyy";
            picker.Font = new Font("Arial", 16);
            picker.TextChanged += DatePicker_Changed;
            picker.Value = DateTime.Today.AddDays(1);
            ctr.Controls.Add(picker);

        }

        public void renderDatePickerStart(string name, int width, int leftPos, int topPos)
        {
            DateTimePicker picker = new DateTimePicker();
            picker.Name = name;
            picker.Width = width;
            picker.Left = leftPos;
            picker.Top = topPos;
            picker.Height = 80;
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "dd-MM-yyyy";
            picker.Font = new Font("Arial", 16);
            picker.TextChanged += DatePicker_Changed;
            ctr.Controls.Add(picker);

        }
        public void renderDatePickerOnlyMonthYear(string name, int width, int leftPos, int topPos)
        {
            DateTimePicker picker = new DateTimePicker();
            picker.Name = name;
            picker.Width = width;
            picker.Left = leftPos;
            picker.Top = topPos;
            picker.Height = 80;
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "MM-yyyy";
            picker.Font = new Font("Arial", 16);
            ctr.Controls.Add(picker);

        }
    }
}
