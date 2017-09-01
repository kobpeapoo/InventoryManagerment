using InventoryManagement.DataAccess;
using InventoryManagement.Model;
using InventoryManagement.Report;
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

namespace InventoryManagement {
    public partial class frmMaterialTracker : Form {
        int pageNumber = 0; // start at index 0 but when need to show just add +1
        int pageSize = 1000;
        PagedList<MaterialTracker> vpList;
        MaterialTrackerParameter search;
        Dictionary<string, bool> checkBoxVal;
        Dictionary<string, string> listBatchMasterProd;
        List<String> lsCalProductBM;

        List<Products_Model> pmList;
        List<Products_Model_Detail> pmdList;

        List<MaterialTracker> pmdl;
        Dictionary<string, bool> checkBoxProductModelDetail;
        Dictionary<String, String> productsCustom = null;
        string selectedModel;

        public frmMaterialTracker() {
            InitializeComponent();
            addcolumn(dgvMaterial);
            addcolumnTab2Model();
            addcolumn(dataGridModel_Detail);
            dgvMaterial.Columns["chk"].Frozen = true;
            dgvMaterial.RowHeadersWidth = dgvMaterial.RowHeadersWidth + 30;
            dgvMaterial.VirtualMode = true;
            dgvMaterial.AllowUserToAddRows = false;
            dgvMaterial.CellValueNeeded += new DataGridViewCellValueEventHandler(dgvMaterial_CellValueNeeded);
            dgvMaterial.CellValuePushed += new DataGridViewCellValueEventHandler(dgvMaterial_CellValuePushed);
            dgvMaterial.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgvMaterial_RowPostPaint);
            search = new MaterialTrackerParameter();
            search.CurrentDate = dtpCurrentDate.Value.ToString("yyyy-MM-dd");
            search.StockCondition = "ALL";
            checkBoxVal = new Dictionary<string, bool>();
            txtMatrialCodeFrom.Select();

            dataGridModel.Columns["chk"].Frozen = true;
            dataGridModel.RowHeadersWidth = dataGridModel.RowHeadersWidth + 20;

            dataGridModel_Detail.Columns["chk"].Frozen = true;
            dataGridModel_Detail.RowHeadersWidth = dataGridModel_Detail.RowHeadersWidth + 30;

            checkBoxProductModelDetail = new Dictionary<string, bool>();
        }

        private void frmMaterialTracker_Load(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Maximized;
            DoPage(pageNumber);


        }
        
        private void dgvMaterial_SelectionChanged(object sender, EventArgs e) {
            getRowSelection(sender);
            
            
        }

