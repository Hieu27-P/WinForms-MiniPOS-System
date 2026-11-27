using MiniPOS.BLL;
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
    public partial class frmLogin : Form
    {
        private readonly UserBLL _userBLL = new UserBLL();

        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin; // Nhấn Enter để đăng nhập nhanh
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserDTO user = _userBLL.Login(username, password);

            if (user != null)
            {
                SessionManager.CurrentUser = user;
                this.Hide();

                frmMain mainForm = new frmMain();
                mainForm.FormClosed += (s, args) => this.Close(); // Đóng app hoàn toàn khi tắt frmMain
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
