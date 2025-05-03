using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class ReviewDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";
        public SqlConnection conn = new SqlConnection(connectionString);

        public List<ReviewDTO> GetReviews()
        {
            List<ReviewDTO> reviews = new List<ReviewDTO>();

            SqlCommand cmd = new SqlCommand("sp_GetReviews", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reviews.Add(new ReviewDTO()
                {
                    MaChuyenDi = reader["MaChuyenDi"].ToString(),
                    MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                    Sao = Convert.ToInt32(reader["Sao"]),
                    BinhLuan = reader["BinhLuan"].ToString()
                });
            }
            reader.Close();
            return reviews;
        }

        public List<TourDTO> LoadTours()
        {
            List<TourDTO> tours = new List<TourDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LoadTours", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tours.Add(new TourDTO
                        {
                            MaChuyenDi = reader.GetString(0)
                        });
                    }
                }
            }
            return tours;
        }

        public List<ReviewDTO> GetReviewsByTour(string maChuyenDi)
        {
            List<ReviewDTO> reviews = new List<ReviewDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetReviewsByTour", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new ReviewDTO
                            {
                                MaChuyenDi = reader["MaChuyenDi"].ToString(),
                                MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                                Sao = Convert.ToInt32(reader["Sao"]),
                                BinhLuan = reader["BinhLuan"].ToString()
                            });
                        }
                    }
                }
            }
            return reviews;
        }
    }
}
