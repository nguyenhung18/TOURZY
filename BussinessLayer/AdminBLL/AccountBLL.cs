using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class AccountBLL
    {
        private AccountDAL taiKhoanDAL = new AccountDAL();

        public int LayMaTaiKhoan(string username)
        {
            return taiKhoanDAL.GetMaTaiKhoanByUsername(username);
        }

        public AccountDTO DangNhap(string tenDangNhap, string matKhau)
        {
            return taiKhoanDAL.DangNhap(tenDangNhap, matKhau);
        }
        public bool KiemTraTaiKhoanTonTai(string username)
        {
            return taiKhoanDAL.KiemTraTaiKhoan(username);
        }

        public bool DangKy(AccountDTO newAcc)
        {
            return taiKhoanDAL.DangKy(newAcc);
        }

        public bool KiemTra(string username, string email)
        {
            return taiKhoanDAL.KiemTra(username, email);
        }

        public void UpdateOTP(string username, string otp)
        {
            taiKhoanDAL.UpdateOTP(username, otp);
        }

        public bool XacNhanOTP(string username, string otpInput)
        {
            return taiKhoanDAL.XacNhanOTP(username, otpInput);
        }

        public string LayMatKhau(string username)
        {
            return taiKhoanDAL.LayMatKhau(username);
        }

        public string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }
    }
}
