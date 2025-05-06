using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using BussinessLayer;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class fLogin : Form
    {
        public string username { get; private set; }
        public int userId { get; private set; }

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
            this.Hide();
            fSignin signin = new fSignin();
            signin.FormClosed += (s, args) => this.Show();
            signin.Show();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtUser.Text.Trim();
            string matKhau = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                AccountBLL accountBLL = new AccountBLL();

                AccountDTO taiKhoan = accountBLL.DangNhap(tenDangNhap, matKhau);

                if (taiKhoan != null)
                {
                    // Lưu tên đăng nhập vào biến tĩnh
                    username = tenDangNhap;

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (taiKhoan.VaiTro == "admin")
                    {
                        this.Hide();
                        Admin adminForm = new Admin();
                        adminForm.StartPosition = FormStartPosition.CenterScreen;
                        adminForm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        username = taiKhoan.TenDangNhap;
                        userId = taiKhoan.ID; 

                        User userForm = new User();
                        userForm.SetUserInfo(tenDangNhap, userId);
                        this.Hide();
                        userForm.ShowDialog();
                        this.Show();

                    }
                    this.Show();
                    txtUser.Clear();
                    txtPass.Clear();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                    txtPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llbQuenMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            fForgot fForgot = new fForgot();
            fForgot.ShowDialog();
            this.Show();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}