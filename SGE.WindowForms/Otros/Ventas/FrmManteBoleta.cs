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

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteBoleta : DevExpress.XtraEditors.XtraForm
    {
        public EBoletaCab oBe = new EBoletaCab();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EBoletaDet> lstBoletaDetalle = new List<EBoletaDet>();
        List<EBoletaDet> lstDelete = new List<EBoletaDet>();
        string strCodCliente = "";
        private decimal? IGV;
        public string PorIVAP;
        public string PorIGV;
        int Numero_dias_vencimiento_cliente = 0;
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
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                lkpMoneda.Enabled = Enabled;
              
            }
        }

        private void setValues()
        {
           
            txtSerie.Text = oBe.bovc_vnumero_boleta.Substring(0, 4);
            txtNumero.Text = oBe.bovc_vnumero_boleta.Substring(4, 8);
            dteFecha.EditValue = oBe.bovc_sfecha_boleta;
          
            lkpSituacion.EditValue = oBe.tablc_iid_situacion;
            bteCliente.Tag = oBe.cliec_icod_cliente;
            bteCliente.Text = oBe.cliec_vnombre_cliente;
            txtDNI.Text = oBe.cliec_cruc;
            txtDireccion.Text = oBe.cliec_vdireccion_cliente;
            txtTelefono.Text = oBe.strTelefonoCliente;
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            //txtPorcentIGV.Text = oBe.bovc_npor_imp_igv.ToString();
            lkpFormaPago.EditValue = oBe.tablc_iid_forma_pago;
            txtMontoNeto.Text = oBe.bovc_nmonto_neto.ToString();
            txtMontoIGV.Text = oBe.bovc_nmonto_imp.ToString();
            txtMontoTotal.Text = oBe.bovc_nmonto_total.ToString();
            dteVencimiento.EditValue = oBe.bovc_sfecha_vencim_boleta;
            txtObservaciones.Text = oBe.bovc_vobservacion;
            //ChkIndArroz.Checked = oBe.bovc_bind_arroz;
            PorIVAP = oBe.bovc_npor_imp_ivap.ToString();
            PorIGV = oBe.bovc_npor_imp_igv.ToString();
          //  txtMontoNetoIVAP.Text = oBe.bovc_nmonto_neto_ivap.ToString();
          //  txtMontoNetoExo.Text = oBe.bovc_nmonto_neto_exo.ToString();
           // txtMontoImpArroz.Text = oBe.bovc_nmonto_ivap.ToString();
            //string[] partes = partes = oBe.bovc_vobservacion.Split('@');
            //txtObservaciones.Lines = partes;
            bteVendedor.Tag = oBe.vendc_icod_vendedor;
            bteVendedor.Text = oBe.NomVendedor;
            Numero_dias_vencimiento_cliente = Convert.ToInt32(oBe.cliec_nnumero_dias);
            lstBoletaDetalle = new BVentas().listarBoletaDetalle(oBe.bovc_icod_boleta);
            viewFactura.RefreshData();

            BtnGuiaRemision.Tag = oBe.remic_icod_remision;
            BtnGuiaRemision.Text = oBe.remic_vnumero_remision;
            if (oBe.remic_icod_remision != 0)
            {
                nuevoToolStripMenuItem.Enabled = false;
                eliminarToolStripMenuItem.Enabled = false;
                //nuevoServicioToolStripMenuItem.Enabled = false;
            }
        }

        public FrmManteBoleta()
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
                //txtPorcentIGV.Text = Parametros.strPorcIGV;
            }    
       
            grdFactura.DataSource = lstBoletaDetalle;
          
        }
        public void CargarControles()
        {
            var lstTipoPago = new BGeneral().listarTablaRegistro(20).Where(x => x.tarec_iid_tabla_registro == 116 || x.tarec_iid_tabla_registro == 117
                  ).ToList();
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(21), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5).Where(x => x.tarec_iid_tabla_registro != 5).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, lstTipoPago, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            
        }
        private void getNroDoc()
        {
            try
            {
                //var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocBoletaVenta); /*Falta Arreglar Por Modificar Planilla*/
                var lst = new BVentas().getCorrelativoRP(1);
                txtSerie.Text = lst[0].rgpmc_vserie_boleta;
                txtNumero.Text = (Convert.ToInt32(lst[0].rgpmc_icorrelativo_boleta) + 1).ToString();
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
           
            using (frmManteBoletaDetalleServicio frm = new frmManteBoletaDetalleServicio())
            {
                frm.lstBoletaDetalle = lstBoletaDetalle;
                //frm.txtMoneda.Text = lkpMoneda.Text;
                //frm.flag_Arrox = ChkIndArroz.Checked;
                frm.SetInsert();
                frm.txtItem.Text = (lstBoletaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstBoletaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //PorIVAP = frm.PorIVAP;
                    //if (Convert.ToDecimal(frm.PorIVAP) != 0)
                    //{
                    //    PorIVAP = frm.PorIVAP;
                    //}
                    //if (Convert.ToDecimal(frm.PorIGV) != 0)
                    //{
                    //    PorIGV = frm.PorIGV;
                    //}
                    //lstBoletaDetalle = frm.lstFacturaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotales();
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
                        txtDNI.Text = frm._Be.cliec_vnumero_doc_cli;
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
                txtDNI.Text = _Be.cliec_cruc;
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
                if (txtSerie.Text == "0")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Boleta");
                }

                if (txtSerie.Text == "00000000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Boleta");
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
                if (Convert.ToDecimal(txtMontoTotal.Text) >= 700)
                {
                    //if (String.IsNullOrWhiteSpace(txtDNI.Text))
                    //{
                    //oBase = txtDNI;
                    //throw new ArgumentException("Cliente no cuenta con RUC registrado, favor de registrar RUC del Cliente");
                    //}
                    //if (XtraMessageBox.Show("El Monto Total es mayor a S/700 debe tener DNI ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    //    throw new ArgumentException(string.Empty);
                    if (XtraMessageBox.Show("El Monto Total es mayor a S/700 debe tener DNI ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        Flag = false;
                        return;
                    }
                }               
                if (Convert.ToDateTime(dteFecha.Text) > Convert.ToDateTime(dteVencimiento.Text))
                {
                    oBase = dteVencimiento;
                    throw new ArgumentException("la fecha de vencimiento no debe ser menor a la fecha de la factura");
                }

                if (Convert.ToDecimal(txtMontoTotal.Text) == 0)
                {
                    oBase = txtMontoTotal;
                    if (XtraMessageBox.Show("El monto Total es 0 ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        throw new ArgumentException(string.Empty);
                    //throw new ArgumentException("El monto Total no puede ser 0");
                }

                oBe.bovc_vnumero_boleta = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.bovc_sfecha_boleta = Convert.ToDateTime(dteFecha.Text);
                oBe.bovc_sfecha_vencim_boleta = Convert.ToDateTime(dteVencimiento.Text);
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.cliec_vnombre_cliente = bteCliente.Text;
                oBe.cliec_vcod_cliente = strCodCliente;
                oBe.bovc_vdireccion_cliente = txtDireccion.Text;
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.tablc_iid_forma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                oBe.tablc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                //oBe.bovc_npor_imp_igv = Convert.ToDecimal(txtPorcentIGV.Text);
                //oBe.bovc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                //oBe.bovc_nmonto_imp = Convert.ToDecimal(txtMontoIGV.Text);
                oBe.bovc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.anio = Parametros.intEjercicio;
                oBe.bovc_flag_estado = true;
                oBe.vendc_icod_vendedor = Convert.ToInt32(bteVendedor.Tag);
                oBe.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
                oBe.remic_vnumero_remision = BtnGuiaRemision.Text;
               
                //oBe.bovc_bind_arroz = Convert.ToBoolean(ChkIndArroz.Checked);
                //if (ChkIndArroz.Checked == true)
                //{
                //    oBe.bovc_npor_imp_ivap = Convert.ToDecimal(PorIVAP);
                //    oBe.bovc_nmonto_ivap = Convert.ToDecimal(txtMontoImpArroz.Text);
                //}
                //else
                //{
                //    oBe.bovc_npor_imp_ivap = 0;
                //    oBe.bovc_nmonto_ivap = 0;
                //}
                oBe.bovc_npor_imp_ivap = Convert.ToDecimal(PorIVAP);
               // oBe.bovc_nmonto_ivap = Convert.ToDecimal(txtMontoImpArroz.Text);
               // oBe.bovc_nmonto_neto_ivap = Convert.ToDecimal(txtMontoNetoIVAP.Text);

                oBe.bovc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                oBe.bovc_nmonto_imp = Convert.ToDecimal(txtMontoIGV.Text);
                oBe.bovc_npor_imp_igv = Convert.ToDecimal(PorIGV);

               // oBe.bovc_nmonto_neto_exo = Convert.ToDecimal(txtMontoNetoExo.Text);

                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "@";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }

                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.nombreLegalEmisor = "MJCGROUP SAC";
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.direccionEmisor = Valores.strDireccionFiscal;

                oBe.nroDocumentoReceptor = txtDNI.Text;
                oBe.nombreLegalReceptor = bteCliente.Text;

                oBe.bovc_vobservacion = Descripci;

                if (Status == BSMaintenanceStatus.CreateNew)
                {                    
                    oBe.bovc_icod_boleta = new BVentas().insertarBoleta(oBe, lstBoletaDetalle);
                }
                else
                {
                    new BVentas().modificarBoleta(oBe, lstBoletaDetalle, lstDelete);
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
                    MiEvento(oBe.bovc_icod_boleta);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();
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
            txtMontoTotal.Text = lstBoletaDetalle.Sum(x => x.bovd_nprecio_total_item).ToString();
            #region Aterior
            //decimal Convertidor;
            //if (lstBoletaDetalle.Count > 0)
            //{
            //    if (ChkIndArroz.Checked == false)
            //    {
            //        txtMontoTotal.Text = lstBoletaDetalle.Sum(x => x.bovd_nprecio_total_item).ToString();
            //        txtMontoNeto.Text = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", "")))), 2).ToString();
            //        txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
            //    }
            //    else
            //    {
            //        txtMontoImpArroz.Text = Math.Round(Convert.ToDecimal(lstBoletaDetalle.Sum(ob => ob.bovd_nmonto_imp_arroz)), 2, MidpointRounding.ToEven).ToString();
            //        txtMontoTotal.Text = lstBoletaDetalle.Sum(x => x.bovd_nprecio_total_item).ToString();
            //        Convertidor = Convert.ToDecimal(PorIVAP);
            //        if (Convertidor < 10)
            //        {
            //            PorIVAP = '0' + Convertidor.ToString();
            //        }
            //        txtMontoNeto.Text = Math.Round((Convert.ToDecimal(txtMontoTotal.Text) / Convert.ToDecimal(String.Format("1.{0}", PorIVAP.Replace(".", "")))), 2).ToString();
            //        //txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text) ).ToString();
            //    }

            //}
            //else
            //{
            //    txtMontoNeto.Text = "0.00";
            //    txtMontoIGV.Text = "0.00";
            //    txtMontoTotal.Text = "0.00";
            //}
            #endregion
            if (lstBoletaDetalle.Count > 0)
            {


                //txtMontoImpArroz.Text = lstBoletaDetalle.Sum(x => x.bovd_nmonto_imp_arroz).ToString();
                //txtMontoNetoIVAP.Text = lstBoletaDetalle.Sum(x => x.bovd_nneto_ivap).ToString();

                txtMontoIGV.Text = lstBoletaDetalle.Sum(x => x.bovd_nmonto_impuesto_item).ToString();
                txtMontoNeto.Text = lstBoletaDetalle.Sum(x => x.bovd_nprecio_total_item).ToString();

                //txtMontoNetoExo.Text = lstBoletaDetalle.Sum(x => x.bovd_nneto_exo).ToString();

            }
            else
            {
                txtMontoNeto.Text = "0.00";
                txtMontoIGV.Text = "0.00";
                txtMontoTotal.Text = "0.00";
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            //if (obe.intClasificacionProducto != Parametros.intTipoPrdServicio)
                modificarItem();
            //else
            //    modificarServicio();           
        }

        private void modificarItem()
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteBoletaDetalleServicio frm = new frmManteBoletaDetalleServicio())
            {
                frm.obe = obe;
                //frm.remic_icod_remision = Convert.ToInt32(BtnGuiaRemision.Tag);
               // frm.dblStockDisponible = Convert.ToDecimal(obe.dblStockDisponible);
               // frm.flag_afecto_ivap = obe.prdc_afecto_ivap;
                //frm.flag_afecto_igv = obe.prdc_afecto_igv;
                frm.lstBoletaDetalle = lstBoletaDetalle;
                frm.SetModify();
                //frm.txtMoneda.Text = lkpMoneda.Text;
                //frm.flag_Arrox = ChkIndArroz.Checked;
                //frm.flag_afecto_ivap = obe.AfectoIVAP;
                frm.txtItem.Text = String.Format("{0:000}", obe.bovd_iitem_boleta);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstBoletaDetalle = frm.lstBoletaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotales();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstBoletaDetalle.Remove(obe);
            viewFactura.RefreshData();
            setTotales();
        }
/////////
        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void nuevoServicio()
        {
            using (frmManteBoletaServicioDetalle frm = new frmManteBoletaServicioDetalle())
            {
                frm.SetInsert();
                frm.lstBoletaDetalle = lstBoletaDetalle;
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = (lstBoletaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstBoletaDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstBoletaDetalle = frm.lstBoletaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotales();

                }
            }
        }

        private void modificarServicio()
        {
            EBoletaDet obe = (EBoletaDet)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteBoletaServicioDetalle frm = new frmManteBoletaServicioDetalle())
            {
                frm.obe = obe;
                frm.lstBoletaDetalle = lstBoletaDetalle;
                frm.SetModify();
                frm.lkpMoneda.EditValue = lkpMoneda.EditValue;
                frm.txtItem.Text = String.Format("{0:000}", obe.bovd_iitem_boleta);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstBoletaDetalle = frm.lstBoletaDetalle;
                    viewFactura.RefreshData();
                    viewFactura.MoveLast();
                    setTotales();
                }
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

        private void txtSerie_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void BtnGuiaRemision_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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


                        lstBoletaDetalle.Clear();


                        BtnGuiaRemision.Tag = frm._Be.remic_icod_remision;
                        BtnGuiaRemision.Text = frm._Be.remic_vnumero_remision;

                        List<EGuiaRemisionDet> mListGuiaDet = new List<EGuiaRemisionDet>();
                        mListGuiaDet = new BVentas().listarGuiaRemisionDet(frm._Be.remic_icod_remision,Parametros.intEjercicio);
                        foreach (var _BEguia in mListGuiaDet)
                        {
                            EBoletaDet _BEe = new EBoletaDet();
                            _BEe.prdc_icod_producto = _BEguia.prdc_icod_producto;
                            _BEe.bovd_iitem_boleta = _BEguia.dremc_inro_item;
                            _BEe.strCodProducto = _BEguia.strCodProducto;
                            _BEe.bovd_vdescripcion = _BEguia.strDesProducto;
                            _BEe.bovd_nprecio_unitario_item = 0;
                            _BEe.bovd_ncantidad = (_BEguia.dremc_ncantidad_producto);
                            _BEe.bovd_nmonto_impuesto_item = 0;
                            _BEe.bovd_nporcentaje_descuento_item = 0;
                            _BEe.bovd_nprecio_total_item = 0;
                            _BEe.bolvd_vobservaciones = _BEguia.dremc_vobservaciones;
                            _BEe.intUsuario = Valores.intUsuario;
                            _BEe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                            _BEe.strAlmacen = frm._Be.strDesAlmacen;

                            _BEe.strDesProducto = _BEguia.strDesProducto;
                            _BEe.prdc_vpart_number = _BEguia.prdc_vpart_number;
                            _BEe.strDesUM = _BEguia.strDesUM;
                            _BEe.strCategoria = _BEguia.strCategoria;
                            _BEe.strSubCategoriaUno = _BEguia.strSubCategoriaUno;
                            _BEe.intTipoOperacion = 1;
                            lstBoletaDetalle.Add(_BEe);

                        }
                       
                            nuevoToolStripMenuItem.Enabled = false;
                            eliminarToolStripMenuItem.Enabled = false;
                            //nuevoServicioToolStripMenuItem.Enabled = false;
                        
                        viewFactura.RefreshData();
                        setTotales();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            //if (Status == BSMaintenanceStatus.CreateNew)
            //{
            //    getNroDoc();
            //}
        }

        private void grdFactura_Click(object sender, EventArgs e)
        {

        }

        private void ChkIndArroz_CheckedChanged(object sender, EventArgs e)
        {
            //if (ChkIndArroz.Checked == false)
            //{
            //    txtPorcentIGV.Text = IGV.ToString();
            //}
            //else
            //    if (Status == BSMaintenanceStatus.CreateNew)
            //        txtPorcentIGV.Text = "0.00";
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

        private void servicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                //if (ChkIndArroz.Checked == false)
                //    txtPorcentIGV.Text = IGV.ToString();
                //else
                //    txtPorcentIGV.Text = Convert.ToDecimal(txtPorcentIGV.Text).ToString();
                using (frmManteBoletaDetalle frm = new frmManteBoletaDetalle())
                {
                    frm.SetInsert();
                    frm.lstFacturaDetalle = lstBoletaDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    //frm.flag_Arrox = ChkIndArroz.Checked;
                    frm.txtItem.Text = (lstBoletaDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstBoletaDetalle.Count + 1);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //PorIVAP = frm.PorIVAP;
                        if (Convert.ToDecimal(frm.PorIVAP) != 0)
                        {
                            PorIVAP = frm.PorIVAP;
                        }
                        if (Convert.ToDecimal(frm.PorIGV) != 0)
                        {
                            PorIGV = frm.PorIGV;
                        }
                        lstBoletaDetalle = frm.lstFacturaDetalle;
                        viewFactura.RefreshData();
                        viewFactura.MoveLast();
                        setTotales();
                    }
                }
            }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {

        }
    }    
}