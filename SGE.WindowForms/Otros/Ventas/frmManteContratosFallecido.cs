using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteContratosFallecido : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EContratoFallecido Obe = new EContratoFallecido();
        public List<EContratoFallecido> lstDetalle = new List<EContratoFallecido>();
        public int icodEspacio;
        public int icodContrato;
        
        public frmManteContratosFallecido()
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
            txtNombreFallecido.Text = Obe.cntc_vnombre_fallecido;
            txtApellidoPFallecido.Text = Obe.cntc_vapellido_paterno_fallecido;
            txtApellidoMFallecido.Text = Obe.cntc_vapellido_materno_fallecido;
            txtDOCFallecimiento.Text = Obe.cntc_vdni_fallecido;
            dtFechaNacFallecido.EditValue = Obe.cntc_sfecha_nac_fallecido;
            lkpNacionalidad1.EditValue = Obe.cntc_inacionalidad;
            dtFechaFallecimiento.EditValue = Obe.cntc_sfecha_fallecimiento;
            dtFechaEntierro.EditValue = Obe.cntc_sfecha_entierro;
            lkpTipoDocFallecido.EditValue = Obe.cntc_itipo_documento_fallecido;
            txtDOCFallecimiento.Text = Obe.cntc_vdocumento_fallecido;
            txtDomicilioFallecido.Text = Obe.cntc_vdireccion_fallecido;
            lkpRelogiones.EditValue = Obe.cntc_icod_religiones;
            lkpTipoDeceso.EditValue = Obe.cntc_icod_tipo_deceso;
            txtObservaciones.Text = Obe.cntc_vobservacion;
            btnNiveles.Tag = Obe.cntc_icod_indicador_espacios;
            btnNiveles.Text = Obe.espad_vnivel;
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
                Obe.cntc_icod_contrato = icodContrato;
                Obe.cntc_vnombre_fallecido = txtNombreFallecido.Text;
                Obe.cntc_vapellido_paterno_fallecido = txtApellidoPFallecido.Text;
                Obe.cntc_vapellido_materno_fallecido = txtApellidoMFallecido.Text;
                Obe.cntc_vdni_fallecido = txtDOCFallecimiento.Text;
                if (dtFechaNacFallecido.DateTime == null || dtFechaNacFallecido.Text == "" || dtFechaNacFallecido.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_nac_fallecido = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_nac_fallecido = Convert.ToDateTime(dtFechaNacFallecido.EditValue);
                }
                Obe.cntc_inacionalidad = Convert.ToInt32(lkpNacionalidad1.EditValue);
                if (dtFechaFallecimiento.DateTime == null || dtFechaFallecimiento.Text == "" || dtFechaFallecimiento.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_fallecimiento = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_fallecimiento = Convert.ToDateTime(dtFechaFallecimiento.EditValue);
                }
                if (dtFechaEntierro.DateTime == null || dtFechaEntierro.Text == "" || dtFechaEntierro.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_entierro = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_entierro = Convert.ToDateTime(dtFechaEntierro.EditValue);
                }
                Obe.cntc_itipo_documento_fallecido = Convert.ToInt32(lkpTipoDocFallecido.EditValue);
                Obe.cntc_vdocumento_fallecido = txtDOCFallecimiento.Text;
                Obe.cntc_icod_religiones = Convert.ToInt32(lkpRelogiones.EditValue);
                Obe.cntc_icod_tipo_deceso = Convert.ToInt32(lkpTipoDeceso.EditValue);
                Obe.cntc_vobservacion = txtObservaciones.Text;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.cntc_icod_indicador_espacios = Convert.ToInt32(btnNiveles.Tag);
                Obe.cntc_vdireccion_fallecido = txtDomicilioFallecido.Text;
                /**/

                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    new BVentas().insertarContratoFallecido(Obe);

                    List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(icodEspacio).Where(x => x.espad_iid_iespacios == Obe.cntc_icod_indicador_espacios).ToList();
                    lstEspacioDet.ForEach(x =>
                    {
                        x.espad_vnom_fallecido = Obe.cntc_vnombre_fallecido;
                        x.espad_vapellido_paterno_fallecido = Obe.cntc_vapellido_paterno_fallecido;
                        x.espad_vapellido_materno_fallecido = Obe.cntc_vapellido_materno_fallecido;
                        x.espad_vdni_fallecido = Obe.cntc_vdni_fallecido;
                        x.espad_sfecha_nac_fallecido = Obe.cntc_sfecha_nac_fallecido;
                        x.espad_sfecha_fallecido = Obe.cntc_sfecha_fallecimiento;
                        x.espad_sfecha_entierro = Obe.cntc_sfecha_entierro;
                        x.espad_inacionalidad = Obe.cntc_inacionalidad;
                        x.espad_icod_iestado = 16;
                        new BVentas().modificarEspaciosDetConsultas(x);
                    });

                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarContratoFallecido(Obe);
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

            BSControls.LoaderLook(lkpNacionalidad1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDocFallecido, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpRelogiones, new BGeneral().listarTablaVentaDet(19), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoDeceso, new BGeneral().listarTablaVentaDet(17), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipoDocFallecido.EditValue = Obe.cntc_itipo_documento_fallecido;
                lkpNacionalidad1.EditValue = Obe.cntc_inacionalidad;
                lkpRelogiones.EditValue = Obe.cntc_icod_religiones;
                lkpTipoDeceso.EditValue = Obe.cntc_icod_tipo_deceso;
            }

        }

        private void btnNiveles_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarNiveles frm = new FrmListarNiveles())
                {
                    frm.icodcontrato = icodContrato;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnNiveles.Tag = frm._Be.espad_iid_iespacios;
                        btnNiveles.Text = frm._Be.espad_vnivel;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpOrigenVenta_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpTipoDeceso_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoDeceso.EditValue) == 3401)
            {
                txtObservaciones.Enabled = true;
            }
            else
            {
                txtObservaciones.Text = "";
                txtObservaciones.Enabled = false;
            }
        }
    }
}