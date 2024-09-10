using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Operaciones;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Compras;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteFactura : DevExpress.XtraEditors.XtraForm
    {
        public EFacturaCab oBe = new EFacturaCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EFacturaDet> lstFacturaDetalle = new List<EFacturaDet>();
        List<EFacturaDet> lstDelete = new List<EFacturaDet>();
        private decimal? IGV;
        string strCodCliente = "";
        int Numero_dias_vencimiento_cliente = 0;
        public int TipodeFactura=0;
        public string PorIVAP;
        public string PorIGV;
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
        public void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;
                //ChkIndArroz.Enabled = false;
                BtnGuiaRemision.Enabled = false;               
            }
        }

        public void setValues()
        {
            //cbIncluyeIGV.Checked = oBe.favc_bincluye_igv;
            txtSerie.Text = oBe.favc_vnumero_factura.Substring(0, 4);
            txtNumero.Text = oBe.favc_vnumero_factura.Substring(4, 8);
            dteFecha.EditValue = oBe.favc_sfecha_factura;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            bteCliente.Tag = oBe.favc_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtRUC.Text = oBe.favc_vruc;
            txtDireccion.Text = oBe.favc_vdireccion_cliente;
            txtTelefono.Text = oBe.strTelefonoCliente;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            //txtPorcentIGV.Text = oBe.favc_npor_imp_igv.ToString();
            lkpFormaPago.EditValue = oBe.tablc_iid_forma_pago;
            txtMontoNeto.Text = oBe.favc_nmonto_neto.ToString();
           // txtMontoIGV.Text = oBe.favc_nmonto_imp.ToString();
            txtMontoTotal.Text = oBe.favc_nmonto_total.ToString();
            dteVencimiento.EditValue = oBe.favc_sfecha_vencim_factura;
            TipodeFactura = Convert.ToInt32(oBe.favc_tipo_factura);
            BtnGuiaRemision.Tag = oBe.remic_icod_remision;
            BtnGuiaRemision.Text = oBe.remic_vnumero_remision;
            bteVendedor.Tag = oBe.vendc_icod_vendedor;
            bteVendedor.Text = oBe.NomVendedor;
            //ChkIndArroz.Checked = oBe.favc_bind_arroz;
            PorIVAP = oBe.favc_npor_imp_ivap.ToString();
            PorIGV = oBe.favc_npor_imp_igv.ToString();
            //PorIVAP = oBe.favc_npor_imp_ivap.ToString();
            string[] partes = partes = oBe.favc_vobservacion.Split('@');
            txtObservaciones.Lines = partes;

            lstFacturaDetalle = new BVentas().listarFacturaServicioDetalle(oBe.favc_icod_factura);
            Numero_dias_vencimiento_cliente = oBe.cliec_nnumero_dias;

            if (oBe.remic_icod_remision != 0)
            {
                nuevoToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem1.Enabled = false;
                //nuevoServicioToolStripMenuItem.Enabled = false;
            }
            viewFactura.RefreshData();
        }

        public FrmManteFactura()
        {
            InitializeComponent();           
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;   
       
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            SetCancel();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
                setFecha(dteVencimiento);
                getNroDoc();
            }
            

           
        }
        public void CargarControles()
        {         
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionDocumento), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaFormaPago).Where(x => x.tarec_iid_tabla_registro == 116 || x.tarec_iid_tabla_registro == 117
                ).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            
        }

        private void getNroDoc()
        {
            try
            {
                //var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocFacturaVenta);/*Falta Arreglar Por Modificar Planilla*/
                var lst = new BVentas().getCorrelativoRP(1);//Icod del Punto de venta Central
                txtSerie.Text = lst[0].rgpmc_vserie_factura;
                txtNumero.Text = (Convert.ToInt32(lst[0].rgpmc_icorrelativo_factura) + 1).ToString();
                //throw new ArgumentException("El N° de Serie de boletas no se encuentra registrado. \nNota: Registrar N° de Serie en la opción REGISTRO DE TIPOS DE DOCUMENTOS");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmManteFacturaDetalleServicio frm = new frmManteFacturaDetalleServicio())
            {


                frm.lstFacturaDetalle = lstFacturaDetalle;
                //frm.txtMoneda.Text = lkpMoneda.Text;
                frm.SetInsert();
                TipodeFactura = 1;

                frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    grdFactura.DataSource = lstFacturaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotal();
                }
            }

        }

        private void nuevoServicio()
        {
            //using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            //{
            //    frm.SetInsert();
            //    frm.lstFacturaDetalle = lstFacturaDetalle;
            //    frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
            //    frm.txtItem.Text = (lstFacturaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstFacturaDetalle.Count + 1);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstFacturaDetalle = frm.lstFacturaDetalle;
            //        viewFactura.RefreshData();
            //        viewFactura.MoveLast();
            //        setTotal();

            //    }
            //}
        }

        private void modificarServicio()
        {
            EFacturaDet obe = (EFacturaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteFacturaServicioDetalle frm = new frmManteFacturaServicioDetalle())
            {
                frm.obe = obe;
                frm.lstFacturaDetalle = lstFacturaDetalle;
                frm.SetModify();
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstFacturaDetalle = frm.lstFacturaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotal();
                }
            }
        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtDireccion.Text = frm._Be.cliec_vdireccion_cliente;
                        txtRUC.Text = frm._Be.cliec_cruc;
                        txtTelefono.Text = frm._Be.cliec_vnro_telefono;
                        strCodCliente = frm._Be.cliec_vcod_cliente;
                        Numero_dias_vencimiento_cliente = Convert.ToInt32(frm._Be.cliec_nnumero_dias);
                        if (frm._Be.cliec_bcredito == true)
                        {
                            lkpFormaPago.EditValue = 117;//forma de pago CREDITO
                            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(Numero_dias_vencimiento_cliente));
                        }
                        else
                        {
                            Numero_dias_vencimiento_cliente = 0;
                            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(Numero_dias_vencimiento_cliente));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setCliente(int intCliente)
        {
            try
            {
                var _Be = new BVentas().ListarCliente().Where(x => x.cliec_icod_cliente == intCliente).ToList()[0];
                bteCliente.Tag = _Be.cliec_icod_cliente;
                bteCliente.Text = _Be.cliec_vnombre_cliente;
                txtDireccion.Text = _Be.cliec_vdireccion_cliente;
                txtRUC.Text = _Be.cliec_cruc;
                txtTelefono.Text = _Be.cliec_vnro_telefono;
                strCodCliente = _Be.cliec_vcod_cliente;                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;      
            try 
            {
                if (txtSerie.Text == "")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Factura");
                }

                if (txtSerie.Text == "000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Factura");
                }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione cliente");
                }

                if (String.IsNullOrWhiteSpace(txtRUC.Text))
                {
                    oBase = txtRUC;
                    throw new ArgumentException("Cliente no cuenta con RUC registrado, favor de registrar RUC del Cliente");
                }

                if (Convert.ToDateTime(dteFecha.Text) > Convert.ToDateTime(dteVencimiento.Text))
                {
                    oBase = dteVencimiento;
                    throw new ArgumentException("la fecha de vencimiento no debe ser menor a la fecha de la factura");
                }


                if (Convert.ToDecimal(txtMontoTotal.Text)==0)
                {
                    oBase = txtMontoTotal;
                    if (XtraMessageBox.Show("El monto Total es 0 ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        throw new ArgumentException(string.Empty);
                    //throw new ArgumentException("El monto Total no puede ser 0");
                }
                oBe.favc_vnumero_factura =  txtSerie.Text + txtNumero.Text;
                oBe.favc_sfecha_factura = Convert.ToDateTime(dteFecha.Text);
                oBe.favc_sfecha_vencim_factura = Convert.ToDateTime(dteVencimiento.Text);
                oBe.favc_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.cliec_vnombre_cliente = bteCliente.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_forma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                //oBe.favc_npor_imp_igv = Convert.ToDecimal(txtPorcentIGV.Text);
                oBe.vendc_icod_vendedor = Convert.ToInt32(bteVendedor.Tag);
                //if (Convert.ToDecimal(txtPorcentIGV.Text) == 0)
                //    oBe.favc_npor_imp_igv = 0;
                //else
                //    oBe.favc_npor_imp_igv = Convert.ToDecimal(txtPorcentIGV.Tag);

                oBe.favc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                //oBe.favc_nmonto_imp = Convert.ToDecimal(txtMontoIGV.Text);
                oBe.favc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                //oBe.favc_bind_arroz = Convert.ToBoolean(ChkIndArroz.Checked);
                //if (ChkIndArroz.Checked == true)
                //{
                //    oBe.favc_npor_imp_ivap = Convert.ToDecimal(PorIVAP);
                //    oBe.favc_nmonto_ivap = Convert.ToDecimal(txtMontoImpArroz.Text);
                //}
                //else
                //{
                //    oBe.favc_npor_imp_ivap = 0;
                //    oBe.favc_nmonto_ivap = 0;
                //}

                oBe.favc_npor_imp_ivap = Convert.ToDecimal(PorIVAP);
                //}
                //else if (Convert.ToDecimal(PorIGV) > 0)
                //{
                oBe.favc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                //oBe.favc_npor_imp_igv = Convert.ToDecimal(PorIGV);
                //}
                //else
                //{
  


                //if (Status==BSMaintenanceStatus.ModifyCurrent)
                //{
                //    TipodeFactura = Convert.ToInt32(oBe.favc_tipo_factura);
                //}
                //else
                //{
                //    oBe.favc_tipo_factura = TipodeFactura;
                //}
                oBe.favc_tipo_factura = TipodeFactura;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                oBe.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);

                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                oBe.favc_vobservacion = Descripci;

                //oBe.favc_bincluye_igv = cbIncluyeIGV.Checked;

                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.nombreLegalEmisor = "MJCGROUP SAC";
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.direccionEmisor = Valores.strDireccionFiscal;

                oBe.nroDocumentoReceptor = txtRUC.Text;
                oBe.nombreLegalReceptor = bteCliente.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (TipodeFactura == 1 )
                    {
                        oBe.favc_icod_factura = new BVentas().insertarFacturaServio(oBe, lstFacturaDetalle);
                    }
                    else
                    {
                        oBe.favc_icod_factura = new BVentas().insertarFactura(oBe, lstFacturaDetalle);
                    }
                    
                }
                else
                {
                    if (TipodeFactura == 1 )
                    {
                        new BVentas().modificarFacturaServicios(oBe, lstFacturaDetalle, lstDelete);
                    }
                    else
                    {
                        new BVentas().modificarFactura(oBe, lstFacturaDetalle, lstDelete);
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
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.favc_icod_factura);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();
            //txtNombreAnticipo.Enabled = true;
            //txtAnticipo.Enabled = true;

            IGV = Convert.ToDecimal(Parametros.strPorcIGV);
            
            
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void setTotales()
        {
            if (lstFacturaDetalle.Count > 0)
            {
                txtMontoTotal.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
                decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
                txtMontoNeto.Text = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2).ToString();
               // txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
            }
            else
            {
                txtMontoNeto.Text = "0.00";
                //txtMontoIGV.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            
                modificarItem();
                 
        }

        private void modificarItem()
        {
            EFacturaDet obe = (EFacturaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;

          
                using (frmManteFacturaDetalleServicio frm = new frmManteFacturaDetalleServicio())
                {
                    frm.obe = obe;
                    frm.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
                    frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
                    frm.lstFacturaDetalle = lstFacturaDetalle;
                    frm.SetModify();
                    //frm.txtMoneda.Text = lkpMoneda.Text;

                    frm.txtItem.Text = String.Format("{0:000}", obe.favd_iitem_factura);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstFacturaDetalle = frm.lstFacturaDetalle;
                        viewFactura.RefreshData();
                        viewFactura.MoveLast();
                        setTotal();
                    }
                }
            
          
        }


        
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFacturaDetalle.Remove(obe);
            viewFactura.RefreshData();
            setTotal();
        }

     

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotal();
        }
        private void setTotal()
        {
            txtMontoTotal.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
            txtMontoNeto.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();
           
            if (lstFacturaDetalle.Count > 0)
            {

                txtMontoNeto.Text = lstFacturaDetalle.Sum(x => x.favd_nprecio_total_item).ToString();


            }
            else
            {
                txtMontoNeto.Text = "0.00";

            }
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void grdFactura_MouseMove(object sender, MouseEventArgs e)
        {
            //this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void dteFecha_EditValueChanged(object sender, EventArgs e)
        {
            
            dteVencimiento.EditValue = Convert.ToDateTime(dteFecha.EditValue).AddDays(Convert.ToInt32(Numero_dias_vencimiento_cliente));
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarGuiaRemision();
        }
        private void listarGuiaRemision()
        {
            try
            {
                using (FrmListarGuiaRemision frm = new FrmListarGuiaRemision())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {


                        lstFacturaDetalle.Clear();
                       

                        BtnGuiaRemision.Tag = frm._Be.remic_icod_remision;
                        BtnGuiaRemision.Text = frm._Be.remic_vnumero_remision;

                        List<EGuiaRemisionDet> mListGuiaDet = new List<EGuiaRemisionDet>();
                        mListGuiaDet = new BVentas().listarGuiaRemisionDet(frm._Be.remic_icod_remision,Parametros.intEjercicio);
                        foreach (var _BEguia in mListGuiaDet)
                        {
                            EFacturaDet _BEe =new EFacturaDet();
                            _BEe.prdc_icod_producto = _BEguia.prdc_icod_producto;
                            _BEe.favd_iitem_factura = _BEguia.dremc_inro_item;
                            _BEe.strCodProducto=_BEguia.strCodProducto;
                            _BEe.favd_vdescripcion = _BEguia.strDesProducto;
                            _BEe.favd_nprecio_unitario_item = 0;
                            _BEe.favd_ncantidad=(_BEguia.dremc_ncantidad_producto);
                            _BEe.favd_nmonto_impuesto_item = 0;
                            _BEe.favd_nporcentaje_descuento_item = 0;
                            _BEe.favd_nprecio_total_item = 0;
                            _BEe.favd_nobservaciones = _BEguia.dremc_vobservaciones;
                            _BEe.intUsuario = Valores.intUsuario;
                            _BEe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                            _BEe.strAlmacen = frm._Be.strDesAlmacen;

                            _BEe.strDesProducto = _BEguia.strDesProducto;
                            _BEe.prdc_vpart_number = _BEguia.prdc_vpart_number;
                            _BEe.strDesUM = _BEguia.strDesUM;
                            _BEe.strCategoria = _BEguia.strCategoria;
                            _BEe.strSubCategoriaUno = _BEguia.strSubCategoriaUno;
                            _BEe.intTipoOperacion = 1;
                            lstFacturaDetalle.Add(_BEe);
                            
                        }
                        nuevoToolStripMenuItem.Enabled = false;
                        eliminarToolStripMenuItem1.Enabled = false;
                        //nuevoServicioToolStripMenuItem.Enabled = false;
                        viewFactura.RefreshData();
                        setTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EFacturaDet obe = (EFacturaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstFacturaDetalle.Remove(obe);
            viewFactura.RefreshData();
            setTotal();
        
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            //if (Status == BSMaintenanceStatus.CreateNew)
            //{
            //    getNroDoc();
            //}
        }



        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        public void ListarProyecto()
        {
        }

    


        private void ChkIndArroz_CheckedChanged(object sender, EventArgs e)
        {
            //if (ChkIndArroz.Checked == false)
            //{
            //    txtPorcentIGV.Text = IGV.ToString();
            //    cbIncluyeIGV.Enabled = true;
            //}
            //else
            //{
            //    //if (Status == BSMaintenanceStatus.CreateNew)
            //    txtPorcentIGV.Text = "0.00";
            //    cbIncluyeIGV.Enabled = false;
            //}
        }

        private void txtMontoIGV_EditValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDecimal(txtMontoIGV.Text) > 0)
            //{
            //    ChkIndArroz.Enabled = false;
            //}
            //else
            //{
            //    ChkIndArroz.Enabled = true;
            //}
        }

        private void bteVendedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarVendedor();
        }
        private void listarVendedor()
        {
            try
            {
                using (FrmListarVendedor frm = new FrmListarVendedor())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteVendedor.Tag = frm._Be.vendc_icod_vendedor;
                        bteVendedor.Text = frm._Be.vendc_vnombre_vendedor;                   
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Servicios_Click(object sender, EventArgs e)
        {
           
               
           
        }

        private void Mercaderia_Click(object sender, EventArgs e)
        {
            

        }

        private void txtObservaciones_EditValueChanged(object sender, EventArgs e)
        {

        }
    }    
}