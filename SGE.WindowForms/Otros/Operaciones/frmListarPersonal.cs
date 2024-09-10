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
using SGE.WindowForms.Otros.Operaciones;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmListarPersonal : DevExpress.XtraEditors.XtraForm
    {
        List<EPersonal> lstPersonal = new List<EPersonal>();
        public EPersonal _Be { get; set; }
        public bool flag_personal_all = false;

        public frmListarPersonal()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            if (flag_personal_all)
                lstPersonal = new BOperaciones().listarPersonal();
            else
                lstPersonal = new BOperaciones().listarPersonal().Where(x => x.perc_flag_comprador == true).ToList();
            grdPersonal.DataSource = lstPersonal;
            viewPersonal.Focus();
        }        
        void reload(int intIcod)
        {
            cargar();
            int index = lstPersonal.FindIndex(x => x.perc_icod_personal == intIcod);
            viewPersonal.FocusedRowHandle = index;
            viewPersonal.Focus();   
        }      
        private void nuevo()
        {
            frmMantePersonal frm = new frmMantePersonal();
            frm.MiEvento += new frmMantePersonal.DelegadoMensaje(reload);            
            if (lstPersonal.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:0000}", lstPersonal.Max(x => x.perc_iid_personal + 1));
            else
                frm.txtCodigo.Text = "0001";
            frm.lstPersonal = lstPersonal;
            frm.SetInsert();
            frm.Show();
            frm.txtApellidoNombres.Focus();
        }
        private void modificar()
        {
            EPersonal Obe = (EPersonal)viewPersonal.GetRow(viewPersonal.FocusedRowHandle);
            if (Obe == null)
                return;
            frmMantePersonal frm = new frmMantePersonal();
            frm.MiEvento += new frmMantePersonal.DelegadoMensaje(reload);
            frm.lstPersonal = lstPersonal;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtApellidoNombres.Focus();
        }
        private void returnSeleccion()
        {
            if (lstPersonal.Count > 0)
            {
                _Be = (EPersonal)viewPersonal.GetRow(viewPersonal.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }              
        private void viewPersonal_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void filtrar()
        {
            if (lstPersonal.Count > 0)
            {
                grdPersonal.DataSource = lstPersonal.Where(x => x.perc_iid_personal.ToString().Contains(txtCodigo.Text)
                    && x.perc_vapellidos_nombres.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
            }
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }        
    }
}