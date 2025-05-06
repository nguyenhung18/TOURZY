using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DanhSachDuKhachDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public void SaveDanhSachDuKhach(string maChuyenDi, DateTime ngayBatDau, string cccd, string ten, string sdt)
        {
            if (!CheckLichTrinhTonTai(maChuyenDi, ngayBatDau))
            {
                throw new Exception("Không tồn tại lịch trình với mã chuyến đi và ngày bắt đầu này!");
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO DanhSachDuKhach (MaChuyenDi, NgayBatDau, CCCD, Ten, SDT)
                    VALUES (@MaChuyenDi, @NgayBatDau, @CCCD, @Ten, @SDT)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@CCCD", cccd);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@SDT", sdt);

                conn.Open();
                cmd.ExecuteNonQuery();
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
    }
}