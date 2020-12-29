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
    public partial class frmVatTu : Form
    {
        private int vitri;
        private Stack<String> stackundo = new Stack<string>(16);
        String query = "";
        Boolean isDel = true;
        public frmVatTu()
        {
            InitializeComponent();
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vattuBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmVatTu_Load(object sender, EventArgs e)
        {
            LoadTable();


        }
        private void LoadTable()
        {
            this.dataSet.EnforceConstraints = false;

            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);

            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);

            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.dataSet.CTPN);

            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.dataSet.CTPX);

            if (Program.mGroup == "CONGTY")
            {
                btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnUndo.Enabled = false;
                btnReload.Enabled = true;
                groupBox1.Enabled = false;
                //vattuGridControl.Enabled = false;
            }
            else
            {
                btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThem.Enabled = true;
                btnUndo.Enabled = btnGhi.Enabled = false;
                groupBox1.Enabled = false;
            }
        }
        private void loadUndo()
        {
            if (stackundo.Count != 0)
            {
                btnUndo.Enabled = true;
            }
            else btnUndo.Enabled = false;
        }
        private void DisableForm()
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnThoat.Enabled = true;
        }
        private int ktvattu(string mavt)
        {
            int result = 1;
            string lenh = string.Format("EXEC sp_timvattu {0}", mavt);
            using (SqlConnection connection = new SqlConnection(Program.connstr))
            {
                connection.Open();
                SqlCommand sqlcmt = new SqlCommand(lenh, connection);
                sqlcmt.CommandType = CommandType.Text;
                try
                {
                    sqlcmt.ExecuteNonQuery();
                }
                catch
                {
                    result = 0;
                }
                return result;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = vattuBindingSource.Position;
            groupBox1.Enabled = true;
            txtMa.Enabled = true;
            vattuBindingSource.AddNew();
            isDel = true;
            DisableForm();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            vitri = vattuBindingSource.Position;
            groupBox1.Enabled = true;
            txtMa.Enabled = false;
            DisableForm();
            isDel = false;
            query = String.Format("Update Vattu Set TENVT=N'{1}', DVT=N'{2}' Where MAVT=N'{0}' ", txtMa.Text, txtTen.Text, txtDVT.Text);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string mavt = "";
            mavt = ((DataRowView)vattuBindingSource[vattuBindingSource.Position])["MAVT"].ToString();
            if (cTDDHBindingSource.Count + cTPNBindingSource.Count + cTPXBindingSource.Count > 0)
            {
                MessageBox.Show("Không thể xóa vật tư này vì đã lập phiếu", "", MessageBoxButtons.OK);
                return;
            }
            else if (MessageBox.Show("Bạn có thật sự muốn xóa vật tư này ???", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    String mavattu = ((DataRowView)vattuBindingSource[vattuBindingSource.Position])["MAVT"].ToString();
                    String tenvattu = ((DataRowView)vattuBindingSource[vattuBindingSource.Position])["TENVT"].ToString();
                    String donvitinh = ((DataRowView)vattuBindingSource[vattuBindingSource.Position])["DVT"].ToString();
                    query = String.Format("Insert into Vattu (MAVT, TENVT, DVT) values(N'{0}', N'{1}', N'{2}' )", mavattu, tenvattu, donvitinh);
                    vattuBindingSource.RemoveCurrent();
                    this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.vattuTableAdapter.Update(this.dataSet.Vattu);
                    MessageBox.Show("Xóa thành công !", "", MessageBoxButtons.OK);
                    stackundo.Push(query);
                    LoadTable();
                    loadUndo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa vật tư. Bạn hãy xóa lại \n", ex.Message, MessageBoxButtons.OK);
                    this.vattuTableAdapter.Fill(this.dataSet.Vattu);
                    vattuBindingSource.Position = vattuBindingSource.Find("MAVT", mavt);
                    return;
                }
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //txtMa.Text = txtMa.Text.Replace(" ", "");
            if (txtMa.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Mã vật tư không được thiếu!", "", MessageBoxButtons.OK);
                txtMa.Focus();
                return;
            }
            if (txtMa.Text.Length > 4)
            {
                MessageBox.Show("Mã vật tư không được quá 4 kí tự !", "", MessageBoxButtons.OK);
                txtMa.Focus();
                return;
            }
            else if (txtMa.Text.Trim().Contains(" "))
            {
                MessageBox.Show("Mã vật tư không được chứa khoảng trắng!", "", MessageBoxButtons.OK);
                txtMa.Focus();
                return;
            }
            if (txtMa.Enabled == true)
            {
                try
                {
                    if (ktvattu(txtMa.EditValue.ToString()) == 1)
                    {
                        MessageBox.Show("Mã vật tư không được trùng!", "", MessageBoxButtons.OK);
                        txtMa.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            if (txtTen.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Tên vật tư không được thiếu !", "", MessageBoxButtons.OK);
                txtTen.Focus();
                return;
            }
            if (txtDVT.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Đơn vị tính không được thiếu!", "", MessageBoxButtons.OK);
                txtDVT.Focus();
                return;
            }
            try
            {
                if (isDel)
                {
                    query = String.Format("Delete from Vattu where MAVT=N'{0}'", txtMa.Text);
                }
                //Lưu vô dataset
                vattuBindingSource.EndEdit();
                vattuBindingSource.ResetCurrentItem();

                //Lưu vô CSDL
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Update(this.dataSet.Vattu);
                MessageBox.Show("Ghi thành công !", "", MessageBoxButtons.OK);
                stackundo.Push(query);
                LoadTable();
                loadUndo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi vật tư.." + ex.Message);
                return;
            }

            //LoadTable();
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
                    loadUndo();
                    //LoadTable();
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
                this.vattuTableAdapter.Fill(this.dataSet.Vattu);

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
                if (MessageBox.Show("Dữ liệu Form Vật Tư vẫn chưa lưu vào Database! \nBạn có chắn chắn muốn thoát?", "Thông báo",
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
            vattuBindingSource.CancelEdit();
            if (btnThem.Enabled == false) vattuBindingSource.Position = vitri;
            vattuGridControl.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = false;
        }
    }
}
