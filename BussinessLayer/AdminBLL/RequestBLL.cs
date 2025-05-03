using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class RequestBLL
    {
        private RequesDAL dal = new RequesDAL();
        public List<RequestDTO> GetYeuCauList()
        {
            return dal.GetRequests();
        }

        public List<AccountDTO> GetUsernames()
        {
            return dal.GetUsernames();
        }

        public string GetIDAccount(string username)
        {
            return dal.GetIDAccount(username);
        }

        public string GetEmail(string matk)
        {
            return dal.GetEmail(matk);
        }

        public void DeleteYeuCau(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau)
        {
            dal.DeleteYeuCau(maTaiKhoan, maChuyenDi, ngayBatDau);
        }

        public void AddItinerary(LichTrinhDTO itinerary)
        {
            dal.AddItinerary(itinerary);
        }

        public bool CheckLichTrinhExists(string maChuyenDi, DateTime ngayBatDau)
        {
            return dal.CheckLichTrinhExists(maChuyenDi, ngayBatDau);
        }

        public bool CheckChuyenDiExists(string maChuyenDi)
        {
            return dal.CheckChuyenDiExists(maChuyenDi);
        }


    }
}
