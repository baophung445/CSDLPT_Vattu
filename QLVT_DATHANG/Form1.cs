using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLVT_DATHANG
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Form checkExist(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == ftype)
                {
                    return f;
                }
            }
            return null;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmNhanVien));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmNhanVien fNV = new frmNhanVien();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmVatTu));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmVatTu fNV = new frmVatTu();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmKho));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmKho fNV = new frmKho();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmTaoLogin));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmTaoLogin fNV = new frmTaoLogin();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program.mGroup.Equals("CHINHANH") || Program.mGroup.Equals("CONGTY"))
            {
                btn1.Enabled = true;
            }
            else
            {
                btn1.Enabled = false;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmDatHang));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmDatHang fNV = new frmDatHang();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmPhieuNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmPhieuNhap fNV = new frmPhieuNhap();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmPhieuXuat));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmPhieuXuat fNV = new frmPhieuXuat();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmINDSNV));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmINDSNV fNV = new frmINDSNV();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmIndsVattu));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmIndsVattu fNV = new frmIndsVattu();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmBangke));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmBangke fNV = new frmBangke();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = this.checkExist(typeof(frmINDSDDKoCoPhieuNhap));
            if (f != null)
            {
                f.Activate();
            }
            else
            {
                frmINDSDDKoCoPhieuNhap fNV = new frmINDSDDKoCoPhieuNhap();
                fNV.MdiParent = this;
                fNV.Show();
            }
        }

        private void MANV_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}
