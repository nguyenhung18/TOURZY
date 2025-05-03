using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public  class DanhSachDangKyDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";
        public void SaveDanhSachDangKy(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau, int soLuong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO DanhSachDangKy (MaTaiKhoan, MaChuyenDi, NgayBatDau, SoLuong, TrangThai)
            VALUES (@MaTaiKhoan, @MaChuyenDi, @NgayBatDau, @SoLuong, @TrangThai)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@TrangThai", "Đã thanh toán");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
