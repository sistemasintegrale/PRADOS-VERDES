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
    public partial class frmManteFacturaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        public EFacturaDet obe = new EFacturaDet();
        public string Categoria, SubCategoria;
        public decimal dblStockDisponible = 0;
        List<EStock> lstStockProductos = new List<EStock>();
        public int tipo_moneda=0;
        public int remic_icod_remision = 0;
        public Boolean afecto;
        public Boolean flag_Arrox;
        public decimal valor_IGV;
        /*Datos IVAP*/
        public Boolean flag_afecto_ivap;
        public string PorIVAP;
        public decimal PorcentajeIVAP;
        /*Datos IGV*/
        public Boolean flag_afecto_igv;
        public string PorIGV;
        public decimal PorcentajeIGV;
        /**/
        public string CodigoSUNAT;
        public Boolean flag_exonerado;
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
                lkpTipoVenta.Enabled = Enabled;
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
        public frmManteFacturaDetalle()
        {
            InitializeComponent();
        }

        private void setValues()
        {

            bteAlmacen.Tag = obe.almac_icod_almacen;
            bteAlmacen.Text = obe.strAlmacen;
            bteProducto.Tag = obe.prdc_icod_producto;
            bteProducto.Text = obe.strCodProducto;
            txtProducto.Text = obe.strDesProducto;
            txtCantidad.Text = obe.favd_ncantidad.ToString();
            txtUnidadMedida.Text = obe.strDesUM;
            txtpartNumber.Text = obe.prdc_vpart_number;

            lkpTipoVenta.Text = obe.tablc_iid_tipo_venta.ToString();
            txtcaracteristicas.Text = obe.favd_nobservaciones;

            txtprecio.Text = obe.favd_nprecio_unitario_item.ToString();
            txtPorIGV.Text = obe.favd_nporcentaje_descuento_item.ToString();
            txtPrecioVenta.Text = obe.favd_nprecio_total_item.ToString();
            txtDPorImpArroz.Text = obe.favd_npor_imp_arroz.ToString();
            CodigoSUNAT = obe.CodigoSUNAT;

            //string[] partes = partes = obe.favd_nobservaciones.Split('@');
            //txtObservaciones.Lines = partes;

            
            
        }
        public void CargarCombos()
        {

            int index = new BGeneral().listarTablaRegistro(Parametros.intTipoventa).FindIndex(x => x.tarec_iid_tabla_registro == 266);
            BSControls.LoaderLook(lkpTipoVenta, new BGeneral().listarTablaRegistro(Parametros.intTipoventa).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpTipoVenta.ItemIndex = index;
            
         
        }
        private void frmManteFacturaDetalle_Load(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
                setAlmacen();


            lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, Convert.ToInt32(bteAlmacen.Tag), "", "");

            CargarCombos();

        }

        private void setAlmacen()
        {
            var lstAlmacen = new BAlmacen().listarAlmacenes().Where(x=> x.almac_icod_almacen == 119).ToList();
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

                if (Convert.ToDecimal(txtPrecioVenta.Text) <= 0)
                {
                    oBase = txtPrecioVenta;
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
                            dblStockDisponible = dblStockDisponible + obe.favd_ncantidad;
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
                //if (flag_Arrox != flag_afecto_ivap)
                //{
                //    throw new ArgumentException("El producto no puede ser agregado, ya que no continen IGV");
                //}

                obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                obe.favd_iitem_factura = Convert.ToInt32(txtItem.Text);
                obe.prdc_icod_producto = Convert.ToInt32(bteProducto.Tag);
                obe.favd_ncantidad = Convert.ToDecimal(txtCantidad.Text);
                obe.favd_vdescripcion = txtProducto.Text;
                obe.strTipoVenta = lkpTipoVenta.Text;
                obe.tablc_iid_tipo_venta = Convert.ToInt32(lkpTipoVenta.EditValue);               
                /**/
                obe.strCodProducto = bteProducto.Text;              
                obe.strDesUM = txtUnidadMedida.Text;
                //obe.CodigoSUNAT = CodigoSUNAT;
                obe.strDesProducto = txtProducto.Text;
                obe.strAlmacen = bteAlmacen.Text;
                obe.dblStockDisponible = dblStockDisponible;
                obe.dblMontoDescuento = (obe.favd_ncantidad * obe.favd_nprecio_unitario_item) - obe.favd_nprecio_total_item;
                obe.strMoneda = txtMoneda.Text;
                obe.intUsuario = Valores.intUsuario;
                obe.strPc = WindowsIdentity.GetCurrent().Name;
                obe.flagPlanilla = true;
                obe.prdc_vpart_number = txtpartNumber.Text;
                obe.favd_nobservaciones = txtcaracteristicas.Text;


                if (Convert.ToBoolean(flag_afecto_ivap) == true)//tiene IVA
                {
                    obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.favd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.favd_npor_imp_arroz = Convert.ToDecimal(txtDPorImpArroz.Text);
                    PorIVAP = txtDPorImpArroz.Text;
                    obe.favd_nmonto_imp_arroz = Math.Round((Convert.ToDecimal((obe.favd_nprecio_total_item * Convert.ToDecimal(txtDPorImpArroz.Text)) / (100 + Convert.ToDecimal(txtDPorImpArroz.Text)))), 2, MidpointRounding.ToEven);
                    obe.favd_nneto_ivap = Convert.ToDecimal(txtPrecioVenta.Text) - obe.favd_nmonto_imp_arroz;
                }
                else if (Convert.ToBoolean(flag_afecto_igv) == true)//tiene IGV
                {
                    obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.favd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.favd_nporcentaje_descuento_item = Convert.ToDecimal(txtPorIGV.Text);
                    PorIGV = txtPorIGV.Text;
                    obe.favd_nmonto_impuesto_item = Math.Round((Convert.ToDecimal((obe.favd_nprecio_total_item * Convert.ToDecimal(txtPorIGV.Text)) / (100 + Convert.ToDecimal(txtPorIGV.Text)))), 2, MidpointRounding.ToEven);
                    obe.favd_nneto_igv = Convert.ToDecimal(txtPrecioVenta.Text) - obe.favd_nmonto_impuesto_item;
                }
                else
                {
                    flag_exonerado = true;
                    obe.favd_nprecio_unitario_item = Convert.ToDecimal(txtprecio.Text);
                    obe.favd_nprecio_total_item = Convert.ToDecimal(txtPrecioVenta.Text);
                    obe.favd_nneto_exo = Convert.ToDecimal(txtPrecioVenta.Text);
                   
                }


                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtcaracteristicas.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] ;
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                obe.favd_nobservaciones = Descripci;

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
                    lstStockProductos = new BAlmacen().listarStockPorAlmacenOptimizado(Parametros.intEjercicio, Convert.ToInt32(bteAlmacen.Tag), "", "");
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
                    frm.afecto = afecto;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteProducto.Tag = frm._Be.prdc_icod_producto;
                        bteProducto.Text = frm._Be.strCodProducto;
                        txtProducto.Text = frm._Be.strDesProducto;
                        txtUnidadMedida.Text = frm._Be.strCodUM;
                        CodigoSUNAT = frm._Be.CodigoSUNAT;
                        Categoria = frm._Be.strCategoria;
                        SubCategoria = frm._Be.strSubCategoriaUno;
                        dblStockDisponible = frm._Be.stocc_stock_producto;
                        /*Datos IVAP*/
                        txtDPorImpArroz.Text = frm._Be.PorcentajeIVAP.ToString();
                        PorcentajeIVAP = frm._Be.PorcentajeIVAP;
                        flag_afecto_ivap = frm._Be.AfectoIVAP;
                        /*Datos IGV*/
                        txtPorIGV.Text = frm._Be.prdc_nporcentaje_igv.ToString();
                        PorcentajeIGV = frm._Be.prdc_nporcentaje_igv;
                        flag_afecto_igv = frm._Be.prdc_afecto_igv;
                        txtPrecioVenta.Text = Convert.ToDecimal(frm._Be.dblPrecioVenta).ToString();
                        if (tipo_moneda == 3)
                        {
                            txtprecio.Text = frm._Be.prdc_nPrecio_soles.ToString();
                            txtDescuento.Text = frm._Be.prdc_nPor_Descuento.ToString();
                            txtPrecioVenta.Text = frm._Be.prdc_nPrecio_venta.ToString();                          
                        }else
                            if (tipo_moneda ==4)
                            {
                            txtprecio.Text = frm._Be.prdc_nPrecio_dolares.ToString();
                            txtDescuento.Text = frm._Be.prdc_nPor_Descuento.ToString();
                            txtPrecioVenta.Text = frm._Be.prdc_nPrecio_venta_Dol.ToString();
                            }
                            
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

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtprecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
        private void Totalizar()
        {
            
            decimal ddescuento = 0;
            ddescuento = ((Convert.ToDecimal(txtprecio.Text) * Convert.ToDecimal(txtDescuento.Text)) / 100);

            txtPrecioVenta.Text = ((Convert.ToDecimal(txtprecio.Text) - ddescuento) * Convert.ToDecimal(txtCantidad.Text)).ToString();
        }
        private void CalcularPrecio()
        {
         
                List<EListaPrecio> lstListaPrecios = new List<EListaPrecio>();
                lstListaPrecios = new BVentas().listarListaPrecio().Where(x => x.prdc_icod_producto == Convert.ToInt32(bteProducto.Tag)).ToList();
                txtprecio.Text = "0.000000";
                lstListaPrecios.ForEach(x =>
                {
                    decimal Cantidad = 0;
                    /*Cuando No tiene Detalle de Rango Se pone el Mismo Precio*/
                    Cantidad = Convert.ToDecimal(txtCantidad.Text);
                    if (x.lprecc_nmonto_unitario != 0 && x.lprecc_indicador_rango == false)
                    {
                        txtprecio.Text = x.lprecc_nmonto_unitario.ToString();
                    }
                    if (x.lprecc_nmonto_unitario == 0 && x.lprecc_indicador_rango == true)
                    {
                        List<EListaPrecioDetalle> lstListaPreciosDetalle = new List<EListaPrecioDetalle>();
                        lstListaPreciosDetalle = new BVentas().listarListaPrecioDetalle(x.lprecc_icod_precio);
                        lstListaPreciosDetalle.ForEach(xd =>
                        {

                            if (Cantidad >= xd.lprecd_icantidad_inicial && Cantidad < xd.lprecd_icantidad_final)
                            {
                                txtprecio.Text = xd.lprecd_nmonto_unitario.ToString();
                            }

                        });
                    }

                });
                       
        }
        private void txtDescuento_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void bteProducto_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                //List<EStock> MlistAux = new List<EStock>();
                //MlistAux = lstStockProductos.Where(ob => ob.strCodProducto.Trim() == bteProducto.Text.Trim()).ToList();
                //if (MlistAux.Count == 1)
                //{
                //    bteProducto.Tag = MlistAux[0].prdc_icod_producto;
                //    bteProducto.Text = MlistAux[0].strCodProducto;
                //    txtProducto.Text = MlistAux[0].strDesProducto;
                //    txtUnidadMedida.Text = MlistAux[0].strDesUM;
                //    Categoria = MlistAux[0].strCategoria;
                //    SubCategoria = MlistAux[0].strSubCategoriaUno;
                //    dblStockDisponible = MlistAux[0].stocc_stock_producto;
                //    txtPrecioVenta.Text = Convert.ToDecimal(MlistAux[0].dblPrecioVenta).ToString();
                //    if (tipo_moneda == 3)
                //    {
                //        txtprecio.Text = MlistAux[0].prdc_nPrecio_soles.ToString();
                //        txtDescuento.Text = MlistAux[0].prdc_nPor_Descuento.ToString();
                //        txtPrecioVenta.Text = MlistAux[0].prdc_nPrecio_venta.ToString();

                //    }
                //    else
                //        txtPrecioVenta.Text = MlistAux[0].prdc_nPrecio_dolares.ToString();

                //}
                //else
                //{
                //    bteProducto.Tag = 0;
                //    txtProducto.Text = "";
                //    txtCantidad.Text = "0";
                //    txtprecio.Text = "0";
                //    txtDescuento.Text = "0";
                //    txtPrecioVenta.Text = "0";
                //}
            }
        }

        private void bteAlmacen_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkpTipoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtDPorImpArroz_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtDPorImpArroz.Text) != PorcentajeIVAP)
            {
                XtraMessageBox.Show("Porcentaje no puede ser diferente");
            }
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
        }

       
    }
}