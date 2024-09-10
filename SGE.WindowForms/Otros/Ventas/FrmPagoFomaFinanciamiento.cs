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
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmPagoFomaFinanciamiento : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public EPagoFomaFinanciamiento Obe = new EPagoFomaFinanciamiento();
        public FrmPagoFomaFinanciamiento()
        {
            InitializeComponent();
        }
        public void setValues() {
            lkptipo.EditValue = Obe.pgs_itipo;
            txtMonto.Text = Obe.pgs_nmonto.ToString();
            txtMontoPagado.Text = Obe.pgs_nmonto_pagado.ToString();
            dtefechaPago.DateTime = Obe.pgs_sfecha_pago == (DateTime?)null ? DateTime.Today : Convert.ToDateTime(Obe.pgs_sfecha_pago);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (Convert.ToDecimal(txtMontoPagado.Text) > Convert.ToDecimal(txtMonto.Text))
                {
                    oBase = txtMontoPagado;
                    throw new ArgumentException("El Monto Pagado no Pueder Ser Mayoral Monto a Pagar");
                }

                Obe.pgs_nmonto_pagado = Convert.ToDecimal(txtMontoPagado.Text);
                Obe.pgs_sfecha_pago = Convert.ToDateTime(dtefechaPago.DateTime);


                this.DialogResult = DialogResult.OK;
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
                    this.Close();
                }
            }

      
            
        }

        public void cargarControles()
        {
            BSControls.LoaderLook(lkptipo, new BGeneral().listarTablaVentaDet(26), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}