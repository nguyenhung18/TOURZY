using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BussinessLayer
{
    public class DanhSachDuKhachBL
    {
        private readonly DanhSachDuKhachDL danhSachDuKhachDL = new DanhSachDuKhachDL();

        public bool SaveDanhSachDuKhach(string maChuyenDi, DateTime ngayBatDau, string cccd, string ten, string sdt, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(cccd) || !long.TryParse(cccd, out _) || cccd.Length != 12)
                {
                    errorMessage = "CCCD phải là 12 số!";
                    return false;
                }

                if (string.IsNullOrEmpty(sdt) || !long.TryParse(sdt, out _) || sdt.Length != 10)
                {
                    errorMessage = "Số điện thoại phải là 10 số!";
                    return false;
                }
                // Kiểm tra LichTrinh tồn tại
                if (!danhSachDuKhachDL.CheckLichTrinhTonTai(maChuyenDi, ngayBatDau))
                {
                    errorMessage = "Lịch trình với mã chuyến đi và ngày bắt đầu không tồn tại!";
                    return false;
                }
                danhSachDuKhachDL.SaveDanhSachDuKhach(maChuyenDi, ngayBatDau, cccd, ten, sdt);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi: {ex.Message}";
                return false;
            }

        }

    }
}