using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteRecibosCaja : DevExpress.XtraEditors.XtraForm
    {
        public EReciboCajaCabecera obj = new EReciboCajaCabecera();
        public List<EReciboCajaDetalle> listDetalle = new List<EReciboCajaDetalle>();
        public List<EReciboCajaDetalle> listDetalleEliminar = new List<EReciboCajaDetalle>();
        public List<EReciboCajaCabecera> lstCabecera = new List<EReciboCajaCabecera>();
        public EContrato obeContrato = new EContrato();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public List<ECuotaFoma> listaFoma = new List<ECuotaFoma>();

        private void FrmManteRecibosCaja_Load(object sender, EventArgs e)
        {
            dteFecha.DateTime = DateTime.Today;

        }

        private void grdDetalle_Click(object sender, EventArgs e)
        {

        }



        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = false;
                txtNumero.Enabled = false;
                lkpMoneda.Enabled = false;
                btnContrato.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtSerie.Enabled = false;
                txtNumero.Enabled = false;
                dteFecha.Enabled = false;
                lkpMoneda.Enabled = false;
                lkpSituacion.Enabled = false;
                bteCliente.Enabled = false;
                txtDireccion.Enabled = false;
                txtDNI.Enabled = false;
                txtTelefono.Enabled = false;
                btnGuardar.Enabled = false;
                contextMenuStrip1.Enabled = false;
                btnContrato.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lkpMoneda.Enabled = false;
                txtSerie.Properties.ReadOnly = true;

            }
        }


        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            lkpMoneda.EditValue = Parametros.intTipoMonedaSoles;
            lkpSituacion.EditValue = Parametros.intSitProveedorGenerado;

            var objParametro = new BVentas().listarRegistroParametro().FirstOrDefault();
            txtSerie.Text = objParametro.rgpmc_vserie_recibo_caja;
            txtNumero.Text = (objParametro.rgpmc_icorrelativo_recibo_caja + 1).ToString();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetView()
        {
            Status = BSMaintenanceStatus.View;
        }

        public FrmManteRecibosCaja()
        {
            InitializeComponent();
        }

        public void CarcarControles()
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(21).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLookRepository(lkpgrdTipo, new BGeneral().listarTablaVentaDet(26).ToList(), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
        }

        public void CalcularTotal()
        {

            decimal monto = listDetalle.Sum(x => x.rcd_dprecio_total);
            txtMontoTotal.Text = monto.ToString();
        }

        public void SetValues()
        {
            txtSerie.Text = obj.rc_vnumero.Substring(0, 4);
            txtNumero.Text = obj.rc_vnumero.Substring(4);
            dteFecha.DateTime = obj.rc_sfecha_recibo;
            lkpMoneda.EditValue = obj.rc_itipo_moneda;
            lkpSituacion.EditValue = obj.rc_isituacion;
            bteCliente.Tag = obj.rc_icod_cliente;
            bteCliente.Text = obj.strCliente;
            txtDireccion.Text = obj.cliec_vdireccion_cliente;
            txtDNI.Text = obj.strNroDocCliente;
            txtTelefono.Text = obj.cliec_vnro_telefono;
            txtMontoTotal.Text = obj.rc_dmonto_total.ToString();
            btnContrato.Tag = obj.rc_icod_contrato;
            btnContrato.Text = obj.strContrato;
            txtNombreCliente.Text = obj.rc_vnombre_cliente;
            txtDireccion.Text = obj.rc_vdireccion_cliente;
            txtDNI.Text = obj.rc_vnro_doc_cliente;
            listDetalle = new BVentas().listar_recibo_caja_detalle(obj.rc_icod_recibo);
            obeContrato = new BVentas().listarContratoPorIcod(obj.rc_icod_contrato);
            grdDetalle.DataSource = listDetalle;
            grdDetalle.RefreshDataSource();
            listaFoma = new BVentas().CuotaFomaListar(obj.rc_icod_contrato);
            obeContrato = new BVentas().listarContratoPorIcod(obj.rc_icod_contrato);

            txtMontoPagar.Text = listaFoma.Where(x => x.rc_icod_recibo == obj.rc_icod_recibo).Sum(x => x.ccf_nmonto_pagar).ToString();
            listaFoma.ForEach(x =>
            {

                if (!string.IsNullOrWhiteSpace(x.strNumRecibo) && x.rc_icod_recibo == obj.rc_icod_recibo)
                {
                    x.select = true;
                }
            });
            btnFoma.Text = getSeleccionados(listaFoma);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteRecibosCajaDetalle frm = new FrmManteRecibosCajaDetalle();
            frm.txtNro.Text = listDetalle.Count != 0 ? (listDetalle.Max(x => x.rcd_inro_item) + 1).ToString() : "1";
            frm.CargarControles();
            frm.SetInsert();
            frm.listDetalle = listDetalle;
            frm.obeContrato = new BVentas().listarContratoPorIcod(Convert.ToInt32(btnContrato.Tag));
            frm.obj = obj;
            frm.obj.rc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
            frm.listaFoma = listaFoma;
            frm.monto_foma = Convert.ToDecimal(txtMontoPagar.Text);
            frm.Text = $"Detalle N° {frm.txtNro.Text} del Recibo {txtSerie.Text}-{txtNumero.Text}";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listaFoma = frm.listaFoma;
                frm.objdetalle.TipoOperacion = 1;
                listDetalle.Add(frm.objdetalle);
                grdDetalle.DataSource = listDetalle;
                grdDetalle.RefreshDataSource();
                grdDetalle.Refresh();
                CalcularTotal();
                if (frm.objdetalle.rc_itipo_pago == Parametros.intTipoFoma)
                {
                    obeContrato.cntc_nmonto_foma = Convert.ToDecimal(frm.txtMontoPagar.Text);
                }
            }
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtSerie.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException($"Ingrese Número de Serie");
                }


                if (Status == BSMaintenanceStatus.CreateNew && new BVentas().verificarSerieReciboCaja(txtSerie.Text + txtNumero.Text) == true)
                {
                    if (XtraMessageBox.Show($"Ya existe Recibo con el N° {txtSerie.Text}{txtNumero.Text}, se ingresará con el N° {txtSerie.Text}" + (Convert.ToInt32(txtNumero.Text) + 1).ToString("d8"), "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        txtNumero.Text = (Convert.ToInt32(txtNumero.Text) + 1).ToString();
                        SetSave();
                        return;
                    }
                    else
                    {
                        oBase = txtNumero;
                        throw new ArgumentException($"Ya existe Recibo con el N° {txtSerie.Text}{txtNumero.Text}");
                    }

                }
                if (Convert.ToInt32(bteCliente.Tag) == 0 && string.IsNullOrEmpty(txtNombreCliente.Text))
                {
                    oBase = txtNombreCliente;
                    throw new ArgumentException("Ingrese Cliente");
                }

                if (Convert.ToInt32(btnContrato.Tag) == 0)
                {
                    oBase = btnContrato;
                    throw new ArgumentException("Ingrese Contrato");
                }
                if (listDetalle.Count() == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Ingrese Detale");
                }
                if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFoma))
                {
                    if (listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFoma).Select(x => x.rcd_dprecio_total).First() != Convert.ToDecimal(txtMontoPagar.Text))
                    {
                        oBase = txtMontoPagar;
                        throw new ArgumentException("El Monto Pagado de la Foma no Concuerda con los Montos Seleccionados");
                    }
                }
                if (listDetalle.Exists(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento))
                {
                    var Reprogramacion = new BVentas().ListarReprogramaciones(obeContrato.cntc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
                    obeContrato.cntc_nfinanciamientro = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : obeContrato.cntc_nfinanciamientro;
 
                    if (listDetalle.Where(x => x.rc_itipo_pago == Parametros.intTipoFinanciamiento).Select(x => x.rcd_dprecio_total).First() != obeContrato.cntc_nfinanciamientro)
                    {
                        oBase = txtMontoPagar;
                        throw new ArgumentException("El Monto Pagado del Financiamiento no Concuerda con el Monto Financiamiento del Contarto");
                    }
                }
                obj.rc_vnumero = string.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                obj.rc_sfecha_recibo = dteFecha.DateTime;
                obj.rc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                obj.rc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                obj.rc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                obj.rc_itipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                obj.rc_dmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                obj.rc_vnombre_cliente = txtNombreCliente.Text;
                obj.rc_vdireccion_cliente = txtDireccion.Text;
                obj.rc_vnro_doc_cliente = txtDNI.Text;
                obj.intUsuario = Valores.intUsuario;
                obj.strPc = WindowsIdentity.GetCurrent().Name;
                var datosPC = WindowsIdentity.GetCurrent();

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.rc_icod_recibo = new BVentas().insertar_recibo_caja(obj, listDetalle, listaFoma.Where(x => x.select).ToList());
                }
                else
                {
                    new BVentas().modificar_recibo_caja(obj, listDetalle, listDetalleEliminar, listaFoma.Where(x => x.select).ToList());
                }


            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(obj.rc_icod_recibo);
                    Close();
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReciboCajaDetalle objdetalle = (EReciboCajaDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (objdetalle == null)
                return;

            FrmManteRecibosCajaDetalle frm = new FrmManteRecibosCajaDetalle();
            frm.monto_foma = Convert.ToDecimal(txtMontoPagar.Text);
            frm.txtNro.Text = listDetalle.Count != 0 ? listDetalle.Max(x => x.rcd_inro_item).ToString() : "1";
            frm.CargarControles();
            frm.objdetalle = objdetalle;
            frm.SetModify();
            frm.listDetalle = listDetalle;
            frm.obeContrato = new BVentas().listarContratoPorIcod(Convert.ToInt32(btnContrato.Tag));
            frm.listaFoma = listaFoma;
            frm.Text = $"Detalle N° {frm.txtNro.Text} del Recibo {txtSerie.Text}-{txtNumero.Text}";
            frm.obj = obj;
            frm.SetValues();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listaFoma = frm.listaFoma;
                objdetalle = frm.objdetalle;
                objdetalle.TipoOperacion = 2;
                viewDetalle.RefreshData();
                CalcularTotal();
                if (frm.objdetalle.rc_itipo_pago == Parametros.intTipoFoma)
                {
                    obeContrato.cntc_nmonto_foma = Convert.ToDecimal(frm.txtMontoPagar.Text);
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReciboCajaDetalle objdetalle = (EReciboCajaDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (objdetalle == null)
                return;
            objdetalle.intUsuario = Valores.intUsuario;
            objdetalle.strPc = WindowsIdentity.GetCurrent().Name;
            listDetalleEliminar.Add(objdetalle);
            listDetalle.Remove(objdetalle);
            int numero = 0;
            listDetalle.ForEach(x =>
            {
                numero++;
                x.rcd_inro_item = numero;
                if (x.TipoOperacion == 0)
                {
                    x.TipoOperacion = 2;
                }
            });
            grdDetalle.DataSource = listDetalle;
            grdDetalle.RefreshDataSource();
            CalcularTotal();
        }

        private void btnContrato_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarContrato frm = new FrmListarContrato())
            {

                frm.ingresaCliente = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnContrato.Tag = frm._Be.cntc_icod_contrato;
                    btnContrato.Text = frm._Be.cntc_vnumero_contrato;
                    obeContrato = new BVentas().listarContratoPorIcod(frm._Be.cntc_icod_contrato);
                    listaFoma = new BVentas().CuotaFomaListar(frm._Be.cntc_icod_contrato);
                }
            }
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FrmListarCliente frm = new FrmListarCliente())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCliente.Tag = frm._Be.cliec_icod_cliente;
                    bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                    txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                    txtDNI.Text = frm._Be.cliec_vnumero_doc_cli;
                    txtNombreCliente.Text = frm._Be.cliec_vnombre_cliente;
                }
            }
        }

        private void btnFoma_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarCuotaFomas frm = new FrmListarCuotaFomas())
                {
                    frm.Obe = new BVentas().listarContratoPorIcod(Convert.ToInt32(btnContrato.Tag));
                    frm.Text = $"Foma del Contrato {obeContrato.cntc_vnumero_contrato}";
                    frm.listaFoma = listaFoma;
                    frm.ojrecibo = obj;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        listaFoma = frm.listaFoma;
                        btnFoma.Text = getSeleccionados(listaFoma);
                        txtMontoPagar.Text = listaFoma.Where(x => x.rc_icod_recibo == obj.rc_icod_recibo || x.rc_icod_recibo == null && x.select == true).Sum(x => x.ccf_nmonto_pagar).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string getSeleccionados(List<ECuotaFoma> listaFoma)
        {
            string valores = string.Empty;
            listaFoma.ForEach(x =>
            {
                if (x.select)
                {
                    valores = valores + " " + x.strNivel;
                }
            });
            return valores;
        }
    }
}