using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class TaoChuyenDiMoi : UserControl
    {
        private TaoChuyenDiMoiBL yeuCauBLL = new TaoChuyenDiMoiBL();
       
        public TaoChuyenDiMoi()
        {
            InitializeComponent();
          
        }
       
        private void btn_GuiYeuCau_Click(object sender, EventArgs e)
        {

            string tenChuyenDi = cb_Diemden.SelectedItem.ToString();
            DateTime ngayBatDau = dateTimePicker_KhoiHanh.Value;

            int soLuong = int.Parse(tb_SoLuongNguoi.Text);

            string tenDangNhap = (this.FindForm() as User)?.username;
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Không xác định được người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TaiKhoanBL tkBL = new TaiKhoanBL();
            int maTaiKhoan = tkBL.GetUserId(tenDangNhap);
            if (maTaiKhoan == -1)
            {
                MessageBox.Show("Tài khoản không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChuyenDiBL chuyenDiBL = new ChuyenDiBL();
            string maChuyenDi = chuyenDiBL.GetMaChuyenDiByTen(tenChuyenDi);

            if (string.IsNullOrEmpty(maChuyenDi))
            {
                MessageBox.Show("Không tìm thấy mã chuyến đi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TaoChuyenDiMoiBL yeuCauBL = new TaoChuyenDiMoiBL();

            if (yeuCauBL.KiemTraYeuCauTonTai(maTaiKhoan, maChuyenDi, ngayBatDau))
            {
                MessageBox.Show("Bạn đã gửi yêu cầu cho tour này rồi nè!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (yeuCauBL.TaoYeuCau(maTaiKhoan, maChuyenDi, ngayBatDau, soLuong, out string errorMessage))
            {
                MessageBox.Show("Gửi yêu cầu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_Diemden.SelectedIndex = -1;                 // Bỏ chọn ComboBox
                dateTimePicker_KhoiHanh.Value = DateTime.Now;  // Đặt lại ngày hiện tại
                tb_SoLuongNguoi.Clear();                       // Xóa ô nhập số lượng
            }
            else
            {
                MessageBox.Show($"Gửi yêu cầu thất bại: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

      

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            // Tìm form cha là 'User'
            User userForm = this.FindForm() as User;
            if (userForm != null)
            {
                // Tìm flowLayoutPanel1 chứa các UserControl
                FlowLayoutPanel flowPanel = userForm.Controls.Find("flowLayoutPanel1", true).FirstOrDefault() as FlowLayoutPanel;
                if (flowPanel != null)
                {
                    flowPanel.Controls.Clear(); // Xóa UserControl hiện tại

                    // Tạo và thêm lại UserControl đích (ví dụ: UserControl_TrangChu)
                    ChuyenDi ucTrangChu = new ChuyenDi();
                    ucTrangChu.Dock = DockStyle.Top; // Hoặc Fill nếu bạn muốn
                    flowPanel.Controls.Add(ucTrangChu);
                }
            }
        }

        private void tb_SoLuongNguoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void TaoChuyenDiMoi_Load(object sender, EventArgs e)
        {

            dateTimePicker_KhoiHanh.MinDate = DateTime.Today;
            ChuyenDiBL chuyenDiBL = new ChuyenDiBL();
            List<string> danhSachTen = chuyenDiBL.LayDanhSachTenChuyenDi();

            cb_Diemden.Items.Clear();
            foreach (string ten in danhSachTen)
            {
                cb_Diemden.Items.Add(ten);
            }

            if (cb_Diemden.Items.Count > 0)
                cb_Diemden.SelectedIndex = 0;
        }
    }
}
