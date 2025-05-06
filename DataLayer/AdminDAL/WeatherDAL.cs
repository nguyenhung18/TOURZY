using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TransferObject;

namespace DataLayer
{
    public class WeatherDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public void ThemThoiTiet(WeatherDTO tt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertOrUpdateThoiTiet", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaChuyenDi", tt.MaChuyenDi);
                    cmd.Parameters.AddWithValue("@Ngay", tt.Ngay);
                    cmd.Parameters.AddWithValue("@DiaDiem", tt.DiaDiem);
                    cmd.Parameters.AddWithValue("@DuBao", tt.DuBao);
                    cmd.Parameters.AddWithValue("@TrangThai", tt.TrangThai);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WeatherDTO> LayTatCa()
        {
            List<WeatherDTO> list = new List<WeatherDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ThoiTiet";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new WeatherDTO
                        {
                            MaChuyenDi = reader["MaChuyenDi"].ToString(),
                            Ngay = Convert.ToDateTime(reader["Ngay"]),
                            DiaDiem = reader["DiaDiem"].ToString(),
                            DuBao = reader["DuBao"].ToString(),
                            TrangThai = reader["TrangThai"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }


        public List<TourDTO> LayTatCaChuyenDi()
        {
            List<TourDTO> list = new List<TourDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaChuyenDi, TenChuyenDi FROM ChuyenDi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new TourDTO
                        {
                            MaChuyenDi = reader["MaChuyenDi"].ToString(),
                            TenChuyenDi = reader["TenChuyenDi"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public List<DateTime> LayNgayDiCuaChuyenDi(string maChuyenDi)
        {
            List<DateTime> ngayDiList = new List<DateTime>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT NgayBatDau FROM LichTrinh WHERE MaChuyenDi = @MaChuyenDi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ngayDiList.Add(Convert.ToDateTime(reader["NgayBatDau"]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ngayDiList;
        }
    }
}