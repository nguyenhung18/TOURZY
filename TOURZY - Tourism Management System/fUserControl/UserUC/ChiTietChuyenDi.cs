using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;

namespace TOURZY___Tourism_Management_System
{
    public partial class ChiTietChuyenDi : UserControl
    {
        private ChuyenDiBL bl = new ChuyenDiBL();
        public int UserId { get; set; }
        public string MaChuyenDi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public static class ImageNames
        {
            public const string AnhBia = "AnhBia.jpg";
            public const string AnhPhu1 = "AnhPhu1.jpg";
            public const string AnhPhu2 = "AnhPhu2.jpg";
            public const string AnhPhu3 = "AnhPhu3.jpg";
        }

        public ChiTietChuyenDi()
        {
            InitializeComponent();
           
           
        }

        public void LoadChiTietChuyenDi(
    string ten, string hanhTrinh, string gia, string soNguoi,
    string soSao, string loaiHinh, string moTa, string ngay, string soNgay)
        {
            lb_Ten.Text = ten;
            lb_HanhTrinh.Text = hanhTrinh;
            lb_GiaTrenNguoi.Text = gia;
            lb_NguoiThamGia.Text = soNguoi;
            lb_SoSao.Text = soSao;
            lb_LoaiHinh.Text = loaiHinh;
            rtb_MoTa.Text = moTa;
            lb_NgayKhoiHanh.Text = ngay;
            lb_SoNgayDi.Text = soNgay;

            this.MaChuyenDi = bl.GetMaChuyenDiByTen(ten);
            LoadAnh();

            this.Refresh();
        }

        private void LoadAnh()
        {
            string baseDirectory = @"D:\TOURZY";
            string pathTourImages = Path.Combine(baseDirectory, "TourImages");

            if (string.IsNullOrEmpty(MaChuyenDi)) return;

            string chuyenDiPath = Path.Combine(pathTourImages, MaChuyenDi);

            ptb_bia.Image = File.Exists(Path.Combine(chuyenDiPath, ImageNames.AnhBia))
                ? Image.FromFile(Path.Combine(chuyenDiPath, ImageNames.AnhBia))
                : null;

            ptb_1.Image = File.Exists(Path.Combine(chuyenDiPath, ImageNames.AnhPhu1))
                ? Image.FromFile(Path.Combine(chuyenDiPath, ImageNames.AnhPhu1))
                : null;

            ptb_2.Image = File.Exists(Path.Combine(chuyenDiPath, ImageNames.AnhPhu2))
                ? Image.FromFile(Path.Combine(chuyenDiPath, ImageNames.AnhPhu2))
                : null;

            ptb_3.Image = File.Exists(Path.Combine(chuyenDiPath, ImageNames.AnhPhu3))
                ? Image.FromFile(Path.Combine(chuyenDiPath, ImageNames.AnhPhu3))
                : null;
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

        private void btn_Dat_Click(object sender, EventArgs e)
        {
            User userForm = this.FindForm() as User;
            if (userForm != null)
            {
                FlowLayoutPanel flowPanel = userForm.Controls.Find("flowLayoutPanel1", true).FirstOrDefault() as FlowLayoutPanel;
                if (flowPanel != null)
                {
                    flowPanel.Controls.Clear();
                    DatChuyenDi ucDatChuyenDi = new DatChuyenDi();
                    ucDatChuyenDi.UserId = userForm.userId; // Truyền userId
                    ucDatChuyenDi.Dock = DockStyle.Top;
                    ucDatChuyenDi.LoadDatChuyenDi(
                        lb_Ten.Text, lb_HanhTrinh.Text, lb_GiaTrenNguoi.Text,
                        lb_NguoiThamGia.Text, lb_SoSao.Text, lb_LoaiHinh.Text,
                        lb_NgayKhoiHanh.Text, lb_SoNgayDi.Text
                    );
                    flowPanel.Controls.Add(ucDatChuyenDi);
                }
            }
        }

        private void btn_DanhGia_Click(object sender, EventArgs e)
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

                    // Tạo và thêm lại UserControl đích (DatChuyenDi)
                 ChuyenDi ucDatChuyenDi = new ChuyenDi();
                    ucDatChuyenDi.Dock = DockStyle.Top; // Hoặc Fill nếu bạn muốn

                    // Tạo và thêm lại UserControl đích (ví dụ: UserControl_TrangChu)
                    DanhGiaChuyenDi ucTrangChu = new DanhGiaChuyenDi();
                    ucTrangChu.Dock = DockStyle.Top; // Hoặc Fill nếu bạn muốn
                    flowPanel.Controls.Add(ucTrangChu);
                }
            }
        }

        private void btn_Tao_Click(object sender, EventArgs e)
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
                   TaoChuyenDiMoi ucTrangChu = new TaoChuyenDiMoi();
                    ucTrangChu.Dock = DockStyle.Top; // Hoặc Fill nếu bạn muốn
                    flowPanel.Controls.Add(ucTrangChu);
                }
            }
        }
    }
}
