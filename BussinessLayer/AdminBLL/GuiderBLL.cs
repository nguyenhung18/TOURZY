using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
using System.Runtime.CompilerServices;
namespace BussinessLayer
{
    public class GuiderBLL
    {
        private GuiderDAL dal = new GuiderDAL();
        public List<GuiderDTO> LoadGuides()
        {
            return dal.LoadGuides();
        }

        public List<string> LoadGuiderIDs()
        {
            return dal.LoadGuiderIDs();
        }

        public List<LichTrinhDTO> LayLichTrinhTheoHDV(string maHDV)
        {
            return dal.GetLichTrinhByHDV(maHDV);
        }

        public void XoaHuongDanVien(string maHDV)
        {
            dal.XoaHuongDanVien(maHDV);
        }

        public string TaoMaHDV()
        {
            return dal.TaoMaHDVTuDong();
        }

        public bool ThemHuongDanVien(GuiderDTO hdv)
        {
            return dal.ThemHuongDanVien(hdv);
        }

        public bool UpdateHDV(GuiderDTO hdv) {
            return dal.UpdateDuongDanVien(hdv);
        }
    }
}
