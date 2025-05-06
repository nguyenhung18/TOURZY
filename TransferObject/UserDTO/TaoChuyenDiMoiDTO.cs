using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
 public class TaoChuyenDiMoiDTO
    {
        public int MaTaiKhoan { get; set; }
        public string MaChuyenDi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public int SoLuong { get; set; }

        public TaoChuyenDiMoiDTO(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau, int soLuong)
        {
            MaTaiKhoan = maTaiKhoan;
            MaChuyenDi = maChuyenDi;
            NgayBatDau = ngayBatDau;
            SoLuong = soLuong;
        }
    }
}
