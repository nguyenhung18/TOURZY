using BussinessLayer;
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
        public string username;
        public int userId;
        private TaiKhoanBL dl = new TaiKhoanBL();

        public User()
        {
            InitializeComponent();
            displayUsername();
        }
        // Phương thức để nhận username và userId từ fLogin
        public void SetUserInfo(string username, int userId)
        { 
            this.username = username;
            this.userId = dl.GetUserId(username);
            displayUsername();
        }

        public void displayUsername()
        {
            if (!string.IsNullOrEmpty(username))
            {
                lblTen.Text = username;
            }
            else
            {
                lblTen.Text = "Không xác định";
            }
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
                this.Hide();
                fLogin loginForm = new fLogin();

                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Cập nhật username và userId từ fLogin
                    SetUserInfo(loginForm.username, loginForm.userId);
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
        }
      
        private void btn_TimChuyenDi_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear(); // Xóa UserControl hiện tại
            ChuyenDi taoMoi = new ChuyenDi();
            taoMoi.UserId = this.userId;// Tạo mới UserControl
            taoMoi.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(taoMoi);

            chuyenDi1.Visible = true;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
         
            danhGiaChuyenDi1.Visible = false;
            thanhToan1.Visible = false;
        }

        private void btn_ChiTiet_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            ChiTietChuyenDi taoMoi = new ChiTietChuyenDi();
            taoMoi.UserId = this.userId;
            taoMoi.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(taoMoi);

            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = true;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
           
            danhGiaChuyenDi1.Visible = false;
            thanhToan1.Visible = false;
        }

        private void btn_DatChuyenDi_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            DatChuyenDi taoMoi = new DatChuyenDi();
       
            taoMoi.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(taoMoi);
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = true;
            taoChuyenDiMoi1.Visible = false;
           
            danhGiaChuyenDi1.Visible = false;
            thanhToan1.Visible = false;
        }

        private void btn_TaoChuyenMoi_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            TaoChuyenDiMoi taoMoi = new TaoChuyenDiMoi();
            taoMoi.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(taoMoi);
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = true;
           
            danhGiaChuyenDi1.Visible = false;
            thanhToan1.Visible = false;
        }

     

        private void btn_DanhGia_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            DanhGiaChuyenDi taoMoi = new DanhGiaChuyenDi();
            taoMoi.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(taoMoi);
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
       
            danhGiaChuyenDi1.Visible = true;
            thanhToan1.Visible = false;
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {

            flowLayoutPanel1.Controls.Clear();
           ThanhToan taoMoi = new ThanhToan();
            taoMoi.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(taoMoi);
            chuyenDi1.Visible = false;
            chiTietChuyenDi1.Visible = false;
            datChuyenDi1.Visible = false;
            taoChuyenDiMoi1.Visible = false;
       
            danhGiaChuyenDi1.Visible = false;
            thanhToan1.Visible = true;
        }

        private void chuyenDi1_Load(object sender, EventArgs e)
        {

        }
    }
}
