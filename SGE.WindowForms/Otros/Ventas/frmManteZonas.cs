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
    public partial class frmManteZonas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteZonas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EZona Obe = new EZona();
        public List<EZona> lstZona = new List<EZona>();


        public frmManteZonas()
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
            txtCodigo.Text = string.Format("{0:00}", Obe.zonc_iid_zona);
            txtDescripcion.Text = Obe.zonc_vdescripcion;
            
         
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
                    throw new ArgumentException("Ingrese código de la Zona");
                }
                if (verificarCodigoZona(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de las Zonas");
                }
                /*----------------------*/
                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese descripción de la Zona");
                }
                if (verificarDescripcionZona(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("La descripción ingresada ya existe en los registros de las Zonas");
                }

                int? intNullVal = null;

                //Obe.famic_iid_familia = (lstFamiliaCab.Count + 1);
                Obe.zonc_iid_zona = Convert.ToInt32(txtCodigo.Text);
                Obe.zonc_vdescripcion = txtDescripcion.Text;
                Obe.zonc_flag_estado = true;                
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.zonc_icod_zona = new BVentas().insertarZona(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarZona(Obe);
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
                    this.MiEvento(Obe.zonc_icod_zona);
                    this.Close();
                }
            }
        }   
        private bool verificarCodigoZona(string strCodigo)
        {
            try 
            {
                bool exists = false;
                if (lstZona.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstZona.Where(x => x.zonc_iid_zona.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstZona.Where(x => x.zonc_iid_zona.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.zonc_icod_zona != Obe.zonc_icod_zona).ToList().Count > 0)
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
                if (lstZona.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstZona.Where(x => x.zonc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstZona.Where(x => x.zonc_vdescripcion.Trim() == strNombre.Trim() && x.zonc_icod_zona!= Obe.zonc_icod_zona).ToList().Count > 0)
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