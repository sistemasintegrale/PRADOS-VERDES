using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System.Linq;
using SGE.BusinessLogic;
using DevExpress.XtraBars;
using SGE.WindowForms.Otros.Almacen.Listados;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteIngresoReporteProduccion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteIngresoReporteProduccion));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BCompras Obl;
        public int IdReporteProduccion = 0;
        public int IdProductoEspecifico = 0;
        public string NumeroRP = "";

        private BSMaintenanceStatus mStatus;

        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        #endregion

        #region "Eventos"

        public FrmManteIngresoReporteProduccion()
        {
            InitializeComponent();
        }

        private void FrmManteIngresoReporteProduccion_Load(object sender, EventArgs e)
        {
            dtmFecha.EditValue = DateTime.Now;
            dtmFecha.Focus();
        }

        #endregion

        #region "Metodos"

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            dtmFecha.Enabled = !Enabled;
            dtmFecha.Focus();
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EReporteProduccion oBe = new EReporteProduccion();
            Obl = new BCompras();
            try
            {
                if (Convert.ToInt32(btnAlmacen.Tag) == 0)
                {
                    oBase = btnAlmacen;
                    throw new ArgumentException("Seleccione el almacén");
                }

                //Datos del Reporte de Producción
                oBe.rp_icod_produccion = IdReporteProduccion;
                oBe.rp_id_kardex_producto_ingreso = 0;
                oBe.rp_sfecha_ing_kardex = Convert.ToDateTime(dtmFecha.EditValue);
                oBe.rp_iid_situacion = Parametros.intSitReporteProduccionActualizado;

                //Datos del Kardex
                EKardex objE_Kardex = new EKardex();
                objE_Kardex.kardc_ianio = Parametros.intEjercicio;
                objE_Kardex.kardc_fecha_movimiento = Convert.ToDateTime(dtmFecha.EditValue);
                objE_Kardex.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                objE_Kardex.prdc_icod_producto = IdProductoEspecifico;
                objE_Kardex.kardc_icantidad_prod = Convert.ToDecimal(txtCantidad.EditValue);
                objE_Kardex.tdocc_icod_tipo_doc = Parametros.intTipoDocReporteProduccion;
                objE_Kardex.kardc_numero_doc = NumeroRP;
                objE_Kardex.kardc_tipo_movimiento = Parametros.intKardexIn;
                objE_Kardex.kardc_iid_motivo = Parametros.intMovIngTransformacion;
                objE_Kardex.kardc_beneficiario = btnAlmacen.Text;
                objE_Kardex.kardc_observaciones = "REPORTE DE PRODUCCIÓN";
                //objE_Kardex.kardc_iusuario_crea = 0;
                //objE_Kardex.kardc_flag_estado = true;

                Obl.ActualizarReporteProduccionKardex(oBe, objE_Kardex);

            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Flag = false;
                }
            }
            finally
            {
                if (Flag)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                        Status = BSMaintenanceStatus.View;
                    else
                        Status = BSMaintenanceStatus.View;

                    Status = BSMaintenanceStatus.View;
                    this.MiEvento();
                    this.Close();
                }
            }
        }

      

        #endregion

        private void btnAgregar_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btncance_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarAlamcen();
        }

        private void ListarAlamcen()
        {
            frmListarAlmacen Almacen = new frmListarAlmacen();
            //Almacen.Carga();
            if (Almacen.ShowDialog() == DialogResult.OK)
            {
                btnAlmacen.Tag = Almacen._Be.almac_icod_almacen;
                btnAlmacen.Text = Almacen._Be.almac_vdescripcion;
            }
            dtmFecha.Focus();
        }
        
    }
}