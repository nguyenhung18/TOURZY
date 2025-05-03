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
    }
}
