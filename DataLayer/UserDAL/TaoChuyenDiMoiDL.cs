using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
namespace DataLayer
{
    public class TaoChuyenDiMoiDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public bool SaveYeuCau(TaoChuyenDiMoiDTO dto, out string error)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO YeuCau (MaTaiKhoan, MaChuyenDi, NgayBatDau, SoLuong) 
                                 VALUES (@MaTaiKhoan, @MaChuyenDi, @NgayBatDau, @SoLuong)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", dto.MaTaiKhoan);
                    cmd.Parameters.AddWithValue("@MaChuyenDi", dto.MaChuyenDi);
                    cmd.Parameters.AddWithValue("@NgayBatDau", dto.NgayBatDau);
                    cmd.Parameters.AddWithValue("@SoLuong", dto.SoLuong);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    error = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool KiemTraYeuCauTonTai(int maTaiKhoan, string maChuyenDi, DateTime ngayBatDau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
     SELECT COUNT(*) FROM YEUCAU
     WHERE MaTaiKhoan = @MaTaiKhoan
       AND MaChuyenDi = @MaChuyenDi
       AND CAST(NgayBatDau AS DATE) = @NgayBatDau";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                    cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau.Date); // bỏ phần giờ
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}