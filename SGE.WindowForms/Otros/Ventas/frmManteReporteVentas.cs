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
using SGE.WindowForms.Ventas.Operaciones;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteReporteVentas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteReporteVentas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EReporteVentas Obe = new EReporteVentas();
        public List<EReporteVentas> lstReporteVentas = new List<EReporteVentas>();


        public frmManteReporteVentas()
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
            txtNroReporte.Enabled = !Enabled;
            lkpAsesor.Enabled = !Enabled;
            txtPrimerNombre.Enabled = !Enabled;
            txtSegundoNombre.Enabled = !Enabled;
            txtApellidoPaterno.Enabled = !Enabled;
            txtApellidoMaterno.Enabled = !Enabled;
            lkpTipoReporte.Enabled = !Enabled;
            lkpDistrito.Enabled = !Enabled;
            txtNombreComercial.Enabled = Enabled;
            txtNumeroDocumento.Enabled = Enabled;
            txtTelefono.Enabled = Enabled;
            txtContacto.Enabled = Enabled;         
            btnGuardar.Enabled = !Enabled;
            
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtNroReporte.Enabled = Enabled;
            

            if (Status == BSMaintenanceStatus.CreateNew)
                txtNroReporte.Enabled = !Enabled;
            
           
        }
        public void setValues()
        
        {
            txtNroReporte.Text = string.Format("{0:00}", Obe.revec_iid_reporte_ventas);
            lkpAsesor.EditValue = Obe.vendc_icod_vendedor;
            txtPrimerNombre.Text = Obe.revec_vprimer_nombre;
            txtSegundoNombre.Text = Obe.revec_vsegundo_nombre;
            txtApellidoPaterno.Text = Obe.revec_vapellido_paterno;
            txtApellidoMaterno.Text = Obe.revec_vapellido_materno;
            lkpTipoReporte.EditValue = Obe.tablc_iid_tipo_tabla;
            lkpDistrito.EditValue = Obe.disc_icod_distrito;
            bteFuneraria1.Tag = Obe.func_icod_funeraria;
            bteFuneraria1.Text = Obe.func_vnombre_comercial;
            txtNombreComercial.Text = Obe.func_vnombre_comercial;
            txtNumeroDocumento.Text = Obe.func_cnumero_docum_fun;
            txtTelefono.Text = Obe.func_vtelefonos;
            txtContacto.Text = Obe.func_vcontacto;
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
       
        private void cargar()
        {
            BSControls.LoaderLook(lkpDistrito, new BVentas().listarDistrito(), "disc_vdescripcion", "disc_icod_distrito", true);
            BSControls.LoaderLook(lkpAsesor, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpTipoReporte, new BGeneral().listarTablaRegistro(94), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            
            try
            {               
                /*----------------------*/
                if (String.IsNullOrEmpty(txtNroReporte.Text))
                {
                    oBase = txtNroReporte;
                    throw new ArgumentException("Ingrese código de Reporte de Venta");
                }
                if (verificarCodigoFuneraria(txtNroReporte.Text))
                {
                    oBase = txtNroReporte;
                    throw new ArgumentException("El código ingresado ya existe en los registros del Reporte de Venta");
                }

                /*----------------------*/

                Obe.revec_iid_reporte_ventas = Convert.ToInt32(txtNroReporte.Text);
                Obe.vendc_icod_vendedor = Convert.ToInt32(lkpAsesor.EditValue);
                Obe.revec_vprimer_nombre = txtPrimerNombre.Text;
                Obe.revec_vsegundo_nombre = txtSegundoNombre.Text;
                Obe.revec_vapellido_paterno = txtApellidoPaterno.Text;
                Obe.revec_vapellido_materno = txtApellidoMaterno.Text;
                Obe.tablc_iid_tipo_tabla = Convert.ToInt32(lkpTipoReporte.EditValue);
                Obe.disc_icod_distrito = Convert.ToInt32(lkpDistrito.EditValue);
                Obe.func_icod_funeraria = Convert.ToInt32(bteFuneraria1.Tag);
                Obe.func_vnombre_comercial = txtNombreComercial.Text;
                Obe.func_cnumero_docum_fun = txtNumeroDocumento.Text;
                Obe.func_vtelefonos = txtTelefono.Text;
                Obe.func_vcontacto = txtContacto.Text;
                Obe.revec_flag_estado = true;                
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.revec_icod_reporte_ventas = new BVentas().insertarReporteVentas(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarReporteVentas(Obe);
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
                    this.MiEvento(Obe.revec_icod_reporte_ventas);
                    this.Close();
                }
            }
        }   
        private bool verificarCodigoFuneraria(string strCodigo)
        {
            try 
            {
                bool exists = false;
                if (lstReporteVentas.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstReporteVentas.Where(x => x.revec_iid_reporte_ventas.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstReporteVentas.Where(x => x.revec_iid_reporte_ventas.ToString().Trim() == Convert.ToInt32(strCodigo).ToString().Trim() && x.revec_icod_reporte_ventas != Obe.revec_icod_reporte_ventas).ToList().Count > 0)
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

        private void frmManteFuneraria_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void lkpTipoReporte_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                if (Convert.ToInt32(lkpTipoReporte.EditValue) == 1)
                    {
                       bteFuneraria1.Enabled = !Enabled;
                       txtNombreComercial.Enabled = !Enabled;
                       txtNumeroDocumento.Enabled = !Enabled;
                       txtTelefono.Enabled = !Enabled;
                       txtContacto.Enabled = !Enabled;
                    }
                else
                    {
                       bteFuneraria1.Enabled = Enabled;
                       txtNombreComercial.Enabled = Enabled;
                       txtNumeroDocumento.Enabled = Enabled;
                       txtTelefono.Enabled = Enabled;
                       txtContacto.Enabled = Enabled;
                    }
            }
        }


        private void bteFuneraria1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarFuneraria();
        }
        private void listarFuneraria()
        {
            try
            {
                using (frm02RegistroFunerarias frm = new frm02RegistroFunerarias()) 
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteFuneraria1.Tag = frm._Be.func_icod_funeraria;
                        bteFuneraria1.Text = frm._Be.func_vnombre_comercial;
                        txtNombreComercial.Text = frm._Be.func_vnombre_comercial ;
                        txtNumeroDocumento.Text = frm._Be.func_cnumero_docum_fun;
                        txtTelefono.Text = frm._Be.func_vtelefonos;
                        txtContacto.Text = frm._Be.func_vcontacto;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}