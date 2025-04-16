using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace TOURZY___Tourism_Management_System
{
    public partial class ChuyenDi : UserControl
    {
        private string connectionString = @"Data Source=.;Initial Catalog=Tourzy;Integrated Security=True;";
      
        public ChuyenDi()
        {
            InitializeComponent();
            this.Load += ChuyenDi_Load;
        }
        // 🔍 Tìm kiếm chuyến đi
        public DataTable FindTour(string diemDen, int giaToiThieu, DateTime? ngayKhoiHanh = null, int? sao = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT MACHUYENDI, TENCHUYENDI, HINHTHUC, HANHTRINH,
                           SONGAYDI, GIA, SOLUONG, CHITIET, NGAYKHOIHANH, SOSAO
                    FROM CHUYENDI
                    WHERE HANHTRINH LIKE @DiemDen
                      AND GIA >= @GiaToiThieu";

                    if (ngayKhoiHanh.HasValue)
                        query += " AND NGAYKHOIHANH = @NgayKhoiHanh";

                    if (sao.HasValue)
                        query += " AND SOSAO = @Sao";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DiemDen", "%" + diemDen + "%");
                    cmd.Parameters.AddWithValue("@GiaToiThieu", giaToiThieu);
                    if (ngayKhoiHanh.HasValue)
                        cmd.Parameters.AddWithValue("@NgayKhoiHanh", ngayKhoiHanh.Value.Date);
                    if (sao.HasValue)
                        cmd.Parameters.AddWithValue("@Sao", sao.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
                    return null;
                }
            }
        }


        private void LoadDSChuyenDi_dgv(DataTable source)
        {
            try
            {
                if (source != null)
                {
                    dgv_DSChuyenDi.DataSource = source;
                    dgv_DSChuyenDi.Columns["MACHUYENDI"].HeaderText = "Mã chuyến đi";
                    dgv_DSChuyenDi.Columns["TENCHUYENDI"].HeaderText = "Tên chuyến đi";
                    dgv_DSChuyenDi.Columns["HINHTHUC"].HeaderText = "Hình thức";
                    dgv_DSChuyenDi.Columns["HANHTRINH"].HeaderText = "Hành trình";
                    dgv_DSChuyenDi.Columns["SONGAYDI"].HeaderText = "Số ngày";
                    dgv_DSChuyenDi.Columns["GIA"].HeaderText = "Giá";
                    dgv_DSChuyenDi.Columns["SOLUONG"].HeaderText = "Số lượng";
                    dgv_DSChuyenDi.Columns["CHITIET"].HeaderText = "Chi tiết";
                    dgv_DSChuyenDi.Columns["NGAYKHOIHANH"].HeaderText = "Ngày khởi hành";
                    dgv_DSChuyenDi.Columns["SOSAO"].HeaderText = "Số sao";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void ChuyenDi_Load(object sender, EventArgs e)
        {
            DataTable dt = FindTour("", 0);
            LoadDSChuyenDi_dgv(dt);  
        }

        private void btn_ChiTiet_Click(object sender, EventArgs e)
        {
            if (dgv_DSChuyenDi.SelectedRows.Count > 0)
            {
                string maChuyenDi = dgv_DSChuyenDi.SelectedRows[0].Cells["MACHUYENDI"].Value.ToString();
                string tenChuyenDi = dgv_DSChuyenDi.SelectedRows[0].Cells["TENCHUYENDI"].Value.ToString();
                string chiTiet = dgv_DSChuyenDi.SelectedRows[0].Cells["CHITIET"].Value.ToString();

                string message = $"Mã chuyến đi: {maChuyenDi}\n" +
                                 $"Tên chuyến đi: {tenChuyenDi}\n" +
                                 $"Chi tiết: {chiTiet}";
                MessageBox.Show(message, "Thông tin chi tiết chuyến đi");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chuyến đi để xem chi tiết!");
            }
        }

        private void btn_TìmKiem_Click(object sender, EventArgs e)
        {
            string diemDen = tb_DiemDen.Text.Trim();

            // Xử lý textbox giá
            int giaToiThieu = 0;
            if (!string.IsNullOrEmpty(tb_Gia.Text))
            {
                if (!int.TryParse(tb_Gia.Text, out giaToiThieu))
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng số cho giá!");
                    return;
                }
            }

            // Ngày khởi hành nếu được bật checkbox
            DateTime? ngayKhoiHanh = cbUseDateTime.Checked ? dateTimePicker_KhoiHanh.Value.Date : (DateTime?)null;

           
            int? soSao = nud_Sao.Value > 0 ? (int?)nud_Sao.Value : null;

            DataTable dt = FindTour(diemDen, giaToiThieu, ngayKhoiHanh, soSao);
            LoadDSChuyenDi_dgv(dt);

        }

        private void cbUseDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_KhoiHanh.Enabled = cbUseDateTime.Checked;
        }
    }
}
