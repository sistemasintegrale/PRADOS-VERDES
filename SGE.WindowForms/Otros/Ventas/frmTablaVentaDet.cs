using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.bVentas;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;
using SGE.WindowForms.Otros.Ventas;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmTablaVentaDet : DevExpress.XtraEditors.XtraForm
    {
        private List<ETablaVentaDet> lstTabla = new List<ETablaVentaDet>();
        public int intTabla = 0;
        public frmTablaVentaDet()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTabla = new BGeneral().listarTablaVentaDet(intTabla);
            grdTablaRegistro.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.tabvd_iid_tabla_venta_det == intIcod);
            viewTablaRegistro.FocusedRowHandle = index;
            viewTablaRegistro.Focus();
        }
        private void nuevo()
        {
            frmManteTablaVentasDet frm = new frmManteTablaVentasDet();
            frm.MiEvento += new frmManteTablaVentasDet.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            frm.SetInsert();
            frm.Show();

        }

        private void modificar()
        {
            ETablaVentaDet Obe = (ETablaVentaDet)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTablaVentasDet frm = new frmManteTablaVentasDet();
            frm.MiEvento += new frmManteTablaVentasDet.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();

        }
        private void eliminar()
        {
            ETablaVentaDet Obe = (ETablaVentaDet)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BAdministracionSistema().eliminarTablaVentaDet(Obe);
                cargar();
            }


        }
        private void imprimir()
        {
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void tablaDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        private void buscarCriterio()
        {
            grdTablaRegistro.DataSource = lstTabla.Where(obj =>
                                                   obj.tabvd_icorrelativo_venta_det.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.tabvd_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void detalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETablaVentaDet Obe = (ETablaVentaDet)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null) return;
            FrmDetalleTableVentaDet frm = new FrmDetalleTableVentaDet();
            frm.Text = $"Detalle de : {Obe.tabvd_vdescripcion}";
            frm.Obe = Obe;
            frm.ShowDialog();
        }

        private void mnuTabla_Opening(object sender, CancelEventArgs e)
        {
            
             
        }
    }
}