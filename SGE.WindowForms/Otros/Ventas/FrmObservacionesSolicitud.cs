using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmObservacionesSolicitud : DevExpress.XtraEditors.XtraForm
    {
        public FrmObservacionesSolicitud()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FrmObservacionesSolicitud_Load(object sender, EventArgs e)
        {

        }
    }
}