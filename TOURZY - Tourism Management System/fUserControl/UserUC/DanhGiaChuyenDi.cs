using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class DanhGiaChuyenDi : UserControl
    {
        private DanhGiaBL bus = new DanhGiaBL();
        private int maTaiKhoan = 1;

        public DanhGiaChuyenDi()
        {
            InitializeComponent();
        }
        private void LoadDanhSach()
        {
            try
            {
                var danhSach = bus.GetAllDanhGia();


                dgv_CacDanhGia.AutoGenerateColumns = false;
                dgv_CacDanhGia.DataSource = null;
                dgv_CacDanhGia.DataSource = danhSach;
                dgv_CacDanhGia.Refresh();

                if (danhSach.Count == 0)
                {
                    MessageBox.Show("Không có đánh giá nào cho chuyến đi này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đánh giá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DanhGiaChuyenDi_Load(object sender, EventArgs e)
        {
            // Configure DataGridView columns if not already set in the designer
            if (dgv_CacDanhGia.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn colMaChuyenDi = new DataGridViewTextBoxColumn
                {
                    Name = "MaChuyenDi",
                    HeaderText = "Mã chuyến đi",
                    DataPropertyName = "MaChuyenDi"
                };
                DataGridViewTextBoxColumn colTenChuyenDi = new DataGridViewTextBoxColumn
                {
                    Name = "TenChuyenDi",
                    HeaderText = "Tên chuyến đi",
                    DataPropertyName = "TenChuyenDi"
                };
                DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn
                {
                    Name = "Ten",
                    HeaderText = "Tên người đánh giá",
                    DataPropertyName = "Ten"
                };
                DataGridViewTextBoxColumn colSao = new DataGridViewTextBoxColumn
                {
                    Name = "Sao",
                    HeaderText = "Số sao",
                    DataPropertyName = "Sao"
                };
                DataGridViewTextBoxColumn colBinhLuan = new DataGridViewTextBoxColumn
                {
                    Name = "BinhLuan",
                    HeaderText = "Bình luận",
                    DataPropertyName = "BinhLuan",
                    Width = 500
                };
               
               
                dgv_CacDanhGia.Columns.AddRange(new DataGridViewColumn[] { colMaChuyenDi, colTenChuyenDi, colTen, colSao, colBinhLuan });
            }

            dgv_CacDanhGia.AutoGenerateColumns = false;
            dgv_CacDanhGia.Visible = true;
            dgv_CacDanhGia.ReadOnly = true;

            try
            {
                LoadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đánh giá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn__DanhGia_Click(object sender, EventArgs e)
        {
            // Get MaChuyenDi from the selected row in DataGridView
            if (dgv_CacDanhGia.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một chuyến đi từ danh sách để đánh giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maChuyenDi = dgv_CacDanhGia.SelectedRows[0].Cells["MaChuyenDi"].Value.ToString();
            string saoInput = tb_SoSao.Text.Trim();
            string binhLuan = rtb_NhanXet.Text.Trim();

            // Validation for Sao (rating)
            int sao;
            if (!int.TryParse(saoInput, out sao))
            {
                MessageBox.Show("Số sao phải là một số nguyên hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sao < 1 || sao > 5)
            {
                MessageBox.Show("Số sao phải từ 1 đến 5!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation for BinhLuan (comment)
            if (string.IsNullOrEmpty(binhLuan))
            {
                MessageBox.Show("Vui lòng nhập nhận xét!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create a new DanhGiaDTO object
            DanhGiaDTO newDanhGia = new DanhGiaDTO
            {
                MaChuyenDi = maChuyenDi,
                MaTaiKhoan = maTaiKhoan, // Hardcoded for now
                Sao = sao,
                BinhLuan = binhLuan
            };

            try
            {
                bool result = bus.AddDanhGia(newDanhGia);
                if (result)
                {
                    MessageBox.Show("Thêm đánh giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSach(); // Refresh the DataGridView with the current filter
                    rtb_NhanXet.Clear(); // Clear the comment field
                    tb_SoSao.Clear(); // Clear the rating field
                }
                else
                {
                    MessageBox.Show("Thêm đánh giá thất bại! Kiểm tra xem mã chuyến đi có tồn tại không.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đánh giá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_CacDanhGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // đảm bảo không click vào header
            {
               DataGridViewRow row = dgv_CacDanhGia.Rows[e.RowIndex];

               string nhanXet = row.Cells["BinhLuan"].Value?.ToString() ?? "";
               string soSaoStr = row.Cells["Sao"].Value?.ToString() ?? "";

               rtb_NhanXet.Text = nhanXet;
               tb_SoSao.Text = soSaoStr;
            }
        }
    }
}

