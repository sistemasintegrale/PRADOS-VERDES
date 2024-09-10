using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmProductividadDetalle : DevExpress.XtraEditors.XtraForm
    {
        List<EProductividadDet> lst = new List<EProductividadDet>();
        public int intPersonal;
        public DateTime? dt1 = null;
        public DateTime? dt2 = null;
        public frmProductividadDetalle()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();

        }

        private void frmProductividadDetalle_Load(object sender, EventArgs e)
        {
            lst = new BOperaciones().listarProductividadDetalle(intPersonal, dt1, dt2, Parametros.intEjercicio);
            gridControl1.DataSource = lst;
        }
    }
}