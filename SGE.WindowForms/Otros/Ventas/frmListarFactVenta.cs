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

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmListarFactVenta : DevExpress.XtraEditors.XtraForm
    {
        List<EFacturaCab> lstFactura = new List<EFacturaCab>();
        public EFacturaCab _Be { get; set; }
        public int favc_icod_cliente = 0;
        public frmListarFactVenta()
        {
            InitializeComponent();
        }
        public void cargar()
        {
            lstFactura = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_icod_cliente == favc_icod_cliente).ToList();
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
                _Be = (EFacturaCab)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstFactura.Where(x =>
                                                   x.favc_icod_factura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.cliec_vnombre_cliente.Contains(txtDescripcion.Text.ToUpper())
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