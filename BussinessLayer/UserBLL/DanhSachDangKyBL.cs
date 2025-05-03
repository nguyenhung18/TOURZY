using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BussinessLayer
{
    public class DanhSachDangKyBL
    {
        private readonly DanhSachDangKyDL danhSachDangKyDL;
        public DanhSachDangKyBL()
        {
            danhSachDangKyDL = new DanhSachDangKyDL();
        }

        public void SaveDanhSachDangKy(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau, int soLuong)
        {
            // Kiểm tra dữ liệu đầu vào
            if (maTaiKhoan <= 0)
                throw new ArgumentException("Mã tài khoản không hợp lệ.");
            if (string.IsNullOrEmpty(maChuyenDi))
                throw new ArgumentException("Mã chuyến đi không được để trống.");
            if (ngayBatDau < DateTime.Now)
                throw new ArgumentException("Ngày bắt đầu không thể nhỏ hơn ngày hiện tại.");
            if (soLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0.");

            // Gọi phương thức lưu từ tầng DAL
            try
            {
                danhSachDangKyDL.SaveDanhSachDangKy(maTaiKhoan, maChuyenDi, ngayBatDau, soLuong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lưu danh sách đăng ký: " + ex.Message);
            }
        }
    }
}
