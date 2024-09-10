using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.DataAccess;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes_Cuotas
{
    public partial class frm07ConsultaContratosPorTipoDePago : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();

        public frm07ConsultaContratosPorTipoDePago()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            var lstTipoPago = new BGeneral().listarTablaRegistro(97).ToList();
            ETablaRegistro eTablaRegistro = new ETablaRegistro()
            {
                tarec_vdescripcion = "TODOS...",
                tarec_iid_tabla_registro = 0
            };
            lstTipoPago.Insert(0, eTablaRegistro);
            BSControls.LoaderLook(lkpTipoPago, lstTipoPago, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLookRepository(lkpgGrdTipoPago, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();
        }

        private void cargar()
        {
            lstContrato = new BVentas().listarContrato(Parametros.intContrato);
            buscarCriterio();
            grdContrato.DataSource = lstContrato;
            viewContrato.Focus();
        }

        void reload(int intIcod)
        {
            cargar();

            int index = lstContrato.FindIndex(x => x.cntc_icod_contrato == intIcod);
            viewContrato.FocusedRowHandle = index;
            viewContrato.Focus();

        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewContrato.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewContrato.ClearColumnsFilter();
        }

        private void filtrar()
        {
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
            if (Convert.ToInt32(lkpTipoPago.EditValue) != 0)
                viewContrato.Columns["cntc_itipo_pago"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_itipo_pago] LIKE '%" + Convert.ToInt32(lkpTipoPago.EditValue) + "%'");
            else
                viewContrato.Columns["cntc_itipo_pago"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_itipo_pago] LIKE '%" + "" + "%'");
        }

        private void txtNumContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtNomContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void viewContrato_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

            }
        }


        private void txtDNIContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            EContrato obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_flag_verificacion == true)
            {
                obe.cntc_flag_verificacion = false;
                new BVentas().modificarContratoVerificacion(obe);
                cargar();
            }
            else
            {
                obe.cntc_flag_verificacion = true;
                new BVentas().modificarContratoVerificacion(obe);
                cargar();
            }
        }

        private void viewContrato_DoubleClick(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es ANULADO "));

                frmMantePreContrato frm = new frmMantePreContrato();
                frm.MiEvento += new frmMantePreContrato.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstContrato = lstContrato;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
                frm.txtSerie.Focus();
                frm.btnEspacios.Enabled = false;
                frm.btnLimpiar.Enabled = true;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewContrato_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.cntc_sfecha_cuota == Convert.ToDateTime("01/01/0001"))
                {
                    Obe.cntc_sfecha_cuota = (DateTime?)null;
                }
                if (Obe.cntc_sfecha_nacimineto_contratante == Convert.ToDateTime("01/01/0001"))
                {
                    Obe.cntc_sfecha_nacimineto_contratante = (DateTime?)null;
                }

                if (Obe.cntc_sfecha_nacimiento_representante == Convert.ToDateTime("01/01/0001"))
                {
                    Obe.cntc_sfecha_nacimiento_representante = (DateTime?)null;
                }

                new VentasData().modificarContrato(Obe);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }



        private void buscarCriterio()
        {
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
        }


        private void txtDNIContratante_EditValueChanged(object sender, EventArgs e)
        {
            grdContrato.DataSource = lstContrato.Where(x => x.cntc_vdni_contratante.Contains(txtDNIContratante.Text));
            grdContrato.RefreshDataSource();
            grdContrato.Refresh();
        }

        private void txtNumContrato_EditValueChanged(object sender, EventArgs e) { }


        private void txtNomContratante_EditValueChanged(object sender, EventArgs e) { }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (saveFileDialog.FileName.Contains(".xlsx"))
                {
                    grdContrato.ExportToXlsx(saveFileDialog.FileName);
                    Process.Start(saveFileDialog.FileName);
                }
                else
                {
                    grdContrato.ExportToXlsx(saveFileDialog.FileName + ".xlsx");
                    Process.Start(saveFileDialog.FileName + ".xlsx");
                }
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            frmMantePreContrato frm = new frmMantePreContrato();
            frm.MiEvento += new frmMantePreContrato.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstContrato = lstContrato;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
            frm.txtSerie.Focus();
            frm.btnEspacios.Enabled = false;
            frm.btnLimpiar.Enabled = true;
            frm.deshabilitarCaja();
            frm.deshabilitarCobranzas();
            frm.txtFoma.Enabled = false;
            frm.txtFinanciamiento.Enabled = false;
        }

        private void grdContrato_Click(object sender, EventArgs e)
        {

        }
    }
}