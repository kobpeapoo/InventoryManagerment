namespace InventoryManagement
{
    partial class frmMaterialViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMatrialCodeTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsability = new System.Windows.Forms.TextBox();
            this.lbUsability = new System.Windows.Forms.Label();
            this.txtSaleName = new System.Windows.Forms.TextBox();
            this.btClear = new System.Windows.Forms.Button();
            this.lbSaleName = new System.Windows.Forms.Label();
            this.btSearch = new System.Windows.Forms.Button();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.lbVenderName = new System.Windows.Forms.Label();
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.lbMaterialName = new System.Windows.Forms.Label();
            this.txtMatrialCodeFrom = new System.Windows.Forms.TextBox();
            this.lbMaterialCode = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lbProductInfo = new System.Windows.Forms.Label();
            this.lbUseInfo = new System.Windows.Forms.Label();
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.txtUseInfo = new System.Windows.Forms.TextBox();
            this.txtProductNameInfo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMatrialCodeTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUsability);
            this.groupBox1.Controls.Add(this.lbUsability);
            this.groupBox1.Controls.Add(this.txtSaleName);
            this.groupBox1.Controls.Add(this.btClear);
            this.groupBox1.Controls.Add(this.lbSaleName);
            this.groupBox1.Controls.Add(this.btSearch);
            this.groupBox1.Controls.Add(this.txtVendorName);
            this.groupBox1.Controls.Add(this.lbVenderName);
            this.groupBox1.Controls.Add(this.txtMaterialName);
            this.groupBox1.Controls.Add(this.lbMaterialName);
            this.groupBox1.Controls.Add(this.txtMatrialCodeFrom);
            this.groupBox1.Controls.Add(this.lbMaterialCode);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(905, 109);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ค้าหา";
            // 
            // txtMatrialCodeTo
            // 
            this.txtMatrialCodeTo.Location = new System.Drawing.Point(231, 18);
            this.txtMatrialCodeTo.Name = "txtMatrialCodeTo";
            this.txtMatrialCodeTo.Size = new System.Drawing.Size(66, 23);
            this.txtMatrialCodeTo.TabIndex = 3;
            this.txtMatrialCodeTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatrialCodeTo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "ถึง";
            // 
            // txtUsability
            // 
            this.txtUsability.Location = new System.Drawing.Point(416, 76);
            this.txtUsability.Name = "txtUsability";
            this.txtUsability.Size = new System.Drawing.Size(168, 23);
            this.txtUsability.TabIndex = 7;
            this.txtUsability.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsability_KeyDown);
            // 
            // lbUsability
            // 
            this.lbUsability.AutoSize = true;
            this.lbUsability.Location = new System.Drawing.Point(338, 80);
            this.lbUsability.Name = "lbUsability";
            this.lbUsability.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbUsability.Size = new System.Drawing.Size(72, 17);
            this.lbUsability.TabIndex = 10;
            this.lbUsability.Text = "การใช้งาน :";
            this.lbUsability.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaleName
            // 
            this.txtSaleName.Location = new System.Drawing.Point(129, 76);
            this.txtSaleName.Name = "txtSaleName";
            this.txtSaleName.Size = new System.Drawing.Size(168, 23);
            this.txtSaleName.TabIndex = 6;
            this.txtSaleName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleName_KeyDown);
            // 
            // btClear
            // 
            this.btClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btClear.Location = new System.Drawing.Point(757, 38);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 41);
            this.btClear.TabIndex = 12;
            this.btClear.Text = "ล้างข้อมูล";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // lbSaleName
            // 
            this.lbSaleName.AutoSize = true;
            this.lbSaleName.Location = new System.Drawing.Point(350, 51);
            this.lbSaleName.Name = "lbSaleName";
            this.lbSaleName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbSaleName.Size = new System.Drawing.Size(60, 17);
            this.lbSaleName.TabIndex = 8;
            this.lbSaleName.Text = "ชื่อผู้ขาย :";
            this.lbSaleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btSearch
            // 
            this.btSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSearch.Location = new System.Drawing.Point(676, 38);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 41);
            this.btSearch.TabIndex = 11;
            this.btSearch.Text = "ค้นหา";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(416, 47);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(168, 23);
            this.txtVendorName.TabIndex = 5;
            this.txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorName_KeyDown);
            // 
            // lbVenderName
            // 
            this.lbVenderName.AutoSize = true;
            this.lbVenderName.Location = new System.Drawing.Point(71, 79);
            this.lbVenderName.Name = "lbVenderName";
            this.lbVenderName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbVenderName.Size = new System.Drawing.Size(52, 17);
            this.lbVenderName.TabIndex = 6;
            this.lbVenderName.Text = "ผู้รับสั่ง :";
            this.lbVenderName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.Location = new System.Drawing.Point(129, 47);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(168, 23);
            this.txtMaterialName.TabIndex = 4;
            this.txtMaterialName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaterialName_KeyDown);
            // 
            // lbMaterialName
            // 
            this.lbMaterialName.AutoSize = true;
            this.lbMaterialName.Location = new System.Drawing.Point(19, 50);
            this.lbMaterialName.Name = "lbMaterialName";
            this.lbMaterialName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbMaterialName.Size = new System.Drawing.Size(104, 17);
            this.lbMaterialName.TabIndex = 4;
            this.lbMaterialName.Text = "ชื่อวัตถุดิบ/สินค้า :";
            this.lbMaterialName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMatrialCodeFrom
            // 
            this.txtMatrialCodeFrom.Location = new System.Drawing.Point(129, 18);
            this.txtMatrialCodeFrom.Name = "txtMatrialCodeFrom";
            this.txtMatrialCodeFrom.Size = new System.Drawing.Size(66, 23);
            this.txtMatrialCodeFrom.TabIndex = 2;
            this.txtMatrialCodeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatrialCode_KeyDown);
            // 
            // lbMaterialCode
            // 
            this.lbMaterialCode.AutoSize = true;
            this.lbMaterialCode.Location = new System.Drawing.Point(11, 24);
            this.lbMaterialCode.Name = "lbMaterialCode";
            this.lbMaterialCode.Size = new System.Drawing.Size(112, 17);
            this.lbMaterialCode.TabIndex = 2;
            this.lbMaterialCode.Text = "รหัสวัตถุดิบ/สินค้า :";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(13, 675);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 10);
            this.progressBar.TabIndex = 36;
            this.progressBar.Visible = false;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(174, 641);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(20, 17);
            this.lblPageInfo.TabIndex = 35;
            this.lblPageInfo.Text = "...";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Location = new System.Drawing.Point(93, 630);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 39);
            this.btnNext.TabIndex = 34;
            this.btnNext.Text = "หน้าถัดไป";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.Location = new System.Drawing.Point(12, 630);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 39);
            this.btnPrevious.TabIndex = 33;
            this.btnPrevious.Text = "หน้าก่อน";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lbProductInfo
            // 
            this.lbProductInfo.AutoSize = true;
            this.lbProductInfo.Location = new System.Drawing.Point(10, 124);
            this.lbProductInfo.Name = "lbProductInfo";
            this.lbProductInfo.Size = new System.Drawing.Size(92, 17);
            this.lbProductInfo.TabIndex = 29;
            this.lbProductInfo.Text = "ชื่อวัตถุดิบสินค้า";
            // 
            // lbUseInfo
            // 
            this.lbUseInfo.AutoSize = true;
            this.lbUseInfo.Location = new System.Drawing.Point(680, 124);
            this.lbUseInfo.Name = "lbUseInfo";
            this.lbUseInfo.Size = new System.Drawing.Size(64, 17);
            this.lbUseInfo.TabIndex = 31;
            this.lbUseInfo.Text = "การใช้งาน";
            // 
            // dgvMaterial
            // 
            this.dgvMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterial.Location = new System.Drawing.Point(13, 221);
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.Size = new System.Drawing.Size(1164, 400);
            this.dgvMaterial.TabIndex = 28;
            this.dgvMaterial.SelectionChanged += new System.EventHandler(this.dgvMaterial_SelectionChanged);
            // 
            // txtUseInfo
            // 
            this.txtUseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUseInfo.Location = new System.Drawing.Point(683, 144);
            this.txtUseInfo.Multiline = true;
            this.txtUseInfo.Name = "txtUseInfo";
            this.txtUseInfo.ReadOnly = true;
            this.txtUseInfo.Size = new System.Drawing.Size(494, 71);
            this.txtUseInfo.TabIndex = 32;
            // 
            // txtProductNameInfo
            // 
            this.txtProductNameInfo.Location = new System.Drawing.Point(13, 144);
            this.txtProductNameInfo.Multiline = true;
            this.txtProductNameInfo.Name = "txtProductNameInfo";
            this.txtProductNameInfo.ReadOnly = true;
            this.txtProductNameInfo.Size = new System.Drawing.Size(664, 71);
            this.txtProductNameInfo.TabIndex = 30;
            // 
            // frmMaterialViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1189, 693);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lbProductInfo);
            this.Controls.Add(this.lbUseInfo);
            this.Controls.Add(this.dgvMaterial);
            this.Controls.Add(this.txtUseInfo);
            this.Controls.Add(this.txtProductNameInfo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMaterialViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "โปรแกรมตรวจสอบวัตถุดิบ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUsability;
        private System.Windows.Forms.Label lbUsability;
        private System.Windows.Forms.TextBox txtSaleName;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Label lbSaleName;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.Label lbVenderName;
        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.Label lbMaterialName;
        private System.Windows.Forms.TextBox txtMatrialCodeFrom;
        private System.Windows.Forms.Label lbMaterialCode;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lbProductInfo;
        private System.Windows.Forms.Label lbUseInfo;
        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.TextBox txtUseInfo;
        private System.Windows.Forms.TextBox txtProductNameInfo;
        private System.Windows.Forms.TextBox txtMatrialCodeTo;
        private System.Windows.Forms.Label label1;
    }
}