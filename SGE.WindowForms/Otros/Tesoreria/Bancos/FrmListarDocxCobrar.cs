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
using System.Linq;


namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmListarDocxCobrar : DevExpress.XtraEditors.XtraForm
    {
        public int? intIcodCliente = null;
        public bool bDocFacBol = true;
        public EDocXCobrar EDocPorCobrar { get; set; }
        public int intOpcionPlanillaVenta = 0;
        public bool flag_multiple = false;
        public int TipoDoc = 0;
        public List<EDocXCobrar> lstDocPorCobrar = new List<EDocXCobrar>();
        public FrmListarDocxCobrar()
        {
            InitializeComponent();
        }
        private void FrmListarDocxCobrar_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            if (bDocFacBol)
                //lstDocPorCobrar = new BCuentasPorCobrar().ListarDocPorCobrarxCliente(intIcodCliente,Parametros.intEjercicio).Where(x=> x.tdocc_icod_tipo_doc == TipoDoc).ToList();
                lstDocPorCobrar = new BCuentasPorCobrar().ListarDocPorCobrarxCliente(intIcodCliente, Parametros.intEjercicio).ToList();
            else
            {
                lstDocPorCobrar = new BCuentasPorCobrar().ListarDocumentoAdelantoNotaCreditoPorCobrarCliente(Convert.ToInt32(intIcodCliente));
                if (intOpcionPlanillaVenta == 1)//SOLO NOTAS DE CREDITO
                    lstDocPorCobrar = lstDocPorCobrar.Where(x => x.tdocc_icod_tipo_doc == 36).ToList();
                else if (intOpcionPlanillaVenta == 2)//SOLO ADELANTOS - ANTICIPOS
                    lstDocPorCobrar = lstDocPorCobrar.Where(x => x.tdocc_icod_tipo_doc == 83).ToList();
            }

            if (flag_multiple)
            {
                gridColumn8.Visible = true;
                lstDocPorCobrar.ForEach(x =>
                {
                    x.flag_multiple = false;
                });
            }
            else
                gridColumn8.Visible = false;

            grdDocxCobrar.DataSource = lstDocPorCobrar;
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void Acceptar()
        {
            EDocPorCobrar = (EDocXCobrar)viewDocxCobrar.GetRow(viewDocxCobrar.FocusedRowHandle);
            
            if (flag_multiple)
            {
                txtNumero.Focus();
                viewDocxCobrar.MoveLast();
                viewDocxCobrar.MoveFirst();
            }

            if (EDocPorCobrar != null)
                this.DialogResult = DialogResult.OK;
        }
        private void viewDocxCobrar_DoubleClick(object sender, EventArgs e)
        {
            if (!flag_multiple)
                Acceptar();
        }
       
        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Acceptar();
        }

        private void txtNumero_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdDocxCobrar.DataSource = lstDocPorCobrar.Where(x => x.doxcc_vnumero_doc.Contains(txtNumero.Text)).ToList();
        }
    }
}