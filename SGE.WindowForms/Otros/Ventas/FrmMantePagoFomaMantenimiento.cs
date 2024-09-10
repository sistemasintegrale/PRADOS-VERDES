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
using SGE.WindowForms.Otros.Ventas;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmMantePagoFomaMantenimiento : DevExpress.XtraEditors.XtraForm
    {
        public EContrato Obe = new EContrato();
        public List<ECuotaFoma> lista = new List<ECuotaFoma>();
        public FrmMantePagoFomaMantenimiento()
        {
            InitializeComponent();
        }
        public void setView() {
            contextMenuStrip1.Enabled = false;
        }
        private void FrmMantePagoFomaMantenimiento_Load(object sender, EventArgs e)
        {
            cargar();
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
        }

        public void cargarControles()
        {
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit1, new BGeneral().listarTablaVentaDet(26), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
        }
        public void SetValues()
        {
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
        }

        public void cargaLista()
        {


        }

        private void modiificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ECuotaFoma obj = (ECuotaFoma)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (obj == null)
                return;
            FrmmanteFomaCuotaContrato frm = new FrmmanteFomaCuotaContrato();
            frm.MiEvento += new FrmmanteFomaCuotaContrato.DelegadoMensaje(reload);
            frm.SetModify();
            frm.obe = Obe;
            frm.objCuota = new BVentas().CuotaFomaGetById(obj.ccf_icod_cuota);
            frm.Setvalues();
            frm.Text = $"Foma del contrato {Obe.cntc_vnumero_contrato}";
            frm.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ECuotaFoma obj = (ECuotaFoma)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (obj == null)
                return;
            if (!string.IsNullOrEmpty(obj.strNumRecibo))
            {
                modiificarToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
            }
            else
            {
                modiificarToolStripMenuItem.Enabled = true;
                eliminarToolStripMenuItem.Enabled = true;
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmmanteFomaCuotaContrato frm = new FrmmanteFomaCuotaContrato();
            frm.MiEvento += new FrmmanteFomaCuotaContrato.DelegadoMensaje(reload);
            frm.Text = $"Foma del contrato {Obe.cntc_vnumero_contrato}";
            frm.obe = Obe;
            frm.SetInsert();
            frm.Show();

        }

        private void reload(int intIcod)
        {
            cargar();
            int index = lista.FindIndex(x => x.ccf_icod_cuota == intIcod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }

        private void cargar()
        {
            lista = new BVentas().CuotaFomaListar(Obe.cntc_icod_contrato);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ECuotaFoma obj = (ECuotaFoma)viewLista.GetRow(viewLista.FocusedRowHandle);

            if (obj == null)
                return;
            if (XtraMessageBox.Show("¿Está Seguro de Eliminar el Registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;
            obj.intUsuario = Valores.intUsuario;
            obj.strPc = WindowsIdentity.GetCurrent().Name;
            new BVentas().CuotaFomaEliminar(obj);
            cargar();

        }
    }
}