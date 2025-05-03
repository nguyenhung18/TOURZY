using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class LichTrinhDTO
    {
        public string MaChuyenDi {  get; set; }
        public DateTime NgayBatDau { get; set; }
        public string MaHDV { get; set; }
        public int SoLuongNow { get; set; }
        public int SoLuongMax { get; set; }
    }
}
