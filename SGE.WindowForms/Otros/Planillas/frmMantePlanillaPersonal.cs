using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Planillas.Registro_de_Datos;
using SGE.WindowForms.Otros.Planillas;
using System.IO;
using System.Data.OleDb;



namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmMantePlanillaPersonal : DevExpress.XtraEditors.XtraForm
    {

        List<EPlanillaPersonalDetalleNuevo> lstPlanillaDetalle = new List<EPlanillaPersonalDetalleNuevo>();

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantePlanillaPersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EPlanillaPersonal oBe = new EPlanillaPersonal();
        /*----------------------------------------------------*/
        public List<EFondosPensiones> lstFondPensionAÑOyMES = new List<EFondosPensiones>();
        /*----------------------------------------------------*/

        List<EPlanillaPersonalDetalle> lstDetalle = new List<EPlanillaPersonalDetalle>();
        List<EPlanillaPersonalDetalle> lstDetalleEliminados = new List<EPlanillaPersonalDetalle>();
        /*----------------------------------------------------*/
        public int codCuotaPrestamo = 0;
        public int cese = 0;
        public int Contrato = 0;
        public int FechInic, FechaFin, SumFecha = 0; //---Para fecha de contrato
        public int ctsmes = 0, ctsdias = 0;
        public DateTime? fechaCese; public int EstFechaCese, SumaFechaCese = 0;
        public int i = 1;
        public int i2 = 0;
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
        public frmMantePlanillaPersonal()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BPlanillas().listarTablaPlanillaDetalle(22), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipo, new BPlanillas().listarTablaPlanillaDetalle(23).Where(z => z.tbpd_icod_tabla_planilla_detalle == 6435).ToList(), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpQuincena, new BGeneral().listarTablaRegistro(100), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpTipoPersonal, new BPlanillas().listarComboTablaPlanillaDetalle(18), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);

            Carga();
            lkpMes.EditValue = Convert.ToInt32(DateTime.Now.Month);
            lkpSituacion.EditValue = 6433;
            lkpTipo.EditValue = 6435;
            dteFecha.EditValue = DateTime.Now;

            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
            {
                lkpMes.EditValue = oBe.mesec_iid_mes;
                lkpSituacion.EditValue = oBe.tablc_iid_situacion_planilla;
                lkpTipo.EditValue = oBe.planc_iid_tipo_planilla;
            }
        }

        private void Carga()
        {
            if (Status != BSMaintenanceStatus.CreateNew)
            {
                if (oBe.planc_iid_tipo_planilla == 6435)
                {
                    lstPlanillaDetalle = new BPlanillas().listarPlanillaPersonalDet(oBe.planc_icod_planilla_personal).OrderBy(z => z.pland_ape_nom).ToList();
                    lstPlanillaDetalle.ForEach(x =>
                    {
                        x.A = x.pland_observaciones;
                    });
                    grdCTS.DataSource = lstPlanillaDetalle;
                }
            }
        }

        private void StatusControl()
        {
            if (Status == BSMaintenanceStatus.View)
            {
                txtNumPlanilla.Enabled = false;
                txtObservaciones.Enabled = false;
                lkpSituacion.Enabled = false;
                lkpTipo.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                lkpMes.Enabled = false;
                btnBuscar.Enabled = false;
                mnu.Enabled = false;
            }
        }

        public void setValues()
        {
            txtNumPlanilla.Tag = oBe.planc_iid_planilla_personal;
            txtNumPlanilla.Text = oBe.NumPlanilla;
            txtObservaciones.Text = oBe.planc_vdescripcion;
            dteFecha.EditValue = oBe.planc_sfecha;
            lkpQuincena.EditValue = oBe.planc_iid_quincena;
            lkpTipoPersonal.EditValue = oBe.planc_iid_tipoPersonal;
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }


        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (lstPlanillaDetalle.Count == 0)
                {
                    throw new ArgumentException("Ingresar al menos un producto");
                }

                if (verificarCCostos(txtNumPlanilla.Text))
                {
                    oBase = lkpMes;
                    throw new ArgumentException(string.Format("No se puede Registrar dos Planillas Personal de la misma Quincena del Mes {0} del Tipo {1} ", lkpMes.Text, lkpTipoPersonal.Text));
                }

                oBe.planc_iid_planilla_personal = Convert.ToInt32(txtNumPlanilla.Text);
                oBe.planc_iid_anio = Parametros.intEjercicio;
                oBe.mesec_iid_mes = Convert.ToInt32(lkpMes.EditValue);
                oBe.tablc_iid_situacion_planilla = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.planc_iid_tipo_planilla = Convert.ToInt32(lkpTipo.EditValue);
                oBe.planc_vdescripcion = txtObservaciones.Text;
                oBe.planc_sfecha = Convert.ToDateTime(dteFecha.EditValue);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.planc_iid_quincena = Convert.ToInt32(lkpQuincena.EditValue);
                oBe.planc_iid_tipoPersonal = Convert.ToInt32(lkpTipoPersonal.EditValue);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.planc_icod_planilla_personal = new BPlanillas().insertarPlanillaPersonal(oBe, lstPlanillaDetalle);
                }
                else
                {
                    new BPlanillas().modificarPlanillaPersonal(oBe, lstPlanillaDetalle, lstDetalleEliminados);
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
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.planc_icod_planilla_personal);
                    this.Close();
                }
            }
        }

        private bool verificarCCostos(string planilla_detalle)
        {
            try
            {
                bool exists = false;
                List<EPlanillaPersonal> lstplanilla = new List<EPlanillaPersonal>();
                lstplanilla = new BPlanillas().listarPlanillaPersonal();
                if (lstplanilla.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstplanilla.Where(x => x.mesec_iid_mes == Convert.ToInt32(lkpMes.EditValue) && // VALIDADON EL MES
                        x.planc_iid_tipoPersonal == Convert.ToInt32(lkpTipoPersonal.EditValue) && //VALIDADNDO EL TIPO DE PERSONAL
                        x.planc_iid_quincena == Convert.ToInt32(lkpQuincena.EditValue) &&  // VALIDANDO LA QUINCENA
                        x.planc_iid_anio == Convert.ToInt32(Parametros.intEjercicio)).ToList().Count > 0) //VALIDANDO EL AÑO DE EJERCICIO 
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
        public string observaciones = "";

        private void viewCTS_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            EPlanillaPersonalDetalleNuevo PPD = (EPlanillaPersonalDetalleNuevo)viewCTS.GetRow(viewCTS.FocusedRowHandle);

            if (PPD != null)
            {
                PPD.pland_observaciones = observaciones;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 6435)
            {
                int tipoPersonal = Convert.ToInt32(lkpTipoPersonal.EditValue);
                List<EPersonal> lstPersonal = new List<EPersonal>();
                lstPersonal = new BPlanillas().listarPersonal().Where(x=>x.perc_icod_tip_personal == tipoPersonal).OrderBy(s => s.ApellNomb).ToList();
                lstFondPensionAÑOyMES = new BPlanillas().listarFondosPensiones();
                BaseEdit oBase = null;
                try
                {

                    if (lstFondPensionAÑOyMES.Count == 0)
                    {
                        throw new ArgumentException("No existe Fondo de Pensiones");
                    }

                    if (lstPersonal.Count ==0)
                    {
                        throw new ArgumentException(string.Format("No existe personal del tipo {0}",lkpTipoPersonal.Text));
                    }

                    if (lstDetalle.Count > 0)
                    {
                        lstDetalle.Clear();
                    }

                    foreach (var item in lstPersonal)
                    {
                        int indicadorQuincena = Convert.ToInt32(lkpQuincena.EditValue);
                        List<EParametroPlanilla> lstParametroPlanilla = new List<EParametroPlanilla>();
                        lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                        EPlanillaPersonalDetalleNuevo PPD = new EPlanillaPersonalDetalleNuevo();
                        PPD.pland_observaciones = "";
                        PPD.planc_icod_personal = item.perc_icod_personal;
                        PPD.pland_iid_planilla_personal_det = i++;
                        PPD.pland_ape_nom = item.ApellNomb;
                        PPD.pland_sfecha_incio = Convert.ToDateTime(item.perc_sfech_inicio);
                        PPD.pland_num_doc = item.perc_vnum_doc;
                        PPD.pland_afp = Convert.ToInt32(item.perc_icod_afp);
                        PPD.strpland_afp = item.fdpc_vdescripcion;
                        PPD.pland_cussp = item.perc_vcuspp;
                        PPD.pland_sueldo_basico = Convert.ToDecimal(item.perc_nmont_basico);
                        if (item.perc_basig_familiar == true)
                        {
                            PPD.pland_nasignacion_familiar = Convert.ToDecimal(lstParametroPlanilla[0].prpc_nasignacion_familiar);
                        }
                        else
                        {
                            PPD.pland_nasignacion_familiar = 0;
                        }
                        PPD.pland_nferiados = 0;
                        PPD.pland_Mtardanzas = 0;
                        PPD.pland_tardanzas = 0;
                        PPD.pland_reintegro = 0;

                        if (item.perc_nasig_transporte == 0 || item.perc_nasig_transporte == null)
                        {
                            PPD.pland_nasignacion_transporte = 0;
                        }
                        else
                        {
                            PPD.pland_nasignacion_transporte = Convert.ToDecimal(item.perc_nasig_transporte);
                        }
                        PPD.pland_bonos = 0;
                        PPD.pland_comisiones = 0;

                        var lstaFaltas = new BPlanillas().ListarInasistenciaPersonal();
                        lstaFaltas = lstaFaltas.Where(x => x.peric_icod_personal == item.perc_icod_personal && Convert.ToInt32(x.peric_sfecha_anasist.Month) == Convert.ToInt32(lkpMes.EditValue)).ToList() ;
                        if (indicadorQuincena == 1)//PRIMERA QUINCENA
                        {
                            
                            PPD.pland_faltas = lstaFaltas.Where(x => Convert.ToInt32(x.peric_sfecha_anasist.Day) <= 15).Count();
                        }
                        else//SEGUNDA QUINCENA
                        {
                            PPD.pland_faltas = lstaFaltas.Where(x => Convert.ToInt32(x.peric_sfecha_anasist.Day) > 15).Count();
                        }



                        PPD.pland_total_neto = Math.Round((Convert.ToDecimal(((PPD.pland_sueldo_basico + PPD.pland_nasignacion_familiar) / 2) + PPD.pland_reintegro + PPD.pland_nasignacion_transporte + PPD.pland_bonos + PPD.pland_comisiones)), 2);

                        if (item.perc_icod_tip_comision == 6386)//FIJA
                        {
                            List<EFondosPensiones> lstfondoPensiones = new List<EFondosPensiones>();
                            lstfondoPensiones = new BPlanillas().listarFondosPensiones().Where(x => x.fdpc_icod_fondo_pension == item.perc_icod_afp).ToList();

                            if (item.perc_icod_afp == 1) //INTEGRA
                            {

                            }
                            else if (item.perc_icod_afp == 2) // PROFUTURO
                            {

                                List<EFondosPensionesConceptos> lstfondoPensionConceptosConceptos = new List<EFondosPensionesConceptos>();
                                lstfondoPensionConceptosConceptos = new BPlanillas().listarFondosPensionesConceptos(2);
                                foreach (var item2 in lstfondoPensionConceptosConceptos)
                                {
                                    if (item2.fdpd_iid_vcodigo_fondo_concepto == "01")// PRIMA SEGURO
                                    {
                                        decimal intporcetAporte = Convert.ToDecimal(item2.fdpd_nporcentaje_concepto);
                                        string porcetAporte = "0.0" + intporcetAporte.ToString().Replace(".", "");
                                        PPD.pland_obligat = Math.Round((Convert.ToDecimal(PPD.pland_total_neto * Convert.ToDecimal(porcetAporte))), 2);
                                    }
                                    if (item2.fdpd_iid_vcodigo_fondo_concepto == "02")// COMISION
                                    {
                                        decimal intporcetAporte = Convert.ToDecimal(item2.fdpd_nporcentaje_concepto);
                                        string porcetAporte = "0.0" + intporcetAporte.ToString().Replace(".", "");
                                        PPD.pland_seguro = Math.Round((Convert.ToDecimal(PPD.pland_total_neto * Convert.ToDecimal(porcetAporte))), 2);
                                    }
                                    if (item2.fdpd_iid_vcodigo_fondo_concepto == "03") // APORTE
                                    {
                                        int intporcetSeguro = Convert.ToInt32(item2.fdpd_nporcentaje_concepto);
                                        string porcetSeguro = "0." + intporcetSeguro;
                                        PPD.pland_porcent = Math.Round((Convert.ToDecimal(PPD.pland_total_neto * Convert.ToDecimal(porcetSeguro))), 2);

                                    }

                                }

                            }
                            else if (item.perc_icod_afp == 3) //HABITAD
                            {

                            }
                            else if (item.perc_icod_afp == 4) // PRIMA
                            {

                            }
                            else if (item.perc_icod_afp == 5) // O.N.P.
                            {

                            }



                        }
                        else //MIXTA
                        {



                        }


                        if (!Convert.ToBoolean(item.perc_brta_5ta))
                        {
                            List<EPlanillaModeloCont> rent_5ta = new BPlanillas().ObtnerRentaPersonal(item.perc_icod_personal);
                            EPlanillaModeloCont objRenta = rent_5ta.OrderByDescending(x => x.plcc_pland_icod).FirstOrDefault();

                            switch (Convert.ToInt32(lkpMes.EditValue))
                            {
                                case 1:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_enero;
                                    break;
                                case 2:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_febrero;
                                    break;
                                case 3:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_marzo;
                                    break;
                                case 4:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_abril;
                                    break;
                                case 5:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_mayo;
                                    break;
                                case 6:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_junio;
                                    break;
                                case 7:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_julio;
                                    break;
                                case 8:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_agosto;
                                    break;
                                case 9:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_setiembre;
                                    break;
                                case 10:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_octubre;
                                    break;
                                case 11:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_noviembre;
                                    break;
                                case 12:
                                    PPD.pland_desc_renta5 = objRenta.plcd_montos_diciembre;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            PPD.pland_desc_renta5 = 0;
                        }


                        PPD.pland_adelanto = 0;

                        EPrestamo objPres = new BPlanillas().ListarPrestamosPersonal().Where(x => x.prtpc_icod_situacion == 218 && x.prtpc_icod_personal == item.perc_icod_personal).FirstOrDefault();

                        if (objPres != null)
                        {

                            var lstPrestamo = new BPlanillas().ListarPrestamoCuotasPersonal(objPres.prtpc_icod_prestamo);
                            lstPrestamo = lstPrestamo.Where(x => Convert.ToInt32(x.prtpd_sfecha_cuota.Month) == Convert.ToInt32(lkpMes.EditValue) && x.prtpd_icod_situacion == 157 && x.prtpd_sfecha_cuota.Year == Parametros.intEjercicio).ToList();

                            if (indicadorQuincena == 1) //PRIMERA QUINCENA
                            {
                                lstPrestamo = lstPrestamo.Where(x => x.prtpd_sfecha_cuota.Day == 15).ToList();
                                PPD.pland_prestamo = Convert.ToDecimal(lstPrestamo.Select(X => X.prtpd_nmonto_cuota).FirstOrDefault());
                                codCuotaPrestamo = Convert.ToInt32(lstPrestamo.Select(X => X.prtpd_icod_prestamo_det).FirstOrDefault()); //PARA CAMBIAR ESTADO DE PRESTAMO
                            }
                            else //SEGUNDA QUINCENA
                            {
                                lstPrestamo =lstPrestamo.Where(x => x.prtpd_sfecha_cuota.Day > 15).ToList();
                                PPD.pland_prestamo = Convert.ToDecimal(lstPrestamo.Select(X => X.prtpd_nmonto_cuota));
                                codCuotaPrestamo = Convert.ToInt32(lstPrestamo.Select(X => X.prtpd_icod_prestamo_det)); //PARA CAMBIAR ESTADO DE PRESTAMO
                            }
                        }
                        else
                        {
                            PPD.pland_prestamo = 0;
                        }




                        PPD.pland_descuento = Math.Round((Convert.ToDecimal(PPD.pland_obligat + PPD.pland_seguro + PPD.pland_porcent + PPD.pland_desc_renta5 + PPD.pland_adelanto + PPD.pland_prestamo)), 2);
                        PPD.pland_regularizar = 0;
                        PPD.pland_total_pagar = Math.Round((Convert.ToDecimal(PPD.pland_total_neto - PPD.pland_descuento)), 2);
                        string porcentEsalud = "0.0" + lstParametroPlanilla[0].prpc_nporc_essalud.ToString().Replace(".", "");
                        PPD.pland_aport_essalud9 = Math.Round((PPD.pland_total_neto * Convert.ToDecimal(porcentEsalud)), 2);
                        PPD.pland_cuenta = "";
                        PPD.pland_observaciones = "";
                        PPD.pland_iusuario_crea = Valores.intUsuario;
                        PPD.pland_iusuario_modifica = Valores.intUsuario;
                        PPD.pland_strpc_crea = WindowsIdentity.GetCurrent().Name;
                        PPD.pland_strpc_modifica = WindowsIdentity.GetCurrent().Name;
                        lstPlanillaDetalle.Add(PPD);

                    }
                    grdCTS.DataSource = lstPlanillaDetalle;
                    grdCTS.RefreshDataSource();
                    btnBuscar.Enabled = false;
                    lkpMes.Enabled = false;
                    lkpTipo.Enabled = false;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        public void viewCTS_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.CreateNew)
            {


                EPlanillaPersonalDetalleNuevo PPD = (EPlanillaPersonalDetalleNuevo)viewCTS.GetRow(viewCTS.FocusedRowHandle);

                if (PPD != null)
                {
                    var lstParametroPlanilla = new BPlanillas().listarParametroPlanilla();
                    PPD.pland_total_neto = Math.Round((Convert.ToDecimal(((PPD.pland_sueldo_basico + PPD.pland_nasignacion_familiar) / 2) + PPD.pland_reintegro + PPD.pland_nasignacion_transporte + PPD.pland_bonos + PPD.pland_comisiones)), 2);
                    PPD.pland_descuento = Math.Round((Convert.ToDecimal(PPD.pland_obligat + PPD.pland_seguro + PPD.pland_porcent + PPD.pland_desc_renta5 + PPD.pland_adelanto + PPD.pland_prestamo)), 2);
                    PPD.pland_total_pagar = Math.Round((Convert.ToDecimal(PPD.pland_total_neto - PPD.pland_descuento)), 2);
                    string porcentEsalud = "0.0" + lstParametroPlanilla[0].prpc_nporc_essalud.ToString().Replace(".", "");
                    PPD.pland_aport_essalud9 = Math.Round((PPD.pland_total_neto * Convert.ToDecimal(porcentEsalud)), 2);
                    PPD.pland_cuenta = "";
                    PPD.pland_observaciones = "";
                    PPD.pland_iusuario_crea = Valores.intUsuario;
                    PPD.pland_iusuario_modifica = Valores.intUsuario;
                    PPD.pland_strpc_crea = WindowsIdentity.GetCurrent().Name;
                    PPD.pland_strpc_modifica = WindowsIdentity.GetCurrent().Name;
                    PPD.pland_observaciones = PPD.A;
                }

                new BPlanillas().modificarPlanillaPersonalDet(PPD);
            }

        }


    }
}