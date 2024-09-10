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

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListarOCLDetalle : DevExpress.XtraEditors.XtraForm
    {
        List<EOrdenCompra> lstHojaOCL = new List<EOrdenCompra>();
        public EOrdenCompra _Be { get; set; }
        public int CodOC = 0;
        public frmListarOCLDetalle()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {

            lstHojaOCL = new BCompras().ListarOrdenCompraDetalleFC(CodOC).Where(x =>  x.prdc_icod_producto > 0 || x.ocod_ncantidad_saldo > 0).ToList();
            grdRubros.DataSource = lstHojaOCL;
            viewRubros.Focus();

        }        
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstHojaOCL.Count > 0)
            {
                _Be = (EOrdenCompra)viewRubros.GetRow(viewRubros.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            //grdRubros.DataSource = lstHojaOCL.Where(x =>
            //                                       x.hjcd3_vcodigo_concepto_hc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
            //                                       x.hjcd3_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
            //                                 ).ToList();
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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }     
    }
}