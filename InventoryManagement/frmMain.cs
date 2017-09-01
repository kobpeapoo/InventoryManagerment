using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void menuProgramMaterialTracker_Click(object sender, EventArgs e)
        {
            frmMaterialTracker frm = new frmMaterialTracker();
            OpenForm(frm);
        }

        private void formMain_Load(object sender, EventArgs e)
        {

        }

        private void OpenForm(Form newForm)
        {
            foreach (Form frm in this.MdiChildren)
            {

                if (frm.Name == newForm.Name)
                {
                    frm.Activate();
                    return;
                }
            }

            newForm.MdiParent = this;
            newForm.Show();
        }

        private void menuOrderHistory_Click(object sender, EventArgs e)
        {
            frmOrderHistory frm = new frmOrderHistory();
            OpenForm(frm);
        }

        private void menuMainMDIForm_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Equals(""))
            {
                e.Item.Visible = false;
            }
        }

        private void Set_ProductCustom_Click(object sender, EventArgs e) {

        }
    }
}
