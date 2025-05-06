using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
namespace BussinessLayer
{
    public class ChuyenDiBL
    {
        private ChuyenDiDL chuyenDiDL = new ChuyenDiDL();  // Dùng DL để truy xuất dữ liệu.

        public List<ChuyenDiDTO> TimKiemChuyenDi(string diemDen, int giaToiThieu, DateTime? ngayKhoiHanh, int? soSao)
        {
            return chuyenDiDL.FindTour(diemDen, giaToiThieu, ngayKhoiHanh, soSao);
        }
        public List<string> LayDanhSachTenChuyenDi()
        {
            return chuyenDiDL.GetAllTenChuyenDi();
        }
        public string GetMaChuyenDiByTen(string tenChuyenDi)
        {
            return chuyenDiDL.GetMaChuyenDiByTen(tenChuyenDi);
        }
        public DateTime GetNgayBatDauByMaChuyenDi(string maChuyenDi)
        {

            return chuyenDiDL.GetNgayBatDauByMaChuyenDi(maChuyenDi);

        }
    }
}
