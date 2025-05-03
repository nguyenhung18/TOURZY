using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class AdminDashboard : UserControl
    {
        private DashboardBLL dbDLL = new DashboardBLL();
        private WeatherBLL weatherBll = new WeatherBLL();
        private LichTrinhBLL bll = new LichTrinhBLL();
        public AdminDashboard()
        {
            InitializeComponent();
            LoadDashboard();
            SetupDataGridView();
            LoadThoiTiet();
        }

        public void LoadDashboard()
        {
            DashboardDTO dashboardDTO = dbDLL.DuLieuDB();

            totalCus.Text = dashboardDTO.CustomerCount.ToString();
            totalTour.Text = dashboardDTO.TourCount.ToString();
            todayInc.Text = dashboardDTO.DailyRevenue.ToString();
            totalInc.Text = dashboardDTO.MonthlyRevenue.ToString();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
        }

        private void SetupDataGridView()
        {
            if (weather_data == null)
            {
                MessageBox.Show("DataGridView 'weather_data' chưa được khởi tạo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            weather_data.Columns.Clear();
            weather_data.Columns.Add("MaChuyenDi", "Mã Chuyến Đi");
            weather_data.Columns.Add("Ngay", "Ngày");
            weather_data.Columns.Add("DiaDiem", "Địa Điểm");
            weather_data.Columns.Add("DuBao", "Dự Báo");
            weather_data.Columns.Add("TrangThai", "Trạng Thái");
            weather_data.CellFormatting += weather_data_CellFormatting;
            weather_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void weather_data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (weather_data.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();
                switch (trangThai)
                {
                    case "Xấu":
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        break;
                    case "Trung bình":
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Tốt":
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Không xác định":
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                        weather_data.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void LoadThoiTiet()
        {
            try
            {
                weather_data.Rows.Clear();
                var thoiTietList = weatherBll.LayTatCaThoiTiet();
                if (thoiTietList == null || !thoiTietList.Any())
                {
                    MessageBox.Show("Không có dữ liệu thời tiết để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (var tt in thoiTietList)
                {
                    weather_data.Rows.Add(tt.MaChuyenDi, tt.Ngay.ToString("yyyy-MM-dd"), tt.DiaDiem, tt.DuBao, tt.TrangThai);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thời tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                await weatherBll.UpdateThoiTietForAllChuyenDi();
                LoadThoiTiet();
                MessageBox.Show("Đã cập nhật thời tiết cho tất cả chuyến đi!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thời tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
