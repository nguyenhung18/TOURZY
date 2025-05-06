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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace TOURZY___Tourism_Management_System
{
    public partial class DatChuyenDi : UserControl
    {
        public int UserId { get; set; }

        public DatChuyenDi()
        {
            InitializeComponent();
        }
        public void LoadDatChuyenDi(
        string ten, string hanhTrinh, string gia, string soNguoi,
        string soSao, string loaiHinh, string ngay, string soNgay)
        {
            // Hiển thị thông tin chuyến đi lên các điều khiển trong DatChuyenDi
            lb_Ten.Text = ten;
            lb_HanhTrinh.Text = hanhTrinh;
            lb_GiaTrenNguoi.Text = gia;
            lb_NguoiThamGia.Text = soNguoi;
            lb_SoSao.Text = soSao;
            lb_LoaiHinh.Text = loaiHinh;
            lb_NgayKhoiHanh.Text = ngay;
            lb_SoNgayDi.Text = soNgay;

        }


        private void btn_QuayLai_Click(object sender, EventArgs e)
        {

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
                // Get user inputs
                string hoVaTen = tb_HoVaTen.Text;
                string cccd = tb_CCCD.Text;
                string sdt = tb_SDT.Text;
                string email = tb_Email.Text;
                string diaChi = tb_DiaChi.Text;
                int soLuong = int.Parse(tb_SoLuongNguoi.Text);

                // Get derived data from the form (not user inputs)
                string tenChuyenDi = lb_Ten.Text;
                string ngayKhoiHanhText = lb_NgayKhoiHanh.Text;
                string tenDangNhap = (this.FindForm() as User)?.username;

                // Validate TenDangNhap
                if (string.IsNullOrEmpty(tenDangNhap))
                {
                    MessageBox.Show("Không thể xác định người dùng đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get price per person (derived from form)
                string priceText = lb_GiaTrenNguoi.Text.Replace(" VND", "");
                int pricePerPerson = int.Parse(priceText);

                // Get max number of people (derived from form)
                string maxPeopleText = lb_NguoiThamGia.Text.Replace("Số người: ", "");
                int maxSoLuong = int.Parse(maxPeopleText);
                if (string.IsNullOrEmpty(hoVaTen) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin cá nhân!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!long.TryParse(cccd, out _) || cccd.Length != 12)
                {
                    MessageBox.Show("CCCD phải là 12 số!", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!long.TryParse(sdt, out _) || sdt.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải là 10 số!", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (soLuong > maxSoLuong)
                {
                    MessageBox.Show($"Số lượng không được vượt quá {maxSoLuong} người!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Initialize BussinessLayer classes
                TaiKhoanBL taiKhoanBL = new TaiKhoanBL();
                ChuyenDiBL chuyenDiBL = new ChuyenDiBL();
                ThongTinCaNhanBL thongTinCaNhanBL = new ThongTinCaNhanBL();
                DanhSachDuKhachBL danhSachDuKhachBL = new DanhSachDuKhachBL();

                // Retrieve MaTaiKhoan using GetUserId
                int maTaiKhoan = taiKhoanBL.GetUserId(tenDangNhap);
                if (maTaiKhoan == -1)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Retrieve MaChuyenDi using GetMaChuyenDiByTen (tenChuyenDi from lb_Ten)
                string maChuyenDi = chuyenDiBL.GetMaChuyenDiByTen(tenChuyenDi);
                if (string.IsNullOrEmpty(maChuyenDi))
                {
                    MessageBox.Show("Chuyến đi không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse NgayBatDau (ngayKhoiHanhText from lb_NgayKhoiHanh)
                if (!DateTime.TryParse(ngayKhoiHanhText, out DateTime ngayBatDau))
                {
                    MessageBox.Show("Ngày khởi hành không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save to ThongTinCaNhan using MaTaiKhoan
                string errorMessage;
                if (!thongTinCaNhanBL.SaveThongTinCaNhan(maTaiKhoan, hoVaTen, sdt, email, diaChi, out errorMessage))
                {
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                else
                {
                    MessageBox.Show("Đã lưu thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Save to DanhSachDuKhach using MaChuyenDi
                if (!danhSachDuKhachBL.SaveDanhSachDuKhach(maChuyenDi, ngayBatDau, cccd, hoVaTen, sdt, out errorMessage))
                {
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calculate total cost
                int tongSoTien = soLuong * pricePerPerson;

                // Navigate to ThanhToan UserControl
                User userForm = this.FindForm() as User;
                if (userForm != null)
                {
                    FlowLayoutPanel flowPanel = userForm.Controls.Find("flowLayoutPanel1", true).FirstOrDefault() as FlowLayoutPanel;
                    if (flowPanel != null)
                    {
                        flowPanel.Controls.Clear();
                        ThanhToan ucThanhToan = new ThanhToan();
                        ucThanhToan.SetPaymentDetails(tenChuyenDi, soLuong, tongSoTien);
                        ucThanhToan.Dock = DockStyle.Top;
                        flowPanel.Controls.Add(ucThanhToan);
                    }
                }

                // Reset the form
                ResetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetData()
        {
            tb_CCCD.ResetText();
            tb_SDT.ResetText();
            tb_HoVaTen.ResetText();
            tb_SoLuongNguoi.ResetText();
            tb_Email.ResetText();
            tb_DiaChi.ResetText();
        }
    }
}

