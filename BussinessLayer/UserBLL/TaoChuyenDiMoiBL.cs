using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
namespace BussinessLayer
{
    public class TaoChuyenDiMoiBL
    {
        private TaoChuyenDiMoiDL dal = new TaoChuyenDiMoiDL();
        private TaoChuyenDiMoiDL taoChuyenDiDL = new TaoChuyenDiMoiDL();
        public bool TaoYeuCau(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau, int soLuong, out string error)
        {
            TaoChuyenDiMoiDTO dto = new TaoChuyenDiMoiDTO(maTaiKhoan, maChuyenDi, ngayBatDau, soLuong);
            return dal.SaveYeuCau(dto, out error);
        }
        public bool KiemTraYeuCauTonTai(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau)
        {
            return taoChuyenDiDL.KiemTraYeuCauTonTai(maTaiKhoan, maChuyenDi, ngayBatDau);
        }
    }
}