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

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmConsultaKardexValorizadoPorFecAlmProd : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultaKardexPorFecAlmProd));
        List<EKardexValorizado> lstKardex = new List<EKardexValorizado>();
        public DateTime f1, f2;
        public int intAlmacen, intProducto;
        public frmConsultaKardexValorizadoPorFecAlmProd()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            buscar();
        }   

        private void buscar()
        {            
            try
            {              
                //lstKardex = new BAlmacen().listarKardexValorizadoPorFechaAlmacenProducto(f1, f2, intAlmacen, intProducto,Parametros.intEjercicio);
                lstKardex = new BAlmacen().ListarKardexValorizadoInventario(intAlmacen, intProducto, f1, f2);
                grdKardex.DataSource = lstKardex;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}