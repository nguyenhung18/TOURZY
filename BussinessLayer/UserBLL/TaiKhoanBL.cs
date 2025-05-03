using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
namespace BussinessLayer
{
public class TaiKhoanBL
    {
        private TaiKhoanDL taiKhoanDAL = new TaiKhoanDL();

        public TaiKhoanDTO Authenticate(string tenDangNhap, string matKhau)
        {
            return taiKhoanDAL.GetTaiKhoanByCredentials(tenDangNhap, matKhau);
        }

        public int GetUserId(string username)
        {
            return taiKhoanDAL.GetIDbyUsername(username);
        }
    }
}
