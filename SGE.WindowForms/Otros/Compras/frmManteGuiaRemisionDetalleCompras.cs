﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System.Linq;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteGuiaRemisionDetalleCompras : DevExpress.XtraEditors.XtraForm
    {
        public List<EGuiaRemisionComprasDetalle> lstDetalle = new List<EGuiaRemisionComprasDetalle>();
        public EGuiaRemisionComprasDetalle oBe = new EGuiaRemisionComprasDetalle();
        public EGuiaRemisionCompras oBeCab = new EGuiaRemisionCompras();
        public decimal Cantidad = 0;


        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteGuiaRemisionDetalleCompras()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {                
               
            }
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

        private void setValues()
        {
            txtCantidad.Text = oBe.grcd_ncantidad.ToString();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {


                oBe.grcd_ncantidad=Convert.ToDecimal(txtCantidad.Text);
                //oBe.grcd_ncantidad2 = Cantidad;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
       

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
                }

                this.DialogResult = DialogResult.OK;
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

        private void frmManteGuiaRemisionDetalle_Load(object sender, EventArgs e)
        {

         

        }

     
     

   
    }
}