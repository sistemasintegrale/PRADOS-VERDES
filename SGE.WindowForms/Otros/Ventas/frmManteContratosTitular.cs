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
    public partial class frmManteContratosTitular : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EContratoTitular1 Obe = new EContratoTitular1();
        public List<EContratoTitular1> lstDetalle = new List<EContratoTitular1>();
        public int icodEspacio;
        public int icodContrato;
        
        public frmManteContratosTitular()
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
            txtNumero.Text = Obe.cntc_inumero;
            txtNombreContratanteTitular1.Text = Obe.cntc_vnombre_contratante;
            txtApellidoPContratanteTitular1.Text = Obe.cntc_vapellido_paterno_contratante;
            txtApellidoMContratanteTitular1.Text = Obe.cntc_vapellido_materno_contratante;
            //txtDOCContratante.Text = Obe.cntc_vdni_contratante;
            txtRucContratanteTitular1.Text = Obe.cntc_vruc_contratante;
            dtFechaNacContratanteTitular1.EditValue = Obe.cntc_sfecha_nacimineto_contratante;
            txtTelContratanteTitular1.Text = Obe.cntc_vtelefono_contratante;
            txtTelContratante2Titular1.Text = Obe.cntc_vtelefono_contratante2;
            txtCorreoTitular1.Text = Obe.cntc_vdireccion_correo_contratante;
            txtDireccionContratanteTitular1.Text = Obe.cntc_vdireccion_contratante;
            lkpNacionalidadContratanteTitular1.EditValue = Obe.cntc_inacionalidad_contratante;
            lkpTipoDocContratanteTitular1.EditValue = Obe.cntc_itipo_documento_contratante;
            txtDOCContratanteTitular1.Text = Obe.cntc_vdocumento_contratante;
            chkCompromisoPago.Checked = Obe.cntc_flag_compromiso_pago;
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
                Obe.cntc_inumero = txtNumero.Text;
                Obe.cntc_vnombre_contratante = txtNombreContratanteTitular1.Text;
                Obe.cntc_vapellido_paterno_contratante = txtApellidoPContratanteTitular1.Text;
                Obe.cntc_vapellido_materno_contratante = txtApellidoMContratanteTitular1.Text;
                Obe.cntc_vruc_contratante = txtRucContratanteTitular1.Text;
                if (dtFechaNacContratanteTitular1.DateTime == null || dtFechaNacContratanteTitular1.Text == "" || dtFechaNacContratanteTitular1.Text == "01/01/0001")
                {
                    Obe.cntc_sfecha_nacimineto_contratante = (DateTime?)null;
                }
                else
                {
                    Obe.cntc_sfecha_nacimineto_contratante = Convert.ToDateTime(dtFechaNacContratanteTitular1.EditValue);
                }
                Obe.cntc_vtelefono_contratante = txtTelContratanteTitular1.Text;
                Obe.cntc_vtelefono_contratante2 = txtTelContratante2Titular1.Text;
                Obe.cntc_vdireccion_correo_contratante = txtCorreoTitular1.Text;
                Obe.cntc_vdireccion_contratante = txtDireccionContratanteTitular1.Text;
                Obe.cntc_inacionalidad_contratante = Convert.ToInt32(lkpNacionalidadContratanteTitular1.EditValue);
                Obe.cntc_vnacionalidad_cotratante = lkpNacionalidadContratanteTitular1.Text;
                Obe.cntc_itipo_documento_contratante = Convert.ToInt32(lkpTipoDocContratanteTitular1.EditValue);
                Obe.cntc_vdocumento_contratante = txtDOCContratanteTitular1.Text;
                Obe.cntc_flag_compromiso_pago = chkCompromisoPago.Checked;
                /**/

                if (Status == BSMaintenanceStatus.CreateNew)
                {

                    new BVentas().insertarContratoTitular1(Obe);


                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarContratoTitular1(Obe);
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

            BSControls.LoaderLook(lkpNacionalidadContratanteTitular1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipoDocContratanteTitular1, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpNacionalidadContratanteTitular1.EditValue = Obe.cntc_inacionalidad_contratante;
                lkpTipoDocContratanteTitular1.EditValue = Obe.cntc_itipo_documento_contratante;
            }

        }

    }
}