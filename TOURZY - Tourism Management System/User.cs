using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TOURZY___Tourism_Management_System
{
    public partial class User : Form
    {
        
        public User()
        {
            InitializeComponent();
            displayUsername();
        }
        public void displayUsername()
        {
            
           // Đặt giá trị username lên label
            string username = fLogin.username.Substring(0, 1).ToUpper() + fLogin.username.Substring(1);
            lblTen.Text = username;
        }
        private void btn_X_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát không?",
                                          "Xác nhận thoát",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }
        private void btn_DangXuat_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Bạn có chắc chắn muốn đăng xuất không?",
            "Xác nhận đăng xuất",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
 );

            if (result == DialogResult.Yes)
            {
                this.Hide(); // Ẩn UserForm (không đóng)

                fLogin loginForm = new fLogin();

                // Dùng ShowDialog để chờ người dùng đăng nhập lại
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show(); // Đăng nhập lại thành công -> hiện UserForm lại
                }
                else
                {
                    this.Close(); // Nếu người dùng thoát luôn -> đóng app
                }

            }
        }

        private void btn_TimChuyenDi_Click(object sender, EventArgs e)
        {

            chuyenDi1.Visible = true;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
            nhieuNguoiDi1.Visible = false;
            danhGiaChuyenDi1.Visible = false;
        }

        private void btn_ChiTiet_Click(object sender, EventArgs e)
        {
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = true;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
            nhieuNguoiDi1.Visible = false;
            danhGiaChuyenDi1.Visible = false;
        }

        private void btn_DatChuyenDi_Click(object sender, EventArgs e)
        {
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = true;
            taoChuyenDiMoi1.Visible = false;
            nhieuNguoiDi1.Visible = false;
            danhGiaChuyenDi1.Visible = false;
        }

        private void btn_TaoChuyenMoi_Click(object sender, EventArgs e)
        {
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = true;
            nhieuNguoiDi1.Visible = false;
            danhGiaChuyenDi1.Visible = false;
        }

        private void btn_NhieuNguoi_Click(object sender, EventArgs e)
        {
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
            nhieuNguoiDi1.Visible = true;
            danhGiaChuyenDi1.Visible = false;
        }

        private void btn_DanhGia_Click(object sender, EventArgs e)
        {
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
            nhieuNguoiDi1.Visible = false;
            danhGiaChuyenDi1.Visible = true;
        }

        private void lblTen_Click(object sender, EventArgs e)
        {

        }
    }
}
