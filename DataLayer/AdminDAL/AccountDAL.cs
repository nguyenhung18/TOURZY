using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TransferObject;
using static DataLayer.AccountDAL;
using static System.Net.WebRequestMethods;
using System.Data;
using System.Data.Common;

namespace DataLayer
{
    public class AccountDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public int GetMaTaiKhoanByUsername(string username)
        {
            int maTaiKhoan = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenDangNhap", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    maTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]);
                }
                conn.Close();
            }
            return maTaiKhoan;
        }

        public AccountDTO DangNhap(string tenDangNhap, string matKhau)
        {
            AccountDTO taiKhoan = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DangNhap", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        command.Parameters.AddWithValue("@MatKhau", matKhau);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                taiKhoan = new AccountDTO
                                {
                                    ID = reader.GetInt32(0),
                                    TenDangNhap = reader.GetString(1),
                                    MatKhau = reader.GetString(2),
                                    VaiTro = reader.GetString(3),
                                    IsDeleted = reader.GetBoolean(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
            return taiKhoan;
        }

        public bool KiemTraTaiKhoan(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_KiemTraTaiKhoan", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool DangKy(AccountDTO newAccount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DangKy", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TenDangNhap", newAccount.TenDangNhap);
                        command.Parameters.AddWithValue("@MatKhau", newAccount.MatKhau);
                        command.Parameters.AddWithValue("@VaiTro", newAccount.VaiTro);
                        command.Parameters.AddWithValue("@IsDeleted", newAccount.IsDeleted);

                        int result = command.ExecuteNonQuery(); // Nếu thành công thì trả về số dòng ảnh hưởng
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return false;
            }
        }


        public bool KiemTra(string username, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_KiemTra", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void UpdateOTP(string username, string otp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateOTP", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OTP", otp);
                    cmd.Parameters.AddWithValue("@Expiry", DateTime.Now.AddMinutes(3));
                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool XacNhanOTP(string username, string otpInput)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_XacNhanOTP", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string resetOTP = reader["ResetOTP"].ToString();
                            DateTime otpExpiry = Convert.ToDateTime(reader["OTPExpiry"]);

                            // Kiểm tra OTP và thời gian hết hạn
                            if (resetOTP == otpInput && otpExpiry > DateTime.Now)
                            {
                                return true; // OTP đúng và chưa hết hạn
                            }
                        }
                    }
                }
            }
            return false; // OTP không đúng hoặc đã hết hạn
        }

        // Lấy mật khẩu từ cơ sở dữ liệu
        public string LayMatKhau(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_LayMatKhau", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenDangNhap", username);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }
    }
}