﻿using System;
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

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroTablaPlanillaModelo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroTablaPlanilla));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        private BSMaintenanceStatus mStatus;

        public EPlanillaModeloCont Obe = new EPlanillaModeloCont();
        public List<EPlanillaModeloCont> lstTablaPlanilla = new List<EPlanillaModeloCont>();

        
        //public decimal SumaPorcentaje=0;
        public frmRegistroTablaPlanillaModelo()
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
            txtNombre.Enabled = !Enabled;              
            btnGuardar.Enabled = !Enabled;
            

            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
               txtCodigo.Enabled = !Enabled;                
                txtNombre.Enabled = !Enabled;
             
                
        }
        public void setValues()
        {
            txtCodigo.Text = String.Format("{00:00}", Obe.plcd_iid);
            txtNombre.Text = Obe.plcd_vdescrpcion;
        
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
                    throw new ArgumentException("Ingrese Descripción Tabla Planilla");
                }
                if (verificarNombreAlmacen(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("La Descripción ingresado ya existe en Tabla Planilla");
                }
             
                 
                                 
                Obe.plcd_iid = Convert.ToInt32(txtCodigo.Text);
                Obe.plcd_vdescrpcion = txtNombre.Text;
                
                //SumaPorcentaje = lstFondosPensionesConceptos.Sum(x => Convert.ToDecimal(x.fdpd_nporcentaje_concepto));
                //Obe.fdpc_nporcentaje_fijo = SumaPorcentaje; 
                
                //Obe.fdpc_nporcentaje_fijo = Convert.ToDecimal(txtFijo.Text);  
                //--------
                
                //Obe.tbpc_flag_estado = true;                
                //Obe.intUsuario = Valores.intUsuario;
                //Obe.strPc = WindowsIdentity.GetCurrent().Name;              
               
                      
                      

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.plcc_pland_icod = new BPlanillas().insertarTablaPlanillaModelo(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                   // new BPlanillas().modificarTablaPlanilla(Obe);
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
                    this.MiEvento(Obe.plcc_pland_icod);
                    this.Close();
                }
            }
        }   
        private bool verificarNombreAlmacen(string strNombre)
        {
            try 
            {
                bool exists = false;
                if (lstTablaPlanilla.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTablaPlanilla.Where(x => x.plcd_vdescrpcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTablaPlanilla.Where(x => x.plcd_vdescrpcion.Trim() == strNombre.Trim() && x.plcd_iid != Obe.plcd_iid).ToList().Count > 0)
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
      
        private void frmManteAlmacen_Load(object sender, EventArgs e)
        {
           
        

        }

    

       

    
        private void lkpEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

      
     
        
    }
}