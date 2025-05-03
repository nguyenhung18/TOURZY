using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
public class TaiKhoanDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";
        public TaiKhoanDTO GetTaiKhoanByCredentials(string tenDangNhap, string matKhau)
        {
            TaiKhoanDTO taiKhoan = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, TenDangNhap, MatKhau, VaiTro, IsDeleted FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau AND IsDeleted = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau); // Lưu ý: Nên mã hóa mật khẩu trong thực tế

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    taiKhoan = new TaiKhoanDTO
                    {
                        ID = reader.GetInt32(0),
                        TenDangNhap = reader.GetString(1),
                        MatKhau = reader.GetString(2),
                        VaiTro = reader.GetString(3),
                        IsDeleted = reader.GetBoolean(4)
                    };
                }
                conn.Close();
            }return taiKhoan;
        }

        public int GetIDbyUsername(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID FROM TaiKhoan WHERE TenDangNhap = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
    }
}
