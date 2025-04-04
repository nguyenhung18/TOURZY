using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOURZY___Tourism_Management_System
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void pbHide_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pbHide, "Ẩn mật khẩu");
        }

        private void pbShow_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pbShow, "Hiển thị mật khẩu");
        }

        private void pbShow_Click(object sender, EventArgs e)
        {
            pbShow.Hide();
            txtPass.UseSystemPasswordChar = false;
            pbHide.Show();
        }

        private void pbHide_Click(object sender, EventArgs e)
        {
            pbHide.Hide();
            txtPass.UseSystemPasswordChar = true;
            pbShow.Show();
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            this.Close();
            fSignin signin = new fSignin();
            signin.Show();
        }
    }
}
