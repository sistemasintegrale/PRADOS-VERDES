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
using SGE.WindowForms.Otros.bVentas;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteFomaFinanciamiento : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public EPagoFomaFinanciamiento obj = new EPagoFomaFinanciamiento();
        public EContrato Obe = new EContrato();
        public frmManteFomaFinanciamiento()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMontoPagado.Text)>Convert.ToDecimal(txtMontoPagar.Text))
                {
                    oBase = txtMontoPagado;
                    throw new ArgumentException("El monto Pagado o Puede ser Mayor al Monto a Pagar");
                }
                if (dteFechaPago.DateTime == null)
                {
                    oBase = dteFechaPago;
                    throw new ArgumentException("Ingrese la Fecha del Pago");
                }
                obj.pgs_icod_contrato = Obe.cntc_icod_contrato;
                obj.pgs_nmonto_pagado = Convert.ToDecimal(txtMontoPagado.Text);
                obj.intusuario = Valores.intUsuario;
                obj.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                obj.pgs_sfecha_pago = dteFechaPago.DateTime;
                new BVentas().FomaFinanciamientoModificar(obj);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void frmManteFomaFinanciamiento_Load(object sender, EventArgs e)
        {
            dteFechaPago.EditValue = obj.pgs_sfecha_pago;
            txtMontoPagado.Text = obj.pgs_nmonto_pagado.ToString();
            txtMontoPagar.Text = obj.pgs_nmonto.ToString() ;
        }
    }
}