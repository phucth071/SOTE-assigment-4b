using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Buoi07_TinhToan3
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		bool alreadyFocused = false; // Flag to track if the textbox is already focused

		private void Form1_Load(object sender, EventArgs e)
		{
			txtSo1.Text = txtSo2.Text = "0";
			radCong.Checked = true;

			txtSo1.Leave += new EventHandler(TextBox_Leave);
			txtSo2.Leave += new EventHandler(TextBox_Leave);

			txtSo1.GotFocus += new EventHandler(TextBox_GotFocus);
			txtSo1.MouseUp += new MouseEventHandler(TextBox_MouseUp);

			txtSo2.GotFocus += new EventHandler(TextBox_GotFocus);
			txtSo2.MouseUp += new MouseEventHandler(TextBox_MouseUp);
		}

		// Validation for invalid number input
		private void TextBox_Leave(object sender, EventArgs e)
		{
			TextBox txt = sender as TextBox;

			// Check if the text is a valid double
			if (!double.TryParse(txt.Text, out _))
			{
				// Show error message
				MessageBox.Show("Dữ liệu nhập vào không hợp lệ.", "Error");

				// Focus on the invalid textbox and select all text for easy correction
				txt.Focus();
				txt.SelectAll();
			}

			// Reset the focus flag since we are leaving the textbox
			alreadyFocused = false;
		}

		// Handle GotFocus event to select all text if the mouse is not down (e.g., tabbing to the textbox)
		private void TextBox_GotFocus(object sender, EventArgs e)
		{
			if (MouseButtons == MouseButtons.None)
			{
				TextBox txt = sender as TextBox;
				txt.SelectAll(); // Select all text when gaining focus
				alreadyFocused = true; // Set focus flag
			}
		}

		// Handle MouseUp event to select all text if the textbox wasn't already focused and no text is selected
		private void TextBox_MouseUp(object sender, MouseEventArgs e)
		{
			TextBox txt = sender as TextBox;

			// Select all text if not already focused and no text has been selected
			if (!alreadyFocused && txt.SelectionLength == 0)
			{
				alreadyFocused = true; // Set the flag
				txt.SelectAll(); // Select all text
			}
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
			//lấy giá trị của 2 ô số
			double so1, so2, kq = 0;

			// Kiểm tra nếu số nhập vào vượt quá giới hạn của kiểu double
			if (!double.TryParse(txtSo1.Text, out so1))
			{
				MessageBox.Show("Số thứ nhất không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtSo1.Focus();
				return;
			}
			if (!double.TryParse(txtSo2.Text, out so2))
			{
				MessageBox.Show("Số thứ nhất hai hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtSo2.Focus();
				return;
			}

			//Thực hiện phép tính dựa vào phép toán được chọn
			if (radCong.Checked) kq = so1 + so2;
			else if (radTru.Checked) kq = so1 - so2;
			else if (radNhan.Checked)
			{
				kq = so1 * so2;
                decimalNumber num1, num2, res = null;
				if (TryParseVerySmallNumber(txtSo1.Text, out num1) && TryParseVerySmallNumber(txtSo2.Text, out num2))
				{;
					res = num1 * num2;
				}
				if (res != null)
				{
					txtKq.Text = res.ToString();
					return;
				}
			}
			else if (radChia.Checked)
			{
				if (so2 == 0)
				{
					MessageBox.Show("Số chia phải khác 0");
					txtSo2.Focus();
					return;
				}
				kq = so1 / so2;
			}
				// Hiển thị kết quả
				if (double.IsInfinity(kq))
				{
					MessageBox.Show("Kết quả vượt quá giới hạn tính toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtKq.Text = "∞";
				}
				else if (Math.Abs(kq) < 1e-308)
				{
					txtKq.Text = kq.ToString("0.0E0");
				}
				else
				{
					txtKq.Text = kq.ToString();
				}

			}

			private bool TryParseVerySmallNumber(string input, out decimalNumber result)
			{
				result = null;
				if (input.Contains("e"))
				{
					var parts = input.Split('e');
					if (parts.Length == 2 && int.TryParse(parts[0], out int mantissa) && int.TryParse(parts[1], out int exponent))
					{
						result = new decimalNumber(mantissa, exponent);
						return true;
					}
				}
				return false;
			}

		}
	}
