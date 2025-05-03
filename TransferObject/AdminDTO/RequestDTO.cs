using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class RequestDTO
    {
        public string MaTaiKhoan {get;set;}
        public string MaChuyenDi { get;set;}
        public DateTime NgayBatDau { get;set;} 
        public int SoLuong { get;set;}

    }
}
