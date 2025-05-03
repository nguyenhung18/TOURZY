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
using BussinessLayer;

namespace TOURZY___Tourism_Management_System
{
    public partial class GuideManage : UserControl
    {
        private GuiderBLL bll = new GuiderBLL();
        private LichTrinhBLL lt = new LichTrinhBLL();
        private enum State
        {
            None,
            Add,
            Edit
        }
        private State currentState;
        public GuideManage()
        {
            InitializeComponent();
        }

        private void ResetTextbox()
        {
            tb_ID.Enabled = true;
            tb_ID.ResetText();
            tb_email.ResetText();
            tb_ten.ResetText();
            tb_sdt.ResetText();
        }
        private void ChangeEnableButton(bool enable)
        {
            btnThem.Enabled = enable;
            btnThaydoi.Enabled = enable;
            btnXoa.Enabled = enable;
        }
        private void ChangeState_pnlInfo()
        {
            if (currentState == State.Add || currentState == State.Edit)
            {
                pnl_changeinfo.Enabled = true;
                ChangeEnableButton(false);
            }
            else { pnl_changeinfo.Enabled = false; ChangeEnableButton(true); }
            if (currentState == State.Add)
                ResetTextbox();
        }
        private void GuideManage_Load(object sender, EventArgs e)
        {
            LoadGuide();
            cbb_ID.DataSource = bll.LoadGuiderIDs();
            cbb_IdGuide.DataSource= bll.LoadGuiderIDs();
        }

        private void LoadGuide()
        {
            dgv_hdv.AllowUserToResizeColumns = true;
            dgv_hdv.AllowUserToOrderColumns = true;
            dgv_hdv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            List<GuiderDTO> guide = bll.LoadGuides();
            dgv_hdv.DataSource = guide;

            dgv_Idtour.AllowUserToResizeColumns = true;
            dgv_Idtour.AllowUserToOrderColumns = true;
            dgv_Idtour.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            List<LichTrinhDTO> lts = lt.LoadLichTrinh();
            dgv_Idtour.DataSource = lts;
            dgv_Idtour.Columns["SoLuongNow"].Visible = false;
            dgv_Idtour.Columns["SoLuongMax"].Visible = false;
        }

        private void btn_Xem_Click(object sender, EventArgs e)
        {
            monthCalendar_hdv.RemoveAllBoldedDates();
            Highlight();
            monthCalendar_hdv.UpdateBoldedDates();
        }

        private void Highlight()
        {
            string maHDV = cbb_ID.Text.Trim();
            List<LichTrinhDTO> lichTrinhs = bll.LayLichTrinhTheoHDV(maHDV);

            rtbLT.Clear();
            monthCalendar_hdv.RemoveAllBoldedDates();
            foreach (var lt in lichTrinhs)
            {
                monthCalendar_hdv.AddBoldedDate(lt.NgayBatDau.Date);
                rtbLT.AppendText($"📅 {lt.NgayBatDau:dd/MM/yyyy}\nChuyến: {lt.MaChuyenDi}\n\n");
            }

            monthCalendar_hdv.UpdateBoldedDates();
        }

        private void dgv_hdv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgv_hdv.Rows[e.RowIndex].Cells["MaHDV"].Value != null)
            {
                var row = dgv_hdv.Rows[e.RowIndex];

                tb_ID.Text = row.Cells["MaHDV"].Value?.ToString();
                tb_ten.Text = row.Cells["Ten"].Value?.ToString();
                tb_sdt.Text = row.Cells["SDT"].Value?.ToString();
                tb_email.Text = row.Cells["Email"].Value?.ToString();
            }
            else
            {
                ResetTextbox();
            }

            ChangeState_pnlInfo();
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {

            currentState = State.None;
            ChangeState_pnlInfo();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            currentState = State.Add;
            ChangeState_pnlInfo();
            string newMa = bll.TaoMaHDV();
            tb_ID.Text = newMa;
            tb_ID.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maHDV = tb_ID.Text.Trim();
            if (!string.IsNullOrEmpty(maHDV))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xoá hướng dẫn viên này?",
                                                       "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bll.XoaHuongDanVien(maHDV);
                    MessageBox.Show("Đã xoá hướng dẫn viên.");
                    LoadGuide();
                }
            }
        }

        private void btnThaydoi_Click(object sender, EventArgs e)
        {
            currentState = State.Edit;
            ChangeState_pnlInfo();
            tb_ID.Enabled = false;
        }

        private void btn_luutt_Click(object sender, EventArgs e)
        {
            GuiderDTO hdv = new GuiderDTO
            {
                MaHDV = tb_ID.Text.Trim(),
                Ten = tb_ten.Text.Trim(),
                SDT = tb_sdt.Text.Trim(),
                Email = tb_email.Text.Trim()
            };

            if (currentState == State.Add)
            {
                hdv.MaHDV = bll.TaoMaHDV();
                bll.ThemHuongDanVien(hdv);
                MessageBox.Show("Thêm hướng dẫn viên thành công!");
            }
            else if (currentState == State.Edit)
            {
                bll.UpdateHDV(hdv);
                MessageBox.Show("Cập nhật hướng dẫn viên thành công!");
            }

            currentState = State.None;
            ChangeState_pnlInfo();
            LoadGuide();
        }

        private void btn_phancong_Click(object sender, EventArgs e)
        {
            int n = this.dgv_Idtour.CurrentCell.RowIndex;
            string IDTour = dgv_Idtour.Rows[n].Cells[0].Value.ToString();
            DateTime StartDay = DateTime.Parse(dgv_Idtour.Rows[n].Cells[1].Value.ToString());
            string IDGuide = cbb_IdGuide.Text;
            try
            {
                lt.Update_LichTrinh(IDTour, StartDay, IDGuide);
                MessageBox.Show("Phân công thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGuide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnhuypc_Click(object sender, EventArgs e)
        {
            int n = this.dgv_Idtour.CurrentCell.RowIndex;
            string IDTour = dgv_Idtour.Rows[n].Cells[0].Value.ToString();
            DateTime StartDay = DateTime.Parse(dgv_Idtour.Rows[n].Cells[1].Value.ToString());
            try
            {
                lt.DeleteHDV_LT(IDTour, StartDay);
                MessageBox.Show("Hủy phân công thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGuide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
