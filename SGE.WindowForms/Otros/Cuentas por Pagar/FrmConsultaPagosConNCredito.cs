using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultaPagosConNCredito : DevExpress.XtraEditors.XtraForm
    {
        public FrmConsultaPagosConNCredito()
        {
            InitializeComponent();
        }

        List<EDocPorPagarNotaCredito> Lista = new List<EDocPorPagarNotaCredito>();
        public long codENotaCreditoCliente;

        private void FrmConsultaPagosConNCredito_Load(object sender, EventArgs e)
        {
            Cargar();
            grd.DataSource = Lista;
        }

        private void Cargar()
        {

            Lista = new BCuentasPorPagar().listarDxpPagosConNc(0, codENotaCreditoCliente, Parametros.intEjercicio);
            grd.DataSource = Lista;
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}