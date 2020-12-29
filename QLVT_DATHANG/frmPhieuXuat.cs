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
    public partial class frmPhieuXuat : Form
    {
        private int vitri;
        private String mavt;
        private String mapx;
        private string soluong;
        private string dongia;
        private bool isAdd = false;
        private Stack<String> stackundo = new Stack<string>(16);
        String query = "";
        Boolean isDel = true;
        public frmPhieuXuat()
        {
            InitializeComponent();
        }

        private void phieuXuatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.phieuXuatBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }
        private void LoadTable()
        {
            try
            {
                this.dataSet.EnforceConstraints = false;

                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.dataSet.Kho);

                this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPXTableAdapter.Fill(this.dataSet.CTPX);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

                if (Program.mGroup == "CONGTY")
                {
                    btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnUndo.Enabled = false;
                    btnReload.Enabled = btnThoat.Enabled = true;
                    panel1.Enabled = true;
                    groupBox1.Enabled = false;
                    contextMenuStrip1.Enabled = false;
                }
                else
                {
                    btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
                    btnGhi.Enabled = btnUndo.Enabled = false;
                    panel1.Enabled = false;
                    groupBox1.Enabled = true;
                }
                if (stackundo.Count != 0)
                {
                    btnUndo.Enabled = true;
                }
                else
                {
                    btnUndo.Enabled = false;
                    groupBox1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);
            LoadTable();
            if (Program.mGroup != "CONGTY")
            {
                this.phieuXuatBindingSource.Filter = "MANV='" + Program.username + "'";
            }
            LoadTable();
            cmbCN.DataSource = Program.bds_dspm.DataSource;
            cmbCN.DisplayMember = "TENCN";
            cmbCN.ValueMember = "TENSERVER";
            cmbCN.SelectedIndex = Program.mChinhanh;
            btnGhiCTPX.Enabled = false;
            groupBox1.Enabled = false;

        }
        private void EnableForm()
        {
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnUndo.Enabled = false;
        }
        private void DisEnableForm()
        {
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnUndo.Enabled = true;
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

        private int KiemTraPhieuXuat(string maPX)
        {
            int result = 1;
            string lenh = string.Format("EXEC sp_timphieuxuat {0}", maPX);
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
            vitri = phieuXuatBindingSource.Position;
            phieuXuatBindingSource.AddNew();
            DisEnableForm();
            groupBox1.Enabled = true;
            txtMaNV.Text = Program.username;
            txtMaNV.Enabled = false;
            txtNgay.Text = DateTime.Now.ToString().Substring(0, 10);
            txtNgay.Enabled = false;
           
            isAdd = true;
            isDel = true;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = phieuXuatBindingSource.Position;
            DisEnableForm();
            groupBox1.Enabled = true;
            txtMaPX.Enabled = txtMaNV.Enabled = false;
            isDel = false;
            query = String.Format("Update PhieuXuat Set NGAY=N'{1}', HOTENKH=N'{2}', MANV={3}, MAKHO=N'{4}' Where MAPX=N'{0}' ", txtMaPX.Text, txtNgay.Text, txtTenKH.Text, Program.username, cmbKho.Text);
            isAdd = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cTPXBindingSource.Count > 0)
            {
                MessageBox.Show("Phiếu Xuất đã có Chi Tiết Phiếu xuất nên không thể xóa !", "", MessageBoxButtons.OK);
                return;
            }

            else if (MessageBox.Show("Bạn thực sự muốn xóa ??", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    String mapx = ((DataRowView)phieuXuatBindingSource[phieuXuatBindingSource.Position])["MAPX"].ToString();
                    String ngay = ((DataRowView)phieuXuatBindingSource[phieuXuatBindingSource.Position])["NGAY"].ToString();
                    String tenkh = ((DataRowView)phieuXuatBindingSource[phieuXuatBindingSource.Position])["HOTENKH"].ToString();
                    String makho = ((DataRowView)phieuXuatBindingSource[phieuXuatBindingSource.Position])["MAKHO"].ToString();

                    phieuXuatBindingSource.RemoveCurrent();

                    this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);

                    query = String.Format("Insert into PhieuXuat (MAPX, NGAY, HOTENKH, MANV, MAKHO) values(N'{0}', N'{1}', N'{2}',{3},N'{4}' )", mapx, ngay, tenkh, Program.username, makho);
                    stackundo.Push(query);
                    LoadTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa phiếu xuất. Bạn hãy xóa lại \n", ex.Message, MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (isAdd)
            {
                if (KiemTraPhieuXuat(txtMaPX.Text) == 1)
                {
                    MessageBox.Show("Mã Phiếu Xuất không được trùng !", "", MessageBoxButtons.OK);
                    txtMaPX.Focus();
                    return;
                }

                if (txtMaPX.Text == string.Empty)
                {
                    MessageBox.Show("Mã Phiếu Xuất không được thiếu !", "", MessageBoxButtons.OK);
                    txtMaPX.Focus();
                    return;
                }

                if (txtMaPX.Text.Length > 8)
                {
                    MessageBox.Show("Mã Phiếu Xuất không được hơn 8 ký tự !", "", MessageBoxButtons.OK);
                    txtMaPX.Focus();
                    return;
                }
            }


            if (txtNgay.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Ngày không được thiếu !", "", MessageBoxButtons.OK);
                return;
            }

            if (txtTenKH.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Họ tên Khách hàng không được thiếu !", "", MessageBoxButtons.OK);
                return;
            }
            if (cmbKho.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Mã kho không được trống !", "", MessageBoxButtons.OK);
                return;
            }
            try
            {
                phieuXuatBindingSource.EndEdit();
                phieuXuatBindingSource.ResetCurrentItem();

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);

                if (isDel == true)
                {
                    query = String.Format("Delete from PhieuXuat where MAPX=N'{0}'", txtMaPX.Text);
                }
                stackundo.Push(query);

                MessageBox.Show("Ghi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi Phiếu xuất .\n" + ex.Message);
                return;
            }
            EnableForm();
            LoadTable();
            groupBox1.Enabled = false;
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
            LoadTable();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (groupBox1.Enabled)
            {
                if (MessageBox.Show("Dữ liệu Form Phiếu Xuất vẫn chưa lưu vào Database! \nBạn có chắn chắn muốn thoát?", "Thông báo",
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

        private void btnThemCTPX_Click(object sender, EventArgs e)
        {
            cTPXBindingSource.AddNew();
            btnGhiCTPX.Enabled = true;
            btnThemCTPX.Enabled = btnXoaCTPX.Enabled = false;
        }

        private void btnXoaCTPX_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn thực sự muốn xóa ??", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {

                    cTPXBindingSource.RemoveCurrent();

                    this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTPXTableAdapter.Update(this.dataSet.CTPX);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi tiết phiếu xuất. Bạn hãy xóa lại \n", ex.Message, MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private bool KiemTraVatTuTrenView(string maVT)
        {
            for (int index = 0; index < cTPXBindingSource.Count - 1; index++)
            {
                if (((DataRowView)cTPXBindingSource[index])["MAVT"].ToString().Equals(maVT))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnGhiCTPX_Click(object sender, EventArgs e)
        {
            mavt = ((DataRowView)cTPXBindingSource[cTPXBindingSource.Count - 1])["MAVT"].ToString();
            mapx = ((DataRowView)cTPXBindingSource[cTPXBindingSource.Count - 1])["MAPX"].ToString();
            soluong = ((DataRowView)cTPXBindingSource[cTPXBindingSource.Count - 1])["SOLUONG"].ToString();
            dongia = ((DataRowView)cTPXBindingSource[cTPXBindingSource.Count - 1])["DONGIA"].ToString();
            if (mavt == String.Empty)
            {
                MessageBox.Show("Vật tư không được thiếu!", "", MessageBoxButtons.OK);
                btnThemCTPX.Enabled = false;
                btnXoaCTPX.Enabled = false;
                return;
            }
            if (KiemTraVatTuTrenView(mavt) == false)
            {
                MessageBox.Show("Vật tư không được trùng!", "", MessageBoxButtons.OK);
                //cTPXBindingSource.RemoveCurrent();
                btnThemCTPX.Enabled = false;
                btnXoaCTPX.Enabled = false;
                return;
            }

            if (soluong == string.Empty)
            {
                MessageBox.Show("Số lượng không được thiếu!", "", MessageBoxButtons.OK);
                btnThemCTPX.Enabled = false;
                btnXoaCTPX.Enabled = false;
                return;
            }

            if (dongia == string.Empty)
            {
                MessageBox.Show("Đơn giá không được thiếu!", "", MessageBoxButtons.OK);
                btnThemCTPX.Enabled = false;
                btnXoaCTPX.Enabled = false;
                return;
            }

            try
            {
                cTPXBindingSource.EndEdit();
                cTPXBindingSource.ResetCurrentItem();

                MessageBox.Show("Ghi thành công !!!");

                this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPXTableAdapter.Update(this.dataSet.CTPX);
            }
            catch (Exception) { }
            btnThemCTPX.Enabled = true;
            btnXoaCTPX.Enabled = true;
            btnGhiCTPX.Enabled = false;
            LoadTable();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            phieuXuatBindingSource.CancelEdit();
            if (btnThem.Enabled == false) phieuXuatBindingSource.Position = vitri;
            phieuXuatGridControl.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnGhi.Enabled = false;
        }
    }
}
