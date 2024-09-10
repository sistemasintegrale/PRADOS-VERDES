using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SGE.Entity;
//using SGE.windowsForm;
using System.Security.Principal;
using SGE.BusinessLogic;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteOrdenDespacho : DevExpress.XtraEditors.XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmManteOrdenDespacho));
        public delegate void DelegadoMensaje();
        public event DelegadoMensaje MiEvento;
        //MyKeyPress myKeyPressHandler = new MyKeyPress();
        private BSMaintenanceStatus mStatus;

        List<EFacturaCab> ListaFac = new List<EFacturaCab>();
        List<EFacturaCab> ListaFacEliminados = new List<EFacturaCab>();
        List<EOrdenDespachoDetalle> Lista = new List<EOrdenDespachoDetalle>();
        List<EOrdenDespachoDetalle> ListaEliminados = new List<EOrdenDespachoDetalle>();        
        public int code; //id de la orden de despacho
        public int code_pedido=0; //id de la orden de despacho


        public Boolean desac_bmodi_doc;
        private string valido = null;
        //private BOrdenDespachoAutoventa Obl;

        public FrmManteOrdenDespacho()
        {
            KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();                        
            //txtRUC.KeyPress += new KeyPressEventHandler(myKeyPressHandler.MyKeyCounter);               
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
            deFecha.Enabled = !Enabled;
            LkpSituacion.Enabled = !Enabled;
            LkpMotivo.Enabled = !Enabled;            
            txtRUC.Enabled = !Enabled;
            txtAtencion.Enabled = !Enabled;
            txtEntregar.Enabled = !Enabled;
            txtEntregar2.Enabled = !Enabled;
            bteTransportista.Enabled = !Enabled;                      
            txtReferencia.Enabled = !Enabled;            
           
            txtPartida.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                LkpSituacion.Enabled = Enabled;
                LkpMotivo.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                
                LkpMotivo.Enabled = Enabled;
                LkpSituacion.Enabled = Enabled;
            }

        }

        private void clearControl()
        {   
            txtNumero.Text = "";
            deFecha.EditValue = DateTime.Today;            
            bteAlmacenDespacho.Tag = null;
            bteAlmacenDespacho.Text = "";
            bteAlmacenIngreso.Tag = null;
            bteAlmacenIngreso.Text = "";
            txtAtencion.Text = "";
            txtEntregar.Text = "";
            LkpMotivo.EditValue = null;            
            txtReferencia.Text = "";
            txtPartida.Text = "";
            LkpSituacion.EditValue = 343;            
            bteTransportista.Tag = null;
            bteTransportista.Text = "";          
            txtRUC.Text = ""; 
        }

        private void FrmManteOrdenDespachoAutoventa_Load(object sender, EventArgs e)
        {
            //Lista = new BOrdenDespachoAutoventa().BuscarOrdenDespachoAutoventaDet(code);
            //cargar();
            //grcDetalle.DataSource = Lista;
            //ListaFac = (new BFacturaCab()).ListarDocumentoVentaXtipoDocumentoDet(Parametros.intEjercicio, code);
            //grDocumento.DataSource = ListaFac;

            //List<EOrdenDespachoDetalle> MlistPedido = new List<EOrdenDespachoDetalle>();
            //MlistPedido = new BOrdenDespachoAutoventa().BuscarPedidoAutoventaDetalle(code_pedido);
            //gridControl1.DataSource = MlistPedido;
        } 

        private void cargar()
        {
            //BSControls.LoaderLook(LkpSituacion, new BTablaRegistro().ListarTablaRegistro(Parametros.intTablaSituacionOrdenDespaAutoventa).ToList(), "Descripcion", "IdTablaRegistro", false);
            //BSControls.LoaderLook(LkpMotivo, new BTablaRegistro().ListarTablaRegistro(Parametros.intTablaMotivoOrdenDespacho).Where(mot => mot.IdTablaRegistro == Parametros.intMotivoOrdenDespacho_Autoventa).ToList(), "Descripcion", "IdTablaRegistro", false); 
        }
        
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            clearControl();
            LkpMotivo.ItemIndex = 0;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;            
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }       

        private void FrmManteGuiRemision_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MiEvento();
        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void bteTransportista_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 0)
            //{
            //    using (FrmListarTransportistaListar frmTransportista = new FrmListarTransportistaListar())
            //    {
            //        if (frmTransportista.ShowDialog() == DialogResult.OK)
            //        {
            //            bteTransportista.Text = frmTransportista._Be.tranc_vnombre_transportista;
            //            bteTransportista.Tag = frmTransportista._Be.tranc_icod_transportista;
            //            txtRUC.Text = frmTransportista._Be.tranc_vruc;                 
            //        }
            //    }
            //}
        }

        private void bteAlmacenDespacho_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 0)
            //{
            //    using (FrmListarAlmacen frmAlmacen = new FrmListarAlmacen())
            //    {
            //        if (frmAlmacen.ShowDialog() == DialogResult.OK)
            //        {
            //            bteAlmacenDespacho.Tag = frmAlmacen._Be.id_almacen;
            //            bteAlmacenDespacho.Text = frmAlmacen._Be.descripcion;
            //            txtAtencion.Text = frmAlmacen._Be.representante;
            //            txtPartida.Text = frmAlmacen._Be.direccion;
            //        }
            //    }
            //}
        }

        private void bteAlmacenIngreso_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 0)
            //{
            //    using (FrmListarAlmacen frmAlmacen = new FrmListarAlmacen())
            //    {
            //        if (frmAlmacen.ShowDialog() == DialogResult.OK)
            //        {
            //            bteAlmacenIngreso.Tag = frmAlmacen._Be.id_almacen;
            //            bteAlmacenIngreso.Text = frmAlmacen._Be.descripcion;
            //        }
            //    }
            //}

        }       

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    FrmManteOrdenDespachoAutoventaDet frmdetalle = new FrmManteOrdenDespachoAutoventaDet();
            //    frmdetalle.mlistDetail = Lista;
            //    frmdetalle.almac_icod_almacen = Convert.ToInt32(bteAlmacenDespacho.Tag);
            //    frmdetalle.fecha =Convert.ToDateTime(deFecha.EditValue);
            //    frmdetalle.Text = "Detalle de Orden de Despacho Autoventa N°: " + txtNumero.Text;
            //    frmdetalle.txtItem.Text = (Lista.Count + 1).ToString();
            //    frmdetalle.btnModificar.Enabled = false;
            //    if (frmdetalle.ShowDialog() == DialogResult.OK)
            //    {
            //        EOrdenDespachoAutoventaDet _be = new EOrdenDespachoAutoventaDet();
            //        _be.ddeac_inro_item = frmdetalle._BE.ddeac_inro_item;
            //        _be.pespc_icod_producto_especifico = frmdetalle._BE.pespc_icod_producto_especifico;
            //        _be.Producto = frmdetalle._BE.Producto;
            //        _be.UME = frmdetalle._BE.UME;
            //        _be.Estado = frmdetalle._BE.Estado;
            //        _be.Situacion = frmdetalle._BE.Situacion;
            //        _be.Descripcion = frmdetalle._BE.Descripcion;
            //        _be.ddeac_ncantidad_despachada = frmdetalle._BE.ddeac_ncantidad_despachada;
            //        _be.tablc_iid_sit_item_ord_despacho = frmdetalle._BE.tablc_iid_sit_item_ord_despacho;
            //        _be.usuario = frmdetalle._BE.usuario;
            //        _be.pc = frmdetalle._BE.pc;
            //        _be.Generico = frmdetalle._BE.Generico;
            //        _be.operacion = frmdetalle._BE.operacion;
                  
            //        Lista.Add(_be);
            //        grcDetalle.RefreshDataSource();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    List<EStockProducto> mLista = new List<EStockProducto>();
            //    FrmManteOrdenDespachoAutoventaDet frmdetalle = new FrmManteOrdenDespachoAutoventaDet();
            //    frmdetalle.mlistDetail = Lista;
            //    frmdetalle.almac_icod_almacen = Convert.ToInt32(bteAlmacenDespacho.Tag);
            //    frmdetalle.Text = "Detalle de la Orden de Despacho Autoventa N°: " + txtNumero.Text;
            //    EOrdenDespachoAutoventaDet _obe = (EOrdenDespachoAutoventaDet)grvDetalle.GetRow(grvDetalle.FocusedRowHandle);
            //    frmdetalle.txtItem.Text = _obe.ddeac_inro_item.ToString();
            //    frmdetalle.btnProducto.Tag = _obe.pespc_icod_producto_especifico;
            //    frmdetalle.btnProducto.Text = _obe.Producto;
            //    frmdetalle.txtDescripcion.Text = _obe.Descripcion;
            //    frmdetalle.txtDCantidad.Text = _obe.ddeac_ncantidad_despachada.ToString();
            //    mLista = new BStockProducto().ListarStockProductoProducto(Parametros.intPeriodo, Convert.ToInt32(bteAlmacenDespacho.Tag), Convert.ToInt32(_obe.pespc_icod_producto_especifico));
            //    if (mLista.Count == 1)
            //    {
            //        if (_obe.ddeac_icod_detalle_despacho != 0)
            //        {
            //            frmdetalle.stock_producto = Convert.ToDecimal(mLista[0].stocc_nstock_prod + _obe.ddeac_ncantidad_despachada);
            //            frmdetalle.icod_familia_Produc = mLista[0].prodc_iid_familia;
            //        }
            //        else
            //        {
            //            frmdetalle.stock_producto = Convert.ToDecimal(mLista[0].stocc_nstock_prod);
            //            frmdetalle.icod_familia_Produc = mLista[0].prodc_iid_familia;
            //        }
            //    }
            //    frmdetalle.btnProducto.Enabled = false;
            //    frmdetalle.btnAgregar.Enabled = false;
            //    if (frmdetalle.ShowDialog() == DialogResult.OK)
            //    {
            //        _obe.ddeac_inro_item = frmdetalle._BE.ddeac_inro_item;
            //        _obe.ddeac_ncantidad_despachada = frmdetalle._BE.ddeac_ncantidad_despachada;
            //        _obe.tablc_iid_sit_item_ord_despacho = frmdetalle._BE.tablc_iid_sit_item_ord_despacho;
            //        _obe.usuario = frmdetalle._BE.usuario;
            //        _obe.pc = frmdetalle._BE.pc;

            //        if (_obe.ddeac_icod_detalle_despacho == 0)
            //            _obe.operacion = 1;
            //        else
            //        {
            //            if (_obe.operacion != 1)
            //            {
            //                _obe.operacion = 2;
            //            }
            //        }
            //        grcDetalle.RefreshDataSource();

            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EliminarDetalle();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void EliminarDetalle()
        {
            //EOrdenDespachoAutoventaDet obj = (EOrdenDespachoAutoventaDet)grvDetalle.GetRow(grvDetalle.FocusedRowHandle);
            //if (obj.operacion == 1)
            //{
            //    Lista.Remove(obj);
            //    grvDetalle.RefreshData();
            //    grvDetalle.MovePrev();
            //}
            //else
            //{
            //    //creo listado de eliminados para mandarlo a la BD para actualizar su estado
            //    if (Lista.Count != 1)
            //    {
            //        obj.operacion = 3;
            //        ListaEliminados.Add(obj);
            //        Lista.Remove(obj);
            //        grvDetalle.RefreshData();
            //        grvDetalle.MovePrev();
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("La orden de despacho de autoventa debe tener al menos un Item", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void SetSave()
        {
            //BaseEdit oBase = null;
            //Boolean Flag = true;
            //EOrdenDespachoAutoventa oBe = new EOrdenDespachoAutoventa();            
            //Obl = new BOrdenDespachoAutoventa();
            //try
            //{
            //    if (Lista.Count == 0)
            //    {
            //        throw new ArgumentException("Ingresar detalles");
            //    }
            //    if (Convert.ToInt32(txtcorrelativo.Text) == 0)
            //    {
            //        oBase = txtcorrelativo;
            //        throw new ArgumentException("Ingresar número");
            //    }
               
            //    if (deFecha.EditValue == null)
            //    {
            //        oBase = deFecha;
            //        throw new ArgumentException("Seleccionar una fecha");
            //    }                
               
            //    if (string.IsNullOrEmpty(txtAtencion.Text))
            //    {
            //        oBase = txtAtencion;
            //        throw new ArgumentException("Ingresar Atención");
            //    }
            //    if (string.IsNullOrEmpty(txtEntregar.Text))
            //    {
            //        oBase = txtEntregar;
            //        throw new ArgumentException("Ingresar Entregar");
            //    }
            //    if (string.IsNullOrEmpty(txtPartida.Text))
            //    {
            //        oBase = txtPartida;
            //        throw new ArgumentException("Ingresar Partida");
            //    }
               
            //    if (VerificarEjercicio(Convert.ToDateTime(deFecha.EditValue)) == false)
            //    {
            //        oBase = deFecha;
            //        throw new ArgumentException("La fecha seleccionada no esta dentro del ejercicio" + Parametros.intEjercicio);
            //    }
            //    if (Convert.ToInt32(btnpedido.Tag)==0)
            //    {
            //        oBase = btnpedido;
            //        throw new ArgumentException("La Orden de Despacho debe estar asociado a un Pedido");
            //    }
               
                
            //    oBe.desac_vnumero_despacho = txtNumero.Text + txtcorrelativo.Text;
            //    oBe.desac_sfecha_despacho = Convert.ToDateTime(deFecha.EditValue);                
            //    oBe.desac_iid_situacion_despacho = Convert.ToInt32(LkpSituacion.EditValue);
            //    oBe.tablc_iid_motivo_despacho = Convert.ToInt32(LkpMotivo.EditValue);
            //    oBe.almac_icod_almacen_salida = Convert.ToInt32(bteAlmacenDespacho.Tag);
            //    oBe.AlmacenSalida = bteAlmacenDespacho.Text;
            //    oBe.almac_icod_almacen_ingreso = Convert.ToInt32(bteAlmacenIngreso.Tag);
            //    oBe.AlmacenIngreso = bteAlmacenIngreso.Text;
            //    oBe.desac_vatencion = txtAtencion.Text;
            //    oBe.desac_ventregar = txtEntregar.Text + "@" + txtEntregar2.Text;
            //    oBe.desac_vreferencia = string.IsNullOrEmpty(txtReferencia.Text) ? null : txtReferencia.Text;
            //    oBe.desac_vpartida = txtPartida.Text;
            //    if(bteTransportista.Tag == null)
            //        oBe.tranc_icod_transportista = null;
            //    else
            //        oBe.tranc_icod_transportista = Convert.ToInt32(bteTransportista.Tag);
            //    oBe.Transportista = string.IsNullOrEmpty(bteTransportista.Text) ? null : bteTransportista.Text;
            //    oBe.tranc_vruc = string.IsNullOrEmpty(txtRUC.Text) ? null : txtRUC.Text;                                
            //    oBe.usuario = Valores.CodeUser;
            //    oBe.pc = WindowsIdentity.GetCurrent().Name.ToString();
            //    oBe.tipo_borden_despacho = true;
            //    oBe.desac_bautoriza_modif_dev = true;
            //    oBe.desac_icod_despacho_pedido = Convert.ToInt32(btnpedido.Tag);
            //    oBe.desac_bmodi_doc = desac_bmodi_doc;


            //    if (desac_bmodi_doc == true)
            //    {
            //        List<EOrdenDespachoDetalle> MlistAnterior = new List<EOrdenDespachoDetalle>();
            //        MlistAnterior = Obl.BuscarOrdenDespachoAutoventaDetalleAnterior(code);
            //        foreach (var _be in Lista)
            //        {
            //            foreach (var _beAnterior in MlistAnterior)
            //            {
            //                if (_be.pespc_icod_producto_especifico == _beAnterior.pespc_icod_producto_especifico)
            //                {
            //                    if (_be.ddeac_ncantidad_despachada > _beAnterior.ddeac_ncantidad_despachada)
            //                    {
            //                        Flag = false;
            //                        XtraMessageBox.Show("LA CANTIDAD " + _be.ddeac_ncantidad_despachada + " DEL PRODUCTO " + _be.Producto + " EXCEDE LA CANTIDAD ORIGINAL(" + _beAnterior.ddeac_ncantidad_despachada+ ")", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    if (Status == BSMaintenanceStatus.CreateNew)
            //    {
            //        oBe.desac_ianio = Parametros.intEjercicio;
            //        Obl.InsertarOrdenDespachoAutoventaNuevo(oBe, Lista, ListaFac);
            //    }
            //    else
            //    {                    
            //        oBe.desac_ianio =Parametros.intEjercicio;
            //        oBe.desac_icod_despacho = code;
            //        Obl.ActualizarOrdenDespachoAutoventaNuevo(oBe, Lista, ListaEliminados, ListaFac, ListaFacEliminados);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (oBase != null)
            //    {
            //        oBase.Focus();
            //        oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
            //        oBase.ErrorText = ex.Message;
            //        oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else                    
            //        XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    Flag = false;
            //}
            //finally
            //{
            //    if (Flag)
            //    {
            //        txtNumero.Enabled = !Enabled;
            //        this.MiEvento();
            //        this.Close();
            //    }
            //}
        }
        private bool VerificarEjercicio(DateTime sfecha)
        {
            bool Rpt;
            if (Parametros.intEjercicio == sfecha.Year)
            {
                Rpt = true;
            }
            else
            {
                Rpt = false;
            }
            return Rpt;
        }
        private void txtEntregar_EditValueChanged(object sender, EventArgs e)
        {
            if (txtEntregar.Text != "")
            {
                int cotador = txtEntregar.Text.IndexOf('@');
                if (cotador > 0)
                {
                    XtraMessageBox.Show("El caracter '@' no puede ser utilizado en este texto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtEntregar2.Enabled = true;
            }
        }

        private void txtEntregar2_EditValueChanged(object sender, EventArgs e)
        {
            int cotador = txtEntregar2.Text.IndexOf('@');
            if (cotador > 0)
            {
                XtraMessageBox.Show("El caracter '@' no puede ser utilizado en este texto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gcRemision_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    FrmManteOrdenDespachoDetRelacionDocumento frmdetalle = new FrmManteOrdenDespachoDetRelacionDocumento();
            //    frmdetalle.MlistFactura = ListaFac;
            //    frmdetalle.btnModificar.Enabled = false;
            //    frmdetalle.intmes = Convert.ToInt32(Convert.ToDateTime(deFecha.EditValue).Month);
            //    if (frmdetalle.ShowDialog() == DialogResult.OK)
            //    {
            //        EFacturaCab _be = new EFacturaCab();
            //        _be.factc_icod_factura = frmdetalle._BE.factc_icod_factura;
            //        _be.factc_vnumero_factura = frmdetalle._BE.factc_vnumero_factura;
            //        _be.icod_tipo_documento = frmdetalle._BE.icod_tipo_documento;
            //        _be.CantidadTotal = frmdetalle._BE.CantidadTotal;
            //        _be.String_tipo_documento = frmdetalle._BE.String_tipo_documento;
            //        _be.nmonto_Total = frmdetalle._BE.nmonto_Total;
            //        _be.factc_sfecha_factura = frmdetalle._BE.factc_sfecha_factura;
            //        _be.operacion = 1;
            //        ListaFac.Add(_be);
            //        grDocumento.RefreshDataSource();

            //        //Obtenemos el Detalle del Documento
            //        List<EFacturaDet> ListFacDet = new List<EFacturaDet>();
            //        if (Convert.ToInt32(frmdetalle._BE.icod_tipo_documento) == 26)
            //        {
            //            ListFacDet = new BFacturaCab().BuscarFacturaDetalleOD(frmdetalle._BE.factc_icod_factura);
                        
            //        }
            //        if (Convert.ToInt32(frmdetalle._BE.icod_tipo_documento) == 9)
            //        {
            //            List<EBoletaDet> ListBolDet = new List<EBoletaDet>();
            //            ListBolDet = new BBoletaCab().BuscarBoletaDetalleOD(frmdetalle._BE.factc_icod_factura);
            //            foreach (var _BE in ListBolDet)
            //            {
            //                EFacturaDet _BEDet = new EFacturaDet();
            //                _BEDet.pespc_icod_producto_especifico = _BE.pespc_icod_producto_especifico;
            //                _BEDet.producto = _BE.producto;
            //                _BEDet.unidc_vabreviatura_unidad_medida = _BE.unidc_vabreviatura_unidad_medida;
            //                _BEDet.Estado = _BE.estac_vdescripcion;
            //                _BEDet.dfacc_ncantidad_producto = Convert.ToDecimal(_BE.dbolc_ncantidad_producto);
            //                _BEDet.pespc_iid_producto_generico = _BE.codProducto;
            //                _BEDet.pespc_npeso_unitario = _BE.pespc_npeso_unitario;
            //                ListFacDet.Add(_BEDet);
            //            }
            //        }
            //        //--------------------------------------------------------------------------

            //        //listar el detalle de los productos
            //        foreach (var _BEE in ListFacDet)
            //        {
            //            foreach (var _DET in Lista)
            //            {
            //                if (_DET.pespc_icod_producto_especifico == _BEE.pespc_icod_producto_especifico)
            //                {
            //                    if (_DET.ddeac_icod_detalle_despacho != 0)
            //                    {
            //                        _DET.operacion = 2;
            //                    }
            //                    _DET.ddeac_ncantidad_despachada = Convert.ToDecimal(_DET.ddeac_ncantidad_despachada + _BEE.dfacc_ncantidad_producto);
            //                    _DET.ddeac_npeso_total_item = _DET.pespc_npeso_unitario * Convert.ToDecimal(_DET.ddeac_ncantidad_despachada);
            //                }
            //            }
            //            if (Lista.Count(ob => ob.pespc_icod_producto_especifico == _BEE.pespc_icod_producto_especifico) == 0)
            //            {
            //                EOrdenDespachoDetalle _BEODA = new EOrdenDespachoDetalle();
            //                _BEODA.ddeac_inro_item = Convert.ToInt16(Lista.Count + 1);
            //                _BEODA.pespc_icod_producto_especifico = Convert.ToInt32(_BEE.pespc_icod_producto_especifico);
            //                _BEODA.Producto = _BEE.producto;
            //                _BEODA.UME = _BEE.unidc_vabreviatura_unidad_medida;
            //                _BEODA.Estado = _BEE.Estado;
            //                _BEODA.Situacion = "ACTIVO";
            //                _BEODA.Descripcion = _BEE.producto;
            //                _BEODA.ddeac_ncantidad_despachada = Convert.ToDecimal(_BEE.dfacc_ncantidad_producto);
            //                _BEODA.tablc_iid_sit_item_ord_despacho = 1;
            //                _BEODA.usuario = 1;
            //                _BEODA.pc = WindowsIdentity.GetCurrent().Name.ToString();
            //                _BEODA.Generico = _BEE.pespc_iid_producto_generico;
            //                _BEODA.pespc_npeso_unitario = _BEE.pespc_npeso_unitario;
            //                _BEODA.ddeac_npeso_total_item = _BEE.pespc_npeso_unitario * Convert.ToDecimal(_BEE.dfacc_ncantidad_producto);
            //                _BEODA.operacion = 1;
            //                Lista.Add(_BEODA);  
            //            }
                      
            //        }

            //        grcDetalle.RefreshDataSource();
            //        SumarPesos();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }


        public void SumarPesos()
        {
            decimal sumPesosTotal = Lista.Sum(ob => ob.ddeac_npeso_total_item);
            txttotalPesos.Text = sumPesosTotal.ToString();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (ListaFac.Count > 0)
            {
                if (XtraMessageBox.Show("Esta seguro de eliminar", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    EliminarDetalleFact();
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void EliminarDetalleFact()
        {
            //EFacturaCab obj = (EFacturaCab)ViewDocumento.GetRow(ViewDocumento.FocusedRowHandle);

            ////Obtenemos el Detalle del Documento
            //List<EFacturaDet> ListFacDet = new List<EFacturaDet>();
            //if (Convert.ToInt32(obj.icod_tipo_documento) == 26)
            //{
            //    ListFacDet = new BFacturaCab().BuscarFacturaDetalleOD(obj.factc_icod_factura);

            //}
            //if (Convert.ToInt32(obj.icod_tipo_documento) == 9)
            //{
            //    List<EBoletaDet> ListBolDet = new List<EBoletaDet>();
            //    ListBolDet = new BBoletaCab().BuscarBoletaDetalleOD(obj.factc_icod_factura);
            //    foreach (var _BE in ListBolDet)
            //    {
            //        EFacturaDet _BEDet = new EFacturaDet();
            //        _BEDet.pespc_icod_producto_especifico = _BE.pespc_icod_producto_especifico;
            //        _BEDet.producto = _BE.producto;
            //        _BEDet.unidc_vabreviatura_unidad_medida = _BE.unidc_vabreviatura_unidad_medida;
            //        _BEDet.Estado = _BE.estac_vdescripcion;
            //        _BEDet.dfacc_ncantidad_producto = Convert.ToDecimal(_BE.dbolc_ncantidad_producto);
            //        _BEDet.pespc_iid_producto_generico = _BE.codProducto;
            //        ListFacDet.Add(_BEDet);
            //    }
            //}
            ////--------------------------------------------------------------------------

            ////listar el detalle de los productos
            //List<EOrdenDespachoAutoventaDet> MlistEliminarOD = new List<EOrdenDespachoAutoventaDet>();
            //foreach (var _BEE in ListFacDet)
            //{
            //    foreach (var _DET in Lista)
            //    {
            //        if (_DET.pespc_icod_producto_especifico == _BEE.pespc_icod_producto_especifico)
            //        {
            //            _DET.ddeac_ncantidad_despachada = Convert.ToDecimal(_DET.ddeac_ncantidad_despachada - _BEE.dfacc_ncantidad_producto);
            //            _DET.ddeac_npeso_total_item = _DET.pespc_npeso_unitario * Convert.ToDecimal(_DET.ddeac_ncantidad_despachada);
            //            _DET.operacion = 2;
            //            if (Convert.ToDecimal(_DET.ddeac_ncantidad_despachada) == 0)
            //            {
            //                MlistEliminarOD.Add(_DET);
            //            }
            //        }
            //    }
            //}
            //foreach (var _eliOD in MlistEliminarOD)
            //{
            //    Lista.Remove(_eliOD);
            //    ListaEliminados.Add(_eliOD);
            //    ViewDocumento.RefreshData();
            //    ViewDocumento.MovePrev();
            //}



            //grcDetalle.RefreshDataSource();


            //if (obj.operacion == 1)
            //{
            //    ListaFac.Remove(obj);
            //    ViewDocumento.RefreshData();
            //    ViewDocumento.MovePrev();
            //}
            //else
            //{
            //    //creo listado de eliminados para mandarlo a la BD para actualizar su estado
            //    if (ListaFac.Count != 1)
            //    {
            //        obj.operacion = 3;
            //        ListaFacEliminados.Add(obj);
            //        ListaFac.Remove(obj);
            //        ViewDocumento.RefreshData();
            //        ViewDocumento.MovePrev();
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("La orden de despacho de autoventa debe tener al menos un Item", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
        }

        private void btnpedido_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (FrmListarPedido frmTransportista = new FrmListarPedido())
            //{
            //    if (frmTransportista.ShowDialog() == DialogResult.OK)
            //    {
            //        btnpedido.Text = frmTransportista._Be.desac_vnumero_despacho;
            //        btnpedido.Tag = frmTransportista._Be.desac_icod_despacho;
            //        bteAlmacenDespacho.Text = frmTransportista._Be.AlmacenSalida;
            //        bteAlmacenDespacho.Tag = frmTransportista._Be.almac_icod_almacen_salida;
            //        bteAlmacenIngreso.Text = frmTransportista._Be.AlmacenIngreso;
            //        bteAlmacenIngreso.Tag = frmTransportista._Be.almac_icod_almacen_ingreso;
            //        List<EOrdenDespachoAutoventaDet> MlistPedido = new List<EOrdenDespachoAutoventaDet>();
            //        MlistPedido = new BOrdenDespachoAutoventa().BuscarPedidoAutoventaDetalle(frmTransportista._Be.desac_icod_despacho);
            //        gridControl1.DataSource = MlistPedido;
            //    }
            //}
        }


        
      
    }
}