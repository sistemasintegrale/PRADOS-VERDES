using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using SGE.Entity;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteCajaTurno : DevExpress.XtraEditors.XtraForm
    {
        public List<ECaja> lista = new List<ECaja>();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteCajaTurno));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public FrmManteCajaTurno()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public ECaja oBe=new ECaja();

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
            txtCaja.Enabled = !Enabled;
            txtSerieFactura.Enabled = !Enabled;
            txtCorrelativoFactura.Enabled = !Enabled;
            txtSerieBoleta.Enabled = !Enabled;
            txtCorrelativoBoleta.Enabled = !Enabled;
            txtSerieNotaCredito.Enabled = !Enabled;
            txtCorrelativoNotaCredito.Enabled = !Enabled;
            TxtCodCaja.Enabled = !Enabled;
            lkpPuntoVenta.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                TxtCodCaja.Enabled = !Enabled;
                txtCaja.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerieFactura.Enabled = !Enabled;
                txtCorrelativoFactura.Enabled = !Enabled;
                txtSerieBoleta.Enabled = !Enabled;
                txtCorrelativoBoleta.Enabled = !Enabled;
                txtSerieNotaCredito.Enabled = !Enabled;
                txtCorrelativoNotaCredito.Enabled = !Enabled;
                txtCaja.Enabled = !Enabled;
                TxtCodCaja.Enabled = Enabled;
                lkpPuntoVenta.Enabled = Enabled;
            }
            txtCaja.Focus();
        }
        public void setValues()
        {
            txtCaja.Text = Convert.ToString(oBe.cajac_vdescripcion);
            txtSerieFactura.Text = oBe.cajac_inro_serie_factura.ToString();
            txtCorrelativoFactura.Text = oBe.cajac_icorrelativo_factura.ToString();
            txtSerieBoleta.Text = oBe.cajac_inro_serie_boleta.ToString();
            txtCorrelativoBoleta.Text = oBe.cajac_icorrelativo_boleta.ToString();
            txtSerieNotaCredito.Text = oBe.cajac_inro_serie_nota_credito.ToString();
            txtCorrelativoNotaCredito.Text = oBe.cajac_icorrelativo_nota_credito.ToString();
            TxtCodCaja.Text = oBe.cajac_vcod_caja.ToString();           
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
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (string.IsNullOrEmpty(txtCaja.Text))
                    {
                        oBase = txtCaja;
                        throw new ArgumentException("Ingresar Descripción");
                    }
                    if (lkpPuntoVenta.EditValue == null)
                    {
                        oBase = lkpPuntoVenta;
                        throw new ArgumentException("Ingresar Punto Venta");
                    }
                    if (lista.Count(ob => ob.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue) && ob.cajac_vcod_caja == Convert.ToInt32(TxtCodCaja.Text)) > 0)
                    {
                        oBase = TxtCodCaja;
                        throw new ArgumentException("El código Existe");
                    }
                }
                oBe.cajac_vdescripcion = txtCaja.Text;
                oBe.cajac_vcod_caja = Convert.ToInt32(TxtCodCaja.Text);
                oBe.puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue);

                if (txtSerieFactura.Text == "000")
                    oBe.cajac_inro_serie_factura = 0;
                else
                    oBe.cajac_inro_serie_factura = Convert.ToInt32(txtSerieFactura.Text);

                if (txtSerieBoleta.Text == "000")
                    oBe.cajac_inro_serie_boleta = 0;
                else
                    oBe.cajac_inro_serie_boleta = Convert.ToInt32(txtSerieBoleta.Text);

                if (txtSerieNotaCredito.Text == "000")
                    oBe.cajac_inro_serie_nota_credito = 0;
                else
                    oBe.cajac_inro_serie_nota_credito = Convert.ToInt32(txtSerieNotaCredito.Text);

                if (txtCorrelativoFactura.Text == "000000")
                    oBe.cajac_icorrelativo_factura = 0;
                else
                    oBe.cajac_icorrelativo_factura = Convert.ToInt32(txtCorrelativoFactura.Text);

                if (txtCorrelativoBoleta.Text == "000000")
                    oBe.cajac_icorrelativo_boleta = 0;
                else
                    oBe.cajac_icorrelativo_boleta = Convert.ToInt32(txtCorrelativoBoleta.Text);

                if (txtCorrelativoNotaCredito.Text == "000000")
                    oBe.cajac_icorrelativo_nota_credito = 0;
                else
                    oBe.cajac_icorrelativo_nota_credito = Convert.ToInt32(txtCorrelativoNotaCredito.Text);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.cajac_icod_caja = new BVentas().insertarCaja(oBe);
                }
                else
                {
                    new BVentas().modificarCaja(oBe);
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
                  
                    this.MiEvento(oBe.cajac_icod_caja);
                    this.Close();
                }
            }
        }
        private void FrmManteCaja_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtCaja_KeyUp(object sender, KeyEventArgs e)
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

        private void FrmManteCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.MiEvento();    
        }
        private void cargar()
        {
            //BSControls.LoaderLook(lkpPuntoVenta, new BVentas().listarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
            TxtCodCaja.Text = (new BVentas().ValidarCodigoCaja()).ToString();
            txtCaja.Text = "CAJA ";
        }

        private void lkpPuntoVenta_EditValueChanged(object sender, EventArgs e)
        {
            int contador=0;
            if (lista.Count > 0 && lista.Where(ob=>ob.puvec_icod_punto_venta==Convert.ToInt32(lkpPuntoVenta.EditValue)).Count()>0)
            {
                contador = lista.Where(ob => ob.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue)).Max(o => Convert.ToInt32(o.cajac_vcod_caja));
            }
            else
                contador = 0;
             TxtCodCaja.Text = (contador + 1).ToString();

        }
    }
}