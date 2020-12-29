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
    public partial class frmIndsVattu : Form
    {
        public frmIndsVattu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportInVatTu report = new ReportInVatTu();
            report.label1.Text = "DS VẬT TƯ";
            ReportPrintTool print = new ReportPrintTool(report);
            print.ShowPreviewDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
