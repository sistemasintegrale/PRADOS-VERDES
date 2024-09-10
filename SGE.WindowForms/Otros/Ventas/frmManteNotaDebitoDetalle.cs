using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using System.Linq;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.Security.Principal;
using SGE.WindowForms.Otros.Operaciones;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteNotaDebitoDetalle : DevExpress.XtraEditors.XtraForm
    {
        public List<ENotaDebitoDet> lstDetalle = new List<ENotaDebitoDet>();
        
        public ENotaDebitoDet oBe = new ENotaDebitoDet();

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;

        public frmManteNotaDebitoDetalle()
        {
            InitializeComponent();
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {                
               
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
            
        }      

        public void setValues()
        {

            txtCantidad.Text = oBe.ddebc_ncantidad_producto.ToString();

            string[] partes = partes = oBe.ddebc_vdescripcion.Split('@');
            txtObservaciones.Lines = partes;
            txtMonto.Text = oBe.ddebc_nmonto_item.ToString();
            txtPrecioTotal.Text = oBe.ddebc_nmonto_total.ToString();
            //lkpMoneda.EditValue = oBeDetFav.;
            //btePersonal.Tag = oBeDetFav.otss_icod_personal;
            //btePersonal.Text = oBeDetFav.strPersonal;
            //txtAreaPersonal.Text = oBeDetFav.otss_area_personal;
            //dtFecha.EditValue = oBeDetFav.otss_fecha_servicio;
            //txtProductividad.Text = oBeDetFav.otss_nporc_productividad.ToString();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            try
            {               
               

                if (Convert.ToDecimal(txtCantidad.Text) <= 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("Ingrese el Monto");
                }

                oBe.ddebc_inro_item = Convert.ToInt16(txtItem.Text);
               // oBe.prdc_icod_producto = null;
                oBe.ddebc_ncantidad_producto = Convert.ToDecimal(txtCantidad.Text);                
                oBe.ddebc_nmonto_unitario = Convert.ToDecimal(txtMonto.Text);
               // oBe.ddebc_nmonto_item = Convert.ToDecimal(txtMonto.Text);
                oBe.ddebc_nmonto_total = Convert.ToDecimal(txtPrecioTotal.Text);
                //oBe.intClasificacion = null;                

                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                string Descripci = "";
                string DescripciExtra = "";
                string[] arraye = txtObservaciones.Lines;
                for (int i = 0; i < arraye.Length; i++)
                {
                    Descripci = Descripci + arraye[i] + "";
                    if (arraye[i] != "")
                        DescripciExtra = DescripciExtra + (i + 1).ToString() + "." + arraye[i] + " ";
                }


                oBe.ddebc_vdescripcion = Descripci;

                if (Status == BSMaintenanceStatus.CreateNew)
                {                   
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else
                {
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;
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

        private void listarPersonal()
        {
            frmListarPersonal frm = new frmListarPersonal();
            frm.flag_personal_all = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btePersonal.Tag = frm._Be.perc_icod_personal;
                btePersonal.Text = frm._Be.perc_vapellidos_nombres;
                txtAreaPersonal.Text = frm._Be.strArea;
            }
        }

        private void listarServicios()
        {
            BaseEdit oBase = null;
            try
            {
               

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

        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarServicios();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

       

        private void bteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarServicios();
        }

        private void btePersonal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarPersonal();
        }

        private void btePersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                listarPersonal();
        }

        private void calcularProductividad()
        {
            //decimal porc_productividad = Convert.ToDecimal(txtProductividad.Text.Substring(0, 5));
            //txtMtoProductividad.Text = (Math.Round(Convert.ToDecimal(txtCantidad.Text) * porc_productividad / 100, 2)).ToString();
        }

        private void txtProductividad_EditValueChanged(object sender, EventArgs e)
        {
            //calcularProductividad();
        }

        private void txtPrecio_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            //calcularProductividad();
        }

        private void txtObservaciones_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.None);
        }

        private void groupControl1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
        }

        private void frmManteNotaDebitoDetalle_Load(object sender, EventArgs e)
        {

        }

        private void txtPrecioTotal_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
        private void Totalizar()

        {

            txtPrecioTotal.Text = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtMonto.Text)).ToString();
        }

        private void txtMonto_EditValueChanged(object sender, EventArgs e)
        {
            Totalizar();
        }
    }
}