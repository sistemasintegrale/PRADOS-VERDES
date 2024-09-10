using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;
using System.Linq;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteEspaciosDetConsulta : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EEspaciosDet oBe = new EEspaciosDet();
        public List<EEspaciosDet> lstDetalle = new List<EEspaciosDet>();
        
        public frmManteEspaciosDetConsulta()
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
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    bteNroDocumento.Enabled = Enabled;
        }


        public void setValues()
        {
            txtNivel.Text = oBe.espad_vnivel;
            lkpSituacion.EditValue = oBe.espad_icod_isituacion;
            lkpEstado.EditValue = oBe.espad_icod_iestado;
            txtNomApe.Text = oBe.espad_vnom_fallecido;
            txtApellidoPFallecido.Text = oBe.espad_vapellido_paterno_fallecido;
            txtApellidoMFallecido.Text = oBe.espad_vapellido_materno_fallecido;
            txtDNIFallecimiento.Text = oBe.espad_vdni_fallecido;
            dtFechaNacFallecido.EditValue = oBe.espad_sfecha_nac_fallecido;
            dteFechaFallecmiento.EditValue = oBe.espad_sfecha_fallecido;
            dtFechaEntierro.EditValue = oBe.espad_sfecha_entierro;
            lkpNacionalidad1.EditValue = oBe.espad_inacionalidad;
            txtHora.Text = oBe.espad_thora;
            txtSolicitante.Text = oBe.espad_vsolicitante;
            txtNroDocumento.Text = oBe.espad_vnro_doc;
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
            setValues();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try 
            {
                if (txtNomApe.Text != "")
                {
                    if (Convert.ToDateTime(dteFechaFallecmiento.EditValue) == null)
                    {
                        oBase = dteFechaFallecmiento;
                        throw new ArgumentException("Ingrese Fecha");
                    }
                }
                else
                {
                    dteFechaFallecmiento.EditValue = "";
                }





                oBe.espad_vnivel = txtNivel.Text;
                oBe.espad_icod_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                if (txtNomApe.Text != "")
                {
                    oBe.espad_icod_iestado = 16;
                }
                else
                {
                    oBe.espad_icod_iestado = 15;
                }


                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                /**/
                oBe.strsituacion = lkpSituacion.Text;
                oBe.strestado = lkpEstado.Text;

                oBe.espad_vnom_fallecido = txtNomApe.Text;
                oBe.espad_vapellido_paterno_fallecido = txtApellidoPFallecido.Text;
                oBe.espad_vapellido_materno_fallecido = txtApellidoMFallecido.Text;
                oBe.espad_vdni_fallecido = txtDNIFallecimiento.Text;
                if (dtFechaNacFallecido.DateTime == null || dtFechaNacFallecido.Text == "" || dtFechaNacFallecido.Text == "01/01/0001")
                {
                    oBe.espad_sfecha_nac_fallecido = (DateTime?)null;
                }
                else
                {
                    oBe.espad_sfecha_nac_fallecido = Convert.ToDateTime(dtFechaNacFallecido.EditValue);
                }
                if (dteFechaFallecmiento.DateTime == null || dteFechaFallecmiento.Text == "" || dteFechaFallecmiento.Text == "01/01/0001")
                {
                    oBe.espad_sfecha_fallecido = (DateTime?)null;
                }
                else
                {
                    oBe.espad_sfecha_fallecido = dteFechaFallecmiento.DateTime;
                }
                if (dtFechaEntierro.DateTime == null || dtFechaEntierro.Text == "" || dtFechaEntierro.Text == "01/01/0001")
                {
                    oBe.espad_sfecha_entierro = (DateTime?)null;
                }
                else
                {
                    oBe.espad_sfecha_entierro = Convert.ToDateTime(dtFechaEntierro.EditValue);
                }
                oBe.espad_inacionalidad = Convert.ToInt32(lkpNacionalidad1.EditValue);
                if (txtHora.Text == "" || txtHora.Text == "000:00")
                {

                    oBe.espad_thora = null;
                }
                else
                {
                    oBe.espad_thora = (txtHora.Text);
                }
                oBe.espad_vsolicitante = txtSolicitante.Text;
                oBe.espad_vnro_doc = txtNroDocumento.Text;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    //oBe.intTipoOperacion = 1;
                    //lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    //if (oBe.intTipoOperacion != 1)
                    //    oBe.intTipoOperacion = 2;

                    new BVentas().modificarEspaciosDetConsultas(oBe);

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
                    this.DialogResult = DialogResult.OK;                
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave(); 
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePercepcionDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(10), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpEstado, new BGeneral().listarTablaVentaDet(11), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNacionalidad1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpEstado.EditValue = oBe.espad_icod_iestado;
                lkpSituacion.EditValue = oBe.espad_icod_isituacion;
                lkpNacionalidad1.EditValue = oBe.espad_inacionalidad;
            }
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    setValues();
        }        
    }
}