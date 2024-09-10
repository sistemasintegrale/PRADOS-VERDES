using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.Security.Principal;
using SGE.WindowForms.Properties;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteRecibosCajaDetalle : DevExpress.XtraEditors.XtraForm
    {
        public EContrato obeContrato = new EContrato();
        public EReciboCajaDetalle objdetalle = new EReciboCajaDetalle();
        public List<EReciboCajaDetalle> listDetalle = new List<EReciboCajaDetalle>();
        public delegate void DelegadoMensaje(int intIcod);       
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EReciboCajaCabecera obj = new EReciboCajaCabecera();
        public List<ECuotaFoma> listaFoma = new List<ECuotaFoma>();
        public decimal monto_foma;
        public FrmManteRecibosCajaDetalle()
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNro.Properties.ReadOnly = true;
                lkpTipo.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtNro.Properties.ReadOnly = true;
                lkpTipo.Enabled = false;
                txtCantidad.Properties.ReadOnly = true;
                txtMontoPagado.Properties.ReadOnly = true;
                txtDescripcion.Properties.ReadOnly = true;
                btnGuardar.Enabled = false;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtNro.Properties.ReadOnly = true;
            }
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }


        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
            txtCantidad.Text = "1";
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetView()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetValues()
        {
            txtNro.Text = objdetalle.rcd_inro_item.ToString();
            lkpTipo.EditValue = objdetalle.rc_itipo_pago;
            txtCantidad.Text = objdetalle.rcd_icantidad.ToString();
            txtMontoPagado.Text = objdetalle.rcd_dprecio_unit.ToString();
            if (objdetalle.rc_itipo_pago == Parametros.intTipoFoma)
            {
                
                txtMontoPagar.Text = monto_foma.ToString();

            }
            else
            {
                var Reprogramacion = new BVentas().ListarReprogramaciones(obeContrato.cntc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
                obeContrato.cntc_nfinanciamientro = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : obeContrato.cntc_nfinanciamientro;
                txtMontoPagar.Text = obeContrato.cntc_nmonto_foma.ToString();
            }
            txtMontoPagado.Text = txtMontoPagar.Text;
        }

        public void CargarControles()
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaVentaDet(26).ToList(), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
        }

        public void SetSave()
        {
            BaseEdit oBase = null;
            try
            {

                if (Convert.ToInt32(txtCantidad.Text) == 0)
                {
                    oBase = txtCantidad;
                    throw new ArgumentException("La cantidad debe ser mayor a 0");
                }

                if (Convert.ToDecimal(txtMontoPagado.Text) == 0)
                {
                    oBase = txtMontoPagado;
                    throw new ArgumentException("El Precio Unitario debe ser mayor a 0");
                }

                if (Convert.ToInt32(lkpTipo.EditValue) == 0)
                {
                    oBase = lkpTipo;
                    throw new ArgumentException("Ingrese el Tipo de Pago");
                }

                if (Convert.ToDecimal(txtMontoPagar.Text) < Convert.ToDecimal(txtMontoPagado.Text))
                {
                    oBase = txtMontoPagado;
                    throw new ArgumentException($"El Monto Pagado no Puede ser Mayor al Monto a Pagar");
                }
                if (listDetalle.Exists(x=> x.rc_itipo_pago == Convert.ToInt32(lkpTipo.EditValue)))
                {
                    var data = listDetalle.Where(x => x.rc_itipo_pago == Convert.ToInt32(lkpTipo.EditValue)).First();
                    if (data.rcd_icod_recibo == 0 && objdetalle.rcd_icod_recibo == 0 )
                    {
                        oBase = lkpTipo;
                        throw new ArgumentException("El Tipo de Pago ya se Encuentra en el Detalle");
                    }
                    if (data.rcd_icod_recibo != objdetalle.rcd_icod_recibo)
                    {
                        oBase = lkpTipo;
                        throw new ArgumentException("El Tipo de Pago ya se Encuentra en el Detalle");
                    }
                }
 

                objdetalle.rcd_inro_item = Convert.ToInt32(txtNro.Text);
                objdetalle.rc_itipo_pago = Convert.ToInt32(lkpTipo.EditValue);
                objdetalle.rcd_dprecio_unit = Convert.ToDecimal(txtMontoPagado.Text);
                objdetalle.rcd_icantidad = Convert.ToInt32(txtCantidad.Text);
                objdetalle.rcd_dprecio_total = Math.Round((objdetalle.rcd_icantidad * objdetalle.rcd_dprecio_unit), 2);
                objdetalle.intUsuario = Valores.intUsuario;
                objdetalle.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objdetalle.TipoOperacion = 1;

                }
                else
                {
                    if (objdetalle.TipoOperacion != 1)
                        objdetalle.TipoOperacion = 2;
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

        public void lkpTipo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == Parametros.intTipoFoma)
            {
                txtMontoPagar.Text = monto_foma.ToString();
            }
            else
            {
                var Reprogramacion = new BVentas().ListarReprogramaciones(obeContrato.cntc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
                obeContrato.cntc_nfinanciamientro = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : obeContrato.cntc_nfinanciamientro;
                txtMontoPagar.Text = obeContrato.cntc_nfinanciamientro.ToString(); 
            }
            txtMontoPagado.Text = txtMontoPagar.Text;


        }

        private void FrmManteRecibosCajaDetalle_Load(object sender, EventArgs e)
        {
            lkpTipo.Focus();
        }
 
        private void btnModificar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

             
        }

         

         
    }
}