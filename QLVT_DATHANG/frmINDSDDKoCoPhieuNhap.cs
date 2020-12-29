using DevExpress.XtraReports.UI;
using QLVT_DATHANG.Report;
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
    public partial class frmINDSDDKoCoPhieuNhap : Form
    {
        public frmINDSDDKoCoPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmINDSDDKoCoPhieuNhap_Load(object sender, EventArgs e)
        {
            cmbCN.DataSource = Program.bds_dspm.DataSource;
            cmbCN.DisplayMember = "TENCN";
            cmbCN.ValueMember = "TENSERVER";
            cmbCN.SelectedIndex = Program.mChinhanh;
            if (Program.mGroup == "CONGTY")
            {
                cmbCN.Enabled = true;
            }
            else
            {
                cmbCN.Enabled = false;
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

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportDSDHHkoCoPhieuNhap rp = new ReportDSDHHkoCoPhieuNhap();
            rp.label1.Text = "DANH SÁCH ĐƠN ĐẶT HÀNG CHƯA CÓ PHIẾU NHẬP CỦA " + cmbCN.Text.ToUpper();
            ReportPrintTool print = new ReportPrintTool(rp);
            print.ShowPreviewDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
