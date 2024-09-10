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
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmMantePreContratoCaja : XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContrato Obe = new EContrato();
        public List<EContrato> lstContrato = new List<EContrato>();
        public List<EContratoFallecido> lstDetalle = new List<EContratoFallecido>();
        public List<EContratoFallecido> lstDelete = new List<EContratoFallecido>();
        public EContratante obj = new EContratante();
        public frmMantePreContratoCaja()
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
            btnGuardar.Enabled = !Enabled;
        }
        public void setValues()

        {
            txtSerie.Text = Obe.cntc_vnumero_contrato.Substring(0, 4);
            txtNumer.Text = Obe.cntc_vnumero_contrato.Substring(4);
            if (Obe.cntc_sfecha_contrato == null)
                Obe.cntc_sfecha_contrato = Convert.ToDateTime("01/01/0001");
            dtFecha.EditValue = Obe.cntc_sfecha_contrato.ToString().Substring(0, 10) == "01/01/0001" ? (DateTime?)null : Obe.cntc_sfecha_contrato;
            lkpNombreIEC.EditValue = Obe.cntc_icod_vendedor;
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
            txtPrecioLista.Text = Obe.cntc_nprecio_lista.ToString();
            txtAporteFondo.Text = Obe.cntc_naporte_fondo.ToString();
            lkpSituacion.EditValue = Obe.cntc_icod_situacion;
            if (Obe.cntc_icod_deceso != 0)

                txtNombreContratante.Enabled = string.IsNullOrWhiteSpace(Obe.cntc_vnombre_contratante);
            txtApellidoMContratante.Enabled = string.IsNullOrWhiteSpace(Obe.cntc_vapellido_materno_contratante);
            txtApellidoPContratante.Enabled = string.IsNullOrWhiteSpace(Obe.cntc_vapellido_paterno_contratante);
            txtCuotaInicial.Text = Obe.cntc_ncuota_inicial.ToString();
            bool pagado = new BVentas().listarContratoCuotas(Obe.cntc_icod_contrato).Where(x => x.cntc_icod_situacion == 340).Any();

            txtCuotaInicial.ReadOnly = pagado; 
            txtPrecioLista.ReadOnly = pagado;
            lkpTipoPagoC19.ReadOnly = pagado;

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

                if (Status == BSMaintenanceStatus.CreateNew && lstContrato.Where(x => x.cntc_vnumero_contrato == String.Format("{0}{1}", txtSerie.Text, txtNumer.Text)).ToList().Any())
                {
                    oBase = txtNumer;
                    throw new ArgumentException("Número de Contrato ya Existe");

                }

                if (string.IsNullOrWhiteSpace(txtNombreContratante.Text))
                {
                    oBase = txtNombreContratante;
                    throw new ArgumentException("Ingrese nombre del Contratante");
                }
                Obe.cntc_itipo_pago = Convert.ToInt32(lkpTipoPagoC19.EditValue);
                Obe.cntc_vnumero_contrato = String.Format("{0}{1}", txtSerie.Text, txtNumer.Text);
                Obe.cntc_sfecha_contrato = Convert.ToDateTime(dtFecha.EditValue);
                Obe.cntc_icod_vendedor = Convert.ToInt32(lkpNombreIEC.EditValue);
                Obe.strNombreIEC = lkpNombreIEC.Text;
                Obe.cntc_vnombre_contratante = txtNombreContratante.Text + " ";
                Obe.cntc_vapellido_paterno_contratante = txtApellidoPContratante.Text + " ";
                Obe.cntc_vapellido_materno_contratante = txtApellidoMContratante.Text + " ";
                Obe.cntc_nprecio_lista = Convert.ToDecimal(txtPrecioLista.Text);
                Obe.cntc_naporte_fondo = Convert.ToDecimal(txtAporteFondo.Text);
                Obe.cntc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.cntc_icod_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.strSituacion = lkpSituacion.Text;
                Obe.cntc_flag_verificacion = true;
                Obe.flag_indicador = true;
                Obe.cntc_iindicador_contr_sol = Parametros.intContrato;
                Obe.cntc_nprecio_total =   Obe.cntc_nprecio_lista;
                Obe.cntc_ncuota_inicial = Convert.ToDecimal(txtCuotaInicial.Text);
                Obe.cntc_iindicador_contr_sol = 2;
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

                var cuotas = new BVentas().listarContratoCuotas(Obe.cntc_icod_contrato);

                if (!cuotas.Where(x => x.cntc_icod_situacion == 340).Any() || !cuotas.Any())
                {
                    if (cuotas.Any())
                    {
                        cuotas.ForEach(x => new BVentas().eliminarContratoCuotas(x));
                    }
                    cuotas.Clear();
                    cuotas.Add(Obe.cntc_itipo_pago == (int)TipoPago.Credito ? cuotaModelCredito() : cuotaModelContado());
                    new BVentas().insertarCCuotas(cuotas);
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

        EContratoCuotas cuotaModelCredito()
        {
            EContratoCuotas EDetF = new EContratoCuotas();
            EDetF.cntc_icod_contrato = Obe.cntc_icod_contrato;
            EDetF.cntc_inro_cuotas = 0;
            EDetF.cntc_sfecha_cuota = Convert.ToDateTime(Obe.cntc_sfecha_contrato);
            EDetF.cntc_icod_tipo_cuota = 336;
            EDetF.cntc_nmonto_cuota = Convert.ToDecimal(Obe.cntc_ncuota_inicial);
            EDetF.cntc_icod_situacion = 338;
            EDetF.intUsuario = Obe.intUsuario;
            EDetF.cntc_nsaldo = EDetF.cntc_nmonto_cuota;
            EDetF.cntc_npagado = 0;
            EDetF.cntc_nmonto_mora_pago = 0;
            EDetF.cntc_itipo_cuota = 0;
            EDetF.cntc_nmonto_mora = 0;
            return EDetF;
        }

        EContratoCuotas cuotaModelContado()
        {

            ETablaVentaDet obj = new BGeneral().listarTablaVentaDet(15).FirstOrDefault(x => x.tabvd_iid_tabla_venta_det == 5430);
            EContratoCuotas EDetF = new EContratoCuotas();
            EDetF.cntc_icod_contrato = Obe.cntc_icod_contrato;
            EDetF.cntc_inro_cuotas = 1;
            EDetF.cntc_sfecha_cuota = Convert.ToDateTime(Obe.cntc_sfecha_contrato);
            EDetF.cntc_icod_tipo_cuota = obj.tabvd_iid_tabla_venta_det;
            EDetF.cntc_nmonto_cuota = Convert.ToDecimal(Obe.cntc_nprecio_total);
            EDetF.cntc_icod_situacion = 338;
            EDetF.intUsuario = Obe.intUsuario;
            EDetF.cntc_nsaldo = EDetF.cntc_nmonto_cuota;
            EDetF.cntc_npagado = 0;
            EDetF.cntc_nmonto_mora_pago = 0;
            EDetF.cntc_itipo_cuota = 0;
            EDetF.cntc_nmonto_mora = 0;
            return EDetF;
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
            BSControls.LoaderLook(lkpNombreIEC, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(14), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoPagoC19, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);

        }










        private void txtNumer_Leave(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
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

        private void lkpTipoPagoC19_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoPagoC19.EditValue) == (int)TipoPago.Credito)
            {
                txtCuotaInicial.ReadOnly = false;

            }
            else 
            {
                txtCuotaInicial.ReadOnly = true;
                if (lkpTipoPagoC19.ContainsFocus)
                    txtCuotaInicial.Text = 0.ToString(); 
            }
        }
    }
}