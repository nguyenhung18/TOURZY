using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class DanhGiaBL
    {
        private DanhGiaDL dal = new DanhGiaDL();

        public List<DanhGiaDTO> GetAllDanhGia()
        {
            return dal.GetAllDanhGia();
        }
        public List<DanhGiaDTO> GetDanhGiaByChuyenDi(string maChuyenDi)
        {
            return dal.GetDanhGiaByChuyenDi(maChuyenDi);
        }

        public bool AddDanhGia(DanhGiaDTO danhGia)
        {
            return dal.AddDanhGia(danhGia);
        }
    }
}