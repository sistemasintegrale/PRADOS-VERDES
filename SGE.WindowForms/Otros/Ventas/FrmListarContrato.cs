using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmListarContrato : XtraForm
    {

        private List<EContrato> lstContrato = new List<EContrato>();
        public bool ingresaCliente = false;
        public string nombreCliente;
        public bool simple = false;
        public EContrato _Be = new EContrato();
        private void txtNroContrato_KeyUp(object sender, KeyEventArgs e) => filtrar();
        private void txtNroContrato_EditValueChanged(object sender, EventArgs e) => filtrar();
        private void txtContratante_EditValueChanged(object sender, EventArgs e) => filtrar();
        private void txtDNI_EditValueChanged(object sender, EventArgs e) => filtrar();
        private void btnBuscar_Click(object sender, EventArgs e) => cargar();
        private void simpleButton1_Click(object sender, EventArgs e) => filtrar();
        public FrmListarContrato() => InitializeComponent();
        private void gridView1_DoubleClick(object sender, EventArgs e) => DgAcept();

        private void DgAcept()
        {
            _Be = (EContrato)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }


        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            if (simple)
                cargar();
            filtrar();
        }

        private void cargar()
        {
            if (!simple)
            {
             
                lstContrato = new BVentas().ContratosListarPorNumeroContratanteDni(txtNroContrato.Text, txtContratante.Text, txtDNI.Text);
            }
            else
            {
                lstContrato = new BVentas().listarContratoSimple(Parametros.intContrato, 332).ToList();
                btnBuscar.Visible = false;
            }

            grd.DataSource = lstContrato;
            grd.RefreshDataSource();

        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstContrato.FindIndex(x => x.cntc_icod_contrato == intIcod);
            viewCliente.FocusedRowHandle = index;
            viewCliente.Focus();
        }

        private void btnsalir_ItemClick(object sender, ItemClickEventArgs e) => this.Close();

        private void btnAceptar_ItemClick(object sender, ItemClickEventArgs e) => DgAcept();

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMantePreContratoCaja frm = new frmMantePreContratoCaja();
            frm.MiEvento += new frmMantePreContratoCaja.DelegadoMensaje(reload);
            frm.lstContrato = lstContrato;
            frm.txtSerie.Text = new BVentas().listarRegistroParametro().FirstOrDefault().rgpmc_vserie_contrato;
            frm.txtSerie.Focus();
            frm.txtSerie.Properties.ReadOnly = false;
            frm.SetInsert();
            frm.Show();
            frm.lkpNombreIEC.Enabled = true;
            frm.txtSerie.Focus();
            frm.Obe.cntc_vorigen_registro = "CAJA";
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (Obe == null) return;

            Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);

            if (Obe.cntc_icod_situacion == 332)//ANULADO
            {
                Services.MessageError("El Contrato no puede ser Modificado, su situación es ANULADO ");
                return;
            }

            frmMantePreContratoCaja frm = new frmMantePreContratoCaja();
            frm.MiEvento += new frmMantePreContratoCaja.DelegadoMensaje(reload);
            frm.Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            frm.lstContrato = lstContrato;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtSerie.Focus();
             
        }

        private void filtrar()
        {
            if (simple)
            {
                viewCliente.Columns["cntc_vnumero_contrato"].FilterInfo = new ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNroContrato.Text + "%'");
                viewCliente.Columns["strNombreCompleto"].FilterInfo = new ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtContratante.Text + "%'");
                viewCliente.Columns["cntc_vdni_contratante"].FilterInfo = new ColumnFilterInfo("[cntc_vdni_contratante] LIKE  '%" + txtDNI.Text + "%'");
            }

        }
       
    }
}