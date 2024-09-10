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

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frm07TablaVentas : DevExpress.XtraEditors.XtraForm
    {
        private List<ETablaVentaCab> lstTabla = new List<ETablaVentaCab>();
        public frm07TablaVentas()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTabla = new BAdministracionSistema().listarTablaVentaCab();
            grdTabla.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.tabvc_iid_tipo_tabla == intIcod);
            viewTabla.FocusedRowHandle = index;
            viewTabla.Focus();
        }
        private void nuevo()
        {
            frmManteTablaVentasCab frm = new frmManteTablaVentasCab();
            frm.MiEvento += new frmManteTablaVentasCab.DelegadoMensaje(reload);
            if (lstTabla.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstTabla.Max(x => Convert.ToInt32(x.tabvc_iid_tipo_tabla) + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstTabla = lstTabla;
            frm.SetInsert();
            frm.Show();

        }
      
        private void modificar()
        {
            ETablaVentaCab Obe = (ETablaVentaCab)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            frmManteTablaVentasCab frm = new frmManteTablaVentasCab();
            frm.MiEvento += new frmManteTablaVentasCab.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();
            
        }
        private void eliminar()
        {
            ETablaVentaCab Obe = (ETablaVentaCab)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {               
                new BAdministracionSistema().eliminarTablaVentaCab(Obe);
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
        private void listarTablaRegistro()
        {
            ETablaVentaCab Obe = (ETablaVentaCab)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;

            frmTablaVentaDet frm = new frmTablaVentaDet();
            frm.intTabla = Obe.tabvc_iid_tipo_tabla;
            frm.Show();
        }
        private void detalleDeTablaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarTablaRegistro();            
        }

        private void btnDetalle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listarTablaRegistro();
        }
        private void buscarCriterio()
        {
            grdTabla.DataSource = lstTabla.Where(obj =>
                                                   obj.tabvc_iid_tipo_tabla.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.tabvc_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void actualizarSepulrurasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<ETablaVentaDet> lstSepulturas = new List<ETablaVentaDet>();
            //lstSepulturas = new BGeneral().listarTablaVentaDet(12);

            //for (int i = 1; i < 241; i++)
            //{
            //    ETablaVentaDet obe = new ETablaVentaDet();
            //    obe.tabvc_iid_tipo_tabla = 12;
            //    obe.tabvd_icorrelativo_venta_det = i;
            //    obe.tabvd_vdescripcion = i.ToString();
            //    obe.tabvd_cestado = 'A';
            //    obe.tabvd_iid_tabla_venta_det = new BAdministracionSistema().insertarTablaVentaDet(obe);
            //}

        }
    }
}