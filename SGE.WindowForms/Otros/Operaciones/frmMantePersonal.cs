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

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmMantePersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMantePersonal));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EPersonal Obe = new EPersonal();
        public List<EPersonal> lstPersonal = new List<EPersonal>();

        public frmMantePersonal()
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
            txtCodigo.Enabled = false;
            dteFechaRegistro.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            txtApellidoNombres.Enabled = !Enabled;
            txtDNI.Enabled = !Enabled;
            dteFechaNacimiento.Enabled = !Enabled;
            lkpCargo.Enabled = !Enabled;
            lkpArea.Enabled = !Enabled;            
            btnGuardar.Enabled = !Enabled;           
        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{0:0000}", Obe.perc_iid_personal);
            dteFechaRegistro.EditValue = Obe.perc_sfecha_registro;
            lkpSituacion.EditValue = Obe.perc_iid_situacion_perso;
            txtApellidoNombres.Text = Obe.perc_vapellidos_nombres;
            txtDNI.Text = Obe.perc_vdni;
            dteFechaNacimiento.EditValue = Obe.perc_sfecha_nacimiento;
            lkpCargo.EditValue = Obe.tablc_iid_tipo_cargo;
            lkpArea.EditValue = Obe.tablc_iid_tipo_area;
            chComprador.Checked = Obe.perc_flag_comprador;        
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
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los registros de personal");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtApellidoNombres.Text))
                {
                    oBase = txtApellidoNombres;
                    throw new ArgumentException("Ingrese Apellidos y Nombres");
                }
                /*----------------------*/

                if (verificarNombre(txtApellidoNombres.Text))
                {
                    oBase = txtApellidoNombres;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de personal");
                }
                
                /*----------------------*/
                if (String.IsNullOrEmpty(lkpCargo.Text))
                {
                    oBase = lkpCargo;
                    throw new ArgumentException("Seleccione cargo");
                }                    
                /*----------------------*/
                if (String.IsNullOrEmpty(lkpArea.Text))
                {
                    oBase = lkpArea;
                    throw new ArgumentException("Seleccione área");
                }    
                /*----------------------*/    
                               
                Obe.perc_iid_personal = txtCodigo.Text;
                Obe.perc_sfecha_registro = Convert.ToDateTime(dteFechaRegistro.EditValue);
                Obe.perc_iid_situacion_perso = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.perc_vapellidos_nombres = txtApellidoNombres.Text;
                Obe.perc_vdni = txtDNI.Text;
                Obe.perc_sfecha_nacimiento =DateTime.Now;
                Obe.tablc_iid_tipo_cargo = Convert.ToInt32(lkpCargo.EditValue);
                Obe.tablc_iid_tipo_area = Convert.ToInt32(lkpArea.EditValue);
                Obe.perc_flag_comprador = chComprador.Checked;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;                

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.perc_icod_personal = new BOperaciones().insertarPersonal(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BOperaciones().modificarPersonal(Obe);
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

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstPersonal.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstPersonal.Where(x => x.perc_vapellidos_nombres.Replace(" ","").Trim() == strNombre.Replace(" ","").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstPersonal.Where(x => x.perc_vapellidos_nombres.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.perc_icod_personal != Obe.perc_icod_personal).ToList().Count > 0)
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

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            dteFechaRegistro.EditValue = DateTime.Now;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpCargo, new BGeneral().listarTablaRegistro(31), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpArea, new BGeneral().listarTablaRegistro(32), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);          
        }

        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {

        }
        
    }
}