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
using System.Configuration;

namespace TOURZY___Tourism_Management_System
{
    public partial class fForgot : Form
    {
        private AccountBLL accountBLL = new AccountBLL();
        public fForgot()
        {
            InitializeComponent();
            pnOTP.Enabled = false;
        }

        private void lbReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            fLogin lg = new fLogin();
            lg.ShowDialog();
            this.Show();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin, nhập đủ giúp TOURZY nhoo~");
                return;
            }

            if (!accountBLL.KiemTra(username, email))
            {
                MessageBox.Show("Tên đăng nhập hoặc Email không chính xác, thử lại nhé!");
                return;
            }

            string otp = accountBLL.GenerateOTP();
            accountBLL.UpdateOTP(username, otp);

            // Gửi OTP qua email
            SendOTPEmail(email, otp);

            MessageBox.Show("Đã gửi OTP tới email! Vui lòng kiểm tra email của bạn.");
            pnOTP.Enabled = true;
            txtUser.Enabled = false;
            txtEmail.Enabled = false;
            btnXacNhan.Enabled = false;
        }

        private void SendOTPEmail(string toEmail, string otp)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["MailAddress"];
                string password = ConfigurationManager.AppSettings["MailPassword"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);

                SmtpClient client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail, "TOURZY");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = "Mã OTP Xác Nhận Từ TOURZY";
                mailMessage.Body = $"<b>Mã OTP của bạn là:</b> <span style='font-size:20px;color:#007bff'>{otp}</span>. <br/>Mã này sẽ hết hạn sau <b>5 phút</b>.";
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi OTP: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string otpInput = txtOTP.Text.Trim();

            if (string.IsNullOrEmpty(otpInput))
            {
                MessageBox.Show("Bạn chưa nhập OTP!");
                return;
            }

            // Kiểm tra OTP
            if (accountBLL.XacNhanOTP(txtUser.Text.Trim(), otpInput))
            {
                string password = accountBLL.LayMatKhau(txtUser.Text.Trim()); // Lấy mật khẩu
                MessageBox.Show("Mật khẩu của bạn là: " + password);
            }
            else
            {
                MessageBox.Show("OTP không đúng hoặc đã hết hạn.");
            }
        }

        private void lbReturn_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form hiện tại
            fLogin loginForm = new fLogin(); // Tạo form đăng nhập mới
            loginForm.ShowDialog(); // Mở form đăng nhập dưới dạng modal
            this.Close();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnXacNhan.PerformClick();
            }
        }

        private void txtOTP_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
