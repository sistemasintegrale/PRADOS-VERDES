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
    public partial class frmListarCodigoBarra : DevExpress.XtraEditors.XtraForm
    {
        public List<ECodigoBarra> lstCodigoBarra = new List<ECodigoBarra>();
        public List<ECodigoBarra> lstDeleteCodigoBarra = new List<ECodigoBarra>();
        public ECodigoBarra _Be { get; set; }
        public int prdc_icod_producto = 0;
        public int Indicador = 0;
        public frmListarCodigoBarra()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            //lstCodigoBarra = new BAlmacen().listarCodigoBarra(prdc_icod_producto);
            grdCodigoBarra.DataSource = lstCodigoBarra.Where(q=>q.prdc_icod_producto==prdc_icod_producto).ToList();
            viewCodigoBarra.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstCodigoBarra.FindIndex(x => x.codb_icod_codigo_barra == intIcod);
            viewCodigoBarra.FocusedRowHandle = index;
            viewCodigoBarra.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        public void returnSeleccion()
        {
            if (lstCodigoBarra.Count > 0)
            {
                _Be = (ECodigoBarra)viewCodigoBarra.GetRow(viewCodigoBarra.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            grdCodigoBarra.DataSource = lstCodigoBarra.Where(x =>
                                                   x.codb_iid_codigo_barra.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.DesProducto.Contains(txtDescripcion.Text.ToUpper())
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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (frmManteCodigoBarra frm = new frmManteCodigoBarra())
            {
                frm.lstCodigoBarra = lstCodigoBarra;
                frm.Indicador = Indicador;
                frm.prdc_icod_producto = prdc_icod_producto;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstCodigoBarra = frm.lstCodigoBarra;
                    grdCodigoBarra.DataSource = lstCodigoBarra;
                    viewCodigoBarra.RefreshData();
                }
            }  
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECodigoBarra Obe = (ECodigoBarra)viewCodigoBarra.GetRow(viewCodigoBarra.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int index = viewCodigoBarra.FocusedRowHandle;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                lstDeleteCodigoBarra.Add(Obe);
                lstCodigoBarra.Remove(Obe);
                viewCodigoBarra.RefreshData();
                viewCodigoBarra.FocusedRowHandle = index;
                viewCodigoBarra.Focus();
                //Obe.intUsuario = Valores.intUsuario;
                //Obe.strPc = WindowsIdentity.GetCurrent().Name;
                //new BAlmacen().eliminarCodigoBarra(Obe);
                //cargar();
            }
        }
   
    }
}