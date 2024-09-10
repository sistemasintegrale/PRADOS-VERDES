using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Ventas;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Reportes.Ventas;

namespace SGE.WindowForms.Ventas.Caja
{
    public partial class Frm13RecibosCajaVenta : DevExpress.XtraEditors.XtraForm
    {
        List<EReciboCajaCabecera> lstCabecera = new List<EReciboCajaCabecera>();
        EContrato obeContrato = new EContrato();
        public Frm13RecibosCajaVenta()
        {
            InitializeComponent();
        }

        private void Frm13RecibosCajaVenta_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit1, new BGeneral().listarTablaRegistro(21).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteRecibosCaja frm = new FrmManteRecibosCaja();
            frm.MiEvento += new FrmManteRecibosCaja.DelegadoMensaje(reload);
            frm.lstCabecera = lstCabecera;
            frm.CarcarControles();
            frm.SetInsert();
            frm.ShowDialog();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;
            FrmManteRecibosCaja frm = new FrmManteRecibosCaja();
            frm.MiEvento += new FrmManteRecibosCaja.DelegadoMensaje(reload);
            frm.SetModify();
            frm.CarcarControles();
            frm.obj = objCabecera;
            frm.SetValues();
            if (objCabecera.rc_isituacion == 11)
            {
                frm.SetView();
            }
            frm.ShowDialog();
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;
            if (objCabecera.rc_isituacion == Parametros.intSitProveedorAnulado)
            {
                XtraMessageBox.Show($"El Recibo {objCabecera.rc_vnumero} ya se Encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XtraMessageBox.Show($"Está Seguro de Anular el Recibo {objCabecera.rc_vnumero} ?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                return;
            objCabecera.rc_isituacion = Parametros.intSitProveedorAnulado;
            objCabecera.intUsuario = Valores.intUsuario;
            objCabecera.strPc = WindowsIdentity.GetCurrent().Name;
            new BVentas().anular_recibo_caja_cabecera(objCabecera);

            cargar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;
            if (XtraMessageBox.Show($"Está Seguro de Eliminar el Recibo {objCabecera.rc_vnumero} ?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                return;
            objCabecera.intUsuario = Valores.intUsuario;
            objCabecera.strPc = WindowsIdentity.GetCurrent().Name;
            new BVentas().eliminar_recibo_caja_cabecera(objCabecera);
            obeContrato = new BVentas().contrato_get_foma_financiamiento(objCabecera.rc_icod_contrato);
            var lstpagos = new BVentas().listarFomaFinanciamiento(objCabecera.rc_icod_contrato, obeContrato);
            var listDetalle = new BVentas().listar_recibo_caja_detalle(objCabecera.rc_icod_recibo);
            lstpagos.ForEach(Obe =>
            {
                var pago = listDetalle.Where(x => x.rc_itipo_pago == Obe.pgs_itipo).FirstOrDefault();
                Obe.intusuario = Valores.intUsuario;
                Obe.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                Obe.pgs_sfecha_pago = (DateTime?)null;
                Obe.pgs_nmonto_pagado = 0;
                new BVentas().FomaFinanciamientoModificar(Obe);
            });
            cargar();
        }

        void cargar()
        {
            lstCabecera = new BVentas().listar_recibos_caja_cabecera();
            grdLista.DataSource = lstCabecera;
            grdLista.RefreshDataSource();
        }

        void reload(int icod)
        {
            cargar();
            int index = lstCabecera.FindIndex(x => x.rc_icod_recibo == icod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }

        private void viewLista_DoubleClick(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;
            FrmManteRecibosCaja frm = new FrmManteRecibosCaja();
            frm.MiEvento += new FrmManteRecibosCaja.DelegadoMensaje(reload);
            frm.SetView();
            frm.CarcarControles();
            frm.obj = objCabecera;
            frm.SetValues();
            frm.ShowDialog();
        }

        private void txtNumero_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtCliente_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        void filtrar()
        {
            viewLista.Columns["rc_vnumero"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rc_vnumero] LIKE '%" + txtNumero.Text + "%'");
            viewLista.Columns["strCliente"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strCliente] LIKE '%" + txtCliente.Text + "%'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                reload(objCabecera.rc_icod_recibo);
            else
                cargar();

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;

            rptVoucher rpt = new rptVoucher();

            var listaDetalle = new BVentas().listar_recibo_caja_detalle(objCabecera.rc_icod_recibo);
            if (objCabecera.rc_isituacion != 8)
            {
                var listaFomaAux = new BVentas().CuotaFomaListar(objCabecera.rc_icod_contrato);
                var listaFoma = new List<ECuotaFoma>();
                if (!string.IsNullOrWhiteSpace(objCabecera.rc_icod_foma_anulado))
                {
                    string[] icods = objCabecera.rc_icod_foma_anulado.Split(' ');
                    for (int i = 0; i < icods.Length; i++)
                    {
                        int icod = string.IsNullOrWhiteSpace(icods[i]) ? 0 : Convert.ToInt32(icods[i]);
                        if (icod != 0)
                            listaFoma.Add(listaFomaAux.Where(x => x.ccf_icod_cuota == icod).FirstOrDefault());
                    }
                }

                listaDetalle.ForEach(x =>
                {
                    if (x.rc_itipo_pago == Parametros.intTipoFoma)
                    {
                        x.strDescripcion = x.strDescripcion + " : " + getSeleccionados(listaFoma);
                    }
                });
            }
            else
            {
                listaDetalle.ForEach(x =>
                {
                    if (x.rc_itipo_pago == Parametros.intTipoFoma)
                    {
                        var listaFoma = new BVentas().CuotaFomaListar(objCabecera.rc_icod_contrato).Where(y => y.rc_icod_recibo == objCabecera.rc_icod_recibo).ToList();
                        x.strDescripcion = x.strDescripcion + " : " + getSeleccionados(listaFoma);
                    }
                });
            }

            rpt.cargar(objCabecera, listaDetalle, new BGeneral().listarTablaVentaDet(26).ToList());
        }
        private string getSeleccionados(List<ECuotaFoma> listaFoma)
        {
            string valores = string.Empty;
            listaFoma.ForEach(x =>
            {

                valores = valores + " " + x.strNivel;

            });
            return valores;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;
            if (objCabecera.rc_isituacion == Parametros.intSitProveedorAnulado)
            {
                mmnuanular.Visible = false;
                mnuActivar.Visible = true;
            }
            else
            {
                mmnuanular.Visible = true;
                mnuActivar.Visible = false;
            }
        }

        private void mnuActivar_Click(object sender, EventArgs e)
        {
            EReciboCajaCabecera objCabecera = (EReciboCajaCabecera)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (objCabecera == null)
                return;
            if (XtraMessageBox.Show($"Está Seguro de Activar el Recibo {objCabecera.rc_vnumero} ?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                return;
            try
            {
                objCabecera.rc_isituacion = 8;
                objCabecera.intUsuario = Valores.intUsuario;
                objCabecera.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().activar_recibo_caja_cabecera(objCabecera);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { reload(objCabecera.rc_icod_recibo); }
        }
    }
}