using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TransferObject;

namespace DataLayer
{
    public class TourDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<TourDTO> GetAllTours()
        {
            List<TourDTO> tours = new List<TourDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllTours", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TourDTO tour = new TourDTO
                    {
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        TenChuyenDi = reader["TenChuyenDi"].ToString(),
                        HinhThuc = reader["HinhThuc"].ToString(),
                        HanhTrinh = reader["HanhTrinh"].ToString(),
                        SoNgayDi = Convert.ToInt32(reader["SoNgayDi"]),
                        Gia = Convert.ToInt32(reader["Gia"]),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        ChiTiet = reader["ChiTiet"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    tours.Add(tour);
                }

                reader.Close();
            }

            return tours;
        }

        public TourDTO GetNameTour(string IDTour)
        {
            TourDTO tour = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetNameTour", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", IDTour);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tour = new TourDTO
                        {
                            TenChuyenDi = reader["TenChuyenDi"].ToString(),
                            HinhThuc = reader["HinhThuc"].ToString()
                        };
                    }
                }
            }

            return tour;
        }

        public bool UpdateTour(TourDTO tour)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateTour", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", tour.MaChuyenDi);
                cmd.Parameters.AddWithValue("@TenChuyenDi", tour.TenChuyenDi);
                cmd.Parameters.AddWithValue("@HinhThuc", tour.HinhThuc);
                cmd.Parameters.AddWithValue("@HanhTrinh", tour.HanhTrinh);
                cmd.Parameters.AddWithValue("@SoNgayDi", tour.SoNgayDi);
                cmd.Parameters.AddWithValue("@Gia", tour.Gia);
                cmd.Parameters.AddWithValue("@SoLuong", tour.SoLuong);
                cmd.Parameters.AddWithValue("@ChiTiet", tour.ChiTiet);
                cmd.Parameters.AddWithValue("@MoTa", tour.MoTa);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool DeleteTour(string IDTour)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteTour", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", IDTour);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }


        public bool AddTour(TourDTO tour)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddTour", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaChuyenDi", tour.MaChuyenDi);
                    cmd.Parameters.AddWithValue("@TenChuyenDi", tour.TenChuyenDi);
                    cmd.Parameters.AddWithValue("@HinhThuc", tour.HinhThuc);
                    cmd.Parameters.AddWithValue("@HanhTrinh", tour.HanhTrinh);
                    cmd.Parameters.AddWithValue("@SoNgayDi", tour.SoNgayDi);
                    cmd.Parameters.AddWithValue("@Gia", tour.Gia);
                    cmd.Parameters.AddWithValue("@SoLuong", tour.SoLuong);
                    cmd.Parameters.AddWithValue("@ChiTiet", tour.ChiTiet);
                    cmd.Parameters.AddWithValue("@MoTa", tour.MoTa);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}

