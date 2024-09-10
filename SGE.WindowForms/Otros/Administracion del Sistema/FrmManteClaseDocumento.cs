using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Modules;
namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class FrmManteClaseDocumento : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteClaseDocumento));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public List<ETipoDocumentoDetalleCta> lstClasesDocumento;
        public ETipoDocumentoDetalleCta oBe = new ETipoDocumentoDetalleCta();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        private List<ECuentaContable> lstCuentaContable = new List<ECuentaContable>();
        public int intTipoDoc = 0;

        public FrmManteClaseDocumento()
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

        public void setValues()
        {
            txtCodigo.Text = oBe.tdocd_iid_codigo_doc_det.ToString();
            txtDescripcion.Text = oBe.tdocd_descripcion;
            txtCtaascnac.Text = (oBe.ctacc_icod_cuenta_asociada_nac == null) ? "" : oBe.ctacc_icod_cuenta_asociada_nac.ToString();
            txtCtaasocext.Text = (oBe.ctacc_icod_cuenta_asociada_extra == null) ? "" : oBe.ctacc_icod_cuenta_asociada_extra.ToString();
            txtCtaextranjera.Text = (oBe.ctacc_icod_cuenta_contable_extra == null) ? "" : oBe.ctacc_icod_cuenta_contable_extra.ToString();
            bteCtaMercaderia.Text = (oBe.ctacc_icod_cuenta_gastos_nac == null) ? "" : oBe.ctacc_icod_cuenta_gastos_nac.ToString();
            bteCtaServicios.Text = (oBe.ctacc_icod_cuenta_servicios == null) ? "" : oBe.ctacc_icod_cuenta_servicios.ToString();
            txtCtaiev.Text = (oBe.ctacc_icod_cuenta_ivap == null) ? "" : oBe.ctacc_icod_cuenta_ivap.ToString();
            txtCtaigv.Text = (oBe.ctacc_icod_cuenta_igv_nac == null) ? "" : oBe.ctacc_icod_cuenta_igv_nac.ToString();
            txtCtaisc.Text = (oBe.ctacc_icod_cuenta_isc == null) ? "" : oBe.ctacc_icod_cuenta_isc.ToString();
            txtCtamatrisext.Text = (oBe.ctacc_icod_cuenta_matris_extra == null) ? "" : oBe.ctacc_icod_cuenta_matris_extra.ToString();
            txtCtamatrisnac.Text = (oBe.ctacc_icod_cuenta_matris_nac == null) ? "" : oBe.ctacc_icod_cuenta_matris_nac.ToString();
            txtCtanacional.Text = (oBe.ctacc_icod_cuenta_contable_nac == null) ? "" : oBe.ctacc_icod_cuenta_contable_nac.ToString();
            txtCtasubext.Text = (oBe.ctacc_icod_subcuenta_extra == null) ? "" : oBe.ctacc_icod_subcuenta_extra.ToString();
            txtCtasubnac.Text = (oBe.ctacc_icod_subcuenta_nac == null) ? "" : oBe.ctacc_icod_subcuenta_nac.ToString();
            txtCoa.Text = (oBe.tdocd_estado_coa == null) ? "" : String.Format("{0:00}", oBe.tdocd_estado_coa);
            cbRegCompras.Checked = (oBe.tdocd_iestado_registro == 0) ? true : false;
            cbRegVentas.Checked = (oBe.tdocd_iestado_registro == 1) ? true : false;              
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            txtCodigo.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtCtaascnac.Enabled = !Enabled;
            txtCtaasocext.Enabled = !Enabled;
            txtCtaextranjera.Enabled = !Enabled;
            bteCtaMercaderia.Enabled = !Enabled;
            txtCtaiev.Enabled = !Enabled;
            txtCtaigv.Enabled = !Enabled;
            txtCtaisc.Enabled = !Enabled;
            txtCtamatrisext.Enabled = !Enabled;
            txtCtamatrisnac.Enabled = !Enabled;
            txtCtanacional.Enabled = !Enabled;
            txtCtasubext.Enabled = !Enabled;
            txtCtasubnac.Enabled = !Enabled;
            cbRegVentas.Enabled = !Enabled;
            cbRegCompras.Enabled = !Enabled;            
            txtCoa.Enabled = !Enabled;
            txtCodigo.Focus();
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

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese descripción");
                }

                if (string.IsNullOrEmpty(txtCtanacional.Text))
                {
                    oBase = txtCtanacional;
                    throw new ArgumentException("Ingrese Cuenta Moneda Nacional");
                }

                if (string.IsNullOrEmpty(txtCtaextranjera.Text))
                {
                    oBase = txtCtaextranjera;
                    throw new ArgumentException("Ingresar Cuenta Moneda Extranjera");
                }
                #region VerificarCuentas
                if (txtCtanacional.Text != "")
                {
                    if (CheckCuenta(txtCtanacional) == false)
                    {
                        oBase = txtCtanacional;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (txtCtamatrisnac.Text != "")
                {
                    if (CheckCuenta(txtCtamatrisnac) == false)
                    {
                        oBase = txtCtamatrisnac;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (txtCtasubnac.Text != "")
                {
                    if (CheckCuenta(txtCtasubnac) == false)
                    {
                        oBase = txtCtasubnac;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (txtCtaascnac.Text != "")
                {
                    if (CheckCuenta(txtCtaascnac) == false)
                    {
                        oBase = txtCtaascnac;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }
                //
                if (txtCtaigv.Text != "")
                {
                    if (CheckCuenta(txtCtaigv) == false)
                    {
                        oBase = txtCtaigv;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (txtCtaextranjera.Text != "")
                {
                    if (CheckCuenta(txtCtaextranjera) == false)
                    {
                        oBase = txtCtaextranjera;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }
                //
                if (txtCtamatrisext.Text != "")
                {
                    if (CheckCuenta(txtCtamatrisext) == false)
                    {
                        oBase = txtCtamatrisext;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (txtCtasubext.Text != "")
                {
                    if (CheckCuenta(txtCtasubext) == false)
                    {
                        oBase = txtCtasubext;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }
                if (txtCtaasocext.Text != "")
                {
                    if (CheckCuenta(txtCtaasocext) == false)
                    {
                        oBase = txtCtaasocext;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (txtCtaisc.Text != "")
                {
                    if (CheckCuenta(txtCtaisc) == false)
                    {
                        oBase = txtCtaisc;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }
                if (txtCtaiev.Text != "")
                {
                    if (CheckCuenta(txtCtaiev) == false)
                    {
                        oBase = txtCtaiev;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }
                if (bteCtaMercaderia.Text != "")
                {
                    if (CheckCuenta(bteCtaMercaderia) == false)
                    {
                        oBase = bteCtaMercaderia;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }

                if (bteCtaServicios.Text != "")
                {
                    if (CheckCuenta(bteCtaServicios) == false)
                    {
                        oBase = bteCtaServicios;
                        throw new ArgumentException("Inrgrese una cuenta válida");
                    }
                }
                //
                #endregion
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCosto = lstClasesDocumento.Where(oB => oB.tdocd_descripcion.ToUpper() == txtDescripcion.Text.ToUpper()).ToList();
                    if (BuscarCosto.Count > 0)
                    {
                        oBase = txtDescripcion;
                        throw new ArgumentException("La descripción ingresada, ya existe");
                    }
                    var CodigoRepetido = lstClasesDocumento.Where(oB => oB.tdocd_iid_codigo_doc_det.ToString().ToUpper() == Convert.ToInt32(txtCodigo.Text).ToString().ToUpper()).ToList();
                    if (CodigoRepetido.Count > 0)
                    {
                        oBase = txtCodigo;
                        throw new ArgumentException("El código ingresado, ya existe");
                    }
                }
                int? nullVal;
                nullVal = null;               
                oBe.tdocd_iid_codigo_doc_det = Convert.ToInt32(txtCodigo.Text);
                oBe.tdocd_descripcion = txtDescripcion.Text;
                oBe.tdocc_icod_tipo_doc = intTipoDoc;
                oBe.ctacc_icod_cuenta_contable_nac = Convert.ToInt32(txtCtanacional.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_contable_extra = Convert.ToInt32(txtCtaextranjera.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_matris_nac = (txtCtamatrisnac.Text == "") ? nullVal : Convert.ToInt32(txtCtamatrisnac.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_matris_extra = (txtCtamatrisnac.Text == "") ? nullVal : Convert.ToInt32(txtCtamatrisext.Text.Replace(".", ""));
                oBe.ctacc_icod_subcuenta_nac = (txtCtasubnac.Text == "") ? nullVal : Convert.ToInt32(txtCtasubnac.Text.Replace(".", ""));
                oBe.ctacc_icod_subcuenta_extra = (txtCtasubext.Text == "") ? nullVal : Convert.ToInt32(txtCtasubext.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_asociada_nac = (txtCtaascnac.Text == "") ? nullVal : Convert.ToInt32(txtCtaascnac.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_asociada_extra = (txtCtaasocext.Text == "") ? nullVal : Convert.ToInt32(txtCtaasocext.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_igv_nac = (txtCtaigv.Text == "") ? nullVal : Convert.ToInt32(txtCtaigv.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_isc = (txtCtaisc.Text == "") ? nullVal : Convert.ToInt32(txtCtaisc.Text.Replace(".", ""));
                
                oBe.ctacc_icod_cuenta_gastos_nac = (bteCtaMercaderia.Text == "") ? nullVal : Convert.ToInt32(bteCtaMercaderia.Text.Replace(".", ""));
                oBe.ctacc_icod_cuenta_servicios = (bteCtaServicios.Text == "") ? nullVal : Convert.ToInt32(bteCtaServicios.Text.Replace(".", ""));
                
                oBe.ctacc_icod_cuenta_ivap = (txtCtaiev.Text == "") ? nullVal : Convert.ToInt32(txtCtaiev.Text.Replace(".", ""));

                oBe.tdocd_iestado_registro = (cbRegCompras.Checked) ? 0 : (cbRegVentas.Checked) ? 1 : nullVal;
                oBe.tdocd_estado_coa = (string.IsNullOrEmpty(txtCoa.Text)) ? nullVal : Convert.ToInt32(txtCoa.Text);
                oBe.tdocd_estado = true;
                oBe.intUsuario= Valores.intUsuario;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    new BAdministracionSistema().insertarTipoDocumentoDetCta(oBe);
                }
                else
                {
                    new BAdministracionSistema().modificarTipoDocumentoDetCta(oBe);
                }
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(oBe.tdocd_iid_correlativo);
                    this.Close();
                }
            }
        }
      

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtctanacional_KeyDown(object sender, KeyEventArgs e)
        {
            ButtonEdit texto = (ButtonEdit)sender;
            if (e.KeyValue == (char)Keys.F10)
            {
                frmListarCuentaContable listaCuenta = new frmListarCuentaContable();
                if (listaCuenta.ShowDialog() == DialogResult.OK)
                    texto.Text = listaCuenta._Be.ctacc_numero_cuenta_contable.ToString();
            }
        }

        private void FrmManteClaseDocumento_Load(object sender, EventArgs e)
        {
            lstCuentaContable = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
            LoadMask();
        }
        private void LoadMask()
        {
            List<EParametroContable> mlista = new BContabilidad().listarParametroContable();
            mlista.ForEach(obe =>
            {         
                this.txtCtanacional.Properties.Mask.BeepOnError = true;
                this.txtCtanacional.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtanacional.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtanacional.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtanacional.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctamatrisnac
                this.txtCtamatrisnac.Properties.Mask.BeepOnError = true;
                this.txtCtamatrisnac.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtamatrisnac.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtamatrisnac.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtamatrisnac.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctasubnac
                this.txtCtasubnac.Properties.Mask.BeepOnError = true;
                this.txtCtasubnac.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtasubnac.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtasubnac.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtasubnac.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctaascnac
                this.txtCtaascnac.Properties.Mask.BeepOnError = true;
                this.txtCtaascnac.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtaascnac.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtaascnac.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtaascnac.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctaigv
                this.txtCtaigv.Properties.Mask.BeepOnError = true;
                this.txtCtaigv.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtaigv.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtaigv.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtaigv.Properties.Mask.UseMaskAsDisplayFormat = true;
                //bteCtaMercaderia
                this.bteCtaMercaderia.Properties.Mask.BeepOnError = true;
                this.bteCtaMercaderia.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCtaMercaderia.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCtaMercaderia.Properties.Mask.ShowPlaceHolders = false;
                this.bteCtaMercaderia.Properties.Mask.UseMaskAsDisplayFormat = true;
                //bteCtaServicios
                this.bteCtaServicios.Properties.Mask.BeepOnError = true;
                this.bteCtaServicios.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCtaServicios.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCtaServicios.Properties.Mask.ShowPlaceHolders = false;
                this.bteCtaServicios.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctaextranjera
                this.txtCtaextranjera.Properties.Mask.BeepOnError = true;
                this.txtCtaextranjera.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtaextranjera.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtaextranjera.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtaextranjera.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctamatrisext
                this.txtCtamatrisext.Properties.Mask.BeepOnError = true;
                this.txtCtamatrisext.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtamatrisext.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtamatrisext.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtamatrisext.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctasubext
                this.txtCtasubext.Properties.Mask.BeepOnError = true;
                this.txtCtasubext.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtasubext.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtasubext.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtasubext.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctaasocext                
                this.txtCtaasocext.Properties.Mask.BeepOnError = true;
                this.txtCtaasocext.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtaasocext.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtaasocext.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtaasocext.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctaisc
                this.txtCtaisc.Properties.Mask.BeepOnError = true;
                this.txtCtaisc.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtaisc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtaisc.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtaisc.Properties.Mask.UseMaskAsDisplayFormat = true;
                //txtctaiev
                this.txtCtaiev.Properties.Mask.BeepOnError = true;
                this.txtCtaiev.Properties.Mask.EditMask = obe.parac_vmascara;
                this.txtCtaiev.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.txtCtaiev.Properties.Mask.ShowPlaceHolders = false;
                this.txtCtaiev.Properties.Mask.UseMaskAsDisplayFormat = true;
            });
        }
        private bool CheckCuenta(TextEdit obj)
        {
            bool rpta;
            int cuenta;
            cuenta = Convert.ToInt32(obj.Text.Replace(".", ""));

            if (lstCuentaContable.Where(x => x.ctacc_icod_cuenta_contable == cuenta).ToList().Count == 1)
                rpta = true;
            else
                rpta = false;
            return rpta;            
        }

        private void cbRegCompras_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRegCompras.Checked == true)
            {
                if (cbRegVentas.Checked == true)
                    cbRegVentas.Checked = false;
            }
        }

        private void cbRegVentas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRegVentas.Checked == true)
            {
                if (cbRegCompras.Checked == true)
                    cbRegCompras.Checked = false;
            }
        }

        private void txtctanacional_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit texto = (ButtonEdit)sender;
            frmListarCuentaContable listaCuenta = new frmListarCuentaContable();
            if (listaCuenta.ShowDialog() == DialogResult.OK)
                texto.Text = listaCuenta._Be.ctacc_numero_cuenta_contable.ToString();
        }
    }
}