using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Tesoreria;
using System.Xml;
using SGE.WindowForms.Otros.Tesoreria.Bancos;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmManteCostosReporteProduccion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCostosReporteProduccion));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        public List<ECostoReporteProduccion> oDetail = new List<ECostoReporteProduccion>();
        List<ETipoCambio> ListaTipoCambio = new List<ETipoCambio>();
        public ECostoReporteProduccion oBE = new ECostoReporteProduccion();
        public BSMaintenanceStatus oState;
        public int intIdReporteProduccion = 0;
        public int intIdCostoReporteProduccion = 0;
        private int intIdProveedor = 0;
        public int icod_moneda;
        public long intIdDocumentoPorPagar = 0;


        #endregion

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

        #region "Eventos"

        public FrmManteCostosReporteProduccion()
        {
            InitializeComponent();
        }

        private void FrmManteCostosReporteProduccion_Load(object sender, EventArgs e)
        {
            CargaControles();
            if (Status != BSMaintenanceStatus.CreateNew)
            {
                lkpTipoCosto.EditValue = oBE.crp_tipo_costo;
                lkpMoneda.EditValue = oBE.tablc_iid_tipo_moneda;
            }
        }

        private void btnProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedor();
        }

        private void btnDocumentoPorPagar_Click(object sender, EventArgs e)
        {
            if (intIdProveedor == 0)
            {
                XtraMessageBox.Show("Seleccione un proveedor.", "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmListarDocumentoPorPagarProveedor DocProveedor = new FrmListarDocumentoPorPagarProveedor();
            DocProveedor.intIcodProveedor = intIdProveedor;
            DocProveedor.intMoneda = icod_moneda;
            DocProveedor.flag_reporte_produccion = true;
            DocProveedor.Carga();
            if (DocProveedor.ShowDialog() == DialogResult.OK)
            {
                intIdDocumentoPorPagar = Convert.ToInt64(DocProveedor._oBe.doxpc_icod_correlativo);
                txtDocumento.Text = DocProveedor._oBe.tdocc_vabreviatura_tipo_doc;
                txtNumero.Text = DocProveedor._oBe.doxpc_vnumero_doc;
                deFechaDoc.EditValue = DocProveedor._oBe.doxpc_sfecha_doc;
            }

            lkpTipoCosto.Focus();
        }

       
      

        #endregion


        #region "Metodos"

        private void ListarProveedor()
        {
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                intIdProveedor = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Tag = Proveedor._Be.iid_icod_proveedor;
                btnProveedor.Text = Proveedor._Be.vcod_proveedor;
                txtProveedor.Text = Proveedor._Be.vnombrecompleto;
            }

            btnDocumentoPorPagar.Focus();
        }

       

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            btnProveedor.Enabled = !Enabled;
            lkpTipoCosto.Enabled = !Enabled;
            lkpMoneda.Enabled = !Enabled;
            txtTipoDeCambio.Enabled = !Enabled;
            txtMonto.Enabled = !Enabled;
            btnProveedor.Focus();
        }

        private void clearControl()
        {
            btnProveedor.Text = "";
            lkpTipoCosto.Text = "";
            lkpMoneda.Text = "";
            txtMonto.Text = "0.00";
        }

        private void CargaControles()
        {
            //BSControls.LoaderLook(lkpMoneda, new BTipoMoneda().ListarTipoMoneda(), "descripcion", "idc_Tipo_Moneda", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpTipoCosto, new BGeneral().listarTablaRegistro(92), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            //BSControls.LoaderLook(lkpTipoCosto, new BTablaRegistro().ListarTablaRegistro(Parametros.intTablaTipoCosto), "Descripcion", "IdTablaRegistro", true);/*Falta Completar*/
            ListaTipoCambio = new BAdministracionSistema().listarTipoCambio();

            var Lista = ListaTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoDeCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
            
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
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
            {
                {
                    BaseEdit oBase = null;
                    Boolean Flag = true;
                    try
                    {

                        if (string.IsNullOrEmpty(btnProveedor.Text))
                        {
                            oBase = btnProveedor;
                            throw new ArgumentException("Seleccionar un proveedor.");
                        }

                        if (intIdDocumentoPorPagar == 0)
                        {
                            oBase = btnProveedor;
                            throw new ArgumentException("Seleccionar un documento por pagar.");
                        }

                        if (string.IsNullOrEmpty(lkpTipoCosto.EditValue.ToString()))
                        {
                            oBase = lkpTipoCosto;
                            throw new ArgumentException("Seleccionar un tipo de costo.");
                        }
                       
                        if (Convert.ToDecimal(txtMonto.Text) == 0)
                        {
                            oBase = txtMonto;
                            throw new ArgumentException("El monto no puede ser 0.");
                        }

                        if (Status == BSMaintenanceStatus.CreateNew)
                        {
                            var BuscarDocumento = oDetail.Where(oB => oB.crp_tipo_costo == Convert.ToInt32(lkpTipoCosto.EditValue)).ToList();
                            if (BuscarDocumento.Count > 0)
                            {
                                oBase = btnProveedor;
                                throw new ArgumentException("El tipo de costo ya existe en el reporte de producción");
                            }
                        }

                        decimal dmlMonto = 0;
                        decimal.TryParse(txtMonto.Text, out dmlMonto);

                        oBE.crp_icod_costo = intIdCostoReporteProduccion;
                        oBE.rp_icod_produccion = intIdReporteProduccion;
                        oBE.proc_icod_proveedor = Convert.ToInt32(btnProveedor.Tag);
                        oBE.proc_vcod_proveedor = btnProveedor.Text;
                        oBE.proc_vnombrecompleto = txtProveedor.Text;
                        oBE.doxpc_icod_correlativo = intIdDocumentoPorPagar;
                        oBE.tdocc_vabreviatura_tipo_doc = txtDocumento.Text;
                        oBE.doxpc_vnumero_doc = txtNumero.Text;
                        oBE.doxpc_sfecha_doc = Convert.ToDateTime(deFechaDoc.EditValue);
                        oBE.crp_tipo_costo = Convert.ToInt32(lkpTipoCosto.EditValue);
                        oBE.TipoCosto = lkpTipoCosto.Text;
                        oBE.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                        oBE.Moneda = lkpMoneda.Text;
                        oBE.crp_nmonto_pago = dmlMonto;
                        oBE.crp_flag_estado = true;
                        oBE.crp_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoDeCambio.Text);
                        //oBE.unidc_vabreviatura_unidad_medida=txtu
                      
                        this.DialogResult = DialogResult.OK;
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
                            this.Close();
                        }
                    }
                }
            }
        }

        #endregion

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnProveedor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode==Keys.F10)
                ListarProveedor();
        }

    }
}