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
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Compras;
using System.Data.OleDb;
using System.IO;
using SGE.WindowForms.Otros.Contabilidad;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteGuiaRemision : DevExpress.XtraEditors.XtraForm
    {
        public EGuiaRemision oBe = new EGuiaRemision();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
        List<EGuiaRemisionDet> lstDelete = new List<EGuiaRemisionDet>();
        string strCodCliente = "";
        public int IcodAlmacen = 0;
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
            txtSerie.Enabled = !Enabled;
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;
            btnCliente.Enabled = !Enabled;
            txtRUC.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            txtNombreComercial.Enabled = !Enabled;
            btnAlmacen.Enabled = !Enabled;
            btnAlmacenIngreso.Enabled = !Enabled;
            txtDestinatario.Enabled = !Enabled;
            txtPartida.Enabled = !Enabled;
            txtLlegada.Enabled = !Enabled;
            btnTransportista.Enabled = !Enabled;
            txtLicencia.Enabled = !Enabled;
            txtMatricula.Enabled = !Enabled;
            txtRUCTransportista.Enabled = !Enabled;
            txtFactura.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                btnCliente.Enabled = false;
                btnAlmacen.Enabled = Enabled;
                btnAlmacenIngreso.Enabled = Enabled;
                bteProyecto.Enabled = Enabled;
                //btnCentroCostos.Enabled = Enabled;
                lkpMotivo.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
            }
        }

        private void setValues()
        {
            txtSerie.Text = oBe.remic_vnumero_remision.Substring(0, 3);
            txtNumero.Text = oBe.remic_vnumero_remision.Substring(3, oBe.remic_vnumero_remision.Length - 3);

            dteFecha.EditValue = oBe.remic_sfecha_remision;
            btnCliente.Tag = oBe.cliec_icod_cliente;
            btnCliente.Text = oBe.NomClie;
            txtDestinatario.Text = oBe.remic_vnombre_destinatario;
            txtRUC.Text = oBe.remic_vruc_destinatario;
            txtPartida.Text = oBe.remic_vdireccion_procedencia;
            txtLlegada.Text = oBe.remic_vdireccion_destinatario;
            btnAlmacen.Tag = oBe.almac_icod_almacen;
            btnAlmacen.Text = oBe.strDesAlmacen;
            lkpMotivo.EditValue = oBe.tablc_iid_motivo;
            btnTransportista.Tag = oBe.tranc_icod_transportista;
            btnTransportista.Text = oBe.strTransportista;
            txtLicencia.Text = oBe.remic_vlicencia;
            txtRUCTransportista.Text = oBe.remic_vruc_transportista;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_remision;
            txtFactura.Text = oBe.remic_vreferencia;
            txttipoDoc.Text = oBe.str_Tipo_doc;
            txtdocNumero.Text = oBe.Vnumero_Documento;
            dteFechaTranslado.EditValue = oBe.remic_sfecha_inicio;
            btnCentroCostos.Tag = oBe.cecoc_icod_centro_costo;
            txtVehiMarPla.Text = oBe.remic_vmarca_placa;
            txtCertificado.Text = oBe.remic_vcertif_inscripcion;
            lstDetalle = new BVentas().listarGuiaRemisionDet(oBe.remic_icod_remision,Parametros.intEjercicio);
            viewGuiaRemision.RefreshData();
            btnAlmacenIngreso.Tag = oBe.almac_icod_almacen_ingreso;
            btnAlmacenIngreso.Text = oBe.strDesAlmaceningreso;
            bteProyecto.Text = oBe.CodProyecto;
            btnCentroCostos.Tag = oBe.cecoc_icod_centro_costo;
            btnCentroCostos.Text = oBe.CentroCosto;
        }

        public frmManteGuiaRemision()
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
            setValues();
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
                getNroDoc();                
            }

            grdGuiaRemision.DataSource = lstDetalle;        
        }
        public void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMotivoGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void getNroDoc()
        {
            try
            {
                var lst = new BAdministracionSistema().getCorrelativoTipoDoc(Parametros.intTipoDocFacturaVenta);/*Falta Arreglar Por Modificar Planilla*/

                if (Convert.ToInt32(lst[0].tdocc_nro_serie) != 0)
                {
                    txtSerie.Text = lst[0].tdocc_nro_serie;
                    txtNumero.Text = (Convert.ToInt32(lst[0].tdocc_nro_correlativo) + 1).ToString();
                }

               
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
            if (Convert.ToInt32(btnAlmacen.Tag) == 0)
            {
                XtraMessageBox.Show("Seleccione el Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (frmManteGuiaRemisionDetalle frm = new frmManteGuiaRemisionDetalle())
            {
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                frm.oBeCab = oBe;
                //frm.IcodAlmacen =Convert.ToInt32(btnAlmacen.Tag);
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;                
                frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
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
                        btnCliente.Tag = frm._Be.cliec_icod_cliente;
                        btnCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtDestinatario.Text = frm._Be.cliec_vnombre_cliente;
                        txtRUC.Text = frm._Be.cliec_cruc;                        
                        strCodCliente = frm._Be.cliec_vcod_cliente;
                        txtLlegada.Text = frm._Be.cliec_vdireccion_cliente;
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
                btnCliente.Tag = _Be.cliec_icod_cliente;
                btnCliente.Text = _Be.cliec_vnombre_cliente;
                txtNombreComercial.Text = _Be.cliec_vnombre_comercial;
                txtDestinatario.Text = _Be.cliec_vdireccion_cliente;
                txtRUC.Text = _Be.cliec_cruc;                
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
                if (Convert.ToInt32(txtSerie.Text) == 0)
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de Guía de Remisión");
                }

                if (txtSerie.Text == "000")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                }

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Guía de Remisión");
                }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                //if (Convert.ToInt32(btnCliente.Tag) == 0)
                //{
                //    oBase = btnCliente;
                //    throw new ArgumentException("Seleccione el destinatario");
                //}

                //if (String.IsNullOrWhiteSpace(txtRUC.Text))
                //{
                //    oBase = txtRUC;
                //    throw new ArgumentException("Cliente no cuenta con RUC registrado, favor de registrar RUC del Cliente");
                //}
                if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                {
                    if (Convert.ToInt32(btnAlmacenIngreso.Tag) == 0)
                    {
                        oBase = btnAlmacenIngreso;
                        throw new ArgumentException("Ingrese el Almacén para el ingreso del Producto");
                    }
                }
                if (Convert.ToInt32(bteProyecto.Tag) > 0)
                {
                    if (Convert.ToInt32(btnCentroCostos.Tag) < 0)
                    {
                         throw new ArgumentException("Ingrese Centro Costo");
                    }
                }
                oBe.remic_vnumero_remision = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.remic_sfecha_remision = Convert.ToDateTime(dteFecha.Text);
                oBe.cliec_icod_cliente = Convert.ToInt32(btnCliente.Tag);
                oBe.remic_vnombre_destinatario = btnCliente.Text;
                oBe.remic_vnombre_destinatario = txtDestinatario.Text;
                oBe.remic_vruc_destinatario = txtRUC.Text;
                oBe.remic_vdireccion_procedencia = txtPartida.Text;
                oBe.remic_vdireccion_destinatario = txtLlegada.Text;
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacen.Tag);
                oBe.tablc_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                oBe.tranc_icod_transportista = Convert.ToInt32(btnTransportista.Tag);
                oBe.remic_vlicencia = txtLicencia.Text;
                oBe.remic_vruc_transportista = txtRUCTransportista.Text;
                oBe.tablc_iid_situacion_remision = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.remic_vreferencia = txtFactura.Text;        
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.tablc_iid_situacion_remision = Convert.ToInt32(lkpSituacion.EditValue);             
				oBe.remic_sfecha_inicio=Convert.ToDateTime(dteFechaTranslado.Text);
                oBe.almac_icod_almacen_ingreso = Convert.ToInt32(btnAlmacenIngreso.Tag);
                oBe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                oBe.cecoc_icod_centro_costo = Convert.ToInt32(btnCentroCostos.Tag);
                oBe.remic_vmarca_placa = txtVehiMarPla.Text;
                oBe.remic_vcertif_inscripcion = txtCertificado.Text;
                if (Status == BSMaintenanceStatus.CreateNew)
                {                    
                    oBe.remic_icod_remision = new BVentas().insertarGuiaRemision(oBe, lstDetalle);
                }
                else
                {
                    new BVentas().modificarGuiaRemision(oBe, lstDetalle, lstDelete);
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
                    MiEvento(oBe.remic_icod_remision);
                    Close();
                }
            }
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

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemisionDet obe = (EGuiaRemisionDet)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteGuiaRemisionDetalle frm = new frmManteGuiaRemisionDetalle())
            {
                frm.oBe = obe;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();                
                
                frm.txtItem.Text = String.Format("{0:000}", obe.dremc_inro_item);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemisionDet obe = (EGuiaRemisionDet)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewGuiaRemision.RefreshData();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }
        private void listarAlmacen()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnAlmacen.Tag = frm._Be.almac_icod_almacen;
                    btnAlmacen.Text = frm._Be.almac_vdescripcion;
                    if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                    {
                        txtPartida.Text = frm._Be.almac_vdireccion;
                    }
                    else
                    {
                        txtPartida.Text = frm._Be.almac_vdireccion;
                    }
                    /***************************************************************/
                    oBe.almac_icod_almacen = frm._Be.almac_icod_almacen;
                    
                }
            }
        }
        private void listarAlmacenIngreso()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnAlmacenIngreso.Tag = frm._Be.almac_icod_almacen;
                    btnAlmacenIngreso.Text = frm._Be.almac_vdescripcion;
                    if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
                    {
                        txtLlegada.Text = frm._Be.almac_vdireccion;
                    }
                    /***************************************************************/
                    oBe.almac_icod_almacen_ingreso = frm._Be.almac_icod_almacen;
                }
            }
        }
        private void bteCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarCliente();
        }

        private void bteAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarAlmacen();
        }
        private void txtItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
        private void txtdias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
              
        private void CargarDetallePedido()
        {
            int contSinStock=0;
            List<EPedidoClienDet> lstDetallePe = new List<EPedidoClienDet>();
            //lstDetallePe = new BVentas().listarPedidoVentaDet(Convert.ToInt32(btnPedido.Tag), Parametros.intEjercicio);
            foreach (var _be in lstDetallePe)
            {
                decimal stock = new BAlmacen().listarStockProductoPorAlmacen(Convert.ToInt32(btnAlmacen.Tag), _be.prdc_icod_producto);
                if (stock >= _be.lpedid_nCant_pedido)
                {
                    EGuiaRemisionDet _bee = new EGuiaRemisionDet();
                    _bee.dremc_icod_detalle_remision = 0;

                    _bee.remic_icod_remision = 0;
                    if (lstDetalle.Count == 0)
                    {
                        _bee.dremc_inro_item = 1;
                    }
                    else
                    {
                        _bee.dremc_inro_item = Convert.ToInt16(lstDetalle.Count() + 1);
                    }
                    _bee.prdc_icod_producto = Convert.ToInt32(_be.prdc_icod_producto);
                    _bee.dremc_ncantidad_producto = _be.lpedid_nCant_pedido;
                    _bee.dremc_vcantidad_producto = _be.lpedid_nCant_pedido.ToString();
                    _bee.kardc_icod_correlativo = 0;
                    _bee.tablc_iid_sit_item_guia_remision = 0;
                    _bee.strCodProducto = _be.prdc_vcode_producto;
                    _bee.strDesProducto = _be.prdc_vdescripcion_larga;
                    _bee.strCategoria = _be.strCategoria;
                    _bee.strSubCategoriaUno = _be.strSubCategoriaUno;
                    _bee.strDesUM = _be.StrUnidadMedida;
                    _bee.dremc_vobservaciones = "";
                    _bee.dblStockDisponible = Convert.ToDecimal(_be.lpedid_nstock_producto);
                    _bee.dremc_PastBibli = false;
                    _bee.dremc_nDescuento = 0;
                    _bee.dremc_nprecio_lista = 0;
                    _bee.dremc_nPrecio_Unitario = Convert.ToDecimal(_be.lpedid_nprecio_uni);
                    _bee.dremc_nMonto_Total = Convert.ToDecimal(_be.lpedid_nCant_pedido) * Convert.ToDecimal(_be.lpedid_nprecio_uni);
                    _bee.dremc_PastBibli = false;
                    _bee.intTipoOperacion = 1;
                    lstDetalle.Add(_bee);
                }else
                {
                    XtraMessageBox.Show("EL producto " + _be.prdc_vdescripcion_larga+ " tiene stock insuficiente para cubrir la cantidad de "+_be.lpedid_nCant_pedido.ToString()+" Productos.", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            grdGuiaRemision.DataSource = lstDetalle;
            viewGuiaRemision.RefreshData();
        }
        private void cargarPedidoDetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnAlmacen.Tag) == 0)
            {
                XtraMessageBox.Show("Seleccione el Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CargarDetallePedido();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                getNroDoc();
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }

        private void lkpMotivo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
            {
                btnAlmacenIngreso.Enabled = true;
            }
            else
            {
                btnAlmacenIngreso.Enabled = false;
            }
        }
        string filePath = "";
        private void importarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                string Extension = Path.GetExtension(ofd.FileName);

                if (Extension == ".xlsx")
                {
                    filePath = ofd.FileName;
                    ImportarDatosExcel();
                }
                else
                {
                    //ClearLista();
                    //XtraMessageBox.Show("El archivo no coincide con el tipo de archivo seleccionado " + lkpTipoArchivo.Text, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void ImportarDatosExcel()
        {
            //ClearLista();

            DataTable dt;
            OleDbDataAdapter MyDataAdapter;

            string connString = "provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection oledbConn = new OleDbConnection(connString);
            try
            {

                MyDataAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet$]", connString);
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
        private void FillList(DataTable dt)
        {
            List<EInventarioDet> lstDetalleaUX = new List<EInventarioDet>();

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
                        EInventarioDet obe = new EInventarioDet();

                        foreach (DataColumn column in dt.Columns)
                        {

                            switch (column.ToString().ToUpper().Trim())
                            {
                                case "CÓDIGO":
                                    nomC = "CÓDIGO";
                                    obe.strCodProducto = row[column].ToString();
                                    break;
                                case "CANTIDAD":
                                    nomC = "CANTIDAD";
                                    obe.invd_icantidad = Convert.ToInt32(row[column]);
                                    break;
                            }
                        }
                        obe.intUsuario = Valores.intUsuario;
                        obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        lstDetalleaUX.Add(obe);

                    }
                }

                foreach (var _BEE in lstDetalleaUX)
                {
                    List<EProducto> MlistProduc = new BAlmacen().listarProductoXCodigp(Parametros.intEjercicio, _BEE.strCodProducto.Trim());
                    if (MlistProduc.Count > 0)
                    {
                        EGuiaRemisionDet _BEGuia = new EGuiaRemisionDet();
                        _BEGuia.dremc_icod_detalle_remision = 0;
                        _BEGuia.remic_icod_remision = 0;
                        if (lstDetalle.Count() == 0)
                            _BEGuia.dremc_inro_item = 1;
                        else
                            _BEGuia.dremc_inro_item = Convert.ToInt16(lstDetalle.Count() + 1);
                        _BEGuia.prdc_icod_producto = Convert.ToInt32(MlistProduc[0].prdc_icod_producto);
                        _BEGuia.dremc_ncantidad_producto = _BEE.invd_icantidad;
                        _BEGuia.dremc_vcantidad_producto = _BEE.invd_icantidad.ToString();
                        _BEGuia.kardc_icod_correlativo = 0;
                        _BEGuia.tablc_iid_sit_item_guia_remision = 1;
                        _BEGuia.strCodProducto = MlistProduc[0].prdc_vcode_producto;
                        _BEGuia.strDesProducto = MlistProduc[0].prdc_vdescripcion_larga;
                        _BEGuia.strCategoria = MlistProduc[0].strCategoria;
                        _BEGuia.strSubCategoriaUno = MlistProduc[0].strSubCategoriaUno;
                        _BEGuia.strDesUM = MlistProduc[0].StrUnidadMedida;
                        _BEGuia.dremc_vobservaciones = "";
                        _BEGuia.dblStockDisponible = new BAlmacen().listarStockProductoPorAlmacen(Convert.ToInt32(btnAlmacen.Tag), Convert.ToInt32(MlistProduc[0].prdc_icod_producto));
                        _BEGuia.dremc_PastBibli = false;
                        _BEGuia.dremc_nDescuento = Convert.ToDecimal(MlistProduc[0].prdc_nPor_Descuento);
                        _BEGuia.dremc_nprecio_lista = Convert.ToDecimal(0);
                        _BEGuia.dremc_nPrecio_Unitario = Convert.ToDecimal(MlistProduc[0].prdc_nPrecio_venta);
                        _BEGuia.dremc_nMonto_Total = Convert.ToDecimal(_BEGuia.dremc_nPrecio_Unitario) * Convert.ToDecimal(_BEE.invd_icantidad);
                        _BEGuia.kardc_icod_correlativo_ingreso = 0;
                        _BEGuia.intTipoOperacion = 1;
                        lstDetalle.Add(_BEGuia);
                    }

                }
                viewGuiaRemision.RefreshData();
                viewGuiaRemision.MoveLast();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error Fila: " + contF + "\t Columna: " + nomC, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnAlmacenIngreso_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }

        private void btnAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }

        private void lkpMotivo_EditValueChanged_2(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpMotivo.EditValue) == 226)
            {
                btnAlmacenIngreso.Enabled = true;
                btnCliente.Enabled = false;
            }
            else
            {
                btnAlmacenIngreso.Enabled = false;
                btnCliente.Enabled = true;
            }
        }

        private void btnTransportista_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarTransportista frm = new frmListarTransportista())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnTransportista.Tag = frm._Be.tranc_icod_transportista;
                    btnTransportista.Text = frm._Be.tranc_vnombre_transportista;
                    txtRUCTransportista.Text = frm._Be.tranc_vruc;
                    txtVehiMarPla.Text = frm._Be.tranc_vnum_placa;
                    txtLicencia.Text = frm._Be.tranc_vnum_licencia_conducir;

                }
            }
        }

        private void btnCentroCostos_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCentroCosto();
        }
        private void ListarCentroCosto()
        {
            using (frmListarCentroCostoProyectos Ccosto = new frmListarCentroCostoProyectos())
            {

                if (Ccosto.ShowDialog() == DialogResult.OK)
                {
                    btnCentroCostos.Text = Ccosto._Be.cecoc_vcodigo_centro_costo;//cecoc_ccodigo_centro_costo - centro_costo
                    btnCentroCostos.Tag = Ccosto._Be.cecoc_icod_centro_costo;//cecoc_icod_centro_costo (correlativo) - centro_costo

                }
            }
        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
        }
        public void AlmacenPrincipal()
        {
            //List<EAlmacen> lstAlamcen = new List<EAlmacen>();
            //lstAlamcen = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == 53).ToList();
            //btnAlmacen.Text = lstAlamcen[0].almac_vdescripcion;
            //txtPartida.Text = lstAlamcen[0].almac_vdireccion;
            //btnAlmacen.Tag = 53;
        }

        private void txtNumero_Leave(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            List<EGuiaRemision> Lstver = new BVentas().getGRCabVerificarNumero(String.Format("{0}{1}", txtSerie.Text, txtNumero.Text));
            if (Lstver.Count > 0)
            {

                oBase = txtNumero;
                XtraMessageBox.Show("El Número " + String.Format("{0}{1}", txtSerie.Text, txtNumero.Text) + " N° G/R: Ya Existia");
 
                
            }
        }

    

        private void frmManteGuiaRemision_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void frmManteGuiaRemision_Load(object sender, EventArgs e)
        {
            cargar();  
        }

        private void btnCliente_KeyDown(object sender, KeyEventArgs e)
        {
          

                
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }    
}