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

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmListarCuotaFomas : DevExpress.XtraEditors.XtraForm
    {
        public List<ECuotaFoma> listaFoma = new List<ECuotaFoma>();
        public EReciboCajaCabecera ojrecibo = new EReciboCajaCabecera();
        public EContrato Obe = new EContrato();
        public FrmListarCuotaFomas()
        {
            InitializeComponent();
        }

        private void FrmListarCuotaFomas_Load(object sender, EventArgs e)
        {
            Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit1, new BGeneral().listarTablaVentaDet(26), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;

            cargar();
        }

        private void viewLista_DoubleClick(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmmanteFomaCuotaContrato frm = new FrmmanteFomaCuotaContrato();
            frm.MiEvento += new FrmmanteFomaCuotaContrato.DelegadoMensaje(reload);
            frm.Text = $"Foma del contrato {Obe.cntc_vnumero_contrato}";
            frm.obe = Obe;
            frm.SetInsert();
            frm.txtMontoPagado.Visible = false;
            frm.label3.Visible = false;
            frm.Show();
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
            frm.txtMontoPagado.Visible = false;
            frm.label3.Visible = false;
            frm.Show();
        }

        private void reload(int intIcod)
        {
            cargar();
            if (listaFoma.Exists(x => x.ccf_icod_cuota == intIcod))
            {
                foreach (var x in listaFoma)
                {
                    if (x.ccf_icod_cuota == intIcod)
                    {
                        bool anterior = x.select;
                        var data = new BVentas().CuotaFomaGetById(intIcod);
                        x.ccf_nmonto_pagar = data.ccf_nmonto_pagar;
                        x.ccf_icod_nivel = data.ccf_icod_nivel;
                        x.strNivel = data.strNivel;
                        x.select = anterior;
                    }
                }
            }
            else
            {
                listaFoma.Add(new BVentas().CuotaFomaGetById(intIcod));
            }
            grdLista.DataSource = listaFoma.Where(x => x.rc_icod_recibo == ojrecibo.rc_icod_recibo || x.rc_icod_recibo == null).ToList();
            grdLista.RefreshDataSource();
            int index = listaFoma.FindIndex(x => x.ccf_icod_cuota == intIcod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }

        private void cargar()
        {
            grdLista.DataSource = listaFoma.Where(x => x.rc_icod_recibo == ojrecibo.rc_icod_recibo || x.rc_icod_recibo == null).ToList();
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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewLista.MoveLast();
            textBox1.Focus();
            if (listaFoma.Where(x => x.select == true).Count() == 0)
            {
                XtraMessageBox.Show("No se Seleccionó Ningún Pago", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}