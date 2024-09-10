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
using SGE.WindowForms.Reportes.Almacen.Consultas;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarProdPrecioDetalle : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListrStockPorAlmacen));
        List<EDocCompraDet> lstProdPrecioDet = new List<EDocCompraDet>();
                
        public int intProducto = 0;

        public frmListarProdPrecioDetalle()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }
    
        private void cargar() 
        {
            lstProdPrecioDet = new BAlmacen().listarProductoPreciosDetalle(intProducto, Parametros.intEjercicio);
            
            grdCompras.DataSource = lstProdPrecioDet;
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}