using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;
using System.Linq;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteContratoCuotasDet : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EContratoCuotas oBe = new EContratoCuotas();
        public List<EContratoCuotas> lstDetalle = new List<EContratoCuotas>();
        public int cntc_icod_contrato;
        public int correlativo;

        public frmManteContratoCuotasDet()
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
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    bteNroDocumento.Enabled = Enabled;
        }


        public void setValues()
        {
            cntc_icod_contrato = oBe.cntc_icod_contrato;
            txtNroCuotas.Text = oBe.cntc_inro_cuotas.ToString();
            lkpTipo.EditValue = oBe.cntc_icod_tipo_cuota;
            dtFechaCuota.EditValue = oBe.cntc_sfecha_cuota;
            txtMonto.Text = oBe.cntc_nmonto_cuota.ToString();
            txtMora.Text = oBe.cntc_nmonto_mora.ToString();
            txtCronograma.Text = oBe.cntc_itipo_cuota.ToString();
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
            setValues();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMonto.Text) == 0)
                {
                    oBase = txtMonto;
                    throw new ArgumentException("Ingrese Monto de la Cuota");
                }

                if (string.IsNullOrEmpty(dtFechaCuota.Text))
                {
                    oBase = dtFechaCuota;
                    throw new ArgumentException("Ingrese Fecha de Pago de la Cuota");
                }
                oBe.cntc_icod_contrato = cntc_icod_contrato;
                oBe.cntc_inro_cuotas = Convert.ToInt32(txtNroCuotas.Text);
                oBe.cntc_icod_tipo_cuota = Convert.ToInt32(lkpTipo.EditValue);
                oBe.strTipo = lkpTipo.Text;
                oBe.cntc_sfecha_cuota = Convert.ToDateTime(dtFechaCuota.EditValue);
                oBe.cntc_nmonto_cuota = Convert.ToDecimal(txtMonto.Text);
                oBe.cntc_nmonto_mora = Convert.ToDecimal(txtMora.Text);

                if (Status == BSMaintenanceStatus.CreateNew) {
                    oBe.cntc_icod_situacion = 338;
                    oBe.strSituacion = "PENDIENTE";
                    oBe.cntc_npagado = 0;
                    oBe.cntc_nsaldo = oBe.cntc_nmonto_cuota;
                    oBe.cntc_nmonto_mora_pago = 0;
                }
                
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
               
               
                oBe.cntc_itipo_cuota = Convert.ToInt32(txtCronograma.Text);// INDICADOR PRINCIPAL
                oBe.strTipoCredito = oBe.cntc_itipo_cuota == 0 ? "PRINCIPAL" : "REPROGRAMACION " + oBe.cntc_itipo_cuota;
                


                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.intTipoOperacion = 1;
                    lstDetalle.Add(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                    if (oBe.intTipoOperacion != 1)
                        oBe.intTipoOperacion = 2;

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
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePercepcionDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaVentaDet(15), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpTipo.EditValue = oBe.cntc_icod_tipo_cuota;
            }
            else
            {
                lkpTipo.EditValue = 337;
            }


            if (Valores.intUsuario == 4)
            {
                lblCronograma.Visible = true;
                txtCronograma.Visible = true;
            }
            else
            {
                lblCronograma.Visible = false;
                txtCronograma.Visible = false;
            }
        }

        private void lkpTipo_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipo.EditValue) == 336)
            {
                txtNroCuotas.Text = "0";
            }
            else
            {
                txtNroCuotas.Text = correlativo.ToString();
            }

        }
    }
}