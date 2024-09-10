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
    public partial class frmListarCategoriaFamilia : DevExpress.XtraEditors.XtraForm
    {
        List<ECategoriaFamilia> lstCategoriaFamilia = new List<ECategoriaFamilia>();
        public ECategoriaFamilia _Be { get; set; }
        public frmListarCategoriaFamilia()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstCategoriaFamilia = new BAlmacen().listarCategoriaFamilia();
            grdCategoria.DataSource = lstCategoriaFamilia;
            viewCategoria.Focus();
        }        
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstCategoriaFamilia.Count > 0)
            {
                _Be = (ECategoriaFamilia)viewCategoria.GetRow(viewCategoria.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            grdCategoria.DataSource = lstCategoriaFamilia.Where(x =>
                                                   x.catf_iid_categoria.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.catf_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
    }
}