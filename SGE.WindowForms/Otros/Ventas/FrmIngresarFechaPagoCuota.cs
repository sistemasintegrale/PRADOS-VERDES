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

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmIngresarFechaPagoCuota : DevExpress.XtraEditors.XtraForm
    {
        public FrmIngresarFechaPagoCuota()
        {
            InitializeComponent();
        }

        private void FrmIngresarFechaPagoCuota_Load(object sender, EventArgs e)
        {
            dteFechaPago.DateTime = DateTime.Now;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}