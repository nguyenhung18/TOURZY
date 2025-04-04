using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOURZY___Tourism_Management_System
{
    public partial class fSignin : Form
    {
        public fSignin()
        {
            InitializeComponent();
        }

        private void ckbShow_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShow.Checked)
            {
                txtPass.PasswordChar = false;
                txtConfirm.PasswordChar = false;
            }
            else
            {
                txtPass.PasswordChar = true;
                txtConfirm.PasswordChar = true;
            }
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
