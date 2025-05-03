using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;

namespace BussinessLayer
{
    public class InfoBLL
    {
        private InfoDAL inf = new InfoDAL();

        public bool AddUserInfo(InfoDTO info)
        {
            return inf.AddUserInfo(info);
        }

        public List<AccountDTO> GetAllAccounts()
        {
            return inf.GetAllAccounts();
        }

        public InfoDTO GetInfoByAccountID(int accountId)
        {
            return inf.GetInfoByAccountID(accountId);
        }

        public List<DanhSachDangKy> danhSachDangKies(int matk)
        {
            return inf.GetJoinedToursByAccount(matk);
        }
        public void DeleteAccount(int accountId)
        {
            inf.DeleteAccount(accountId);
        }

        public bool IsUsernameExists(string username)
        {
            return inf.CheckUsername(username);
        }

        public int AddAccount(AccountDTO acc)
        {
            return inf.AddAccount(acc);
        }

        public void AddInfo(InfoDTO info)
        {
            inf.AddInfo(info);
        }

        public void UpdateInfo(InfoDTO info)
        {
            inf.UpdateInfo(info);
        }

    }
}
