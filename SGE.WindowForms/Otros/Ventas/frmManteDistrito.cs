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



namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteDistrito : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteDistrito));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EDistritos Obe = new EDistritos();
        public List<EDistritos> lstDistritos = new List<EDistritos>();


        public frmManteDistrito()
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
            txtDescripcion.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
            
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            

            if (Status == BSMaintenanceStatus.CreateNew)
                txtCodigo.Enabled = !Enabled;
            
           
        }
        public void setValues()
        
        {
            txtCodigo.Text = string.Format("{0:00}", Obe.disc_iid_distrito);
            txtDescripcion.Text = Obe.disc_vdescripcion;
            
         
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
                /*----------------------*/
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código del Distrito");
                }
                if (verificarCodigoZona(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de distritos");
                }
                /*----------------------*/
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese descripción del Distrito");
                }
                if (verificarDescripcionZona(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La descripción ingresada ya existe en los registros de Distritos");
                }

                int? intNullVal = null;

                //Obe.famic_iid_familia = (lstFamiliaCab.Count + 1);
                Obe.disc_iid_distrito = Convert.ToInt32(txtCodigo.Text);
                Obe.disc_vdescripcion = txtDescripcion.Text;
                Obe.disc_flag_estado = true;                
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.disc_icod_distrito = new BVentas().insertarDistrito(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarDistrito(Obe);
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
                    this.MiEvento(Obe.disc_icod_distrito);
                    this.Close();
                }
            }
        }   
        private bool verificarCodigoZona(string strCodigo)
        {
            try 
            {
                bool exists = false;
                if (lstDistritos.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstDistritos.Where(x => x.disc_iid_distrito.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstDistritos.Where(x => x.disc_iid_distrito.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.disc_icod_distrito != Obe.disc_icod_distrito).ToList().Count > 0)
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
      
        private bool verificarDescripcionZona(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstDistritos.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstDistritos.Where(x => x.disc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstDistritos.Where(x => x.disc_vdescripcion.Trim() == strNombre.Trim() && x.disc_icod_distrito!= Obe.disc_icod_distrito).ToList().Count > 0)
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
            //BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(63), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            //BSControls.LoaderLook(lkpAlmContable, new BContabilidad().listarAlmacenContable(), "almcc_vdescripcion", "almcc_icod_almacen", true);
        }

        //private void bteClasificacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    listarClasificacion();
        //}

        //private void listarClasificacion()
        //{
        //    using (frmListarClasificacionProducto frm = new frmListarClasificacionProducto())
        //    {
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            bteClasificacion.Tag = frm._Be.clasc_icod_clasificacion;
        //            bteClasificacion.Text = frm._Be.clasc_vdescripcion;
        //        }
        //    }
        //}
        
    }
}