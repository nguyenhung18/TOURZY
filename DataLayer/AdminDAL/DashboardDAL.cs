using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;
using System.Data.SqlClient;


namespace DataLayer
{
    public class DashboardDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public int SoLuongCus()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE VaiTro='user' AND IsDeleted = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int SoLuongTour()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM ChuyenDi";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public string DoanhThuNgay()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT SUM(CAST(cd.Gia * dsdk.SoLuong AS DECIMAL(18,2))) 
                            FROM DanhSachDangKy dsdk 
                            JOIN ChuyenDi cd ON dsdk.MaChuyenDi = cd.MaChuyenDi
                            WHERE CAST(dsdk.NgayBatDau AS DATE) = CAST(GETDATE() AS DATE)
                            AND dsdk.TrangThai = N'Đã xác nhận'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        return "0 VNĐ";
                    }
                    decimal doanhThu = Convert.ToDecimal(result);
                    return $"{doanhThu:##,###} VNĐ";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tính doanh thu: {ex.Message}");
                return "0 VNĐ";
            }
        }

        public string DoanhThuThang()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"SELECT SUM(CAST(cd.Gia * dsdk.SoLuong AS DECIMAL(18,2))) 
                            FROM DanhSachDangKy dsdk 
                            JOIN ChuyenDi cd ON dsdk.MaChuyenDi = cd.MaChuyenDi
                            WHERE MONTH(dsdk.NgayBatDau) = MONTH(GETDATE())
                            AND YEAR(dsdk.NgayBatDau) = YEAR(GETDATE())
                            AND dsdk.TrangThai = N'Đã xác nhận'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        return "0 VNĐ";
                    }
                    decimal doanhThu = Convert.ToDecimal(result);
                    return $"{doanhThu:##,###} VNĐ";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tính doanh thu: {ex.Message}");
                return "0 VNĐ";
            }
        }
    }
}
