using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
  public class DatChuyenDiDTO
    {
        public int MaTaiKhoan { get; set; }
        public string MaChuyenDi { get; set; } 
        public DateTime NgayBatDau { get; set; } 
        public int SoLuong { get; set; }
        string TrangThai { get; set; } 
        public string HoVaTen { get; set; } 
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public int TongSoTien { get; set; } 
    }
}
