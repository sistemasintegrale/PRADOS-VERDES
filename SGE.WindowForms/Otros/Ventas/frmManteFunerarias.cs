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
    public partial class frmManteFunerarias : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteFunerarias));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EFunerarias Obe = new EFunerarias();
        public List<EFunerarias> lstFuneraria = new List<EFunerarias>();


        public frmManteFunerarias()
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
            //txtCodigo.Enabled = !Enabled;
            txtRazonSocial.Enabled = !Enabled;
            txtNombreComercial.Enabled = !Enabled;
            txtNumeroDocumento.Enabled = !Enabled;
            txtDireccion.Enabled = !Enabled;
            txtTelefono.Enabled = !Enabled;
            txtCorreo.Enabled = !Enabled;
            txtContacto.Enabled = !Enabled;
            lkpDistrito.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
            
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            

            //if (Status == BSMaintenanceStatus.CreateNew)
                //txtCodigo.Enabled = !Enabled;
            
           
        }
        public void setValues()
        
        {
            txtCodigo.Text = string.Format("{0:0000}", Obe.func_iid_funeraria);
            txtRazonSocial.Text = Obe.func_vrazon_social;
            txtNombreComercial.Text = Obe.func_vnombre_comercial;
            txtNumeroDocumento.Text = Obe.func_cnumero_docum_fun;
            txtDireccion.Text = Obe.func_vdireccion;
            txtTelefono.Text = Obe.func_vtelefonos;
            txtCorreo.Text = Obe.func_vcorreo;
            txtContacto.Text = Obe.func_vcontacto;
            lkpDistrito.EditValue = Obe.disc_icod_distrito;
            txtDNI.Text = Obe.func_vruc;
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
       
        private void cargar()
        {
            BSControls.LoaderLook(lkpDistrito, new BVentas().listarDistrito(), "disc_vdescripcion", "disc_icod_distrito", true);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            
            try
            {               
                /*----------------------*/
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código de la Funeraria");
                }
                //if (verificarCodigoFuneraria(txtCodigo.Text))
                //{
                //    oBase = txtCodigo;
                //    throw new ArgumentException("El código ingresado ya existe en los registros de las funerarias");
                //}

                /*----------------------*/

                Obe.func_iid_funeraria = Convert.ToInt32(txtCodigo.Text);
                Obe.func_vrazon_social = txtRazonSocial.Text;
                Obe.func_vnombre_comercial = txtNombreComercial.Text;
                Obe.func_cnumero_docum_fun = txtNumeroDocumento.Text;
                Obe.func_vruc = txtDNI.Text;
                Obe.func_vdireccion = txtDireccion.Text;
                Obe.func_vtelefonos = txtTelefono.Text;
                Obe.func_vcorreo = txtCorreo.Text;
                Obe.func_vcontacto = txtContacto.Text;
                Obe.disc_icod_distrito = Convert.ToInt32(lkpDistrito.EditValue);
                Obe.func_flag_estado = true;                
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.disc_icod_distrito = new BVentas().insertarFuneraria(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarFuneraria(Obe);
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
                    this.MiEvento(Obe.func_icod_funeraria);
                    this.Close();
                }
            }
        }   
        private bool verificarCodigoFuneraria(string strCodigo)
        {
            try 
            {
                bool exists = false;
                if (lstFuneraria.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstFuneraria.Where(x => x.func_iid_funeraria.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstFuneraria.Where(x => x.func_iid_funeraria.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.func_icod_funeraria != Obe.func_icod_funeraria).ToList().Count > 0)
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

        private void frmManteFuneraria_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}