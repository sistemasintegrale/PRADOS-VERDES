using System;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmManteParametro : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteParametro));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;      
        public EParametro oBe = new EParametro();
        public ControlEquipos objEquipo = new ControlEquipos();
        public frmManteParametro()
        {            
            InitializeComponent();
        }

        private void FrmVariables_Load(object sender, EventArgs e)
        {
            btnUbicacionActualizador.Text = Valores.strUbicacionActualizador;
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
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
            txtIGV.Enabled = !Enabled;
            txtUIT.Enabled = !Enabled;
            txt4taCategoria.Enabled = !Enabled;
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
                if (txtIGV.Text=="0.00")
                {
                    oBase = txtIGV;
                    throw new ArgumentException("Ingrese IGV");
                }
              
               
                if (txtUIT.Text == "0.00")
                {
                    oBase = txtIGV;
                    throw new ArgumentException("Ingrese UIT");
                }
                if (string.IsNullOrEmpty(txtEmpresa.Text))
                {
                    oBase = txtEmpresa;
                    throw new ArgumentException("Ingrese nombre de empresa");
                }
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingrese dirección fiscal de la empresa");
                }
                if (string.IsNullOrEmpty(txtRuc.Text))
                {
                    oBase = txtRuc;
                    throw new ArgumentException("Ingrese RUC de la empresa");
                }                                
                
                oBe.pm_nigv_parametro = Convert.ToDecimal(txtIGV.Text);
                oBe.pm_nuit_parametro = Convert.ToDecimal(txtUIT.Text);
                oBe.pm_ncategoria_parametro = Convert.ToDecimal(txt4taCategoria.Text);
                oBe.pm_nombre_empresa = txtEmpresa.Text;
                oBe.pm_direccion_empresa = txtDireccion.Text;
                oBe.pm_vruc = txtRuc.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.pm_correlativo_OR = Convert.ToInt64(txtCorrelativoOR.Text);
                oBe.pm_ntipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                oBe.urlServicioFacturaElectronica = txtServicioFE.Text;
                oBe.urlServicioNotaCredito = txtServicioNC.Text;
                oBe.urlServicioNotaDebito = txtServicioND.Text;
                oBe.Ruc = txtRucFE.Text;
                oBe.UsuarioSol = txtUsuarioSol.Text;
                oBe.ClaveSol = txtClaveSol.Text;
                oBe.EndPointUrlPrueba = txtURLPrueba.Text;
                oBe.EndPointUrlDesarrollo = txtURLDesarrollo.Text;
                oBe.PasswordCertificado = txtPwCertificado.Text;
                oBe.CertificadoDigital = txtCertificadoDigital.Text;
                oBe.urlServicioEnviarDocumento = txtServicioEnviarDoc.Text;
                oBe.urlServicioFirma = txtServicioFirma.Text;
                oBe.IdServiceValidacion = txtValidacion.Text;
                oBe.pm_sfecha_inicio = Convert.ToDateTime(dtmFechaInicio.DateTime);
                oBe.DirecciónXML = txtDireccionXML.Text;
                oBe.urlServicioEnvioResumen = txtServicioER.Text;
                oBe.urlServicoGenerarResumen = txtServicioGR.Text;
                oBe.IdServiceValidacionResumen = txtValidacionResumen.Text;
                oBe.pm_vruta_resumen = txtDireccionResumen.Text;
                oBe.ServiceConsultaTiket = txtServicioCT.Text;
                new BAdministracionSistema().modificarParametro(oBe);
                objEquipo = new BAdministracionSistema().Equipo_Obtner_Datos(Valores.strNombreEquipo, Valores.strIdCpu);
                objEquipo.cep_vubicacion_actualizador = btnUbicacionActualizador.Text;
                new BAdministracionSistema().Equipo_Modificar(objEquipo);
                Valores.strUbicacionActualizador = btnUbicacionActualizador.Text;
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {                   
                    this.MiEvento();
                    this.Close();
                }
            }
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnUbicacionActualizador_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                btnUbicacionActualizador.Text = openFileDialog.FileName;
                 
            }
        }
    }
}