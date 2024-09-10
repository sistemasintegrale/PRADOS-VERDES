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


namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultaPagosDocumentosXPagarAUnaFecha : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosDocumentosXPagar));

        private List<EDocPorPagarPago> Lista = new List<EDocPorPagarPago>();
        public DateTime sfecha;
        //public long doxcc_icod_correlativo;
        public EDocPorPagar eDocXPagar;

        public FrmConsultaPagosDocumentosXPagarAUnaFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultaPagosDocumentosXPagarAUnaFecha_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXPagar.tdocc_vabreviatura_tipo_doc + " - " + eDocXPagar.doxpc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXPagar.tdocc_vabreviatura_tipo_doc + " - " + eDocXPagar.doxpc_vnumero_doc;
            Carga();
        }
        private void Carga()
        {
            Lista = new BCuentasPorPagar().ListarPagoDocumentoXPagarXIdDocXPagarAunaFecha(eDocXPagar.doxpc_icod_correlativo, Parametros.intEjercicio, sfecha);
            grd.DataSource = Lista;
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}