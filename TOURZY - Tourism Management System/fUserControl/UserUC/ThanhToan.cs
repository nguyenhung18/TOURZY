using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TOURZY___Tourism_Management_System
{
    public partial class ThanhToan : UserControl
    {
        DanhSachDangKyBL danhSachDangKyBL = new DanhSachDangKyBL();
        public ThanhToan()
        {
            InitializeComponent();
        }
        public void SetPaymentDetails(string tenChuyenDi, int soLuong, int tongSoTien)
        {
            // Assuming you have labels in your UserControl
            lb_Ten.Text = tenChuyenDi;
            lb_SoLuongNguoi.Text = soLuong.ToString();
            lb_Tien.Text = tongSoTien.ToString() + " VND";


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

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy các thông tin liên quan đến chuyến đi
                string tenChuyenDi = lb_Ten.Text;
                string soLuong = lb_SoLuongNguoi.Text;
                string tenDangNhap = (this.FindForm() as User)?.username;

                // Kiểm tra người dùng đăng nhập
                if (string.IsNullOrEmpty(tenDangNhap))
                {
                    MessageBox.Show("Không thể xác định người dùng đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Khởi tạo các lớp BusinessLayer
                TaiKhoanBL taiKhoanBL = new TaiKhoanBL();
                ChuyenDiBL chuyenDiBL = new ChuyenDiBL();
                ThongTinCaNhanBL thongTinCaNhanBL = new ThongTinCaNhanBL();
                DanhSachDuKhachBL danhSachDuKhachBL = new DanhSachDuKhachBL();
                DanhSachDangKyBL danhSachDangKyBL = new DanhSachDangKyBL();

                // Lấy MaTaiKhoan từ tên đăng nhập
                int maTaiKhoan = taiKhoanBL.GetUserId(tenDangNhap);
                if (maTaiKhoan == -1)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy MaChuyenDi từ tên chuyến đi
                string maChuyenDi = chuyenDiBL.GetMaChuyenDiByTen(tenChuyenDi);
                if (string.IsNullOrEmpty(maChuyenDi))
                {
                    MessageBox.Show("Chuyến đi không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy NgayBatDau từ LichTrinh
                DateTime ngayBatDau = chuyenDiBL.GetNgayBatDauByMaChuyenDi(maChuyenDi);
                if (ngayBatDau == DateTime.MinValue)
                {
                    MessageBox.Show("Không tìm thấy lịch trình cho chuyến đi này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuyển đổi SoLuong từ chuỗi sang số nguyên
                if (!int.TryParse(soLuong, out int soLuongNguoi) || soLuongNguoi <= 0)
                {
                    MessageBox.Show("Số lượng người không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (danhSachDangKyBL.KiemTraDaDangKy(maTaiKhoan, maChuyenDi, ngayBatDau))
                {
                    MessageBox.Show("Bạn đã đăng ký chuyến đi này rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thiết lập TrangThai
                string trangThai = "Đã đăng ký "; // Có thể thay đổi tùy theo logic nghiệp vụ

                // Thêm vào bảng DanhSachDangKy

                if (danhSachDangKyBL.SaveDanhSachDangKy(maTaiKhoan, maChuyenDi, ngayBatDau, soLuongNguoi, trangThai, out string errorMessage))
                {
                    MessageBox.Show("Thanh toán chuyến đi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Thanh toán thất bại: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            User userForm = this.FindForm() as User;
            if (userForm != null)
            {
                FlowLayoutPanel flowPanel = userForm.Controls.Find("flowLayoutPanel1", true).FirstOrDefault() as FlowLayoutPanel;
                if (flowPanel != null)
                {
                    flowPanel.Controls.Clear();
                    ChuyenDi ucThanhToan = new ChuyenDi();
                    ucThanhToan.Dock = DockStyle.Top;
                    flowPanel.Controls.Add(ucThanhToan);
                }
            }
        }
    }
}
