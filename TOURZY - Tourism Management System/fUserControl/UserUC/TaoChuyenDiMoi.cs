using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class TaoChuyenDiMoi : UserControl
    {
        private TaoChuyenDiMoiBL yeuCauBLL = new TaoChuyenDiMoiBL();
       
        public TaoChuyenDiMoi()
        {
            InitializeComponent();
          
        }
       
        private void btn_GuiYeuCau_Click(object sender, EventArgs e)
        {
            
        }
        

      

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            // Tìm form cha là 'User'
            User userForm = this.FindForm() as User;
            if (userForm != null)
            {
                // Tìm flowLayoutPanel1 chứa các UserControl
                FlowLayoutPanel flowPanel = userForm.Controls.Find("flowLayoutPanel1", true).FirstOrDefault() as FlowLayoutPanel;
                if (flowPanel != null)
                {
                    flowPanel.Controls.Clear(); // Xóa UserControl hiện tại

                    // Tạo và thêm lại UserControl đích (ví dụ: UserControl_TrangChu)
                    ChuyenDi ucTrangChu = new ChuyenDi();
                    ucTrangChu.Dock = DockStyle.Top; // Hoặc Fill nếu bạn muốn
                    flowPanel.Controls.Add(ucTrangChu);
                }
            }
        }

        private void tb_SoLuongNguoi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
