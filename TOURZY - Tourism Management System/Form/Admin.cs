using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOURZY___Tourism_Management_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
           
        }

        bool menuExpand = false;
        private void menuTransaction_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 198)
                {
                    menuContainer.Height = 262; // Ép buộc giá trị cuối cùng
                    menuTransaction.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 64)
                {
                    menuContainer.Height = 43; // Ép buộc giá trị cuối cùng
                    menuTransaction.Stop();
                    menuExpand = false;
                }
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            menuTransaction.Start();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = true;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
        }

        private void btnTour_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
            tourManage2.Visible = true;
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
            tourManage2.Visible = false;
        }

        private void btnSche_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = true;
            tourCustomer2.Visible = false;
            tourManage2.Visible = false;
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = true;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
            tourManage2.Visible = false;
        }

        private void btnRev_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = true;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
            tourManage2.Visible = false;
        }

        private void btnCusAcc_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = true;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
            tourManage2.Visible = false;
        }

        private void btnCusTour_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = false;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = true;
            tourManage2.Visible = false;
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            adminDashboard2.Visible = false;
            customerManage2.Visible = false;
            guideManage2.Visible = false;
            requestManage2.Visible = true;
            revManage2.Visible = false;
            scheduleManage2.Visible = false;
            tourCustomer2.Visible = false;
            tourManage2.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                fLogin login = new fLogin();
                login.Show();
            }
        }
    }
}
