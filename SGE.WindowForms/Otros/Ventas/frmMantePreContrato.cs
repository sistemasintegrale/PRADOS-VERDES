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



namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmMantePreContrato : DevExpress.XtraEditors.XtraForm
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
        public int indicador = 0;
        public int area = 0;
        public int cobranza = 2;
        public int caja = 1;
        public bool cargando = true;
        public EContratante obj = new EContratante();
        public frmMantePreContrato()
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
            txtFinanciamiento.Enabled = true;


        }
        public void setValues()

        {
            txtSerie.Text = Obe.cntc_vnumero_contrato.Substring(0, 4);
            txtNumer.Text = Obe.cntc_vnumero_contrato.Substring(4);
            txtPagoCovid19.Text = Obe.cntc_npago_covid19.ToString();
            lblCorrelativo.Text = Obe.cntc_vnumero_contrato_corr;
            if (Obe.cntc_sfecha_contrato == null)
                Obe.cntc_sfecha_contrato = Convert.ToDateTime("01/01/0001");
            dtFecha.EditValue = Obe.cntc_sfecha_contrato.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_contrato;
            lkpNombreIEC.EditValue = Obe.cntc_icod_vendedor;
            lkpOrigenVenta.EditValue = Obe.cntc_iorigen_venta;
            btnFunerarios.Tag = Obe.cntc_icod_funeraria;
            btnFunerarios.Text = Obe.cntc_vnombre_comercial;
            txtNombreContratante.Text = Obe.cntc_vnombre_contratante;
            txtApellidoPContratante.Text = Obe.cntc_vapellido_paterno_contratante;
            txtApellidoMContratante.Text = Obe.cntc_vapellido_materno_contratante;
            if (Obe.cntc_itipo_pago != 0)
                lkpTipoPagoC19.EditValue = Obe.cntc_itipo_pago;
            else
            {
                if (Obe.cntc_inro_cuotas != 0)
                {
                    lkpTipoPagoC19.EditValue = 674;
                }
            }
            //txtDOCContratante.Text = Obe.cntc_vdni_contratante;
            txtRucContratante.Text = Obe.cntc_vruc_contratante;

            txtTelContratante.Text = Obe.cntc_vtelefono_contratante;
            txtCorreo.Text = Obe.cntc_vdireccion_correo_contratante;
            txtDireccionContratante.Text = Obe.cntc_vdireccion_contratante;
            txtDireccionContratante.Text = Obe.cntc_vdireccion_contratante;
            lkpNacionalidadContratante.EditValue = Obe.cntc_inacionalidad_contratante;
            lkpTipoDocContratante.EditValue = Obe.cntc_itipo_documento_contratante;
            txtDOCContratante.Text = Obe.cntc_vdocumento_contratante;
            txtNombreRepresentante.Text = Obe.cntc_vnombre_representante;
            txtApellidoPRepresentante.Text = Obe.cntc_vapellido_paterno_representante;
            txtApellidoMRepresentante.Text = Obe.cntc_vapellido_materno_representante;
            txtDNIRepresentante.Text = Obe.cntc_vdni_representante;
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
            txtNombreFallecido.Text = Obe.cntc_vnombre_fallecido;
            txtApellidoPFallecido.Text = Obe.cntc_vapellido_paterno_fallecido;
            txtApellidoMFallecido.Text = Obe.cntc_vapellido_materno_fallecido;
            txtDNIFallecimiento.Text = Obe.cntc_vdni_fallecido;
            if (Obe.cntc_sfecha_nac_fallecido == null)
                Obe.cntc_sfecha_nac_fallecido = Convert.ToDateTime("01/01/0001");
            dtFechaNacFallecido.EditValue = Obe.cntc_sfecha_nac_fallecido.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_nac_fallecido;
            lkpNacionalidad1.EditValue = Obe.cntc_inacionalidad;
            if (Obe.cntc_sfecha_fallecimiento == null)
                Obe.cntc_sfecha_fallecimiento = Convert.ToDateTime("01/01/0001");
            dtFechaFallecimiento.EditValue = Obe.cntc_sfecha_fallecimiento.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_fallecimiento;
            if (Obe.cntc_sfecha_entierro == null)
                Obe.cntc_sfecha_entierro = Convert.ToDateTime("01/01/0001");
            dtFechaEntierro.EditValue = Obe.cntc_sfecha_entierro.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_entierro;
            lkpCodigoPlan.EditValue = Obe.cntc_icodigo_plan;
            lkpNombrePlan.EditValue = Obe.cntc_icod_nombre_plan;
            //lkpNombrePlan.Text = Obe.strNombreplan;
            txtPrecioLista.Text = Obe.cntc_nprecio_lista.ToString();
            txtDescuento.Text = Obe.cntc_ndescuento.ToString();
            txtInhumacion.Text = Obe.cntc_ninhumacion.ToString();
            txtAporteFondo.Text = Obe.cntc_naporte_fondo.ToString();
            txtIGV.Text = Obe.cntc_nIGV.ToString();
            txtPrecioTotal.Text = Obe.cntc_nprecio_total.ToString();
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
            txtCapacContrat.Text = Obe.cntc_vcapacidad_contrato;
            txtCapacTotal.Text = Obe.cntc_vcapacidad_total;
            txtUrnas.Text = Obe.cntc_vurnas;
            txtServInhumacion.Text = Obe.cntc_vservico_inhumacion;
            lkpPlataforma.EditValue = Obe.cntc_icod_plataforma;
            lkpManzana.EditValue = Obe.cntc_icod_manzana;
            bteSepultura.Tag = Obe.cntc_icod_isepultura;
            bteSepultura.Text = Obe.strsepultura;
            btnEspacios.Tag = Convert.ToInt32(Obe.espac_iid_iespacios);
            btnEspacios.Text = Obe.espac_icod_vespacios;
            icodSepulturaTemp = Convert.ToInt32(Obe.espac_iid_iespacios);
            ckbNivel1.Checked = Convert.ToBoolean(Obe.cntc_bnivel1);
            ckbNivel2.Checked = Convert.ToBoolean(Obe.cntc_bnivel2);
            ckbNivel3.Checked = Convert.ToBoolean(Obe.cntc_bnivel3);
            ckbNivel4.Checked = Convert.ToBoolean(Obe.cntc_bnivel4);
            ckbNivel5.Checked = Convert.ToBoolean(Obe.cntc_bnivel5);
            ckbNivel6.Checked = Convert.ToBoolean(Obe.cntc_bnivel6);
            txtCodigoSepultura.Text = Obe.cntc_vcodigo_sepultura;
            txtNrReserva.Text = Obe.cntc_vnumero_reserva;
            icodEspacioDetT[0] = Convert.ToInt32(Obe.espad_iid_iespacios1);
            icodEspacioDetT[1] = Convert.ToInt32(Obe.espad_iid_iespacios2);
            icodEspacioDetT[2] = Convert.ToInt32(Obe.espad_iid_iespacios3);
            icodEspacioDetT[3] = Convert.ToInt32(Obe.espad_iid_iespacios4);
            icodEspacioDetT[4] = Convert.ToInt32(Obe.espad_iid_iespacios5);
            icodEspacioDetT[5] = Convert.ToInt32(Obe.espad_iid_iespacios6);

            lkpSituacion.EditValue = Obe.cntc_icod_situacion;
            if (Convert.ToDecimal(Obe.cntc_ncuota_inicial) != 0)
            {
                txtCuotaInicial.Text = (Convert.ToDecimal(Obe.cntc_ncuota_inicial)).ToString();
            }

            txtNroCuotas.Text = Obe.cntc_inro_cuotas.ToString();

            if (Convert.ToDecimal(Obe.cntc_nmonto_cuota) != 0)
            {
                txtMontoCuotas.Text = Obe.cntc_nmonto_cuota.ToString();
            }
            
            dtFechaCuota.EditValue = Obe.cntc_sfecha_cuota;


            dtFechaNacContratante.EditValue = Obe.cntc_sfecha_nacimineto_contratante;




            txtFoma.Text = Obe.cntc_naporte_fondo == 0 ? Obe.cntc_nmonto_foma.ToString() :  Obe.cntc_naporte_fondo.ToString();
            

            lkpNiveles.EditValue = Obe.cntc_icod_indicador_espacios;
            txtObservaciones.Text = Obe.cntc_vobservaciones;

            CheckBox[] textoCantidad = GetTextoCantidad();
            List<EEspaciosDet> lstNivelesDetMod = new List<EEspaciosDet>();
            //lstNiveles = new BVentas().listarEspacios().Where(x => x.espac_isepultura == Convert.ToInt32(bteSepultura.Tag)).ToList();
            lstNivelesDetMod = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios);
            int count = 0;

            lstNivelesDetMod.ForEach(x =>
            {
                if (x.espad_icod_isituacion == 13 || x.cntc_icod_contrato == Obe.cntc_icod_contrato)
                {
                    textoCantidad[count].Enabled = true;
                    icodEspacioDet[count] = x.espad_iid_iespacios;

                }
                else
                {
                    textoCantidad[count].Enabled = false;
                }

                count++;
            });
            if (Obe.cntc_icod_deceso != 0)
                lkpDeceso.EditValue = Obe.cntc_icod_deceso;

            lkpFomaMante.EditValue = Obe.cntc_icod_foma_mante;
            txtFinanciamiento.Text = Obe.cntc_nfinanciamientro.ToString();
            txtNombreContratante.Enabled = string.IsNullOrWhiteSpace(Obe.cntc_vnombre_contratante);
            txtApellidoMContratante.Enabled = string.IsNullOrWhiteSpace(Obe.cntc_vapellido_materno_contratante);
            txtApellidoPContratante.Enabled = string.IsNullOrWhiteSpace(Obe.cntc_vapellido_paterno_contratante);
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
                if (area == caja)
                {
                    if (string.IsNullOrEmpty(dtFecha.Text))
                    {
                        oBase = dtFecha;
                        throw new ArgumentException("Ingrese la Fecha del Precontrato");
                    }

                    if (String.IsNullOrEmpty(txtSerie.Text))
                    {
                        oBase = txtSerie;
                        throw new ArgumentException("Ingrese Serie");
                    }
                    if (string.IsNullOrEmpty(txtNombreContratante.Text))
                    {
                        oBase = txtNombreContratante;
                        throw new ArgumentException("Ingrese Nombre del Contratante");
                    }
                    if (string.IsNullOrEmpty(txtApellidoPContratante.Text))
                    {
                        oBase = txtApellidoPContratante;
                        throw new ArgumentException("Ingrese el Apellido Pat. del Contratante");
                    }
                    if (txtSerie.Text != "D003" && txtSerie.Text != "0002")
                    {
                        oBase = txtSerie;
                        throw new ArgumentException("La serie del Contrato debe ser D003 o 0002");
                    }

                }
                if (area == cobranza)
                {
                    if (lkpTipoPagoC19.Text == "CREDITO")
                    {
                        if (Convert.ToDecimal(txtCuotaInicial.Text) == 0)
                        {
                            oBase = txtCuotaInicial;
                            throw new ArgumentException("Ingrese la Cuota Inicial");
                        }

                        if (Convert.ToInt32(txtNroCuotas.Text) == 0)
                        {
                            oBase = txtNroCuotas;
                            throw new ArgumentException("Ingrese El Número de Cuotas");
                        }

                        if (Convert.ToDecimal(txtMontoCuotas.Text) == 0)
                        {
                            oBase = txtMontoCuotas;
                            throw new ArgumentException("Ingrese El Monto de Cuotas");
                        }
                        if (string.IsNullOrEmpty(dtFechaCuota.Text) || dtFechaCuota.Text == "01/01/0001")
                        {
                            oBase = dtFechaCuota;
                            throw new ArgumentException("Ingrese La fecha de la Cuota");
                        }
                    }

                    if (Convert.ToDecimal(txtPrecioTotal.Text) == 0)
                    {
                        oBase = txtPrecioTotal;
                        throw new ArgumentException("El precio Total no Puede ser Igual a 0");
                    }
                }


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    List<EContrato> lstContratoExite = lstContrato.Where(x => x.cntc_vnumero_contrato == String.Format("{0}{1}", txtSerie.Text, txtNumer.Text)).ToList();
                    if (lstContratoExite.Count > 0)
                    {
                        oBase = txtNumer;
                        throw new ArgumentException("Número de Contrato ya Existe");
                    }
                }

                if (string.IsNullOrWhiteSpace(txtNombreContratante.Text))
                {
                    oBase = txtNombreContratante;
                    throw new ArgumentException("Ingrese nombre del Contratante");
                }
                Obe.cntc_npago_covid19 = Convert.ToDecimal(txtPagoCovid19.Text);
                Obe.cntc_itipo_pago = Convert.ToInt32(lkpTipoPagoC19.EditValue);
                Obe.cntc_vnumero_contrato = String.Format("{0}{1}", txtSerie.Text, txtNumer.Text);
                Obe.cntc_sfecha_contrato = Convert.ToDateTime(dtFecha.EditValue);
                Obe.cntc_icod_vendedor = Convert.ToInt32(lkpNombreIEC.EditValue);
                Obe.strNombreIEC = lkpNombreIEC.Text;
                Obe.cntc_iorigen_venta = Convert.ToInt32(lkpOrigenVenta.EditValue);
                Obe.strorigenventa = lkpOrigenVenta.Text;
                Obe.cntc_icod_funeraria = Convert.ToInt32(btnFunerarios.Tag);
                Obe.cntc_vnombre_comercial = btnFunerarios.Text;
                Obe.cntc_vnombre_contratante = txtNombreContratante.Text + " ";
                Obe.cntc_vapellido_paterno_contratante = txtApellidoPContratante.Text + " ";
                Obe.cntc_vapellido_materno_contratante = txtApellidoMContratante.Text + " ";
                Obe.cntc_vruc_contratante = txtRucContratante.Text;
                if (dtFechaNacContratante.DateTime == null || dtFechaNacContratante.Text == "" || dtFechaNacContratante.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_nacimineto_contratante = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(dtFechaNacContratante.EditValue);
                }
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
                Obe.cntc_vdni_representante = txtDNIRepresentante.Text;
                Obe.cntc_ruc_representante = txtRUCRepresentante.Text;
                if (dtFechaNacRepresentante.DateTime == null || dtFechaNacRepresentante.Text == "" || dtFechaNacRepresentante.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_nacimiento_representante = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_nacimiento_representante = Convert.ToDateTime(dtFechaNacRepresentante.EditValue);
                }
                Obe.cntc_iestado_civil_representante = Convert.ToInt32(lkpEstadoCivil.EditValue);
                Obe.cntc_inacionalidad_respresentante = Convert.ToInt32(lkpNacionalidad.EditValue);
                Obe.cntc_vtelefono_representante = txtTelfRepresentante.Text;
                Obe.cntc_vdireccion_representante = txtDomicilio.Text;
                Obe.cntc_vnumero_direccion_representante = txtNRepresentante.Text;
                Obe.cntc_vdepartamento_representante = txtDptoRepresentante.Text;
                Obe.cntc_idistrito_representante = Convert.ToInt32(lkpDistrito.EditValue);
                Obe.cntc_vcodigo_postal_representante = txtCodigoPostalRepresentante.Text;
                Obe.cntc_vnombre_fallecido = txtNombreFallecido.Text;
                Obe.cntc_vapellido_paterno_fallecido = txtApellidoPFallecido.Text;
                Obe.cntc_vapellido_materno_fallecido = txtApellidoMFallecido.Text;
                Obe.cntc_vdni_fallecido = txtDNIFallecimiento.Text;
                if (String.IsNullOrEmpty(dtFechaNacFallecido.Text) || dtFechaNacFallecido.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_nac_fallecido = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_nac_fallecido = Convert.ToDateTime(dtFechaNacFallecido.EditValue);
                }
                Obe.cntc_inacionalidad = Convert.ToInt32(lkpNacionalidad1.EditValue);
                if (String.IsNullOrEmpty(dtFechaFallecimiento.Text) || dtFechaFallecimiento.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_fallecimiento = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_fallecimiento = Convert.ToDateTime(dtFechaFallecimiento.EditValue);
                }
                if (String.IsNullOrEmpty(dtFechaEntierro.Text) || dtFechaEntierro.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_entierro = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_entierro = Convert.ToDateTime(dtFechaEntierro.EditValue);
                }
                Obe.cntc_icodigo_plan = Convert.ToInt32(lkpCodigoPlan.EditValue);
                Obe.strcodigoplan = lkpCodigoPlan.Text;
                Obe.cntc_icod_nombre_plan = Convert.ToInt32(lkpNombrePlan.EditValue);
                Obe.cntc_vnombre_plan = lkpNombrePlan.Text;
                Obe.cntc_nprecio_lista = Convert.ToDecimal(txtPrecioLista.Text);
                Obe.cntc_ndescuento = Convert.ToDecimal(txtDescuento.Text);
                Obe.cntc_ninhumacion = Convert.ToDecimal(txtInhumacion.Text);
                Obe.cntc_naporte_fondo = Convert.ToDecimal(txtAporteFondo.Text);
                Obe.cntc_nIGV = Convert.ToDecimal(txtIGV.Text);
                Obe.cntc_nprecio_total = Convert.ToDecimal(txtPrecioTotal.Text);
                Obe.cntc_itipo_sepultura = Convert.ToInt32(lkpTipoSepultura.EditValue);
                Obe.strtiposepultura = lkpTipoSepultura.Text;
                Obe.cntc_vcapacidad_contrato = txtCapacContrat.Text;
                Obe.cntc_vcapacidad_total = txtCapacTotal.Text;
                Obe.cntc_vurnas = txtUrnas.Text;
                Obe.cntc_vservico_inhumacion = txtServInhumacion.Text;
                Obe.cntc_icod_plataforma = Convert.ToInt32(lkpPlataforma.EditValue);
                Obe.strplataforma = lkpPlataforma.Text;
                Obe.cntc_icod_manzana = Convert.ToInt32(lkpManzana.EditValue);
                Obe.strmanzana = lkpManzana.Text;
                Obe.cntc_icod_isepultura = Convert.ToInt32(bteSepultura.Tag);
                Obe.espac_iid_iespacios = Convert.ToInt32(btnEspacios.Tag);
                Obe.strsepultura = bteSepultura.Text;
                Obe.cntc_bnivel1 = ckbNivel1.Checked;
                if (ckbNivel1.Checked == true)
                {
                    Obe.espad_iid_iespacios1 = icodEspacioDet[0];
                }
                else if (ckbNivel1.Checked == false && ckbNivel1.Enabled == false)
                {
                    Obe.espad_iid_iespacios1 = icodEspacioDet[0];
                }
                else
                {
                    Obe.espad_iid_iespacios1 = 0;
                }

                Obe.cntc_bnivel2 = ckbNivel2.Checked;
                if (ckbNivel2.Checked == true)
                {
                    Obe.espad_iid_iespacios2 = icodEspacioDet[1];
                }
                else if (ckbNivel2.Checked == false && ckbNivel2.Enabled == false)
                {
                    Obe.espad_iid_iespacios2 = icodEspacioDet[1];
                }
                else
                {
                    Obe.espad_iid_iespacios2 = 0;
                }

                Obe.cntc_bnivel3 = ckbNivel3.Checked;
                if (ckbNivel3.Checked == true)
                {
                    Obe.espad_iid_iespacios3 = icodEspacioDet[2];
                }
                else if (ckbNivel3.Checked == false && ckbNivel3.Enabled == false)
                {
                    Obe.espad_iid_iespacios3 = icodEspacioDet[2];
                }
                else
                {
                    Obe.espad_iid_iespacios3 = 0;
                }

                Obe.cntc_bnivel4 = ckbNivel4.Checked;
                if (ckbNivel4.Checked == true)
                {
                    Obe.espad_iid_iespacios4 = icodEspacioDet[3];
                }
                else if (ckbNivel4.Checked == false && ckbNivel4.Enabled == false)
                {
                    Obe.espad_iid_iespacios4 = icodEspacioDet[3];
                }
                else
                {
                    Obe.espad_iid_iespacios4 = 0;
                }

                Obe.cntc_bnivel5 = ckbNivel5.Checked;
                if (ckbNivel5.Checked == true)
                {
                    Obe.espad_iid_iespacios5 = icodEspacioDet[4];
                }
                else if (ckbNivel5.Checked == false && ckbNivel5.Enabled == false)
                {
                    Obe.espad_iid_iespacios5 = icodEspacioDet[4];
                }
                else
                {
                    Obe.espad_iid_iespacios5 = 0;
                }

                Obe.cntc_bnivel6 = ckbNivel6.Checked;
                if (ckbNivel6.Checked == true)
                {
                    Obe.espad_iid_iespacios6 = icodEspacioDet[5];
                }
                else if (ckbNivel6.Checked == false && ckbNivel6.Enabled == false)
                {
                    Obe.espad_iid_iespacios6 = icodEspacioDet[5];
                }
                else
                {
                    Obe.espad_iid_iespacios6 = 0;
                }

                Obe.espad_iid_iespaciosT1 = icodEspacioDet[0];
                Obe.espad_iid_iespaciosT2 = icodEspacioDet[1];
                Obe.espad_iid_iespaciosT3 = icodEspacioDet[2];
                Obe.espad_iid_iespaciosT4 = icodEspacioDet[3];
                Obe.espad_iid_iespaciosT5 = icodEspacioDet[4];
                Obe.espad_iid_iespaciosT6 = icodEspacioDet[5];


                Obe.cntc_vcodigo_sepultura = txtCodigoSepultura.Text;
                Obe.cntc_vnumero_reserva = txtNrReserva.Text;
                Obe.cntc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;



                Obe.cntc_icod_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.strSituacion = lkpSituacion.Text;
                Obe.cntc_ncuota_inicial = Convert.ToDecimal(txtCuotaInicial.Text);
                Obe.cntc_inro_cuotas = Convert.ToInt32(txtNroCuotas.Text);
                Obe.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                if (String.IsNullOrEmpty(dtFechaCuota.Text) || dtFechaCuota.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_cuota = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.EditValue);
                }

                Obe.cntc_icod_indicador_espacios = Convert.ToInt32(lkpNiveles.EditValue);
                Obe.cntc_vobservaciones = txtObservaciones.Text;
                Obe.cntc_flag_verificacion = true;

                Obe.cntc_indicador_pre_contrato = indicador;

                Obe.cntc_nmonto_foma = Convert.ToDecimal(txtFoma.Text);

                Obe.flag_indicador = true;

                Obe.cntc_icod_foma_mante = Convert.ToInt32(lkpFomaMante.EditValue);
                Obe.cntc_icod_deceso = Convert.ToInt32(lkpDeceso.EditValue);
                Obe.cntc_nfinanciamientro = Convert.ToDecimal(txtFinanciamiento.Text);
                Obe.cntc_iindicador_contr_sol = Parametros.intContrato;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.cntc_icod_contrato = new BVentas().insertarContrato(Obe, lstDetalle);
                    obj = _obj();
                    int icodContratante = new BVentas().insertarContratante(obj);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarContrato(Obe, lstDetalle, lstDelete);
                    obj = _obj();
                    var obe = new BVentas().listarContratantes(Obe.cntc_icod_contrato).Where(x => x.cntcc_bflag_seleccion == true).FirstOrDefault();
                    if (obe == null)// SI NO TIENE CONTRANTES CREA UNO NUEVO 
                    {
                        obj = _obj();
                        int icodContratante = new BVentas().insertarContratante(obj);
                    }
                    else
                    {
                        obj.cntcc_icod_contratante = obe.cntcc_icod_contratante;
                        new BVentas().modificarContratatante(obj);
                    }

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

        public EContratante _obj()
        {
            EContratante _obj = new EContratante();

            _obj.cntc_icod_contrato = Obe.cntc_icod_contrato;
            _obj.cntcc_icod_contratante = txtNombreContratante.Tag != null ? Convert.ToInt32(txtNombreContratante.Tag) : 1;
            _obj.cntcc_iid_contratante = Obe.cntcc_iid_contratante == 0 ? 1 : Obe.cntcc_iid_contratante;
            _obj.cntcc_vnombre_contratante = txtNombreContratante.Text;
            _obj.cntcc_vapellido_paterno_contratante = txtApellidoPContratante.Text;
            _obj.cntcc_vapellido_materno_contratante = txtApellidoMContratante.Text;
            _obj.cntcc_vdni_contratante = txtDOCContratante.Text;
            _obj.cntcc_vruc_contratante = txtRucContratante.Text;
            _obj.cntcc_sfecha_nacimineto_contratante = dtFechaNacContratante.DateTime.Year == 1 ? (DateTime?)null : dtFechaNacContratante.DateTime;
            _obj.cntcc_vtelefono_contratante = txtTelContratante.Text;
            _obj.cntcc_vdireccion_correo_contratante = txtCorreo.Text;
            _obj.cntcc_vdireccion_contratante = txtDireccionContratante.Text;
            _obj.cntcc_inacionalidad_contratante = Convert.ToInt32(lkpNacionalidad.EditValue);
            _obj.cntcc_itipo_documento_contratante = Convert.ToInt32(lkpTipoDocContratante.EditValue);
            _obj.cntcc_iusuario_crea = Valores.intUsuario;
            _obj.cntcc_sfecha_crea = DateTime.Today;
            _obj.cntcc_vpc_crea = WindowsIdentity.GetCurrent().Name;
            _obj.cntcc_iusuario_modifica = Valores.intUsuario;
            _obj.cntcc_sfecha_modifica = DateTime.Today;
            _obj.cntcc_vpc_modifica = WindowsIdentity.GetCurrent().Name;
            _obj.cntcc_bflag_estado = true;
            _obj.cntcc_bflag_seleccion = true;
            return _obj;
        }
        private bool verificarCodigoZona(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstContrato.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstContrato.Where(x => x.cntc_iid_contrato.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstContrato.Where(x => x.cntc_iid_contrato.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.cntc_icod_contrato != Obe.cntc_icod_contrato).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
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
            BSControls.LoaderLook(lkpPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            //BSControls.LoaderLook(lkpNivel, new BGeneral().listarTablaVentaDet(6), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpManzana, new BGeneral().listarTablaVentaDet(5), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNombreIEC, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpEstadoCivil, new BGeneral().listarTablaRegistro(78), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpNacionalidad, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpNacionalidad1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpDistrito, new BVentas().listarDistrito(), "disc_vdescripcion", "disc_icod_distrito", false);
            BSControls.LoaderLook(lkpNombrePlan, new BGeneral().listarTablaVentaDet(13), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(14), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoPagoC19, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

            BSControls.LoaderLook(lkpNacionalidadContratante, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocContratante, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);

            BSControls.LoaderLook(lkpDeceso, new BGeneral().listarTablaVentaDet(20), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpFomaMante, new BGeneral().listarTablaVentaDet(21), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNumDoc();
            }

        }

        public void getNumDoc() {
            var parametro = new BVentas().listarRegistroParametro()[0];
            string numero = $"0000000{parametro.rgpmc_icorrelativo_contrato + 1}";
            numero = numero.Substring((parametro.rgpmc_icorrelativo_contrato + 1).ToString().Length);
            lblCorrelativo.Text = $"{parametro.rgpmc_vserie_contrato}{numero}";
            Obe.cntc_vnumero_contrato_corr = lblCorrelativo.Text;
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
            //List<ETablaVentaDet> list = new List<ETablaVentaDet>(); 
            //list = new BGeneral().listarTablaVentaDet(2).Where(x => x.tabvd_iid_tabla_venta_det = lkpCodigoPlan.EditValue);
            //txtNombrePlan.Text = list[0].tabvd_vdescripcion;
        }

        private void txtDNIContratante_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDOCContratante.Text == "")
            {
                txtRucContratante.Enabled = true;
            }
            else
            {
                txtRucContratante.Enabled = false;
            }
        }

        private void txtRucContratante_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRucContratante.Text == "")
            {
                txtDOCContratante.Enabled = true;
            }
            else
            {
                txtDOCContratante.Enabled = false;
            }
        }
        private CheckBox[] GetTextoCantidad()
        {
            CheckBox[] texto = new CheckBox[] { ckbNivel1, ckbNivel2, ckbNivel3, ckbNivel4, ckbNivel5, ckbNivel6 };
            return texto;
        }
        public string TotalNivel { get; set; }
        private void bteSepultura_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarTablaSepultura frm = new FrmListarTablaSepultura())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteSepultura.Tag = frm._Be.tabvd_iid_tabla_venta_det;
                        bteSepultura.Text = frm._Be.tabvd_vdescripcion;

                        CheckBox[] textoCantidad = GetTextoCantidad();
                        List<EEspacios> lstNiveles = new List<EEspacios>();
                        lstNiveles = new BVentas().listarEspacios().Where(x => x.espac_isepultura == Convert.ToInt32(bteSepultura.Tag)).ToList();
                        if (lstNiveles.Count > 0)
                        {
                            lkpPlataforma.EditValue = lstNiveles[0].espac_icod_iplataforma;
                            lkpPlataforma.Text = lstNiveles[0].strplataforma;
                            lkpManzana.EditValue = lstNiveles[0].espac_icod_imanzana;
                            lkpManzana.Text = lstNiveles[0].strmanzana;
                            int count = 0;

                            for (int i = 0; i < lstNiveles.Count; i++)
                            {
                                textoCantidad[i].Enabled = true;

                            }

                        }

                        TotalNivel = lstNiveles.Count.ToString();
                        txtCapacTotal.Text = TotalNivel;
                        //bteSepultura.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int[] icodEspacioDet = new int[7];

        public int[] icodEspacioDetT = new int[7];

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarSepultura frm = new FrmListarSepultura())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnEspacios.Tag = frm._Be.espac_iid_iespacios;
                        btnEspacios.Text = frm._Be.espac_icod_vespacios;

                        CheckBox[] textoCantidad = GetTextoCantidad();
                        List<EEspaciosDet> lstNivelesDet = new List<EEspaciosDet>();
                        lstNivelesDet = new BVentas().listarEspaciosDet(frm._Be.espac_iid_iespacios);
                        if (lstNivelesDet.Count > 0)
                        {
                            lkpPlataforma.EditValue = frm._Be.espac_icod_iplataforma;
                            lkpPlataforma.Text = frm._Be.strplataforma;
                            lkpManzana.EditValue = frm._Be.espac_icod_imanzana;
                            lkpManzana.Text = frm._Be.strmanzana;
                            bteSepultura.Tag = frm._Be.espac_isepultura;
                            bteSepultura.Text = frm._Be.strsepultura;
                            int count = 0;

                            lstNivelesDet.ForEach(x =>
                            {
                                if (x.espad_icod_isituacion == 13)
                                {
                                    textoCantidad[count].Enabled = true;
                                    icodEspacioDet[count] = x.espad_iid_iespacios;
                                }
                                else
                                {
                                    textoCantidad[count].Enabled = false;

                                }

                                count++;
                            });

                        }

                        TotalNivel = lstNivelesDet.Count.ToString();
                        txtCapacTotal.Text = TotalNivel;
                        Niveles();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BaseEdit oBase = null;
            //try
            //{

            //    using (frmManteContratosBeneficiarios frm = new frmManteContratosBeneficiarios())
            //    {
            //        frm.SetInsert();
            //        frm.lstDetalle = lstDetalle;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            lstDetalle = frm.lstDetalle;
            //            grdDetalle.DataSource = lstDetalle;
            //            viewDetalle.RefreshData();
            //            viewDetalle.Focus();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (oBase != null)
            //    {
            //        oBase.Focus();
            //        oBase.ErrorText = ex.Message;
            //        oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //    }
            //    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EContratoBeneficiario oBe_ = (EContratoBeneficiario)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            //if (oBe_ == null)
            //    return;
            //using (frmManteContratosBeneficiarios frm = new frmManteContratosBeneficiarios())
            //{
            //    frm.Obe = oBe_;
            //    frm.SetModify();
            //    frm.lstDetalle = lstDetalle;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstDetalle = frm.lstDetalle;
            //        viewDetalle.RefreshData();
            //        viewDetalle.Focus();
            //    }
            //}
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //EContratoBeneficiario oBe_ = (EContratoBeneficiario)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            //if (oBe_ == null)
            //    return;
            //lstDelete.Add(oBe_);
            //lstDetalle.Remove(oBe_);
            //viewDetalle.RefreshData();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void btnEspacios_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (XtraMessageBox.Show("¿Esta seguro que desea Limpiar el Espacio, se eliminara toda relacion?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                List<EEspaciosDet> lstEpaDet = new BVentas().listarEspaciosDet(Convert.ToInt32(btnEspacios.Tag));
                lstEpaDet.ForEach(x =>
                {
                    if (x.cntc_icod_contrato == Obe.cntc_icod_contrato)
                    {
                        x.espad_icod_isituacion = 13;
                        x.cntc_icod_contrato = 0;
                        new BVentas().modificarEspaciosDet(x);
                    }
                    //icodEspacioDet[count] = 0;

                });
                lkpPlataforma.EditValue = 0;
                lkpPlataforma.Text = "";
                lkpManzana.EditValue = 0;
                lkpManzana.Text = "";
                bteSepultura.Tag = 0;
                bteSepultura.Text = "";
                btnEspacios.Tag = 0;
                btnEspacios.Text = "";
                ckbNivel1.Checked = false;
                ckbNivel2.Checked = false;
                ckbNivel3.Checked = false;
                ckbNivel4.Checked = false;
                ckbNivel5.Checked = false;
                ckbNivel6.Checked = false;

                ckbNivel1.Enabled = false;
                ckbNivel2.Enabled = false;
                ckbNivel3.Enabled = false;
                ckbNivel4.Enabled = false;
                ckbNivel5.Enabled = false;
                ckbNivel6.Enabled = false;
                icodEspacioDet[0] = 0;
                icodEspacioDet[1] = 0;
                icodEspacioDet[2] = 0;
                icodEspacioDet[3] = 0;
                icodEspacioDet[4] = 0;
                icodEspacioDet[5] = 0;
                txtCapacTotal.Text = "";
                btnEspacios.Enabled = true;
            }
        }
        public void Niveles()
        {
            List<EEspaciosDet> listNiveles = new List<EEspaciosDet>();
            listNiveles = new BVentas().listarEspaciosDet(Convert.ToInt32(btnEspacios.Tag));
            BSControls.LoaderLook(lkpNiveles, listNiveles, "espad_vnivel", "espad_iid_iespacios", true);

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl74_Click(object sender, EventArgs e)
        {

        }

        private void txtNumer_Leave(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew && !cargando)
            {
                lkpNombreIEC.Enabled = true;
                if (txtNumer.Text != "0000000")
                {
                    bool existeSerie = new BVentas().ObtenerExistenciaSerie(txtSerie.Text + txtNumer.Text);
                    if (existeSerie)
                    {
                        XtraMessageBox.Show(string.Format("Ya Existe un Pre contrato con la serie {0}-{1}", txtSerie.Text, txtNumer.Text), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNumer.Focus();
                    }

                    EVendedor ObjIEC = new BVentas().ObtenerIECDesdeFormatos(txtSerie.Text + txtNumer.Text);

                    lkpNombreIEC.EditValue = ObjIEC != null ? ObjIEC.vendc_icod_vendedor : 0;



                }
                else
                {
                    XtraMessageBox.Show("Ingrese La serie del Pre Contrato", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNumer.Focus();
                }


            }



        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {

        }

        void calcularTotal()
        {

            if (lkpTipoPagoC19.Text != "CONTADO")
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

        private void txtCuotaInicial_EditValueChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void txtNroCuotas_EditValueChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void txtMontoCuotas_EditValueChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void lkpTipoPagoC19_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoPagoC19.Text == "CONTADO")
            {
                txtCuotaInicial.Properties.ReadOnly = true;
                txtNroCuotas.Properties.ReadOnly = true;
                txtMontoCuotas.Properties.ReadOnly = true;
                dtFechaCuota.Enabled = false;
                txtCuotaInicial.Text = "0";
                txtMontoCuotas.Text = "0";
                txtNroCuotas.Text = "0";
                dtFechaCuota.EditValue = (DateTime?)null;
            }
            else
            {
                txtCuotaInicial.Properties.ReadOnly = false;
                txtNroCuotas.Properties.ReadOnly = false;
                txtMontoCuotas.Properties.ReadOnly = false;
                dtFechaCuota.Enabled = true;
            }

            calcularTotal();
        }
        public void deshabilitarCaja()
        {
            lkpNombreIEC.Enabled = false;
            lkpOrigenVenta.Enabled = false;
            btnFunerarios.Enabled = false;
            txtObservaciones.Enabled = false;
            txtCorreo.Enabled = false;
            txtDireccionContratante.Enabled = false;
            lkpTipoDocContratante.Enabled = false;
            txtDOCContratante.Enabled = false;
            txtRucContratante.Enabled = false;
            dtFechaNacContratante.Enabled = false;
            txtTelContratante.Enabled = false;
            lkpNacionalidadContratante.Enabled = false;
            lkpCodigoPlan.Enabled = false;
            lkpNombrePlan.Enabled = false;
            txtPrecioLista.Enabled = false;
            txtDescuento.Enabled = false;
            txtInhumacion.Enabled = false;
            lkpDeceso.Enabled = false;
            txtPagoCovid19.Enabled = false;
            lkpFomaMante.Enabled = false;
            txtAporteFondo.Enabled = false;
            txtIGV.Enabled = false;
            lkpTipoPagoC19.Enabled = false;
            txtCuotaInicial.Enabled = false;
            txtNroCuotas.Enabled = false;
            txtMontoCuotas.Enabled = false;
            dtFechaCuota.Enabled = false;
            txtPrecioTotal.Enabled = false;
            lkpTipoSepultura.Enabled = false;
            txtFinanciamiento.Enabled = false;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtAporteFondo.Enabled = true;
                txtFinanciamiento.Enabled = true;
                lkpFomaMante.Enabled = false;
            }
            else
            {
                txtAporteFondo.Enabled = false;
                txtFinanciamiento.Enabled = true;
                lkpFomaMante.Enabled = false;
            }
        }

        public void deshabilitarCobranzas()
        {
            txtSerie.Enabled = false;
            txtNumer.Enabled = false;
            dtFecha.Enabled = false;
            lkpSituacion.Enabled = false;
            lkpNombreIEC.Enabled = false;
            lkpOrigenVenta.Enabled = false;
            btnFunerarios.Enabled = false;
            txtObservaciones.Enabled = false;
            txtNombreContratante.Enabled = false;
            lkpTipoDocContratante.Enabled = false;
            txtApellidoPContratante.Enabled = false;
            txtDOCContratante.Enabled = false;
            txtApellidoMContratante.Enabled = false;
            txtRucContratante.Enabled = false;
            txtCorreo.Enabled = false;
            dtFechaNacContratante.Enabled = false;
            txtDireccionContratante.Enabled = false;
            txtTelContratante.Enabled = false;
            lkpNacionalidadContratante.Enabled = false;
            lkpCodigoPlan.Enabled = false;
            lkpNombrePlan.Enabled = false;
            lkpTipoSepultura.Enabled = false;

            

        }

        private void txtPrecioLista_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoPagoC19.Text == "CONTADO")
            {
                txtPrecioTotal.Text = (Convert.ToDecimal(txtPrecioLista.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
            }
        }

        private void lkpDeceso_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpDeceso.Text == "NATURAL")
            {
                txtPagoCovid19.Properties.ReadOnly = true;
            }
            else
            {
                txtPagoCovid19.Properties.ReadOnly = false;
            }
        }

        private void txtFinanciamiento_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}