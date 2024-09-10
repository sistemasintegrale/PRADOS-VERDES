using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Reportes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Caja
{
    public partial class frm08PlanillaVentaDiaria : XtraForm
    {
        List<EPlanillaCobranzaCab> lstPlanillas = new List<EPlanillaCobranzaCab>();
        public frm08PlanillaVentaDiaria()
        {
            InitializeComponent();
        }
        private void frm08PlanillaCobranza_Load(object sender, EventArgs e) => cargar();
        private void cargar()
        {
            lstPlanillas = new BVentas().listarPlanillaCobranzaCab(Parametros.intEjercicio);
            grdPlanilla.DataSource = lstPlanillas;
        }
        private void reload(int intIcod)
        {
            cargar();
            int index = lstPlanillas.FindIndex(x => x.plnc_icod_planilla == intIcod);
            viewPlanilla.FocusedRowHandle = index;
            viewPlanilla.Focus();
        }
        private void nuevo()
        {
            frmMantePlanillaCab frm = new frmMantePlanillaCab();
            frm.MiEvento += new frmMantePlanillaCab.DelegadoMensaje(reload);
            frm.ObePlnCab.intTipoOperacion = 1;
            frm.txtPlanilla.Text = String.Format("{0:00000}", Convert.ToInt32(lstPlanillas.Max(x => x.plnc_vnumero_planilla)) + 1);
            frm.SetInsert();
            frm.ShowDialog();
        }
        private void modificar()
        {
            EPlanillaCobranzaCab oBe = (EPlanillaCobranzaCab)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            if (oBe.tblc_iid_situacion == 2)
            {
                XtraMessageBox.Show("La Planilla de Venta esta CERRADA, no puede ser modificada", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmMantePlanillaCab frm = new frmMantePlanillaCab();
            frm.MiEvento += new frmMantePlanillaCab.DelegadoMensaje(reload);
            oBe.intTipoOperacion = 2;
            frm.ObePlnCab = oBe;
            frm.SetModify();
            frm.setValues();
            frm.Show();
        }

        

        private void eliminar()
        {
            EPlanillaCobranzaCab oBe = (EPlanillaCobranzaCab)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            
            if (new BVentas().listarPlanillaCobranzaDetalle(oBe.plnc_icod_planilla).Any())
            {
                Services.MessageError("La Planilla ya tiene documentos, no se puede eliminar");
                return;
            }
            if (Services.MessageQuestion("¿Esta seguro que desea eliminar el registro?") == DialogResult.No)
                return;
            new BVentas().eliminarPlanillaCab(oBe);
            cargar();
        }

        private void filtrar()
        {
            grdPlanilla.DataSource = lstPlanillas.Where(x => x.plnc_vnumero_planilla.Contains(txtPlanilla.Text)
              && x.plnc_vobservaciones.Contains(txtDescripcion.Text.ToUpper())).ToList();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => modificar();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e) => nuevo();
        private void btnModificar_ItemClick(object sender, ItemClickEventArgs e) => modificar();
        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e) => eliminar();
        private void txtPlanilla_KeyUp(object sender, KeyEventArgs e) => filtrar();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaCab oBe = (EPlanillaCobranzaCab)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            var lstPlanillaDet = new BVentas().listarPlanillaCobranzaImpresionDetalle(oBe.plnc_icod_planilla);
            rpt01PlanillaDetalle rpt = new rpt01PlanillaDetalle();
            rpt.cargar(lstPlanillaDet, oBe.plnc_vnumero_planilla, oBe.plnc_sfecha_planilla.ToShortDateString());

        }
        private void cerrarPVDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaCab oBe = (EPlanillaCobranzaCab)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            if (oBe.tblc_iid_situacion == 2)
            {
                XtraMessageBox.Show("La Planilla de Venta esta CERRADA", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmCerrarPlanilla frm = new frmCerrarPlanilla();
            frm.MiEvento += new frmCerrarPlanilla.DelegadoMensaje(reload);
            frm.oBePlnCab = oBe;
            frm.ShowDialog();
        }
        private void revertirCierreDePVDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaCab oBe = (EPlanillaCobranzaCab)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            if (oBe.tblc_iid_situacion == 2)
            {
                if (XtraMessageBox.Show("¿Esta seguro que desea REVERTIR el cierre de la planilla?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    new BVentas().revertirCierre(oBe.plnc_icod_planilla);
                    reload(oBe.plnc_icod_planilla);

                }
            }
            else
            {
                XtraMessageBox.Show("La Planilla de Venta esta GENERADA", "Información del Sistema", MessageBoxButtons.OK);
            }
        }

        private void viewPlanilla_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strSituacion"]);
                if (strSituacion == "CERRADO")
                {
                    e.Appearance.BackColor = Color.LightGreen;

                }
            }
        }

        private void viewPlanilla_DoubleClick(object sender, EventArgs e)
        {
            EPlanillaCobranzaCab oBe = (EPlanillaCobranzaCab)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            frmMantePlanillaCab frm = new frmMantePlanillaCab();
            frm.MiEvento += new frmMantePlanillaCab.DelegadoMensaje(reload);
            oBe.intTipoOperacion = 2;
            frm.ObePlnCab = oBe;
            frm.SetCancel();
            frm.setValues();
            frm.Show();
            frm.mnu.Enabled = false;
        }


        private void procesoDeActualizacionDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var _be in lstPlanillas)
            {
                List<EPlanillaCobranzaDet> lstPlanillasDetalle = new BVentas().listarPlanillaCobranzaDetalle(_be.plnc_icod_planilla);
                foreach (var _det in lstPlanillasDetalle)
                {
                    if (_det.tablc_iid_tipo_mov == 1)
                    {
                        List<EPagoDocVenta> lstPagos =
                         new BVentas().listarPago(Convert.ToInt32(_det.plnd_icod_tipo_doc), Convert.ToInt32(_det.plnd_icod_documento));
                        foreach (var _pag in lstPagos)
                        {
                            if (_pag.pgoc_tipo_pago == 2)
                            {
                                //new BVentas().ActualizarPagos(_pag);

                            }
                        }
                    }
                }
            }
        }
    }
}