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
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;
using System.Data.SqlClient;

namespace TOURZY___Tourism_Management_System
{
    public partial class CustomerManage : UserControl
    {
        private InfoBLL infoBLL = new InfoBLL();
        private AccountDTO account = new AccountDTO();


        private enum State
        {
            Add,
            Edit,
            None
        }

        private State currentState = State.None;

        public CustomerManage()
        {
            InitializeComponent();
            ChangeState();
            account.TenDangNhap = "";
            account.MatKhau = "";
        }

        private void CustomerManage_Load(object sender, EventArgs e)
        {
            loadAccounts();
        }

        public void loadAccounts()
        {
            List<AccountDTO> accounts = infoBLL.GetAllAccounts();
            dgv_dataacc.DataSource = accounts;
            dgv_dataacc.AllowUserToResizeColumns = true;
            dgv_dataacc.AllowUserToOrderColumns = true;
            dgv_dataacc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_dataacc.Columns["MatKhau"].Visible = false;
            dgv_dataacc.Columns["VaiTro"].Visible = false;
            dgv_dataacc.Columns["IsDeleted"].Visible = false;

            dgv_dataacc.Columns["ID"].HeaderText = "Mã Tài Khoản";
            dgv_dataacc.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
            dgv_dataacc.Columns["ID"].Width = 100;
            dgv_dataacc.Columns["TenDangNhap"].Width = 140;
        }

        private void dgv_dataacc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgv_dataacc.Rows[e.RowIndex];
                AccountDTO selectedAccount = (AccountDTO)selectedRow.DataBoundItem;

                account = selectedAccount; // Gán vào biến global
                ShowAccountDetail(selectedAccount);
                LoadJoinTourList(selectedAccount.ID);
            }
        }

        private void ShowAccountDetail(AccountDTO account)
        {
            InfoDTO info = infoBLL.GetInfoByAccountID(account.ID);
            if (info != null)
            {
                lb_value_ten.Text = info.Ten;
                lb_value_sdt.Text = info.SDT;
                lb_value_email.Text = info.Email;
                lb_value_diachi.Text = info.DiaChi;

                if (currentState == State.Edit)
                {
                    tb_modify_tendn.Text = account.TenDangNhap;
                    tb_modify_mk.Text = account.MatKhau;
                    tb_modify_role.Text = account.VaiTro;

                    tb_ten.Text = info.Ten;
                    tb_sdt.Text = info.SDT;
                    tb_email.Text = info.Email;
                    tb_diachi.Text = info.DiaChi;
                }
            }
            else
            {
                lb_value_diachi.Text = lb_value_email.Text = lb_value_sdt.Text = lb_value_ten.Text = "Chưa có thông tin!";
            }
        }

        private void LoadJoinTourList(int matk)
        {
            List<DanhSachDangKy> tours = infoBLL.danhSachDangKies(matk);
            dgv_dangky.DataSource = tours;

             dgv_dangky.Columns["MaTaiKhoan"].HeaderText = "Mã tài khoản";
             dgv_dangky.Columns["MaChuyenDi"].HeaderText = "Mã chuyến đi";
             dgv_dangky.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
             dgv_dangky.Columns["SoLuong"].HeaderText = "Số lượng";
             dgv_dangky.Columns["TrangThai"].HeaderText = "Trạng thái";

             dgv_dangky.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ResetPnlAccount()
        {   
            tb_modify_tendn.Text = "";
            tb_modify_mk.Text = "";
            tb_modify_role.Text = "";
        }

        private void ResetPnlInfo()
        {
            tb_ten.ResetText();
            tb_sdt.ResetText();
            tb_email.ResetText();
            tb_diachi.ResetText();
        }

        private void ChangeState()
        {
            ResetPnlAccount();
            ResetPnlInfo();
            if (currentState == State.None || currentState == State.Add)
            {
                ResetPnlAccount();
                ResetPnlInfo();
                pnl_change_account.Enabled = currentState == State.None ? false : true;
                pnl_change_info.Enabled = currentState == State.None ? false : true;
            }
            else
            {
                pnl_change_account.Enabled = false;
                pnl_change_info.Enabled = true;
            }
        }

        private void btn_accthem_Click(object sender, EventArgs e)
        {
            currentState = State.Add;
            ChangeState();
        }

        private void btn_accsua_Click(object sender, EventArgs e)
        {
            currentState = State.Edit;
            ChangeState();
        }

        private void btn_accxoa_Click(object sender, EventArgs e)
        {
            if(account.TenDangNhap != "")
            {
                DialogResult result;
                result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    account.IsDeleted = true;
                    infoBLL.DeleteAccount(account.ID);
                    loadAccounts();
                    ResetPnlAccount();
                    ResetPnlInfo();
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Hãy chọn tài khoản bạn muốn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }    
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            currentState = State.None;
            ChangeState();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {

            if (currentState == State.Add)
            {
                string tendn = tb_modify_tendn.Text.Trim();
                string matkhau = tb_modify_mk.Text.Trim();
                string vaitro = tb_modify_role.Text.Trim();

                string ten = tb_ten.Text.Trim();
                string sdt = tb_sdt.Text.Trim();
                string email = tb_email.Text.Trim();
                string diachi = tb_diachi.Text.Trim();

                if (infoBLL.IsUsernameExists(tendn))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo tài khoản mới
                AccountDTO newAccount = new AccountDTO
                {
                    TenDangNhap = tendn,
                    MatKhau = matkhau,
                    VaiTro = string.IsNullOrEmpty(vaitro) ? "user" : vaitro
                };

                int newID = infoBLL.AddAccount(newAccount); // Trả về ID vừa thêm

                if (newID > 0)
                {
                    InfoDTO newInfo = new InfoDTO
                    {
                        MaTaiKhoan = newID,
                        Ten = ten,
                        SDT = sdt,
                        Email = email,
                        DiaChi = diachi
                    };

                    infoBLL.AddInfo(newInfo);

                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadAccounts();
                    currentState = State.None;
                    ChangeState();
                }
            }
            else if (currentState == State.Edit)
            {
                InfoDTO updatedInfo = new InfoDTO
                {
                    MaTaiKhoan = account.ID,
                    Ten = tb_ten.Text.Trim(),
                    SDT = tb_sdt.Text.Trim(),
                    Email = tb_email.Text.Trim(),
                    DiaChi = tb_diachi.Text.Trim()
                };

                infoBLL.UpdateInfo(updatedInfo);

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadAccounts();
                currentState = State.None;
                ChangeState();
            }
        }
    }
}
