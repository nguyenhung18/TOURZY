using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using BussinessLayer;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class RequestManage : UserControl
    {
        private RequestBLL bll = new RequestBLL();
        public RequestManage()
        {
            InitializeComponent();
        }

        private void RequestManage_Load(object sender, EventArgs e)
        {
            LoadReq();
        }

        private void LoadReq()
        {
            dgv_yeucau.AllowUserToResizeColumns = true;
            dgv_yeucau.AllowUserToOrderColumns = true;
            dgv_yeucau.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<RequestDTO> rqs = bll.GetYeuCauList();
            dgv_yeucau.DataSource = rqs;

            List<AccountDTO> usernames = bll.GetUsernames();
            cbb_idacc.DataSource = usernames;
            cbb_idacc.DisplayMember = "TenDangNhap";
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string email = bll.GetEmail(bll.GetIDAccount(cbb_idacc.Text));

            // Kiểm tra email hợp lệ
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi");
                return;
            }

            // Kiểm tra tiêu đề và nội dung
            if (string.IsNullOrEmpty(tb_tieude.Text) || string.IsNullOrEmpty(tb_noidung.Text))
            {
                MessageBox.Show("Tiêu đề và nội dung không được để trống.", "Lỗi");
                return;
            }

            try
            {
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("vut462733@gmail.com", "rdjk ilny yyla byxy")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("vut462733@gmail.com"),
                    Subject = tb_tieude.Text,
                    Body = tb_noidung.Text
                };
                mailMessage.To.Add(new MailAddress(email));

                smtpClient.Send(mailMessage);
                MessageBox.Show("Gửi thành công.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi thất bại... " + ex.Message, "Thông báo");
            }
        }

        // Kiểm tra email hợp lệ
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            if (dgv_yeucau.CurrentRow != null)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgv_yeucau.CurrentRow;

                // Kiểm tra nếu dòng đã chọn có dữ liệu hợp lệ
                if (selectedRow.Cells["MaTaiKhoan"].Value != null &&
                    selectedRow.Cells["MaChuyenDi"].Value != null &&
                    selectedRow.Cells["NgayBatDau"].Value != null)
                {
                    int maTaiKhoan = Convert.ToInt32(selectedRow.Cells["MaTaiKhoan"].Value);
                    string maChuyenDi = selectedRow.Cells["MaChuyenDi"].Value.ToString();
                    DateTime ngayBatDau = Convert.ToDateTime(selectedRow.Cells["NgayBatDau"].Value);

                    try
                    {
                        // Gọi phương thức xóa từ BLL
                        bll.DeleteYeuCau(maTaiKhoan, maChuyenDi, ngayBatDau);
                        MessageBox.Show("Xóa yêu cầu thành công.", "Thông báo");

                        // Cập nhật lại DataGridView sau khi xóa
                        LoadReq();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa yêu cầu: " + ex.Message, "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Dòng chọn chưa đủ thông tin để xóa.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu để xóa.", "Thông báo");
            }
        }

        private void btn_nhan_Click(object sender, EventArgs e)
        {
            if (dgv_yeucau.CurrentRow != null)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgv_yeucau.CurrentRow;

                // Kiểm tra nếu dòng đã chọn có dữ liệu hợp lệ
                if (selectedRow.Cells["MaTaiKhoan"].Value != null &&
                    selectedRow.Cells["MaChuyenDi"].Value != null &&
                    selectedRow.Cells["NgayBatDau"].Value != null)
                {
                    int maTaiKhoan = Convert.ToInt32(selectedRow.Cells["MaTaiKhoan"].Value);
                    string maChuyenDi = selectedRow.Cells["MaChuyenDi"].Value.ToString();
                    DateTime ngayBatDau = Convert.ToDateTime(selectedRow.Cells["NgayBatDau"].Value);

                    // Kiểm tra MaChuyenDi có rỗng hoặc null không
                    if (string.IsNullOrWhiteSpace(maChuyenDi))
                    {
                        MessageBox.Show("Mã chuyến đi không được để trống.", "Thông báo");
                        return;
                    }

                    LichTrinhDTO itinerary = new LichTrinhDTO
                    {
                        MaChuyenDi = maChuyenDi,
                        NgayBatDau = ngayBatDau
                    };

                    try
                    {
                        // Kiểm tra MaChuyenDi có tồn tại trong bảng ChuyenDi
                        if (!bll.CheckChuyenDiExists(maChuyenDi))
                        {
                            MessageBox.Show("Mã chuyến đi không tồn tại ", "Thông báo");
                            return;
                        }

                        // Kiểm tra lịch trình có tồn tại không
                        if (bll.CheckLichTrinhExists(maChuyenDi, ngayBatDau))
                        {
                            MessageBox.Show("Lịch trình này đã tồn tại!", "Thông báo");
                            return;
                        }

                        // Thêm lịch trình và xóa yêu cầu
                        bll.AddItinerary(itinerary);
                        bll.DeleteYeuCau(maTaiKhoan, maChuyenDi, ngayBatDau);
                        MessageBox.Show("Đã thêm lịch trình mới vào hệ thống.", "Thông báo");
                        LoadReq();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xử lý yêu cầu: {ex.Message}", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Dòng chọn chưa đủ thông tin để chấp nhận.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu để xử lý.", "Thông báo");
            }
        }
    }
}
