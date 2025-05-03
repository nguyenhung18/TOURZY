using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;
using DataLayer;
using System.Data.SqlClient;


namespace BussinessLayer
{
    public class TourBLL
    {
        private TourDAL dal = new TourDAL();

        public List<TourDTO> GetAllTours()
        {
            return dal.GetAllTours();
        }

        public TourDTO GetNameTour(string IDTour)
        {
            return dal.GetNameTour(IDTour);
        }

        public bool UpdateTour(TourDTO tour)
        {
            return dal.UpdateTour(tour);
        }

        public bool DeleteTour(TourDTO tour)
        {
            return dal.DeleteTour(tour.MaChuyenDi);
        }

        public bool AddTour(TourDTO tour)
        {
            // Optional: Validate logic trước khi gọi DAL nếu cần
            return dal.AddTour(tour);
        }

    }
}
