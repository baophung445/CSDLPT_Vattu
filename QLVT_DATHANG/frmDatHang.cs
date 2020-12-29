using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class frmDatHang : Form
    {
        private int vitri;
        private string maddh;
        private string mavt;
        private string soluong;
        private string dongia;
        private Stack<String> stackundo = new Stack<string>(16);
        String query = "";
        Boolean isDel = true;
        public frmDatHang()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.datHangBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }
        private void LoadTable()
        {
            try
            {
                this.dataSet.EnforceConstraints = false;

                //this.taDSVT.Connection.ConnectionString = Program.connstr;
                //this.taDSVT.Fill(this.dataset.sp_dsVT);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);

                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.dataSet.Kho);

                if (Program.mGroup == "CONGTY")
                {
                    btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnUndo.Enabled = false;
                    btnReload.Enabled = btnThoat.Enabled = true;
                    panel1.Enabled = true;
                    groupBox1.Enabled = false;
                    contextMenuStrip1.Enabled = false;
                    //cTDDHGridControl.Enabled = false;

                    
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
                else btnUndo.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmDatHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.Vattu' table. You can move, or remove it, as needed.
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);
            if (Program.mGroup != "CONGTY")
            {
                this.datHangBindingSource.Filter = "MANV='" + Program.username + "'";
            }
            LoadTable();
            cmbCN.DataSource = Program.bds_dspm.DataSource;
            cmbCN.DisplayMember = "TENCN";
            cmbCN.ValueMember = "TENSERVER";
            cmbCN.SelectedIndex = Program.mChinhanh;
            btnGhiCTDDH.Enabled = false;
            groupBox1.Enabled = false;

        }

        private void ghiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThemCTDDH.Enabled = btnXoaCTDDH.Enabled = true;
            maddh = ((DataRowView)cTDDHBindingSource[cTDDHBindingSource.Count - 1])["MasoDDH"].ToString();
            mavt = ((DataRowView)cTDDHBindingSource[cTDDHBindingSource.Count - 1])["MAVT"].ToString();
            soluong = ((DataRowView)cTDDHBindingSource[cTDDHBindingSource.Count - 1])["SOLUONG"].ToString();
            dongia = ((DataRowView)cTDDHBindingSource[cTDDHBindingSource.Count - 1])["DONGIA"].ToString();
            if (mavt == String.Empty)
            {
                MessageBox.Show("Vật tư không được thiếu!", "", MessageBoxButtons.OK);
                //cTDDHBindingSource.RemoveCurrent();
                //btnGhiCTDDH.Enabled = false;
                btnThemCTDDH.Enabled = false;
                btnXoaCTDDH.Enabled = false;
                return;
            }

            if (ktctddh(maddh, mavt) == 1)
            {
                MessageBox.Show("Vật tư không được trùng!", "", MessageBoxButtons.OK);
                //cTDDHBindingSource.RemoveCurrent();
                //btnGhiCTDDH.Enabled = false;
                btnThemCTDDH.Enabled = false;
                btnXoaCTDDH.Enabled = false;
                return;
            }

            if (soluong == string.Empty)
            {
                MessageBox.Show("Số lượng không được thiếu!", "", MessageBoxButtons.OK);
                //btnGhiCTDDH.Enabled = false;
                btnThemCTDDH.Enabled = false;
                btnXoaCTDDH.Enabled = false;
                return;
            }

            if (dongia == string.Empty)
            {
                MessageBox.Show("Đơn giá không được thiếu!", "", MessageBoxButtons.OK);
                //btnGhiCTDDH.Enabled = false;
                btnThemCTDDH.Enabled = false;
                btnXoaCTDDH.Enabled = false;
                return;
            }

            try
            {
                cTDDHBindingSource.EndEdit();
                cTDDHBindingSource.ResetCurrentItem();

                MessageBox.Show("Ghi thành công !!!");

                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Update(this.dataSet.CTDDH);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi chi tiết đơn đặt hàng " + ex.Message);
            }
            btnThemCTDDH.Enabled = btnXoaCTDDH.Enabled = true;
            btnGhiCTDDH.Enabled = false;
        }
        private bool KiemTraVatTuTrenView(string maVT)
        {
            bool kt = false;
            for (int index = 0; index < cTDDHBindingSource.Count - 1; index++)
            {
                if (((DataRowView)cTDDHBindingSource[index])["MAVT"].ToString().Equals(maVT))
                {
                    kt = true;
                }
            }
            return kt;
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

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = datHangBindingSource.Position;
            datHangBindingSource.AddNew();
            DisEnableForm();
            txtMaNV.Text = Program.username;
            txtNgay.Text = DateTime.Now.ToString().Substring(0,10);
            txtNgay.Enabled = false;
            groupBox1.Enabled = true;
            txtMaNV.Enabled = false;
            isDel = true;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox1.Enabled = true;
            vitri = datHangBindingSource.Position;
            txtMaDDH.Enabled = txtMaNV.Enabled=txtNgay.Enabled = false;
            groupBox1.Enabled = true;
            isDel = false;
            query = String.Format("Update DatHang Set NGAY=N'{1}', NhaCC=N'{2}', MANV={3}, MAKHO=N'{4}' Where MasoDDH=N'{0}' ", txtMaDDH.Text, txtNgay.Text, txtNhaCC.Text, Program.username, cmbKho.Text);
            DisEnableForm();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cTDDHBindingSource.Count + phieuNhapBindingSource.Count > 0)
            {
                MessageBox.Show("Đơn đặt hàng đã có phiếu nhập hoặc chi tiết đơn đặt hàng nên không thể xóa !", "", MessageBoxButtons.OK);
                return;
            }
            else if (MessageBox.Show("Bạn có thật sự muốn xóa ??? ", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    String masoddh = ((DataRowView)datHangBindingSource[datHangBindingSource.Position])["MasoDDH"].ToString();
                    String ngay = ((DataRowView)datHangBindingSource[datHangBindingSource.Position])["NGAY"].ToString();
                    String nhacc = ((DataRowView)datHangBindingSource[datHangBindingSource.Position])["NhaCC"].ToString();
                    String makho = ((DataRowView)datHangBindingSource[datHangBindingSource.Position])["MAKHO"].ToString();
                    datHangBindingSource.RemoveCurrent();

                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.dataSet.DatHang);

                    query = String.Format("Insert into DatHang (MasoDDH, NGAY, NHACC, MANV, MAKHO) values(N'{0}', N'{1}', N'{2}',{3},N'{4}' )", masoddh, ngay, nhacc, Program.username, makho);
                    stackundo.Push(query);
                    LoadTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa đơn đặt hàng. Bạn hãy xóa lại \n", ex.Message, MessageBoxButtons.OK);
                    this.datHangTableAdapter.Fill(this.dataSet.DatHang);
                    return;
                }
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtMaDDH.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Mã đơn đặt hàng không được thiếu !", "", MessageBoxButtons.OK);
                txtMaDDH.Focus();
                return;
            }

            if (txtMaDDH.Text.Length > 8)
            {
                MessageBox.Show("Mã đơn đặt hàng không được quá 8 kí tự !", "", MessageBoxButtons.OK);
                txtMaDDH.Focus();
                return;
            }
            if (txtMaDDH.Enabled == true)
            {
                try
                {
                    if (ktddh(txtMaDDH.Text) == 1)
                    {
                        MessageBox.Show("Mã đơn đặt hàng không được trùng !", "", MessageBoxButtons.OK);
                        txtMaDDH.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            if (txtNgay.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Ngày không được thiếu !", "", MessageBoxButtons.OK);
                txtNgay.Focus();
                return;
            }


            if (txtNhaCC.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Nhà cung cấp không được thiếu!", "", MessageBoxButtons.OK);
                txtNhaCC.Focus();
                return;
            }
            if (cmbKho.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Mã kho không được thiếu!", "", MessageBoxButtons.OK);
                cmbKho.Focus();
                return;
            }
            try
            {
                datHangBindingSource.EndEdit();
                datHangBindingSource.ResetCurrentItem();

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Update(this.dataSet.DatHang);

                if (isDel == true)
                {
                    query = String.Format("Delete from DatHang where MasoDDH=N'{0}'", txtMaDDH.Text);
                }
                stackundo.Push(query);

                MessageBox.Show("Ghi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi Đơn Đặt Hàng .\n" + ex.Message);
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
            try
            {
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);
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
                if (MessageBox.Show("Dữ liệu Form Đơn Đặt Hàng vẫn chưa lưu vào Database! \nBạn có chắn chắn muốn thoát?", "Thông báo",
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

        private int ktddh(string maddh)
        {
            int result = 1;
            string lenh = string.Format("EXEC sp_timddh {0}", maddh);
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

        private int ktctddh(string maddh, string mavt)
        {
            int result = 1;
            string lenh = string.Format("EXEC sp_timctddh {0},{1}", maddh, mavt);
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

        private void btnThemCTDDH_Click(object sender, EventArgs e)
        {
            cTDDHBindingSource.AddNew();
            btnGhiCTDDH.Enabled = true;
            btnThemCTDDH.Enabled = btnXoaCTDDH.Enabled = false;
        }

        private void btnXoaCTDDH_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thực sự muốn xóa ??", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {

                    cTDDHBindingSource.RemoveCurrent();

                    this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTDDHTableAdapter.Update(this.dataSet.CTDDH);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi tiết đơn đặt hàng. Bạn hãy xóa lại \n", ex.Message, MessageBoxButtons.OK);
                    this.cTDDHTableAdapter.Fill(this.dataSet.CTDDH);
                    return;
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            datHangBindingSource.CancelEdit();
            if (btnThem.Enabled == false) datHangBindingSource.Position = vitri;
            datHangGridControl.Enabled = true;
            groupBox1.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled  = true;
            btnGhi.Enabled = false;
        }

        private void txtNgay_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
