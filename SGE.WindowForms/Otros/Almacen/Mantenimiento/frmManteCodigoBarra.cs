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
    public partial class frmManteCodigoBarra : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteCodigoBarra));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ECodigoBarra Obe = new ECodigoBarra();
        public List<ECodigoBarra> lstCodigoBarra = new List<ECodigoBarra>();
        public int prdc_icod_producto = 0;
        public int Indicador = 0;
        public frmManteCodigoBarra()
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
            txtCodigoBarra.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
        }
        public void setValues()
        {
            txtCodigoBarra.Text = Obe.codb_iid_codigo_barra;            
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
                if (String.IsNullOrEmpty(txtCodigoBarra.Text))
                {
                    oBase = txtCodigoBarra;
                    throw new ArgumentException("Ingrese Codigo de Barra");
                }
                //if (verificarDescripcionTipoDoc(txtCodigoBarra.Text))
                //{
                //    oBase = txtCodigoBarra;
                //    throw new ArgumentException("El nombre ingresado ya existe en los registros de Módulo");
                //}

                Obe.prdc_icod_producto = prdc_icod_producto;
                Obe.codb_iid_codigo_barra = txtCodigoBarra.Text;               
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.Indicador = Indicador;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    lstCodigoBarra.Add(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    //new BAlmacen().modificarCodi(Obe);
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
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }   
       
        //private bool verificarDescripcionTipoDoc(string strNombre)
        //{
            //try
            //{
            //    bool exists = false;
            //    if (lstModulo.Count > 0)
            //    {
            //        if (Status == BSMaintenanceStatus.CreateNew)
            //        {
            //            if (lstModulo.Where(x => x.moduc_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
            //                exists = true;
            //        }
            //        if (Status == BSMaintenanceStatus.ModifyCurrent)
            //        {
            //            if (lstModulo.Where(x => x.moduc_vdescripcion.Trim() == strNombre.Trim() && x.moduc_icod_modulo != Obe.moduc_icod_modulo).ToList().Count > 0)
            //                exists = true;
            //        }
            //    }
            //    return exists;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        //}

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