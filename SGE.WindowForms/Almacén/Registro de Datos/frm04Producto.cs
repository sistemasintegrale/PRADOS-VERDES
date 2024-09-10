using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Otros.Almacen.Listados;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm04Producto : DevExpress.XtraEditors.XtraForm
    {
        List<EProducto> lstProductos = new List<EProducto>();
        public frm04Producto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio);
            grdProducto.DataSource = lstProductos;
            viewProducto.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstProductos.FindIndex(x => x.prdc_icod_producto == intIcod);
            viewProducto.FocusedRowHandle = index;
            viewProducto.Focus();   
        }        
        private void nuevo()
        {
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.MiEvento += new frmManteProductoAntiguo.DelegadoMensaje(reload);          
            frm.lstProductos = lstProductos;
            frm.SetInsert();
            frm.Show();
            //frm.groupBox1.Enabled = false;
            //frm.txtCorrelativo.Focus();
            
            
        }
        private void modificar()
        {
            EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.MiEvento += new frmManteProductoAntiguo.DelegadoMensaje(reload);
            frm.lstProductos = lstProductos;
            frm.Indicador = 1;
            frm.prdc_icod_producto = Obe.prdc_icod_producto;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();           
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();      
        }
        private void eliminar()
        {
            try
            {
                EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewProducto.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarProducto(Obe);
                    cargar();
                    if (lstProductos.Count >= index + 1)
                        viewProducto.FocusedRowHandle = index;
                    else
                        viewProducto.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            if (lstProductos.Count > 0)
            {
                rpt001RegistroProducto rpt = new rpt001RegistroProducto();
                rpt.cargar(lstProductos.Where(x =>
                                                   x.prdc_vcode_producto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.prdc_vdescripcion_larga.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList());
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        
        private void buscarCriterio()
        {
            //if (txtCodigo.Text == "" && txtDescripcion.Text == "")
            //{
            //    lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            //}
            //else
            //{
            //    lstProductos = new BAlmacen().listarProductoCodigoDescrip(Parametros.intEjercicio, txtCodigo.Text, txtDescripcion.Text);
            //}
            //grdProducto.DataSource = lstProductos;
            //grdProducto.RefreshDataSource();

            grdProducto.DataSource = lstProductos.Where(x =>
                                                  x.prdc_vcode_producto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                  x.prdc_vdescripcion_larga.Contains(txtDescripcion.Text.ToUpper())
                                            ).ToList();
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

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewProducto.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewProducto.ClearColumnsFilter();
        }

    }
}