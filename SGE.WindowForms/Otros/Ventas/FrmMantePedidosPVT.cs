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
using DevExpress.XtraGrid.Views.Grid;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Otros.Almacen.Listados;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;

namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    public partial class FrmMantePedidosPVT : DevExpress.XtraEditors.XtraForm
    {
        public EPedidosPVT oBe = new EPedidosPVT();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<EPedidosPVTDetalle> lstPVTDetalle = new List<EPedidosPVTDetalle>();
        List<EPedidosPVTDetalle> lstDelete = new List<EPedidosPVTDetalle>();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int IcodRubros = 0;
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
                bteCliente.Enabled = true;   
            }
        }

        public void setValues()
        {
            txtNumero.Text = oBe.pdvc_numero_pedido;
            dteFecha.EditValue = oBe.pdvc_sfecha_pedido;
            bteCliente.Tag = oBe.cliec_icod_cliente;
            bteCliente.Text =string.Format(oBe.pdvc_vcliente);
            lstPVTDetalle = new BVentas().listarPedidosPVTDetalle(oBe.pdvc_icod_pedido);
            grdPVTDetalle.DataSource = lstPVTDetalle;

        }

        public FrmMantePedidosPVT()
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
        }

        private void cargar()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
            }

            grdPVTDetalle.DataSource = lstPVTDetalle;

           
        }
        public void CargarControles()
        {
              
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }
        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;      
            try 
            {
                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de Requerimiento");
                }

                oBe.pdvc_numero_pedido = txtNumero.Text;
                oBe.pdvc_sfecha_pedido = Convert.ToDateTime(dteFecha.Text);
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.pdvc_vobservaciones = txtObservaciones.Text;
               
                oBe.pdvc_vcliente = bteCliente.Text;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.tablc_iid_situación = 645;
                    oBe.pdvc_icod_pedido = new BVentas().insertarPedidosPVT(oBe, lstPVTDetalle);

                }
                else
                {
                    new BVentas().modificarPedidosPVT(oBe, lstPVTDetalle,lstDelete);
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
                    MiEvento(oBe.pdvc_icod_pedido);
                    Close();
                }
            }
        }

        private void FrmManteFactura_Load(object sender, EventArgs e)
        {
            cargar();
        
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
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (obe == null)
                return;           
                modificarItem();                 
        }

        private void modificarItem()
        {
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (obe == null)
                return;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPedidosPVTDetalle obe = (EPedidosPVTDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstPVTDetalle.Remove(obe);
            viewPVTDetalle.RefreshData();
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

    
        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EPedidosPVTDetalle obe = (EPedidosPVTDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstPVTDetalle.Remove(obe);
            viewPVTDetalle.RefreshData();
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EPedidosPVTDetalle item = new EPedidosPVTDetalle();
           
                if (lstPVTDetalle.Count == 0)
                {
                    item.pdvd_iid_pedido_detalle = 1;
                }
                else
                item.pdvd_iid_pedido_detalle = lstPVTDetalle.Max(x => x.pdvd_iid_pedido_detalle) + 1;
                item.pdvc_icod_pedido = 0;
                item.prdc_icod_producto = 0;
                item.pdvd_ncantidad = 1;
                item.CodPro = "";
                item.DesLarga = "";
                item.DesCorta = "";
                item.AbreUM = "";
                item.pdvd_nprecio_unitario = 0;
                item.pdvd_nprecio_total = 0;
                item.Indicador = 0;
                item.intUsuario = Valores.intUsuario;
                item.strPc = WindowsIdentity.GetCurrent().Name;
                lstPVTDetalle.Add(item);
                grdPVTDetalle.RefreshDataSource();
        }

        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewPVTDetalle_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            EPedidosPVTDetalle item = (EPedidosPVTDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (item == null)
                return;
            List<ECodigoBarra> lstProductoCodBarra = new List<ECodigoBarra>();
            lstProductoCodBarra = new BAlmacen().listarCodigoBarraTodo();
            foreach (var CB in lstProductoCodBarra)
            {
                if (item.CodPro == CB.codb_iid_codigo_barra)
                {
                    List<EProducto> lstProducto = new List<EProducto>();
                    lstProducto = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(a => a.prdc_icod_producto == CB.prdc_icod_producto).ToList();
                    foreach (var PR in lstProducto)
                    {
                        item.prdc_icod_producto = PR.prdc_icod_producto;
                        item.DesLarga = PR.prdc_vdescripcion_larga;
                       // item.DesCorta = PR.prdc_vdescripcion_corta;
                        item.AbreUM = PR.StrUnidadMedida;
                        CalcularPrecio();
                    }
                }
                item.pdvd_nprecio_total = Math.Round(Convert.ToDecimal(item.pdvd_nprecio_unitario * item.pdvd_ncantidad), 2);
            }
            if (view.FocusedColumn.FieldName == "pdvd_ncantidad")
            {
                List<EListaPrecio> lstListaPrecios = new List<EListaPrecio>();
                lstListaPrecios = new BVentas().listarListaPrecio().Where(x => x.prdc_icod_producto == Convert.ToInt32(item.prdc_icod_producto)).ToList();
                item.pdvd_nprecio_unitario = 0;
                lstListaPrecios.ForEach(x =>
                {
                    decimal Cantidad = 0;
                    /*Cuando No tiene Detalle de Rango Se pone el Mismo Precio*/
                    Cantidad = Convert.ToDecimal(item.pdvd_ncantidad);
                    if (x.lprecc_nmonto_unitario != 0 && x.lprecc_indicador_rango == false)
                    {
                        item.pdvd_nprecio_unitario = x.lprecc_nmonto_unitario;
                    }
                    if (x.lprecc_nmonto_unitario == 0 && x.lprecc_indicador_rango == true)
                    {
                        List<EListaPrecioDetalle> lstListaPreciosDetalle = new List<EListaPrecioDetalle>();
                        lstListaPreciosDetalle = new BVentas().listarListaPrecioDetalle(x.lprecc_icod_precio);
                        lstListaPreciosDetalle.ForEach(xd =>
                        {

                            if (Cantidad >= xd.lprecd_icantidad_inicial && Cantidad < xd.lprecd_icantidad_final)
                            {
                                item.pdvd_nprecio_unitario = xd.lprecd_nmonto_unitario;
                            }

                        });
                    }
                    item.pdvd_nprecio_total = Math.Round(Convert.ToDecimal(item.pdvd_nprecio_unitario * item.pdvd_ncantidad), 2);
                });
            }
        }
        private void CalcularPrecio()
        {
            EPedidosPVTDetalle item = (EPedidosPVTDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (item == null)
                return;
            List<EListaPrecio> lstListaPrecios = new List<EListaPrecio>();
            lstListaPrecios = new BVentas().listarListaPrecio().Where(x => x.prdc_icod_producto == Convert.ToInt32(item.prdc_icod_producto)).ToList();
            item.pdvd_nprecio_unitario = 0;
            lstListaPrecios.ForEach(x =>
            {
                decimal Cantidad = 0;
                /*Cuando No tiene Detalle de Rango Se pone el Mismo Precio*/
                Cantidad = Convert.ToDecimal(item.pdvd_ncantidad);
                if (x.lprecc_nmonto_unitario != 0 && x.lprecc_indicador_rango == false)
                {
                    item.pdvd_nprecio_unitario = x.lprecc_nmonto_unitario;
                }
                if (x.lprecc_nmonto_unitario == 0 && x.lprecc_indicador_rango == true)
                {
                    List<EListaPrecioDetalle> lstListaPreciosDetalle = new List<EListaPrecioDetalle>();
                    lstListaPreciosDetalle = new BVentas().listarListaPrecioDetalle(x.lprecc_icod_precio);
                    lstListaPreciosDetalle.ForEach(xd =>
                    {

                        if (Cantidad >= xd.lprecd_icantidad_inicial && Cantidad < xd.lprecd_icantidad_final)
                        {
                            item.pdvd_nprecio_unitario = xd.lprecd_nmonto_unitario;
                        }

                    });
                }
                item.pdvd_nprecio_total = Math.Round(Convert.ToDecimal(item.pdvd_nprecio_unitario * item.pdvd_ncantidad), 2);
            });
        }
        private void Listar_Producto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }
        private void listarProducto()
        {
            EPedidosPVTDetalle item = (EPedidosPVTDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (item == null)
                return;
            try
            {
                frmListarProducto frm = new frmListarProducto();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    item.prdc_icod_producto = frm._Be.prdc_icod_producto;
                    item.DesLarga = frm._Be.prdc_vdescripcion_larga;
                   /// item.DesCorta = frm._Be.prdc_vdescripcion_corta;
                    item.AbreUM = frm._Be.StrUnidadMedida;
                    item.pdvd_nprecio_unitario = frm._Be.prdc_nPrecio_soles;
                    item.pdvd_nprecio_total = Math.Round(Convert.ToDecimal(item.pdvd_nprecio_unitario * item.pdvd_ncantidad), 2);
                    item.Indicador = 1;
                    CalcularPrecio();
                    grdPVTDetalle.Refresh();
                    grdPVTDetalle.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Eliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EPedidosPVTDetalle obe = (EPedidosPVTDetalle)viewPVTDetalle.GetRow(viewPVTDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstPVTDetalle.Remove(obe);
            viewPVTDetalle.RefreshData();
        }
        private void viewPVTDetalle_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn Flag = viewPVTDetalle.Columns["Indicador"];
            int Indicador = Convert.ToInt32(viewPVTDetalle.GetRowCellValue(viewPVTDetalle.FocusedRowHandle, Flag));
            if (view.FocusedColumn.FieldName == "CodPro")
            {
                if (Indicador == 1)
                {
                    XtraMessageBox.Show("La Lista Se Encuentra Habilitada");
                }
            }
        }

    }    
}