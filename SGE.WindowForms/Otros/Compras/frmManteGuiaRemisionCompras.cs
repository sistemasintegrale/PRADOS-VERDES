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

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmManteGuiaRemisionCompras : DevExpress.XtraEditors.XtraForm
    {
        public EGuiaRemisionCompras oBe = new EGuiaRemisionCompras();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
       public List<EGuiaRemisionComprasDetalle> lstDetalle = new List<EGuiaRemisionComprasDetalle>();
        List<EGuiaRemisionComprasDetalle> lstDelete = new List<EGuiaRemisionComprasDetalle>();

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
            bteProveedor.Enabled = !Enabled;
            lkpMotivo.Enabled = !Enabled;
            txtNombreComercial.Enabled = !Enabled;
            txtLicencia.Enabled = !Enabled;
            txtMatricula.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = !Enabled;
                txtNumero.Enabled = !Enabled;
                dteFecha.Enabled = !Enabled;
                btnConsultar.Enabled = !Enabled;
                bteProveedor.Enabled = !Enabled;
                btnOrdenCompra.Enabled = !Enabled;
            }
        }
        private void setValues()
        {
            txtSerie.Text = oBe.grcc_vnumero_grc.Substring(0, 4);
            txtNumero.Text = oBe.grcc_vnumero_grc.Substring(4, oBe.grcc_vnumero_grc.Length - 4);
            dteFecha.EditValue = oBe.grcc_sfecha_grc;
            lkpMotivo.EditValue = oBe.tablc_iid_motivo;
            lkpSituacion.EditValue = oBe.tablc_iid_situacion_grc;;
            dteFechaIngreso.EditValue = oBe.grcc_sfecha_ingreso;
            bteProveedor.Tag = oBe.proc_icod_proveedor;
            bteProveedor.Text = oBe.NomProveedor;
            btnOrdenCompra.Tag = oBe.ococ_icod_orden_compra;
            btnOrdenCompra.Text = oBe.NumOC;
            btnAlmacenIngreso.Tag = oBe.almac_icod_almacen;
            btnAlmacenIngreso.Text = oBe.DesAlmacen;

           lstDetalle = new BCompras().listarGuiaRemisionComprasDetalle(oBe.grcc_icod_grc);
           lstDetalle.OrderBy(E=> E.grcd_iid_detalle);
           grdGuiaRemision.DataSource = lstDetalle;
           lstDetalle.ForEach(x => 
           {
               x.grcd_ncantidad2 = x.grcd_ncantidad;
           });
        }

        public frmManteGuiaRemisionCompras()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            SetFecha();
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
            setValues();
            ConsultarDetalleModificar();           
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
            setValues();
        }
        private void frmManteGuiaRemisionCompras_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            CargarControles();
            
        }
        private void CargarControles()
        {
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMotivoGuiaRemision), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void SetFecha()
        {
            dteFecha.EditValue = DateTime.Now;
            dteFechaIngreso.EditValue = DateTime.Now;
        
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            using (frmManteGuiaRemisionDetalleCompras frm = new frmManteGuiaRemisionDetalleCompras())
            {   
                frm.oBeCab = oBe;
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;                
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                    CalcularMontos();
                }
            }
        }
        public void CalcularMontos()
        {            
            txtCantTotal.Text = lstDetalle.Sum(ob => ob.CantidadSaldo).ToString();
            txtItems.Text = lstDetalle.Count().ToString();         
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

                if (Convert.ToInt32(bteProveedor.Tag) == 0)
                {
                    oBase = bteProveedor;
                    throw new ArgumentException("Seleccione el Proveedor");
                }
                oBe.proc_icod_proveedor = Convert.ToInt32(bteProveedor.Tag);
                oBe.ococ_icod_orden_compra = Convert.ToInt32(btnOrdenCompra.Tag);
                oBe.grcc_vnumero_grc = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.grcc_sfecha_grc = Convert.ToDateTime(dteFecha.Text);
                oBe.tablc_iid_motivo = Convert.ToInt32(lkpMotivo.EditValue);
                oBe.tablc_iid_situacion_grc = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.grcc_ncantidad =Convert.ToDecimal(txtCantTotal.Text);             
				oBe.grcc_sfecha_ingreso=Convert.ToDateTime(dteFechaIngreso.EditValue);
				oBe.grcc_ncantidad=Convert.ToDecimal(txtCantTotal.Text);
                oBe.almac_icod_almacen = Convert.ToInt32(btnAlmacenIngreso.Tag);
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.grcc_flag_estatdo = true;
                if (Status == BSMaintenanceStatus.CreateNew)
                {                    
                    oBe.grcc_icod_grc = new BCompras().insertarGuiaRemisionCompras(oBe, lstDetalle);
                }
                else
                {
                    new BCompras().modificarGuiaRemisionCompras(oBe, lstDetalle, lstDelete);
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
                    MiEvento(oBe.grcc_icod_grc);
                    Close();
                }
            }
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
            EGuiaRemisionComprasDetalle obe = (EGuiaRemisionComprasDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            using (frmManteGuiaRemisionDetalleCompras frm = new frmManteGuiaRemisionDetalleCompras())
            {
                
                frm.oBe = obe;
                frm.lstDetalle = lstDetalle;
                //frm.Cantidad = obe.grcd_ncantidad;             
                frm.SetModify();                                              
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewGuiaRemision.RefreshData();
                    viewGuiaRemision.MoveLast();
                    CalcularMontos();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemisionComprasDetalle obe = (EGuiaRemisionComprasDetalle)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            viewGuiaRemision.RefreshData();
            CalcularMontos();
        }
        private void btnAlmacenIngreso_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }
        private void listarAlmacenIngreso()
        {
            using (frmListarAlmacen frm = new frmListarAlmacen())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnAlmacenIngreso.Tag = frm._Be.almac_icod_almacen;
                    btnAlmacenIngreso.Text = frm._Be.almac_vdescripcion;

                }
            }
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
        private void txtPorIGV_EditValueChanged(object sender, EventArgs e)
        {
            CalcularMontos();
        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacenIngreso();
        }     
        private void bteProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProveedores();
        }
        private void ListarProveedores()
        {           
            FrmListarProveedor Proveedor = new FrmListarProveedor();
            Proveedor.Carga();
            if (Proveedor.ShowDialog() == DialogResult.OK)
            {
                bteProveedor.Tag = Proveedor._Be.iid_proveedor;
                bteProveedor.Text = Proveedor._Be.vnombrecompleto;
            }       
        }
        private void btnOrdenCompra_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarOrdenCompra();
        }
        private void ListarOrdenCompra()
        {
            FrmListarOrdenCompra OrdenCompra = new FrmListarOrdenCompra();
            OrdenCompra.proc_icod_proveedor =Convert.ToInt32(bteProveedor.Tag);
            OrdenCompra.Carga();
            if (OrdenCompra.ShowDialog() == DialogResult.OK)
            {
                btnOrdenCompra.Tag = OrdenCompra._Be.ococ_icod_orden_compra;
                btnOrdenCompra.Text = OrdenCompra._Be.ococ_numero_orden_compra;
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarDetalle();    
        }
        public void ConsultarDetalle()
        {
            List<EOrdenCompra> lstOrdenCompraDetalle = new List<EOrdenCompra>();
            lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraDetalle(Convert.ToInt32(btnOrdenCompra.Tag));
            foreach (var _bee in lstOrdenCompraDetalle)
            {
                EGuiaRemisionComprasDetalle BGRCOMPRAS = new EGuiaRemisionComprasDetalle();
                BGRCOMPRAS.grcd_iid_detalle = _bee.ocod_iitem;
                BGRCOMPRAS.strCodProd = _bee.strCodigoProducto;
                BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                BGRCOMPRAS.grcd_flag_estado = _bee.ocod_flag_estado;
                BGRCOMPRAS.grcd_ncantidad = BGRCOMPRAS.grcd_ncantidad;
                BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                lstDetalle.Add(BGRCOMPRAS);
                lstDetalle.OrderBy(E=> E.grcd_iid_detalle).ToList();
                grdGuiaRemision.DataSource = lstDetalle;
                CalcularMontos();
            }
            btnConsultar.Enabled = false;
            grdGuiaRemision.RefreshDataSource();

        }
        public void ConsultarDetalleModificar()
        {
            List<EOrdenCompra> lstOrdenCompraDetalle = new List<EOrdenCompra>();
            List<EGuiaRemisionComprasDetalle> lstDetalleOrdenada = new List<EGuiaRemisionComprasDetalle>();
            lstOrdenCompraDetalle = new BCompras().ListarOrdenCompraDetalle(Convert.ToInt32(btnOrdenCompra.Tag)).Where(x => x.ocod_ncantidad_facturada == 0).ToList();
            foreach (var _bee in lstOrdenCompraDetalle)
            {               
                EGuiaRemisionComprasDetalle BGRCOMPRAS = new EGuiaRemisionComprasDetalle();
                BGRCOMPRAS.grcd_iid_detalle = _bee.ocod_iitem;
                BGRCOMPRAS.strCodProd = _bee.strCodigoProducto;
                BGRCOMPRAS.DesProducto = _bee.strDescProducto;
                BGRCOMPRAS.Unidad = _bee.strAbrevUniMed;
                BGRCOMPRAS.ocod_icod_detalle_oc = _bee.ocod_icod_detalle_oc;
                BGRCOMPRAS.prdc_icod_producto = Convert.ToInt32(_bee.prdc_icod_producto);
                BGRCOMPRAS.grcd_flag_estado = _bee.ocod_flag_estado;                
                BGRCOMPRAS.CantidadSaldo = _bee.ocod_ncantidad;
                BGRCOMPRAS.CantidadRecibida = _bee.ocod_ncantidad_facturada;
                BGRCOMPRAS.grcd_ncantidad = BGRCOMPRAS.grcd_ncantidad;              
                lstDetalle.Add(BGRCOMPRAS);
                lstDetalleOrdenada = lstDetalle.OrderBy(x=> x.grcd_iid_detalle).ToList();
                grdGuiaRemision.DataSource = lstDetalleOrdenada;
                CalcularMontos();             
            }
            btnConsultar.Enabled = false;
            grdGuiaRemision.RefreshDataSource();         
        }
    }    
}