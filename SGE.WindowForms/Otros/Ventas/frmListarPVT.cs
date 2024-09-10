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
    public partial class frmListarPVT : DevExpress.XtraEditors.XtraForm
    {
        List<EPedidosPVT> lstPedidosPVT = new List<EPedidosPVT>();
        public EPedidosPVT _Be { get; set; }
        public int intCategoria = 0;
        public frmListarPVT()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstPedidosPVT = new BVentas().listarPedidosPVT();
            grdPVT.DataSource = lstPedidosPVT;
        }        
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstPedidosPVT.Count > 0)
            {
                _Be = (EPedidosPVT)viewPVT.GetRow(viewPVT.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            //grdPVT.DataSource = lstPedidosPVT.Where(x =>
            //                                       x.famic_vabreviatura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
            //                                       x.famic_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
    }
}