using InventoryManagement.DataAccess;
using InventoryManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class frmMaterialViewer : Form
    {
        int pageNumber = 0;
        int pageSize = 1000;
        PagedList<MaterialTracker> vpList;
        MaterialTrackerParameter search;

        public frmMaterialViewer()
        {
            InitializeComponent();
            addcolumn(dgvMaterial);
            dgvMaterial.RowHeadersWidth = dgvMaterial.RowHeadersWidth + 30;
            dgvMaterial.VirtualMode = true;
            dgvMaterial.AllowUserToAddRows = false;
            dgvMaterial.CellValueNeeded += new DataGridViewCellValueEventHandler(dgvMaterial_CellValueNeeded);
            dgvMaterial.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgvMaterial_RowPostPaint);
            search = new MaterialTrackerParameter();
            search.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            search.StockCondition = "ALL";
            txtMatrialCodeFrom.Select();
            dgvMaterial.EditMode = DataGridViewEditMode.EditProgrammatically; // make grid read only
            

            DoPage(pageNumber);
        }

        private void addcolumn(DataGridView objDataGrid)
        {
            objDataGrid.Columns.Add("Product_Code", "รหัส");
            objDataGrid.Columns.Add("Product_Name", "ชื่อวัตถุดิบ/สินค้า");

            objDataGrid.Columns.Add("BATCH", "BATCH");
            objDataGrid.Columns.Add("MASTER", "MASTER");

            objDataGrid.Columns.Add("Operation_Detail", "การใช้งาน");
            objDataGrid.Columns.Add("Min_Stock", "Min");
            objDataGrid.Columns.Add("Order_to", "สั่งใคร");
            objDataGrid.Columns.Add("TD_QTY", "TD_QTY");
            objDataGrid.Columns.Add("LP_QTY", "LP_QTY");
            objDataGrid.Columns.Add("TOTAL_QTY", "TOTAL_QTY");
            objDataGrid.Columns.Add("LP_MV", "LP เคลื่อนไหว");
            objDataGrid.Columns.Add("TD_MV", "TD เคลื่อนไหว");

            objDataGrid.Columns.Add("Total_ConvertQTY", "Total_ConvertQTY");
            objDataGrid.Columns.Add("Check_TD", "TD เช็ค จน.");
            objDataGrid.Columns.Add("Check_LP", "LP เช็ค จน.");
            objDataGrid.Columns.Add("Note_Vendor", "ผู้ขายNote");
            objDataGrid.Columns.Add("P0", "ราคาตั้ง");
            objDataGrid.Columns.Add("P1", "ลด 1 %");
            objDataGrid.Columns.Add("P2", "ลด 2 %");
            objDataGrid.Columns.Add("P3", "ลด 3 %");
            objDataGrid.Columns.Add("P_Note", "Note");
            objDataGrid.Columns.Add("Vat", "VAT");
            objDataGrid.Columns.Add("S0", "ราคาขายปลีก");
            objDataGrid.Columns.Add("S1", "ราคาขายส่ง 1");
            objDataGrid.Columns.Add("S2", "ราคาขายส่ง 2");
            objDataGrid.Columns.Add("S3", "ราคาขายส.ศิริ ศ93");
            objDataGrid.Columns.Add("S_Note", "Note");
            objDataGrid.Columns.Add("QC_Form", "QC Form");

            objDataGrid.Columns.Add("FACTOR", "ตัวคุณ");
            objDataGrid.Columns.Add("FNAVGCOST", "ต้นทุนเฉลี่ย");
            objDataGrid.Columns.Add("History_Date", "History_Date");
            objDataGrid.Columns.Add("Note_Purchase", "Note_Purchase");
            objDataGrid.Columns.Add("Note_Unit_Convert", "แปลงหน่วย");
            objDataGrid.Columns.Add("Note_Vendor", "Note_Vendor");
            objDataGrid.Columns.Add("PUnit_Name", "PUnit_Name");
            objDataGrid.Columns.Add("Suggest_Order", "ปริมาณสั่งซึ้อ");
            objDataGrid.Columns.Add("SUnit_Name", "แปลงหน่วย");
            objDataGrid.Columns.Add("Vendor", "ผู้ขายชื่อ");
            objDataGrid.Columns.Add("Vendor_to_Purchase", "ชื่อสั่งซื้อ");

            objDataGrid.Columns["TD_QTY"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["TD_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["LP_QTY"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["LP_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["TOTAL_QTY"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["TOTAL_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["Min_Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["TD_MV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["LP_MV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["P0"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["P1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["P2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["P3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["S0"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["S1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["S2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["S3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["Suggest_Order"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["FACTOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["FNAVGCOST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["Product_Code"].Width = 120;
            objDataGrid.Columns["Product_Name"].Width = 150;
            objDataGrid.Columns["LP_MV"].Width = 120;
            objDataGrid.Columns["TD_MV"].Width = 120;
            objDataGrid.Columns["Total_ConvertQTY"].Width = 140;
            objDataGrid.Columns["Operation_Detail"].Width = 130;
            objDataGrid.Columns["S0"].Width = 130;
            objDataGrid.Columns["S1"].Width = 130;
            objDataGrid.Columns["S2"].Width = 130;
            objDataGrid.Columns["S3"].Width = 140;
            objDataGrid.Columns["Note_Purchase"].Width = 120;
            objDataGrid.Columns["Note_Unit_Convert"].Width = 140;
            objDataGrid.Columns["Suggest_Order"].Width = 120;
            objDataGrid.Columns["Vendor_to_Purchase"].Width = 150;

            objDataGrid.Columns["BATCH"].Width = 100;
            objDataGrid.Columns["MASTER"].Width = 100;
            objDataGrid.Columns["BATCH"].ReadOnly = true;
            objDataGrid.Columns["MASTER"].ReadOnly = true;

            objDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
        }

        private void dgvMaterial_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            var grid = sender as DataGridView;
            var entitiy = vpList.Entities[e.RowIndex];
            string columnName = grid.Columns[e.ColumnIndex].Name;
            var columnProperty = entitiy.GetType().GetProperty(columnName);

            e.Value = columnProperty.GetValue(entitiy, null);
        }

        private void dgvMaterial_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = string.Format("{0}", (pageSize * pageNumber) + (e.RowIndex + 1));

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private async void DoPage(int? page)
        {

            lblPageInfo.Text = "กรุณารอสักครู่...";
            this.ActiveControl = txtMatrialCodeFrom;
            // show progress bar
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;

            search.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            search.MatrialCodeFrom = txtMatrialCodeFrom.Text.Trim();
            search.MatrialCodeTo = txtMatrialCodeTo.Text.Trim();
            search.MatrialName = txtMaterialName.Text.Trim();
            search.SaleName = txtSaleName.Text.Trim();
            search.VendorName = txtVendorName.Text.Trim();
            search.Usability = txtUsability.Text.Trim();

            CultureInfo UsaCulture = new CultureInfo("en-US");
            search.CurrentDateEng = DateTime.Now.ToString("yyyy-MM-dd", UsaCulture);
            search.StockCondition = "ALL";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            dgvMaterial.Rows.Clear();
            dgvMaterial.Refresh();

            txtProductNameInfo.Text = "";
            txtUseInfo.Text = "";

            btnPrevious.Enabled = false;
            btnNext.Enabled = false;

            MaterialTrackerAccess mtAc = new MaterialTrackerAccess();
            vpList = await mtAc.GetStock((page ?? 0) * pageSize, pageSize, search);

            btnPrevious.Enabled = vpList.HasPrevious;
            btnNext.Enabled = vpList.HasNext;
            dgvMaterial.RowCount = vpList.Entities.Count;

            stopwatch.Stop();
            lblPageInfo.Text = string.Format("{0} รายการ. หน้า[ {1}/{2} ]. ใช้เวลา[ {3} ].", vpList.Records, pageNumber + (vpList.Records == 0 ? 0 : 1), vpList.Pages, stopwatch.Elapsed);

            // hide progress bar
            progressBar.Visible = false;
            progressBar.Style = ProgressBarStyle.Continuous;
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMatrialCodeFrom.Text) && !String.IsNullOrEmpty(txtMatrialCodeTo.Text))
            {
                MessageBox.Show("กรุณาระบุรหัสเริ่มต้นของวัตถุดิบให้ถูกต้อง !", "โปรดทราบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ActiveControl = txtMatrialCodeFrom;
                return;
            }

            DoPage(pageNumber = 0);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            txtMaterialName.Text = "";
            txtMatrialCodeFrom.Text = "";
            txtSaleName.Text = "";
            txtUsability.Text = "";
            txtVendorName.Text = "";
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            DoPage(--pageNumber);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            DoPage(++pageNumber);
        }

        private void dgvMaterial_SelectionChanged(object sender, EventArgs e)
        {
            txtProductNameInfo.Text = (dgvMaterial.CurrentRow.Cells["Product_Name"].Value == null) ? "" : dgvMaterial.CurrentRow.Cells["Product_Name"].Value.ToString();
            txtUseInfo.Text = (dgvMaterial.CurrentRow.Cells["Operation_Detail"].Value == null) ? "" : dgvMaterial.CurrentRow.Cells["Operation_Detail"].Value.ToString();
        }

        private void txtMatrialCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtMaterialName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtSaleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtUsability_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtMatrialCodeTo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }
    }
}
