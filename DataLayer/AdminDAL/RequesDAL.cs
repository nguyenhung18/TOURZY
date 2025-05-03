using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class RequesDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<RequestDTO> GetRequests()
        {
            List<RequestDTO> rqs = new List<RequestDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RequestDTO yeuCau = new RequestDTO
                    {
                        MaTaiKhoan = reader["MaTaiKhoan"].ToString(),
                        MaChuyenDi = reader["MaChuyenDi"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        SoLuong = Convert.ToInt32(reader["SoLuong"])
                    };
                    rqs.Add(yeuCau);
                }
            }

            return rqs;   
        }

        public List<AccountDTO> GetUsernames()
        {
            List<AccountDTO> list = new List<AccountDTO>();

            string query = "SELECT TenDangNhap FROM TaiKhoan WHERE VaiTro='user'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new AccountDTO
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString()
                    });
                }

                reader.Close();
            }

            return list;
        }

        public string GetIDAccount(string username)
        {
            string matk = null;

            string query = "SELECT ID,TenDangNhap FROM TaiKhoan WHERE TenDangNhap = @Username";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    matk = result.ToString();
                }
            }

            return matk;
        }

        public string GetEmail(string matk)
        {
            string email = null;

            string query = "SELECT Email FROM ThongTinCaNhan WHERE MaTaiKhoan = @MaTaiKhoan";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", matk);

                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    email = result.ToString();
                }
            }

            return email;
        }

        public void DeleteYeuCau(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteReq", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddItinerary(LichTrinhDTO itinerary)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ThemLichTrinh", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaChuyenDi", itinerary.MaChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", itinerary.NgayBatDau);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi thêm chuyến đi: " + ex.Message);
                }
            }
        }

        public bool CheckLichTrinhExists(string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM LichTrinh WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                    cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau.Date);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool CheckChuyenDiExists(string maChuyenDi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ChuyenDi WHERE MaChuyenDi = @MaChuyenDi", conn);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }



    }
}
