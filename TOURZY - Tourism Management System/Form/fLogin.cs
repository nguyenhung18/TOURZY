using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Thêm thư viện để làm việc với SQL Server
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace TOURZY___Tourism_Management_System
{
    public partial class fLogin : Form
    {
        public static string username;

        // Chuỗi kết nối đến cơ sở dữ liệu Tourzy (dùng chuỗi bạn cung cấp)
        private string connectionString = @"Data Source=.;Initial Catalog=Tourzy;Integrated Security=True;";

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
            signin.ShowDialog();
            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameInput = txtUser.Text.Trim();
            string passwordInput = txtPass.Text.Trim();

            // Kiểm tra xem người dùng đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Câu truy vấn SQL để kiểm tra thông tin đăng nhập
                    string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENDANGNHAP = @username AND MATKHAU = @password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số để tránh SQL Injection
                        command.Parameters.AddWithValue("@username", usernameInput);
                        command.Parameters.AddWithValue("@password", passwordInput);

                        // Thực thi truy vấn và lấy kết quả
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Đăng nhập thành công
                            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fLogin.username = usernameInput; // Lưu tên đăng nhập vào biến static
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            // Đăng nhập thất bại
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có vấn đề khi kết nối hoặc truy vấn cơ sở dữ liệu
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}