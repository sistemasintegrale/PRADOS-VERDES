using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using SGE.Entity;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteRegistroParametro : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteRegistroParametro));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public FrmManteRegistroParametro()
        {
            this.KeyUp += cerrar_form;
            InitializeComponent();
        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public List<ERegistroParametro> lista = new List<ERegistroParametro>();
        public ERegistroParametro oBe = new ERegistroParametro();

        public int Correlative = 0;
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
            txtPuntoVenta.Enabled = !Enabled;
            txtCodigoPuntoVenta.Enabled = !Enabled;
            lkpsituacion.Enabled = !Enabled;
            txtPuntoVenta.Focus();
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtPuntoVenta.Enabled = !Enabled;
                txtCodigoPuntoVenta.Enabled = Enabled;
                lkpsituacion.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigoPuntoVenta.Enabled = Enabled;
                lkpsituacion.Enabled = !Enabled;
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
        }

        public void setValues()
        {
            txtSerieSolicitud.Text = DateTime.Now.Year.ToString();
            txtSerieSolicitud.Properties.ReadOnly = true;
            txtCodigoPuntoVenta.Text = (oBe.rgpmc_vcod_registro_parametro).ToString();
            txtPuntoVenta.Text = oBe.rgpmc_vdescripcion;
            txtSerieFactura.Text = oBe.rgpmc_vserie_factura;
            txtCorrelativoFactura.Text = oBe.rgpmc_icorrelativo_factura.ToString();
            txtSerieBoleta.Text = oBe.rgpmc_vserie_boleta;
            txtCorrelativoBoleta.Text = oBe.rgpmc_icorrelativo_boleta.ToString();
            txtSerieNotaCreditoF.Text = oBe.rgpmc_vserieF_nota_credito;
            txtSerieNotaCreditoB.Text = oBe.rgpmc_vserieB_nota_credito;
            txtCorrelativoNotaCredito.Text = oBe.rgpmc_icorrelativo_nota_credito.ToString();
            txtSerieNotaDebitoF.Text = oBe.rgpmc_vserieF_nota_debito;
            txtSerieNotaDebitoB.Text = oBe.rgpmc_vserieB_nota_debito;
            txtCorrelativoNotaDebito.Text = oBe.rgpmc_icorrelativo_nota_debito.ToString();
            txtCorretivoRC.Text = oBe.rgpmc_icorrelativo_recibo_caja.ToString();
            txtSerieRC.Text = oBe.rgpmc_vserie_recibo_caja;
            txtSerieContrato.Text = oBe.rgpmc_vserie_contrato;
            txtCorrelativoContrato.Text = oBe.rgpmc_icorrelativo_contrato.ToString();
            txtCorrelativoSolicitud.Text = oBe.rgpmc_icorrelativo_solicitud.ToString();
            txtDiasMora.Text = oBe.rgpmc_idias_mora.ToString();
            txtMora.Text = oBe.rgpmc_nmonto_mora.ToString();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;


            try
            {

                if (string.IsNullOrEmpty(txtPuntoVenta.Text))
                {
                    oBase = txtPuntoVenta;
                    throw new ArgumentException("Ingresar Descripción");
                }
                if (string.IsNullOrEmpty(txtCodigoPuntoVenta.Text))
                {
                    oBase = txtCodigoPuntoVenta;
                    throw new ArgumentException("Ingresar Código");
                }

                if (txtSerieContrato.Text.Length != 4)
                {
                    oBase = txtSerieContrato;
                    throw new ArgumentException("La Serie del Contrato debe ser de 4 Carateres");
                }


                oBe.rgpmc_vdescripcion = txtPuntoVenta.Text;
                oBe.rgpmc_vcod_registro_parametro = Convert.ToInt32(txtCodigoPuntoVenta.Text);
                oBe.tabl_iid_situacion = Convert.ToInt32(lkpsituacion.EditValue);

                if (txtSerieFactura.Text == "0000")
                    oBe.rgpmc_vserie_factura = "";
                else
                    oBe.rgpmc_vserie_factura = txtSerieFactura.Text;

                if (txtCorrelativoFactura.Text == "00000000")
                    oBe.rgpmc_icorrelativo_factura = 0;
                else
                    oBe.rgpmc_icorrelativo_factura = Convert.ToInt32(txtCorrelativoFactura.Text);

                if (txtSerieBoleta.Text == "0000")
                    oBe.rgpmc_vserie_boleta = "";
                else
                    oBe.rgpmc_vserie_boleta = txtSerieBoleta.Text;

                if (txtCorrelativoBoleta.Text == "00000000")
                    oBe.rgpmc_icorrelativo_boleta = 0;
                else
                    oBe.rgpmc_icorrelativo_boleta = Convert.ToInt32(txtCorrelativoBoleta.Text);

                if (txtSerieNotaCreditoF.Text == "0000")
                    oBe.rgpmc_vserieF_nota_credito = "";
                else
                    oBe.rgpmc_vserieF_nota_credito = txtSerieNotaCreditoF.Text;
                if (txtSerieNotaCreditoB.Text == "0000")
                    oBe.rgpmc_vserieB_nota_credito = "";
                else
                    oBe.rgpmc_vserieB_nota_credito = txtSerieNotaCreditoB.Text;

                if (txtCorrelativoNotaCredito.Text == "00000000")
                    oBe.rgpmc_icorrelativo_nota_credito = 0;
                else
                    oBe.rgpmc_icorrelativo_nota_credito = Convert.ToInt32(txtCorrelativoNotaCredito.Text);

                if (txtSerieNotaDebitoF.Text == "0000")
                    oBe.rgpmc_vserieF_nota_debito = "";
                else
                    oBe.rgpmc_vserieF_nota_debito = txtSerieNotaDebitoF.Text;

                if (txtSerieNotaDebitoB.Text == "0000")
                    oBe.rgpmc_vserieB_nota_debito = "";
                else
                    oBe.rgpmc_vserieB_nota_debito = txtSerieNotaDebitoB.Text;

                if (txtCorrelativoNotaDebito.Text == "00000000")
                    oBe.rgpmc_icorrelativo_nota_debito = 0;
                else
                    oBe.rgpmc_icorrelativo_nota_debito = Convert.ToInt32(txtCorrelativoNotaDebito.Text);

                if (txtCorretivoRC.Text == "00000000")
                    oBe.rgpmc_icorrelativo_recibo_caja = 0;
                else
                    oBe.rgpmc_icorrelativo_recibo_caja = Convert.ToInt32(txtCorretivoRC.Text);

                if (txtSerieRC.Text == "0000")
                    oBe.rgpmc_vserie_recibo_caja = "";
                else
                    oBe.rgpmc_vserie_recibo_caja = txtSerieRC.Text;


                oBe.intUsuario = Valores.intUsuario;

                oBe.rgpmc_vserie_contrato = txtSerieContrato.Text;
                oBe.rgpmc_icorrelativo_contrato = Convert.ToInt32(txtCorrelativoContrato.Text);
                oBe.rgpmc_icorrelativo_solicitud = Convert.ToInt32(txtCorrelativoSolicitud.Text);
                oBe.rgpmc_nmonto_mora = Convert.ToDecimal(txtMora.Text);
                oBe.rgpmc_idias_mora = Convert.ToInt32(txtDiasMora.Text);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.rgpmc_icod_registro_parametro = new BVentas().insertarRegistroParametro(oBe);
                }
                else
                {
                    new BVentas().modificarRegistroParametro(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {

                    this.MiEvento(oBe.rgpmc_icod_registro_parametro);
                    this.Close();
                }
            }
        }



        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmMantePuntoVenta_Load(object sender, EventArgs e)
        {
            carga();
        }

        private void txtPuntoVenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape)
            {
                cerrar_form(sender, e);
            }
        }
        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void FrmMantePuntoVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.MiEvento();
        }
        private void carga()
        {
            BSControls.LoaderLook(lkpsituacion, new BGeneral().listarTablaRegistro(1), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            lkpsituacion.EditValue = 6;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
            {
                lkpsituacion.EditValue = oBe.tabl_iid_situacion;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtCorretivoRC_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}