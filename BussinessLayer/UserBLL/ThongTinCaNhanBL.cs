using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BussinessLayer
{
    public class ThongTinCaNhanBL
    {
        private readonly ThongTinCaNhanDL thongTinCaNhanDL = new ThongTinCaNhanDL();

        public bool SaveThongTinCaNhan(int maTaiKhoan, string ten, string sdt, string email, string diaChi, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(sdt))
                {
                    errorMessage = "Họ tên và Số điện thoại không được để trống!";
                    return false;
                }

                if (!long.TryParse(sdt, out _) || sdt.Length != 10)
                {
                    errorMessage = "Số điện thoại phải là 10 số!";
                    return false;
                }

                if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
                {
                    errorMessage = "Email không đúng định dạng!";
                    return false;
                }
                thongTinCaNhanDL.SaveThongTinCaNhan(maTaiKhoan, ten, sdt, email, diaChi);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi: {ex.Message}";
                return false;
            }
        }

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
    }
}
