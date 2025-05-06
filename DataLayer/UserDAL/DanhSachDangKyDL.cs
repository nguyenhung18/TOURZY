using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
   public  class DanhSachDangKyDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";
        public bool SaveDanhSachDangKy(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau, int soLuong, string trangThai)
        {
            try
            {
                  if (!CheckLichTrinhTonTai(maChuyenDi, ngayBatDau))
            {
                throw new Exception("Không tồn tại lịch trình với mã chuyến đi và ngày bắt đầu này!");
            }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        INSERT INTO DanhSachDangKy (MaTaiKhoan, MaChuyenDi, NgayBatDau, SoLuong, TrangThai)
                        VALUES (@MaTaiKhoan, @MaChuyenDi, @NgayBatDau, @SoLuong, @TrangThai)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                        cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm vào danh sách đăng ký: " + ex.Message);
            }
        }
        public bool CheckLichTrinhTonTai(string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM LichTrinh WHERE MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public bool KiemTraTonTai(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM DanhSachDangKy 
                         WHERE MaTaiKhoan = @MaTaiKhoan AND MaChuyenDi = @MaChuyenDi AND NgayBatDau = @NgayBatDau";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
