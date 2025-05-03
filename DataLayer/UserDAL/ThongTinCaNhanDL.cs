using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public  class ThongTinCaNhanDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public void SaveThongTinCaNhan(int maTaiKhoan, string ten, string sdt, string email, string diaChi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    IF EXISTS (SELECT 1 FROM ThongTinCaNhan WHERE MaTaiKhoan = @MaTaiKhoan)
                        UPDATE ThongTinCaNhan 
                        SET Ten = @Ten, SDT = @SDT, DiaChi = @DiaChi, Email = @Email
                        WHERE MaTaiKhoan = @MaTaiKhoan
                    ELSE
                        INSERT INTO ThongTinCaNhan (MaTaiKhoan, Ten, SDT, DiaChi, Email)
                        VALUES (@MaTaiKhoan, @Ten, @SDT, @DiaChi, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                cmd.Parameters.AddWithValue("@Ten", ten);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@DiaChi",diaChi);
                cmd.Parameters.AddWithValue("@Email",  email);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
