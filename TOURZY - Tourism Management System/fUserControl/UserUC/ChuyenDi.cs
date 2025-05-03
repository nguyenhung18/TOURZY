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
using BussinessLayer;
using TransferObject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TOURZY___Tourism_Management_System
{
    public partial class ChuyenDi : UserControl
    {
        private ChuyenDiBL chuyenDiBL = new ChuyenDiBL();
        private TaiKhoanBL taiKhoanBLL = new TaiKhoanBL();
        private string maChuyenDi;
        private DateTime ngayBatDau;

        public int UserId { get; set; }

        // Khai báo sự kiện để thông báo khi nút "Chi Tiết" được nhấn
        public event EventHandler<string> ChiTietClicked;

        public ChuyenDi()
        {
            InitializeComponent();
            this.Load += ChuyenDi_Load;
        }

        private void LoadDSChuyenDi_dgv(List<ChuyenDiDTO> list)
        {
            if (list != null && list.Count > 0)
            {
                dgv_DSChuyenDi.DataSource = null;
                dgv_DSChuyenDi.AutoGenerateColumns = false;
                dgv_DSChuyenDi.Columns.Clear();
                dgv_DSChuyenDi.Font = new Font("Arial", 10);

                // Thêm các cột thủ công và ánh xạ với thuộc tính của DTO
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaChuyenDi",
                    HeaderText = "Mã Chuyến Đi",
                    Name = "MaChuyenDi"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenChuyenDi",
                    HeaderText = "Tên Chuyến Đi",
                    Name = "TenChuyenDi"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "HinhThuc",
                    HeaderText = "Hình Thức",
                    Name = "HinhThuc"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "HanhTrinh",
                    HeaderText = "Hành Trình",
                    Name = "HanhTrinh"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoNgayDi",
                    HeaderText = "Số Ngày Đi",
                    Name = "SoNgayDi"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Gia",
                    HeaderText = "Giá",
                    Name = "Gia"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoLuong",
                    HeaderText = "Số Lượng",
                    Name = "SoLuong"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ChiTiet",
                    HeaderText = "Chi Tiết",
                    Name = "ChiTiet"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MoTa",
                    HeaderText = "Mô Tả",
                    Name = "MoTa"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayBatDau",
                    HeaderText = "Ngày Bắt Đầu",
                    Name = "NgayBatDau"
                });
                dgv_DSChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoSao",
                    HeaderText = "Số Sao",
                    Name = "SoSao"
                });

                // Bind dữ liệu vào DataGridView
                dgv_DSChuyenDi.DataSource = list;

                // Tùy chỉnh chiều rộng cột
                dgv_DSChuyenDi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                dgv_DSChuyenDi.DataSource = null;
                dgv_DSChuyenDi.Columns.Clear();
                MessageBox.Show("Không có TOUR để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ChuyenDi_Load(object sender, EventArgs e)
        {
            List<ChuyenDiDTO> result = chuyenDiBL.TimKiemChuyenDi("", 0, null, null);
            LoadDSChuyenDi_dgv(result);
        }

        private void btn_ChiTiet_Click(object sender, EventArgs e)
        {
            if (dgv_DSChuyenDi.SelectedRows.Count > 0)
            {

                DataGridViewRow row = dgv_DSChuyenDi.SelectedRows[0];

                // Lấy dữ liệu từ dòng được chọn
                string ten = row.Cells["TenChuyenDi"].Value.ToString();
                string hanhTrinh = row.Cells["HanhTrinh"].Value.ToString();
                string gia = row.Cells["Gia"].Value.ToString();
                string soNguoi = row.Cells["SoLuong"].Value.ToString();
                string soSao = row.Cells["SoSao"].Value.ToString();
                string loaiHinh = row.Cells["HinhThuc"].Value.ToString();
                string moTa = row.Cells["MoTa"].Value.ToString();
                string ngay = Convert.ToDateTime(row.Cells["NgayBatDau"].Value).ToString("dd/MM/yyyy");
                string soNgay = row.Cells["SoNgayDi"].Value.ToString();

                // Tạo form ChiTietChuyenDi và truyền dữ liệu vào
                ChiTietChuyenDi chiTietForm = new ChiTietChuyenDi();
                chiTietForm.UserId = this.UserId;
                chiTietForm.MaChuyenDi = maChuyenDi;
                chiTietForm.NgayBatDau = ngayBatDau;
                chiTietForm.LoadChiTietChuyenDi(ten, hanhTrinh, gia, soNguoi, soSao, loaiHinh, moTa, ngay, soNgay);

                // Tìm form cha là 'User'
                User userForm = this.FindForm() as User;
                if (userForm != null)
                {
                    // Tìm flowLayoutPanel1 chứa các UserControl
                    FlowLayoutPanel flowPanel = userForm.Controls.Find("flowLayoutPanel1", true).FirstOrDefault() as FlowLayoutPanel;
                    if (flowPanel != null)
                    {
                        flowPanel.Controls.Clear(); // Xóa UserControl hiện tại

                        chiTietForm.Dock = DockStyle.Top; // hoặc Fill nếu muốn
                        flowPanel.Controls.Add(chiTietForm);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chuyến đi để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btn_TìmKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ giao diện
                string diemDen = tb_DiemDen.Text.Trim();

                int giaToiThieu = 0;
                if (!string.IsNullOrEmpty(tb_Gia.Text))
                {
                    if (!int.TryParse(tb_Gia.Text.Trim(), out giaToiThieu) || giaToiThieu < 0)
                    {
                        MessageBox.Show("Vui lòng nhập giá là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DateTime? ngayKhoiHanh = cbUseDateTime.Checked ? dateTimePicker_KhoiHanh.Value.Date : (DateTime?)null;

                int? soSao = null;
                if (nud_Sao.Value >= 1 && nud_Sao.Value <= 5)
                {
                    soSao = (int)nud_Sao.Value;
                }

                // Gọi tầng Business Layer để tìm kiếm
                List<ChuyenDiDTO> danhSachChuyenDi = chuyenDiBL.TimKiemChuyenDi(diemDen, giaToiThieu, ngayKhoiHanh, soSao);

                // Hiển thị kết quả
                LoadDSChuyenDi_dgv(danhSachChuyenDi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbUseDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_KhoiHanh.Enabled = cbUseDateTime.Checked;
        }

        private void dgv_DSChuyenDi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể thêm logic nếu cần khi người dùng click vào ô trong DataGridView
        }
    }
}