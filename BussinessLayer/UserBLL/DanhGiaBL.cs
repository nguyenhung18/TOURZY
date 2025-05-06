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
        public bool ThemDanhGia(DanhGiaDTO danhGia)
        {
            return dal.ThemDanhGia(danhGia);
        }
        public bool DanhGiaDaTonTai(string maChuyenDi, int maTaiKhoan)
        {
            return dal.DanhGiaDaTonTai(maChuyenDi, maTaiKhoan);
        }
    }
}