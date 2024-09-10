using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Modules;
using static SGE.Common.TableVenta;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes_Cuotas
{
    public partial class Frm05RegistroCuotasXContratos : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EContratoCuotas> Lista;

        public Frm05RegistroCuotasXContratos()
        {
            InitializeComponent();
        }


        private void FrmEstadoCuentaClientes_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }

        private void Cargar()
        {
            Lista = new BVentas().listarCuotas();
            Lista.ForEach(x =>
            {
                string fecha = x.cntc_sfecha_pago_cuota.ToString().Substring(0, 10);
                if (x.cntc_sfecha_pago_cuota.ToString().Substring(0, 10) == "01/01/0001")
                {
                    x.cntc_sfecha_pago_cuota = (DateTime?)null;
                }
            });
            dgr.DataSource = Lista;
        }



        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas oBe_ = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteContratoCuotasDet frm = new frmManteContratoCuotasDet())
            {
                frm.oBe = oBe_;
                frm.cntc_icod_contrato = oBe_.cntc_icod_contrato;
                frm.SetModify();
                frm.lstDetalle = Lista;
                frm.txtNroCuotas.Enabled = false;
                frm.lkpTipo.Enabled = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Lista = frm.lstDetalle;
                    view.RefreshData();
                    view.Focus();
                }
            }
        }
        private void filtrar()
        {
            view.Columns["NumContrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[NumContrato] LIKE '%" + txtNroContrato.Text + "%'");
            view.Columns["cntc_vnombre_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnombre_contratante] LIKE '%" + txtContratante.Text + "%'");
            view.Columns["cntc_vdni_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdni_contratante] LIKE '%" + txtDNI.Text + "%'");
        }
        private void txtNroContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtDNI_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cargar();
        }

        private void consultarContratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (obe == null)
                return;
            FrmConsultarDocumentoscs frm = new FrmConsultarDocumentoscs();
            frm.cod = obe.cntc_icod_contrato_cuotas;
            frm.Text = "Consulta de Documentos Por Cuota";
            frm.cargar();
            frm.ShowDialog();
            var lstpago = new BVentas().Listar_Pagos_Documentos(obe.cntc_icod_contrato_cuotas);
            if (lstpago.Any() && obe.cntc_icod_situacion == 338)
            {
                obe.intUsuario = Valores.intUsuario;
                obe.cntc_npagado = lstpago.Sum(x => x.pgc_nmonto_pago);
                obe.cntc_nmonto_mora_pago = lstpago.Sum(x => x.pgc_nmonto_pago_mora);
                obe.cntc_nsaldo = obe.cntc_nmonto_cuota - obe.cntc_npagado;
                obe.cntc_icod_situacion = Math.Round(obe.cntc_nsaldo, 0, MidpointRounding.AwayFromZero) == 0 ? (int)EstadoCuota.Cancelado : (int)EstadoCuota.ParcialmentePagado;

                var listaAux = new List<EContratoCuotas>();
                listaAux.Add(obe);
                new BVentas().modificarCCuotas(listaAux, new List<EContratoCuotas>()); ;
                view.RefreshData();

            }


        }

        private void view_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)view.GetRow(view.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_icod_documento > 0)
            {
                consultarContratosToolStripMenuItem.Enabled = false;

            }
            else
            {
                consultarContratosToolStripMenuItem.Enabled = true;

            }
        }

        private void txtNroContrato_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}