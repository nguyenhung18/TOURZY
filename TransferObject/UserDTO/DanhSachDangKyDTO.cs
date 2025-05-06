using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class DanhSachDangKyDTO
    {
        public int MaTaiKhoan { get; set; }
        public string MaChuyenDi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; } = "Đã xác nhận"; // mặc định
    }
}
