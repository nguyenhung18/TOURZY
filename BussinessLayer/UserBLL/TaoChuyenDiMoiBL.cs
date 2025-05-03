using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
namespace BussinessLayer
{
    public class TaoChuyenDiMoiBL
    {
        private TaoChuyenDiMoiDL dal = new TaoChuyenDiMoiDL();

        public bool GuiYeuCau(TaoChuyenDiMoiDTO yc)
        {
            // Có thể thêm kiểm tra hợp lệ dữ liệu ở đây
            return dal.ThemYeuCau(yc);
        }
    }
}


