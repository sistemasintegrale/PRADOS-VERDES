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

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarProducto : DevExpress.XtraEditors.XtraForm
    {
        List<EProducto> lstProductos = new List<EProducto>();
        public EProducto _Be { get; set; }
        public bool flag_solo_prods = false;
        public Boolean flag_producto_terminado;//Indicador para filtrar Producto Terminado (true)
        public Boolean flag_sub_producto;//Indicador para filtrar SubProducto (true)
        public frmListarProducto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();            
        }       
        
        private void cargar()
        {
            //if (flag_solo_prods)
            //    lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion && x.tarec_icorrelativo_registro_tipo != 3).ToList();
            //else
            if (flag_producto_terminado == true)//Productos Terminados
            {
                lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion && x.clasc_icod_clasificacion == 7).ToList();
            }
            else if (flag_sub_producto == true)
            {
                lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion && x.clasc_icod_clasificacion == 2).ToList();
            }
            else
            {
                lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            }
            

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
      
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstProductos.Count > 0)
            {
                _Be = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
       
        private void buscarCriterio()
        {
            if (txtCodigo.Text == "" && txtDescripcion.Text == "")
            {
                lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion).ToList();
            }
            else
            {
                lstProductos = new BAlmacen().listarProductoCodigoDescrip(Parametros.intEjercicio,txtCodigo.Text,txtDescripcion.Text);
            }
            grdProducto.DataSource = lstProductos;
            grdProducto.RefreshDataSource();
        }

        

        

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void nuevo()
        {
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.MiEvento += new frmManteProductoAntiguo.DelegadoMensaje(reload);
            frm.lstProductos = lstProductos;
            frm.SetInsert();
            frm.Show();

        }
        private void modificar()
        {
            EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.MiEvento += new frmManteProductoAntiguo.DelegadoMensaje(reload);
            frm.lstProductos = lstProductos;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewProducto.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewProducto.ClearColumnsFilter();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            buscarCriterio();
        }
    }
}