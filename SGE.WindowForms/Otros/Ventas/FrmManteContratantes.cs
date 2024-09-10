using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteContratantes : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public int cntc_icod_contrato;
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContratante obj = new EContratante();
        public FrmManteContratantes()
        {
            InitializeComponent();
        }

        private void FrmManteContratantes_Load(object sender, EventArgs e)
        {
            cargarControles();
        }

        void cargarControles()
        {
            BSControls.LoaderLook(lkpNacionalidad, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLook(lkpTipoDoc, new BGeneral().listarTablaRegistro(96), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            dtFechaNac.EditValue = DateTime.Today;
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

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }


            if (Status == BSMaintenanceStatus.CreateNew)
            {

            }



        }

        public void setvalues()
        {
            obj.cntcc_sfecha_nacimineto_contratante = Convert.ToDateTime(obj.cntcc_sfecha_nacimineto_contratante).Year == 1 ? (DateTime?)null : obj.cntcc_sfecha_nacimineto_contratante;
            txtid.Text = obj.cntcc_iid_contratante.ToString();
            txtNombre.Text = obj.cntcc_vnombre_contratante;
            txtApellidoP.Text = obj.cntcc_vapellido_paterno_contratante;
            txtApellidoM.Text = obj.cntcc_vapellido_materno_contratante;
            txtCorreo.Text = obj.cntcc_vdireccion_correo_contratante;
            txtDireccion.Text = obj.cntcc_vdireccion_contratante;
            txtNumDoc.Text = obj.cntcc_vdni_contratante;
            lkpTipoDoc.EditValue = obj.cntcc_itipo_documento_contratante;
            txtRuc.Text = obj.cntcc_vruc_contratante;
            dtFechaNac.EditValue = obj.cntcc_sfecha_nacimineto_contratante;
            txtTelefono.Text = obj.cntcc_vtelefono_contratante;
            lkpNacionalidad.EditValue = obj.cntcc_inacionalidad_contratante;
            chckSelec.Checked = obj.cntcc_bflag_seleccion;

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
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {

                if (String.IsNullOrEmpty(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("Ingrese Nombre del Contratante");
                }
                if (String.IsNullOrEmpty(txtApellidoP.Text))
                {
                    oBase = txtApellidoP;
                    throw new ArgumentException("Ingrese Apellido Paterno del Contratante");
                }

                obj.cntc_icod_contrato = cntc_icod_contrato;
                obj.cntcc_iid_contratante = Convert.ToInt32(txtid.Text);
                obj.cntcc_vnombre_contratante = txtNombre.Text;
                obj.cntcc_vapellido_paterno_contratante = txtApellidoP.Text;
                obj.cntcc_vapellido_materno_contratante = txtApellidoM.Text;
                obj.cntcc_vdni_contratante = txtNumDoc.Text;
                obj.cntcc_vruc_contratante = txtRuc.Text;
                obj.cntcc_sfecha_nacimineto_contratante = Convert.ToDateTime(dtFechaNac.EditValue);
                obj.cntcc_sfecha_nacimineto_contratante = Convert.ToDateTime(obj.cntcc_sfecha_nacimineto_contratante).Year == 1 ? (DateTime?)null : obj.cntcc_sfecha_nacimineto_contratante;
                obj.cntcc_vtelefono_contratante = txtTelefono.Text;
                obj.cntcc_vdireccion_correo_contratante = txtCorreo.Text;
                obj.cntcc_vdireccion_contratante = txtDireccion.Text;
                obj.cntcc_inacionalidad_contratante = Convert.ToInt32(lkpNacionalidad.EditValue);
                obj.cntcc_itipo_documento_contratante = Convert.ToInt32(lkpTipoDoc.EditValue);
                obj.cntcc_iusuario_crea = Valores.intUsuario;
                obj.cntcc_sfecha_crea = DateTime.Today;
                obj.cntcc_vpc_crea = WindowsIdentity.GetCurrent().Name;
                obj.cntcc_iusuario_modifica = Valores.intUsuario;
                obj.cntcc_sfecha_modifica = DateTime.Today;
                obj.cntcc_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                obj.cntcc_bflag_estado = true;
                obj.cntcc_bflag_seleccion = chckSelec.Checked;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.cntcc_icod_contratante = new BVentas().insertarContratante(obj);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarContratatante(obj);

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
                    this.MiEvento(obj.cntcc_icod_contratante);
                    this.Close();
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
    }
}