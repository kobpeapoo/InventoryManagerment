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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;

            if (await CheckLogin())
            {
                lblStatus.Text = "*** เข้าสู่ระบบสำเร็จ!";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                Application.Exit();
                new System.Threading.Thread(() => new frmMain().ShowDialog()).Start();
            }
            else
            {
                lblStatus.Text = "*** ไม่พบผู้ใช้งานนี้ กรุณาตรวจสอบข้อมูล!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            lblStatus.Visible = true;

            progressBar.Visible = false;
            progressBar.Style = ProgressBarStyle.Continuous;
        }

        private async Task<Boolean> CheckLogin()
        {
            return await Task.Factory.StartNew(() =>
            {
                bool result = false;

                using (PMEntities db = new PMEntities())
                {
                    User user = db.Users.Where(x => x.Username.Equals(txtUsername.Text) && x.Password.Equals(txtPassword.Text)).SingleOrDefault();

                    if (user != null)
                    {
                        result = true;
                    }
                }
                return result;
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            new System.Threading.Thread(() => new frmMaterialViewer().ShowDialog()).Start();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }
    }
}
