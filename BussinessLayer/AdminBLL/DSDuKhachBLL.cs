using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;

namespace BussinessLayer
{
    public class DSDuKhachBLL
    {
        private DSDuKhachDAL dal = new DSDuKhachDAL();

        public List<DSDuKhachDTO> GetDSDuKhach() {
            return dal.GetDSDuKhach();
        }
        public List<DateTime> GetNgayBatDauByTour(string tourId)
        {
            return dal.GetNgayBatDauByTour(tourId);
        }
        public int CountHanhKhachByTourAndDate(string maChuyenDi, DateTime ngayBatDau)
        {
            return dal.CountHanhKhachByTourAndDate(maChuyenDi, ngayBatDau);
        }
        public List<DSDuKhachDTO> GetFilteredCustomer(bool att1, bool att2, bool att3, bool att4, string ma, DateTime date, string cccd, string ten)
        {
            return dal.FilterCustomer(att1, att2, att3, att4, ma, date, cccd, ten);
        }

        public bool DeleteCustomer(string maChuyenDi, DateTime ngayBatDau, string cccd)
        {
            return dal.DeleteCustomer(maChuyenDi, ngayBatDau, cccd);
        }

        public bool UpdateCustomer(string maChuyenDi, DateTime ngayBatDau, string cccd, string ten, string sdt)
        {
            return dal.UpdateCustomer(maChuyenDi, ngayBatDau, cccd, ten, sdt);
        }

        public bool AddCustomer(string maChuyenDi, DateTime ngayBatDau, string cccd, string ten, string sdt)
        {
            if (!dal.IsValidLichTrinh(maChuyenDi, ngayBatDau))
            {
                return false;
            }

            return dal.AddCustomer(maChuyenDi, ngayBatDau, cccd, ten, sdt);
        }
    }
}
