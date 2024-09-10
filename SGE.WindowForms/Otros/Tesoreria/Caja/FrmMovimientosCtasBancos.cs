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


namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmMovimientosCtasBancos : DevExpress.XtraEditors.XtraForm
    {
        private List<ELibroBancos> mlist = new List<ELibroBancos>();
        public string FechaI;
        public string FechaF;
        public int icod_cuenta;
        public FrmMovimientosCtasBancos()
        {
            InitializeComponent();
        }

        private void FrmMovimientosCtasBancos_Load(object sender, EventArgs e)
        {
            mlist = new BTesoreria().ListarMovimientoCuentasMovimientos(Convert.ToDateTime(FechaI), Convert.ToDateTime(FechaF), Parametros.intEjercicio, icod_cuenta);
            grdMov.DataSource = mlist;
        }

        private void viewMov_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void grdMov_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}