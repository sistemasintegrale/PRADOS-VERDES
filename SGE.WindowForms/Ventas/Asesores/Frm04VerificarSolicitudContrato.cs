using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.DataAccess;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Otros.Ventas;
using SGI.WindowsForm.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Asesores
{
    public partial class Frm04VerificarSolicitudContrato : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();

        public Frm04VerificarSolicitudContrato()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {

            BSControls.LoaderLookRepository(lkpgrdEstado, new BGeneral().listarTablaVentaDet(28), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpgridCOdAsesor, new BVentas().listarVendedor(), "vendc_vcod_vendedor", "vendc_icod_vendedor", true);
            cargar();
        }
        private void cargar()
        {
            lstContrato = new BVentas().listarContrato(Parametros.intSolicitud);
            if (Valores.intUsuario != 4)
                lstContrato = lstContrato.Where(x => x.cntc_icod_vendedor == Valores.vendc_icod_vendedor).ToList();
            grdContrato.DataSource = lstContrato;
            buscarCriterio();
            viewContrato.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstContrato.FindIndex(x => x.cntc_icod_contrato == intIcod);
            viewContrato.FocusedRowHandle = index;
            viewContrato.Focus();

        }


        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FrmManteSolicitudContrato frm = new FrmManteSolicitudContrato();
            frm.MiEvento += new FrmManteSolicitudContrato.DelegadoMensaje(reload);
            frm.lstContrato = lstContrato;
            frm.SetInsert();
            frm.Show();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es ANULADO "));

                FrmManteSolicitudContrato frm = new FrmManteSolicitudContrato();
                frm.MiEvento += new FrmManteSolicitudContrato.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstContrato = lstContrato;
                frm.SetModify();
                frm.Show();
                frm.setValues();

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Eliminado, su situación es ANULADO "));


                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarContrato(Obe);
                    cargar();
                    if (lstContrato.Count == 0)
                    {
                        cargar();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewContrato.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewContrato.ClearColumnsFilter();
        }

        private void filtrar()
        {
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vnumero_solicitud"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_solicitud] LIKE '%" + txtNumContrato.Text + "%'");

        }

        private void txtNumContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtNomContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtDNIContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e) { }

        private void viewContrato_DoubleClick(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteSolicitudContrato frm = new FrmManteSolicitudContrato();
            frm.MiEvento += new FrmManteSolicitudContrato.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstContrato = lstContrato;
            frm.SetCancel();
            frm.Show();
            frm.setValues();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void buscarCriterio()
        {
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vnumero_solicitud"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_solicitud] LIKE '%" + txtNumContrato.Text + "%'");
        }



        private void txtDNIContratante_EditValueChanged(object sender, EventArgs e)
        {
            grdContrato.DataSource = lstContrato.Where(x => x.cntc_vdni_contratante.Contains(txtDNIContratante.Text));
            grdContrato.RefreshDataSource();
            grdContrato.Refresh();
        }

        private void txtNumContrato_EditValueChanged(object sender, EventArgs e) { }
        private void txtNomContratante_EditValueChanged(object sender, EventArgs e) { }
        private void mnuContrato_Opening(object sender, CancelEventArgs e) { }
        private void viewContrato_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) { }
        private void viewContrato_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) { }

        private void verificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteSolicitudContrato frm = new FrmManteSolicitudContrato();
            frm.MiEvento += new FrmManteSolicitudContrato.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstContrato = lstContrato;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
            frm.lkpEstadoSolicitud.Enabled = true;
            frm.btnGuardar.Enabled = true;
            frm.SetModify();
        }
    }
}