using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteCertificadoUsoPerpetuo : DevExpress.XtraEditors.XtraForm
    {
        public EContrato obj = new EContrato();
        public List<ECertificadoUsoPerpetuo> lista = new List<ECertificadoUsoPerpetuo>();
        public FrmManteCertificadoUsoPerpetuo() => InitializeComponent();
        private void FrmManteCertificadoUsoPerpetuo_Load(object sender, EventArgs e) => Cargar();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => Nuevo();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Modificar();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => Eliminar();
        private void simpleButton1_Click(object sender, EventArgs e) => refresh();
        public void Cargar()
        {
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit1, new BGeneral().listarTablaRegistro(101), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);

            lista = new BVentas().CetificadoUsoPerpetuolistar(obj.cntc_icod_contrato);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }
        public void Reload(int icod)
        {
            Cargar();
            int index = lista.FindIndex(x => x.cup_icod == icod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }
        public void Nuevo()
        {
            FrmCertificadoPerpetuo frm = new FrmCertificadoPerpetuo();
            frm.Text = $"Nuevo Certificado del Contrato N° {obj.cntc_vnumero_contrato}";
            frm.MiEvento += new FrmCertificadoPerpetuo.DelegadoMensaje(Reload);
            frm.Status = Maintenance.BSMaintenanceStatus.CreateNew;
            frm.Obe = obj;
            frm.Show();
        }

        public void Modificar()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;

            if (Obe.cup_isituacion == Parametros.Autorizado || Obe.cup_isituacion == Parametros.Entregado)
            {
                string estado = Obe.cup_isituacion == Parametros.Entregado ? "Entregado" : "Autorizado";
                XtraMessageBox.Show($"No se Puede Modificar el Certificado {Obe.cup_vnumero}, su estado es {estado}", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FrmCertificadoPerpetuo frm = new FrmCertificadoPerpetuo();
            frm.Text = $"Modificando certificado N° {Obe.cup_vnumero}";
            frm.MiEvento += new FrmCertificadoPerpetuo.DelegadoMensaje(Reload);
            frm.Status = Maintenance.BSMaintenanceStatus.ModifyCurrent;
            frm.Obe = obj;
            frm.objCertificado = Obe;
            frm.Show();
            frm.SetValues();
        }

        public void Eliminar()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;
            if (Obe.cup_isituacion == Parametros.Entregado)
            {
                XtraMessageBox.Show($"No se Puede Eliminar el Certificado {Obe.cup_vnumero}, su estado es ENTREGADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (XtraMessageBox.Show($"Está Seguro de Eliminar el Certificado {Obe.cup_vnumero}?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;
            Obe.intUsuario = Valores.intUsuario;
            Obe.strPc = WindowsIdentity.GetCurrent().Name;
            Obe.flag = false;
            new BVentas().CetificadoUsoPerpetuoModificar(Obe);
            Cargar();
        }
        private void refresh()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null)
                Cargar();
            else
                Reload(Obe.cup_icod);
        }


    }
}