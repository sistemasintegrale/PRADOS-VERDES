using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.Entity;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmMantePagoCuotas : XtraForm
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteContratoCuotas));
        public EContratoCuotas oBe_ = new EContratoCuotas();
        public FrmMantePagoCuotas()
        {
            InitializeComponent();
        }
        public void cargardatos()
        {
            txtMontoAPagar.Text = oBe_.cntc_nmonto_mora.ToString();
            txtMontoPagado.Text = oBe_.cntc_nmonto_mora_pago.ToString();
            ckAutomatico.Checked = oBe_.cntc_bautomatico;
            ckManual.Checked = !oBe_.cntc_bautomatico;

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMontoAPagar.Text) < Convert.ToDecimal(txtMontoPagado.Text))
                {
                    oBase = txtMontoPagado;
                    throw new ArgumentException("El Monto Pagado no Puede ser Mayor al Monto a Pagar");
                }
                oBe_.cntc_nmonto_mora = Convert.ToDecimal(txtMontoAPagar.Text);
                oBe_.cntc_nmonto_mora_pago = Convert.ToDecimal(txtMontoPagado.Text);
                oBe_.cntc_bautomatico = ckAutomatico.Checked;
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

        private void ckManualChanged(object sender, EventArgs e)
        {
            if (!ckManual.ContainsFocus)
                return;
            ckAutomatico.Checked = !ckManual.Checked;
        }

        private void ckAutomatico_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckAutomatico.ContainsFocus)
                return;
            ckManual.Checked = !ckAutomatico.Checked;
        }
    }
}