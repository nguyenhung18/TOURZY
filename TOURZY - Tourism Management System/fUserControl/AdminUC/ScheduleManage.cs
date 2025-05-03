using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using System.Configuration;

namespace TOURZY___Tourism_Management_System
{
    public partial class ScheduleManage : UserControl
    {
        private LichTrinhBLL bll = new LichTrinhBLL();
        private ReviewBLL review = new ReviewBLL();
        private GuiderBLL guider = new GuiderBLL();
        private TourBLL tour = new TourBLL();   
        public ScheduleManage()
        {
            InitializeComponent();
        }

        private void ScheduleManage_Load(object sender, EventArgs e)
        {
            gridSchedule.AllowUserToResizeColumns = true;
            gridSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSchedule.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            LoadSches(DateTime.Parse("1753-01-01"), DateTime.MaxValue);
            cbMa.DataSource = review.LoadTours();
            cbMa.DisplayMember = "MaChuyenDi";
            cbMaTour.DataSource = review.LoadTours();
            cbMaTour.DisplayMember = "MaChuyenDi";
            cbMaGuide.DataSource = guider.LoadGuides();
            cbMaGuide.DisplayMember = "MaHDV";
        }

        private void LoadSches(DateTime from, DateTime to)
        {
            var list = bll.LayLichTrinhThongKe(from, to);
            gridSchedule.DataSource = list;
        }

        private void btnFindCalen_Click(object sender, EventArgs e)
        {
            DateTime from = this.Calendar.SelectionRange.Start;
            DateTime to = this.Calendar.SelectionRange.End;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadSches(from, to);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadSches(DateTime.Parse("1753-01-01"), DateTime.MaxValue);
        }

        private RequestBLL reqbll = new RequestBLL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maChuyenDi = this.cbMa.Text;
            DateTime ngayBD = this.datetimeAdd.Value.Date;
            LichTrinhDTO itinerary = new LichTrinhDTO
            {
                MaChuyenDi = maChuyenDi,
                NgayBatDau = ngayBD
            };
            reqbll.AddItinerary(itinerary);
            LoadSches(DateTime.Parse("1753-01-01"), DateTime.MaxValue);
        }

        private void btnFindTour_Click(object sender, EventArgs e)
        {
            string maCD = cbMaTour.Text;
            string maGuid = cbMaGuide.Text;
            int quantity = Convert.ToInt32(this.maxParti.Value);
            bool notEligible = ckbEnough.Checked; 

            var list = bll.FilterData(maCD, maGuid, quantity, notEligible);
            gridSchedule.DataSource = list;
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            string maChuyenDi = lbID_Tour.Text;
            DateTime ngayBatDau = datetimeCancel.Value;

            List<string> emailList = bll.LayDanhSachEmailHanhKhach(maChuyenDi, ngayBatDau);

            SendCancellationEmail(emailList, maChuyenDi, ngayBatDau);

            bll.DeleteItinerary(maChuyenDi, ngayBatDau);
            MessageBox.Show("Chuyến đi đã được xóa thành công!");
            LoadSches(DateTime.Parse("1753-01-01"), DateTime.MaxValue);
        }

        private void gridSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gridSchedule.Rows[e.RowIndex];
                lbID_Tour.Text = row.Cells["MaChuyenDi"].Value.ToString();
                lb_Max.Text = row.Cells["SoLuongMax"].Value.ToString();
                lbName.Text = tour.GetNameTour(lbID_Tour.Text)?.TenChuyenDi;
                lbType_.Text = tour.GetNameTour(lbID_Tour.Text)?.HinhThuc;
                datetimeCancel.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value
                );
            }
        }

        public void SendCancellationEmail(List<string> emailList, string maChuyenDi, DateTime ngayBatDau)
        {
            string fromEmail = ConfigurationManager.AppSettings["MailAddress"];
            string displayName = ConfigurationManager.AppSettings["MailDisplayName"];
            string password = ConfigurationManager.AppSettings["MailPassword"];
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);

            foreach (string email in emailList)
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(fromEmail, displayName);
                        mail.To.Add(email);
                        mail.Subject = "TOURZY THÔNG BÁO HỦY TOUR";
                        mail.IsBodyHtml = true;

                        mail.Body = $@"
<p>Kính gửi Quý Khách,</p>

<p>
Chuyến đi mang mã <b>{maChuyenDi}</b>, dự kiến khởi hành vào ngày <b>{ngayBatDau:dd/MM/yyyy}</b>, đã bị hủy do những lý do không mong muốn.
</p>

<p>
Chúng tôi thành thật xin lỗi vì sự bất tiện này và xin cam kết:<br/>
- Hoàn tiền 100% cho các khoản thanh toán đã thực hiện.<br/>
- Đề xuất các lịch trình thay thế hấp dẫn trong thời gian sớm nhất.
</p>

<p>
Để được hỗ trợ thêm, Quý Khách vui lòng liên hệ qua:<br/>
📧 Email: tourzy.hotro@gmail.com
</p>

<p>
Cảm ơn Quý Khách đã luôn tin tưởng và đồng hành cùng <b>TOURZY</b>.
</p>

<p>
Trân trọng,<br/>
<b>Đội ngũ TOURZY</b>
</p>";

                        using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                        {
                            smtp.Credentials = new NetworkCredential(fromEmail, password);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gửi mail đến {email} thất bại: {ex.Message}", "Lỗi gửi mail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
