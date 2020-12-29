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

namespace QLVT_DATHANG
{
    public partial class frmTaoLogin : Form
    {
        public frmTaoLogin()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            this.dataSet.EnforceConstraints = false;
            this.dsnvchuacotkTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dsnvchuacotkTableAdapter.Fill(this.dataSet.dsnvchuacotk);
        }

        private void dsnvchuacotkBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.dsnvchuacotkBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);
        }

        private void frmTaoLogin_Load(object sender, EventArgs e)
        {
            LoadData();

            txtPassword.UseSystemPasswordChar = true;
            txtUsername.Enabled = false;

            if (Program.mGroup == "CONGTY")
            {
                rdCN.Enabled = rdUser.Enabled = false;
            }
            if (Program.mGroup == "CHINHANH")
            {
                rdCT.Enabled = false;
            }
        }

        private bool CreateLogin(string loginName, string password, string username, string role)
        {
            bool result = true;
            string strLenh = string.Format("EXEC SP_TAOTAIKHOAN {0},{1},{2},{3}", loginName, password, username, role);
            using (SqlConnection connection = new SqlConnection(Program.connstr))
            {
                connection.Open();
                SqlCommand sqlcmd = new SqlCommand(strLenh, connection);
                sqlcmd.CommandType = CommandType.Text;
                try
                {
                    sqlcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result = false;
                    // MessageBox.Show(ex.Message + " ");
                }
            }
            return result;
        }

        private bool ktraTrungTenLogin(string loginName)  // kiểm tra tên login có bị trùng hay ko -> lúc click vào nút tạo tk
        {
            bool result = true;
            string strLenh = string.Format("EXEC sp_kiemtratrungtenlogin {0}", loginName);
            using (SqlConnection connection = new SqlConnection(Program.connstr))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(strLenh, connection);
                sqlCommand.CommandType = CommandType.Text;
                try
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    string kq = "";
                    while (reader.Read())
                    {
                        kq = (string)reader["name"];
                    }

                    if (kq.Length == 0)
                    {
                        result = false;
                    }

                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message + " ");
                }
                return result;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtLoginName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("LoginName không được thiếu !", "", MessageBoxButtons.OK);
                txtLoginName.Focus();
                return;
            }
            if (txtLoginName.Text.Contains(" "))
            {
                MessageBox.Show("LoginName không được có khoảng trống !", "", MessageBoxButtons.OK);
                txtLoginName.Focus();
                return;
            }
            if (ktraTrungTenLogin(txtLoginName.Text))
            {
                MessageBox.Show("LoginName bị trùng. Vui lòng chọn LoginName khác !", "", MessageBoxButtons.OK);
                txtLoginName.Focus();
                return;
            }
            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Password không được thiếu !", "", MessageBoxButtons.OK);
                txtPassword.Focus();
                return;
            }
            if ((rdCN.Checked || rdCT.Checked || rdUser.Checked) == false)
            {
                MessageBox.Show("Vai trò không được thiếu !", "", MessageBoxButtons.OK);
                return;
            }
            try
            {
                String role = rdCT.Checked ? "CONGTY" : (rdCN.Checked ? "CHINHANH" : "USSER");
                CreateLogin(txtLoginName.Text, txtPassword.Text, txtUsername.Text, role);
                MessageBox.Show("Tạo Login thành công!", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi.\n" + ex.Message);
                return;
            }
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbHoTen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
