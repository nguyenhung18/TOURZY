using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class GuiderDAL
    {
        private const string connectionString = "Data Source=.;Initial Catalog=TOURZY;Integrated Security=True";

        public List<GuiderDTO> LoadGuides()
        {
            List<GuiderDTO> list = new List<GuiderDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllGuides", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GuiderDTO guider = new GuiderDTO
                    {
                        MaHDV = reader["MaHDV"].ToString(),
                        Ten = reader["Ten"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                    list.Add(guider);
                }

                reader.Close();
            }

            return list;
        }

        public List<string> LoadGuiderIDs()
        {
            List<string> ids = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllGuides", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ids.Add(reader["MaHDV"].ToString());
                }

                reader.Close();
            }

            return ids;
        }

        public List<LichTrinhDTO> GetLichTrinhByHDV(string maHDV)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<LichTrinhDTO> list = new List<LichTrinhDTO>();
            SqlCommand cmd = new SqlCommand("sp_LichTrinhHD", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaHDV", maHDV);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LichTrinhDTO lt = new LichTrinhDTO()
                {
                    MaChuyenDi = reader["MaChuyenDi"].ToString(),
                    NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                    MaHDV = reader["MaHDV"].ToString()
                };
                list.Add(lt);
            }
            conn.Close();
            return list;
        }

        public void XoaHuongDanVien(string maHDV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteGuide", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHDV", maHDV);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public string TaoMaHDVTuDong()
        {
            string maHDV = "HDV001";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 MaHDV FROM HuongDanVien ORDER BY MaHDV DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastMa = result.ToString();
                    int so = int.Parse(lastMa.Substring(3)) + 1;
                    maHDV = "HDV" + so.ToString("D3");
                }
                conn.Close();
            }
            return maHDV;
        }

        public bool ThemHuongDanVien(GuiderDTO hdv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ThemHuongDanVien", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaHDV", hdv.MaHDV);
                    cmd.Parameters.AddWithValue("@Ten", hdv.Ten);
                    cmd.Parameters.AddWithValue("@SDT", hdv.SDT);
                    cmd.Parameters.AddWithValue("@Email", hdv.Email);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateDuongDanVien(GuiderDTO guider)
        {
           try
           {
                using (SqlConnection conn = new SqlConnection(connectionString))
               {
                    SqlCommand cmd = new SqlCommand("sp_UpdateHuongDanVien", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaHDV", guider.MaHDV);
                    cmd.Parameters.AddWithValue("@Ten", guider.Ten);
                    cmd.Parameters.AddWithValue("@SDT", guider.SDT);
                    cmd.Parameters.AddWithValue("@Email", guider.Email);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
               }
           }
           catch 
           { 
                return false; 
           }
        }
    }
}
