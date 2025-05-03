using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace TOURZY___Tourism_Management_System
{
    public partial class RevManage : UserControl
    {
       private ReviewBLL reviewBLL = new ReviewBLL();
        public RevManage()
        {
            InitializeComponent();
        }

        private void RevManage_Load(object sender, EventArgs e)
        {
            LoadReview();
            LoadTourComboBox();
            cbbMaTour.Text = "";
        }
        private void LoadReview()
        {
            List<ReviewDTO> reviews = reviewBLL.GetReviews();
            dgvQLDanhGia.DataSource = reviews;
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedMaTour = cbbMaTour.SelectedValue?.ToString();

                List<ReviewDTO> reviews = reviewBLL.GetReviewsByTour(selectedMaTour);
                Console.WriteLine($"Number of reviews retrieved: {reviews.Count}");

                dgvQLDanhGia.AutoGenerateColumns = true;
                dgvQLDanhGia.DataSource = reviews;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm đánh giá: " + ex.Message);
            }
        }

        private TourBLL tourBLL = new TourBLL();
        private void LoadTourComboBox()
        {

            List<TourDTO> listTour = tourBLL.GetAllTours();
            cbbMaTour.DataSource = listTour;
            cbbMaTour.DisplayMember = "MaChuyenDi";
            cbbMaTour.ValueMember = "MaChuyenDi";
        }
    }
}
