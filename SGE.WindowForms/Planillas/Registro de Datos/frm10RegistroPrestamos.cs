using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Planillas;
using SGE.WindowForms.Reportes.Planilla;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using DevExpress.XtraBars;


namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class frm10RegistroPrestamos : XtraForm
    {
        List<EPrestamo> lstAreas = new List<EPrestamo>();
        public frm10RegistroPrestamos() => InitializeComponent();
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e) => Imprimir();
        private void frmAlamcen_Load(object sender, EventArgs e) => cargar();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => modificar();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => eliminar();
        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e) => nuevo();
        private void btnModificar_ItemClick(object sender, ItemClickEventArgs e) => modificar();
        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e) => eliminar();
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e) => buscarCriterio();
        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e) => buscarCriterio();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => nuevo();
        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e) => Imprimir();
        private void cargar()
        {
            lstAreas = new BPlanillas().ListarPrestamosPersonal();
            grdPrestamo.DataSource = lstAreas;
            viewPrestamo.Focus();
        }

        public void reload(int intIcod)
        {
            cargar();
            int index = lstAreas.FindIndex(x => x.prtpc_icod_prestamo == intIcod);
            viewPrestamo.FocusedRowHandle = index;
            viewPrestamo.Focus();
        }
        private void nuevo()
        {
            frmMantePrestamoPersonal frm = new frmMantePrestamoPersonal();
            frm.MiEvento += new frmMantePrestamoPersonal.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.txtMontoCuotas.Focus();
            frm.Show();
        }

        private void modificar()
        {
            EPrestamo Obe = (EPrestamo)viewPrestamo.GetRow(viewPrestamo.FocusedRowHandle);
            if (Obe == null) return;
            frmMantePrestamoPersonal frm = new frmMantePrestamoPersonal();
            frm.MiEvento += new frmMantePrestamoPersonal.DelegadoMensaje(reload);
            frm.ObeC = Obe;
            frm.SetModify();
            frm.setValues();
            frm.Show();
        }

        private void eliminar()
        {
            EPrestamo Obe = (EPrestamo)viewPrestamo.GetRow(viewPrestamo.FocusedRowHandle);
            if (Obe == null) return;
            int index = viewPrestamo.FocusedRowHandle;
            var list = new BPlanillas().ListarPrestamoCuotasPersonal(Obe.prtpc_icod_prestamo);
            if (list.Exists(x => x.prtpd_icod_situacion == 348))
            {
                XtraMessageBox.Show("No se Puede Eliminar Préstamo " + Obe.prtpc_vnumero_prestamo + ", ya se Encuentra con Pagos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el Préstamo " + Obe.prtpc_vnumero_prestamo + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Obe.prtpd_iusuario_elimina = Valores.intUsuario;
            Obe.prtpd_vpc_elimina = WindowsIdentity.GetCurrent().Name;
            new BPlanillas().EliminarPrestamoPersonal(Obe);
            cargar();
        }


        private void buscarCriterio()
        {
            grdPrestamo.DataSource = lstAreas.Where(x =>
                                                   x.prtpc_vnumero_prestamo.Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strNombrePersonal.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void viewAlmacen_DoubleClick(object sender, EventArgs e)
        {
            EPrestamo Obe = (EPrestamo)viewPrestamo.GetRow(viewPrestamo.FocusedRowHandle);
            if (Obe == null) return;
            frmMantePrestamoPersonal frm = new frmMantePrestamoPersonal();
            frm.ObeC = Obe;
            frm.SetCancel();
            frm.setValues();
            frm.Show();
        }
        private void Imprimir()
        {
            EPrestamo Obe = (EPrestamo)viewPrestamo.GetRow(viewPrestamo.FocusedRowHandle);
            if (Obe == null) return;
            var lstDetalle = new BPlanillas().ListarPrestamoCuotasPersonal(Obe.prtpc_icod_prestamo);
            rptPrestamo rpt = new rptPrestamo();
            rpt.Cargar(Obe, lstDetalle);
        }

        
    }
}