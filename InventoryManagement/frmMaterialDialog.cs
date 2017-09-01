using InventoryManagement.DataAccess;
using InventoryManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement {
    public partial class frmMaterialDialog : Form {
        public string product_code { get; set; }
        public string mix_product_type { get; set; }
        public frmMaterialDialog() {
            InitializeComponent();
            addColumn(dataGridBatchMaster);
        }
        public frmMaterialDialog(string product_code, string mix_product_type) {
            InitializeComponent();
            addColumn(dataGridBatchMaster);
            this.product_code = product_code;
            this.mix_product_type = mix_product_type;

            label1.Text = this.product_code.ToUpper();
            label2.Text = this.mix_product_type;

            String msgData = "";
            if (mix_product_type.Equals("BATCH"))
            {
                msgData = "รายการ Master ที่ค้นหา";
            }
            else {
                msgData = "รายการ Batch ที่ค้นหา";
            }
            label6.Text = msgData;

            getData();
        }
        public void addColumn(DataGridView objDataGrid) {
            //Code,Name,Use,Min,คงเหลือ


            objDataGrid.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.Name = "Chk";
            doWork.HeaderText = "";
            doWork.FalseValue = false;
            doWork.TrueValue = true;
            objDataGrid.Columns.Insert(0, doWork);

            //Rectangle rect = objDataGrid.GetCellDisplayRectangle(0, -1, true);
            //rect.Y = 5;
            //rect.X = 60;
            //CheckBox checkboxHeader = new CheckBox();
            //checkboxHeader.Name = "checkboxHeader";
            //datagridview[0, 0].ToolTipText = "sdfsdf";
            //checkboxHeader.BackColor = Color.Transparent;
            //checkboxHeader.Size = new Size(18, 18);
            //checkboxHeader.Location = rect.Location;
            //checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            //objDataGrid.Controls.Add(checkboxHeader);

            objDataGrid.Columns.Add("Product_Code", "รหัส");
            objDataGrid.Columns.Add("Product_Name", "ชื่อวัตถุดิบ/สินค้า");
            objDataGrid.Columns.Add("Operation_Detail", "การใช้งาน");
            objDataGrid.Columns.Add("Min_Stock", "Min");
            objDataGrid.Columns.Add("TOTAL_QTY", "คงเหลือ");


            objDataGrid.Columns["TOTAL_QTY"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["TOTAL_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            objDataGrid.Columns["Min_Stock"].DefaultCellStyle.Format = "0.00##";
            objDataGrid.Columns["Min_Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            objDataGrid.Columns["Product_Code"].DataPropertyName = "Product_Code";
            objDataGrid.Columns["Product_Name"].DataPropertyName = "Product_Name";
            objDataGrid.Columns["Operation_Detail"].DataPropertyName = "Operation_Detail";
            objDataGrid.Columns["Min_Stock"].DataPropertyName = "Min_Stock";
            objDataGrid.Columns["TOTAL_QTY"].DataPropertyName = "TOTAL_QTY";

            objDataGrid.Columns["Chk"].Width = 50;
            objDataGrid.Columns["Product_Code"].Width = 120;
            objDataGrid.Columns["Product_Name"].Width = 150;
            objDataGrid.Columns["Operation_Detail"].Width = 130;
            objDataGrid.Columns["Min_Stock"].Width = 100;
            objDataGrid.Columns["TOTAL_QTY"].Width = 100;


        }
        public async void getData() {

            label3.Text = "กรุณารอสักครู่ ระบบกำลังตรวจสอบข้อมูล";

            button1.Visible = false;

            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;

            List<MaterialTracker> lsMater = await LoadData();
            dataGridBatchMaster.DataSource = lsMater;


            //Check Box
            foreach (DataGridViewRow row in dataGridBatchMaster.Rows)
            {
                double min_Stock = Convert.ToDouble(row.Cells["Min_Stock"].Value);
                double TOTAL_QTY = Convert.ToDouble(row.Cells["TOTAL_QTY"].Value);

                String product_Code = row.Cells["Product_Code"].Value.ToString().Trim().ToLower();

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                bool chkResult = false;
                if (TOTAL_QTY< min_Stock) {
                    chkResult = true;
                }
                if (StaticVariable.dicBM.ContainsKey(product_Code)) {//กรณีเลือกเข้าเพิ่มเติม
                    chkResult = true;
                }
                if (StaticVariable.dicBMRemove.ContainsKey(product_Code)) {//กรณีเลือกออก
                    chkResult = false;
                }
                chk.Value = chkResult;

            }

            // hide progress bar
            button1.Visible = true;
            label3.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Continuous;

        }
        private async Task<List<MaterialTracker>> LoadData() {
            return await Task.Factory.StartNew(() =>
            {
                MaterialBatchAndMasterAccess data = new MaterialBatchAndMasterAccess();
                List<MaterialTracker> lsMater = data.GetDataBatchAndMaster(this.product_code, this.mix_product_type);

                return lsMater;
            });
        }

        private void button1_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridBatchMaster.Rows)
            {
                String Product_Code = row.Cells["Product_Code"].Value.ToString().Trim().ToLower();
                if ((bool)row.Cells["CHK"].Value == true)
                {
                    MaterialTracker mater = row.DataBoundItem as MaterialTracker;

                    if (!StaticVariable.dicBM.ContainsKey(Product_Code))
                    {
                        StaticVariable.dicBM.Add(Product_Code, mater);
                    }
                }
                else {
                    if (StaticVariable.dicBM.ContainsKey(Product_Code))
                    {
                        //Remove Key เมื่อ Check = False
                        StaticVariable.dicBM.Remove(Product_Code);
                    }
                }
            }
            this.Hide();
        }

        private void dataGridBatchMaster_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            bool colBatch = (bool)dataGridBatchMaster.CurrentRow.Cells["chk"].Value;
            String Product_Code = dataGridBatchMaster.CurrentRow.Cells["Product_Code"].Value.ToString().ToLower();

            if (!colBatch)
            {
                if (!StaticVariable.dicBMRemove.ContainsKey(Product_Code))
                {
                    StaticVariable.dicBMRemove.Add(Product_Code, null);
                }
                    
            }
            else {
                if (StaticVariable.dicBMRemove.ContainsKey(Product_Code)) {
                    StaticVariable.dicBMRemove.Remove(Product_Code);
                }
            }
        }
    }
}
