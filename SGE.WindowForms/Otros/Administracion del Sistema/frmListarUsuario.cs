using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmListarUsuario : DevExpress.XtraEditors.XtraForm
    {
        private List<EUsuario> lstUsuarios = new List<EUsuario>();
        public EUsuario _Be { get; set; }
        public frmListarUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstUsuarios = new BAdministracionSistema().listarUsuarios();
            lstUsuarios.OrderBy(x=> x.usua_codigo_usuario);
            grdUsuario.DataSource = lstUsuarios;
        }

        private void returnSeleccion()
        {
            if (lstUsuarios.Count > 0)
            {
                _Be = (EUsuario)viewUsuario.GetRow(viewUsuario.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
        private void viewUsuario_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
        private void buscarCriterio()
        {
            grdUsuario.DataSource = lstUsuarios.Where(x =>
                                                    x.usua_codigo_usuario.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.usua_nombre_usuario.Contains(txtDescripcion.Text.ToUpper())
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
    }
}