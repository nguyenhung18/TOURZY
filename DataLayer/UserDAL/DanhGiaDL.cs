using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class DanhGiaDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<DanhGiaDTO> GetAllDanhGia()
        {
            List<DanhGiaDTO> danhSach = new List<DanhGiaDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = @"SELECT dg.MaChuyenDi, dg.MaTaiKhoan, cd.TenChuyenDi, ttcn.Ten, dg.Sao, dg.BinhLuan 
                                   FROM DanhGia dg
                                   INNER JOIN ChuyenDi cd ON dg.MaChuyenDi = cd.MaChuyenDi
                                   INNER JOIN ThongTinCaNhan ttcn ON dg.MaTaiKhoan = ttcn.MaTaiKhoan";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DanhGiaDTO danhGia = new DanhGiaDTO
                        {
                            MaChuyenDi = reader["MaChuyenDi"].ToString(),
                            MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                            TenChuyenDi = reader["TenChuyenDi"].ToString(),
                            Ten = reader["Ten"].ToString(),
                            Sao = Convert.ToInt32(reader["Sao"]),
                            BinhLuan = reader["BinhLuan"].ToString()
                        };
                        danhSach.Add(danhGia);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy danh sách đánh giá: {ex.Message}");
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return danhSach;
        }
        public bool DanhGiaDaTonTai(string maChuyenDi, int maTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM DanhGia WHERE MaChuyenDi = @MaChuyenDi AND MaTaiKhoan = @MaTaiKhoan";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public bool ThemDanhGia(DanhGiaDTO danhGia)
        {
            string query = @"INSERT INTO DanhGia (MaChuyenDi, MaTaiKhoan, Sao, BinhLuan)
                         VALUES (@MaChuyenDi, @MaTaiKhoan, @Sao, @BinhLuan)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaChuyenDi", danhGia.MaChuyenDi);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", danhGia.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@Sao", danhGia.Sao);
                cmd.Parameters.AddWithValue("@BinhLuan", danhGia.BinhLuan);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}