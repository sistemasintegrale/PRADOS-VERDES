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
using SGE.WindowForms.Otros.bVentas;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Ventas.Caja
{
    public partial class Frm12PagosFomaFinanciamiento : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();
        public Frm12PagosFomaFinanciamiento()
        {
            InitializeComponent();
        }

        private void pagarFomaFinanciamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmMantePagoFomaMantenimiento frm = new FrmMantePagoFomaMantenimiento();
            frm.Text = string.Format("Foma/Financiamiento del Contrato : {0}", Obe.cntc_vnumero_contrato);
            frm.Obe = Obe;
            frm.cargarControles();
            frm.cargaLista();
            frm.ShowDialog();
        }

        private void Frm12PagosFomaFinanciamiento_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstContrato = new BVentas().listarContrato(Parametros.intContrato);
                        buscarCriterio();
            grdContrato.DataSource = lstContrato;
            viewContrato.Focus();
        }

        private void buscarCriterio()
        {

            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
            // viewContrato.Columns["cntc_vdocumento_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdocumento_contratante] LIKE '%" + txtDNIContratante.Text + "%'");


        }

        private void txtDNIContratante_EditValueChanged(object sender, EventArgs e)
        {
            grdContrato.DataSource = lstContrato.Where(x => x.cntc_vdni_contratante.Contains(txtDNIContratante.Text));
            grdContrato.RefreshDataSource();
            grdContrato.Refresh();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewContrato.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewContrato.ClearColumnsFilter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void txtNomContratante_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }

        private void txtNumContrato_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }
    }
}