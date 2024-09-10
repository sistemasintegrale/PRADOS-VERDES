using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;
using SGE.WindowForms.Maintenance;
using System.Security.Principal;
using SGE.WindowForms.Otros.Almacen.Listados;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmMantePlanillaCobranza : DevExpress.XtraEditors.XtraForm
    {
        public EPlanillaCobranzaCab ObePlnCab = new EPlanillaCobranzaCab();
        public EPlanillaCobranzaDet oBePln = new EPlanillaCobranzaDet();
        //public EPlanillaCobranzaDet oBePln2 = new EPlanillaCobranzaDet();
        public EFacturaCab oBe = new EFacturaCab();
        //public EFacturaCab oBe2 = new EFacturaCab();
        List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        List<EFacturaDet> lstDelete = new List<EFacturaDet>();
        List<EPagoDocVenta> lstPagos = new List<EPagoDocVenta>();
        List<EPagoDocVenta> lstDeletePagos = new List<EPagoDocVenta>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public List<EPedidosPVTDetalle> listaPVT = new List<EPedidosPVTDetalle>();
        public delegate void DelegadoMensaje(int intIcod, EPlanillaCobranzaCab oBePlnCabReload);
        public event DelegadoMensaje MiEvento;
        private decimal? IGV;
        public string CondicionPorIVAP;
        public string CondicionPorIGV;
        public string PorIVAP;
        public string PorIGV;
        public bool flag_MasDeUnaFactura = false;
        public int intClienteOT = 0;
        public int IcodVendedor = 0;
        List<EPedidosPVTDetalle> lstPVTDelete = new List<EPedidosPVTDetalle>();
        List<EParametro> lstParametro = new List<EParametro>();
        public Boolean indicador_ivap;
        public decimal porcentaje_ivap;
        public decimal monto_ivap;
        public int icodCuotaAnterior;
        decimal total_pago;
        decimal total_pago_aux;
        List<EDetPago> lstpagos = new List<EDetPago>();
        public int contrato;
        public int situacion = 0;
        public int modificar = 1;
        public int nuevo = 0;
        public bool View = false;
        class EDetPago
        {
            public int cntc_icod_contrato { get; set; }
            public decimal cntc_nmonto_cuota { get; set; }
        }
        public frmMantePlanillaCobranza()
        {
            InitializeComponent();
        }

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtSerie.Enabled = !Enabled;
            txtNumero.Enabled = !Enabled;
            //bteNroDoc.Enabled = !Enabled;
            //lkpMoneda.Enabled = !Enabled;
            bteCliente.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            mnuPagos.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            lkpFormaPago.Enabled = !Enabled;
            bteRefreshTipoCambio.Enabled = !Enabled;
            btnContrato.Properties.ReadOnly = Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                //bteNroDoc.Enabled = Enabled;
                //lkpMoneda.Enabled = Enabled;
                btnContrato.Properties.ReadOnly = true;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                btnContrato.Properties.ReadOnly = false;
            }
        }

        private void frmManteVentaPorDia_Load(object sender, EventArgs e)
        {
            // groupControl2.Height = 200;
            cargar();
            IGV = Convert.ToDecimal(Parametros.strPorcIGV);
            if (Status == BSMaintenanceStatus.CreateNew)
                setVendedor();
        }
        private void setVendedor()
        {
            EVendedor Vendedor = new BVentas().listarVendedor().Where(x => x.vendc_icod_vendedor == IcodVendedor).ToList().FirstOrDefault();
            bteVendedor.Text = Vendedor == null ? "" : Vendedor.vendc_vnombre_vendedor;
            bteVendedor.Tag = Vendedor == null ? 0 : Vendedor.vendc_icod_vendedor;

        }
        private void cargar()
        {
            grdDetalle.DataSource = lstFacturaDetalle;
            grdPagos.DataSource = lstPagos;
            List<TipoDoc> lst = new List<TipoDoc>();
            lst.Add(new TipoDoc { intCodigo = 26, strTipoDoc = "FAV" });
            lst.Add(new TipoDoc { intCodigo = 9, strTipoDoc = "BOV" });
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaFormaPago).Where(x => x.tarec_icorrelativo_registro == 1 || x.tarec_icorrelativo_registro == 2
                ).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            /**/
            //dtFechaVenc.DateTime = DateTime.Today;
            lstParametro = new BVentas().listarParametroVenta();
            //lstContrato = new BVentas().listarContratoCuotas(Convert.ToInt32(btnContrato.Tag)).Where(x=>(x.cntc_icod_documento == oBe.doc_icod_documento) || (x.cntc_icod_documento == 0) ).ToList();
            lstContrato = new BVentas().listarPagos(Convert.ToInt32(btnContrato.Tag)).Where(x => (x.cntc_icod_documento == oBePln.plnd_icod_documento) || (x.cntc_icod_documento == 0)).ToList();
            if (lstContrato.Count() > 0)
                situacion = modificar;

            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
                getTipoCambio();
                situacion = nuevo;
                //txtPorcentIGV.Text = Parametros.strPorcIGV;
            }

        }
      
        public void setValues()
        {

            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
            {
                EBoletaCab oBeBov = new BVentas().getBoletaCab(Convert.ToInt32(oBePln.plnd_icod_documento))[0];
                oBe = getFactura(oBeBov);

                var lst = new BVentas().listarBoletaDetallePlanilla(oBeBov.bovc_icod_boleta);
                lstFacturaDetalle = getFacturaDetalle(lst);

                grdDetalle.DataSource = lstFacturaDetalle;
            }

            #region Cabecera
            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                oBe = new BVentas().getFacturaCab(Convert.ToInt32(oBePln.plnd_icod_documento))[0];

            if (oBe.favc_vnumero_factura.Length == 10)
            {
                txtSerie.Text = oBe.favc_vnumero_factura.Substring(0, 3);
                txtNumero.Text = oBe.favc_vnumero_factura.Substring(3, 7);
            }
            else
            {
                txtSerie.Text = oBe.favc_vnumero_factura.Substring(0, 4);
                txtNumero.Text = oBe.favc_vnumero_factura.Substring(4, 8);
            }

            dtFechaDoc.EditValue = oBe.favc_sfecha_factura;
            bteCliente.Tag = oBe.cliec_icod_cliente;


            txtMontoCredito.Text = oBe.facv_nmonto_credito.ToString();
            txtMonto1raCuota.Text = oBe.facv_nmonto_1era_cuota.ToString();
            if (oBe.facv_sfecha_pago_1era_cuota != null)
            {
                if (oBe.facv_sfecha_pago_1era_cuota.ToString().Substring(0, 10) == "01/01/0001")
                {
                    dteFechaPago1raCuota.EditValue = (DateTime?)null;
                }

            }
            else
            {
                dteFechaPago1raCuota.EditValue = oBe.facv_sfecha_pago_1era_cuota;
            }

            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
            {
                bteCliente.Text = oBe.cliec_vnombre_cliente;
                txtRUC.Text = oBe.favc_vruc;
                txtDireccion.Text = oBe.favc_vdireccion_cliente;
                txtNombre.Text = oBe.cliec_vnombre_cliente;
            }
            else
            {
                bteCliente.Text = oBe.cliec_vnombre_cliente;
                bteCliente.Text = oBe.bovc_vnombre_cliente;
                txtNombre.Text = oBe.bovc_vnombre_cliente;
                txtDNI.Text = oBe.bovc_vDNI_cliente;
                txtDireccion.Text = oBe.bovc_vdireccion_cliente;
            }

            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtMontoTotal.Text = oBe.favc_nmonto_total.ToString();
            CondicionPorIVAP = oBe.favc_npor_imp_ivap.ToString();
            //txtMontoImpArroz.Text = oBe.favc_nmonto_ivap.ToString();
            CondicionPorIGV = oBe.favc_npor_imp_igv.ToString();
            //txtMontoNetoIVAP.Text = oBe.favc_nmonto_neto_ivap.ToString();
            //txtMontoNetoExo.Text = oBe.favc_nmonto_neto_exo.ToString();
            lkpFormaPago.EditValue = oBe.tablc_iid_forma_pago;
            PorIVAP = oBe.favc_npor_imp_ivap.ToString();
            PorIGV = oBe.favc_npor_imp_igv.ToString();
            if (oBePln.plnd_tipo_cambio == 0)
                oBePln.plnd_tipo_cambio = new BContabilidad().getTipoCambioPorFecha(oBe.favc_sfecha_factura);
            dtFechaVenc.EditValue = oBe.favc_sfecha_vencim_factura;
            txtTipoCambio.Text = oBePln.plnd_tipo_cambio.ToString();
            int intDias = ((TimeSpan)(oBe.favc_sfecha_vencim_factura - oBe.favc_sfecha_factura)).Days;
            //spinDias.EditValue = intDias;
            switch (oBePln.plnd_icod_tipo_doc)
            {
                case 26:
                    rbFactura.Checked = true;
                    break;
                case 9:
                    rbBoleta.Checked = true;
                    break;
            }

            txtmonto.Text = oBe.desc_nMonto.ToString();

            btnContrato.Tag = oBe.cntc_icod_contrato;
            //btnCuotas.Tag = oBe.cntc_icod_contrato_cuotas;
            btnContrato.Text = oBe.cntc_vnumero_contrato;
            //btnCuotas.Text = oBe.cntc_inro_cuotas.ToString();

            icodCuotaAnterior = oBe.cntc_icod_contrato_cuotas;


            #endregion
            if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
            {
                lstFacturaDetalle = new BVentas().listarFacturaDetallePlanilla(oBe.favc_icod_factura);
                grdDetalle.DataSource = lstFacturaDetalle;

            }
            //
            lstPagos = new BVentas().listarPago(Convert.ToInt32(oBePln.plnd_icod_tipo_doc), Convert.ToInt32(oBePln.plnd_icod_documento));
            grdPagos.DataSource = lstPagos;
            setTotales();

            if (Convert.ToInt32(oBe.orpc_iid_orden_trabajo) > 0)
            {
                lkpMoneda.Enabled = false;
                mnu.Enabled = false;
                bteCliente.Enabled = false;
            }
            lstContrato = new BVentas().listarPagos(Convert.ToInt32(btnContrato.Tag)).Where(x => (x.cntc_icod_documento == oBePln.plnd_icod_documento) || (x.cntc_icod_documento == 0)).ToList();
            lblMontoPagarCuota.Text = string.Format("Monto Pagar Cuotas: {0}", lstContrato.Sum(x => x.monto_pagar));
            lblMontoPagarMora.Text = string.Format("Monto Pagar Mora : {0}", lstContrato.Sum(x => x.cntc_nmonto_mora_pago));

            if (lstContrato.Sum(x => x.monto_pagar) > 0 || lstContrato.Sum(x => x.cntc_nmonto_mora) > 0)
            {
                groupControl2.Height = 228;
            }
            else
            {
                groupControl2.Height = 200;
            }

        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            string strMasDeUnaFactura = "";

            try
            {
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione Cliente");
                }
                if (Convert.ToInt32(btnContrato.Tag) == 0)
                {
                    oBase = btnContrato;
                    throw new ArgumentException("Seleccione Contrato");
                }
                if (txtSerie.Text == "0000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(btnContrato.Tag) == 0)
                {
                    if (XtraMessageBox.Show("No ha ingresado Contrato. ¿Esta seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        Flag = false;
                        return;
                    }
                }

                if (String.IsNullOrEmpty(dtFechaVenc.Text))
                {
                    oBase = dtFechaVenc;
                    throw new ArgumentException("Ingrese la Fecha ");
                }

                if (!flag_MasDeUnaFactura)
                    if (lstPagos.Count == 0)
                    {
                        if (XtraMessageBox.Show("No ha ingresado pagos para este documento. El documento solo será grabado en Cuentas Corrientes y no en la Planilla de Cobranza\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t¿Esta seguro que desea continuar con la grabación?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            Flag = false;
                            return;
                        }
                    }
                if (rbFactura.Checked)
                {
                    if (string.IsNullOrEmpty(txtRUC.Text))
                    {
                        oBase = bteCliente;
                        throw new ArgumentException("El Cliente no tiene RUC, Para Completar esta Operación Ingrese el RUC del Cliente");
                    }
                    if (txtRUC.Text.Trim().Count() != 11)
                    {
                        oBase = bteCliente;
                        throw new ArgumentException("El Cliente tiene RUC no Válido");
                    }
                }
                if (rbBoleta.Checked)
                {
                    if (Convert.ToDecimal(txtPago.Text) >= 700)
                    {
                        if (string.IsNullOrEmpty(txtDNI.Text))
                        {
                            oBase = bteCliente;
                            throw new ArgumentException("El Cliente no tiene DNI, Para Completar esta Operación Ingrese el DNI del Cliente");
                        }
                        if (txtDNI.Text.Trim().Count() != 8)
                        {
                            oBase = bteCliente;
                            throw new ArgumentException("El Cliente tiene RUC no Válido");
                        }
                    }
                }

                #region Cabecera del Doc - Factura o Boleta
                oBe.favc_vnumero_factura = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.favc_sfecha_factura = Convert.ToDateTime(dtFechaDoc.Text);
                oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dtFechaDoc.Text);
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.bovc_vnombre_cliente = txtNombre.Text;
                oBe.favc_vdireccion_cliente = txtDireccion.Text;
                oBe.bovc_vdireccion_cliente = txtDireccion.Text;
                oBe.favc_vruc = txtRUC.Text;
                oBe.bovc_vDNI_cliente = txtDNI.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_forma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dtFechaVenc.EditValue);
                oBe.tablc_iid_situacion = 8;
                oBe.favc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.favc_nmonto_imp = Convert.ToDecimal(txtMontoIGV.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.strDesCliente = bteCliente.Text;
                oBe.desc_nMonto = Convert.ToDecimal(txtmonto.Text);
                oBe.favc_npor_imp_ivap = Convert.ToDecimal(PorIVAP);
                oBe.favc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                oBe.favc_npor_imp_igv = Convert.ToDecimal(PorIGV);
                oBe.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                oBe.cntc_icod_contrato_cuotas = Convert.ToInt32(btnCuotas.Tag);
                oBe.cntc_icod_contrato_cuotas_anterior = icodCuotaAnterior;
                oBe.icod_anio = Parametros.intEjercicio;

                oBe.facv_nmonto_credito = Convert.ToDecimal(txtMontoCredito.Text);
                oBe.facv_nmonto_1era_cuota = Convert.ToDecimal(txtMonto1raCuota.Text);

                if (dteFechaPago1raCuota.DateTime == null || dteFechaPago1raCuota.Text == "" || dteFechaPago1raCuota.Text == "01/01/0001")
                {
                    oBe.facv_sfecha_pago_1era_cuota = (DateTime?)null;
                }
                else
                {
                    oBe.facv_sfecha_pago_1era_cuota = Convert.ToDateTime(dteFechaPago1raCuota.EditValue);
                }

                #endregion
                #region Datos de la Planilla
                if (oBePln == null)
                    oBePln = new EPlanillaCobranzaDet();
                oBePln.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBePln.plnd_sfecha_doc = Convert.ToDateTime(dtFechaDoc.EditValue);
                oBePln.plnd_icod_tipo_doc = (rbFactura.Checked) ? 26 : 9;//(Factura = 26; Boleta = 9)
                oBePln.plnd_vnumero_doc = txtSerie.Text + txtNumero.Text;
                oBePln.plnd_nmonto = oBe.favc_nmonto_total;
                oBePln.plnd_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBePln.intUsuario = Valores.intUsuario;
                oBePln.strPc = WindowsIdentity.GetCurrent().Name;
                #endregion

                #region Factura Electronica
                oBe.idDocumento = oBe.favc_vnumero_factura.Remove(4, 8) + '-' + oBe.favc_vnumero_factura.Remove(0, 4).Trim();
                oBe.fechaEmision = oBe.favc_sfecha_factura.ToString();
                oBe.fechaVencimiento = oBe.favc_sfecha_vencim_factura.ToString();
                if (oBePln.plnd_icod_tipo_doc == 26)
                {
                    oBe.tipoDocumento = "01";
                }
                else
                {
                    oBe.tipoDocumento = "03";
                }

                if (Convert.ToInt32(lkpMoneda.EditValue) == 3)
                {
                    oBe.moneda = "PEN";
                }
                else
                {
                    oBe.moneda = "USD";
                }
                oBe.cantidadItems = lstFacturaDetalle.Count;
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.nombreLegalEmisor = "PRADOS VERDES";
                oBe.tipoDocumentoEmisor = "6";
                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.CodLocalEmisor = "0000";
                if (oBePln.plnd_icod_tipo_doc == 26)
                {
                    oBe.nroDocumentoReceptor = txtRUC.Text;
                    oBe.tipoDocumentoReceptor = "6";
                }
                else
                {
                    oBe.nroDocumentoReceptor = txtDNI.Text;
                    oBe.tipoDocumentoReceptor = "1";
                }
                oBe.nombreLegalReceptor = bteCliente.Text;
                oBe.direccionReceptor = txtDireccion.Text;
                oBe.CodMotivoDescuento = 0;
                oBe.PorcDescuento = 0;
                oBe.MontoDescuentoGlobal = 0;
                oBe.BaseMontoDescuento = 0;
                oBe.CodigoTributo = 1000;
                oBe.MontoExonerado = 0;
                if (Convert.ToDecimal(txtIGV.Text) == 0)
                {
                    oBe.MontoInafecto = oBe.favc_nmonto_total;
                    oBe.MontoTotalImpuesto = 0;
                    oBe.MontoGravadasIGV = 0;
                }
                else
                {
                    oBe.MontoInafecto = 0;
                    oBe.MontoTotalImpuesto = oBe.favc_nmonto_imp;
                    oBe.MontoGravadasIGV = oBe.favc_nmonto_neto;
                }

                oBe.MontoGratuitoImpuesto = 0;
                oBe.MontoBaseGratuito = 0;
                oBe.totalIgv = oBe.favc_nmonto_imp;
                oBe.MontoGravadosISC = 0;
                oBe.totalIsc = 0;
                oBe.MontoGravadosOtros = 0;
                oBe.totalOtrosTributos = 0;
                oBe.TotalValorVenta = oBe.favc_nmonto_neto;
                oBe.TotalPrecioVenta = oBe.favc_nmonto_total;
                oBe.MontoDescuento = 0;
                oBe.MontoTotalCargo = 0;
                oBe.MontoTotalAnticipo = 0;
                oBe.ImporteTotalVenta = oBe.favc_nmonto_total;
                oBe.EstadoFacturacion = 4;
                if (Convert.ToInt32(lkpFormaPago.EditValue) == 1)
                {
                    oBe.FormaPagoS = "Contado";
                }
                else if (Convert.ToInt32(lkpFormaPago.EditValue) == 2)
                {
                    oBe.FormaPagoS = "Credito";
                }

                oBe.MontoTotalPago = oBe.facv_nmonto_credito;

                oBe.MontoCuota = oBe.facv_nmonto_1era_cuota;
                oBe.FechaPago = oBe.facv_sfecha_pago_1era_cuota.ToString();
                #endregion
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                    {

                        if (lstFacturaDetalle.Count <= 12)
                        {
                            oBe.favc_icod_factura = new BVentas().insertarFacturaDesdePlanilla(oBe, lstFacturaDetalle, ref ObePlnCab, oBePln, lstPagos, lstContrato);
                        }
                        else
                        {
                            int nroFacturas = Convert.ToInt32(Math.Ceiling((double)(lstFacturaDetalle.Count) / (double)(12)));

                            for (int i = 0; i < nroFacturas; i++)
                            {
                                //DETALLE DE LA FACTURA Y DETALLE DE LA PLANILLA
                                var lstParcial = getFavDetalleParcial(i);
                                oBe.favc_nmonto_total = lstParcial.Sum(x => x.favd_nprecio_total_item);
                                oBe.favc_nmonto_neto = Math.Round((oBe.favc_nmonto_total / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                                oBe.favc_nmonto_imp = oBe.favc_nmonto_total - oBe.favc_nmonto_neto;
                                if (oBePln != null)
                                    oBePln.plnd_nmonto = oBe.favc_nmonto_total;

                                if (i > 0)
                                {

                                    int intCont = 0;
                                    getNroDoc();
                                    oBe.favc_vnumero_factura = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                                    if (oBePln != null)
                                        oBePln.plnd_vnumero_doc = oBe.favc_vnumero_factura;

                                    lstParcial.ForEach(x =>
                                    {
                                        intCont += 1;
                                        x.favd_iitem_factura = intCont;
                                    });
                                }
                                strMasDeUnaFactura = strMasDeUnaFactura + txtSerie.Text + "-" + txtNumero.Text + " ";
                                oBe.favc_icod_factura = new BVentas().insertarFacturaDesdePlanilla(oBe, lstParcial, ref ObePlnCab, oBePln, lstPagos, lstContrato);

                            }
                        }


                    }
                    else if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                    {
                        EBoletaCab oBeBov = getBoleta(oBe);
                        List<EBoletaDet> lstBoletaDetalle = getBoletaDetalle(lstFacturaDetalle);
                        //List<EBoletaDet> lstBoletaDetalle = getBoletaDetallePVT(listaPVT);


                        if (lstBoletaDetalle.Count <= 12)
                        {
                            oBe.favc_icod_factura = new BVentas().insertarBoletaDesdePlanilla(oBeBov, lstBoletaDetalle, ref ObePlnCab, oBePln, lstPagos, lstContrato);
                        }
                        else
                        {
                            int nroFacturas = Convert.ToInt32(Math.Ceiling((double)(lstBoletaDetalle.Count) / (double)(12)));

                            for (int i = 0; i < nroFacturas; i++)
                            {
                                //DETALLE DE LA FACTURA Y DETALLE DE LA PLANILLA
                                var lstParcial = getBovDetalleParcial(i, lstBoletaDetalle);
                                oBeBov.bovc_nmonto_total = lstParcial.Sum(x => x.bovd_nprecio_total_item);
                                oBeBov.bovc_nmonto_neto = Math.Round((oBeBov.bovc_nmonto_total / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2);
                                oBeBov.bovc_nmonto_imp = oBeBov.bovc_nmonto_total - oBeBov.bovc_nmonto_neto;
                                if (oBePln != null)
                                    oBePln.plnd_nmonto = oBe.favc_nmonto_total;

                                if (i > 0)
                                {
                                    //if (ObePlnCab != null)
                                    //    ObePlnCab.intTipoOperacion = 2;
                                    int intCont = 0;
                                    getNroDoc();

                                    oBeBov.bovc_vnumero_boleta = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                                    if (oBePln != null)
                                        oBePln.plnd_vnumero_doc = oBeBov.bovc_vnumero_boleta;

                                    lstParcial.ForEach(x =>
                                    {
                                        intCont += 1;
                                        x.bovd_iitem_boleta = intCont;
                                    });
                                }

                                strMasDeUnaFactura = strMasDeUnaFactura + txtSerie.Text + "-" + txtNumero.Text + " ";
                                oBe.favc_icod_factura = new BVentas().insertarBoletaDesdePlanilla(oBeBov, lstParcial, ref ObePlnCab, oBePln, lstPagos, lstContrato);
                            }
                        }


                        new BVentas().ActualizarContrato(contrato);
                    }

                }
                else
                {
                    if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                        new BVentas().modificarFacturaDesdePlanilla(oBe, lstFacturaDetalle, lstDelete, ObePlnCab, oBePln, lstPagos, lstDeletePagos, lstContrato);
                    else if (oBePln.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                    {
                        EBoletaCab oBeBov = getBoleta(oBe);
                        List<EBoletaDet> lstBoletaDetalle = getBoletaDetalle(lstFacturaDetalle);
                        //List<EBoletaDet> lstBoletaDetalle = getBoletaDetallePVT(listaPVT);
                        List<EBoletaDet> lstDeleteBoletaDet = getBoletaDetalle(lstDelete);
                        new BVentas().modificarBoletaDesdePlanilla(oBeBov, lstBoletaDetalle, lstDeleteBoletaDet, ObePlnCab, oBePln, lstPagos, lstDeletePagos, lstContrato);
                    }
                    lstContrato = lstContrato.Where(x => (x.cntc_npagado > 0) || (x.flag_multiple == true) || (x.cntc_icod_documento > 0) || (x.pgc_icod_pago > 0)).ToList();
                    lstContrato.ForEach(obj =>
                    {
                        obj.cntc_icod_documento = Convert.ToInt32(oBePln.plnd_icod_documento);
                        obj.intUsuario = Valores.intUsuario;
                        obj.cntc_sfecha_pago_cuota = Convert.ToDateTime(dtFechaDoc.EditValue);
                        obj.tdocc_icod_tipo_doc = Convert.ToInt32(oBePln.plnd_icod_tipo_doc);
                        new BVentas().Modificar_Pagos(obj);
                    });
                    new BVentas().ActualizarContrato(Convert.ToInt32(btnContrato.Tag));
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
                    if (strMasDeUnaFactura != "")
                        XtraMessageBox.Show(String.Format("Las {0}´s creadas son: {1}", strMasDeUnaFactura), "Información del Sistema");
                    MiEvento(oBe.favc_icod_factura, ObePlnCab);
                    Close();
                }
            }
        }

        private List<EFacturaDet> getFavDetalleParcial(int intRango)
        {
            List<EFacturaDet> lstParcial = new List<EFacturaDet>();
            switch (intRango)
            {
                case 0:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura <= 12).ToList();
                    break;
                case 1:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 12 && x.favd_iitem_factura <= 24).ToList();
                    break;
                case 2:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 24 && x.favd_iitem_factura <= 36).ToList();
                    break;
                case 3:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 36 && x.favd_iitem_factura <= 48).ToList();
                    break;
                case 4:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 48 && x.favd_iitem_factura <= 60).ToList();
                    break;
                case 5:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 60 && x.favd_iitem_factura <= 72).ToList();
                    break;
                case 6:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 72 && x.favd_iitem_factura <= 84).ToList();
                    break;
                case 7:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 84 && x.favd_iitem_factura <= 96).ToList();
                    break;
                case 8:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 96 && x.favd_iitem_factura <= 108).ToList();
                    break;
                case 9:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 108 && x.favd_iitem_factura <= 120).ToList();
                    break;
                case 10:
                    lstParcial = lstFacturaDetalle.Where(x => x.favd_iitem_factura > 120 && x.favd_iitem_factura <= 132).ToList();
                    break;

            }

            return lstParcial;
        }

        private List<EBoletaDet> getBovDetalleParcial(int intRango, List<EBoletaDet> lstBoletaDet)
        {
            List<EBoletaDet> lstParcial = new List<EBoletaDet>();
            switch (intRango)
            {
                case 0:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta <= 12).ToList();
                    break;
                case 1:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 12 && x.bovd_iitem_boleta <= 24).ToList();
                    break;
                case 2:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 24 && x.bovd_iitem_boleta <= 36).ToList();
                    break;
                case 3:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 36 && x.bovd_iitem_boleta <= 48).ToList();
                    break;
                case 4:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 48 && x.bovd_iitem_boleta <= 60).ToList();
                    break;
                case 5:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 60 && x.bovd_iitem_boleta <= 72).ToList();
                    break;
                case 6:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 72 && x.bovd_iitem_boleta <= 84).ToList();
                    break;
                case 7:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 84 && x.bovd_iitem_boleta <= 96).ToList();
                    break;
                case 8:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 96 && x.bovd_iitem_boleta <= 108).ToList();
                    break;
                case 9:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 108 && x.bovd_iitem_boleta <= 120).ToList();
                    break;
                case 10:
                    lstParcial = lstBoletaDet.Where(x => x.bovd_iitem_boleta > 120 && x.bovd_iitem_boleta <= 132).ToList();
                    break;
            }

            return lstParcial;
        }

        private EBoletaCab getBoleta(EFacturaCab oBeFav)
        {
            EBoletaCab oBeBov = new EBoletaCab();
            oBeBov.bovc_icod_boleta = oBeFav.favc_icod_factura;
            oBeBov.bovc_vnumero_boleta = oBeFav.favc_vnumero_factura;
            oBeBov.bovc_sfecha_boleta = oBeFav.favc_sfecha_factura;
            oBeBov.bovc_sfecha_vencim_boleta = oBeFav.favc_sfecha_vencim_factura;
            oBeBov.cliec_icod_cliente = oBeFav.cliec_icod_cliente;
            oBeBov.clic_vcod_cliente = oBeFav.clic_vcod_cliente;
            oBeBov.bovc_vdireccion_cliente = oBeFav.favc_vdireccion_cliente;
            oBeBov.bovc_vruc = oBeFav.favc_vruc;
            oBeBov.tablc_iid_tipo_moneda = oBeFav.tablc_iid_tipo_moneda;
            oBeBov.tablc_iid_forma_pago = oBeFav.tablc_iid_forma_pago;
            oBeBov.tablc_iid_situacion = oBeFav.tablc_iid_situacion;
            oBeBov.bovc_npor_imp_igv = oBeFav.favc_npor_imp_igv;
            oBeBov.bovc_nmonto_neto = oBeFav.favc_nmonto_neto;
            oBeBov.bovc_nmonto_imp = oBeFav.favc_nmonto_imp;
            oBeBov.bovc_nmonto_total = oBeFav.favc_nmonto_total;
            oBeBov.dxcc_iid_doc_por_cobrar = oBeFav.dxcc_iid_doc_por_cobrar;
            oBeBov.intUsuario = oBeFav.intUsuario;
            oBeBov.strPc = oBe.strPc;
            oBeBov.strDesCliente = oBeFav.strDesCliente;
            oBeBov.bovc_vobservacion = oBeFav.favc_vobservacion;
            oBeBov.desc_nFactura_porc = oBeFav.desc_nFactura_porc;
            oBeBov.desc_nMonto = oBeFav.desc_nMonto;
            oBeBov.nmonto_nDescuento = oBeFav.nmonto_nDescuento;
            oBeBov.pdvc_icod_pedido = oBeFav.pdvc_icod_pedido;
            oBeBov.vendc_icod_vendedor = oBeFav.vendc_icod_vendedor;
            oBeBov.bovc_icod_pvt = oBeFav.favc_icod_pvt;
            oBeBov.doxcc_icod_correlativo = oBeFav.doxcc_icod_correlativo;
            oBeBov.anio_icod_anio = oBeFav.icod_anio;
            oBeBov.bovc_bind_arroz = oBeFav.favc_bind_arroz;
            oBeBov.bovc_nmonto_neto_ivap = oBeFav.favc_nmonto_neto_ivap;
            oBeBov.bovc_npor_imp_ivap = oBeFav.favc_npor_imp_ivap;
            oBeBov.bovc_nmonto_ivap = oBeFav.favc_nmonto_ivap;
            oBeBov.bovc_nmonto_neto_exo = oBeFav.favc_nmonto_neto_exo;
            oBeBov.cntc_icod_contrato = oBeFav.cntc_icod_contrato;
            oBeBov.cntc_icod_contrato_cuotas = oBeFav.cntc_icod_contrato_cuotas;
            oBeBov.cntc_vnumero_contrato = oBeFav.cntc_vnumero_contrato;
            oBeBov.cntc_inro_cuotas = oBeFav.cntc_inro_cuotas;
            oBeBov.cntc_icod_contrato_cuotas_anterior = oBeFav.cntc_icod_contrato_cuotas_anterior;

            oBeBov.nroDocumentoEmisior = oBeFav.nroDocumentoEmisior;
            oBeBov.nombreLegalEmisor = oBeFav.nombreLegalEmisor;
            oBeBov.nombreComercialEmisor = oBeFav.nombreComercialEmisor;
            oBeBov.direccionEmisor = oBeFav.direccionEmisor;
            oBeBov.nroDocumentoReceptor = oBeFav.nroDocumentoReceptor;
            oBeBov.nombreLegalReceptor = oBeFav.nombreLegalReceptor;
            oBeBov.direccionReceptor = oBeFav.direccionReceptor;
            oBeBov.moneda = oBeFav.moneda;
            oBeBov.bovc_vnombre_cliente = oBeFav.bovc_vnombre_cliente;
            oBeBov.bovc_vdireccion_cliente = oBeFav.bovc_vdireccion_cliente;
            oBeBov.bovc_vDNI_cliente = oBeFav.bovc_vDNI_cliente;

            #region Factura Electronica
            oBeBov.idDocumento = oBeFav.idDocumento;
            oBeBov.fechaEmision = oBeFav.fechaEmision;
            oBeBov.fechaVencimiento = oBeFav.fechaVencimiento;
            oBeBov.tipoDocumento = oBeFav.tipoDocumento;
            oBeBov.moneda = oBeFav.moneda;
            oBeBov.cantidadItems = oBeFav.cantidadItems;
            oBeBov.nombreComercialEmisor = oBeFav.nombreComercialEmisor;
            oBeBov.nombreLegalEmisor = oBeFav.nombreLegalEmisor;
            oBeBov.tipoDocumentoEmisor = oBeFav.tipoDocumentoEmisor;
            oBeBov.nroDocumentoEmisior = oBeFav.nroDocumentoEmisior;
            oBeBov.CodLocalEmisor = oBeFav.CodLocalEmisor;
            oBeBov.nroDocumentoReceptor = oBeFav.nroDocumentoReceptor;
            oBeBov.tipoDocumentoReceptor = oBeFav.tipoDocumentoReceptor;
            oBeBov.nombreLegalReceptor = oBeFav.nombreLegalReceptor;
            oBeBov.direccionReceptor = oBeFav.direccionReceptor;
            oBeBov.CodMotivoDescuento = oBeFav.CodMotivoDescuento;
            oBeBov.PorcDescuento = oBeFav.PorcDescuento;
            oBeBov.MontoDescuentoGlobal = oBeFav.MontoDescuentoGlobal;
            oBeBov.BaseMontoDescuento = oBeFav.BaseMontoDescuento;
            oBeBov.MontoTotalImpuesto = oBeFav.MontoTotalImpuesto;
            oBeBov.MontoGravadasIGV = oBeFav.MontoGravadasIGV;
            oBeBov.CodigoTributo = oBeFav.CodigoTributo;
            oBeBov.MontoExonerado = oBeFav.MontoExonerado;
            oBeBov.MontoInafecto = oBeFav.MontoInafecto;
            oBeBov.MontoGratuitoImpuesto = oBeFav.MontoGratuitoImpuesto;
            oBeBov.MontoBaseGratuito = oBeFav.MontoBaseGratuito;
            oBeBov.totalIgv = oBeFav.totalIgv;
            oBeBov.MontoGravadosISC = oBeFav.MontoGravadosISC;
            oBeBov.totalIsc = oBeFav.totalIsc;
            oBeBov.MontoGravadosOtros = oBeFav.MontoGravadosOtros;
            oBeBov.totalOtrosTributos = oBeFav.totalOtrosTributos;
            oBeBov.TotalValorVenta = oBeFav.TotalValorVenta;
            oBeBov.TotalPrecioVenta = oBeFav.TotalPrecioVenta;
            oBeBov.MontoDescuento = oBeFav.MontoDescuento;
            oBeBov.MontoTotalCargo = oBeFav.MontoTotalCargo;
            oBeBov.MontoTotalAnticipo = oBeFav.MontoTotalAnticipo;
            oBeBov.ImporteTotalVenta = oBeFav.ImporteTotalVenta;
            oBeBov.EstadoFacturacion = oBeFav.EstadoFacturacion;
            oBeBov.doc_icod_documento = oBeFav.doc_icod_documento;

            oBeBov.FormaPagoS = oBeFav.FormaPagoS;

            oBeBov.MontoTotalPago = oBeFav.MontoTotalPago;

            oBeBov.NroCuota = oBeFav.NroCuota;

            oBeBov.MontoCuota = oBeFav.MontoCuota;

            oBeBov.FechaPago = oBeFav.FechaPago;



            #endregion
            return oBeBov;
        }
        private EFacturaCab getFactura(EBoletaCab oBeBov_)
        {
            EFacturaCab oBeFav = new EFacturaCab();
            oBeFav.favc_icod_factura = oBeBov_.bovc_icod_boleta;
            oBeFav.favc_vnumero_factura = oBeBov_.bovc_vnumero_boleta;
            oBeFav.favc_sfecha_factura = oBeBov_.bovc_sfecha_boleta;
            oBeFav.favc_sfecha_vencim_factura = oBeBov_.bovc_sfecha_vencim_boleta;
            oBeFav.cliec_icod_cliente = oBeBov_.cliec_icod_cliente;
            oBeFav.cliec_vnumero_doc_cli = oBeBov_.cliec_vnumero_doc_cli;
            oBeFav.cliec_vnombre_cliente = oBeBov_.cliec_vnombre_cliente;
            oBeFav.clic_vcod_cliente = oBeBov_.clic_vcod_cliente;
            oBeFav.favc_vdireccion_cliente = oBeBov_.bovc_vdireccion_cliente;
            oBeFav.favc_vruc = oBeBov_.bovc_vruc;
            oBeFav.tablc_iid_tipo_moneda = oBeBov_.tablc_iid_tipo_moneda;
            oBeFav.tablc_iid_forma_pago = oBeBov_.tablc_iid_forma_pago;
            oBeFav.tablc_iid_situacion = oBeBov_.tablc_iid_situacion;
            oBeFav.favc_npor_imp_igv = oBeBov_.bovc_npor_imp_igv;
            oBeFav.favc_nmonto_neto = oBeBov_.bovc_nmonto_neto;
            oBeFav.favc_nmonto_imp = oBeBov_.bovc_nmonto_imp;
            oBeFav.favc_nmonto_total = oBeBov_.bovc_nmonto_total;
            oBeFav.dxcc_iid_doc_por_cobrar = oBeBov_.dxcc_iid_doc_por_cobrar;
            oBeFav.intUsuario = oBeBov_.intUsuario;
            oBeFav.strPc = oBeBov_.strPc;
            oBeFav.strDesCliente = oBeBov_.strDesCliente;
            oBeFav.favc_vobservacion = oBeBov_.bovc_vobservacion;
            oBeFav.strNroOrdenTrabajo = oBeBov_.strNroOrdenTrabajo;
            oBeFav.desc_nFactura_porc = oBeBov_.desc_nFactura_porc;
            oBeFav.desc_nMonto = oBeBov_.desc_nMonto;
            oBeFav.nmonto_nDescuento = oBeBov_.nmonto_nDescuento;
            oBeFav.pdvc_icod_pedido = oBeBov_.pdvc_icod_pedido;
            oBeFav.vendc_icod_vendedor = oBeBov_.vendc_icod_vendedor;
            oBeFav.favc_icod_pvt = oBeBov_.bovc_icod_pvt;
            oBeFav.doxcc_icod_correlativo = oBeBov_.doxcc_icod_correlativo;
            oBeFav.favc_bind_arroz = oBeBov_.bovc_bind_arroz;
            oBeFav.favc_nmonto_neto_ivap = oBeBov_.bovc_nmonto_neto_ivap;
            oBeFav.favc_npor_imp_ivap = oBeBov_.bovc_npor_imp_ivap;
            oBeFav.favc_nmonto_ivap = oBeBov_.bovc_nmonto_ivap;
            oBeFav.icod_anio = oBeBov_.anio_icod_anio;
            oBeFav.favc_nmonto_neto_exo = oBeBov_.bovc_nmonto_neto_exo;
            oBeFav.cntc_icod_contrato = oBeBov_.cntc_icod_contrato;
            oBeFav.cntc_icod_contrato_cuotas = oBeBov_.cntc_icod_contrato_cuotas;
            oBeFav.cntc_vnumero_contrato = oBeBov_.cntc_vnumero_contrato;
            oBeFav.cntc_inro_cuotas = oBeBov_.cntc_inro_cuotas;
            oBeFav.cntc_icod_contrato_cuotas_anterior = oBeBov_.cntc_icod_contrato_cuotas_anterior;

            oBeFav.nroDocumentoEmisior = oBeBov_.nroDocumentoEmisior;
            oBeFav.nombreLegalEmisor = oBeBov_.nombreComercialEmisor;
            oBeFav.nombreComercialEmisor = oBeBov_.nombreComercialEmisor;
            oBeFav.direccionEmisor = oBeBov_.direccionEmisor;
            oBeFav.nroDocumentoReceptor = oBeBov_.nroDocumentoReceptor;
            oBeFav.nombreLegalReceptor = oBeBov_.nombreLegalReceptor;
            oBeFav.direccionReceptor = oBeBov_.direccionReceptor;
            oBeFav.moneda = oBeBov_.moneda;
            oBeFav.bovc_vnombre_cliente = oBeBov_.bovc_vnombre_cliente;
            oBeFav.bovc_vdireccion_cliente = oBeBov_.bovc_vdireccion_cliente;
            oBeFav.bovc_vDNI_cliente = oBeBov_.bovc_vDNI_cliente;

            #region FacturaElectronica
            oBeFav.idDocumento = oBeBov_.idDocumento;
            oBeFav.fechaEmision = oBeBov_.fechaEmision;
            oBeFav.fechaVencimiento = oBeBov_.fechaVencimiento;
            oBeFav.tipoDocumento = oBeBov_.tipoDocumento;
            oBeFav.moneda = oBeBov_.moneda;
            oBeFav.cantidadItems = oBeBov_.cantidadItems;
            oBeFav.nombreComercialEmisor = oBeBov_.nombreComercialEmisor;
            oBeFav.nombreLegalEmisor = oBeBov_.nombreLegalEmisor;
            oBeFav.tipoDocumentoEmisor = oBeBov_.tipoDocumentoEmisor;
            oBeFav.nroDocumentoEmisior = oBeBov_.nroDocumentoEmisior;
            oBeFav.CodLocalEmisor = oBeBov_.CodLocalEmisor;
            oBeFav.nroDocumentoReceptor = oBeBov_.nroDocumentoReceptor;
            oBeFav.tipoDocumentoReceptor = oBeBov_.tipoDocumentoReceptor;
            oBeFav.nombreLegalReceptor = oBeBov_.nombreLegalReceptor;
            oBeFav.direccionReceptor = oBeBov_.direccionReceptor;
            oBeFav.CodMotivoDescuento = oBeBov_.CodMotivoDescuento;
            oBeFav.PorcDescuento = oBeBov_.PorcDescuento;
            oBeFav.MontoDescuentoGlobal = oBeBov_.MontoDescuentoGlobal;
            oBeFav.BaseMontoDescuento = oBeBov_.BaseMontoDescuento;
            oBeFav.MontoTotalImpuesto = oBeBov_.MontoTotalImpuesto;
            oBeFav.MontoGravadasIGV = oBeBov_.MontoGravadasIGV;
            oBeFav.CodigoTributo = oBeBov_.CodigoTributo;
            oBeFav.MontoExonerado = oBeBov_.MontoExonerado;
            oBeFav.MontoInafecto = oBeBov_.MontoInafecto;
            oBeFav.MontoGratuitoImpuesto = oBeBov_.MontoGratuitoImpuesto;
            oBeFav.MontoBaseGratuito = oBeBov_.MontoBaseGratuito;
            oBeFav.totalIgv = oBeBov_.totalIgv;
            oBeFav.MontoGravadosISC = oBeBov_.MontoGravadosISC;
            oBeFav.totalIsc = oBeBov_.totalIsc;
            oBeFav.MontoGravadosOtros = oBeBov_.MontoGravadosOtros;
            oBeFav.totalOtrosTributos = oBeBov_.totalOtrosTributos;
            oBeFav.TotalValorVenta = oBeBov_.TotalValorVenta;
            oBeFav.TotalPrecioVenta = oBeBov_.TotalPrecioVenta;
            oBeFav.MontoDescuento = oBeBov_.MontoDescuento;
            oBeFav.MontoTotalCargo = oBeBov_.MontoTotalCargo;
            oBeFav.MontoTotalAnticipo = oBeBov_.MontoTotalAnticipo;
            oBeFav.ImporteTotalVenta = oBeBov_.ImporteTotalVenta;
            oBeFav.EstadoFacturacion = oBeBov_.EstadoFacturacion;
            oBeFav.doc_icod_documento = oBeBov_.doc_icod_documento;

            oBeFav.FormaPagoS = oBeBov_.FormaPagoS;

            oBeFav.MontoTotalPago = oBeBov_.MontoTotalPago;

            oBeFav.NroCuota = oBeBov_.NroCuota;

            oBeFav.MontoCuota = oBeBov_.MontoCuota;

            oBeFav.FechaPago = oBeBov_.FechaPago;

            #endregion
            return oBeFav;
        }
        private List<EBoletaDet> getBoletaDetalle(List<EFacturaDet> lstDetFav)
        {
            List<EBoletaDet> lstBovDetalle = new List<EBoletaDet>();
            lstDetFav.ForEach(x =>
            {
                EBoletaDet oBeDetBov = new EBoletaDet();
                oBeDetBov.bovd_icod_item_boleta = x.favd_icod_item_factura;
                oBeDetBov.bovc_icod_boleta = x.favc_icod_factura;
                oBeDetBov.bovd_iitem_boleta = x.favd_iitem_factura;
                oBeDetBov.almac_icod_almacen = x.almac_icod_almacen;
                oBeDetBov.prdc_icod_producto = x.prdc_icod_producto;
                oBeDetBov.strCodProducto = x.strCodProducto;
                oBeDetBov.bovd_ncantidad = x.favd_ncantidad;
                oBeDetBov.bovd_vdescripcion = x.favd_vdescripcion;
                oBeDetBov.bovd_nprecio_unitario_item = x.favd_nprecio_unitario_item;
                oBeDetBov.bovd_nmonto_impuesto_item = x.favd_nmonto_impuesto_item;
                oBeDetBov.bovd_nporcentaje_descuento_item = x.favd_nporcentaje_descuento_item;
                oBeDetBov.bovd_nprecio_total_item = x.favd_nprecio_total_item;
                oBeDetBov.kardc_icod_correlativo = x.kardc_icod_correlativo;
                oBeDetBov.intClasificacionProducto = x.intClasificacionProducto;
                oBeDetBov.bovd_npor_imp_arroz = x.favd_npor_imp_arroz;
                oBeDetBov.bovd_nmonto_imp_arroz = x.favd_nmonto_imp_arroz;
                oBeDetBov.intTipoOperacion = x.intTipoOperacion;
                oBeDetBov.intUsuario = x.intUsuario;
                oBeDetBov.strPc = x.strPc;
                oBeDetBov.flagPlanilla = x.flagPlanilla;
                oBeDetBov.bovd_nneto_ivap = x.favd_nneto_ivap;
                oBeDetBov.bovd_nneto_igv = x.favd_nneto_igv;
                oBeDetBov.bovd_nneto_exo = x.favd_nneto_exo;
                oBeDetBov.dblStockDisponible = x.dblStockDisponible;
                oBeDetBov.prdc_afecto_ivap = x.prdc_afecto_ivap;
                oBeDetBov.prdc_afecto_igv = x.prdc_afecto_igv;
                oBeDetBov.CodigoSUNAT = x.CodigoSUNAT;
                oBeDetBov.bolvd_vobservaciones = x.favd_nobservaciones;
                oBeDetBov.favd_iicod_tipo_pago = x.favd_iicod_tipo_pago;

                #region Factura Electronica Detalle
                oBeDetBov.NumeroOrdenItem = x.NumeroOrdenItem;
                oBeDetBov.cantidad = x.cantidad;
                oBeDetBov.unidadMedida = x.unidadMedida;
                oBeDetBov.ValorVentaItem = x.ValorVentaItem;
                oBeDetBov.CodMotivoDescuentoItem = x.CodMotivoDescuentoItem;
                oBeDetBov.FactorDescuentoItem = x.FactorDescuentoItem;
                oBeDetBov.DescuentoItem = x.DescuentoItem;
                oBeDetBov.BaseDescuentotem = x.BaseDescuentotem;
                oBeDetBov.CodMotivoCargoItem = x.CodMotivoCargoItem;
                oBeDetBov.FactorCargoItem = x.FactorCargoItem;
                oBeDetBov.MontoCargoItem = x.MontoCargoItem;
                oBeDetBov.BaseCargoItem = x.BaseCargoItem;
                oBeDetBov.MontoTotalImpuestosItem = x.MontoTotalImpuestosItem;
                oBeDetBov.MontoImpuestoIgvItem = x.MontoImpuestoIgvItem;
                oBeDetBov.MontoAfectoImpuestoIgv = x.MontoAfectoImpuestoIgv;
                oBeDetBov.PorcentajeIGVItem = x.PorcentajeIGVItem;
                oBeDetBov.MontoInafectoItem = x.MontoInafectoItem;
                oBeDetBov.MontoImpuestoISCItem = x.MontoImpuestoISCItem;
                oBeDetBov.MontoAfectoImpuestoIsc = x.MontoAfectoImpuestoIsc;
                oBeDetBov.PorcentajeISCtem = x.PorcentajeISCtem;
                oBeDetBov.MontoImpuestoIVAPtem = x.MontoImpuestoIVAPtem;
                oBeDetBov.MontoAfectoImpuestoIVAPItem = x.MontoAfectoImpuestoIVAPItem;
                oBeDetBov.PorcentajeIVAPItem = x.PorcentajeIVAPItem;
                oBeDetBov.descripcion = x.descripcion;
                oBeDetBov.codigoItem = x.codigoItem;
                oBeDetBov.ObservacionesItem = x.ObservacionesItem;
                oBeDetBov.ValorUnitarioItem = x.ValorUnitarioItem;
                oBeDetBov.PrecioVentaUnitarioItem = x.PrecioVentaUnitarioItem;
                oBeDetBov.tipoImpuesto = x.tipoImpuesto;
                #endregion
                lstBovDetalle.Add(oBeDetBov);
            });
            return lstBovDetalle;
        }
        private List<EFacturaDet> getFacturaDetalle(List<EBoletaDet> lstDetBov)
        {
            List<EFacturaDet> lstFavDetalle = new List<EFacturaDet>();
            lstDetBov.ForEach(x =>
            {
                EFacturaDet oBeDetFav = new EFacturaDet();
                oBeDetFav.favd_icod_item_factura = x.bovd_icod_item_boleta;
                oBeDetFav.favc_icod_factura = x.bovc_icod_boleta;
                oBeDetFav.favd_iitem_factura = x.bovd_iitem_boleta;
                oBeDetFav.almac_icod_almacen = x.almac_icod_almacen;
                oBeDetFav.prdc_icod_producto = x.prdc_icod_producto;
                oBeDetFav.strCodProducto = x.strCodProducto;
                oBeDetFav.favd_ncantidad = x.bovd_ncantidad;
                oBeDetFav.favd_vdescripcion = x.bovd_vdescripcion;
                oBeDetFav.favd_nprecio_unitario_item = x.bovd_nprecio_unitario_item;
                oBeDetFav.favd_nmonto_impuesto_item = x.bovd_nmonto_impuesto_item;
                oBeDetFav.favd_nporcentaje_descuento_item = x.bovd_nporcentaje_descuento_item;
                oBeDetFav.favd_nprecio_total_item = x.bovd_nprecio_total_item;
                oBeDetFav.kardc_icod_correlativo = x.kardc_icod_correlativo;
                oBeDetFav.intClasificacionProducto = x.intClasificacionProducto;
                oBeDetFav.favd_npor_imp_arroz = x.bovd_npor_imp_arroz;
                oBeDetFav.favd_nmonto_imp_arroz = x.bovd_nmonto_imp_arroz;
                oBeDetFav.intTipoOperacion = x.intTipoOperacion;
                oBeDetFav.intUsuario = x.intUsuario;
                oBeDetFav.strPc = x.strPc;
                oBeDetFav.flagPlanilla = x.flagPlanilla;
                oBeDetFav.strCodProducto = x.strCodProducto;
                oBeDetFav.strDesProducto = x.strDesProducto;
                oBeDetFav.strLinea = x.strLinea;
                oBeDetFav.strSubLinea = x.strSubLinea;
                oBeDetFav.strDesUM = x.strDesUM;
                oBeDetFav.strAlmacen = x.strAlmacen;
                oBeDetFav.favd_nneto_ivap = x.bovd_nneto_ivap;
                oBeDetFav.favd_nneto_igv = x.bovd_nneto_igv;
                oBeDetFav.favd_nneto_exo = x.bovd_nneto_exo;
                oBeDetFav.dblStockDisponible = x.dblStockDisponible;
                oBeDetFav.prdc_afecto_ivap = x.prdc_afecto_ivap;
                oBeDetFav.prdc_afecto_igv = x.prdc_afecto_igv;
                oBeDetFav.CodigoSUNAT = x.CodigoSUNAT;
                oBeDetFav.favd_nobservaciones = x.bolvd_vobservaciones;
                oBeDetFav.favd_iicod_tipo_pago = x.favd_iicod_tipo_pago;
                oBeDetFav.strTipoServicio = x.strTipoServicio;
                #region Factura Electronica Detalle
                oBeDetFav.NumeroOrdenItem = x.NumeroOrdenItem;
                oBeDetFav.cantidad = x.cantidad;
                oBeDetFav.unidadMedida = x.unidadMedida;
                oBeDetFav.ValorVentaItem = x.ValorVentaItem;
                oBeDetFav.CodMotivoDescuentoItem = x.CodMotivoDescuentoItem;
                oBeDetFav.FactorDescuentoItem = x.FactorDescuentoItem;
                oBeDetFav.DescuentoItem = x.DescuentoItem;
                oBeDetFav.BaseDescuentotem = x.BaseDescuentotem;
                oBeDetFav.CodMotivoCargoItem = x.CodMotivoCargoItem;
                oBeDetFav.FactorCargoItem = x.FactorCargoItem;
                oBeDetFav.MontoCargoItem = x.MontoCargoItem;
                oBeDetFav.BaseCargoItem = x.BaseCargoItem;
                oBeDetFav.MontoTotalImpuestosItem = x.MontoTotalImpuestosItem;
                oBeDetFav.MontoImpuestoIgvItem = x.MontoImpuestoIgvItem;
                oBeDetFav.MontoAfectoImpuestoIgv = x.MontoAfectoImpuestoIgv;
                oBeDetFav.PorcentajeIGVItem = x.PorcentajeIGVItem;
                oBeDetFav.MontoInafectoItem = x.MontoInafectoItem;
                oBeDetFav.MontoImpuestoISCItem = x.MontoImpuestoISCItem;
                oBeDetFav.MontoAfectoImpuestoIsc = x.MontoAfectoImpuestoIsc;
                oBeDetFav.PorcentajeISCtem = x.PorcentajeISCtem;
                oBeDetFav.MontoImpuestoIVAPtem = x.MontoImpuestoIVAPtem;
                oBeDetFav.MontoAfectoImpuestoIVAPItem = x.MontoAfectoImpuestoIVAPItem;
                oBeDetFav.PorcentajeIVAPItem = x.PorcentajeIVAPItem;
                oBeDetFav.descripcion = x.descripcion;
                oBeDetFav.codigoItem = x.codigoItem;
                oBeDetFav.ObservacionesItem = x.ObservacionesItem;
                oBeDetFav.ValorUnitarioItem = x.ValorUnitarioItem;
                oBeDetFav.PrecioVentaUnitarioItem = x.PrecioVentaUnitarioItem;
                oBeDetFav.tipoImpuesto = x.tipoImpuesto;
                #endregion
                lstFavDetalle.Add(oBeDetFav);
            });
            return lstFavDetalle;
        }


        private void getNroDoc()
        {
            try
            {


                if (rbFactura.Checked == true)
                {
                    var lst = new BVentas().getCorrelativoRP(1);

                    txtSerie.Text = lst[0].rgpmc_vserie_factura;
                    txtNumero.Text = (Convert.ToInt32(lst[0].rgpmc_icorrelativo_factura) + 1).ToString();
                }

                if (rbBoleta.Checked == true)
                {
                    var lst = new BVentas().getCorrelativoRP(1);

                    txtSerie.Text = lst[0].rgpmc_vserie_boleta;
                    txtNumero.Text = (Convert.ToInt32(lst[0].rgpmc_icorrelativo_boleta) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public class TipoDoc
        {
            public int intCodigo { get; set; }
            public string strTipoDoc { get; set; }
        }

        private void clearControls()
        {
            bteCliente.Tag = null;
            bteCliente.Text = String.Empty;
            txtDNI.Text = String.Empty;
            txtRUC.Text = String.Empty;
            txtDireccion.Text = String.Empty;
        }

        private void listarVentaDirecta()
        {
            using (frmListarVentaDirecta frm = new frmListarVentaDirecta())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle.Clear();
                    viewDetalle.RefreshData();
                    clearControls();
                    /*Cabecera*/
                    EVentaDirecta oBeVD = frm._Be;
                    bteCliente.Tag = oBeVD.dvdc_icod_cliente;
                    bteCliente.Text = oBeVD.strDesCliente;
                    txtDNI.Text = oBeVD.dvdc_vdni;
                    txtRUC.Text = oBeVD.dvdc_vruc;
                    txtDireccion.Text = oBeVD.dvdc_vdireccion_cliente;
                    lkpMoneda.EditValue = oBeVD.tablc_iid_tipo_moneda;
                    //bteNroDoc.Tag = oBeVD.dvdc_icod_doc_venta_directa;
                    //bteNroDoc.Text = oBeVD.dvdc_vnumero_doc_venta_directa;
                    /*Detalle*/
                    var lstVDDetalle = new BVentas().listarVentaDirectaDetalle(oBeVD.dvdc_icod_doc_venta_directa);
                    int cont = 0;
                    lstVDDetalle.ForEach(x =>
                    {
                        cont += 1;
                        EFacturaDet oBeDetFav = new EFacturaDet();
                        oBeDetFav.favd_iitem_factura = cont;
                        oBeDetFav.almac_icod_almacen = x.dvdd_iid_almacen;
                        oBeDetFav.prdc_icod_producto = x.dvdd_iid_producto;
                        oBeDetFav.favd_ncantidad = x.dvdd_ncantidad;
                        oBeDetFav.favd_vdescripcion = x.strDesProducto;
                        oBeDetFav.favd_nprecio_unitario_item = x.dvdd_nprecio_unitario_item;
                        //oBeDetFav.favd_nmonto_impuesto_item = ;
                        oBeDetFav.favd_nprecio_total_item = x.dvdd_nprecio_total_item;
                        //oBeDetFav.favd_icod_kardex = ;
                        oBeDetFav.intClasificacionProducto = 1;
                        oBeDetFav.intTipoOperacion = 1;
                        oBeDetFav.intUsuario = x.intUsuario;
                        oBeDetFav.strPc = x.strPc;
                        oBeDetFav.strCodProducto = x.strCodProducto;
                        oBeDetFav.strDesProducto = x.strDesProducto;
                        //oBeDetFav.strLinea = x.strLinea;
                        //oBeDetFav.strSubLinea = x.strSubLinea;
                        oBeDetFav.strDesUM = x.strDesUM;
                        oBeDetFav.strAlmacen = x.strAlmacen;
                        oBeDetFav.strMoneda = lkpMoneda.Text;
                        oBeDetFav.flagPlanilla = true;
                        lstFacturaDetalle.Add(oBeDetFav);
                    });
                    /**/
                    grdDetalle.DataSource = lstFacturaDetalle;
                    viewDetalle.RefreshData();
                    setTotales();
                    mnu.Enabled = false;
                }
            }
        }

        private void bteNroOT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //listarOT();
        }

        private void bteNroDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVentaDirecta();
        }

        private void nuevoItem()
        {
            using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
            {
                frm.txtDescuento.Visible = false;
                frm.lblDescuento.Visible = false;
                frm.SetInsert();
                frm.lstFacturaDetalle = lstFacturaDetalle;
                //frm.flag_Arrox = ChkIndArroz.Checked;
                //frm.valor_IGV = Convert.ToDecimal(txtPorcentIGV.Text);
                frm.txtMoneda.Text = lkpMoneda.Text;
                frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //if (Convert.ToDecimal(frm.PorIVAP) != 0)
                    //{
                    //    PorIVAP = frm.PorIVAP;
                    //}
                    //if (Convert.ToDecimal(frm.PorIGV) != 0)
                    //{
                    //    PorIGV = frm.PorIGV;
                    //}
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.MoveLast();
                    setTotales();

                }
            }

        }

        private void nuevoServicio()
        {
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.situacion = situacion;
                frm.lstContrato = lstContrato;
                frm.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                frm.SetInsert();
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                frm.IGV = Convert.ToDecimal(txtIGV.Text);
                frm.txtCantidad.Text = (Math.Round(Convert.ToDecimal(1))).ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    lstContrato = frm.lstContrato;
                    viewDetalle.RefreshData();
                    viewDetalle.MoveLast();
                    setTotales();

                }
            }
        }

        private void modificarItem()
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFacturaDetalle frm = new frmManteFacturaDetalle())
            {
                frm.txtDescuento.Visible = false;
                frm.lblDescuento.Visible = false;
                frm.obe = obe;
                frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
                frm.flag_afecto_ivap = obe.prdc_afecto_ivap;
                frm.flag_afecto_igv = obe.prdc_afecto_igv;
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.SetModify();
                frm.txtMoneda.Text = lkpMoneda.Text;
                frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.MoveLast();
                    setTotales();
                }
            }
        }

        private void modificarServicio()
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.lstContrato = lstContrato;
                frm.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                frm.obe = obe;
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.SetModify();
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.MoveLast();
                    setTotales();
                }
            }
        }

        private void eliminarItem()
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFacturaDetalle.Remove(obe);
            //txtMontoTotal.Text = listaPVT[0].pdvd_nprecio_total.ToString();
            viewDetalle.RefreshData();
            setTotales();
        }

        private void nuevoPago()
        {
            using (frmPagoPlanilla frm = new frmPagoPlanilla())
            {
                frm.oBe.pgoc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                frm.oBe.pgoc_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                frm.oBe.tdocc_icoc_tipo_documento = Parametros.intTipoDocPlanillaVenta;
                frm.oBe.pgoc_vnumero_planilla = ObePlnCab.plnc_vnumero_planilla;
                frm.dblTotal = Convert.ToDecimal(txtResta.Text);
                frm.monto = listaPVT.Sum(x => x.pdvd_nprecio_total);
                frm.lstPagos = lstPagos;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstPagos = frm.lstPagos;
                    viewPagos.RefreshData();
                    setTotales();
                }
            }
        }

        private void modificarPago()
        {
            EPagoDocVenta oBe = (EPagoDocVenta)viewPagos.GetRow(viewPagos.FocusedRowHandle);
            if (oBe == null)
                return;
            using (frmPagoPlanilla frm = new frmPagoPlanilla())
            {
                frm.dblTotal = Convert.ToDecimal(txtResta.Text);
                frm.lstPagos = lstPagos;
                frm.oBe = oBe;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstPagos = frm.lstPagos;
                    viewPagos.RefreshData();
                    setTotales();
                }
            }
        }

        private void eliminarPago()
        {
            EPagoDocVenta oBe = (EPagoDocVenta)viewPagos.GetRow(viewPagos.FocusedRowHandle);
            if (oBe == null)
                return;
            if (XtraMessageBox.Show("¿Está seguro que desea eliminar el pago?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstPagos.Remove(oBe);
                setTotales();
                viewPagos.RefreshData();
            }

        }

        private void setTotales()
        {
            if (Convert.ToDecimal(txtIGV.Text) == 0)
            {
                txtMontoTotal.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                txtmonto.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                txtMontoNeto.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
            }
            else
            {

                txtmonto.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                txtMontoNeto.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                txtMontoIGV.Text = Math.Round(Convert.ToDecimal((lstFacturaDetalle.Sum(ob => ob.favd_nprecio_total_item) * Convert.ToDecimal(Parametros.strPorcIGV)) / 100), 2, MidpointRounding.ToEven).ToString();
                txtMontoTotal.Text = txtMontoNeto.Text + txtMontoIGV.Text;
            }


            /****************************************************************************************************/
            decimal dcmlMontoPagado = 0;
            decimal dcmlMontoAux = 0;
            if (lstPagos.Where(x => x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares).ToList().Count > 0)
            {
                lstPagos.ForEach(x =>
                {
                    dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares) ? x.pgoc_nmonto * x.pgoc_tipo_cambio : x.pgoc_nmonto;
                    dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
                });
                txtPago.Text = dcmlMontoPagado.ToString();
            }
            else
                txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto).ToString();
            /****************************************************************************************************/
            txtResta.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtPago.Text)).ToString();

        }
        private void setTotalesPVT()
        {

            //txtsubTotalVenta.Text = listaPVT.Sum(x => x.pdvd_nprecio_total).ToString();
            //txttotaldescuento.Text = (Convert.ToDecimal(txtmonto.Text).ToString());
            txtMontoTotal.Text = listaPVT.Sum(x => x.pdvd_nprecio_total).ToString();

            /****************************************************************************************************/
            decimal dcmlMontoPagado = 0;
            decimal dcmlMontoAux = 0;
            if (lstPagos.Where(x => x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares).ToList().Count > 0)
            {
                lstPagos.ForEach(x =>
                {
                    dcmlMontoAux = (x.pgoc_icod_tipo_moneda == Parametros.intTipoMonedaDolares) ? x.pgoc_nmonto * x.pgoc_tipo_cambio : x.pgoc_nmonto;
                    dcmlMontoPagado = dcmlMontoPagado + dcmlMontoAux;
                });
                txtPago.Text = dcmlMontoPagado.ToString();
            }
            else
                txtPago.Text = lstPagos.Sum(x => x.pgoc_nmonto).ToString();
            /****************************************************************************************************/
            txtResta.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtPago.Text)).ToString();

        }
        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {


                        if (rbFactura.Checked == true)
                        {
                            if (frm._Be.cliec_cruc.Trim().Length != 11 || string.IsNullOrEmpty(frm._Be.cliec_cruc.Trim()))
                            {
                                throw new ArgumentException($"El cliente {frm._Be.cliec_vnombre_cliente} posee el RUC inválido");
                            }
                            txtRUC.Text = frm._Be.cliec_cruc;
                        }
                        else
                        {
                            if (frm._Be.cliec_vnumero_doc_cli.Trim().Length != 8)
                            {
                                throw new ArgumentException($"El cliente {frm._Be.cliec_vnombre_cliente} posee el número de DNI inválido");
                            }
                            txtDNI.Text = frm._Be.cliec_vnumero_doc_cli;
                        }

                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtNombre.Text = frm._Be.cliec_vnombre_cliente;
                        txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                bteCliente.Tag = _Be.cliec_icod_cliente;
                bteCliente.Text = _Be.cliec_vnombre_cliente;
                txtDireccion.Text = _Be.cliec_vdireccion_cliente;
                txtRUC.Text = _Be.cliec_cruc;
                //txtTelefono.Text = _Be.cliec_vnro_telefono;
                //strCodCliente = _Be.cliec_vcod_cliente;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listarVehiculo()
        {
            BaseEdit oBase = null;
            try
            {


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
            }
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btePlaca_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVehiculo();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoItem();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.intClasificacionProducto != Parametros.intTipoPrdServicio)
                modificarServicio();
            //modificarItem();


        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminarItem();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nuevoPago();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            modificarPago();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            eliminarPago();
        }





        public void getTipoCambio()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lstTipoCambio = new BAdministracionSistema().listarTipoCambio();

                var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFechaDoc.EditValue).ToShortDateString()).ToList();
                Lista.ForEach(obe =>
                {
                    txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
                });
                //if (lstDetalle.Count > 0)
                //    recalcular();
            }
        }

        private void bteRefreshTipoCambio_Click(object sender, EventArgs e)
        {
            getTipoCambio();
        }

        private void cbPucusana_CheckedChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }




        private void listarProducto()
        {
            EPedidosPVTDetalle item = (EPedidosPVTDetalle)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (item == null)
                return;
            try
            {
                frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen();
                List<EAlmacen> lstAlmcen = new List<EAlmacen>();
                lstAlmcen = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_pvt == Valores.rgpmc_icod_registro_parametro).ToList();
                frm.intAlmacen = lstAlmcen[0].almac_icod_almacen;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    item.prdc_icod_producto = frm._Be.prdc_icod_producto;
                    item.DesLarga = frm._Be.strDesProducto;
                    //item.DesCorta = frm._Be.strDesProducto;
                    item.AbreUM = frm._Be.strDesUM;
                    item.pdvd_nprecio_unitario = frm._Be.prdc_nPrecio_soles;
                    item.pdvd_nprecio_total = Math.Round(Convert.ToDecimal(item.pdvd_nprecio_unitario * item.pdvd_ncantidad), 2);
                    item.Indicador = 1;
                    indicador_ivap = Convert.ToBoolean(frm._Be.AfectoIVAP);
                    porcentaje_ivap = Convert.ToDecimal(frm._Be.PorcentajeIVAP);
                    //CalcularPrecio();
                    grdDetalle.Refresh();
                    grdDetalle.RefreshDataSource();
                    /**/
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }



        private void bteCliente_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void rbFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void rbBoleta_CheckedChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
                if (rbBoleta.Checked == true)
                {
                    txtNombre.Enabled = true;
                    txtDireccion.Enabled = true;
                    txtDNI.Enabled = true;
                }
                else
                {
                    txtDNI.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtNombre.Enabled = false;
                }
            }

        }

        private void bteVendedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVendedor();
        }
        private void listarVendedor()
        {
            try
            {
                using (FrmListarVendedor frm = new FrmListarVendedor())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteVendedor.Tag = frm._Be.vendc_icod_vendedor;
                        bteVendedor.Text = frm._Be.vendc_vnombre_vendedor;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnContrato_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarContrato();
        }

        private void listarContrato()
        {
            using (FrmListarContrato frm = new FrmListarContrato())
            {

                frm.ingresaCliente = false;
                frm.txtNroContrato.Text = btnContrato.Text;
                frm.simple = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnContrato.Tag = frm._Be.cntc_icod_contrato;
                    btnContrato.Text = frm._Be.cntc_vnumero_contrato;

                }
            }
        }

        private void btnCuotas_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCuotas();
        }
        public List<EContratoCuotas> lstContrato = new List<EContratoCuotas>();
        private void listarCuotas()
        {
            try
            {
                using (FrmListarCuota frm = new FrmListarCuota())
                {
                    frm.situacion = situacion;
                    frm.lstContrato = lstContrato.OrderBy(x => x.strTipoCredito).ToList();
                    frm.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                    frm.flag_multiple = true;
                    frm.View = View;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstContrato = frm.lstContrato;
                        lblMontoPagarCuota.Text = string.Format("Monto Pagar Cuotas: {0}", frm.txtMontoCuota.Text);
                        lblMontoPagarMora.Text = string.Format("Monto Pagar Mora : {0}", frm.txtMontoMora.Text);
                        if (lstContrato.Sum(x => x.monto_pagar) > 0 || lstContrato.Sum(x => x.cntc_nmonto_mora) > 0)
                        {
                            groupControl2.Height = 228;
                        }
                        else
                        {
                            groupControl2.Height = 200;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void lkpFormaPago_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpFormaPago.EditValue) == 2)
            {
                txtMontoCredito.Enabled = true;
                txtMonto1raCuota.Enabled = true;
                dteFechaPago1raCuota.Enabled = true;
            }
            else if (Convert.ToInt32(lkpFormaPago.EditValue) == 1)
            {
                txtMontoCredito.Enabled = false;
                txtMonto1raCuota.Enabled = false;
                dteFechaPago1raCuota.Enabled = false;

                txtMontoCredito.Text = "";
                txtMonto1raCuota.Text = "";
                dteFechaPago1raCuota.EditValue = "";

            }
        }


        private void txtmonto_EditValueChanged_1(object sender, EventArgs e)
        {
            txtMontoTotal.Text = txtmonto.Text;
        }

    }
}