using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class ReviewBLL
    {
        private ReviewDAL reviewDAL = new ReviewDAL();
        public List<ReviewDTO> GetReviews()
        {
            return reviewDAL.GetReviews();
        }

        public List<TourDTO> LoadTours()
        {
            return reviewDAL.LoadTours();
        }

        public List<ReviewDTO> GetReviewsByTour(string maTour)
        {
            return reviewDAL.GetReviewsByTour(maTour);
        }
    }
}
