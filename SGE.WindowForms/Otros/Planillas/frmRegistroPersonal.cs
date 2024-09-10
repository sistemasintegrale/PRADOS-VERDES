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
using DevExpress.XtraEditors.Controls;
using SGE.WindowForms.Otros.Contabilidad;
using System.Threading.Tasks;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroPersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroPersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EPersonal Obe = new EPersonal();
        public List<EPersonal> lstPersonal = new List<EPersonal>();
        public List<EPersonal> lstPersonalContrato = new List<EPersonal>();
        public List<EPersonalCCostos> lstPersonalCC = new List<EPersonalCCostos>();
        public List<EPersonalCCostos> lstPersonalCCDelete = new List<EPersonalCCostos>();
        public List<EPersonal> lstPersonalContratoDelete = new List<EPersonal>();
        public EPersonalCCostos ObeCC = new EPersonalCCostos();
        List<EArchivos> lstArchivos = new List<EArchivos>();

        public frmRegistroPersonal()
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
            txtCodigo.Enabled = !Enabled;
            dteFechaRegistro.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;

            lkpTipDoc.Enabled = !Enabled;
            txtDocumento.Enabled = !Enabled;
            lkpPais.Enabled = !Enabled;
            dteFechaNacimiento.Enabled = !Enabled;
            txtApellidoPat.Enabled = !Enabled;
            txtApellidoMat.Enabled = !Enabled;
            txtNombres.Enabled = !Enabled;
            lkpSexo.Enabled = !Enabled;
            lkpNacionalidad.Enabled = !Enabled;
            lkpCodLDN.Enabled = !Enabled;
            txtNumTelf.Enabled = !Enabled;
            txtCorreo.Enabled = !Enabled;
            lkpTipVia.Enabled = !Enabled;
            txtNomVia.Enabled = !Enabled;
            txtNum.Enabled = !Enabled;
            txtDpto.Enabled = !Enabled;
            txtInt.Enabled = !Enabled;
            txtMz.Enabled = !Enabled;
            txtLt.Enabled = !Enabled;
            txtKm.Enabled = !Enabled;
            txtBlock.Enabled = !Enabled;
            txtEtapa.Enabled = !Enabled;
            lkpTipZona.Enabled = !Enabled;
            txtNomZona.Enabled = !Enabled;
            txtReferen.Enabled = !Enabled;
            lkpUBIGEO.Enabled = !Enabled;
            txtNomUBIGEO.Enabled = !Enabled;
            lkpEstadoCivil.Enabled = !Enabled;


            lkpCargo.Enabled = !Enabled;
            lkpArea.Enabled = !Enabled;
            txtRUC.Enabled = !Enabled;
            txtNumSeguro.Enabled = !Enabled;
            lkpTipFdoPension.Enabled = !Enabled;
            dteFechInicio.Enabled = !Enabled;
            lkpAFP.Enabled = !Enabled;
            lkpTipComision.Enabled = !Enabled;
            txtCUSPP.Enabled = !Enabled;
            chkEPS.Enabled = !Enabled;
            lkpTipPersonal.Enabled = !Enabled;
            txtMontBasico.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;
            chkRta5ta.Enabled = !Enabled;
            txtMontAntAfecto.Enabled = !Enabled;
            txtMotRetenido.Enabled = !Enabled;
            chkAsigFamiliar.Enabled = !Enabled;
            txtRetenc_Judicial.Enabled = !Enabled;
            txtAsig_Transporte.Enabled = !Enabled;
            //dteFechCese.Enabled = !Enabled;
            mnu.Enabled = !Enabled;


            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
                lkpSituacion.Enabled = false;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = false;
        }



        public void setValues()
        {
            if (Obe.perc_sfecha_cese == null || Obe.perc_sfecha_cese == Convert.ToDateTime("01/01/0001"))
            {
                dteFechaCese.Text = "";
            }
            else
            {
                dteFechaCese.DateTime = Convert.ToDateTime(Obe.perc_sfecha_cese);
            }
            txtCodigo.Text = Obe.perc_iid_personal;
            dteFechaRegistro.EditValue = Obe.perc_sfecha_registro;

            txtDocumento.Text = Obe.perc_vnum_doc;

            dteFechaNacimiento.DateTime = Convert.ToDateTime(Obe.perc_sfecha_nacimiento);
            txtApellidoPat.Text = Obe.perc_vapellido_pat;
            txtApellidoMat.Text = Obe.perc_vapellido_mat;
            txtNombres.Text = Obe.perc_vnombres;
            txtNumTelf.Text = Obe.perc_vnum_telf_ldn;
            txtCorreo.Text = Obe.perc_vcorreo;
            txtNomVia.Text = Obe.perc_vnomb_via;
            txtNum.Text = Obe.perc_vnum_via;
            txtDpto.Text = Obe.perc_vdepartamento;
            txtInt.Text = Obe.perc_vinterior;
            txtMz.Text = Obe.perc_vmanzana;
            txtLt.Text = Obe.perc_vlote;
            txtKm.Text = Obe.perc_vkilometro;
            txtBlock.Text = Obe.perc_vblock;
            txtEtapa.Text = Obe.perc_vetapa;
            txtNomZona.Text = Obe.perc_vnomb_zona;
            txtReferen.Text = Obe.perc_vreferencia;

            txtRUC.Text = Obe.perc_vruc;
            txtNumSeguro.Text = Obe.perc_vnum_seguro;
            dteFechInicio.DateTime = Convert.ToDateTime(Obe.perc_sfech_inicio);
            txtCUSPP.Text = Obe.perc_vcuspp;
            chkEPS.Checked = Convert.ToBoolean(Obe.perc_beps);
            txtMontBasico.Text = Convert.ToString(Obe.perc_nmont_basico);

            chkRta5ta.Checked = Convert.ToBoolean(Obe.perc_brta_5ta);
            txtMontAntAfecto.Text = Convert.ToString(Obe.perc_nmont_ant_afecto);
            txtMotRetenido.Text = Convert.ToString(Obe.perc_nmont_retenido);
            chkAsigFamiliar.Checked = Convert.ToBoolean(Obe.perc_basig_familiar);
            txtRetenc_Judicial.Text = Convert.ToString(Obe.perc_retenc_judicial);
            txtAsig_Transporte.Text = Convert.ToString(Obe.perc_nasig_transporte);
            lstPersonalCC = new BPlanillas().listarPersonalCCostos(Obe.perc_icod_personal);
            grdAlmacen.DataSource = lstPersonalCC;
            cargar();
            txthoraLV.Text = Obe.perc_hora_inical_LV;
            txthoraLVf.Text = Obe.perc_hora_final_LV;
            txthoraSi.Text = Obe.perc_hora_inical_S;
            txthoraSf.Text = Obe.perc_hora_final_S;
            txthoraTotalLV.Text = Obe.perc_hora_total_LV;
            txthoratotalS.Text = Obe.perc_hora_total_S;
            txtBancariaHaberes.Text = Obe.perc_vbanc_haber;
            txtbancariaCTS.Text = Obe.perc_vbanc_cts;
            viewAlmacen.RefreshData();
        }

        void cargar()
        {
            lstPersonalContrato = new BPlanillas().listarPersonal_contratacion(Obe.perc_icod_personal);
            lstArchivos = new BPlanillas().listarArchivos(Obe.perc_icod_personal);
            grdcontatacion.DataSource = lstPersonalContrato;
        }
        private void bteAnalitica_ButtonClick(object sender, ButtonPressedEventArgs e) => listarAnalitica();
        public void SetInsert() => Status = BSMaintenanceStatus.CreateNew;

        public void SetCancel() => Status = BSMaintenanceStatus.View;

        public void SetModify() => Status = BSMaintenanceStatus.ModifyCurrent;
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (txtCodigo.Text == "")
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                /*----------------------*/
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de personal");
                }
                /*----------------------*/
                if (verificarDocumento(txtDocumento.Text))
                {
                    oBase = txtDocumento;
                    throw new ArgumentException("El Documento ingresado ya existe en los registros de personal");
                }
                /*----------------------*/
                if (String.IsNullOrEmpty(txtDocumento.Text))
                {
                    oBase = txtDocumento;
                    throw new ArgumentException("Ingrese el Documento");
                }

                /*----------------------*/
                if (dteFechaNacimiento.DateTime == null || dteFechaNacimiento.Text == "")
                {
                    oBase = dteFechaNacimiento;
                    throw new ArgumentException("Ingrese Fecha de Nacimiento ");
                }

                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtApellidoPat.Text))
                {
                    oBase = txtApellidoPat;
                    throw new ArgumentException("Ingrese Apellido Paterno");
                }
                /*----------------------*/

                if (String.IsNullOrWhiteSpace(txtApellidoMat.Text))
                {
                    oBase = txtApellidoMat;
                    throw new ArgumentException("Ingrese Apellido Materno");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtNombres.Text))
                {
                    oBase = txtNombres;
                    throw new ArgumentException("Ingrese Nombre(s)");
                }

                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtNomVia.Text))
                {
                    oBase = txtNomVia;
                    throw new ArgumentException("Ingrese Nombre de la Vía");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtNomZona.Text))
                {
                    oBase = txtNomZona;
                    throw new ArgumentException("Ingrese Nombre de la Zona");
                }
                /*----------------------*/
                if (lkpArea.EditValue == null)
                {
                    oBase = lkpArea;
                    throw new ArgumentException("Ingrese Area del Personal");
                }
                /*----------------------*/
                if (lkpCargo.EditValue == null)
                {
                    oBase = lkpCargo;
                    throw new ArgumentException("Ingrese Cargo del Personal");
                }
                /*----------------------*/
                if (lkpTipFdoPension.EditValue == null)
                {
                    oBase = lkpTipFdoPension;
                    throw new ArgumentException("Ingrese Tipo de Fondo de Pesión");
                }

                /*----------------------*/
                if (dteFechInicio.DateTime == null || dteFechInicio.Text == "")
                {
                    oBase = dteFechInicio;
                    throw new ArgumentException("Ingrese Fecha de Inicio ");
                }

                /*----------------------*/
                if (lkpTipPersonal.EditValue == null)
                {
                    oBase = lkpTipPersonal;
                    throw new ArgumentException("Ingrese Tipo de Personal");
                }
                /*----------------------*/
                if (lkpMoneda.EditValue == null)
                {
                    oBase = lkpMoneda;
                    throw new ArgumentException("Ingrese Tipo de Fondo de Pesión");
                }
                /*adicionar*/




                if (txthoraLV.EditValue != null)
                {
                    if (txthoraLVf.EditValue == null || txthoraLVf.Text == "__:__ __")
                    {
                        oBase = txthoraLVf;
                        throw new ArgumentException("Ingrese la hora de salida de H-Lunes - Viernes");
                    }

                }
                if (txthoraSi.EditValue != null)
                {
                    if (txthoraSf.EditValue == null || txthoraSf.Text == "__:__ __")
                    {
                        oBase = txthoraSf;
                        throw new ArgumentException("Ingrese la hora de salida de H-Sabado");
                    }

                }

                if (txthoraLV.Text.Trim() == "__:__ __" && txthoraLVf.Text.Trim() == "__:__ __" && (txthoraSi.Text.Trim() == "__:__ __" && txthoraSf.Text.Trim() == "__:__ __"))
                {
                    if (XtraMessageBox.Show("¿Las horas de trabajo esta vacios, Desea Continuar ?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                }



                Obe.perc_iid_personal = txtCodigo.Text;
                Obe.perc_sfecha_registro = Convert.ToDateTime(dteFechaRegistro.EditValue);
                Obe.perc_iid_situacion_perso = Convert.ToInt32(lkpSituacion.EditValue);
                //----------------------------
                Obe.tbpd_icod_tip_doc = Convert.ToInt32(lkpTipDoc.EditValue);
                Obe.perc_vnum_doc = txtDocumento.Text;
                Obe.tbpd_icod_pais_emi_doc = Convert.ToInt32(lkpPais.EditValue);
                Obe.perc_sfecha_nacimiento = dteFechaNacimiento.DateTime;
                Obe.perc_vapellido_pat = txtApellidoPat.Text;
                Obe.perc_vapellido_mat = txtApellidoMat.Text;
                Obe.perc_vnombres = txtNombres.Text;
                Obe.perc_icod_sexo = Convert.ToInt32(lkpSexo.EditValue);
                Obe.tbpd_icod_nacionalidad = Convert.ToInt32(lkpNacionalidad.EditValue);
                Obe.tbpd_icod_telf_ldn = Convert.ToInt32(lkpCodLDN.EditValue);
                Obe.perc_vnum_telf_ldn = txtNumTelf.Text;
                Obe.perc_vcorreo = txtCorreo.Text;
                Obe.tbpd_icod_tip_via = Convert.ToInt32(lkpTipVia.EditValue);
                Obe.perc_vnomb_via = txtNomVia.Text;
                Obe.perc_vnum_via = txtNum.Text;
                Obe.perc_vdepartamento = txtDpto.Text;
                Obe.perc_vinterior = txtInt.Text;
                Obe.perc_vmanzana = txtMz.Text;
                Obe.perc_vlote = txtLt.Text;
                Obe.perc_vkilometro = txtKm.Text;
                Obe.perc_vblock = txtBlock.Text;
                Obe.perc_vetapa = txtEtapa.Text;
                Obe.tbpd_icod_tip_zona = Convert.ToInt32(lkpTipZona.EditValue);
                Obe.perc_vnomb_zona = txtNomZona.Text;
                Obe.perc_vreferencia = txtReferen.Text;
                Obe.tbpd_icod_ubi_geo = Convert.ToInt32(lkpUBIGEO.EditValue);
                Obe.tarec_icod_est_civil = Convert.ToInt32(lkpEstadoCivil.EditValue);//--modif
                //----------------------------
                Obe.tablc_iid_tipo_area = Convert.ToInt32(lkpArea.EditValue);
                Obe.tablc_iid_tipo_cargo = Convert.ToInt32(lkpCargo.EditValue);

                Obe.perc_vruc = txtRUC.Text;
                Obe.perc_vnum_seguro = txtNumSeguro.Text;
                Obe.perc_icod_tip_fdo_pension = Convert.ToInt32(lkpTipFdoPension.EditValue);
                Obe.perc_sfech_inicio = dteFechInicio.DateTime;
                Obe.perc_icod_afp = Convert.ToInt32(lkpAFP.EditValue);
                Obe.perc_icod_tip_comision = Convert.ToInt32(lkpTipComision.EditValue);
                Obe.perc_vcuspp = txtCUSPP.Text;
                Obe.perc_beps = chkEPS.Checked;
                Obe.perc_icod_tip_personal = Convert.ToInt32(lkpTipPersonal.EditValue);
                Obe.perc_nmont_basico = Convert.ToDecimal(txtMontBasico.Text);
                Obe.tarec_icod_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.perc_brta_5ta = chkRta5ta.Checked;
                Obe.perc_nmont_ant_afecto = Convert.ToDecimal(txtMontAntAfecto.Text);
                Obe.perc_nmont_retenido = Convert.ToDecimal(txtMotRetenido.Text);
                Obe.perc_basig_familiar = chkAsigFamiliar.Checked;

                //campos nuevo 
                Obe.perc_hora_inical_LV = txthoraLV.Text;
                Obe.perc_hora_final_LV = txthoraLVf.Text;

                Obe.perc_hora_inical_S = txthoraSi.Text;
                Obe.perc_hora_final_S = txthoraSf.Text;

                Obe.perc_hora_total_LV = txthoraTotalLV.Text;
                Obe.perc_hora_total_S = txthoratotalS.Text;


                Obe.perc_icod_tip_contrato = Convert.ToInt32(lkpTipoContrato.EditValue);
                Obe.perc_icod_banc_haber = Convert.ToInt32(lkpCBancariaHaberes.EditValue);
                Obe.perc_icod_banc_cts = Convert.ToInt32(lkpCBancariaCTS.EditValue);
                Obe.perc_vbanc_haber = txtBancariaHaberes.Text;
                Obe.perc_vbanc_cts = txtbancariaCTS.Text;
                Obe.perc_retenc_judicial = Convert.ToDecimal(txtRetenc_Judicial.Text);
                Obe.perc_nasig_transporte = Convert.ToDecimal(txtAsig_Transporte.Text);
                if (dteFechaCese.Text == "")
                {
                    Obe.perc_sfecha_cese = null;
                }
                else
                {
                    Obe.perc_sfecha_cese = dteFechaCese.DateTime;
                }

                if (lkpMotivoCese.Text == "")
                {
                    Obe.perc_icod_motiv_cese = null;
                }
                else
                {
                    Obe.perc_icod_motiv_cese = Convert.ToInt32(lkpMotivoCese.EditValue);
                }

                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {


                    Obe.perc_icod_personal = new BPlanillas().insertarPersonal(Obe, lstPersonalCC, lstPersonalContrato, lstArchivos);


                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    Obe.anac_icod_analitica = Convert.ToInt32(bteAnalitica.Tag);
                    new BPlanillas().modificarPersonal(Obe, lstPersonalCC, lstPersonalCCDelete, lstPersonalContrato, lstPersonalContratoDelete, lstArchivos);
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
                    this.MiEvento(Obe.perc_icod_personal);
                    this.Close();
                }
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.perc_iid_personal == strCodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.perc_iid_personal == strCodigo && x.perc_icod_personal != Obe.perc_icod_personal).ToList().Count > 0)
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

        private bool verificarDocumento(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.perc_vnum_doc == strCodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.perc_vnum_doc == strCodigo && x.perc_icod_personal != Obe.perc_icod_personal).ToList().Count > 0)
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
        private  async void frmMantePersonal_Load(object sender, EventArgs e)
        {

            txtNomUBIGEO.Enabled = false;
            lkpAFP.Enabled = false;
            lkpTipComision.Enabled = false;
            dteFechaRegistro.EditValue = DateTime.Now;
            await cargarCombos();


            //--------------------------------------------------------------------------------
            //DATO DE COMBO INICIALES
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lkpNacionalidad.EditValue = 223;
                lkpPais.EditValue = 4413;
                lkpMoneda.EditValue = 3;
                lkpMotivoCese.EditValue = null;
            }




            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpSituacion.EditValue = Obe.perc_iid_situacion_perso;
                lkpTipDoc.EditValue = Convert.ToInt32(Obe.tbpd_icod_tip_doc);
                lkpPais.EditValue = Convert.ToInt32(Obe.tbpd_icod_pais_emi_doc);
                lkpSexo.EditValue = Obe.perc_icod_sexo;
                lkpNacionalidad.EditValue = Convert.ToInt32(Obe.tbpd_icod_nacionalidad);
                lkpCodLDN.EditValue = Convert.ToInt32(Obe.tbpd_icod_telf_ldn);
                lkpTipVia.EditValue = Convert.ToInt32(Obe.tbpd_icod_tip_via);
                lkpTipZona.EditValue = Convert.ToInt32(Obe.tbpd_icod_tip_zona);
                lkpUBIGEO.EditValue = Convert.ToInt32(Obe.tbpd_icod_ubi_geo);
                lkpEstadoCivil.EditValue = Convert.ToInt32(Obe.tarec_icod_est_civil);//--modif
                //----------------------------
                lkpArea.EditValue = Obe.tablc_iid_tipo_area;
                lkpCargo.EditValue = Obe.tablc_iid_tipo_cargo;
                lkpTipFdoPension.EditValue = Obe.perc_icod_tip_fdo_pension;
                lkpAFP.EditValue = Obe.perc_icod_afp;
                lkpTipComision.EditValue = Obe.perc_icod_tip_comision;
                lkpTipPersonal.EditValue = Obe.perc_icod_tip_personal;
                lkpMoneda.EditValue = Obe.tarec_icod_moneda;
                lkpTipoContrato.EditValue = Obe.perc_icod_tip_contrato;
                lkpCBancariaHaberes.EditValue = Obe.perc_icod_banc_haber;
                lkpCBancariaCTS.EditValue = Obe.perc_icod_banc_cts;
                lkpMotivoCese.EditValue = Obe.perc_icod_motiv_cese;
            }


            if (Status == BSMaintenanceStatus.View)
            {
                lkpSituacion.EditValue = Obe.perc_iid_situacion_perso;
                lkpTipDoc.EditValue = Convert.ToInt32(Obe.tbpd_icod_tip_doc);
                lkpPais.EditValue = Convert.ToInt32(Obe.tbpd_icod_pais_emi_doc);
                lkpSexo.EditValue = Obe.perc_icod_sexo;
                lkpNacionalidad.EditValue = Convert.ToInt32(Obe.tbpd_icod_nacionalidad);
                lkpCodLDN.EditValue = Convert.ToInt32(Obe.tbpd_icod_telf_ldn);
                lkpTipVia.EditValue = Convert.ToInt32(Obe.tbpd_icod_tip_via);
                lkpTipZona.EditValue = Convert.ToInt32(Obe.tbpd_icod_tip_zona);
                lkpUBIGEO.EditValue = Convert.ToInt32(Obe.tbpd_icod_ubi_geo);
                lkpEstadoCivil.EditValue = Convert.ToInt32(Obe.tarec_icod_est_civil);
                lkpArea.EditValue = Obe.tablc_iid_tipo_area;
                lkpCargo.EditValue = Obe.tablc_iid_tipo_cargo;
                lkpTipFdoPension.EditValue = Obe.perc_icod_tip_fdo_pension;
                lkpAFP.EditValue = Obe.perc_icod_afp;
                lkpTipComision.EditValue = Obe.perc_icod_tip_comision;
                lkpTipPersonal.EditValue = Obe.perc_icod_tip_personal;
                lkpMoneda.EditValue = Obe.tarec_icod_moneda;
                lkpTipoContrato.EditValue = Obe.perc_icod_tip_contrato;
                lkpCBancariaHaberes.EditValue = Obe.perc_icod_banc_haber;
                lkpCBancariaCTS.EditValue = Obe.perc_icod_banc_cts;
                lkpMotivoCese.EditValue = Obe.perc_icod_motiv_cese;
            }

            cargar();
        }

        public async Task cargarCombos()
        {
            var datos = (await Task.WhenAll(obtenerDatosCombo()))[0];
            BSControls.LoaderLook(lkpTipDoc, datos.listaComboTabla.Where(x=>x.tbpc_icod_tabla_planilla ==8), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpPais, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 12), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpSexo, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 15), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpNacionalidad, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 9), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpCodLDN, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 13), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipVia, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 10), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipZona, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 11), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpUBIGEO, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 14), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipFdoPension, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 16), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipComision, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 17), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipPersonal, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 18), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipoContrato, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 19), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpCBancariaHaberes, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 20), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpCBancariaCTS, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 20), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpMotivoCese, datos.listaComboTabla.Where(x => x.tbpc_icod_tabla_planilla == 21), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpSituacion, datos.listaTablaRegistro.Where(x=>x.tablc_iid_tipo_tabla == Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpEstadoCivil, datos.listaTablaRegistro.Where(x => x.tablc_iid_tipo_tabla == 78), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, datos.listaTablaRegistro.Where(x => x.tablc_iid_tipo_tabla == 5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpArea, datos.listaArea, "arec_vdescripcion", "arec_icod_cargo", true);
            BSControls.LoaderLook(lkpCargo, datos.listaCargo, "carg_vdescripcion", "carg_icod_cargo", true);
            BSControls.LoaderLook(lkpAFP, datos.listaFondosPensiones, "fdpc_vdescripcion", "fdpc_icod_fondo_pension", true);

        }


        private async Task<(
            List<ETablaPlanillaDetalle> listaComboTabla,
            List<ETablaRegistro> listaTablaRegistro,
            List<EAreas> listaArea,
            List<ECargo> listaCargo,
            List<EFondosPensiones> listaFondosPensiones
        )> obtenerDatosCombo()
        {
            (List<ETablaPlanillaDetalle> listaComboTabla, List<ETablaRegistro> listaTablaRegistro, List<EAreas> listaArea, List<ECargo> listaCargo, List<EFondosPensiones> listaFondosPensiones) datos = default;
            datos.listaComboTabla = await Task.Run(() => new BPlanillas().listarComboTablaPlanillaDetalle(0));
            datos.listaTablaRegistro = await Task.Run(() => new BGeneral().listarTablaRegistro(0));
            datos.listaArea = await Task.Run(() => new BPlanillas().listarComboArea());
            datos.listaCargo = await Task.Run(() => new BPlanillas().listarComboCargo());
            datos.listaFondosPensiones = await Task.Run(() => new BPlanillas().listarFondosPensiones().Where(x => x.tablc_iid_tipo_fondo_pensiones == true).ToList());
            return datos;
        }


        private void lkpTipFdoPension_EditValueChanged(object sender, EventArgs e)
        {

            if (Status == BSMaintenanceStatus.CreateNew)
            {

                if (lkpTipFdoPension.Text == "AFP" || Convert.ToInt32(lkpTipFdoPension.EditValue) == 6384)
                {
                    lkpAFP.Enabled = true;
                    lkpTipComision.Enabled = true;
                }

                if (lkpTipFdoPension.Text == "ONP" || Convert.ToInt32(lkpTipFdoPension.EditValue) == 6385)
                {
                    lkpAFP.Enabled = false;
                    lkpTipComision.Enabled = false;
                    lkpAFP.EditValue = 0;
                    lkpTipComision.EditValue = 0;
                }
            }

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                if (lkpTipFdoPension.Text == "AFP" || Convert.ToInt32(lkpTipFdoPension.EditValue) == 6384)
                {
                    lkpAFP.Enabled = true;
                    lkpTipComision.Enabled = true;
                }
                if (lkpTipFdoPension.Text == "ONP" || Convert.ToInt32(lkpTipFdoPension.EditValue) == 6385)
                {
                    lkpAFP.Enabled = false;
                    lkpTipComision.Enabled = false;
                    lkpAFP.EditValue = 0;
                    lkpTipComision.EditValue = 0;
                }
            }


        }

        private void btnGuardar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => SetSave();
        private void btnCancelar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => Close();

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmRegistroPersonalCcosto frm = new frmRegistroPersonalCcosto())
                {
                    frm.perc_vnum_doc = txtCodigo.Text;
                    frm.lstPersonalCC = lstPersonalCC;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstPersonalCC = frm.lstPersonalCC;
                        grdAlmacen.DataSource = lstPersonalCC;
                        grdAlmacen.Refresh();
                        grdAlmacen.RefreshDataSource();
                        viewAlmacen.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void lkpUBIGEO_EditValueChanged(object sender, EventArgs e)
        {
            List<ETablaPlanillaDetalle> lstTablaDetalle = new List<ETablaPlanillaDetalle>();
            lstTablaDetalle = new BPlanillas().listarTablaPlanillaDetalle(14);
            List<ETablaPlanillaDetalle> aux = new List<ETablaPlanillaDetalle>();
            if (lkpUBIGEO.Text == "")
            {
                txtNomUBIGEO.Text = null;
                return;
            }

            aux = lstTablaDetalle.Where(x => x.tbpd_icod_tabla_planilla_detalle == Convert.ToInt32(lkpUBIGEO.EditValue)).ToList();


            if (aux.Count == 1)
            {
                txtNomUBIGEO.Text = aux[0].tbpd_iid_vcodigo_tabla_planilla_detalle;
            }




        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EPersonalCCostos obe = (EPersonalCCostos)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewAlmacen.FocusedRowHandle;
                    using (frmRegistroPersonalCcosto frm = new frmRegistroPersonalCcosto())
                    {
                        frm.perc_vnum_doc = txtCodigo.Text;
                        frm.Obe = obe;
                        frm.lstPersonalCC = lstPersonalCC;
                        frm.SetModify();
                        frm.setValues();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            lstPersonalCC = frm.lstPersonalCC;
                            viewAlmacen.RefreshData();
                            viewAlmacen.FocusedRowHandle = index;
                            viewAlmacen.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EPersonalCCostos obe = (EPersonalCCostos)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewAlmacen.FocusedRowHandle;

                    lstPersonalCCDelete.Add(obe);
                    lstPersonalCC.Remove(obe);
                    viewAlmacen.RefreshData();
                    viewAlmacen.FocusedRowHandle = index;
                    viewAlmacen.Focus();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txthoraLV_Leave(object sender, EventArgs e) { }
        private void txthoraLVf_Leave(object sender, EventArgs e) { }
        private void txthoraSi_Leave(object sender, EventArgs e) { }

        private void txthoraSf_Leave(object sender, EventArgs e) { }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (frmRegistroFechaContrato frm = new frmRegistroFechaContrato())
            {
                frm.lstDatosContrato = lstPersonalContrato;
                frm.lstArchivos = lstArchivos;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstPersonalContrato = frm.lstDatosContrato;
                    lstArchivos = frm.lstArchivos;
                    griddetalleContrato.RefreshData();
                    griddetalleContrato.MoveLast();
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) { }


        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            EPersonal obe = (EPersonal)griddetalleContrato.GetRow(griddetalleContrato.FocusedRowHandle);
            if (obe != null)
            {
                int index = griddetalleContrato.FocusedRowHandle;
                using (frmRegistroFechaContrato frm = new frmRegistroFechaContrato())
                {

                    frm.obe = obe;
                    frm.lstDatosContrato = lstPersonalContrato;
                    frm.lstArchivos = lstArchivos;
                    frm.perc_icod_personal = obe.perc_icod_personal;
                    frm.SetModify();
                    frm.setValues();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstPersonalContrato = frm.lstDatosContrato;
                        lstArchivos = frm.lstArchivos;
                        griddetalleContrato.RefreshData();
                        griddetalleContrato.FocusedRowHandle = index;
                        griddetalleContrato.Focus();
                    }
                }
            }

        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                EPersonal obe = (EPersonal)griddetalleContrato.GetRow(griddetalleContrato.FocusedRowHandle);
                if (obe != null)
                {
                    int index = griddetalleContrato.FocusedRowHandle;

                    lstPersonalContratoDelete.Add(obe);
                    lstPersonalContrato.Remove(obe);
                    griddetalleContrato.RefreshData();
                    griddetalleContrato.FocusedRowHandle = index;
                    griddetalleContrato.Focus();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fechaDeActivadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmRegistrofechaActividad frm = new frmRegistrofechaActividad())
                {

                    frm.lstDatosActividad = lstPersonalContrato;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstPersonalContrato = frm.lstDatosActividad;
                        griddetalleContrato.RefreshData();
                        griddetalleContrato.MoveLast();


                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fechaContratoToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void lkpTipoContrato_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoContrato.EditValue) == 6391)
            {
                dteFechaCese.Enabled = true;
                lkpMotivoCese.Enabled = true;
            }
            else
            {
                dteFechaCese.Enabled = false;
                lkpMotivoCese.Enabled = false;
            }
        }

        private void listarAnalitica()
        {
            using (frmListarAnaliticaDetalle frm = new frmListarAnaliticaDetalle())
            {
                frm.intTipoAnalitica = 4;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAnalitica.Tag = frm._Be.anad_icod_analitica;
                    bteAnalitica.Text = frm._Be.anad_iid_analitica;

                }
            }
        }
    }
}