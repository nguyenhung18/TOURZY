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
using BussinessLayer;
using TransferObject;
using System.Security.AccessControl;

namespace TOURZY___Tourism_Management_System
{
    public partial class fSignin : Form
    {

        private AccountBLL accountBLL = new AccountBLL();

        public fSignin()
        {
            InitializeComponent();
        }

        private void ckbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShow.Checked)
            {
                txtPass.PasswordChar = '\0';  // '\0' là ký tự rỗng, không ẩn mật khẩu
                txtConfirm.PasswordChar = '\0';  // '\0' là ký tự rỗng, không ẩn mật khẩu
            }
            else
            {
                txtPass.PasswordChar = '*';  // Hiển thị dấu '*' thay cho mật khẩu
                txtConfirm.PasswordChar = '*';  // Hiển thị dấu '*' thay cho mật khẩu
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
                this.Close(); // Đóng form đăng ký
            }
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin. Nhập lại giúp TOURZY nha ^^", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác minh không khớp rồi :<", "Sai mật khẩu xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (accountBLL.KiemTraTaiKhoanTonTai(username))
            {
                MessageBox.Show("Tên đăng nhập đã có người dùng mất rùi TT", "Tài khoản tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AccountDTO newAcc = new AccountDTO
            {
                TenDangNhap = username,
                MatKhau = password,
                VaiTro = "user",
                IsDeleted = false
            };

            if (accountBLL.DangKy(newAcc))
            {
                MessageBox.Show("Đăng ký thành công. Chào bạn đến với TOURZY!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang form đăng nhập
                this.Hide();
                fLogin logForm = new fLogin();
                logForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi trong quá trình đăng ký!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            fLogin lg = new fLogin();
            lg.ShowDialog();
            this.Show();
        }

        private void txtConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSignin_Click(sender, e);
            }
        }
    }
}
