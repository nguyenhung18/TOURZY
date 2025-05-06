using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TransferObject;
namespace DataLayer
{

    public class ChuyenDiDL
    {
        private string connectionString = @"Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<ChuyenDiDTO> FindTour(string diemDen, int giaToiThieu, DateTime? ngayKhoiHanh = null, int? soSao = null)
        {
            List<ChuyenDiDTO> result = new List<ChuyenDiDTO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT cd.MaChuyenDi, cd.TenChuyenDi, cd.HinhThuc, cd.HanhTrinh,
                               cd.SoNgayDi, cd.Gia, cd.SoLuong, cd.ChiTiet, cd.MoTa,
                               (SELECT TOP 1 lt.NgayBatDau 
                                FROM LichTrinh lt 
                                WHERE lt.MaChuyenDi = cd.MaChuyenDi 
                                ORDER BY lt.NgayBatDau DESC) AS NgayBatDau,
                               (SELECT AVG(CAST(dg.Sao AS FLOAT)) 
                                FROM DanhGia dg 
                                WHERE dg.MaChuyenDi = cd.MaChuyenDi) AS Sao
                        FROM ChuyenDi cd
                        WHERE cd.HanhTrinh LIKE @DiemDen
                          AND cd.Gia >= @GiaToiThieu";

                    if (ngayKhoiHanh.HasValue)
                    {
                        query += " AND EXISTS (SELECT 1 FROM LichTrinh lt WHERE lt.MaChuyenDi = cd.MaChuyenDi AND CONVERT(date, lt.NgayBatDau) = @NgayKhoiHanh)";
                    }

                    if (soSao.HasValue)
                    {
                        query += " AND (SELECT AVG(CAST(dg.Sao AS FLOAT)) FROM DanhGia dg WHERE dg.MaChuyenDi = cd.MaChuyenDi) = @SoSao";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DiemDen", $"%{diemDen}%");
                        cmd.Parameters.AddWithValue("@GiaToiThieu", giaToiThieu);

                        if (ngayKhoiHanh.HasValue)
                            cmd.Parameters.AddWithValue("@NgayKhoiHanh", ngayKhoiHanh.Value.Date);

                        if (soSao.HasValue)
                            cmd.Parameters.AddWithValue("@SoSao", soSao.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dto = new ChuyenDiDTO
                                {
                                    MaChuyenDi = reader["MaChuyenDi"].ToString(),
                                    TenChuyenDi = reader["TenChuyenDi"].ToString(),
                                    HinhThuc = reader["HinhThuc"].ToString(),
                                    HanhTrinh = reader["HanhTrinh"].ToString(),
                                    NgayBatDau = reader["NgayBatDau"] != DBNull.Value ? Convert.ToDateTime(reader["NgayBatDau"]) : (DateTime?)null,
                                    SoNgayDi = Convert.ToInt32(reader["SoNgayDi"]),
                                    Gia = Convert.ToDecimal(reader["Gia"]),
                                    SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                    ChiTiet = reader["ChiTiet"].ToString(),
                                    MoTa = reader["MoTa"].ToString(),
                                    SoSao = reader["Sao"] != DBNull.Value ? (int)Math.Round(Convert.ToDouble(reader["Sao"])) : 0
                                };
                                // Debug để kiểm tra dữ liệu
                                Console.WriteLine($"TenChuyenDi: {dto.TenChuyenDi}, HanhTrinh: {dto.HanhTrinh}, Sao: {dto.SoSao}");
                                result.Add(dto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy dữ liệu: {ex.Message}");
                throw;
            }

            return result;
        }
        public List<string> GetAllTenChuyenDi()
        {
            List<string> danhSachTen = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TenChuyenDi FROM ChuyenDi";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    danhSachTen.Add(reader.GetString(0));
                }
            }

            return danhSachTen;
        }
        public string GetMaChuyenDiByTen(string tenChuyenDi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaChuyenDi FROM ChuyenDi WHERE TenChuyenDi = @TenChuyenDi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenChuyenDi", tenChuyenDi);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString() ?? throw new Exception("Chuyến đi không tồn tại");
            }
        }
        public DateTime GetNgayBatDauByMaChuyenDi(string maChuyenDi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TOP 1 NgayBatDau FROM LichTrinh WHERE MaChuyenDi = @MaChuyenDi ORDER BY NgayBatDau DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaChuyenDi", maChuyenDi);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToDateTime(result);
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy lịch trình cho chuyến đi này.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy ngày bắt đầu: " + ex.Message);
            }
        }
    }
}

