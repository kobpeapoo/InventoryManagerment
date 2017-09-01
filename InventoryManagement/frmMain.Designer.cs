namespace InventoryManagement
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuMainMDIForm = new System.Windows.Forms.MenuStrip();
            this.programsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProgramMaterialTracker = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrderHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_ProductCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_Day_Alert = new System.Windows.Forms.ToolStripMenuItem();
            this.set3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.set4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_BatchMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainMDIForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMainMDIForm
            // 
            this.menuMainMDIForm.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuMainMDIForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programsToolStripMenuItem,
            this.SettingMenu});
            this.menuMainMDIForm.Location = new System.Drawing.Point(0, 0);
            this.menuMainMDIForm.Name = "menuMainMDIForm";
            this.menuMainMDIForm.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuMainMDIForm.Size = new System.Drawing.Size(883, 32);
            this.menuMainMDIForm.TabIndex = 1;
            this.menuMainMDIForm.Text = "menuStrip1";
            this.menuMainMDIForm.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuMainMDIForm_ItemAdded);
            // 
            // programsToolStripMenuItem
            // 
            this.programsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProgramMaterialTracker,
            this.menuOrderHistory});
            this.programsToolStripMenuItem.Image = global::InventoryManagement.Properties.Resources.Microsoft;
            this.programsToolStripMenuItem.Name = "programsToolStripMenuItem";
            this.programsToolStripMenuItem.Size = new System.Drawing.Size(85, 28);
            this.programsToolStripMenuItem.Text = "โปรแกรม";
            // 
            // menuProgramMaterialTracker
            // 
            this.menuProgramMaterialTracker.Image = global::InventoryManagement.Properties.Resources.icon_4_track;
            this.menuProgramMaterialTracker.Name = "menuProgramMaterialTracker";
            this.menuProgramMaterialTracker.Size = new System.Drawing.Size(160, 30);
            this.menuProgramMaterialTracker.Text = "ตรวจสอบวัตถุดิบ";
            this.menuProgramMaterialTracker.Click += new System.EventHandler(this.menuProgramMaterialTracker_Click);
            // 
            // menuOrderHistory
            // 
            this.menuOrderHistory.Image = global::InventoryManagement.Properties.Resources.Dollar;
            this.menuOrderHistory.Name = "menuOrderHistory";
            this.menuOrderHistory.Size = new System.Drawing.Size(160, 30);
            this.menuOrderHistory.Text = "ประวัติการสั่งซื้อ";
            this.menuOrderHistory.Click += new System.EventHandler(this.menuOrderHistory_Click);
            // 
            // SettingMenu
            // 
            this.SettingMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Set_ProductCustom,
            this.Set_Day_Alert,
            this.set3ToolStripMenuItem,
            this.set4ToolStripMenuItem,
            this.Set_BatchMaster});
            this.SettingMenu.Image = ((System.Drawing.Image)(resources.GetObject("SettingMenu.Image")));
            this.SettingMenu.Name = "SettingMenu";
            this.SettingMenu.Size = new System.Drawing.Size(67, 28);
            this.SettingMenu.Text = "ตั้งค่า";
            // 
            // Set_ProductCustom
            // 
            this.Set_ProductCustom.Name = "Set_ProductCustom";
            this.Set_ProductCustom.Size = new System.Drawing.Size(209, 22);
            this.Set_ProductCustom.Text = "ชื่อรายการสั่งทำ";
            // 
            // Set_Day_Alert
            // 
            this.Set_Day_Alert.Name = "Set_Day_Alert";
            this.Set_Day_Alert.Size = new System.Drawing.Size(209, 22);
            this.Set_Day_Alert.Text = "วันแจ้งเตือนใบงานย่อย";
            // 
            // set3ToolStripMenuItem
            // 
            this.set3ToolStripMenuItem.Name = "set3ToolStripMenuItem";
            this.set3ToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.set3ToolStripMenuItem.Text = "ผู้ที่รับผิดชอบใบงานย่อย";
            // 
            // set4ToolStripMenuItem
            // 
            this.set4ToolStripMenuItem.Name = "set4ToolStripMenuItem";
            this.set4ToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.set4ToolStripMenuItem.Text = "จัดการวัตถุดิบแบบ Group";
            // 
            // Set_BatchMaster
            // 
            this.Set_BatchMaster.Name = "Set_BatchMaster";
            this.Set_BatchMaster.Size = new System.Drawing.Size(209, 22);
            this.Set_BatchMaster.Text = "จัดการรายการ Bacth Master";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 321);
            this.Controls.Add(this.menuMainMDIForm);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMainMDIForm;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "  ฟอร์มหลัก";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formMain_Load);
            this.menuMainMDIForm.ResumeLayout(false);
            this.menuMainMDIForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMainMDIForm;
        private System.Windows.Forms.ToolStripMenuItem programsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuProgramMaterialTracker;
        private System.Windows.Forms.ToolStripMenuItem menuOrderHistory;
        private System.Windows.Forms.ToolStripMenuItem Set_ProductCustom;
        private System.Windows.Forms.ToolStripMenuItem SettingMenu;
        private System.Windows.Forms.ToolStripMenuItem Set_Day_Alert;
        private System.Windows.Forms.ToolStripMenuItem set3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem set4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Set_BatchMaster;
    }
}