using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class LichTrinhDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<LichTrinhDTO> GetLichTrinhWithSoLuong(DateTime fromDate, DateTime toDate)
        {
            List<LichTrinhDTO> list = new List<LichTrinhDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetLichTrinhWithSoLuong", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LichTrinhDTO dto = new LichTrinhDTO
                    {
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        MaHDV = reader["MaHDV"].ToString(),
                        SoLuongNow = Convert.ToInt32(reader["SoLuongNow"]),
                        SoLuongMax = Convert.ToInt32(reader["SoLuongMax"])
                    };
                    list.Add(dto);
                }
                reader.Close();
            }

            return list;
        }

        public List<LichTrinhDTO> FilterData(string maChuyenDi, string maHDV, int soLuong, bool notEligible)
        {
            List<LichTrinhDTO> list = new List<LichTrinhDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_FilterLichTrinh", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", string.IsNullOrEmpty(maChuyenDi) ? (object)DBNull.Value : maChuyenDi);
                cmd.Parameters.AddWithValue("@MaHDV", string.IsNullOrEmpty(maHDV) ? (object)DBNull.Value : maHDV);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@NotEligible", notEligible);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new LichTrinhDTO
                    {
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        MaHDV = reader["MaHDV"].ToString(),
                        SoLuongNow = Convert.ToInt32(reader["SoLuongHienTai"]),
                        SoLuongMax = Convert.ToInt32(reader["SoLuongToiDa"])
                    });
                }
            }
            return list;
        }

        public void DeleteItinerary(LichTrinhDTO lt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteItinerary", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaChuyenDi", lt.MaChuyenDi);
                    cmd.Parameters.AddWithValue("@NgayBatDau", lt.NgayBatDau);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<LichTrinhDTO> GetLichTrinh()
        {
            List<LichTrinhDTO> lts = new List<LichTrinhDTO>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllHDVTours", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LichTrinhDTO lichTrinh = new LichTrinhDTO
                    {
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        MaHDV = reader["MaHDV"].ToString()
                    };
                    lts.Add(lichTrinh);
                }
                reader.Close();
            }
            return lts;
        }

        public void Update_LichTrinh(LichTrinhDTO lichTrinh)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateLichTrinh", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaChuyenDi", lichTrinh.MaChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", lichTrinh.NgayBatDau);
                cmd.Parameters.AddWithValue("@MaHDV", lichTrinh.MaHDV);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public bool UpdateLichTrinh(string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteHDV", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public List<string> GetEmailsByLichTrinh(string maChuyenDi, DateTime ngayBatDau)
        {
            List<string> emails = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetEmailsByLichTrinh", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emails.Add(reader.GetString(0));
                    }
                }
            }

            return emails;
        }

    }
}
