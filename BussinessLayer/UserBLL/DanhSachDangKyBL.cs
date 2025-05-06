using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class DanhSachDangKyBL
    {
        private readonly DanhSachDangKyDL danhSachDangKyDL = new DanhSachDangKyDL();

        public bool SaveDanhSachDangKy(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau, int soLuong, string trangThai, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (!danhSachDangKyDL.CheckLichTrinhTonTai(maChuyenDi, ngayBatDau))
                {
                    errorMessage = "Không tồn tại lịch trình với mã chuyến đi và ngày bắt đầu này!";
                    return false;
                }

                return danhSachDangKyDL.SaveDanhSachDangKy(maTaiKhoan, maChuyenDi, ngayBatDau, soLuong, trangThai);
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi thêm vào danh sách đăng ký: " + ex.Message;
                return false;
            }
        }
        public bool KiemTraDaDangKy(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau)
        {
            return danhSachDangKyDL.KiemTraTonTai(maTaiKhoan, maChuyenDi, ngayBatDau);
        }

    }
}

