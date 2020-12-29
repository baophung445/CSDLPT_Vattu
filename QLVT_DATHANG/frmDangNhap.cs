using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            string chuoiketnoi = "Data Source=DESKTOP-LAFLFDC;Initial Catalog=QLVT_DATHANG;Integrated Security=True";
            Program.conn.ConnectionString = chuoiketnoi;
            Program.conn.Open();
            DataTable dt = new DataTable();
            dt = Program.ExecSqlDataTable("SELECT * FROM V_DS_PHANMANH");
            Program.bds_dspm.DataSource = dt;
            cmbCN.DataSource = dt;
            cmbCN.DisplayMember = "TENCN";
            cmbCN.ValueMember = "TENSERVER";
            cmbCN.SelectedIndex = -1;
            cmbCN.SelectedIndex = 0;
            //txtMatKhau.UseSystemPasswordChar = true;
        }

        private void cmbCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCN.SelectedValue == null) return;
            try
            {
                Program.servername = cmbCN.SelectedValue.ToString();
            }
            catch (Exception) { };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Login name và mật mã không được trống", "", MessageBoxButtons.OK);
                return;
            }

            Program.mlogin = txtTaiKhoan.Text;
            Program.password = txtMatKhau.Text;

            if (Program.KetNoi() == 0) return;

            Program.mChinhanh = cmbCN.SelectedIndex;

            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;

            string strLenh = "EXEC SP_THONGTINDANGNHAP1 '" + Program.mlogin + "'";

            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null)
            {

                return;
            }
            Program.myReader.Read(); // đọc 1 dòng

            // sp_login trả về 3 cột
            //

            if (Program.myReader.GetString(0) == "0")
            {
                MessageBox.Show("Đăng nhập thất bại !", "", MessageBoxButtons.OK);
                return;
            }
            Program.username = Program.myReader.GetString(0);     // Lay user name, đọc cột đầu tiên
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username, password", "", MessageBoxButtons.OK);
                return;
            }
            Program.mHoten = Program.myReader.GetString(1);

            Program.mGroup = Program.myReader.GetString(2);
            Program.myReader.Close();
            Program.conn.Close();
            MessageBox.Show("Nhân viên - Nhóm : " + Program.mHoten + " - " + Program.mGroup, "", MessageBoxButtons.OK);
            Form1 f = new Form1();
            f.Show();
            this.Hide();
            f.FormClosed += (o, w) => this.Show();

            f.MANV.Text = "Mã nhân viên : " + Program.username;
            f.HOTEN.Text = "Họ tên : " + Program.mHoten;
            f.NHOM.Text = "Nhóm : " + Program.mGroup;
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
