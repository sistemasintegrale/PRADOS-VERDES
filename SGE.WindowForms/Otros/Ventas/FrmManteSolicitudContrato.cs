using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;
using SGE.WindowForms.Otros.Ventas;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteSolicitudContrato : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContrato Obe = new EContrato();
        public List<EContrato> lstContrato = new List<EContrato>();
        public List<EContratoFallecido> lstDetalle = new List<EContratoFallecido>();
        public List<EContratoFallecido> lstDelete = new List<EContratoFallecido>();
        public int icodSepulturaTemp = 0;
        public int icodTitular1, icodTitular2;
        List<EContratoTitular1> lstcontarto1 = new List<EContratoTitular1>();
        List<EContratoTitular2> lstcontarto2 = new List<EContratoTitular2>();
        public EContratante obj = new EContratante();
        public FrmManteSolicitudContrato()
        {
            InitializeComponent();
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
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //txtSerie.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
            btnFunerarios.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)


                if (Status == BSMaintenanceStatus.CreateNew)
                    btnFunerarios.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.View)
            {
                txtSerieSol.Properties.ReadOnly = true;
                txtNumSol.Properties.ReadOnly = true;
                txtPagoCovid19.Properties.ReadOnly = true;
                dtFecha.Enabled = false;
                lkpNombreIEC.Enabled = false;
                lkpOrigenVenta.Enabled = false;
                btnFunerarios.Enabled = false;
                txtNombreContratante.Properties.ReadOnly = true;
                txtApellidoPContratante.Properties.ReadOnly = true;
                txtApellidoMContratante.Properties.ReadOnly = true;
                txtRucContratante.Properties.ReadOnly = true;
                dtFechaNacContratante.Enabled = false;
                txtTelContratante.Properties.ReadOnly = true;
                txtCorreo.Properties.ReadOnly = true;
                txtDireccionContratante.Properties.ReadOnly = true;
                lkpNacionalidadContratante.Enabled = false;
                lkpTipoDocContratante.Enabled = false;
                txtDOCContratante.Properties.ReadOnly = true;
                txtNombreRepresentante.Properties.ReadOnly = true;
                txtApellidoPRepresentante.Properties.ReadOnly = true;
                txtApellidoMRepresentante.Properties.ReadOnly = true;
                txtRUCRepresentante.Properties.ReadOnly = true;
                dtFechaNacRepresentante.Enabled = false;
                lkpEstadoCivil.Enabled = false;
                lkpNacionalidad.Enabled = false;
                txtTelfRepresentante.Properties.ReadOnly = true;
                txtDomicilio.Properties.ReadOnly = true;
                txtNRepresentante.Properties.ReadOnly = true;
                txtDptoRepresentante.Properties.ReadOnly = true;
                lkpDistrito.Enabled = false;
                txtCodigoPostalRepresentante.Properties.ReadOnly = true;
                lkpTipoDocRepresentante.Enabled = false;
                txtDOCRepresentante.Properties.ReadOnly = true;
                lkpCodigoPlan.Enabled = false;
                lkpNombrePlan.Enabled = false;
                txtPrecioLista.Properties.ReadOnly = true;
                txtDescuento.Properties.ReadOnly = true;
                txtInhumacion.Properties.ReadOnly = true;
                txtAporteFondo.Properties.ReadOnly = true;
                txtIGV.Properties.ReadOnly = true;
                lkpEstadoSolicitud.Enabled = false;
                txtCuotaInicial.Properties.ReadOnly = true;
                txtNroCuotas.Properties.ReadOnly = true;
                txtMontoCuotas.Properties.ReadOnly = true;
                dtFechaCuota.Enabled = false;                
                lkpTipoPago.Enabled = false;
                lkpTipoPrestamo.Enabled = false;
                btnPrestamo.Enabled = false;
                txtFoma.Properties.ReadOnly = true;
                txtDOCContratante.Properties.ReadOnly = true;        
                lkpDeceso.Enabled = false;
                lkpFomaMante.Enabled = false;
                txtFinanciamiento.Properties.ReadOnly = true;
                lkpNombreIEC.Enabled = false;
                txtPrecioTotal.Properties.ReadOnly = true;
            }

        }
        public void setValues()
        {
            txtObservacionesSolicitud.Text = Obe.cntc_vobservaciones_sol;
            txtSerieSol.Text = Obe.cntc_vnumero_solicitud.Substring(0, 4);
            txtNumSol.Text = Obe.cntc_vnumero_solicitud.Substring(4);
            txtPagoCovid19.Text = Obe.cntc_npago_covid19.ToString();
            dtFecha.EditValue = Obe.cntc_sfecha_contrato;
            lkpNombreIEC.EditValue = Obe.cntc_icod_vendedor;
            lkpOrigenVenta.EditValue = Obe.cntc_iorigen_venta;
            btnFunerarios.Tag = Obe.cntc_icod_funeraria;
            btnFunerarios.Text = Obe.cntc_vnombre_comercial;
            txtNombreContratante.Text = Obe.cntc_vnombre_contratante;
            txtApellidoPContratante.Text = Obe.cntc_vapellido_paterno_contratante;
            txtApellidoMContratante.Text = Obe.cntc_vapellido_materno_contratante;
            txtRucContratante.Text = Obe.cntc_vruc_contratante;
            if (Obe.cntc_sfecha_nacimineto_contratante == null)
                Obe.cntc_sfecha_nacimineto_contratante = Convert.ToDateTime("01/01/0001");
            dtFechaNacContratante.EditValue = Obe.cntc_sfecha_nacimineto_contratante.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_nacimineto_contratante;
            txtTelContratante.Text = Obe.cntc_vtelefono_contratante;
            txtCorreo.Text = Obe.cntc_vdireccion_correo_contratante;
            txtDireccionContratante.Text = Obe.cntc_vdireccion_contratante;
            lkpNacionalidadContratante.EditValue = Obe.cntc_inacionalidad_contratante;
            lkpTipoDocContratante.EditValue = Obe.cntc_itipo_documento_contratante;
            txtDOCContratante.Text = Obe.cntc_vdocumento_contratante;
            txtNombreRepresentante.Text = Obe.cntc_vnombre_representante;
            txtApellidoPRepresentante.Text = Obe.cntc_vapellido_paterno_representante;
            txtApellidoMRepresentante.Text = Obe.cntc_vapellido_materno_representante;
            txtRUCRepresentante.Text = Obe.cntc_ruc_representante;
            if (Obe.cntc_sfecha_nacimiento_representante == null)
                Obe.cntc_sfecha_nacimiento_representante = Convert.ToDateTime("01/01/0001");
            dtFechaNacRepresentante.EditValue = Obe.cntc_sfecha_nacimiento_representante.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_nacimiento_representante;
            lkpEstadoCivil.EditValue = Obe.cntc_iestado_civil_representante;
            lkpNacionalidad.EditValue = Obe.cntc_inacionalidad_respresentante;
            txtTelfRepresentante.Text = Obe.cntc_vtelefono_representante;
            txtDomicilio.Text = Obe.cntc_vdireccion_representante;
            txtNRepresentante.Text = Obe.cntc_vnumero_direccion_representante;
            txtDptoRepresentante.Text = Obe.cntc_vdepartamento_representante;
            lkpDistrito.EditValue = Obe.cntc_idistrito_representante;
            txtCodigoPostalRepresentante.Text = Obe.cntc_vcodigo_postal_representante;
            lkpTipoDocRepresentante.EditValue = Obe.cntc_itipo_documento_representante;
            txtDOCRepresentante.Text = Obe.cntc_vdocumento_respresentante;
            lkpCodigoPlan.EditValue = Obe.cntc_icodigo_plan;
            lkpNombrePlan.EditValue = Obe.cntc_icod_nombre_plan;
            txtPrecioLista.Text = Obe.cntc_nprecio_lista.ToString();
            txtDescuento.Text = Obe.cntc_ndescuento.ToString();
            txtInhumacion.Text = Obe.cntc_ninhumacion.ToString();
            txtAporteFondo.Text = Obe.cntc_naporte_fondo.ToString();
            txtIGV.Text = Obe.cntc_nIGV.ToString();
            icodSepulturaTemp = Convert.ToInt32(Obe.espac_iid_iespacios);
            icodEspacioDetT[0] = Convert.ToInt32(Obe.espad_iid_iespacios1);
            icodEspacioDetT[1] = Convert.ToInt32(Obe.espad_iid_iespacios2);
            icodEspacioDetT[2] = Convert.ToInt32(Obe.espad_iid_iespacios3);
            icodEspacioDetT[3] = Convert.ToInt32(Obe.espad_iid_iespacios4);
            icodEspacioDetT[4] = Convert.ToInt32(Obe.espad_iid_iespacios5);
            icodEspacioDetT[5] = Convert.ToInt32(Obe.espad_iid_iespacios6);
            lkpEstadoSolicitud.EditValue = Obe.cntc_iestado_sol;
            txtCuotaInicial.Text = Obe.cntc_ncuota_inicial.ToString();
            txtNroCuotas.Text = Obe.cntc_inro_cuotas.ToString();
            txtMontoCuotas.Text = Obe.cntc_nmonto_cuota.ToString();
            dtFechaCuota.EditValue = Obe.cntc_sfecha_cuota;
            lkpTipoPago.EditValue = Obe.cntc_itipo_pago;
            lkpTipoPrestamo.EditValue = Obe.cntc_itipo_doc_prestamo;
            btnPrestamo.Tag = Obe.func_icod_funeraria_prestamo;
            btnPrestamo.Text = Obe.strFunerariaPrestamo;
            txtFoma.Text = Obe.cntc_nmonto_foma.ToString();
            txtNombreContratante.Tag = Obe.cntcc_icod_contratante;
            List<EEspaciosDet> lstNivelesDetMod = new List<EEspaciosDet>();
            lstNivelesDetMod = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios);
            int count = 0;
            txtDOCContratante.Text = Obe.cntc_vdni_contratante;
            lstcontarto1 = new BVentas().listarContratoTitular1(Obe.cntc_icod_contrato);
            lstcontarto2 = new BVentas().listarContratoTitular2(Obe.cntc_icod_contrato);
            lkpDeceso.EditValue = Obe.cntc_icod_deceso;
            lkpFomaMante.EditValue = Obe.cntc_icod_foma_mante;
            txtFinanciamiento.Text = Obe.cntc_nfinanciamientro.ToString();
            lstDetalle = new BVentas().listarContratoFallecido(Obe.cntc_icod_contrato);
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            dtFecha.EditValue = DateTime.Now;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                Obe.cntc_npago_covid19 = Convert.ToDecimal(txtPagoCovid19.Text);
                Obe.cntc_vnumero_solicitud = String.Format("{0}{1}", txtSerieSol.Text, txtNumSol.Text);
                Obe.cntc_sfecha_contrato = Convert.ToDateTime(dtFecha.EditValue);
                Obe.cntc_icod_vendedor = Convert.ToInt32(lkpNombreIEC.EditValue);
                Obe.strNombreIEC = lkpNombreIEC.Text;
                Obe.cntc_iorigen_venta = Convert.ToInt32(lkpOrigenVenta.EditValue);
                Obe.strorigenventa = lkpOrigenVenta.Text;
                Obe.cntc_icod_funeraria = Convert.ToInt32(btnFunerarios.Tag);
                Obe.cntc_vnombre_comercial = btnFunerarios.Text;
                Obe.cntc_vnombre_contratante = txtNombreContratante.Text;
                Obe.cntc_vapellido_paterno_contratante = txtApellidoPContratante.Text;
                Obe.cntc_vapellido_materno_contratante = txtApellidoMContratante.Text;
                Obe.cntc_vruc_contratante = txtRucContratante.Text;
                Obe.cntc_sfecha_nacimineto_contratante = (DateTime?)dtFechaNacContratante.EditValue;
                Obe.cntc_vtelefono_contratante = txtTelContratante.Text;
                Obe.cntc_vdireccion_correo_contratante = txtCorreo.Text;
                Obe.cntc_vdireccion_contratante = txtDireccionContratante.Text;
                Obe.cntc_inacionalidad_contratante = Convert.ToInt32(lkpNacionalidadContratante.EditValue);
                Obe.cntc_vnacionalidad_cotratante = lkpNacionalidadContratante.Text;
                Obe.cntc_itipo_documento_contratante = Convert.ToInt32(lkpTipoDocContratante.EditValue);
                Obe.cntc_vdocumento_contratante = txtDOCContratante.Text;
                Obe.cntc_vnombre_representante = txtNombreRepresentante.Text;
                Obe.cntc_vapellido_paterno_representante = txtApellidoPRepresentante.Text;
                Obe.cntc_vapellido_materno_representante = txtApellidoMRepresentante.Text;
                Obe.cntc_ruc_representante = txtRUCRepresentante.Text;
                Obe.cntc_sfecha_nacimiento_representante = (DateTime?)dtFechaNacRepresentante.EditValue;
                Obe.cntc_iestado_civil_representante = Convert.ToInt32(lkpEstadoCivil.EditValue);
                Obe.cntc_inacionalidad_respresentante = Convert.ToInt32(lkpNacionalidad.EditValue);
                Obe.cntc_vtelefono_representante = txtTelfRepresentante.Text;
                Obe.cntc_vdireccion_representante = txtDomicilio.Text;
                Obe.cntc_vnumero_direccion_representante = txtNRepresentante.Text;
                Obe.cntc_vdepartamento_representante = txtDptoRepresentante.Text;
                Obe.cntc_idistrito_representante = Convert.ToInt32(lkpDistrito.EditValue);
                Obe.cntc_vcodigo_postal_representante = txtCodigoPostalRepresentante.Text;
                Obe.cntc_itipo_documento_representante = Convert.ToInt32(lkpTipoDocRepresentante.EditValue);
                Obe.cntc_vdocumento_respresentante = txtDOCRepresentante.Text;
                Obe.cntc_icodigo_plan = Convert.ToInt32(lkpCodigoPlan.EditValue);
                Obe.strcodigoplan = lkpCodigoPlan.Text;
                Obe.cntc_icod_nombre_plan = Convert.ToInt32(lkpNombrePlan.EditValue);
                Obe.cntc_vnombre_plan = lkpNombrePlan.Text;
                Obe.cntc_nprecio_lista = Convert.ToDecimal(txtPrecioLista.Text);
                Obe.cntc_ndescuento = Convert.ToDecimal(txtDescuento.Text);
                Obe.cntc_ninhumacion = Convert.ToDecimal(txtInhumacion.Text);
                Obe.cntc_naporte_fondo = Convert.ToDecimal(txtAporteFondo.Text);
                Obe.cntc_nIGV = Convert.ToDecimal(txtIGV.Text);
                Obe.cntc_vobservaciones_sol = txtObservacionesSolicitud.Text;
                Obe.espad_iid_iespacios1 = 0;
                Obe.espad_iid_iespacios2 = 0;
                Obe.espad_iid_iespacios3 = 0;
                Obe.espad_iid_iespacios4 = 0;
                Obe.espad_iid_iespacios5 = 0;
                Obe.espad_iid_iespacios6 = icodEspacioDet[5];
                Obe.espad_iid_iespacios6 = 0;
                Obe.espad_iid_iespaciosT1 = icodEspacioDet[0];
                Obe.espad_iid_iespaciosT2 = icodEspacioDet[1];
                Obe.espad_iid_iespaciosT3 = icodEspacioDet[2];
                Obe.espad_iid_iespaciosT4 = icodEspacioDet[3];
                Obe.espad_iid_iespaciosT5 = icodEspacioDet[4];
                Obe.espad_iid_iespaciosT6 = icodEspacioDet[5];
                Obe.cntc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.cntc_iestado_sol = Convert.ToInt32(lkpEstadoSolicitud.EditValue);
                Obe.cntc_ncuota_inicial = Convert.ToDecimal(txtCuotaInicial.Text);
                Obe.cntc_inro_cuotas = Convert.ToInt32(txtNroCuotas.Text);
                Obe.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                Obe.cntc_sfecha_cuota = (DateTime?)dtFechaCuota.EditValue;
                Obe.cntc_flag_verificacion = true;
                Obe.cntc_indicador_pre_contrato = 2;
                Obe.cntc_itipo_pago = Convert.ToInt32(lkpTipoPago.EditValue);
                if (Convert.ToInt32(btnPrestamo.Tag) > 0 && Convert.ToInt32(lkpTipoPrestamo.EditValue) > 0)
                {
                    Obe.cntc_itipo_doc_prestamo = Convert.ToInt32(lkpTipoPrestamo.EditValue);
                    Obe.func_icod_funeraria_prestamo = Convert.ToInt32(btnPrestamo.Tag);
                }
                else
                {
                    Obe.cntc_itipo_doc_prestamo = 0;
                    Obe.func_icod_funeraria_prestamo = 0;
                }
                Obe.cntc_nmonto_foma = Convert.ToDecimal(Obe.cntc_naporte_fondo);
                Obe.cntc_icod_foma_mante = Convert.ToInt32(lkpFomaMante.EditValue);
                Obe.cntc_icod_deceso = Convert.ToInt32(lkpDeceso.EditValue);
                Obe.cntc_nfinanciamientro = Convert.ToDecimal(txtFinanciamiento.Text);
                Obe.cntc_iindicador_contr_sol = Parametros.intSolicitud;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.cntc_icod_contrato = new BVentas().SolicitudContratoInsertar(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().SolicitudContratoModificar(Obe);
                }
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
            BSControls.LoaderLook(lkpNombreIEC, new BVentas().listarVendedor().Where(x => x.tablc_iid_situacion_vendedor == 6).ToList(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpEstadoCivil, new BGeneral().listarTablaRegistro(78), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpNacionalidad, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpDistrito, new BVentas().listarDistrito(), "disc_vdescripcion", "disc_icod_distrito", false);
            BSControls.LoaderLook(lkpNombrePlan, new BGeneral().listarTablaVentaDet(13), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);

            BSControls.LoaderLook(lkpEstadoSolicitud, new BGeneral().listarTablaVentaDet(28), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);

            BSControls.LoaderLook(lkpNacionalidadContratante, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocContratante, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocRepresentante, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoPago, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoPrestamo, new BGeneral().listarTablaRegistro(98), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpDeceso, new BGeneral().listarTablaVentaDet(20), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            BSControls.LoaderLook(lkpFomaMante, new BGeneral().listarTablaVentaDet(21), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            if (Status == BSMaintenanceStatus.CreateNew)

                getNumDoc();


            if (Valores.intUsuario != 4)
            {
                lkpNombreIEC.EditValue = Valores.vendc_icod_vendedor;
                lkpNombreIEC.Enabled = false;
            }
            else
                lkpNombreIEC.Enabled = true;

        }

        public void getNumDoc()
        {
            txtSerieSol.Text = DateTime.Now.Year.ToString();
            var parametro = new BVentas().listarRegistroParametro()[0];
            string numero = $"0000000{parametro.rgpmc_icorrelativo_solicitud + 1}";
            int correlativo = Convert.ToInt32(numero.Substring((parametro.rgpmc_icorrelativo_solicitud + 1).ToString().Length));
            txtNumSol.Text = correlativo == 0 ? "1" : correlativo.ToString();


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

        private void lkpCodigoPlan_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDNIContratante_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtRucContratante_EditValueChanged(object sender, EventArgs e)
        {

        }


        public int[] icodEspacioDet = new int[7];
        public int[] icodEspacioDetT = new int[7];

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonEdit1_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarFunerariasPrestamo();
        }

        private void txtCuotaInicial_EditValueChanged(object sender, EventArgs e)
        {
            Calculartotal();
        }

        private void listarFunerariasPrestamo()
        {
            try
            {
                using (frmListarFunerarias frm = new frmListarFunerarias())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnPrestamo.Tag = frm._Be.func_icod_funeraria;
                        btnPrestamo.Text = frm._Be.func_vnombre_comercial;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNroCuotas_EditValueChanged(object sender, EventArgs e)
        {
            Calculartotal();
        }

        private void txtMontoCuotas_EditValueChanged(object sender, EventArgs e)
        {
            Calculartotal();
        }

        private void dtFechaCuota_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpTipoPago_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoPago.Text == "CONTADO")
            {
                txtCuotaInicial.Properties.ReadOnly = true;
                txtNroCuotas.Properties.ReadOnly = true;
                txtMontoCuotas.Properties.ReadOnly = true;
                dtFechaCuota.Enabled = false;
                txtCuotaInicial.Text = "0";
                txtMontoCuotas.Text = "0";
                txtNroCuotas.Text = "0";
            }
            else
            {
                txtCuotaInicial.Properties.ReadOnly = false;
                txtNroCuotas.Properties.ReadOnly = false;
                txtMontoCuotas.Properties.ReadOnly = false;
                dtFechaCuota.Enabled = true;
            }

            Calculartotal();
        }

        private void txtPrecioLista_EditValueChanged(object sender, EventArgs e)
        {
            Calculartotal();
        }

        private void lkpEstadoSolicitud_EditValueChanged(object sender, EventArgs e)
        {

        }

        void Calculartotal()
        {
            if (lkpTipoPago.Text != "CONTADO")
            {
                decimal cuotaInicial = txtCuotaInicial.Text == "" ? 0 : Convert.ToDecimal(txtCuotaInicial.Text);
                decimal nroCuotas = txtNroCuotas.Text == "" ? 0 : Convert.ToDecimal(txtNroCuotas.Text);
                decimal montoCuotas = txtMontoCuotas.Text == "" ? 0 : Convert.ToDecimal(txtMontoCuotas.Text);
                decimal total = Math.Round((cuotaInicial + (nroCuotas * montoCuotas)), 2);


                txtPrecioTotal.Text = total.ToString();
            }
            else
            {
                txtPrecioTotal.Text = txtPrecioLista.Text;
            }
        }


    }


}