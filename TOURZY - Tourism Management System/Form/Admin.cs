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
                if (menuContainer.Height >= 262)
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
            adminDashboard1.Visible = true;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
        }

        private void btnTour_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
            tourManage1.Visible = true;
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = true;
            tourCustomer1.Visible = false;
            tourManage1.Visible = false;
        }

        private void btnSche_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = true;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
            tourManage1.Visible = false;
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = true;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
            tourManage1.Visible = false;
        }

        private void btnRev_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = true;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
            tourManage1.Visible = false;
        }

        private void btnCusAcc_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = true;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
            tourManage1.Visible = false;
        }

        private void btnCusTour_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = false;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = true;
            tourManage1.Visible = false;
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            adminDashboard1.Visible = false;
            customerManage1.Visible = false;
            guideManage1.Visible = false;
            requestManage1.Visible = true;
            revManage1.Visible = false;
            scheduleManage1.Visible = false;
            ticketManage1.Visible = false;
            tourCustomer1.Visible = false;
            tourManage1.Visible = false;
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
