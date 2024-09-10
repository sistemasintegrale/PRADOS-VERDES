using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmListarContratantes : DevExpress.XtraEditors.XtraForm
    {
        public List<EContratante> lista = new List<EContratante>();
        public int cntc_icod_contrato;
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public FrmListarContratantes()
        {
            InitializeComponent();
        }

        private void FrmListarContratantes_Load(object sender, EventArgs e)
        {
            carga();
            viewContratantes.GroupPanelText = "";
        }
        void carga()
        {
            lista = new BVentas().listarContratantes(cntc_icod_contrato);
            grdContratantes.DataSource = lista;
            grdContratantes.RefreshDataSource();
        }

        private void modiifcarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seleccionar();
        }
        void nuevo()
        {
            FrmManteContratantes frm = new FrmManteContratantes();
            frm.MiEvento += new FrmManteContratantes.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.cntc_icod_contrato = cntc_icod_contrato;
            frm.txtid.Text = lista.Count() != 0 ? (lista.Max(x => x.cntcc_iid_contratante) + 1).ToString() : "1";
            frm.Show();

        }


        void modificar()
        {
            EContratante obj = (EContratante)viewContratantes.GetRow(viewContratantes.FocusedRowHandle);
            if (obj == null)
                return;
            FrmManteContratantes frm = new FrmManteContratantes();
            frm.MiEvento += new FrmManteContratantes.DelegadoMensaje(reload);
            frm.Show();
            frm.SetModify();
            frm.cntc_icod_contrato = cntc_icod_contrato;
            frm.obj = obj;
            frm.setvalues();
        }
        void Eliminar()
        {
            EContratante obj = (EContratante)viewContratantes.GetRow(viewContratantes.FocusedRowHandle);
            if (obj == null)
                return;
            if (obj.cntcc_bflag_seleccion == true)
            {
                XtraMessageBox.Show("No se Puede Eliminar Contratante que esté Seleccionado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            obj.cntcc_iusuario_modifica = Valores.intUsuario;
            obj.cntcc_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            obj.cntcc_sfecha_nacimineto_contratante = Convert.ToDateTime(obj.cntcc_sfecha_nacimineto_contratante).Year == 1 ? (DateTime?)null : obj.cntcc_sfecha_nacimineto_contratante;
            obj.cntcc_bflag_estado = false;
            new BVentas().modificarContratatante(obj);
            carga();
        }

        void reload(int intIcod)
        {
            carga();
            int index = lista.FindIndex(x => x.cntcc_icod_contratante == intIcod);
            viewContratantes.FocusedRowHandle = index;
            viewContratantes.Focus();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        void seleccionar()
        {
            EContratante obj = (EContratante)viewContratantes.GetRow(viewContratantes.FocusedRowHandle);
            if (obj == null)
                return;
            obj.cntcc_iusuario_modifica = Valores.intUsuario;
            obj.cntcc_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            obj.cntcc_bflag_seleccion = true;
            obj.cntcc_sfecha_nacimineto_contratante = Convert.ToDateTime(obj.cntcc_sfecha_nacimineto_contratante).Year == 1 ? (DateTime?)null : obj.cntcc_sfecha_nacimineto_contratante;
            obj.cntcc_bflag_estado = true;
            new BVentas().modificarContratatante(obj);
            carga();
        }

        private void FrmListarContratantes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento(cntc_icod_contrato);
        }
    }
}