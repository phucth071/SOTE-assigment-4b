using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics; // Thêm namespace này

namespace Buoi07_TinhToan3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSo1.Text = txtSo2.Text = "0";
            radCong.Checked = true;             //đầu tiên chọn phép cộng
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            double so1, so2, kq = 0;

            // Kiểm tra nếu số nhập vào vượt quá giới hạn của kiểu double
            if (!double.TryParse(txtSo1.Text, out so1))
            {
                MessageBox.Show("Nhập vào số quá lớn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo1.Focus();
                return;
            }
            if (!double.TryParse(txtSo2.Text, out so2))
            {
                MessageBox.Show("Nhập vào số quá lớn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo2.Focus();
                return;
            }


            // Thực hiện phép tính dựa vào phép toán được chọn
            if (radCong.Checked) kq = so1 + so2;
            else if (radTru.Checked) kq = so1 - so2;
            else if (radNhan.Checked) kq = so1 * so2;
            else if (radChia.Checked && so2 != 0) kq = so1 / so2;

            // Hiển thị kết quả
            if (double.IsInfinity(kq))
            {
                MessageBox.Show("Kết quả vượt quá giới hạn tính toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKq.Text = "∞";
            }
            else
            {
                txtKq.Text = kq.ToString();
            }
        }

    }
}
