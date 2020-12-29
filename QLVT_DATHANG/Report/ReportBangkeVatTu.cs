using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLVT_DATHANG.Report
{
    public partial class ReportBangkeVatTu : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportBangkeVatTu(string quyen, string loai, string ngaybd, string ngaykt)
        {

            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = quyen;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = loai;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = ngaybd;
            this.sqlDataSource1.Queries[0].Parameters[3].Value = ngaykt;
            this.sqlDataSource1.Fill();
        }

    }
}
