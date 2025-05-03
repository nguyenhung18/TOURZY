using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
  
    public class ChuyenDiDTO
    {
        public string MaChuyenDi { get; set; }
        public string TenChuyenDi { get; set; }
        public string HinhThuc { get; set; }
        public string HanhTrinh { get; set; }
        public int SoNgayDi { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string ChiTiet { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public int SoSao { get; set; }

    }
}
