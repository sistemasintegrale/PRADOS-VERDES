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
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.Linq;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteUsuario : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public EUsuario Obe = new EUsuario();
        public List<EUsuario> mlist = new List<EUsuario>();
        private string OldPswd = "";
        private BSMaintenanceStatus mStatus;

        public frmManteUsuario()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtUsuario.Enabled = false;
                txtCntr.Enabled = false;
                cbModificarContrasena.Enabled = true;
                lkpSituacion.Enabled = true;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtUsuario.Enabled = false;
                txtNombreApe.Enabled = false;
                txtCntr.Enabled = false;
                btnGuardar.Enabled = false;
                lkpSituacion.Enabled = false;
            }
        }
        public void setValues() 
        {
            txtUsuario.Text = Obe.usua_codigo_usuario;
            txtNombreApe.Text = Obe.usua_nombre_usuario;
            txtCntr.Text = "ClaveEncriptada";
            lkpSituacion.EditValue = Convert.ToInt32(Obe.usua_iactivo);
            OldPswd = Modules.UserCoDec.decod(Obe.usua_password_usuario);
            chkAsesor.Checked = Obe.usua_indicador_asesor;
            lkpAsesor.EditValue = Obe.vendc_icod_vendedor;
            ckWeb.Checked = Obe.usua_bweb;
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
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    oBase = txtUsuario;
                    throw new ArgumentException("Ingrese usuario");
                }
                if (string.IsNullOrEmpty(txtNombreApe.Text))
                {
                    oBase = txtNombreApe;
                    throw new ArgumentException("Ingrese nombres y apellidos del usuario");
                }
                if (string.IsNullOrEmpty(txtCntr.Text))
                {
                    oBase = txtCntr;
                    throw new ArgumentException("Ingrese contraseña");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (string.IsNullOrEmpty(txtCntrConfirma.Text))
                    {
                        oBase = txtCntrConfirma;
                        throw new ArgumentException("Ingrese confimación de contraseña");
                    }
                    if (txtCntrConfirma.Text != txtCntr.Text)
                    {
                        oBase = txtCntrConfirma;
                        throw new ArgumentException("Los campos Contraseña y Confirmar Contraseña no coinciden");
                    }
                }
                else
                {
                    if (cbModificarContrasena.Checked == true)
                    {
                        //if (string.IsNullOrEmpty(txtCntrAntigua.Text))
                        //{
                        //    oBase = txtCntrAntigua;
                        //    throw new ArgumentException("Ingrese contraseña antigua");
                        //}
                        if (txtCntrAntigua.Text != OldPswd)
                        {
                            oBase = txtCntrAntigua;
                            throw new ArgumentException("La contraseña antigua ingresada no coincide con la contraseña registrada");
                        }
                        if (string.IsNullOrEmpty(txtCntrNueva.Text))
                        {
                            oBase = txtCntrNueva;
                            throw new ArgumentException("Ingrese contraseña nueva");
                        }
                        if (string.IsNullOrEmpty(txtCntrNuevaConfirma.Text))
                        {
                            oBase = txtCntrNuevaConfirma;
                            throw new ArgumentException("Ingrese confirmación de contraseña nueva");
                        }
                        if (txtCntrNuevaConfirma.Text != txtCntrNueva.Text)
                        {
                            oBase = txtCntrNuevaConfirma;
                            throw new ArgumentException("El campo de Contraseña Nueva y Confirmación no coinciden");
                        }
                    }
                }
                Obe.usua_codigo_usuario = txtUsuario.Text;
                Obe.usua_nombre_usuario = txtNombreApe.Text;
                Obe.usua_password_usuario = Modules.UserCoDec.codec(txtCntr.Text);
                Obe.usua_iactivo = Convert.ToBoolean(lkpSituacion.EditValue);
                Obe.intUsuario = Valores.intUsuario;
                Obe.usua_indicador_asesor = Convert.ToBoolean(chkAsesor.Checked);
                Obe.vendc_icod_vendedor = Convert.ToInt32(lkpAsesor.EditValue);
                Obe.usua_bweb = ckWeb.Checked;
                if (Status == BSMaintenanceStatus.CreateNew)                                    
                    Obe.usua_icod_usuario =  new BAdministracionSistema().insertarUsuario(Obe);                
                else
                {
                    if (cbModificarContrasena.Checked == true)
                        Obe.usua_password_usuario = Modules.UserCoDec.codec(txtCntrNueva.Text);
                    else
                        Obe.usua_password_usuario = Modules.UserCoDec.codec(OldPswd);

                    new BAdministracionSistema().modificarUsuario(Obe);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    MiEvento(Obe.usua_icod_usuario);
                    Close();
                }
            }
        }
       
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void cbModificarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            if (cbModificarContrasena.Checked == true)
            {
                this.Height = 285;
                pnModificarContraseña.Visible = true;
                limpiarContrasenia();
            }
            else
            {
                this.Height = 204;
                pnModificarContraseña.Visible = false;
                limpiarContrasenia();
            }
        }
        private void limpiarContrasenia()
        {
            txtCntrAntigua.Text = string.Empty;
            txtCntrNueva.Text = string.Empty;
            txtCntrNuevaConfirma.Text = string.Empty;
        }

        private void txtCntr_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCntr.Text.Length > 0)
                txtCntrConfirma.Enabled = true;
            else
                txtCntrConfirma.Enabled = false;
        }

        private void txtCntrAntigua_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCntrAntigua.Text == OldPswd)
                txtCntrNueva.Enabled = true;
            else
            {
                txtCntrNueva.Enabled = false;
                txtCntrNueva.Text = string.Empty;
                txtCntrNuevaConfirma.Enabled = false;
                txtCntrNuevaConfirma.Text = string.Empty;
            }      
        }

        private void txtCntrNueva_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCntrNueva.Text.Length > 0)
                txtCntrNuevaConfirma.Enabled = true;
            else
                txtCntrNuevaConfirma.Enabled = false;
        }
        char space = ' ';

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                XtraMessageBox.Show("Espacios en blanco no son permitidos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Text = txtUsuario.Text.Trim(space);
            }    
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void frmManteUsuario_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaEstado), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lkpAsesor, new BVentas().listarVendedor().Where(x=>x.tablc_iid_situacion_vendedor ==6).ToList(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", false);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerContaseña.Checked == true)
            {
                txtCntr.Properties.UseSystemPasswordChar = false;
                txtCntr.Text = OldPswd;
            }
            else
            {
                txtCntr.Properties.UseSystemPasswordChar = true;
                txtCntr.Text = "ClaveEncriptada";
            }
        }

        private void chkAsesor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsesor.Checked == true)
            {
                lkpAsesor.Enabled = true;
            }
            else
            {
                lkpAsesor.Enabled = false;
                lkpAsesor.EditValue = 0;
                lkpAsesor.Text = "";
            }
        }
    }
}