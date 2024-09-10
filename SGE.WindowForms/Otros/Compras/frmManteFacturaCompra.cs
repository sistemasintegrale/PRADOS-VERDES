using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using System.Security.Principal;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Otros.Operaciones;
using System.IO;
using System.Data.OleDb;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteFacturaCompra : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        /*--------------*/
        public EFacturaCompra Obe = new EFacturaCompra();
        public List<EFacturaCompraDet> lstDetalle = new List<EFacturaCompraDet>();
        public List<EFacturaCompraDet> lstDelete = new List<EFacturaCompraDet>();
        private List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public int OCL = 0;
        /*--------------*/
        public int CodOC = 0;
       
        public frmManteFacturaCompra()
        {
            InitializeComponent();
        }

        private void frmManteFacturaCompra_Load(object sender, EventArgs e)
        {
            cargar();
        }

        public void setGuardar()
        {
            SetSave();
           
        }
    
        private void cargar()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(21), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(5), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpFormaPago, new BGeneral().listarTablaRegistro(20), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
           
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtIGV.Text = Parametros.strPorcIGV;
                setFecha(dtFecha);
                setFecha(dtFechaVencimiento);
            
                getTipoCambio();
                setAlmacen();
                
            }
            else
                //CARGAR EL DETALLE SI ES MODIFICACION O CONSULTA
                lstDetalle = new BCompras().listarFacCompraDet(Obe.fcoc_icod_doc);
            grdDetalle.DataSource = lstDetalle;
        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        public void getTipoCambio()
        {
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            var Lista = lstTipoCambio.Where(ob => ob.ticac_fecha_tipo_cambio.ToShortDateString() == Convert.ToDateTime(dtFecha.EditValue).ToShortDateString()).ToList();
            Lista.ForEach(obe =>
            {
                txtTipoCambio.Text = obe.ticac_tipo_cambio_venta.ToString();
            });
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
            btnGuardar.Enabled = !Enabled;
            mnu.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)            
                enableControls(false);          

            if(Status == BSMaintenanceStatus.ModifyCurrent )
            {
                bteRefreshTipoCambio.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtObservacion.Properties.ReadOnly = true;

                txtMtoDesGrav.Properties.ReadOnly = true;
                txtImpDesGrav.Properties.ReadOnly = true;
                txtMtoDesMixto.Properties.ReadOnly = true;
                txtImpDesMixto.Properties.ReadOnly = true;
                txtMtoDesNoGrav.Properties.ReadOnly = true;
                txtImpDesNoGrav.Properties.ReadOnly = true;
                txtMtoNoGravada.Properties.ReadOnly = true;
                txtNroDetraccion.Properties.ReadOnly = true;
                dtFechaDetracc.Enabled = false;
                bteRefreshTipoCambio.Enabled = false;
                lkpMes.Enabled = false;
            }
            
        }

        private void enableControls(bool Enabled)
        {
            txtSerie.Enabled = Enabled;
            txtCorrelativo.Enabled = Enabled;
            dtFecha.Enabled = Enabled;
            bteProveedor.Enabled = Enabled;
            dtFechaVencimiento.Enabled = Enabled;
            lkpFormaPago.Enabled = Enabled;
            lkpMoneda.Enabled = Enabled;
            bteAlmacen.Enabled = Enabled;
            txtIGV.Enabled = Enabled;
            cbIncluyeIGV.Enabled = Enabled;
            txtTipoCambio.Enabled = Enabled;
            
        }

        public void setValues()
        {
            bteProveedor.Tag = Obe.proc_icod_proveedor;
            bteProveedor.Text = Obe.strProveedor;
            txtSerie.Text = Obe.fcoc_num_doc.Substring(0, 4);
            txtCorrelativo.Text = Obe.fcoc_num_doc.Substring(4, 8);       
            dtFecha.EditValue = Obe.fcoc_sfecha_doc;
            lkpFormaPago.EditValue = Obe.fcoc_iforma_pago;
            dtFechaVencimiento.EditValue = Obe.fcoc_svencimiento;
            txtObservacion.Text = Obe.fcoc_vreferencia;
            lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
            bteAlmacen.Text = Obe.strAlmacen;
            bteAlmacen.Tag = Obe.almac_icod_almacen;
            txtIGV.Text = Obe.fcoc_nporcent_imp_doc.ToString();
            lkpSituacion.EditValue = Obe.fcoc_isituacion;
            //
            txtMtoDesGrav.Text = Obe.fcoc_nmonto_destino_gravado.ToString();
            txtMtoDesMixto.Text = Obe.fcoc_nmonto_destino_mixto.ToString();
            txtMtoDesNoGrav.Text = Obe.fcoc_nmonto_destino_nogravado.ToString();
            txtMtoNoGravada.Text = Obe.fcoc_nmonto_nogravado.ToString();
            txtImpDesGrav.Text = Obe.fcoc_nmonto_imp_destino_gravado.ToString();
            txtImpDesMixto.Text = Obe.fcoc_nmonto_imp_destino_mixto.ToString();
            txtImpDesNoGrav.Text = Obe.fcoc_nmonto_imp_destino_nogravado.ToString();
            txtTipoCambio.Text = Obe.fcoc_nmonto_tipo_cambio.ToString();
            lkpMes.EditValue = Convert.ToInt32(Obe.fcoc_imes_iid_proceso);
            cbIncluyeIGV.Checked = Obe.fcoc_bincluye_igv;
            txtNroDetraccion.Text = Obe.fcoc_vnro_depo_detraccion;
            dtFechaDetracc.Text = (Obe.fcoc_sfecha_depo_detraccion == null) ? "" : Convert.ToDateTime(Obe.fcoc_sfecha_depo_detraccion).ToShortDateString();
            txtMtoNeto.Text = Obe.fcoc_nmonto_neto_detalle.ToString();
            txtMtoTotal.Text = Obe.fcoc_nmonto_total_detalle.ToString();
            btnOC.Tag = Obe.ococ_icod_orden_compra;
            btnOC.Text = Obe.ococ_numero_orden_compra;
            //bteProveedor.Tag = Obe.proc_icod_proveedor;
            //bteProveedor.Text = Obe.strProveedor;
           
            
            
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
                if (bteProveedor.Tag == null)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione proveedor");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (txtSerie.Enabled)
                    {
                        if (txtSerie.Text == "000")
                        {
                            txtSerie.Focus();
                            throw new ArgumentException("Ingrese nro. de Serie de la factura");
                        }

                        if (txtCorrelativo.Text == "000000")
                        {
                            txtCorrelativo.Focus();
                            throw new ArgumentException("Ingrese nro. de la factura");
                        }

                    }
                
                }
                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                {
                    oBase = lkpMes;
                    throw new ArgumentException("Seleccione el Mes del Proceso");
                }
                if (lstDetalle.Count == 0)
                {
                    XtraMessageBox.Show("La Factura de Compra, debe tener al menos un registro de un producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Flag = false;
                    return;
                    
                }
                
                if (Convert.ToDecimal(txtMontoTotalCabecera.Text) != Convert.ToDecimal(txtMtoTotal.Text))
                {
                    if (XtraMessageBox.Show("Los Montos de la Cabecera no coinciden con los Item de la Factura ¿Desea continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        Flag = false;
                        return;
                    }

                }
                /**/
                DateTime? dtNullVal = null;
                int? intNullVal = null;
                Int16? intNullVal16 = null;

                Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);

                Obe.fcoc_num_doc = txtSerie.Text + txtCorrelativo.Text;
                
                Obe.fcoc_sfecha_doc = Convert.ToDateTime(dtFecha.EditValue);
                Obe.almac_icod_almacen = Convert.ToInt32(bteAlmacen.Tag);
                Obe.fcoc_svencimiento = Convert.ToDateTime(dtFechaVencimiento.EditValue);
                Obe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                Obe.fcoc_iforma_pago = Convert.ToInt32(lkpFormaPago.EditValue);
                Obe.fcoc_vreferencia = txtObservacion.Text;
                Obe.fcoc_nporcent_imp_doc = Convert.ToDecimal(txtIGV.Text);
                Obe.fcoc_nmonto_destino_gravado = Convert.ToDecimal(txtMtoDesGrav.Text);
                Obe.fcoc_nmonto_destino_mixto = Convert.ToDecimal(txtMtoDesMixto.Text);
                Obe.fcoc_nmonto_destino_nogravado = Convert.ToDecimal(txtMtoDesNoGrav.Text);
                Obe.fcoc_nmonto_nogravado = Convert.ToDecimal(txtMtoNoGravada.Text);
                Obe.fcoc_nmonto_imp_destino_gravado = Convert.ToDecimal(txtImpDesGrav.Text);
                Obe.fcoc_nmonto_imp_destino_mixto = Convert.ToDecimal(txtImpDesMixto.Text);
                Obe.fcoc_nmonto_imp_destino_nogravado = Convert.ToDecimal(txtImpDesNoGrav.Text);
                Obe.fcoc_nmonto_tipo_cambio = Convert.ToDecimal(txtTipoCambio.Text);
                Obe.fcoc_imes_iid_proceso = Convert.ToInt16(lkpMes.EditValue);
                Obe.fcoc_bincluye_igv = cbIncluyeIGV.Checked;
                Obe.fcoc_vnro_depo_detraccion = txtNroDetraccion.Text;
                Obe.fcoc_sfecha_depo_detraccion = (String.IsNullOrWhiteSpace(dtFechaDetracc.Text)) ? dtNullVal : Convert.ToDateTime(dtFechaDetracc.Text);
                Obe.fcoc_nmonto_neto_detalle = Convert.ToDecimal(txtMtoNeto.Text);
                Obe.fcoc_nmonto_total_detalle = Convert.ToDecimal(txtMontoTotalCabecera.Text);
                Obe.fcoc_isituacion = Convert.ToInt32(lkpSituacion.EditValue);
                //Obe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                Obe.ococ_icod_orden_compra = Convert.ToInt32(btnOC.Tag);
                Obe.fcoc_anio = Convert.ToInt16(Parametros.intEjercicio);           
                /**/
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                Obe.fcoc_flag_importacion = false;///////////////////
                Obe.fcoc_iestado_recep = 273;//FACTURADO
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.fcoc_icod_doc = new BCompras().insertarFacCompraNacional(Obe, lstDetalle);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BCompras().modificarFacCompraNacional(Obe, lstDetalle, lstDelete);
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
                    this.MiEvento(Obe.fcoc_icod_doc);
                    this.Close();
                }
            }
        }

        private void listarProveedor()
        {
            FrmListarProveedor frm = new FrmListarProveedor();
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = frm._Be.iid_icod_proveedor;
                bteProveedor.Text = frm._Be.vnombrecompleto;
            }
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
        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProveedor();
            txtSerie.Focus();
        }

        private void bteProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarProveedor();       
        }
        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
            txtMtoDesGrav.Focus();
        }
        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacen();
        }
        private void nuevo()
        {
            BaseEdit oBase = null;
            try
            {
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione Almacén");
                }
                using (frmManteFacCompraDetalle frm = new frmManteFacCompraDetalle())
                {
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.txtMoneda.Text = lkpMoneda.Text;
                    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                    frm.CodOC = CodOC;
                    frm.OCL = OCL;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        setTotal();
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
        private void modificar()
        {
            EFacturaCompraDet obe = (EFacturaCompraDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewDetalle.FocusedRowHandle;
            using (frmManteFacCompraDetalle frm = new frmManteFacCompraDetalle())
            {
                frm.obe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.txtMoneda.Text = lkpMoneda.Text;
                frm.txtItem.Text = String.Format("{0:000}", obe.fcod_iitem);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();                    
                    setTotal();
                    viewDetalle.FocusedRowHandle = index;
                }
            }
        }
        private void eliminar()
        {
            EFacturaCompraDet obe = (EFacturaCompraDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewDetalle.RefreshData();
            setTotal();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            getTipoCambio();
            dtFechaVencimiento.EditValue = dtFecha.EditValue;
            lkpMes.EditValue = Convert.ToDateTime(dtFecha.EditValue).Month;
        }

        private void setTotal()
        {
            if (lstDetalle.Count > 0)
            {
                if (cbIncluyeIGV.Checked)
                {
                    decimal total = lstDetalle.Sum(x => x.fcod_nmonto_total);
                    decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                    txtMtoNeto.Text =Convertir.RedondearNumero( neto).ToString();
                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                }
                else
                {
                    decimal neto = lstDetalle.Sum(x => x.fcod_nmonto_total);
                    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                }
            }          
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotal();
        }

        private void txtMtoDesGrav_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesGrav.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesGrav.Text =Convertir.RedondearNumero(impuesto).ToString();
        }
        private void txtMontocabecera()
        {
                decimal monto_total = Convert.ToDecimal(txtMtoDesGrav.Text) + Convert.ToDecimal(txtMtoDesMixto.Text) + Convert.ToDecimal(txtMtoDesNoGrav.Text) + Convert.ToDecimal(txtMtoNoGravada.Text) +
                   Convert.ToDecimal(txtImpDesGrav.Text) + Convert.ToDecimal(txtImpDesMixto.Text) + Convert.ToDecimal(txtImpDesNoGrav.Text);
                txtMontoTotalCabecera.Text = Convertir.RedondearNumero(monto_total).ToString(); 
        }
        private void txtMtoDesMixto_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesMixto.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesMixto.Text = Convertir.RedondearNumero(impuesto).ToString();
        }

        private void txtMtoDesNoGrav_()
        {
            decimal neto = Convert.ToDecimal(txtMtoDesNoGrav.Text);
            decimal impuesto = neto * Convert.ToDecimal("0." + txtIGV.Text.Replace(".", ""));
            txtImpDesNoGrav.Text = Convertir.RedondearNumero(impuesto).ToString();
        }
        private void txtMtoDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesGrav_();
            txtMontocabecera();
        }

        private void txtMtoDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesMixto_();
            txtMontocabecera();
        }

        private void txtMtoDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMtoDesNoGrav_();
            txtMontocabecera();
        }

        private void txtIGV_EditValueChanged(object sender, EventArgs e)
        {
            setTotal();
            txtMtoDesGrav_(); txtMtoDesMixto_(); txtMtoDesNoGrav_();
        }

        private void bteRefreshTipoCambio_Click(object sender, EventArgs e)
        {
            getTipoCambio();
        }

        private void txtImpDesGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtImpDesMixto_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtMtoNoGravada_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void txtImpDesNoGrav_EditValueChanged(object sender, EventArgs e)
        {
            txtMontocabecera();
        }

        private void btnOC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarOrdenCompra();
        }
        private void listarOrdenCompra()
        {
            
            FrmListarOrdenCompra frm = new FrmListarOrdenCompra();
            frm.proc_icod_proveedor =Convert.ToInt32(bteProveedor.Tag);
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnOC.Tag = frm._Be.ococ_icod_orden_compra;
                btnOC.Text = frm._Be.ococ_numero_orden_compra;
                CodOC =Convert.ToInt32(btnOC.Tag);
                if (frm._Be.facc_bIncluido_igv == true)
                {
                    cbIncluyeIGV.Checked = true;
                }
                else
                {
                    cbIncluyeIGV.Checked = false;
                }
                OCL = 1;
            }
        }

        // importar datos de un excel
        string filePath = "";
        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))//busca el archivo excel
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xls")// tipo de extension
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcel();
                }
                else
                {
                    ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }
        private void ClearLista()
        {
            lstDetalle.Clear();
            viewDetalle.RefreshData();
        }
        private void ImportarDatosExcel()
        {
            ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.Jet.OLEDB.4.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";//version del 97-2003
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [FactCompra$]", connString);//nombre de la hoja del excel
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                FillList(dt);


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                oledbConn.Close();
            }
        }
        private void FillList(DataTable dt)//todos los datos del excel
        {
            int contF = 0;
            string nomC = String.Empty;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    int b = dt.Rows.Count;
                    foreach (DataRow row in dt.Rows)
                    {
                        contF++;
                        EFacturaCompraDet obe = new EFacturaCompraDet();

                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())//todo en mayuscula 
                            {
                                //planilla

                                case "CODPROVEEDOR":
                                    nomC = "CODPROVEEDOR";
                                    obe.prov_vcodigo_prov = row[column].ToString();
                                    break;
                                case "CODIGOPROD":
                                    nomC = "CODIGOPROD";
                                    obe.strCodProducto = row[column].ToString();
                                    List<EProducto> MlistProd = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, obe.strCodProducto);
                                    if (MlistProd.Count > 0)
                                    {
                                        obe.prd_icod_producto = Convert.ToInt32(MlistProd[0].prdc_icod_producto);
                                        obe.fcod_vdescripcion_item = (MlistProd[0].prdc_vdescripcion_larga);
                                        obe.strLinea = (MlistProd[0].strCategoria);
                                        obe.strSubLinea = (MlistProd[0].strSubCategoriaUno);
                                        obe.strDesUM = (MlistProd[0].StrUnidadMedida);
                                        
                                        obe.strMoneda = lkpMoneda.Text;
                                    }
                                    else
                                    {
                                        obe.prd_icod_producto = 0;
                                    }
                                    break;
                               
                                    case "CANTIDAD":
                                    nomC = "CANTIDAD";                             
                                        obe.fcod_ncantidad = Convert.ToInt32(row[column]);
                                    break;
                                    case "PUNITARIO":
                                    nomC = "PUNITARIO";
                                    obe.fcod_nmonto_unit = Convert.ToDecimal(row[column]);
                                    obe.fcod_nmonto_total = obe.fcod_nmonto_unit * obe.fcod_ncantidad;
                                    break;
                                    }

                            if (lstDetalle.Count > 0)
                            {
                                if (cbIncluyeIGV.Checked)
                                {
                                    decimal total = lstDetalle.Sum(x => x.fcod_nmonto_total);
                                    decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                                }
                                else
                                {
                                    decimal neto = lstDetalle.Sum(x => x.fcod_nmonto_total);
                                    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIGV.Text.Replace(".", "")), 2);
                                    txtMtoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                                    txtMtoTotal.Text = Convertir.RedondearNumero(total).ToString();
                                }
                            } 
                        }

                        obe.intTipoOperacion = 1;
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstDetalle.Add(obe);

                    }
                }
                //List<EFacturaCompraDet> mlistNuevos=new List<EFacturaCompraDet> ();
                //foreach (var BE in lstDetalle)
                //{
                //    if (BE.prd_icod_producto == 0)
                //    {
                //        mlistNuevos.Add(BE);
                //    }
                //}
                lstDetalle = lstDetalle.Where(ob => ob.prd_icod_producto != 0).ToList();
                int i=1;
                foreach (var _bee in lstDetalle)
                {
                    _bee.fcod_iitem = i;
                    i = i + 1;
                }
                grdDetalle.DataSource = lstDetalle;
                viewDetalle.RefreshData();

                //if (mlistNuevos.Count > 0)
                //{
                //    if (XtraMessageBox.Show("¿Desea Exportar los que no existen en el catálogo de productos?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        ExportarDatos(mlistNuevos);
                //    }
                //}
                //XtraMessageBox.Show("Se importaron " + (lstDetalle.Where(e => e.prov_vcodigo_prov == vcod_proveedor).ToList().GroupBy(i => i.prdc_vcode_producto).Select(group => group.First()).ToList().Count()) + " registros", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //txtTotalRegistro.Text = lstDetalle.Count().ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bteLimpiar_Click(object sender, EventArgs e)
        {
            
            btnOC.Tag = 0;
            btnOC.Text = "";
            
            OCL = 0;
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {


            if (txtSerie.Text.Length == 4)
            {
                
            }
            else
            {
                XtraMessageBox.Show("N° Serie debe ser 4");
            }
            if (txtSerie.Text =="")
            {
                XtraMessageBox.Show("N° Serie no puede ser vacio");
            }
        }

        private void txtCorrelativo_Leave(object sender, EventArgs e)
        {
            //List<EFacturaCompra> Lstver = new BCompras().getVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtCorrelativo.Text));
            //if (Lstver.Count > 0)
            //{             
            //    XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtSerie.Text, txtCorrelativo.Text) + " N° Factura Ya Existia");             
            //}
        }

        private void btnDRC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDRC();
        }
        private void listarDRC()
        {
            FrmListarDRC frm = new FrmListarDRC();
            frm.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
            frm.Carga();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnDRC.Tag = frm._Be.drcc_icod_doc_recepcion_compra;
                btnDRC.Text = frm._Be.drcc_vnumero_doc_recepcion_compra;
                OCL = 1;
                CargarDRC(Convert.ToInt32(btnDRC.Tag));
            }
        }

        private void CargarDRC(int IcodDRC)
        {
            List<EDocRecepcionCompraDet> lstDocRecepcionCompra = new List<EDocRecepcionCompraDet>();
            lstDocRecepcionCompra = new BCompras().listarDocRecepcionComprasDetalle(IcodDRC);
            foreach (var _bee in lstDocRecepcionCompra)
            {
                EFacturaCompraDet BEFCD = new EFacturaCompraDet();
                BEFCD.fcod_iitem = _bee.drcd_iitem;
                BEFCD.strCodProducto = _bee.strCodProducto;
                BEFCD.fcod_ncantidad = _bee.drcd_ncantidad;
                BEFCD.fcod_vdescripcion_item = _bee.drcd_vdescripcion_item;
                BEFCD.strDesUM = _bee.strDesUM;
                BEFCD.fcod_nmonto_unit = 0;
                BEFCD.fcod_nmonto_total = 0;
                lstDetalle.Add(BEFCD);
            }
            grdDetalle.RefreshDataSource();
        }
    }
}