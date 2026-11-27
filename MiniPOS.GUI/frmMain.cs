using MiniPOS.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPOS.GUI
{
    public partial class frmMain : Form
    {
        private Form _activeForm = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (SessionManager.CurrentUser == null)
            {
                this.Close();
                return;
            }

            // Hiển thị thông tin nhân viên đang trực ca
            lblUserInfo.Text = $"Xin chào: {SessionManager.CurrentUser.FullName}\nVai trò: {SessionManager.CurrentUser.Role}";

            // Logic phân quyền: Staff bị ẩn Quản lý Sản phẩm & Báo cáo Doanh thu
            if (SessionManager.CurrentUser.Role == "Staff")
            {
                btnProducts.Visible = false;
                btnReports.Visible = false;
            }

            // Mặc định mở sẵn màn hình Bán hàng (POS) khi vào hệ thống
            // OpenChildForm(new frmPOS()); // Bỏ comment dòng này khi đã tạo frmPOS ở Giai đoạn 3
        }

        // Hàm dùng chung để nhúng Form con vào pnlMain
        private void OpenChildForm(Form childForm)
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
            }

            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(childForm);
            pnlMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            // OpenChildForm(new frmPOS());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // OpenChildForm(new frmProductManagement()); // Sẽ gắn ở Bước 2.3
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            // OpenChildForm(new frmRevenueReport()); // Sẽ gắn ở Giai đoạn 4
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn đăng xuất khỏi hệ thống?",
                "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                SessionManager.Clear();
                this.Hide();
                frmLogin loginForm = new frmLogin();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }
    }
}
