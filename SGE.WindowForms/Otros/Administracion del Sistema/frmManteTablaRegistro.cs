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

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteTablaRegistro : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteTablaRegistro));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETablaRegistro Obe = new ETablaRegistro();
        public List<ETablaRegistro> lstTabla = new List<ETablaRegistro>();
        public int intTabla = 0;
        public bool flagExterno = false;

        public frmManteTablaRegistro()
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
            lkpSituacion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;            
            
        }
        public void setValues()
        {
            txtCodigo.Text = Obe.tarec_icorrelativo_registro.ToString();
            txtDescripcion.Text = Obe.tarec_vdescripcion;
            lkpSituacion.EditValue = (Obe.tarec_cestado == 'A') ? 1 : 0;
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
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código de tabla");
                }                  

                if (String.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese nombre de la tabla");
                }
                if (verificarDescripcionUnidadM(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("El nombre ingresado ya existe en los registros de tabla");
                }


                Obe.tablc_iid_tipo_tabla = intTabla;
                Obe.tarec_icorrelativo_registro = Convert.ToInt32(txtCodigo.Text);
                Obe.tarec_vdescripcion = txtDescripcion.Text;
                Obe.tarec_cestado = (Convert.ToInt32(lkpSituacion.EditValue) == 1) ? 'A' : 'I';
              
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tarec_iid_tabla_registro = new BAdministracionSistema().insertarTablaRegistro(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAdministracionSistema().modificarTablaRegistro(Obe);
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
                    if (flagExterno)
                        this.MiEvento(Convert.ToInt32(Obe.tarec_vdescripcion));
                    else
                        this.MiEvento(Obe.tarec_iid_tabla_registro);
                    this.Close();
                }
            }
        }   
      
        private bool verificarDescripcionUnidadM(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstTabla.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstTabla.Where(x => x.tarec_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTabla.Where(x => x.tarec_vdescripcion.Trim() == strNombre.Trim() && x.tarec_iid_tabla_registro != Obe.tarec_iid_tabla_registro).ToList().Count > 0)
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

        private void frmManteTabla_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);          
        }
        
    }
}