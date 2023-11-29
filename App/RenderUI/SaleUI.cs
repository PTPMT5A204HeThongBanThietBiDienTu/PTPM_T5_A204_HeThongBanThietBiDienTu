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
        public static List<Object> bills = new List<Object>(){};
        DataGridView dtgvBillProduct = new DataGridView();
        
        public SaleUI(Control ctr)
        {
            this.ctr = ctr;
        }
        private void dtgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgvBill = (DataGridView)sender;
            TextBox txt = (TextBox)ctr.Controls.Find("txtBillId", false)[0];
            dtgvBillProduct = (DataGridView)ctr.Controls.Find("dtgvBillProduct", false)[0];

            string billId = dtgvBill.CurrentRow.Cells[0].Value.ToString();
            txt.Text = billId;
            
            loadDataBillProduct(dtgvBillProduct, billId);
        }
        public void renderDataGridView(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Left = leftPos;
            dtgv.Top = topPos;

            dtgv.Width = 650;
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
            lbl.Text = "Không có hóa đơn nào cần duyệt";
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
            dtgv.Width = 650;
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
        public void renderButtonExport(string name,int leftPos, int topPos)
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
            DateTimePicker picker  = (DateTimePicker)ctr.Controls.Find("startDate", false)[0];
            if(picker.Value.Date>DateTime.Now)
                MessageBox.Show("Ngày thống kế không thể lớn hơn ngày hiện tại!");
            if(picker.Value==null)
                MessageBox.Show("Vui lòng chọn một ngày để thống kê doanh thu!");
            exportExcel("ngày",picker.Text);

            BLL_DAL_Recommend bdr = new BLL_DAL_Recommend();
            bdr.runAlgorithm();
        }
        private void exportExcel(string datetype,string date)
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
            if(datetype=="ngày")
            {
                double[] gio = new double[24];
                foreach (BillInfo item in billList)
                {
                    if(item.CreatedAt.ToString("yyyy-MM-dd") == date)
                    {
                        gio[item.CreatedAt.Hour] += item.Total;
                    }    
                }
               
                xlWorkSheet.Cells[1, 1] = "Giờ";
                xlWorkSheet.Cells[1, 2] = "Tổng Tiền";

                for (int i=0;i<gio.Length;i++)
                {

                    xlWorkSheet.Cells[row, 1] = i;
                    xlWorkSheet.Cells[row, 2] = gio[i].ToString();
                    row++;
                }    
            }    
            Excel.Range chartRange;
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(200, 80, 500, 250);
            Excel.Chart chartPage = myChart.Chart;
            chartRange = xlWorkSheet.get_Range("A1", "B"+(row-1));
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
            picker.CustomFormat = "yyyy-MM-dd";
            picker.Font = new Font("Arial", 16);
            ctr.Controls.Add(picker);
            
        }
    }
}
