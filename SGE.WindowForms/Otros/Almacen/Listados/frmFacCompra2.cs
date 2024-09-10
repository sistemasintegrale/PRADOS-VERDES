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
    public partial class frmFacCompra2 : DevExpress.XtraEditors.XtraForm
    {
        List<EFacturaCompra> lstFactura = new List<EFacturaCompra>();
        public EFacturaCompra _Be { get; set; }
        public frmFacCompra2()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            lstFactura = new BCompras().listarFacCompra(Parametros.intEjercicio).Where(ob => ob.fcoc_iestado_recep == 273 & ob.fcoc_flag_importacion == true).ToList();
            //lstFactura = new BCompras().listarFacCompra(Parametros.intEjercicio);
            grdAlmacen.DataSource = lstFactura;
            viewAlmacen.Focus();
        }        
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstFactura.Count > 0)
            {
                _Be = (EFacturaCompra)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstFactura.Where(x =>
                                                   x.fcoc_icod_doc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strProveedor.Contains(txtDescripcion.Text.ToUpper())
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

        private void frmFacCompra2_Load_1(object sender, EventArgs e)
        {
            cargar();
        }     
    }
}