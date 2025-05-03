using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DSDuKhachDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<DSDuKhachDTO> GetDSDuKhach()
        {
            List<DSDuKhachDTO> ds = new List<DSDuKhachDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetDSDuKhach", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DSDuKhachDTO duKhach = new DSDuKhachDTO
                    {
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        CCCD = reader["CCCD"].ToString(),
                        Ten = reader["Ten"].ToString(),
                        SDT = reader["SDT"].ToString(),
                    };
                    ds.Add(duKhach);
                }
            }
            return ds;            
        }
        public List<DateTime> GetNgayBatDauByTour(string tourId)
        {
            List<DateTime> dates = new List<DateTime>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetNgayBatDauByTour", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", tourId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dates.Add(Convert.ToDateTime(reader["NgayBatDau"]));
                }
            }

            return dates;
        }

        public int CountHanhKhachByTourAndDate(string maChuyenDi, DateTime ngayBatDau)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_CountHanhKhach", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);

                count = (int)cmd.ExecuteScalar();
            }
            return count;
        }

        public List<DSDuKhachDTO> FilterCustomer(bool att1, bool att2, bool att3, bool att4, string ma, DateTime date, string cccd, string ten)
        {
            List<DSDuKhachDTO> customerList = new List<DSDuKhachDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_FilterCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số cho stored procedure
                cmd.Parameters.AddWithValue("@MaChuyenDi", ma);
                cmd.Parameters.AddWithValue("@NgayBatDau", date);
                cmd.Parameters.AddWithValue("@CCCD", cccd);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@Att1", att1);
                cmd.Parameters.AddWithValue("@Att2", att2);
                cmd.Parameters.AddWithValue("@Att3", att3);
                cmd.Parameters.AddWithValue("@Att4", att4);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DSDuKhachDTO customer = new DSDuKhachDTO
                    {
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        CCCD = reader["CCCD"].ToString(),
                        Ten = reader["Ten"].ToString(),
                        SDT = reader["SDT"].ToString()
                    };

                    customerList.Add(customer);
                }

                reader.Close();
            }

            return customerList;
        }
        public bool DeleteCustomer(string maChuyenDi, DateTime ngayBatDau, string cccd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@CCCD", cccd);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public bool AddCustomer(string maChuyenDi, DateTime ngayBatDau, string cccd, string ten, string sdt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@SDT", sdt);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
            }
        }
        public bool UpdateCustomer(string maChuyenDi, DateTime ngayBatDau, string cccd, string ten, string sdt)
        {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@SDT", sdt);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
        }

        public bool IsValidLichTrinh(string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM DanhSachDuKhach WHERE MaChuyenDi = @ma AND NgayBatDau = @ngay", conn);
                cmd.Parameters.AddWithValue("@ma", maChuyenDi);
                cmd.Parameters.AddWithValue("@ngay", ngayBatDau);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


    }
}

