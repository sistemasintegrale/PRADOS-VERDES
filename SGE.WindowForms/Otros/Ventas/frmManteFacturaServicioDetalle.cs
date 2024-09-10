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
    public partial class frmManteFacturaServicioDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        public string Categoria, SubCategoria;
        public EFacturaDet obe = new EFacturaDet();
        public decimal IGV;
        public int situacion = 0;
        public int modificar = 1;
        public int nuevo = 0;
        public List<EContratoCuotas> lstContrato = new List<EContratoCuotas>();
        public int cntc_icod_contrato;

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteFacturaServicioDetalle()
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
            setValues();
        }

        private void setValues()
        {

            txtDescripcion1.Text = obe.favd_vdescripcion;
            txtCantidad.Text = obe.favd_ncantidad.ToString();
            txtPrecio.Text = obe.favd_nprecio_unitario_item.ToString();
            //string[] partes = partes = obe.favd_nobservaciones.Split();
            txtObservaciones.Text = obe.favd_nobservaciones;
            txtPrecioVenta.Text = Convert.ToString(obe.favd_nprecio_total_item);
            lpkTipoItem.EditValue = obe.favd_iicod_tipo_pago;
            situacion = modificar;
            #region Factura Electronica Detalle
            obe.NumeroOrdenItem = obe.favd_iitem_factura;
            obe.cantidad = obe.favd_ncantidad;
            obe.unidadMedida = "ZZ";
            obe.ValorVentaItem = obe.favd_nprecio_total_item;
            obe.CodMotivoDescuentoItem = 0;
            obe.FactorDescuentoItem = 0;
            obe.DescuentoItem = 0;
            obe.BaseDescuentotem = 0;
            obe.CodMotivoCargoItem = 0;
            obe.FactorCargoItem = 0;
            obe.MontoCargoItem = 0;
            obe.BaseCargoItem = 0;
            obe.MontoTotalImpuestosItem = obe.favd_nmonto_impuesto_item;
            obe.MontoImpuestoIgvItem = obe.favd_nmonto_impuesto_item;

            if (obe.favd_nmonto_impuesto_item == 0)
            {
                obe.MontoInafectoItem = obe.favd_nprecio_total_item;
                obe.MontoAfectoImpuestoIgv = 0;
            }
            else
            {

                obe.MontoInafectoItem = 0;
                obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
            }
            obe.PorcentajeIGVItem = obe.favd_nporcentaje_descuento_item;
            obe.MontoImpuestoISCItem = 0;
            obe.MontoAfectoImpuestoIsc = 0;
            obe.PorcentajeISCtem = 0;
            obe.MontoImpuestoIVAPtem = obe.favd_nmonto_imp_arroz;
            obe.MontoAfectoImpuestoIVAPItem = obe.favd_nneto_ivap;
            obe.PorcentajeIVAPItem = obe.favd_npor_imp_arroz;
            obe.descripcion = obe.favd_vdescripcion + " " + obe.favd_nobservaciones.Trim();
            obe.codigoItem = "SERV0" + obe.favd_iitem_factura;
            obe.ObservacionesItem = "";
            obe.ValorUnitarioItem = (((obe.MontoInafectoItem + obe.MontoAfectoImpuestoIgv) + obe.favd_nmonto_impuesto_item) / obe.favd_ncantidad);
            if (obe.favd_nmonto_impuesto_item == 0)
            {
                obe.tipoImpuesto = "30";
            }
            else
            {
                obe.tipoImpuesto = "10";
            }

            lblMonto.Text = lstContrato.Sum(x => x.monto_pagar) == 0 ? "": string.Format("Monto : {0}", Math.Round(lstContrato.Sum(x => x.monto_pagar)));
            #endregion

        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion1.Text))
                {
                    oBase = txtDescripcion1;
                    throw new ArgumentException("Ingrese la Descripción del Servicio");
                }
                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Convert.ToDecimal(txtPrecio.Text) <= 0)
                {
                    oBase = txtPrecio;
                    throw new ArgumentException("El precio unitario debe ser mayor a 0");
                }

                obe.almac_icod_almacen = null;
                obe.favd_iitem_factura = Convert.ToInt32(txtItem.Text);
                obe.favd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.favd_vdescripcion = txtDescripcion1.Text;
                obe.favd_nobservaciones = txtObservaciones.Text;
                obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                obe.favd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                obe.favd_iicod_tipo_pago = Convert.ToInt32(lpkTipoItem.EditValue);
                if (IGV == 0)
                {
                    obe.favd_nmonto_impuesto_item = 0;
                    //obe.favd_nporcentaje_descuento_item = 0;
                }
                else
                {
                    obe.favd_nmonto_impuesto_item = Math.Round(obe.favd_nprecio_unitario_item * (Convert.ToDecimal(Parametros.strPorcIGV) / 100), 2);
                }
                obe.favd_nporcentaje_descuento_item = Convert.ToDecimal(Parametros.strPorcIGV);


                obe.strMoneda = lkpMoneda.Text;
                obe.flagPlanilla = true;

                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.strTipoServicio = lpkTipoItem.Text;


                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i];
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                obe.favd_nobservaciones = Descripci;

                #region Factura Electronica Detalle
                obe.NumeroOrdenItem = obe.favd_iitem_factura;
                obe.cantidad = obe.favd_ncantidad;
                obe.unidadMedida = "ZZ";
                obe.ValorVentaItem = obe.favd_nprecio_total_item;
                obe.CodMotivoDescuentoItem = 0;
                obe.FactorDescuentoItem = 0;
                obe.DescuentoItem = 0;
                obe.BaseDescuentotem = 0;
                obe.CodMotivoCargoItem = 0;
                obe.FactorCargoItem = 0;
                obe.MontoCargoItem = 0;
                obe.BaseCargoItem = 0;
                obe.MontoTotalImpuestosItem = obe.favd_nmonto_impuesto_item;
                obe.MontoImpuestoIgvItem = obe.favd_nmonto_impuesto_item;

                if (obe.favd_nmonto_impuesto_item == 0)
                {
                    obe.MontoInafectoItem = obe.favd_nprecio_total_item;
                    obe.MontoAfectoImpuestoIgv = 0;
                }
                else
                {
                    obe.MontoInafectoItem = 0;
                    obe.MontoAfectoImpuestoIgv = obe.favd_nneto_igv;
                }
                obe.PorcentajeIGVItem = obe.favd_nporcentaje_descuento_item;
                obe.MontoImpuestoISCItem = 0;
                obe.MontoAfectoImpuestoIsc = 0;
                obe.PorcentajeISCtem = 0;
                obe.MontoImpuestoIVAPtem = obe.favd_nmonto_imp_arroz;
                obe.MontoAfectoImpuestoIVAPItem = obe.favd_nneto_ivap;
                obe.PorcentajeIVAPItem = obe.favd_npor_imp_arroz;
                obe.descripcion = obe.favd_vdescripcion + " " + obe.favd_nobservaciones.Trim();
                obe.codigoItem = "SERV0" + obe.favd_iitem_factura;
                obe.ObservacionesItem = "";
                obe.ValorUnitarioItem = obe.favd_nprecio_unitario_item;
                obe.PrecioVentaUnitarioItem = (((obe.MontoInafectoItem + obe.MontoAfectoImpuestoIgv) + obe.favd_nmonto_impuesto_item) / obe.favd_ncantidad);
                if (obe.favd_nmonto_impuesto_item == 0)
                {
                    obe.tipoImpuesto = "30";
                }
                else
                {
                    obe.tipoImpuesto = "10";
                }
                #endregion

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strCategoria = Categoria;
                    obe.strSubCategoriaUno = SubCategoria;
                    obe.intTipoOperacion = 1;
                    lstFacturaDetalle.Add(obe);
                }
                else
                {
                    if (obe.intTipoOperacion != 1)
                        obe.intTipoOperacion = 2;
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

        private void frmManteOTSSolicitado_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            BSControls.LoaderLook(lpkTipoItem, new BGeneral().listarTablaVentaDet(23), "tabvd_vdescripcion", "tabvd_icorrelativo_venta_det", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                setValues();
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
            txtMtoProductividad.Text = (Math.Round(Convert.ToDecimal(txtPrecio.Text) * porc_productividad / 100, 2)).ToString();
            txtPrecioVenta.Text = (Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }

        private void txtProductividad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            calcularProductividad();
        }

        private void txtItem_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtItem_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtDescripcion1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void btnCuotas_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmListarCuota frm = new FrmListarCuota())
                {
                    frm.situacion = situacion;
                    frm.lstContrato = lstContrato.OrderBy(x => x.strTipoCredito).ToList();
                    frm.cntc_icod_contrato = cntc_icod_contrato;
                    frm.flag_multiple = true;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstContrato = frm.lstContrato;
                        lblMonto.Text = string.Format("Monto : {0}",frm.txtMontoTotal.Text);
                        txtPrecio.Text = Convert.ToDecimal(frm.txtMontoTotal.Text).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lpkTipoItem_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lpkTipoItem.EditValue) == 1)
            {
                btnCuotas.Enabled = true;
                btnCuotas.Text = "Varios";
            }
            else
            {

                btnCuotas.Enabled = false;
                btnCuotas.Text = "";
            }
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }
    }
}