        private void getRowSelection(object sender) {
            txtProductNameInfo.Text = (dgvMaterial.CurrentRow.Cells["Product_Name"].Value == null) ? "" : dgvMaterial.CurrentRow.Cells["Product_Name"].Value.ToString();
            txtUseInfo.Text = (dgvMaterial.CurrentRow.Cells["Operation_Detail"].Value == null) ? "" : dgvMaterial.CurrentRow.Cells["Operation_Detail"].Value.ToString();
        }
        private void showDialogBatch_Master(object sender) {
            string mix_product_type = "";
            if (dgvMaterial.CurrentCell.ColumnIndex == 3)
            {
                string colBatch = dgvMaterial.CurrentRow.Cells["BATCH"].Value.ToString();
                if (!colBatch.Equals("-"))
                {
                    mix_product_type = colBatch;
                }
            }
            else if (dgvMaterial.CurrentCell.ColumnIndex == 4)
            {

                string colMaster = dgvMaterial.CurrentRow.Cells["MASTER"].Value.ToString();
                if (!colMaster.Equals("-"))
                {
                    mix_product_type = colMaster;
                }
            }
            if (!String.IsNullOrEmpty(mix_product_type))
            {
               

                string param_product_code = (dgvMaterial.CurrentRow.Cells["Product_Code"].Value == null) ? "" : dgvMaterial.CurrentRow.Cells["Product_Code"].Value.ToString().ToLower();
                string param_mix_product_type = mix_product_type.Trim();
                frmMaterialDialog frm = new frmMaterialDialog(param_product_code, param_mix_product_type);
                frm.ShowDialog();
            }
            

        }
        private void button1_Click(object sender, EventArgs e) {

            if (MessageBox.Show("คุณต้องการบันทึกข้อมูลและออกใบงานย่อย ใช่หรือไม่ ", "ยืนยันการบันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (productsCustom==null) {
                    MaterialTrackerAccess mta = new MaterialTrackerAccess();
                    productsCustom = mta.GetProductsNameCustom();
                }

                List<Product_Order_Report_Details> pordList = new List<Product_Order_Report_Details>();

                int countOrder = 0;

                foreach (DataGridViewRow row in dgvMaterial.Rows)
                {
                    countOrder++;
                    if ((bool)row.Cells["CHK"].Value == true)
                    {
                        Product_Order_Report p = new Product_Order_Report();
                        p.CreateDate = DateTime.Now.ToString("yyyyMMdd");
                        p.CreateTimestamp = DateTime.Now.ToString("HH:mm:ss");

                        Product_Order_Report_Details pd = new Product_Order_Report_Details();
                        //pd.closeDate = row.Cells[""].Value
                        pd.FDDATE = DateTime.Now.ToString("yyyyMMdd");
                        pd.List_Num_Order = countOrder;
                        pd.LP_QTY = 0;
                        pd.Min_Stock = double.Parse(row.Cells["Min_Stock"].Value.ToString());
                        pd.Note_Close = "";
                        pd.Note_Vendor = (row.Cells["Note_Vendor"].Value == null) ? "" : row.Cells["Note_Vendor"].Value.ToString();
                        pd.Operation_Detail = (row.Cells["Operation_Detail"].Value == null) ? "" : row.Cells["Operation_Detail"].Value.ToString();
                        pd.Order_Status = 0;
                        pd.Order_to = (row.Cells["Order_to"].Value == null) ? "" : row.Cells["Order_to"].Value.ToString();
                        pd.P0 = (row.Cells["P0"].Value == null) ? "" : row.Cells["P0"].Value.ToString();
                        pd.P1 = (row.Cells["P1"].Value == null) ? "" : row.Cells["P1"].Value.ToString();
                        pd.P2 = (row.Cells["P2"].Value == null) ? "" : row.Cells["P2"].Value.ToString();
                        pd.P3 = (row.Cells["P3"].Value == null) ? "" : row.Cells["P3"].Value.ToString();
                        pd.Product_Code = (row.Cells["Product_Code"].Value == null) ? "" : row.Cells["Product_Code"].Value.ToString();
                        pd.Product_Name = (row.Cells["Product_Name"].Value == null) ? "" : row.Cells["Product_Name"].Value.ToString();
                        pd.PUnit_Name = (row.Cells["PUnit_Name"].Value == null) ? "" : row.Cells["PUnit_Name"].Value.ToString();
                        pd.Purchase_Name = (row.Cells["Note_Purchase"].Value == null) ? "" : row.Cells["Note_Purchase"].Value.ToString();
                        pd.P_Amount = 0;
                        pd.P_Note = (row.Cells["P_Note"].Value == null) ? "" : row.Cells["P_Note"].Value.ToString();
                        pd.Suggest_Order = (double?)row.Cells["Suggest_Order"].Value;
                        pd.SUnit_Name = (row.Cells["SUnit_Name"].Value == null) ? "" : row.Cells["SUnit_Name"].Value.ToString();
                        pd.TD_QTY = 0.00;
                        pd.LP_QTY = double.Parse(row.Cells["TOTAL_QTY"].Value.ToString());
                        pd.TOTAL_QTY = double.Parse(row.Cells["TOTAL_QTY"].Value.ToString()); //(double?)row.Cells["TD_QTY"].Value;
                        
                        String vendor = (row.Cells["Vendor"].Value == null) ? "" : row.Cells["Vendor"].Value.ToString();
                        pd.Vendor = vendor;
                        pd.Alert_ID = (productsCustom.ContainsValue(vendor)) ? 1 : 2;



                        pordList.Add(pd);
                    }
                }

                foreach (var item in StaticVariable.dicBM)
                {
                    Product_Order_Report_Details pd = new Product_Order_Report_Details();
                    MaterialTracker mt = (MaterialTracker)item.Value;

                    if (StaticVariable.dicBMRemove.ContainsKey(mt.Product_Code.ToLower())) {
                        continue;
                    }

                    countOrder++;
                    pd.FDDATE = DateTime.Now.ToString("yyyyMMdd");
                    pd.List_Num_Order = countOrder;
                    pd.LP_QTY = 0;
                    pd.Min_Stock = Convert.ToDouble(mt.Min_Stock);
                    pd.Note_Close = "";
                    pd.Note_Vendor = (mt.Note_Vendor == null) ? "" : mt.Note_Vendor;
                    pd.Operation_Detail = (mt.Operation_Detail == null) ? "" : mt.Operation_Detail;
                    pd.Order_Status = 0;
                    pd.Order_to = (mt.Order_to == null) ? "" : mt.Order_to;
                    pd.P0 = (mt.P0 == null) ? "" : mt.P0;
                    pd.P1 = (mt.P1 == null) ? "" : mt.P1;
                    pd.P2 = (mt.P2 == null) ? "" : mt.P2;
                    pd.P3 = (mt.P3 == null) ? "" : mt.P3;
                    pd.Product_Code = (mt.Product_Code == null) ? "" : mt.Product_Code;
                    pd.Product_Name = (mt.Product_Name == null) ? "" : mt.Product_Name;
                    pd.PUnit_Name = (mt.PUnit_Name == null) ? "" : mt.PUnit_Name;
                    pd.Purchase_Name = (mt.Note_Purchase == null) ? "" : mt.Note_Purchase;
                    pd.P_Amount = 0;
                    pd.P_Note = (mt.P_Note == null) ? "" : mt.P_Note;
                    pd.Suggest_Order = Convert.ToDouble(mt.Suggest_Order);
                    pd.SUnit_Name = (mt.SUnit_Name == null) ? "" : mt.SUnit_Name;
                    pd.TD_QTY = 0.00;
                    pd.LP_QTY = Convert.ToDouble(mt.TOTAL_QTY);
                    pd.TOTAL_QTY = Convert.ToDouble(mt.TOTAL_QTY);
                    pd.Vendor = (mt.Vendor == null) ? "" : mt.Vendor;

                    pordList.Add(pd);
                }

                if (pordList.Count() > 0)
                {
                    Product_Order_Report prdOrderReport = new Product_Order_Report();
                    CultureInfo thaiCulture = new CultureInfo("th-TH");
                    prdOrderReport.CreateDate = dtpCurrentDate.Value.ToString("yyyyMMdd", thaiCulture);
                    prdOrderReport.CreateTimestamp = dtpCurrentDate.Value.ToString("HH:mm:ss", thaiCulture);

                    ProductOrderReportAccess prdAcc = new ProductOrderReportAccess();
                    prdAcc.addProductOrderReport(prdOrderReport, pordList);

                    frmProductOrderReport frmReport = new frmProductOrderReport();
                    frmReport.CreatePrintPreView(prdOrderReport.Order_ID);

                }
                else {
                    MessageBox.Show("กรุณาเลือกข้อมูลที่จะออกใบงานย่อย", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        private void btSearch_Click(object sender, EventArgs e) {

            StaticVariable.dicBM = new Dictionary<string, MaterialTracker>();
            StaticVariable.dicBMRemove = new Dictionary<string, MaterialTracker>();

            if (String.IsNullOrEmpty(txtMatrialCodeFrom.Text) && !String.IsNullOrEmpty(txtMatrialCodeTo.Text)) {
                MessageBox.Show("กรุณาระบุรหัสเริ่มต้นของวัตถุดิบให้ถูกต้อง !","โปรดทราบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                
               
                return;
            }

            DoPage(pageNumber = 0);
        }

        private async void DoPage(int? page) {

            lblPageInfo.Text = "กรุณารอสักครู่...";
            tabControl1.TabPages["tabPage1"].Text = "กรุณารอสักครู่...";

            this.ActiveControl = txtMatrialCodeFrom;

            // show progress bar
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;

            search.CurrentDate = dtpCurrentDate.Value.ToString("yyyy-MM-dd");
            search.MatrialCodeFrom = txtMatrialCodeFrom.Text.Trim();
            search.MatrialCodeTo = txtMatrialCodeTo.Text.Trim();
            search.MatrialName = txtMaterialName.Text.Trim();
            search.SaleName = txtSaleName.Text.Trim();
            search.VendorName = txtVendorName.Text.Trim();
            search.Usability = txtUsability.Text.Trim();

            CultureInfo UsaCulture = new CultureInfo("en-US");
            search.CurrentDateEng = dtpCurrentDate.Value.ToString("yyyy-MM-dd", UsaCulture);

            if (rdoAll.Checked)
            {
                search.StockCondition = "ALL";
            }
            else if (rdoMin.Checked)
            {
                search.StockCondition = "LESSMIN";
            }
            else if (rdoOrderNow.Checked)
            {
                search.StockCondition = "ORDERNOW";
            }

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

            dtpGCurrentDate.Value = dtpCurrentDate.Value;
            DoPageProductModel(search);
        }

        private void btnPrevious_Click(object sender, EventArgs e) {
            DoPage(--pageNumber);
        }

        private void btnNext_Click(object sender, EventArgs e) {
            DoPage(++pageNumber);
        }

        private void dgvMaterial_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e) {
            if (e.ColumnIndex == 0)
            {
                var grid = sender as DataGridView;
                var entitiy = vpList.Entities[e.RowIndex];
                var columnProperty = entitiy.GetType().GetProperty("Product_Code");
                var data = (string)columnProperty.GetValue(entitiy, null);

                if (checkBoxVal.ContainsKey(data))
                {
                    e.Value = true;
                }
                else
                {
                    e.Value = false;
                }
            }
            else
            {
                var grid = sender as DataGridView;
                var entitiy = vpList.Entities[e.RowIndex];
                string columnName = grid.Columns[e.ColumnIndex].Name;
                var columnProperty = entitiy.GetType().GetProperty(columnName);

                e.Value = columnProperty.GetValue(entitiy, null);
            }
        }

        private void dgvMaterial_CellValuePushed(object sender, DataGridViewCellValueEventArgs e) {
            if (e.ColumnIndex == 0)
            {
                var grid = sender as DataGridView;
                var entitiy = vpList.Entities[e.RowIndex];
                var columnProperty = entitiy.GetType().GetProperty("Product_Code");
                var data = (string)columnProperty.GetValue(entitiy, null);

                if (checkBoxVal.ContainsKey(data))
                {
                    checkBoxVal.Remove(data);
                }
                else
                {
                    checkBoxVal.Add(data, true);
                }

                grid.NotifyCurrentCellDirty(true);
            }
        }

        private void dgvMaterial_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) {
            
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
       
        private void addcolumn(DataGridView objDataGrid) {
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.Name = "Chk";
            doWork.HeaderText = "";
            doWork.FalseValue = false;
            doWork.TrueValue = true;
            objDataGrid.Columns.Insert(0, doWork);

            Rectangle rect = objDataGrid.GetCellDisplayRectangle(0, -1, true);
            rect.Y = 5;
            rect.X = 91;
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            //datagridview[0, 0].ToolTipText = "sdfsdf";
            checkboxHeader.BackColor = Color.Transparent;
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            objDataGrid.Controls.Add(checkboxHeader);

            objDataGrid.Columns.Add("Product_Code", "รหัส");
            objDataGrid.Columns.Add("Product_Name", "ชื่อวัตถุดิบ/สินค้า");

            //DataGridViewLinkColumn col = new DataGridViewLinkColumn();
            //col.DataPropertyName = "BATCH";
            //col.Name = "BATCH";
            //objDataGrid.Columns.Add(col);

            objDataGrid.Columns.Add("BATCH", "BATCH");
            objDataGrid.Columns.Add("MASTER", "MASTER");

            objDataGrid.Columns.Add("Operation_Detail", "การใช้งาน");
            objDataGrid.Columns.Add("Min_Stock", "Min");
            objDataGrid.Columns.Add("Order_to", "สั่งใคร");
            //objDataGrid.Columns.Add("TD_QTY", "คงเหลือ TD");
            //objDataGrid.Columns.Add("LP_QTY", "คงเหลือ LP");
            objDataGrid.Columns.Add("TOTAL_QTY", "คงเหลือ");
            objDataGrid.Columns.Add("LP_MV", "เคลื่อนไหว");
            //objDataGrid.Columns.Add("TD_MV", "TD เคลื่อนไหว");

            objDataGrid.Columns.Add("Total_ConvertQTY", "แปลงหน่วย");
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

            //dgvMaterial.Columns.Add("checkLP", "LP เช็ค จน.");
            //dgvMaterial.Columns.Add("remainTD", "คงคลัง TD");
            //dgvMaterial.Columns.Add("reamainLP", "คงคลัง LP");
            //dgvMaterial.Columns.Add("sumRemain", "รวม");
            //dgvMaterial.Columns.Add("stockUnit", "หน่วย");
            //dgvMaterial.Columns.Add("convertSum", "แปลงหน่วยรวม");
            //dgvMaterial.Columns.Add("convertUnit", "แปลงหน่วย");
            //dgvMaterial.Columns.Add("convertMulti", "ตัวคุณ");
            //dgvMaterial.Columns.Add("convertNote", "แปลงหน่วย");
            //dgvMaterial.Columns.Add("Suggest_Order", "ปริมาณสั่งซึ้อ");
            //dgvMaterial.Columns.Add("Suggest_Order_unit", "หน่วยสั่งซื้อ");
            //dgvMaterial.Columns.Add("SuggestNote", "สั่งซึ้อ Note");
            //dgvMaterial.Columns.Add("costAvg", "ต้นทุนเฉลี่ย");
            //dgvMaterial.Columns.Add("costNote", "ต้นทุนNote");
            //dgvMaterial.Columns.Add("empty", "หมด");
            //dgvMaterial.Columns.Add("changeTD", "เคลื่อนไหว TD");
            //dgvMaterial.Columns.Add("changeLP", "เคลื่อนไหว LP");
            //dgvMaterial.Columns.Add("vendorName", "ผู้ขายชื่อ");
            //dgvMaterial.Columns.Add("nameOrder", "ชื่อสั่งซื้อ");

            //dgvMaterial.Columns["Product_Name"].Visible = false;

            //objDataGrid.Columns["TD_QTY"].DefaultCellStyle.Format = "0.00##";
            //objDataGrid.Columns["TD_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["Min_Stock"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["Min_Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["LP_MV"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["LP_MV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["TOTAL_QTY"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["TOTAL_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


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

            objDataGrid.Columns["BATCH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            objDataGrid.Columns["MASTER"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            objDataGrid.Columns["Chk"].Width = 50;
            objDataGrid.Columns["Product_Code"].Width = 70;
            objDataGrid.Columns["Product_Name"].Width = 150;
            objDataGrid.Columns["Operation_Detail"].Width = 120;
            objDataGrid.Columns["LP_MV"].Width = 80;
            //objDataGrid.Columns["TD_MV"].Width = 120;
            objDataGrid.Columns["Total_ConvertQTY"].Width = 70;

            objDataGrid.Columns["Min_Stock"].Width = 80;
            objDataGrid.Columns["TOTAL_QTY"].Width = 80;

            objDataGrid.Columns["S0"].Width = 130;
            objDataGrid.Columns["S1"].Width = 130;
            objDataGrid.Columns["S2"].Width = 130;
            objDataGrid.Columns["S3"].Width = 140;
            objDataGrid.Columns["Note_Purchase"].Width = 70;
            objDataGrid.Columns["Note_Unit_Convert"].Width = 70;
            objDataGrid.Columns["Suggest_Order"].Width = 70;
            objDataGrid.Columns["Vendor_to_Purchase"].Width = 70;

            objDataGrid.Columns["BATCH"].Width = 90;
            objDataGrid.Columns["MASTER"].Width = 90;
            objDataGrid.Columns["BATCH"].ReadOnly = true;
            objDataGrid.Columns["MASTER"].ReadOnly = true;

            objDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
        }

        private void btClear_Click(object sender, EventArgs e) {
            dtpCurrentDate.Value = DateTime.Now;
            txtMaterialName.Text = "";
            txtMatrialCodeFrom.Text = "";
            txtSaleName.Text = "";
            txtUsability.Text = "";
            txtVendorName.Text = "";

            rdoOrderNow.Checked = true;
        }

        private void dgvMaterial_CurrentCellDirtyStateChanged(object sender, EventArgs e) {

            var grid = sender as DataGridView;

            if (grid.IsCurrentCellDirty)
            {
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e) {
            var chkAll = sender as CheckBox;

            DataGridView dgv = (DataGridView)chkAll.Parent;

            if (dgv.Name.Equals("dgvMaterial"))
            {
                dgvMaterial.EndEdit();

                if (chkAll.Checked)
                {
                    foreach (DataGridViewRow row in dgvMaterial.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        bool check = (bool)chk.Value;
                        if (!check)
                        {
                            chk.Value = true;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dgvMaterial.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        bool check = (bool)chk.Value;
                        if (check)
                        {
                            chk.Value = false;
                        }
                    }
                }
            }
            else if (dgv.Name.Equals("dataGridModel_Detail"))
            {
                dataGridModel_Detail.EndEdit();

                if (chkAll.Checked)
                {
                    foreach (DataGridViewRow row in dataGridModel_Detail.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        bool check = (bool)chk.Value;
                        if (!check)
                        {
                            var orderType = pmdList.Where(x => x.Product_Code.ToUpper().Equals(row.Cells["Product_Code"].Value.ToString())).Select(o => o.Order_Type).FirstOrDefault();
                            if (orderType.Contains("N"))
                            {
                                return;
                            }
                            chk.Value = true;
                        }

                        string pd = row.Cells["Product_Code"].Value.ToString();
                        string tmpModel = string.Format("{0}_{1}", selectedModel, pd);
                        if (checkBoxProductModelDetail.ContainsKey(tmpModel))
                        {
                            checkBoxProductModelDetail.Remove(tmpModel);
                            checkBoxProductModelDetail.Add(tmpModel, (bool)chk.Value);
                        }
                        else
                        {
                            checkBoxProductModelDetail.Add(tmpModel, (bool)chk.Value);
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridModel_Detail.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        bool check = (bool)chk.Value;
                        if (check)
                        {
                            var orderType = pmdList.Where(x => x.Product_Code.ToUpper().Equals(row.Cells["Product_Code"].Value.ToString())).Select(o => o.Order_Type).FirstOrDefault();
                            if (orderType.Contains("N"))
                            {
                                chk.Value = true;
                            }
                            else
                            {
                                chk.Value = false;
                            }
                        }

                        string pd = row.Cells["Product_Code"].Value.ToString();
                        string tmpModel = string.Format("{0}_{1}", selectedModel, pd);
                        if (checkBoxProductModelDetail.ContainsKey(tmpModel))
                        {
                            checkBoxProductModelDetail.Remove(tmpModel);
                            checkBoxProductModelDetail.Add(tmpModel, (bool)chk.Value);
                        }
                        else
                        {
                            checkBoxProductModelDetail.Add(tmpModel, (bool)chk.Value);
                        }
                    }
                }
            }
        }

        private void rdoAll_CheckedChanged(object sender, EventArgs e) {
            if (rdoAll.Checked)
            {
                DoPage(pageNumber = 0);
            }
        }

        private void rdoMin_CheckedChanged(object sender, EventArgs e) {
            if (rdoMin.Checked)
            {
                DoPage(pageNumber = 0);
            }
        }

        private void rdoOrderNow_CheckedChanged(object sender, EventArgs e) {
            if (rdoOrderNow.Checked)
            {
                DoPage(pageNumber = 0);
            }
        }

        private void dtpCurrentDate_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtMatrialCode_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtMaterialName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtVendorName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtSaleName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void txtUsability_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void dgvMaterial_CellClick(object sender, DataGridViewCellEventArgs e) {
            //String a = dgvMaterial.CurrentRow.Cells["BATCH"].Value.ToString();
            //MessageBox.Show(a);
            showDialogBatch_Master(sender);
        }

        private async void DoPageProductModel(MaterialTrackerParameter searchCon) {

            // show progress bar
            GprogressBar.Visible = true;
            GprogressBar.Style = ProgressBarStyle.Marquee;
            GprogressBar.MarqueeAnimationSpeed = 30;

            //search.MatrialCode = txtMatrialCode.Text.Trim();
            //search.MatrialName = txtMatrialCode.Text.Trim();
            //search.SaleName = txtSaleName.Text.Trim();
            //search.VendorName = txtVendorName.Text.Trim();
            //search.Usability = txtUsability.Text.Trim();

            //CultureInfo UsaCulture = new CultureInfo("en-US");
            //search.CurrentDateEng = dtpCurrentDate.Value.ToString("yyyy-MM-dd", UsaCulture);

            tabControl1.TabPages["tabPage1"].Text = "กรุณารอสักครู่...";

            txtGProductNameInfo.Text = "";
            txtGUseInfo.Text = "";

            MaterialTrackerAccess mtAc = new MaterialTrackerAccess();
            var list = await mtAc.GetProductsModel(searchCon);

            pmList = list.ProductModelsList;
            pmdList = list.ProductsModelDetailList;

            var pmml = from p in pmList
                     group p by p.Model_Code into groups
                     select groups.First();

            dataGridModel.Rows.Clear();
            foreach (Products_Model m in pmList)
            {
                dataGridModel.Rows.Add(
                    false,
                    m.Model_Code,
                    m.Model_Name
                );
            }
            dataGridModel.ClearSelection();
            dataGridModel_Detail.Rows.Clear();

            var pcl = (from pmd in pmdList
                    join pm in pmList on pmd.Model_Code equals pm.Model_Code
                    select pmd.Product_Code.ToUpper()).ToList();

            pmdl = await mtAc.GetProductsModelDetail(pcl);

            tabControl1.TabPages["tabPage1"].Text = string.Format("วัตถุแบบ Group [ {0} ]", pmList.Count);

            // hide progress bar
            GprogressBar.Visible = false;
            GprogressBar.Style = ProgressBarStyle.Continuous;
        }

        private void addcolumnTab2Model() {
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.Name = "Chk";
            doWork.DataPropertyName = "Chk";
            doWork.Width = 50;
            doWork.HeaderText = "";
            doWork.FalseValue = false;
            doWork.TrueValue = true;
            doWork.ReadOnly = false;
            dataGridModel.Columns.Insert(0, doWork);

            //Header Check Box
            //Rectangle rect = dataGridModel.GetCellDisplayRectangle(0, -1, true);
            //rect.Y = 5;
            //rect.X = 80;
            //CheckBox checkboxHeader = new CheckBox();
            //checkboxHeader.Name = "checkboxGHeader";
            //checkboxHeader.BackColor = Color.Transparent;
            //checkboxHeader.Size = new Size(18, 18);
            //checkboxHeader.Location = rect.Location;
            //checkboxHeader.CheckedChanged += new EventHandler(checkboxGHeader_CheckChanged);
            //dataGridModel.Controls.Add(checkboxHeader);

            //Add Column
            dataGridModel.Columns.Add("Model_Code", "รหัสรุ่น");
            dataGridModel.Columns.Add("Model_Name", "รายละเอียด");

            dataGridModel.Columns["Model_Code"].ReadOnly = true;
            dataGridModel.Columns["Model_Name"].ReadOnly = true;

            dataGridModel.Columns["Model_Code"].DataPropertyName = "Model_Code";
            dataGridModel.Columns["Model_Name"].DataPropertyName = "Model_Name";

            dataGridModel.Columns["Model_Code"].Width = 150;
            dataGridModel.Columns["Model_Name"].Width = 250;

            dataGridModel.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            dataGridModel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //private void checkboxGHeader_CheckChanged(object sender, EventArgs e) {
        //    dataGridModel.EndEdit();

        //    var chkAll = sender as CheckBox;

        //    if (chkAll.Checked)
        //    {
        //        foreach (DataGridViewRow row in dataGridModel.Rows)
        //        {
        //            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
        //            bool check = (bool)chk.Value;
        //            if (!check)
        //            {
        //                chk.Value = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow row in dataGridModel.Rows)
        //        {
        //            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
        //            bool check = (bool)chk.Value;
        //            if (check)
        //            {
        //                chk.Value = false;
        //            }
        //        }
        //    }
        //}

        private void btnGSearch_Click(object sender, EventArgs e) {
            checkBoxProductModelDetail = new Dictionary<string, bool>();

            dataGridModel.Rows.Clear();
            dataGridModel_Detail.Rows.Clear();

            CultureInfo UsaCulture = new CultureInfo("en-US");
            search.CurrentDateEng = dtpGCurrentDate.Value.ToString("yyyy-MM-dd", UsaCulture);
            DoPageProductModel(search);
        }

        private void btnGClear_Click(object sender, EventArgs e) {
            dtpGCurrentDate.Value = DateTime.Now;
            txtGMaterialName.Text = "";
            txtGMatrialCode.Text = "";
            txtGSaleName.Text = "";
            txtGUsability.Text = "";
            txtGVendorName.Text = "";
        }

        private void dataGridModel_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dataGridModel_Detail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) {
            var grid = sender as DataGridView;
            var rowIdx = string.Format("{0}", e.RowIndex + 1);

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dataGridModel_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            var grid = sender as DataGridView;

            if (grid.IsCurrentCellDirty)
            {
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridModel_Detail_SelectionChanged(object sender, EventArgs e) {
            txtGProductNameInfo.Text = (dataGridModel_Detail.CurrentRow.Cells["Product_Name"].Value == null) ? "" : dataGridModel_Detail.CurrentRow.Cells["Product_Name"].Value.ToString();
            txtGUseInfo.Text = (dataGridModel_Detail.CurrentRow.Cells["Operation_Detail"].Value == null) ? "" : dataGridModel_Detail.CurrentRow.Cells["Operation_Detail"].Value.ToString();
        }

        private void txtMatrialCodeTo_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
            {
                btSearch_Click(this, new EventArgs());
            }
        }

        private void dataGridModel_SelectionChanged(object sender, EventArgs e)
        {
            dataGridModel_Detail.Rows.Clear();

            foreach (DataGridViewRow roww in dataGridModel.SelectedRows)
            {
                string modelCode = roww.Cells["Model_Code"].Value.ToString().Trim();

                if (!string.IsNullOrEmpty(modelCode))
                {
                    selectedModel = modelCode;

                    var mtl = (from pmd in pmdList
                              join pmdll in pmdl on pmd.Product_Code.ToLower() equals pmdll.Product_Code.ToLower()
                              where pmd.Model_Code.ToLower().Equals(modelCode.ToLower())
                              select pmdll).ToList();

                    foreach (MaterialTracker mt in mtl)
                    {
                        string tmpModel = string.Format("{0}_{1}" , selectedModel, mt.Product_Code);

                        if (checkBoxProductModelDetail.ContainsKey(tmpModel))
                        {
                            mt.Chk = checkBoxProductModelDetail[tmpModel];
                        }
                        else
                        {
                            mt.Chk = true;
                            checkBoxProductModelDetail.Add(tmpModel, mt.Chk);
                        }

                        dataGridModel_Detail.Rows.Add(
                            mt.Chk,
                            mt.Product_Code,
                            mt.Product_Name,
                            mt.BATCH,
                            mt.MASTER,
                            mt.Operation_Detail,
                            mt.Min_Stock,
                            mt.Order_to,
                            mt.TOTAL_QTY,
                            mt.LP_MV,
                            mt.Total_ConvertQTY,
                            mt.Check_TD,
                            mt.Check_LP,
                            mt.Note_Vendor,
                            mt.P0,
                            mt.P1,
                            mt.P2,
                            mt.P3,
                            mt.P_Note,
                            mt.Vat,
                            mt.S0,
                            mt.S1,
                            mt.S2,
                            mt.S3,
                            mt.S_Note,
                            mt.QC_Form,
                            mt.FACTOR,
                            mt.FNAVGCOST,
                            mt.History_Date,
                            mt.Note_Purchase,
                            mt.Note_Unit_Convert,
                            mt.Note_Vendor,
                            mt.PUnit_Name,
                            mt.Suggest_Order,
                            mt.SUnit_Name,
                            mt.Vendor,
                            mt.Vendor_to_Purchase
                        );
                    }

                    foreach (DataGridViewRow row in dataGridModel_Detail.Rows)
                    {
                        var orderType = pmdList.Where(x => x.Product_Code.ToUpper().Equals(row.Cells["Product_Code"].Value.ToString())).Select(o => o.Order_Type).FirstOrDefault();
                        if (orderType.Contains("N"))
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.ReadOnly = true;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }

        private void dataGridModel_Detail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridModel_Detail.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string Product_Code = dataGridModel_Detail.Rows[e.RowIndex].Cells["Product_Code"].Value.ToString();

                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridModel_Detail.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool achecked = Convert.ToBoolean(checkCell.Value);

                string tmpModel = string.Format("{0}_{1}", selectedModel, Product_Code);

                if (achecked)
                {
                    if (checkBoxProductModelDetail.ContainsKey(tmpModel))
                    {
                        checkBoxProductModelDetail.Remove(tmpModel);
                        checkBoxProductModelDetail.Add(tmpModel, achecked);
                    }
                    else
                    {
                        checkBoxProductModelDetail.Add(tmpModel, achecked);
                    }
                }
                else
                {
                    if (checkBoxProductModelDetail.ContainsKey(tmpModel))
                    {
                        checkBoxProductModelDetail.Remove(tmpModel);
                        checkBoxProductModelDetail.Add(tmpModel, achecked);
                    }
                    else
                    {
                        checkBoxProductModelDetail.Add(tmpModel, achecked);
                    }
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                string tabText = tabControl1.TabPages["tabPage1"].Text;
                if (tabText.Contains("กรุณารอสักครู่..."))
                {
                    e.Cancel = true;
                }
            }
        }
        
        private void printReportFromGroupModel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการบันทึกข้อมูลและออกใบงานย่อย ใช่หรือไม่ ", "ยืนยันการบันทึก", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (productsCustom == null)
                {
                    MaterialTrackerAccess mta = new MaterialTrackerAccess();
                    productsCustom = mta.GetProductsNameCustom();
                }

                List<Product_Order_Report_Details> pordList = new List<Product_Order_Report_Details>();

                int countOrder = 0;

                foreach (DataGridViewRow row in dataGridModel.Rows)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)row.Cells["Chk"];
                    bool achecked = Convert.ToBoolean(checkCell.Value);

                    if(achecked)
                    {
                        string modelCode = row.Cells["Model_Code"].Value.ToString();

                        foreach (var pair in checkBoxProductModelDetail)
                        {
                            string[] mmodel = pair.Key.Split('_');

                            if(mmodel[0].Equals(modelCode) && pair.Value == true)
                            {
                                List<MaterialTracker> l = pmdl.Where(o => o.Product_Code.ToUpper().Equals(mmodel[1])).ToList();

                                foreach (MaterialTracker m in l)
                                {
                                    countOrder++;

                                    Product_Order_Report_Details pd = new Product_Order_Report_Details();
                                    pd.FDDATE = DateTime.Now.ToString("yyyyMMdd");
                                    pd.List_Num_Order = countOrder;
                                    pd.LP_QTY = 0;
                                    pd.Min_Stock = double.Parse(m.Min_Stock.ToString());
                                    pd.Note_Close = "";
                                    pd.Note_Vendor = (m.Note_Vendor == null) ? "" : m.Note_Vendor;
                                    pd.Operation_Detail = (m.Operation_Detail == null) ? "" : m.Operation_Detail;
                                    pd.Order_Status = 0;
                                    pd.Order_to = (m.Order_to == null) ? "" : m.Order_to;
                                    pd.P0 = (m.P0 == null) ? "" : m.P0;
                                    pd.P1 = (m.P1 == null) ? "" : m.P1;
                                    pd.P2 = (m.P2 == null) ? "" : m.P2;
                                    pd.P3 = (m.P3 == null) ? "" : m.P3;
                                    pd.Product_Code = (m.Product_Code == null) ? "" : m.Product_Code;
                                    pd.Product_Name = (m.Product_Name == null) ? "" : m.Product_Name;
                                    pd.PUnit_Name = (m.PUnit_Name == null) ? "" : m.PUnit_Name;
                                    pd.Purchase_Name = (m.Note_Purchase == null) ? "" : m.Note_Purchase;
                                    pd.P_Amount = 0;
                                    pd.P_Note = (m.P_Note == null) ? "" : m.P_Note;
                                    pd.Suggest_Order = m.Suggest_Order;
                                    pd.SUnit_Name = (m.SUnit_Name == null) ? "" : m.SUnit_Name;
                                    pd.TD_QTY = 0.00;
                                    pd.LP_QTY = double.Parse(m.TOTAL_QTY.ToString());
                                    pd.TOTAL_QTY = double.Parse(m.TOTAL_QTY.ToString());

                                    String vendor = (m.Vendor == null) ? "" : m.Vendor.ToString();
                                    pd.Vendor = vendor;
                                    pd.Alert_ID = (productsCustom.ContainsValue(vendor)) ? 1 : 2;

                                    pordList.Add(pd);
                                }
                            }
                        }
                    }
                }

                if (pordList.Count() > 0)
                {
                    Product_Order_Report prdOrderReport = new Product_Order_Report();
                    CultureInfo thaiCulture = new CultureInfo("th-TH");
                    prdOrderReport.CreateDate = dtpCurrentDate.Value.ToString("yyyyMMdd", thaiCulture);
                    prdOrderReport.CreateTimestamp = dtpCurrentDate.Value.ToString("HH:mm:ss", thaiCulture);

                    ProductOrderReportAccess prdAcc = new ProductOrderReportAccess();
                    prdAcc.addProductOrderReport(prdOrderReport, pordList);

                    frmProductOrderReport frmReport = new frmProductOrderReport();
                    frmReport.CreatePrintPreView(prdOrderReport.Order_ID);

                }
                else {
                    MessageBox.Show("กรุณาเลือกข้อมูลที่จะออกใบงานย่อย", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
