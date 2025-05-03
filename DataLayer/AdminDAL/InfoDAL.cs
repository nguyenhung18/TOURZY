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
    public class InfoDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";
        public SqlConnection conn = new SqlConnection(connectionString);

        public bool AddUserInfo(InfoDTO info)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddIn4", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", info.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@Ten", info.Ten);
                cmd.Parameters.AddWithValue("@SDT", info.SDT);
                cmd.Parameters.AddWithValue("@DiaChi", info.DiaChi);
                cmd.Parameters.AddWithValue("@Email", info.Email);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }


        public List<AccountDTO> GetAllAccounts()
        {
            List<AccountDTO> accounts = new List<AccountDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    accounts.Add(new AccountDTO
                    {
                        ID = reader.GetInt32(0), // ID
                        TenDangNhap = reader.GetString(1), // TenDangNhap
                        MatKhau = reader.IsDBNull(2) ? null : reader.GetString(2), // MatKhau
                        VaiTro = reader.IsDBNull(3) ? null : reader.GetString(3), // VaiTro
                        IsDeleted = reader.GetBoolean(4) // IsDeleted
                    });
                }
            }
            return accounts;
        }

        public InfoDTO GetInfoByAccountID(int accountId)
        {
            InfoDTO info = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetInfoByAccountID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", accountId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    info = new InfoDTO
                    {
                        MaTaiKhoan = reader.GetInt32(0),
                        Ten = reader.GetString(1),
                        SDT = reader.GetString(2),
                        DiaChi = reader.GetString(3),
                        Email = reader.GetString(4)
                    };
                }
            }
            return info;
        }

        public List<DanhSachDangKy> GetJoinedToursByAccount(int matk)
        {
            List<DanhSachDangKy> list = new List<DanhSachDangKy>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("sp_GetToursByAccount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", matk);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DanhSachDangKy dto = new DanhSachDangKy
                        {
                            MaTaiKhoan = reader.GetInt32(0),
                            MaChuyenDi = reader.GetString(1),
                            NgayBatDau = reader.GetDateTime(2),
                            SoLuong = reader.GetInt32(3),
                            TrangThai = reader.GetString(4)
                        };
                        list.Add(dto);
                    }
                }
            }

            return list;
        }

        public void DeleteAccount(int accountId)
        {
          SqlCommand cmd = new SqlCommand("sp_DeleteAccount", conn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("@MaTaiKhoan", accountId);
          conn.Open();
          cmd.ExecuteNonQuery();
          conn.Close();  
        }

        public bool CheckUsername(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_KiemTraTenDangNhapTonTai", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public int AddAccount(AccountDTO account)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ThemTaiKhoan", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TenDangNhap", account.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", account.MatKhau);
                cmd.Parameters.AddWithValue("@VaiTro", account.VaiTro);

                SqlParameter outputId = new SqlParameter("@NewID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }


        public void AddInfo(InfoDTO info)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ThemThongTinCaNhan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", info.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@Ten", info.Ten);
                cmd.Parameters.AddWithValue("@SDT", info.SDT);
                cmd.Parameters.AddWithValue("@DiaChi", info.DiaChi);
                cmd.Parameters.AddWithValue("@Email", info.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateInfo(InfoDTO info)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_CapNhatThongTinCaNhan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTaiKhoan", info.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@Ten", info.Ten);
                cmd.Parameters.AddWithValue("@SDT", info.SDT);
                cmd.Parameters.AddWithValue("@DiaChi", info.DiaChi);
                cmd.Parameters.AddWithValue("@Email", info.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
