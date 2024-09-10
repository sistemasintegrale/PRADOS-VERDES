using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Administracion_del_Sistema.Listados;
using SGE.WindowForms.Reportes.Almacen.Registros;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteReporteConversion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteNotaIngreso));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /*----------------------------------------------------*/
        public EReporteConversionCab oBe = new EReporteConversionCab();
        /*----------------------------------------------------*/
        List<EReporteConversionDet> lstDetalle = new List<EReporteConversionDet>();
        List<EReporteConversionDet> lstDelete = new List<EReporteConversionDet>();
        /*----------------------------------------------------*/
        public List<EReporteConversionCab> lstCabecerasNI = new List<EReporteConversionCab>();
        /*----------------------------------------------------*/        
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
        public frmManteReporteConversion()
        {
            InitializeComponent();
        }

        private void FrmManteNotaIngreso_Load(object sender, EventArgs e)
        {
            cargarControles();    
            Carga();
            if (Parametros.intEjercicio == DateTime.Now.Year)
                dteFecha.EditValue = DateTime.Now;
            else
                dteFecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
            lkpTipo.EditValue = 239;//EMPAQUE

        }
        private void cargarControles()
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(52), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }
        private void Carga()
        {
            lstDetalle = new BAlmacen().ReporteConversionDetListar(oBe.rcc_icod_reporte_conversion);
            grdDetalle.DataSource = lstDetalle;
        }
     
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtNumero.Enabled = !Enabled;
            dteFecha.Enabled = !Enabled;
            txtUm.Enabled = !Enabled;
            
            txtObservaciones.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteAlmacen.Enabled = false;
                lkpTipo.Enabled = false;
            }
        
        
        }

        public void setValues()
        {
            txtNumero.Text = oBe.rcc_vnuemro_reporte_conversion;
            dteFecha.EditValue = oBe.rcc_sfecha;
            lkpTipo.EditValue = oBe.tablc_itipo_conversion;
            bteAlmacen.Tag = oBe.almac_icod_almacen;
            bteAlmacen.Text = oBe.almac_vdescripcion;
            btnProducto.Tag = oBe.prdc_icod_producto;
            btnProducto.Text = oBe.prdc_vdescripcion_larga;
            txtObservaciones.Text = oBe.rcc_vobservaciones;
            txtUm.Text = oBe.unidc_vabreviatura_unidad_medida;
            txtCantidad.Text = oBe.rcc_dcantidad_conversion.ToString();
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

      
      
        public void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            try
            {            
                if (Convert.ToInt32(bteAlmacen.Tag) == 0)
                {
                    oBase = bteAlmacen;
                    throw new ArgumentException("Seleccione Almacén");
                }
                
                if (Convert.ToDateTime(dteFecha.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada no esta dentro del año de ejercicio" + Parametros.intEjercicio);
                }
                if (Convert.ToDecimal(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese Cantidad");
                }
                /*---------------------------------------------------------*/
                oBe.rcc_vnuemro_reporte_conversion=txtNumero.Text ;
                oBe.rcc_sfecha=Convert.ToDateTime(dteFecha.EditValue);
                oBe.tablc_itipo_conversion=Convert.ToInt32(lkpTipo.EditValue);
                oBe.prdc_icod_producto = Convert.ToInt32(btnProducto.Tag);
                oBe.almac_icod_almacen=Convert.ToInt32(bteAlmacen.Tag );
                oBe.rcc_vobservaciones=txtObservaciones.Text;
                oBe.rcc_flag_estado = true;
                oBe.rcc_dcantidad_conversion = Convert.ToDecimal(txtCantidad.Text);
                oBe.prdc_vdescripcion_larga = btnProducto.Text;
                /*---------------------------------------------------------*/
                
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BAlmacen().ReporteConversionInsertar(oBe, lstDetalle);
                }
                else
                {
                    new BAlmacen().ReporteConversionModificar(oBe, lstDetalle, lstDelete);
                }
                /*-------------------------------------------------*/
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;                    
                }
                XtraMessageBox.Show(ex.Message, "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.rcc_icod_reporte_conversion);
                    //imprimir();
                    this.Close();
                }
            }
        }
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnProducto_DoubleClick(object sender, EventArgs e)
        {
            
        }


        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void bteAlmacen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarAlmacen();
        }
        private void listarAlmacen()
        {
            try
            {
                frmListarAlmacen frm = new frmListarAlmacen();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteAlmacen.Tag = frm._Be.almac_icod_almacen;
                    bteAlmacen.Text = frm._Be.almac_vdescripcion;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProducto_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }
        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                //frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = frm._Be.prdc_icod_producto;
                    btnProducto.Text = frm._Be.prdc_vdescripcion_larga;
                    //txtDescripcion.Text = frm._Be.prdc_vdescripcion_larga;
                    txtUm.Text = frm._Be.StrUnidadMedida;
                    //Categoria = frm._Be.strCategoria;
                    //SubCategoria = frm._Be.strSubCategoriaUno;
                }
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                using (FrmManteReporteConversionDetalle frm = new FrmManteReporteConversionDetalle())
                {
                    //frm.obeCab = oBe;
                    frm.lstDetalle = lstDetalle;
                    frm.SetInsert();
                    frm.txtitem.Text = (lstDetalle.Count + 1).ToString();
                    frm.intAlmacen = Convert.ToInt32(bteAlmacen.Tag);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //lstDetalle = frm.lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.MoveLast();
                        viewDetalle.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EReporteConversionDet obe = (EReporteConversionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    using (FrmManteReporteConversionDetalle frm = new FrmManteReporteConversionDetalle())
                    {
                        frm.oBe = obe;
                        //frm.obeCab = oBe;
                        frm.lstDetalle = lstDetalle;
                        frm.SetModify();
                        frm.setValues();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            lstDetalle = frm.lstDetalle;
                            viewDetalle.RefreshData();
                            viewDetalle.FocusedRowHandle = index;
                            viewDetalle.Focus();
                       
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EReporteConversionDet obe = (EReporteConversionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
                if (obe != null)
                {
                    int index = viewDetalle.FocusedRowHandle;
                    lstDelete.Add(obe);
                    lstDetalle.Remove(obe);
                    viewDetalle.RefreshData();
                    viewDetalle.FocusedRowHandle = index;
                    viewDetalle.Focus();
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}