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
    public partial class TourManage : UserControl
    {
        private TourBLL tourBLL = new TourBLL();
        private string path;
        private readonly string baseDirectory = @"D:\TOURZY";
        private bool adding = false;
        private bool editing = false;
        
        public static class ImageNames
        {
            public const string AnhBia = "AnhBia.jpg";
            public const string AnhPhu1 = "AnhPhu1.jpg";
            public const string AnhPhu2 = "AnhPhu2.jpg";
            public const string AnhPhu3 = "AnhPhu3.jpg";
        }

        public TourManage()
        {
            InitializeComponent();
            pnDes.Visible = false;
        }

        private void TourManage_Load(object sender, EventArgs e)
        {
            loadTour();
        }

        private void ResetForm()
        {
            txtMaTour.Clear();
            txtTenTour.Clear();
            txtType.Clear();
            txtAdven.Clear();
            txtDayNum.Clear();
            txtPrice.Clear();
            txtNum.Clear();
            richChiTiet.Clear();
            richMota.Clear();
            lbID.Text = string.Empty;
            lblName.Text = string.Empty;
            lbType.Text = string.Empty;
            lbDays.Text = string.Empty;
            lbPrice.Text = string.Empty;
            lbChiTiet.Text = string.Empty;
            txtMota.Clear();
            pbBia.Image = null;
            pbImage1.Image = null;
            pbImage2.Image = null;
            pbImage3.Image = null;
            path = null;
        }

        private void loadTour()
        {
            try
            {
                var tours = tourBLL.GetAllTours();
                dataTourManage.DataSource = tours;

                string pathTourImages = Path.Combine(baseDirectory, "TourImages");
                if (!Directory.Exists(pathTourImages))
                {
                    Directory.CreateDirectory(pathTourImages);
                }

                foreach (var tour in tours)
                {
                    string tourFolder = Path.Combine(pathTourImages, tour.MaChuyenDi);

                    if (!Directory.Exists(tourFolder))
                    {
                        Directory.CreateDirectory(tourFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu hoặc tạo thư mục: " + ex.Message);
            }
        }


        private void dataTourManage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = this.dataTourManage.CurrentCell.RowIndex;
            var selectedTour = dataTourManage.Rows[n];

            this.lbID.Text = selectedTour.Cells[0].Value.ToString();       
            this.lblName.Text = selectedTour.Cells[1].Value.ToString();   
            this.lbType.Text = selectedTour.Cells[2].Value.ToString();    
            this.lbDays.Text = selectedTour.Cells[4].Value.ToString();      
            this.lbPrice.Text = selectedTour.Cells[5].Value.ToString();   
            this.lbChiTiet.Text = selectedTour.Cells[7].Value.ToString();   
            this.txtMota.Text = selectedTour.Cells[8].Value.ToString();   

            this.txtMaTour.Text = selectedTour.Cells[0].Value.ToString();
            this.txtTenTour.Text = selectedTour.Cells[1].Value.ToString();
            this.txtType.Text = selectedTour.Cells[2].Value.ToString();
            this.txtAdven.Text = selectedTour.Cells[3].Value.ToString();    
            this.txtNum.Text = selectedTour.Cells[6].Value.ToString();      
            this.txtDayNum.Text = selectedTour.Cells[4].Value.ToString();
            this.txtPrice.Text = selectedTour.Cells[5].Value.ToString();

            string pathTourImages = Path.Combine(baseDirectory, "TourImages");
            path = Path.Combine(pathTourImages, this.lbID.Text);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Load images for the tour
            LoadImage(pbBia, Path.Combine(path, "AnhBia.jpg"));
            LoadImage(pbImage1, Path.Combine(path, "AnhPhu1.jpg"));
            LoadImage(pbImage2, Path.Combine(path, "AnhPhu2.jpg"));
            LoadImage(pbImage3, Path.Combine(path, "AnhPhu3.jpg"));
        }

        private void LoadImage(PictureBox pb, string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    pb.Image = Image.FromStream(stream);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                pb.Image = null;
            }
        }

        private void UploadImage(PictureBox pictureBox, string imageName, string title)
        {
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Vui lòng chọn một tour trước khi thêm ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OpenFileDialog source = new OpenFileDialog())
            {
                source.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                source.Title = title;

                if (source.ShowDialog() == DialogResult.OK)
                {
                    string filePath = Path.Combine(path, imageName);
                    try
                    {
                        File.Copy(source.FileName, filePath, true);
                        LoadImage(pictureBox, filePath);
                        MessageBox.Show($"Thêm {imageName} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Lỗi khi lưu ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBia_Click(object sender, EventArgs e)
        {
            UploadImage(pbBia, ImageNames.AnhBia, "Chọn ảnh bìa cho Tour");
        }

        private void btnImage1_Click(object sender, EventArgs e)
        {
                UploadImage(pbImage1, ImageNames.AnhPhu1, "Sarah chọn ảnh phụ 1 cho Tour");
        }

        private void btnIamge2_Click(object sender, EventArgs e)
        {
            UploadImage(pbImage2, ImageNames.AnhPhu2, "Chọn ảnh phụ 2 cho Tour");
        }

        private void btnImage3_Click(object sender, EventArgs e)
        {
            UploadImage(pbImage3, ImageNames.AnhPhu3, "Chọn ảnh phụ 3 cho Tour");
        }

        private void btnAdj_Click(object sender, EventArgs e)
        {
            try
            {
                editing = true;
                adding = false;

                // Lấy dữ liệu từ input
                string maTour = this.lbID.Text;
                string tenTour = this.txtTenTour.Text;
                string hinhThuc = this.txtType.Text;
                string hanhTrinh = this.txtAdven.Text;
                string chiTiet = string.IsNullOrWhiteSpace(this.richChiTiet.Text)? this.lbChiTiet.Text: this.richChiTiet.Text;

                string moTa = string.IsNullOrWhiteSpace(this.richMota.Text)? this.txtMota.Text: this.richMota.Text;


                if (string.IsNullOrWhiteSpace(tenTour) || string.IsNullOrWhiteSpace(hinhThuc) ||
                    string.IsNullOrWhiteSpace(this.txtDayNum.Text) || string.IsNullOrWhiteSpace(this.txtPrice.Text) ||
                    string.IsNullOrWhiteSpace(this.txtNum.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int soNgayDi = int.Parse(this.txtDayNum.Text.Trim());
                int gia = int.Parse(this.txtPrice.Text.Trim());
                int soLuong = int.Parse(this.txtNum.Text.Trim());

                TourDTO tourDTO = new TourDTO()
                {
                    MaChuyenDi = maTour,
                    TenChuyenDi = tenTour,
                    HinhThuc = hinhThuc,
                    HanhTrinh = hanhTrinh,
                    SoNgayDi = soNgayDi,
                    Gia = gia,
                    SoLuong = soLuong,
                    ChiTiet = string.IsNullOrWhiteSpace(chiTiet) ? "Chưa cập nhật" : chiTiet,
                    MoTa = string.IsNullOrWhiteSpace(moTa) ? "Chưa cập nhật" : moTa
                };

                bool result = tourBLL.UpdateTour(tourDTO);

                if (result)
                {
                    MessageBox.Show("Cập nhật tour thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadTour();
                    ResetForm();
                    pnDes.Visible = false;
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số (Số ngày, Giá, Số lượng).", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã chuyến đi từ label
                string maTour = this.lbID.Text;

                if (string.IsNullOrWhiteSpace(maTour))
                {
                    MessageBox.Show("Không có tour nào được chọn để xoá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xoá tour này không?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                // Gọi BLL để xoá
                bool result = tourBLL.DeleteTour(new TourDTO { MaChuyenDi = maTour });

                if (result)
                {
                    string pathTourImages = Path.Combine(baseDirectory, "TourImages");
                    string tourPath = Path.Combine(pathTourImages, maTour);

                    // Kiểm tra xem thư mục có tồn tại không, nếu có thì xóa
                    if (Directory.Exists(tourPath))
                    {
                        Directory.Delete(tourPath, true);
                        ResetForm();
                        loadTour();
                        MessageBox.Show("Thư mục và các ảnh liên quan đã được xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    pnDes.Visible = false;
                }
                else
                {
                    MessageBox.Show("Xoá thất bại! Có thể tour đang được sử dụng.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            adding = true;
            editing = false;
            try
            {
                string maTour = this.txtMaTour.Text.Trim();
                string tenTour = this.txtTenTour.Text.Trim();
                string hinhThuc = this.txtType.Text.Trim();
                string hanhTrinh = this.txtAdven.Text.Trim();
                string moTa = this.richMota.Text.Trim();
                string chiTiet = this.richChiTiet.Text.Trim();

                if (string.IsNullOrWhiteSpace(maTour) || string.IsNullOrWhiteSpace(tenTour) ||
                    string.IsNullOrWhiteSpace(hinhThuc) || string.IsNullOrWhiteSpace(this.txtDayNum.Text) ||
                    string.IsNullOrWhiteSpace(this.txtPrice.Text) || string.IsNullOrWhiteSpace(this.txtNum.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int soNgayDi = int.Parse(this.txtDayNum.Text);
                int gia = int.Parse(this.txtPrice.Text);
                int soLuong = int.Parse(this.txtNum.Text);

                TourDTO newTour = new TourDTO()
                {
                    MaChuyenDi = maTour,
                    TenChuyenDi = tenTour,
                    HinhThuc = hinhThuc,
                    HanhTrinh = hanhTrinh,
                    SoNgayDi = soNgayDi,
                    Gia = gia,
                    SoLuong = soLuong,
                    MoTa = string.IsNullOrWhiteSpace(moTa) ? "Chưa cập nhật" : moTa,
                    ChiTiet = string.IsNullOrWhiteSpace(chiTiet) ? "Chưa cập nhật" : chiTiet
                };

                bool result = tourBLL.AddTour(newTour);

                if (result)
                {
                    // Tạo thư mục hình ảnh
                    string baseDirectory = @"D:\TOURZY";
                    string pathTourImages = Path.Combine(baseDirectory, "TourImages");
                    path = Path.Combine(pathTourImages, maTour);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    MessageBox.Show("Thêm tour mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadTour();
                    ResetForm();
                    pnDes.Visible = false;
                }
                else
                {
                    MessageBox.Show("Thêm tour thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDescrip_Click(object sender, EventArgs e)
        {

            pnDes.Visible = !pnDes.Visible;
            if (pnDes.Visible && editing)
            {
                Control txtChiTiet = pnDes.Controls["richChiTiet"];
                Control txtMoTaMini = pnDes.Controls["richMota"];
                if (txtChiTiet != null) txtChiTiet.Text = this.lbChiTiet.Text;
                if (txtMoTaMini != null) txtMoTaMini.Text = this.txtMota.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}

