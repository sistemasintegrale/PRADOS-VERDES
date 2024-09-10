using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteContratoWeb : XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContratoWeb Obe = new EContratoWeb();
        public List<EContratoCuotas> lstCuotas = new List<EContratoCuotas>();
        public frmManteContratoWeb() => InitializeComponent();
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

            if (Status == BSMaintenanceStatus.View)
            {
                Services.DeshabilitarControles(this, btnGuardar);
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.ReadOnly = true;
                txtNumer.ReadOnly = true;
            }
        }
        public void setValues()
        {
            txtSerie.Text = Obe.cntc_vnumero_contrato.Substring(0, 4);
            txtNumer.Text = Obe.cntc_vnumero_contrato.Substring(4);
            dtFecha.EditValue = Obe.cntc_sfecha_contrato;
            lkpSituacion.EditValue = Obe.cntc_icod_situacion;
            lkpNombreIEC.EditValue = Obe.cntc_icod_vendedor;
            lkpOrigenVenta.EditValue = Obe.cntc_iorigen_venta;
            btnFunerarios.Tag = Obe.cntc_icod_funeraria;
            lkpTipoPrestamo.EditValue = Obe.cntc_itipo_doc_prestamo;
            txtNombreFallecido.Text = Obe.cntc_vnombre_fallecido;
            lkpTipoDocFallecido.EditValue = Obe.cntc_itipo_documento_fallecido;
            txtDocFallecido.Text = Obe.cntc_vdocumento_fallecido;
            txtApePatFallecido.Text = Obe.cntc_vapellido_paterno_fallecido;
            dteFechaNacFallecido.EditValue = Obe.cntc_sfecha_nac_fallecido;
            txtApeMatFallecido.Text = Obe.cntc_vapellido_materno_fallecido;
            lkpNacionalidadFallecido.EditValue = Obe.cntc_inacionalidad;
            dteFechaFallecimiento.EditValue = Obe.cntc_sfecha_fallecimiento;
            dteFechaEntierro.EditValue = Obe.cntc_sfecha_entierro;
            lkpReligion.EditValue = Obe.cntc_icod_religiones;
            lkpTipoDeseso.EditValue = Obe.cntc_icod_tipo_deceso;
            txtObservacion.Text = Obe.cntc_vobservacion;
            lkpTipoDocContratante1.EditValue = Obe.cntc_itipo_documento_contratante;
            txtDocContratante1.Text = Obe.cntc_vdocumento_contratante;
            txtRucContratante1.Text = Obe.cntc_vruc_contratante;
            txtNombreContratante1.Text = Obe.cntc_vnombre_contratante;
            txtApellidoPatContratante1.Text = Obe.cntc_vapellido_paterno_contratante;
            txtApellidoMatContratante1.Text = Obe.cntc_vapellido_materno_contratante;
            dteFechaNacimientoContratante1.EditValue = Obe.cntc_sfecha_nacimineto_contratante;
            txtCorreoContrante1.Text = Obe.cntc_vdireccion_correo_contratante;
            txtTelefonoContrante1.Text = Obe.cntc_vtelefono_contratante;
            txtDireccionContrante1.Text = Obe.cntc_vdireccion_correo_contratante;
            lkpNacionalidadContrante1.EditValue = Obe.cntc_inacionalidad_contratante;
            lkpEstadoCivilContrante1.EditValue = Obe.cntc_iestado_civil_contratante;
            lkpParentescoContratante1.EditValue = Obe.cntc_iparentesco_contratante;
            txtParentescoContrante1.Text = Obe.cntc_vparentesco_contratante;
            lkpTipoDocContrante2.EditValue = Obe.cntc_itipo_documento_contratante2;
            txtDocContratante2.Text = Obe.cntc_vdocumento_contratante2;
            txtRucContratante2.Text = Obe.cntc_vruc_contratante2;
            txtNombreContratante2.Text = Obe.cntc_vnombre_contratante2;
            txtApellidoPatContratante2.Text = Obe.cntc_vapellido_paterno_contratante2;
            txtApellidoMatContratante2.Text = Obe.cntc_vapellido_materno_contratante2;
            dteFechaNacimientoContratante2.EditValue = Obe.cntc_sfecha_nacimineto_contratante2;
            txtCorreoContrante2.Text = Obe.cntc_vdireccion_correo_contratante2;
            txtTelefonoContrante2.Text = Obe.cntc_vtelefono_contratante2;
            txtDireccionContrante2.Text = Obe.cntc_vdireccion_correo_contratante2;
            lkpNacionalidadContrante2.EditValue = Obe.cntc_inacionalidad_contratante2;
            lkpEstadoCivilContrante2.EditValue = Obe.cntc_iestado_civil_contratante2;
            lkpParentescoContescoContratante2.EditValue = Obe.cntc_iparentesco_contratante2;
            txtParentescoContrante2.Text = Obe.cntc_vparentesco_contratante2;
            lkpCodigoPlan.EditValue = Obe.cntc_icodigo_plan;
            lkpTipoNecesidad.EditValue = Obe.cntc_icod_nombre_plan;
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
            lkpTipoPago.EditValue = Obe.cntc_itipo_pago;
            lkpDocumentoFinancia.EditValue = Obe.cntc_idocumento_financiado;
            lkpPlataforma.EditValue = Obe.cntc_icod_plataforma;
            txtPrecioLista.Text = Obe.cntc_nprecio_lista.ToString();
            txtDescuento.Text = Obe.cntc_ndescuento.ToString();
            txtInhumacion.Text = Obe.cntc_ninhumacion.ToString();
            lkpDeceso.EditValue = Obe.cntc_icod_deceso;
            txtPagoCovid19.Text = Obe.cntc_npago_covid19.ToString();
            txtFomaMantenimiento.Text = Obe.cntc_naporte_fondo.ToString();
            txtIGV.Text = Obe.cntc_nIGV.ToString();
            txtFinanciamiento.Text = Obe.cntc_nfinanciamientro.ToString();
            
            txtCuotaInicial.EditValue = Obe.cntc_ncuota_inicial;
            txtCuotas.Text = Obe.cntc_inro_cuotas.ToString();
            txtMontoCuotas.Text = Obe.cntc_nmonto_cuota.ToString();
            dteFechaCuota.EditValue = Obe.cntc_sfecha_cuota;
            dteFinPago.EditValue = Obe.cntc_sfecha_fin_pago;
            btnFunerarios.Text = Obe.Funeraria;
           
            txtCapacidadTotal.Text = Obe.cntc_vcapacidad_total;
            txtCapacidadContratada.Text = Obe.cntc_vcapacidad_contrato;
            txtSaldo.Text = Obe.cntc_nsaldo.ToString();
            txtPrecioTotal.Text = Obe.cntc_nprecio_total.ToString();
            txtUrnas.Text = Obe.cntc_vurnas;
            txtServicioIhumacion.Text = Obe.cntc_vservico_inhumacion;
          
            lkpTipoContrato.EditValue = Obe.cntc_icod_tipo_contrato;
            var cuotas = new BVentas().listarContratoCuotas(Obe.cntc_icod_contrato);
            if (cuotas.Exists(x => x.cntc_icod_situacion != (int)TableVenta.EstadoCuota.Pendiente))
            {
                Services.DeshabilitarControles(groupBox1);
                Services.DeshabilitarControles(groupBox7);
                Services.DeshabilitarControles(groupBox6);
            }

        }
        public void SetInsert() => Status = BSMaintenanceStatus.CreateNew;

        public void SetCancel() => Status = BSMaintenanceStatus.View;

        public void SetModify() => Status = BSMaintenanceStatus.ModifyCurrent;
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                Obe.cntc_vnumero_contrato = txtSerie.Text + txtNumer.Text;
                Obe.cntc_sfecha_contrato = (DateTime?)dtFecha.EditValue;
                Obe.cntc_icod_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.cntc_icod_vendedor = Convert.ToInt32(lkpNombreIEC.EditValue);
                Obe.cntc_iorigen_venta = Convert.ToInt32(lkpOrigenVenta.EditValue);
                Obe.cntc_icod_funeraria = Convert.ToInt32(btnFunerarios.Tag);
                Obe.cntc_itipo_doc_prestamo = Convert.ToInt32(lkpTipoPrestamo.EditValue);
                Obe.cntc_vnombre_fallecido = txtNombreFallecido.Text;
                Obe.cntc_itipo_documento_fallecido = Convert.ToInt32(lkpTipoDocFallecido.EditValue);
                Obe.cntc_vdocumento_fallecido = txtDocFallecido.Text;
                Obe.cntc_vapellido_paterno_fallecido = txtApePatFallecido.Text;
                Obe.cntc_sfecha_nac_fallecido = (DateTime?)dteFechaNacFallecido.EditValue;
                Obe.cntc_vapellido_materno_fallecido = txtApeMatFallecido.Text;
                Obe.cntc_inacionalidad = Convert.ToInt32(lkpNacionalidadFallecido.EditValue);
                Obe.cntc_sfecha_fallecimiento = (DateTime?)dteFechaFallecimiento.EditValue;
                Obe.cntc_sfecha_entierro = (DateTime?)dteFechaEntierro.EditValue;
                Obe.cntc_icod_religiones = Convert.ToInt32(lkpReligion.EditValue);
                Obe.cntc_icod_tipo_deceso = Convert.ToInt32(lkpTipoDeseso.EditValue);
                Obe.cntc_vobservacion = txtObservacion.Text;
                Obe.cntc_itipo_documento_contratante = Convert.ToInt32(lkpTipoDocContratante1.EditValue);
                Obe.cntc_vdocumento_contratante = txtDocContratante1.Text;
                Obe.cntc_vruc_contratante = txtRucContratante1.Text;
                Obe.cntc_vnombre_contratante = txtNombreContratante1.Text;
                Obe.cntc_vapellido_paterno_contratante = txtApellidoPatContratante1.Text;
                Obe.cntc_sfecha_nacimineto_contratante = (DateTime?)dteFechaNacimientoContratante1.EditValue;
                Obe.cntc_vdireccion_correo_contratante = txtCorreoContrante1.Text;
                Obe.cntc_vtelefono_contratante = txtTelefonoContrante1.Text;
                Obe.cntc_vdireccion_correo_contratante = txtDireccionContrante1.Text;
                Obe.cntc_inacionalidad_contratante = Convert.ToInt32(lkpNacionalidadContrante1.EditValue);
                Obe.cntc_iestado_civil_contratante = Convert.ToInt32(lkpEstadoCivilContrante1.EditValue);
                Obe.cntc_iparentesco_contratante = Convert.ToInt32(lkpParentescoContratante1.EditValue);
                Obe.cntc_vparentesco_contratante = txtParentescoContrante1.Text;
                Obe.cntc_itipo_documento_contratante2 = Convert.ToInt32(lkpTipoDocContrante2.EditValue);
                Obe.cntc_vdocumento_contratante2 = txtDocContratante2.Text;
                Obe.cntc_vruc_contratante2 = txtRucContratante2.Text;
                Obe.cntc_vnombre_contratante2 = txtNombreContratante2.Text;
                Obe.cntc_vapellido_paterno_contratante2 = txtApellidoPatContratante2.Text;
                Obe.cntc_sfecha_nacimineto_contratante2 = (DateTime?)dteFechaNacimientoContratante2.EditValue;
                Obe.cntc_vdireccion_correo_contratante2 = txtCorreoContrante2.Text;
                Obe.cntc_vtelefono_contratante2 = txtTelefonoContrante2.Text;
                Obe.cntc_vdireccion_correo_contratante2 = txtDireccionContrante2.Text;
                Obe.cntc_inacionalidad_contratante2 = Convert.ToInt32(lkpNacionalidadContrante2.EditValue);
                Obe.cntc_iestado_civil_contratante2 = Convert.ToInt32(lkpEstadoCivilContrante2.EditValue);
                Obe.cntc_iparentesco_contratante2 = Convert.ToInt32(lkpParentescoContescoContratante2.EditValue);
                Obe.cntc_vparentesco_contratante2 = txtParentescoContrante2.Text;
                Obe.cntc_icodigo_plan = Convert.ToInt32(lkpCodigoPlan.EditValue);
                Obe.cntc_icod_nombre_plan = Convert.ToInt32(lkpTipoNecesidad.EditValue);
                Obe.cntc_itipo_sepultura = Convert.ToInt32(lkpTipoSepultura.EditValue);
                Obe.cntc_nprecio_lista = Convert.ToDecimal(txtPrecioLista.Text);
                Obe.cntc_ndescuento = Convert.ToDecimal(txtDescuento.Text);
                Obe.cntc_ninhumacion = Convert.ToDecimal(txtInhumacion.Text);
                Obe.cntc_icod_deceso = Convert.ToInt32(lkpDeceso.EditValue);
                Obe.cntc_npago_covid19 = Convert.ToDecimal(txtPagoCovid19.Text);
                Obe.cntc_naporte_fondo = Convert.ToDecimal(txtFomaMantenimiento.Text);
                Obe.cntc_nIGV = Convert.ToDecimal(txtIGV.Text);
                Obe.cntc_nfinanciamientro = Convert.ToDecimal(txtFinanciamiento.Text);
                Obe.cntc_itipo_pago = Convert.ToInt32(lkpTipoPago.EditValue);
                Obe.cntc_ncuota_inicial = Convert.ToDecimal(txtCuotaInicial.EditValue);
                Obe.cntc_inro_cuotas = Convert.ToInt32(txtCuotas.Text);
                Obe.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                Obe.cntc_sfecha_cuota = (DateTime?)dteFechaCuota.EditValue;
                Obe.cntc_sfecha_fin_pago = (DateTime?)dteFinPago.EditValue;
                Obe.cntc_iusuario_crea = Valores.intUsuario;
                Obe.cntc_vpc_crea = WindowsIdentity.GetCurrent().Name;
                Obe.cntc_sfecha_crea = DateTime.Now;
                Obe.cntc_idocumento_financiado = Convert.ToInt32(lkpDocumentoFinancia.EditValue);
                Obe.cntc_vcapacidad_total = txtCapacidadTotal.Text;
                Obe.cntc_vcapacidad_contrato = txtCapacidadContratada.Text;
                Obe.cntc_nsaldo = Convert.ToDecimal(txtSaldo.Text);
                Obe.cntc_nprecio_total = Convert.ToDecimal(txtPrecioTotal.Text);
                Obe.cntc_vurnas = txtUrnas.Text;
                Obe.cntc_vservico_inhumacion = txtServicioIhumacion.Text;
                Obe.cntc_icod_plataforma = Convert.ToInt32(lkpPlataforma.EditValue);
                Obe.cntc_icod_tipo_contrato = Convert.ToInt32(lkpTipoContrato.EditValue);
                bool GuardarCuotas = false;

                if (Obe.cntc_icod_contrato == 0 && Services.MessageQuestion("¿Desea Generar las Cuotas del Contrato?") == DialogResult.Yes)
                {
                    GenerarCuotas();
                    GuardarCuotas = true;
                }

                if (Obe.cntc_icod_contrato > 0 && Obe.cntc_vorigen_registro == "ASESOR")
                {
                    var cuotas = new BVentas().listarContratoCuotas(Obe.cntc_icod_contrato);
                    if (cuotas.Count == 0)
                    {
                        if (Services.MessageQuestion("¿Desea Generar las Cuotas del Contrato?") == DialogResult.Yes)
                        {
                            GenerarCuotas();
                            GuardarCuotas = true;
                        }
                    }
                    else
                    {
                        if (cuotas.Exists(x => x.cntc_icod_situacion != (int)TableVenta.EstadoCuota.Pendiente))
                        {
                            GuardarCuotas = false;
                        }
                        else
                        {
                            GenerarCuotas();
                            GuardarCuotas = true;
                        }
                    }
                }
                Obe.cntc_icod_contrato = new BVentas().ContratoWebGuardar(Obe, GuardarCuotas, lstCuotas);
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.cntc_icod_contrato);
                    this.Close();
                }
            }
        }

        private void GenerarCuotas()
        {
            if (Convert.ToInt32(lkpTipoPago.EditValue) == 674) // CREDITO
            {
                int NroCuotas = (Convert.ToInt32(txtCuotas.Text));

                for (int y = 0; y <= NroCuotas; y++)
                {
                    EContratoCuotas EDet = new EContratoCuotas();
                    if (y == 0)
                    {

                        EDet.cntc_inro_cuotas = y;
                        EDet.cntc_sfecha_cuota = Convert.ToDateTime(dtFecha.DateTime);
                        EDet.cntc_icod_tipo_cuota = 336;
                        EDet.strTipo = "C. INICIAL";
                        EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtCuotaInicial.Text);
                        EDet.cntc_icod_situacion = 338;
                        EDet.strSituacion = "PENDIENTE";
                        EDet.intUsuario = Valores.intUsuario;
                        EDet.strPc = WindowsIdentity.GetCurrent().Name;
                        EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                        EDet.cntc_npagado = 0;
                        EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                        EDet.strTipoCredito = "PRINCIPAL";
                        EDet.cntc_nmonto_mora = 0;
                        EDet.cntc_nmonto_mora_pago = 0;
                        EDet.intTipoOperacion = 1;
                        lstCuotas.Add(EDet);




                    }
                    else if (y == 1)
                    {

                        EDet.cntc_inro_cuotas = y;
                        EDet.cntc_sfecha_cuota = Convert.ToDateTime(dteFechaCuota.DateTime);
                        EDet.cntc_icod_tipo_cuota = 337;
                        EDet.strTipo = "CUOTA";
                        EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                        EDet.cntc_icod_situacion = 338;
                        EDet.strSituacion = "PENDIENTE";
                        EDet.intUsuario = Valores.intUsuario;
                        EDet.strPc = WindowsIdentity.GetCurrent().Name;
                        EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                        EDet.cntc_npagado = 0;
                        EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                        EDet.strTipoCredito = "PRINCIPAL";
                        EDet.cntc_nmonto_mora = 0;
                        EDet.cntc_nmonto_mora_pago = 0;
                        EDet.intTipoOperacion = 1;
                        lstCuotas.Add(EDet);


                    }
                    else
                    {

                        EDet.cntc_inro_cuotas = y;
                        EDet.cntc_sfecha_cuota = Convert.ToDateTime(dteFechaCuota.DateTime.AddMonths(y - 1));
                        EDet.cntc_icod_tipo_cuota = 337;
                        EDet.strTipo = "CUOTA";
                        EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                        EDet.cntc_icod_situacion = 338;
                        EDet.strSituacion = "PENDIENTE";
                        EDet.intUsuario = Valores.intUsuario;
                        EDet.strPc = WindowsIdentity.GetCurrent().Name;
                        EDet.cntc_nsaldo = EDet.cntc_nmonto_cuota;
                        EDet.cntc_npagado = 0;
                        EDet.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                        EDet.strTipoCredito = "PRINCIPAL";
                        EDet.cntc_nmonto_mora = 0;
                        EDet.cntc_nmonto_mora_pago = 0;
                        EDet.intTipoOperacion = 1;
                        lstCuotas.Add(EDet);

                    }

                }
            }
            else
            {
                ETablaVentaDet obj = new BGeneral().listarTablaVentaDet(15).Where(x => x.tabvd_iid_tabla_venta_det == 5430).FirstOrDefault();
                EContratoCuotas EDetF = new EContratoCuotas();
                EDetF.cntc_inro_cuotas = 1;
                EDetF.cntc_sfecha_cuota = Convert.ToDateTime(dtFecha.DateTime);
                EDetF.cntc_icod_tipo_cuota = obj.tabvd_iid_tabla_venta_det;
                EDetF.strTipo = obj.tabvd_vdescripcion;
                EDetF.cntc_nmonto_cuota = Convert.ToDecimal(txtPrecioTotal.Text);
                EDetF.cntc_icod_situacion = 338;
                EDetF.strSituacion = "PENDIENTE";
                EDetF.intUsuario = Valores.intUsuario;
                EDetF.strPc = WindowsIdentity.GetCurrent().Name;
                EDetF.cntc_nsaldo = EDetF.cntc_nmonto_cuota;
                EDetF.cntc_npagado = 0;
                EDetF.cntc_nmonto_mora_pago = 0;
                EDetF.cntc_itipo_cuota = 0;// INDICADOR PRINCIPAL
                EDetF.strTipoCredito = "PRINCIPAL";
                EDetF.cntc_nmonto_mora = 0;
                EDetF.intTipoOperacion = 1;
                lstCuotas.Add(EDetF);
            }
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteFamiliaCab_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpOrigenVenta, new BGeneral().listarTablaVentaDet(1), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpCodigoPlan, new BGeneral().listarTablaVentaDet(2), "tabvd_vdesc_abreviado", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNombreIEC, new BVentas().listarVendedor().Where(x => x.tablc_iid_situacion_vendedor == 6).ToList(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpEstadoCivilContrante1, new BGeneral().listarTablaRegistro(78), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpEstadoCivilContrante2, new BGeneral().listarTablaRegistro(78), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpNacionalidadContrante1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpNacionalidadContrante2, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoNecesidad, new BGeneral().listarTablaVentaDet(13), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(14), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNacionalidadFallecido, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocFallecido, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocContrante2, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocContratante1, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoPago, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipoPrestamo, new BGeneral().listarTablaRegistro(98), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpDeceso, new BGeneral().listarTablaVentaDet(20), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);

            BSControls.LoaderLook(lkpDocumentoFinancia, new BGeneral().listarTablaVentaDet(30), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            BSControls.LoaderLook(lkpPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            BSControls.LoaderLook(lkpTipoContrato, new BGeneral().listarTablaVentaDet(31), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);


            if (Status == BSMaintenanceStatus.CreateNew)
                getNumDoc();
            else
                setValues();
            if (Valores.vendc_icod_vendedor > 0)
            {
                lkpNombreIEC.EditValue = Valores.vendc_icod_vendedor;
                lkpNombreIEC.ReadOnly = true;
            }
        }

        public void getNumDoc()
        {
            dtFecha.EditValue = DateTime.Now;
            dteFechaCuota.EditValue = DateTime.Now;
            dteFinPago.EditValue = DateTime.Now;

            var parametros = new BVentas().listarRegistroParametro().FirstOrDefault();
            txtSerie.Text = parametros.rgpmc_vserie_contrato;
        }

        private void lkpOrigenVenta_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpOrigenVenta.Text == "FUNERARIA")
            {
                btnFunerarios.Enabled = true;
            }
            else
            {
                btnFunerarios.Enabled = false;
                btnFunerarios.Text = "";
            }
        }

        private void btnFunerarios_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarFunerarias();
        }
        private void listarFunerarias()
        {
            try
            {
                using (frmListarFunerarias frm = new frmListarFunerarias())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnFunerarios.Tag = frm._Be.func_icod_funeraria;
                        btnFunerarios.Text = frm._Be.func_vnombre_comercial;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtNumer_Leave(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (txtNumer.Text != "0000000")
                {
                    bool existeSerie = new BVentas().ObtenerExistenciaSerie(txtSerie.Text + txtNumer.Text);
                    if (existeSerie)
                    {
                        XtraMessageBox.Show(string.Format("Ya Existe un contrato con la serie {0}-{1}", txtSerie.Text, txtNumer.Text), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNumer.Focus();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Ingrese La serie del Contrato", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNumer.Focus();
                }


            }
        }


        private void lkpTipoPago_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoPago.Text == "CONTADO")
            {
                txtCuotaInicial.Properties.ReadOnly = true;
                txtCuotas.Properties.ReadOnly = true;
                txtMontoCuotas.Properties.ReadOnly = true;
                dteFechaCuota.Enabled = false;
                txtCuotaInicial.Text = "0";
                txtMontoCuotas.Text = "0";
                txtCuotas.Text = "0";
                txtSaldo.Text = txtPrecioLista.Text;
                txtPrecioTotal.Text = txtPrecioLista.Text;
                dteFechaCuota.DateTime = dtFecha.DateTime;
                dteFinPago.DateTime = dtFecha.DateTime;
            }
            else
            {
                txtCuotaInicial.Properties.ReadOnly = false;
                txtCuotas.Properties.ReadOnly = false;
                txtMontoCuotas.Properties.ReadOnly = false;
                dteFechaCuota.Enabled = true;
                dteFechaCuota.DateTime = dtFecha.DateTime.AddMonths(1);
                dteFinPago.DateTime = dtFecha.DateTime.AddMonths(1);
            }

            lkpTipoSepultura_EditValueChanged(null, null);

        }

        private void lkpCodigoPlan_EditValueChanged(object sender, EventArgs e) => CargarPrecioLista();

        private void lkpTipoNecesidad_EditValueChanged(object sender, EventArgs e) => CargarPrecioLista();

        private void lkpTipoSepultura_EditValueChanged(object sender, EventArgs e)
        {
            var select = new BVentas().PlanNecisidadSepulturaListar(Convert.ToInt32(lkpTipoSepultura.EditValue), Convert.ToInt32(lkpCodigoPlan.EditValue), Convert.ToInt32(lkpTipoNecesidad.EditValue)).FirstOrDefault();
            if (select != null)
            {
                lblMensaje.Text = "Los Parametros Ingresados Están Dentro de la Lista de Precios";
                txtPrecioLista.Text = select.nprecio_lista.ToString();
                txtDescuento.Text = select.nmonto_descuento.ToString();
                if (Convert.ToInt32(lkpTipoPago.EditValue) == (int)TipoPago.Credito)
                    txtCuotaInicial.Text = select.ncuota_inicial.ToString();
                var detalle = new BVentas().PlanNecisidadSepulturaDetalleListar(select.id).FirstOrDefault();
                if (detalle != null)
                {
                    
                    if (Convert.ToInt32(lkpTipoPago.EditValue) == (int)TipoPago.Credito)
                    {

                        txtMontoCuotas.Text = detalle.nmonto.ToString();
                        txtCuotas.Text = detalle.icantidad_cuotas.ToString();
                    }

                    txtPrecioLista_EditValueChanged(sender, e);
                }
                

            }
            else
            {
                lblMensaje.Text = "Los Parametros Ingresados no se Encuentran en la Lista de Precios";
            }
        }
        private void CargarPrecioLista()
        {

            if (Convert.ToInt32(lkpTipoSepultura.EditValue) > 0 || Convert.ToInt32(lkpCodigoPlan.EditValue) > 0 || Convert.ToInt32(lkpTipoNecesidad.EditValue) > 0)
            {
                var select = new BVentas().PlanNecisidadSepulturaListar(0, Convert.ToInt32(lkpCodigoPlan.EditValue), Convert.ToInt32(lkpTipoNecesidad.EditValue));
                BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3).OrderBy(x=>x.tabvd_vdescripcion), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            }


        }

        private void lkpCuotas_EditValueChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(lkpTipoPago.EditValue) == (int)TipoPago.Credito && Convert.ToInt32(lkpTipoSepultura.EditValue) > 0 && Convert.ToInt32(lkpCodigoPlan.EditValue) > 0 && Convert.ToInt32(lkpTipoNecesidad.EditValue) > 0)
            {
                var select = new BVentas().PlanNecisidadSepulturaListar(Convert.ToInt32(lkpTipoSepultura.EditValue), Convert.ToInt32(lkpCodigoPlan.EditValue), Convert.ToInt32(lkpTipoNecesidad.EditValue)).FirstOrDefault();
                if (select != null)
                {
                    var detalle = new BVentas().PlanNecisidadSepulturaDetalleListar(select.id).Where(x => x.icantidad_cuotas == Convert.ToInt32(txtCuotas.Text)).FirstOrDefault();
                    if (detalle != null)
                    {
                        txtMontoCuotas.Text = detalle.nmonto.ToString();
                        txtCuotaInicial.Text = select.ncuota_inicial.ToString();
                        txtPrecioLista.Text = select.nprecio_lista.ToString();

                    }

                }

            }
            if (dteFechaCuota.Text != "" && txtCuotas.Text != "00")
            {
                dteFinPago.DateTime = dteFechaCuota.DateTime.AddMonths((Convert.ToInt32(txtCuotas.Text) - 1));
            }
            txtPrecioLista_EditValueChanged(sender, e);
        }

        private void txtPrecioLista_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoPago.EditValue) == (int)TipoPago.Credito)
            {
                txtSaldo.Text = (Convert.ToDecimal(txtMontoCuotas.Text) * Convert.ToDecimal(txtCuotas.Text)).ToString();
                txtPrecioTotal.Text = (Convert.ToDecimal(txtCuotaInicial.Text) + Convert.ToDecimal(txtSaldo.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
            }
            else
            {
                txtSaldo.Text = (Convert.ToDecimal(txtPrecioLista.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
                txtPrecioTotal.Text = txtSaldo.Text;
            }
        }

        private void txtCuotaInicial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCuotaInicial.ContainsFocus)
                txtPrecioLista_EditValueChanged(sender, e);
        }

        private void dteFechaCuota_EditValueChanged(object sender, EventArgs e)
        {
            if (dteFechaCuota.ContainsFocus)
                if (dteFechaCuota.Text != "" && txtCuotas.Text != "00")
                {
                    dteFinPago.DateTime = dteFechaCuota.DateTime.AddMonths((Convert.ToInt32(txtCuotas.Text) - 1));
                }
        }

        private void txtMontoCuotas_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMontoCuotas.ContainsFocus)
                txtPrecioLista_EditValueChanged(sender, e);
        }

        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDescuento.ContainsFocus)
                txtPrecioLista_EditValueChanged(sender, e);
        }
    }
}