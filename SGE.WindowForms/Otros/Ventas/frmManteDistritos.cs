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
    public partial class frmManteDistritos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteDistritos));
        public delegate void DelegadoMensaje(int intIcod, int intIcodCategoria);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EDistritoZona Obe = new EDistritoZona();
        public List<EDistritoZona> lstDistritoZona = new List<EDistritoZona>();
        public int intIcodZona = 0;
        public int intDistrito = 0;
        public frmManteDistritos()
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
            btnGuardar.Enabled = !Enabled;
  
            
           
        }
        public void setValues()
        
        {            
         
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
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    List<EDistritoZona> listaexistente = new BVentas().listarVerificarDistrito(Convert.ToInt32(lkpDistrito.EditValue));

                    if (listaexistente.Count > 0)
                    {
                        oBase = lkpDistrito;
                        throw new ArgumentException("El nombre ingresado ya existe en los registros de Almacenes");
                    }
                }

                //Obe.famic_iid_familia = (lstFamiliaCab.Count + 1);
                Obe.zonc_icod_zona = intIcodZona;
                Obe.disc_icod_distrito = Convert.ToInt32(lkpDistrito.EditValue); 
                Obe.disd_flag_estado = true;                
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.disd_icod_distrito_zona = new BVentas().insertarDistritoZona(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarDistritoZona(Obe);
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
                    this.MiEvento(Obe.disd_icod_distrito_zona, intIcodZona);
                    this.Close();
                }
            }
        }   

        private void frmManteFamiliaCab_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpDistrito, new BVentas().listarDistrito(), "disc_vdescripcion", "disc_icod_distrito", true);
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    

    }
}