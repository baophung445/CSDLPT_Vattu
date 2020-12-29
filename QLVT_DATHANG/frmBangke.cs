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
    public partial class frmBangke : Form
    {
        public frmBangke()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String quyen;
            if (Program.mGroup == "CONGTY")
            {
                quyen = "F";
            }
            else
            {
                quyen = "C";
            }
            string loai1 = comboBox1.Text;
            String loai = comboBox1.Text.Substring(0, 1);
            String bd = dateTimePicker1.Text;
            String kt = dateTimePicker2.Text;

            if (bd.CompareTo(kt) > 0)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu", string.Empty, MessageBoxButtons.OK);
                return;
            }
            ReportBangkeVatTu rp = new ReportBangkeVatTu(quyen, loai, bd, kt);

            dateTimePicker2.CustomFormat = dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            String bd1 = dateTimePicker1.Text;
            String kt1 = dateTimePicker2.Text;
            rp.label1.Text = "BẢNG KÊ CHI TIẾT SỐ LƯỢNG - TRỊ GIÁ PHIẾU " + loai1.ToUpper() + " " + "TỪ NGÀY " + bd1 + " ĐẾN NGÀY: " + kt1;
            ReportPrintTool print = new ReportPrintTool(rp);
            print.ShowPreviewDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
