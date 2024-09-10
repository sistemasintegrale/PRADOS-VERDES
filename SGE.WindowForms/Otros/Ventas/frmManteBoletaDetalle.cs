using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteBoletaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EBoletaDet> lstFacturaDetalle = new List<EBoletaDet>();
        public EBoletaDet obe = new EBoletaDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        public int intClasificacion = 0;
        public Boolean flag_Arrox;
        public Boolean flag_afecto_ivap;
        public decimal valor_IGV;
        public string PorIVAP;
        public decimal PorcentajeIVAP;
        /*Datos IGV*/
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public decimal PorcentajeIGV;
        /**/
        public Boolean flag_exonerado;
        public int remic_icod_remision = 0;

        public BSMaintenanceStatus oState;
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
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteAlmacen.Enabled = Enabled;
                bteProducto.Enabled = Enabled;
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
            setValues();
        }
        public frmManteBoletaDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {
            bteAlmacen.Tag = obe.almac_icod_almacen;
            bteAlmacen.Text = obe.strAlmacen;
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtDescuento.Text = obe.bovd_nporcentaje_descuento_item.ToString();
            txtProducto.Text = obe.strDesProducto;
            txtCantidad.Text = obe.bovd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtPrecio.Text = obe.bovd_nprecio_unitario_item.ToString();
            txtpartNumber.Text = obe.prdc_vpart_number;
            txtDPorImpArroz.Text = obe.bovd_npor_imp_arroz.ToString();

            string[] partes = partes = obe.bolvd_vobservaciones.Split('@');
            txtObservaciones.Lines = partes;

        }

        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                setAlmacen();
        }

        private void setAlmacen()
        {
            var lstAlmacen = new BAlmacen().listarAlmacenes();
            if (lstAlmacen.Count > 0)
            {
                bteAlmacen.Text = lstAlmacen[0].almac_vdescripcion;
                bteAlmacen.Tag = lstAlmacen[0].almac_icod_almacen;
            }
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione almacén");
                }

                if (Convert.ToInt32(bteProducto.Tag) == 0)
                {
                    oBase = bteProducto;
                    throw new ArgumentException("Seleccione producto");
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

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (lstFacturaDetalle.Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList().Count > 0)
                    {
                        oBase = bteProducto;
                        throw new ArgumentException("El producto seleccionado ya se encuentra en el detalle de la factura");
                    }
                }
                if (remic_icod_remision == 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                        {
                            oBase = txtCantidad;
                            throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                        }
                    }
                    else
                    {
                        if (obe.intTipoOperacion != 1)
                        {
                            dblStockDisponible = dblStockDisponible + obe.bovd_ncantidad;
                            if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                            {
                                oBase = txtCantidad;
                                throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                            }
                        }
                        else
                            if (Convert.ToDecimal(txtCantidad.Text) > dblStockDisponible)
                            {
                                oBase = txtCantidad;
                                throw new ArgumentException("La cantidad ingresada sobrepasa el stock disponible del producto");
                            }
                    }
                }

                if (flag_Arrox != flag_afecto_ivap)
                {
                    throw new ArgumentException("El producto no puede ser agregado, ya que no continen IGV");
                }


                PorIVAP = txtDPorImpArroz.Text;
                obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                obe.bovd_iitem_boleta = Convert.ToInt32(txtItem.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.bovd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.bovd_vdescripcion = txtProducto.Text;
                //obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                //obe.bovd_nporcentaje_descuento_item = Convert.ToDecimal(txtDescuento.Text);
                //obe.bovd_nprecio_total_item = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item);
                //obe.bovd_nprecio_total_item = obe.bovd_nprecio_total_item - (obe.bovd_nprecio_total_item * Convert.ToDecimal(String.Format("0.{0}", txtDescuento.Text.Replace(".", ""))));
                //obe.bovd_nmonto_impuesto_item = obe.bovd_nprecio_total_item * Convert.ToDecimal(String.Format("0.{0}", PorIVAP.Replace(".", "")));
                /**/
                obe.strCodProducto = bteProducto.Text;              
                obe.strDesUM = txtUnidadMedida.Text;
                obe.strDesProducto = txtProducto.Text;
                obe.strAlmacen = bteAlmacen.Text;
                obe.dblStockDisponible = dblStockDisponible;
                obe.dblMontoDescuento = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item) - obe.bovd_nprecio_total_item;
                obe.strMoneda = txtMoneda.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.prdc_vpart_number = txtpartNumber.Text;

                //obe.bovd_npor_imp_arroz = Convert.ToDecimal(txtDPorImpArroz.Text);

                //if (Convert.ToDecimal(txtDPorImpArroz.Text) != 0)//tiene IVA
                //{
                //    obe.bovd_nmonto_imp_arroz = Math.Round((Convert.ToDecimal((obe.bovd_nprecio_total_item * Convert.ToDecimal(txtDPorImpArroz.Text)) / (100 + Convert.ToDecimal(txtDPorImpArroz.Text)))), 2, MidpointRounding.ToEven);
                //}
                if (Convert.ToBoolean(flag_afecto_ivap) == true)//tiene IVA
                {
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                    obe.bovd_nprecio_total_item = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item);
                    obe.bovd_npor_imp_arroz = Convert.ToDecimal(txtDPorImpArroz.Text);
                    PorIVAP = txtDPorImpArroz.Text;
                    obe.bovd_nmonto_imp_arroz = Math.Round((Convert.ToDecimal((obe.bovd_nprecio_total_item * Convert.ToDecimal(txtDPorImpArroz.Text)) / (100 + Convert.ToDecimal(txtDPorImpArroz.Text)))), 2, MidpointRounding.ToEven);
                    obe.bovd_nneto_ivap = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item) - obe.bovd_nmonto_imp_arroz;
                }
                else if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                    obe.bovd_nprecio_total_item = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item);
                    obe.bovd_nporcentaje_descuento_item = Convert.ToDecimal(txtPorIGV.Text);
                    PorIGV = txtPorIGV.Text;
                    obe.bovd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((obe.bovd_nprecio_total_item * Convert.ToDecimal(txtPorIGV.Text)) / (100 + Convert.ToDecimal(txtPorIGV.Text)))), 2, MidpointRounding.ToEven);
                    obe.bovd_nneto_igv = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item) - obe.bovd_nmonto_impuesto_item;
                }
                else
                {
                    flag_exonerado = true;
                    obe.bovd_nprecio_unitario_item = Convert.ToDecimal(txtPrecio.Text);
                    obe.bovd_nprecio_total_item = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item);
                    obe.bovd_nneto_exo = (obe.bovd_ncantidad * obe.bovd_nprecio_unitario_item);
                }
                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                obe.bolvd_vobservaciones = Descripci;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obe.strCategoria = Categoria ;
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

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
        }

        private void listarProducto()
        {
            BaseEdit oBase = null;
            try 
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione almacén");
                }
                using (frmListarStockPorAlmacen frm = new frmListarStockPorAlmacen())
                {
                    frm.intAlmacen = Convert.ToInt32(bteAlmacen.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodProducto;
                        txtProducto.Text = frm._Be.strDesProducto;
                        txtUnidadMedida.Text = frm._Be.strDesUM;
                        Categoria = frm._Be.strCategoria;
                        SubCategoria = frm._Be.strSubCategoriaUno;
                        dblStockDisponible = frm._Be.stocc_stock_producto;
                        txtPrecio.Text = Convert.ToDecimal(frm._Be.dblPrecioVenta).ToString();
                        //txtDPorImpArroz.Text = frm._Be.PorcentajeIVAP.ToString();
                        //PorcentajeIVAP = frm._Be.PorcentajeIVAP;
                        //flag_afecto_ivap = frm._Be.AfectoIVAP;
                        /*Datos IVAP*/
                        txtDPorImpArroz.Text = frm._Be.PorcentajeIVAP.ToString();
                        PorcentajeIVAP = frm._Be.PorcentajeIVAP;
                        flag_afecto_ivap = frm._Be.AfectoIVAP;
                        /*Datos IGV*/
                        txtPorIGV.Text = frm._Be.prdc_nporcentaje_igv.ToString();
                        PorcentajeIGV = frm._Be.prdc_nporcentaje_igv;
                        flag_afecto_igv = frm._Be.prdc_afecto_igv;
                    }
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void txtDPorImpArroz_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDPorImpArroz.Text) != PorcentajeIVAP)
            {
                XtraMessageBox.Show("Porcentaje no puede ser diferente");
            }
        }       
    }
}