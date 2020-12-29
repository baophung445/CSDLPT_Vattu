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
    public partial class frmKho : Form
    {
        private int vitri;
        private string macn;
        private Stack<String> stackundo = new Stack<string>(16);
        String query = "";
        Boolean isDel = true;
        public frmKho()
        {
            InitializeComponent();
        }

        private void khoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.khoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            LoadTable();
            cmbCN.DataSource = Program.bds_dspm.DataSource;
            cmbCN.DisplayMember = "TENCN";
            cmbCN.ValueMember = "TENSERVER";
            cmbCN.SelectedIndex = Program.mChinhanh;


        }
        private void LoadTable()
        {
            try
            {
                this.dataSet.EnforceConstraints = false;

                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.dataSet.Kho);

                this.chiNhanhTableAdapter.Connection.ConnectionString = Program.connstr;
                this.chiNhanhTableAdapter.Fill(this.dataSet.ChiNhanh);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);


                macn = ((DataRowView)khoBindingSource[0])["MACN"].ToString();
                if (Program.mGroup.Equals("CONGTY"))
                {
                    btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnReload.Enabled = false;
                    btnGhi.Enabled = btnUndo.Enabled = false;
                    cmbCN.Enabled = true;
                    groupBox1.Enabled = false;
                    //khoGridControl.Enabled = false;
                }
                else if (Program.mGroup == "USSER")
                {
                    btnXoa.Enabled = btnSua.Enabled = btnReload.Enabled = true;
                    btnThem.Enabled = true;
                    cmbCN.Enabled = txtCN.Enabled = false;
                    btnGhi.Enabled = btnUndo.Enabled = false;
                    groupBox1.Enabled = false;
                }
                else if (Program.mGroup == "CHINHANH")
                {
                    btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnReload.Enabled
                        = true;
                    btnGhi.Enabled = btnUndo.Enabled = false;
                    cmbCN.Enabled = false; txtCN.Enabled = false;
                    groupBox1.Enabled = false;
                }
                if (stackundo.Count != 0)
                {
                    btnUndo.Enabled = true;
                }
                else btnUndo.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCN.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            Program.servername = cmbCN.SelectedValue.ToString();

            if (cmbCN.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }

            if (Program.KetNoi() == 0)
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", string.Empty, MessageBoxButtons.OK);
            else
            {
                LoadTable();
            }
        }

        private int ktkho(string makho)
        {
            int result = 1;
            string lenh = string.Format("EXEC sp_timkho {0}", makho);
            using (SqlConnection connection = new SqlConnection(Program.connstr))
            {
                connection.Open();
                SqlCommand sqlcmt = new SqlCommand(lenh, connection);
                sqlcmt.CommandType = CommandType.Text;
                try
                {
                    sqlcmt.ExecuteNonQuery();   // return 1;
                }
                catch
                {
                    result = 0;
                }
                return result;
            }
        }

        private void DisableForm()
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnThoat.Enabled = true;
            txtCN.Enabled = cmbCN.Enabled = false;

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox1.Enabled = true;
            vitri = khoBindingSource.Position;
            khoBindingSource.AddNew();
            txtCN.Text = macn;
            txtMa.Enabled = true;
            isDel = true;
            DisableForm();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = khoBindingSource.Position;
            groupBox1.Enabled = true;
            txtMa.Enabled = false;
            isDel = false;
            query = String.Format("Update Kho Set TENKHO=N'{1}', DIACHI=N'{2}', MACN=N'{3}' Where MAKHO=N'{0}' ", txtMa.Text, txtTen.Text, txtDiaChi.Text, txtCN.Text);
            DisableForm();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string makho = "";
            makho = ((DataRowView)khoBindingSource[khoBindingSource.Position])["MAKHO"].ToString();
            if (phieuNhapBindingSource.Count + phieuXuatBindingSource.Count + datHangBindingSource.Count > 0)
            {
                MessageBox.Show("Không thể xóa kho này vì đã lập phiếu", "", MessageBoxButtons.OK);
                return;
            }
            else if (MessageBox.Show("Bạn có thật sự muốn xóa kho  này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    String tenkho = ((DataRowView)khoBindingSource[khoBindingSource.Position])["TENKHO"].ToString();
                    String diachi = ((DataRowView)khoBindingSource[khoBindingSource.Position])["DIACHI"].ToString();
                    khoBindingSource.RemoveCurrent();

                    // macn = ((DataRowView)khoBindingSource[khoBindingSource.Position])["MACN"].ToString();
                    this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khoTableAdapter.Update(this.dataSet.Kho);
                    query = String.Format("Insert into Kho (MAKHO, TENKHO, DIACHI, MACN) values(N'{0}', N'{1}', N'{2}',N'{3}' )", makho, tenkho, diachi, macn);
                    stackundo.Push(query);
                    LoadTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa vật tư. Bạn hãy xóa lại \n", ex.Message, MessageBoxButtons.OK);
                    //Đặt con trỏ về vị trí hiện thời
                    this.khoTableAdapter.Fill(this.dataSet.Kho);
                    khoBindingSource.Position = khoBindingSource.Find("MAKHO", makho);
                    return;
                }
            }

        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMa.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Mã kho không được thiếu!", "", MessageBoxButtons.OK);
                txtMa.Focus();
                return;
            }
            if (txtMa.Text.Length > 4)
            {
                MessageBox.Show("Mã kho không được quá 4 kí tự !", "", MessageBoxButtons.OK);
                txtMa.Focus();
                return;
            }
            else if (txtMa.Text.Contains(" "))
            {
                MessageBox.Show("Mã kho không được chứa khoảng trắng!", "", MessageBoxButtons.OK);
                txtMa.Focus();
                return;
            }
            if (txtMa.Enabled == true)
            {
                try
                {
                    if (ktkho(txtMa.EditValue.ToString()) == 1)
                    {
                        MessageBox.Show("Mã kho không được trùng!", "", MessageBoxButtons.OK);
                        txtMa.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //txtMAVT.SelectAll();
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            if (txtTen.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Tên kho không được thiếu !", "", MessageBoxButtons.OK);
                txtTen.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Địa chỉ không được thiếu!", "", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return;
            }
            try
            {
                //Lưu vô dataset
                khoBindingSource.EndEdit();
                khoBindingSource.ResetCurrentItem();

                //Lưu vô CSDL
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Update(this.dataSet.Kho);
                if (isDel)
                {
                    query = String.Format("Delete from Kho where MAKHO=N'{0}'", txtMa.Text);
                }
                stackundo.Push(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi kho." + ex.Message);
                return;
            }

            LoadTable();
        }

        private void btnUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String lenh = stackundo.Pop();
            using (SqlConnection connection = new SqlConnection(Program.connstr))
            {
                connection.Open();
                SqlCommand sqlcmt = new SqlCommand(lenh, connection);
                sqlcmt.CommandType = CommandType.Text;
                try
                {
                    //MessageBox.Show(lenh);
                    sqlcmt.ExecuteNonQuery();
                    LoadTable();
                }
                catch
                {
                    MessageBox.Show(lenh);
                }
            }
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.khoTableAdapter.Fill(this.dataSet.Kho);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload :" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (groupBox1.Enabled)
            {
                if (MessageBox.Show("Dữ liệu Form Kho vẫn chưa lưu vào Database! \nBạn có chắn chắn muốn thoát?", "Thông báo",
                            MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            khoBindingSource.CancelEdit();
            if (btnThem.Enabled == false) khoBindingSource.Position = vitri;
            khoGridControl.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = false;
        }
    }
}
