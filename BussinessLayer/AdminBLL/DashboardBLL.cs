using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class DashboardBLL
    {
        private DashboardDAL dbDAL = new DashboardDAL();

        public DashboardDTO DuLieuDB()
        {
            DashboardDTO dto = new DashboardDTO()
            {
                CustomerCount = dbDAL.SoLuongCus(),
                TourCount = dbDAL.SoLuongTour(),
                DailyRevenue = dbDAL.DoanhThuNgay(),
                MonthlyRevenue = dbDAL.DoanhThuThang()
            };
            return dto;
        }
    }
}
