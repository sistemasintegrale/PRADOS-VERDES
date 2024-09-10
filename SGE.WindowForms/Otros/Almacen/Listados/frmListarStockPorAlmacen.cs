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
    public partial class frmListarStockPorAlmacen : DevExpress.XtraEditors.XtraForm
    {
        List<EStock> lstStockProductos = new List<EStock>();
        public EStock _Be { get; set; }
        public int intAlmacen = 0;
        public Boolean afecto ;
        public Boolean intClasProd;
        public frmListarStockPorAlmacen()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            if (intClasProd == true)
            {
                lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, intAlmacen, "", "").Where(x => x.clasc_icod_clasificacion == 4 || x.clasc_icod_clasificacion == 6 || x.clasc_icod_clasificacion == 5 || x.clasc_icod_clasificacion == 8).ToList();
                lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, intAlmacen, "", "").ToList();
            }
            else
            {
                lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, intAlmacen, "", "");
            }           
            grdStock.DataSource = lstStockProductos;
            viewStock.Focus();
        }       
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstStockProductos.Count > 0)
            {
                _Be = (EStock)viewStock.GetRow(viewStock.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            //grdStock.DataSource = new BAlmacen().listarStockPorAlmacenOptimizadoAfecto(Parametros.intEjercicio, intAlmacen, txtCodigo.Text.Trim(), txtDescripcion.Text.Trim(),afecto);
            grdStock.DataSource = lstStockProductos.Where(x =>
                                                   x.strCodProducto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strDesProducto.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
       

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewStock.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewStock.ClearColumnsFilter();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {

        }     
    }
}