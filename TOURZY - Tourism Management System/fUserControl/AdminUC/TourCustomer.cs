using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;

namespace TOURZY___Tourism_Management_System
{
    public partial class TourCustomer : UserControl
    {
        private enum State
        {
            Add,
            Edit,
            None
        }

        public DSDuKhachBLL bll = new DSDuKhachBLL();
        private bool starting = true;
        private State currentState = State.None;

        public TourCustomer()
        {
            InitializeComponent();
            Enable_Info_text(false);
        }

        private void TourCustomer_Load(object sender, EventArgs e)
        {
            LoadDS();
            LoadComboBoxData();
        }

        private void LoadDS()
        {
            dgv_hanhkhach.AllowUserToResizeColumns = true;
            dgv_hanhkhach.AllowUserToOrderColumns = true;
            dgv_hanhkhach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<DSDuKhachDTO> ds = bll.GetDSDuKhach();
            dgv_hanhkhach.DataSource = ds;
            starting = false;
        }

        private void LoadComboBoxData()
        {
            List<DSDuKhachDTO> ds = bll.GetDSDuKhach();
            foreach (var tourID in ds)
            {
                if (!cbb_idtour.Items.Contains(tourID.MaChuyenDi) && !cbb_id_tk.Items.Contains(tourID.MaChuyenDi))
                {
                    cbb_idtour.Items.Add(tourID.MaChuyenDi);
                    cbb_id_tk.Items.Add(tourID.MaChuyenDi);
                }
            }

            foreach (var date in ds)
            {
                if (!cbb_date.Items.Contains(date.NgayBatDau))
                {
                    cbb_date.Items.Add(date.NgayBatDau);
                }
            }

            foreach (var cccd in ds)
            {
                if (!cbb_cccd.Items.Contains(cccd.CCCD))
                    cbb_cccd.Items.Add(cccd.CCCD);
            }

            foreach (var ten in ds)
            {
                if (!cbb_ten.Items.Contains(ten.Ten))
                    cbb_ten.Items.Add(ten.Ten);
            }
        }

        private void Enable_Info_text(bool state)
        {
            tb_id.Enabled = state;
            dtp_ngaydi.Enabled = state;
            tb_cccd.Enabled = state;
            tb_ten.Enabled = state;
            tb_sdt.Enabled = state;
            pnl_save.Enabled = state;
        }

        private void Reset_Text()
        {
            tb_id.ResetText();
            tb_cccd.ResetText();
            tb_ten.ResetText();
            tb_sdt.ResetText();
            dtp_ngaydi.ResetText();
        }

        private void cbb_id_tk_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCustomerCount();
            UpdateNgayDiComboBox();
        }

        private void UpdateNgayDiComboBox()
        {
            cbb_ngaydi_tk.Items.Clear();
            string selectedTourId = cbb_id_tk.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTourId)) return;

            var dates = bll.GetNgayBatDauByTour(selectedTourId);
            foreach (var date in dates)
            {
                cbb_ngaydi_tk.Items.Add(date.ToShortDateString());
            }

            if (cbb_ngaydi_tk.Items.Count > 0)
                cbb_ngaydi_tk.SelectedIndex = 0;
        }

        private void UpdateCustomerCount()
        {
            string selectedTourId = cbb_id_tk.SelectedItem?.ToString();
            string selectedDateStr = cbb_ngaydi_tk.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedTourId) || string.IsNullOrEmpty(selectedDateStr))
            {
                tb_kh.Text = "0";
                return;
            }

            try
            {
                DateTime selectedDate = Convert.ToDateTime(selectedDateStr);
                int count = bll.CountHanhKhachByTourAndDate(selectedTourId, selectedDate);
                tb_kh.Text = count.ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Lỗi format ngày: {ex.Message}");
                tb_kh.Text = "0";
            }
        }

        private void cbb_ngaydi_tk_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCustomerCount();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadDS();
        }

        private void btn_loc_Click(object sender, EventArgs e)
        {
            bool att1 = cb_Id.Checked;
            bool att2 = cb_ngaydi.Checked;
            bool att3 = cb_cccd.Checked;
            bool att4 = cb_ten.Checked;

            string ma = cbb_idtour.Text;
            DateTime date = Convert.ToDateTime(cbb_date.Text);
            string cccd = cbb_cccd.Text;
            string ten = cbb_ten.Text;

            List<DSDuKhachDTO> filteredData = bll.GetFilteredCustomer(att1, att2, att3, att4, ma, date, cccd, ten);

            // Hiển thị kết quả lên DataGridView
            dgv_hanhkhach.DataSource = filteredData;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            currentState = State.Add;
            Enable_Info_text(true);
            Reset_Text();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            currentState = State.Edit;
            Enable_Info_text(true);
            tb_id.Enabled = false;
            dtp_ngaydi.Enabled = false;
            tb_cccd.Enabled = false;
        }

        private void dgv_hanhkhach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedCus = dgv_hanhkhach.Rows[e.RowIndex];

                this.tb_id.Text = selectedCus.Cells[0].Value?.ToString(); // MaChuyenDi
                this.dtp_ngaydi.Value = Convert.ToDateTime(selectedCus.Cells[1].Value); // NgayBatDau
                this.tb_cccd.Text = selectedCus.Cells[2].Value?.ToString(); // CCCD
                this.tb_ten.Text = selectedCus.Cells[3].Value?.ToString(); // Ten
                this.tb_sdt.Text = selectedCus.Cells[4].Value?.ToString(); // SDT
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dgv_hanhkhach.CurrentRow == null || dgv_hanhkhach.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một hành khách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgv_hanhkhach.CurrentRow;

            if (selectedRow.Cells[0].Value == null || selectedRow.Cells[1].Value == null || selectedRow.Cells[2].Value == null)
            {
                MessageBox.Show("Thông tin hành khách không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maChuyenDi = selectedRow.Cells[0].Value.ToString();
            DateTime ngayBatDau = Convert.ToDateTime(selectedRow.Cells[1].Value);
            string cccd = selectedRow.Cells[2].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa hành khách này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                bool success = bll.DeleteCustomer(maChuyenDi, ngayBatDau, cccd);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDS();
                    Reset_Text();
                    Enable_Info_text(false);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            currentState = State.None;
            Enable_Info_text(false);
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_id.Text) || string.IsNullOrEmpty(tb_cccd.Text) || string.IsNullOrEmpty(tb_ten.Text) || string.IsNullOrEmpty(tb_sdt.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maChuyenDi = tb_id.Text;
            DateTime ngayBatDau = dtp_ngaydi.Value.Date.AddHours(0);
            string cccd = tb_cccd.Text;
            string ten = tb_ten.Text;
            string sdt = tb_sdt.Text;

            bool success = false;

            if (currentState == State.Add)
            {
                success = bll.AddCustomer(maChuyenDi, ngayBatDau, cccd, ten, sdt);
            }
            else if (currentState == State.Edit)
            {
                success = bll.UpdateCustomer(maChuyenDi, ngayBatDau, cccd, ten, sdt);
            }

            if (success)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDS();
                Reset_Text();
                Enable_Info_text(false);
                currentState = State.None;
            }
            else
            {
                MessageBox.Show("Lưu thất bại. Dữ liệu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}