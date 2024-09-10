using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteTablaVentasDet : XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteTablaVentasDet));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ETablaVentaDet Obe = new ETablaVentaDet();
        public List<ETablaVentaDet> lstTabla = new List<ETablaVentaDet>();
        public int intTabla = 0;
        public bool flagExterno = false;

        public frmManteTablaVentasDet()
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
            txtAbreviatura.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtCodigo.Enabled = Enabled;

        }
        public void setValues()
        {
            txtCodigo.Text = Obe.tabvd_icorrelativo_venta_det.ToString();
            txtAbreviatura.Text = Obe.tabvd_vdesc_abreviado;
            txtDescripcion.Text = Obe.tabvd_vdescripcion;
            lkpSituacion.EditValue = (Obe.tabvd_cestado == 'A') ? 1 : 0;
            lkpCodPlan.EditValue = Obe.tabvd_icod_ref;
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


                Obe.tabvc_iid_tipo_tabla = intTabla;
                Obe.tabvd_icorrelativo_venta_det = Convert.ToInt32(txtCodigo.Text);
                Obe.tabvd_vdesc_abreviado = txtAbreviatura.Text;
                Obe.tabvd_vdescripcion = txtDescripcion.Text;
                Obe.tabvd_cestado = (Convert.ToInt32(lkpSituacion.EditValue) == 1) ? 'A' : 'I';
                Obe.tabvd_icod_ref = Convert.ToInt32(lkpCodPlan.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.tabvd_iid_tabla_venta_det = new BAdministracionSistema().insertarTablaVentaDet(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAdministracionSistema().modificarTablaVentaDet(Obe);
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
                        this.MiEvento(Convert.ToInt32(Obe.tabvd_vdescripcion));
                    else
                        this.MiEvento(Obe.tabvd_iid_tabla_venta_det);
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
                        if (lstTabla.Where(x => x.tabvd_vdescripcion.Trim() == strNombre.Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstTabla.Where(x => x.tabvd_vdescripcion.Trim() == strNombre.Trim() && x.tabvd_iid_tabla_venta_det != Obe.tabvd_iid_tabla_venta_det).ToList().Count > 0)
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