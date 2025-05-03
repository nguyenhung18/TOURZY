using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;
namespace BussinessLayer
{
    public class LichTrinhBLL
    {
        private LichTrinhDAL dal = new LichTrinhDAL();

        public List<LichTrinhDTO> LayLichTrinhThongKe(DateTime fromDate, DateTime toDate)
        {
            return dal.GetLichTrinhWithSoLuong(fromDate, toDate);
        }

        public List<LichTrinhDTO> FilterData(string maChuyenDi, string maHDV, int soLuong, bool notEligible)
        {
            return dal.FilterData(maChuyenDi, maHDV, soLuong, notEligible);
        }


        public void DeleteItinerary(string IDTour, DateTime StartDay)
        {
            LichTrinhDTO itinerary = new LichTrinhDTO
            {
                MaChuyenDi = IDTour,
                NgayBatDau = StartDay
            };

            dal.DeleteItinerary(itinerary);
        }

        public List<LichTrinhDTO> LoadLichTrinh()
        {
            return dal.GetLichTrinh();
        }

        public void Update_LichTrinh(string maChuyenDi, DateTime ngayBatDau, string maHDV)
        {
            LichTrinhDTO lichTrinh = new LichTrinhDTO
            {
                MaChuyenDi = maChuyenDi,
                NgayBatDau = ngayBatDau,
                MaHDV = maHDV
            };

            dal.Update_LichTrinh(lichTrinh);
        }

        public bool DeleteHDV_LT(string maChuyenDi, DateTime ngayBatDau)
        {
            return dal.UpdateLichTrinh(maChuyenDi, ngayBatDau);
        }

        public List<string> LayDanhSachEmailHanhKhach(string maChuyenDi, DateTime ngayBatDau)
        {
            return dal.GetEmailsByLichTrinh(maChuyenDi, ngayBatDau);
        }
    }
}
