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

        public bool ThemYeuCau(TaoChuyenDiMoiDTO yc)
        {
            string query = "INSERT INTO YeuCau (TenNguoiGui, TenChuyenDi, NgayBatDau, SoLuong) VALUES (@TenNguoiGui, @TenChuyenDi, @NgayBatDau, @SoLuong)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TenNguoiGui", yc.TenNguoiGui);
                cmd.Parameters.AddWithValue("@TenChuyenDi", yc.TenChuyenDi);
                cmd.Parameters.AddWithValue("@NgayBatDau", yc.NgayBatDau);
                cmd.Parameters.AddWithValue("@SoLuong", yc.SoLuong);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }

    }
}

