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
using SGE.Entity;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmConsultarDocumentoscs : DevExpress.XtraEditors.XtraForm
    {
        public List<EPagosCuotas> lstpago = new List<EPagosCuotas>();
        public int cod;
        public FrmConsultarDocumentoscs()
        {
            InitializeComponent();
        }

        public void cargar()
        {
            lstpago = new BVentas().Listar_Pagos_Documentos(cod);
            grdDocumentos.DataSource = lstpago;
            grdDocumentos.Refresh();
        }

        private void verDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EPagosCuotas obj = (EPagosCuotas)viewDocumentos.GetRow(viewDocumentos.FocusedRowHandle);


            EPlanillaCobranzaDet oBe = new BVentas().ObtenerDocumentoXid(obj.cntc_icod_documento);
            if (oBe == null)
                return;
            frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
            //frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
            //frm.ObePlnCab = ObePlnCab;
            frm.oBePln = oBe;
            frm.View = true;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
    }
}