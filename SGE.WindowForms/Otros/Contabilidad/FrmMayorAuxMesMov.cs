using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmMayorAuxMesMov : DevExpress.XtraEditors.XtraForm
    {
        public List<EVoucherContableDet> mlist = new List<EVoucherContableDet>();
        public bool NoMostrarMndExt;

        public FrmMayorAuxMesMov()
        {
            InitializeComponent();
        }

        private void FrmMayorAuxMesMov_Load(object sender, EventArgs e)
        {
            grdMov.DataSource = mlist;
            viewMov.RefreshData();
            CargarCamposMndExt();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void CargarCamposMndExt()
        {
            if (NoMostrarMndExt)
            {
                gridColumn4.Visible = false;
                gridColumn8.Visible = false;
                gridColumn9.Visible = false;
            }
        }
    }
}