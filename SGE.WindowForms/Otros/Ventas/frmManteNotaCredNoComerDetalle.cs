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
using System.Linq;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.Security.Principal;
using SGE.WindowForms.Otros.Operaciones;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteNotaCredNoComerDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaCreditoDet> lstDetalle = new List<ENotaCreditoDet>();
        
        public ENotaCreditoDet oBe = new ENotaCreditoDet();
        public decimal IGV;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteNotaCredNoComerDetalle()
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
            
        }      

        public void setValues()
        {

            //txtCantidad.Text = oBe.dcrec_nmonto_item.ToString();
             
            txtObservaciones.Text = oBe.dcrec_vdescripcion;
            txtCantidad.Text = oBe.dcrec_ncantidad_producto.ToString();
            txtMonto.Text = oBe.dcrec_nmonto_unitario.ToString();
            txtPrecioTotal.Text = oBe.dcrec_nmonto_total.ToString();
            //lkpMoneda.EditValue = oBeDetFav.;
            //btePersonal.Tag = oBeDetFav.otss_icod_personal;
            //btePersonal.Text = oBeDetFav.strPersonal;
            //txtAreaPersonal.Text = oBeDetFav.otss_area_personal;
            //dtFecha.EditValue = oBeDetFav.otss_fecha_servicio;
            //txtProductividad.Text = oBeDetFav.otss_nporc_productividad.ToString();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {               
               

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese el Monto");
                }

                oBe.dcrec_inro_item = Convert.ToInt16(txtItem.Text);
               // oBe.prdc_icod_producto = null;
                oBe.dcrec_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);                
                oBe.dcrec_nmonto_unitario = Convert.ToDecimal(txtMonto.Text);
                oBe.dcrec_nmonto_item = oBe.dcrec_nmonto_unitario;
                oBe.dcrec_nporcentaje_impuesto = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.dcrec_nmonto_total = Convert.ToDecimal(txtPrecioTotal.Text);
                if (IGV == 0)
                {
                    oBe.dcrec_nmonto_impuesto = 0;
                }
                else
                {
                    //oBe.dcrec_nmonto_impuesto = Math.Round((Convert.ToDecimal((oBe.dcrec_nmonto_item * Convert.ToDecimal(Parametros.strPorcIGV)) / (100 + Convert.ToDecimal(Parametros.strPorcIGV)))), 2, MidpointRounding.ToEven);
                    oBe.dcrec_nmonto_impuesto = Math.Round(oBe.dcrec_nmonto_unitario * (Convert.ToDecimal(Parametros.strPorcIGV) / 100), 2);
                }
                
                oBe.dcrec_nneto_igv = (Math.Round((oBe.dcrec_ncantidad_producto * oBe.dcrec_nmonto_unitario), 2)) - oBe.dcrec_nmonto_impuesto;
                //oBe.intClasificacion = null;                

                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                oBe.dcrec_vdescripcion = Descripci;
                oBe.strDesProductoPresentar = Descripci;

                #region Factura Electronica Detalle
                oBe.NumeroOrdenItem = oBe.dcrec_inro_item;
                oBe.cantidad = oBe.dcrec_ncantidad_producto;
                oBe.unidadMedida = "ZZ";
                oBe.ValorVentaItem = oBe.dcrec_nmonto_total;
                oBe.CodMotivoDescuentoItem = 0;
                oBe.FactorDescuentoItem = 0;
                oBe.DescuentoItem = 0;
                oBe.BaseDescuentotem = 0;
                oBe.CodMotivoCargoItem = 0;
                oBe.FactorCargoItem = 0;
                oBe.MontoCargoItem = 0;
                oBe.BaseCargoItem = 0;
                oBe.MontoTotalImpuestosItem = oBe.dcrec_nmonto_impuesto;
                oBe.MontoImpuestoIgvItem = oBe.dcrec_nmonto_impuesto;

                if (oBe.dcrec_nmonto_impuesto == 0)
                {
                    oBe.MontoInafectoItem = oBe.dcrec_nmonto_total;
                    oBe.MontoAfectoImpuestoIgv = 0;
                }
                else
                {

                    oBe.MontoInafectoItem = 0;
                    oBe.MontoAfectoImpuestoIgv = oBe.dcrec_nneto_igv;
                }
                oBe.PorcentajeIGVItem = Convert.ToDecimal(Parametros.strPorcIGV);
                oBe.MontoImpuestoISCItem = 0;
                oBe.MontoAfectoImpuestoIsc = 0;
                oBe.PorcentajeISCtem = 0;
                //oBe.MontoImpuestoIVAPtem = oBe.favd_nmonto_imp_arroz;
                //oBe.MontoAfectoImpuestoIVAPItem = oBe.favd_nneto_ivap;
                //oBe.PorcentajeIVAPItem = oBe.favd_npor_imp_arroz;
                oBe.descripcion = oBe.dcrec_vdescripcion;
                oBe.codigoItem = "SERV0" + oBe.dcrec_inro_item;
                oBe.ObservacionesItem = "";
                oBe.ValorUnitarioItem = oBe.dcrec_nmonto_unitario;
                if (oBe.dcrec_nmonto_impuesto == 0)
                {
                    oBe.tipoImpuesto = "30";
                }
                else
                {
                    oBe.tipoImpuesto = "10";
                }
                #endregion

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

        private void listarPersonal()
        {
            frmListarPersonal frm = new frmListarPersonal();
            frm.flag_personal_all = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btePersonal.Tag = frm._Be.perc_icod_personal;
                btePersonal.Text = frm._Be.perc_vapellidos_nombres;
                txtAreaPersonal.Text = frm._Be.strArea;
            }
        }

        private void listarServicios()
        {
            BaseEdit oBase = null;
            try
            {
               

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

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarServicios();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

       

        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarServicios();
        }

        private void btePersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }

        private void btePersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarPersonal();
        }

        private void calcularProductividad()
        {
            decimal porc_productividad = Convert.ToDecimal(txtProductividad.Text.Substring(0, 5));
            txtMtoProductividad.Text = (Math.Round(Convert.ToDecimal(txtCantidad.Text) * porc_productividad / 100, 2)).ToString();
        }

        private void txtProductividad_EditValueChanged(object sender, EventArgs e)
        {
           // calcularProductividad();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            //calcularProductividad();
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void txtPrecioTotal_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();

        }
        private void Totalizar()

        {

            txtPrecioTotal.Text = (Convert.ToDecimal(txtMonto.Text) * Convert.ToInt32(txtCantidad.Text)).ToString();
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
    }
}