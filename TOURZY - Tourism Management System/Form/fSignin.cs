using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace TOURZY___Tourism_Management_System
{
    public partial class fSignin : Form
    {
        private string connectionString = @"Data Source=.;Initial Catalog=Tourzy;Integrated Security=True";
        public fSignin()
        {
            InitializeComponent();
        }

        private void ckbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShow.Checked)
            {
                txtPass.PasswordChar = false;
                txtConfirm.PasswordChar = false;
            }
            else
            {
                txtPass.PasswordChar = true;
                txtConfirm.PasswordChar = true;
            }
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Bạn có chắc chắn muốn thoát khỏi chương trình?",
       "Xác nhận thoát",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question
   );

            if (result == DialogResult.Yes)
            {
                this.Hide(); // Ẩn form đăng ký
                fLogin loginForm = new fLogin();
                loginForm.ShowDialog(); // Hiện lại form đăng nhập
                this.Close(); // Đóng form đăng ký
            }
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();
            }
        }
    }
}
