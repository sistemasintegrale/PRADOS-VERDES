﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using System.Security.Principal;
using SGE.WindowForms.Modules;
using System.Net;
using System.Text;
namespace SGI.WindowsForm.Otros.Ventas
{
  
    public partial class FrmComunicacionBaja : DevExpress.XtraEditors.XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmComunicacionBaja));
        public FrmComunicacionBaja()
        {
            KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();            
        }

        public EPlanillaCobranzaDet obj = new EPlanillaCobranzaDet();
        public int IndicadorAnulacion = 0;
        public bool flag_close = false;


        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;            
            }
        }
                
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;            
        }
                
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void SetSave()
        {
            BaseEdit oControl = null;
            Boolean Flag = true;
            
            try
            {                
                if (string.IsNullOrEmpty(txtDescripcionMotivo.Text))
                {
                    oControl = txtDescripcionMotivo;
                    throw new ArgumentException("Ingresar la Observación");
                }
                //obj.fechaGeneracion = Convert.ToDateTime(deFecha.EditValue);
                obj.favc_descripcion_motivo_baja = txtDescripcionMotivo.Text;                                                
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                     new BVentas().ActualizarMotivoBajaCabeceraFactura(obj);
                    IndicadorAnulacion = 1;

                    //ExportarATXT(obj);
                }
                else
                {                    
                    //Obl.ActualizarObservacionGuiaRemision(obj);
                }
            }
            catch (Exception ex)
            {
                oControl.Focus();
                oControl.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                oControl.ErrorText = ex.Message;
                oControl.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    flag_close = true;
                    this.DialogResult = DialogResult.OK;
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                     else
                        Status = BSMaintenanceStatus.View;
                      
                    Status = BSMaintenanceStatus.View;                    
                    this.Close();
                }
            }
        }
      
        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmObservacionGuiaRemision_Load(object sender, EventArgs e)
        {

        }
   
    }
}