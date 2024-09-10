﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;
using SGE.WindowForms.Otros.Contabilidad;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmRegistroConceptosIngresos : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroConceptosIngresos));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EConceptosIngresos Obe = new EConceptosIngresos();
        public List<EConceptosIngresos> lstIngreso = new List<EConceptosIngresos>();
        public List<ECuentaContable> mlistCuenta = new List<ECuentaContable>();
        //mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();
        public List<EParametroContable> mlista = (new BContabilidad()).listarParametroContable();

        public frmRegistroConceptosIngresos()
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
            txtCodigo.Enabled = false;
            lkpPlanilla.Enabled = !Enabled;
            lkpSituacion.Enabled = !Enabled;
            txtDescripcion.Enabled = !Enabled;
            txtPorcentaje.Enabled = !Enabled;
            lkpTipo.Enabled = !Enabled;
            lkpTipoCalculo.Enabled = !Enabled;
            lkpAccion.Enabled = !Enabled;
            txtMonto.Enabled = false;
            bteCuenta.Enabled = !Enabled;
            bteCuenta2.Enabled = !Enabled;
            cke5cat.Enabled = false;
            btnGuardar.Enabled = !Enabled;




            if (Status == BSMaintenanceStatus.CreateNew)
            {
                lkpSituacion.Enabled = false;

            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtCodigo.Enabled = false;


            }
        }
        public void setValues()
        {

            txtCodigo.Text = String.Format("{0:0000}", Obe.cipc_iid_concepto_ing_plan);
            //lkpPlanilla.EditValue = Obe.tbpc_icod_tipo_planilla;//--
            //lkpSituacion.EditValue = Obe.tbpc_icod_situacion_concepto_plan;//--            
            txtDescripcion.Text = Obe.cipc_vdescripcion;
            txtPorcentaje.Text = Obe.cipc_nmonto_porcentaje_planilla.ToString();
            txtMonto.Text = Obe.cipc_nmonto_calculo_planilla.ToString();
            //lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
            //lkpAccion.EditValue = Obe.tbpc_icod_tipo_accion_planilla;//--
            cke5cat.Checked = Convert.ToBoolean(Obe.cipc_bafecto_renta_quinta);
            //lkpTipo.EditValue = Obe.tbpc_icod_tipo_concepto_planilla;//--
            bteCuenta.Tag = Obe.ctcc_icod_cuenta_contable_debe;
            bteCuenta.Text = Obe.ctacc_numero_cuenta_contable;
            bteCuenta2.Tag = Obe.ctcc_icod_cuenta_contable_haber;
            bteCuenta2.Text = Obe.ctacc_numero_cuenta_contable_haber;
            txtCuentaDes.Text = Obe.strDebe;
            txtCuentaDes2.Text = Obe.strHaber;



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
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("Ingrese código");
                }
                if (verificarCodigo(txtCodigo.Text))
                {
                    oBase = txtCodigo;
                    throw new ArgumentException("El código ingresado ya existe en los Conceptos por Ingreso");
                }
                /*----------------------*/
                if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    oBase = txtDescripcion;
                    throw new ArgumentException("Ingrese la Descripción");
                }
                /*----------------------*/

                //if (verificarNombre(txtDescripcion.Text))
                //{
                //    oBase = txtDescripcion;
                //    throw new ArgumentException("La Descripción ya existe en los Conceptos por Ingreso");
                //}

                /*----------------------*/

                //if ((bteCuenta.Text != "" && bteCuenta2.Text == "") || (bteCuenta.Text == "" && bteCuenta2.Text != ""))
                //{
                //     oBase = bteCuenta2; 
                //    throw new ArgumentException("Ingrese las cuentas DEBE/HABER o Ninguna");

                //}


                if (Convert.ToDecimal(txtPorcentaje.Text) >= 100)
                {
                    oBase = txtPorcentaje;
                    throw new ArgumentException("El Porcentaje no debe ser MAYOR al 100%");
                }



                /*----------------------*/

                Obe.cipc_iid_concepto_ing_plan = txtCodigo.Text;
                Obe.tbpc_icod_tipo_planilla = Convert.ToInt32(lkpPlanilla.EditValue);
                Obe.tbpc_icod_situacion_concepto_plan = Convert.ToInt32(lkpSituacion.EditValue);
                Obe.cipc_vdescripcion = txtDescripcion.Text;
                Obe.cipc_bafecto_renta_quinta = cke5cat.Checked;
                Obe.tbpc_icod_tipo_concepto_planilla = Convert.ToInt32(lkpTipo.EditValue);
                Obe.tbpc_icod_tipo_accion_planilla = Convert.ToInt32(lkpAccion.EditValue);
                Obe.cipc_nmonto_calculo_planilla = Convert.ToDecimal(txtMonto.Text);
                Obe.cipc_nmonto_porcentaje_planilla = Convert.ToDecimal(txtPorcentaje.Text);
                Obe.tbpc_icod_tipo_calculo_planilla = Convert.ToInt32(lkpTipoCalculo.EditValue);
                if (bteCuenta.Text == null || bteCuenta.Text == "")
                { Obe.ctcc_icod_cuenta_contable_debe = 0; }
                else
                {
                    Obe.ctcc_icod_cuenta_contable_debe = Convert.ToInt32(bteCuenta.Tag);
                }

                if (bteCuenta2.Text == null || bteCuenta2.Text == "")
                { Obe.ctcc_icod_cuenta_contable_haber = 0; }
                else
                {
                    Obe.ctcc_icod_cuenta_contable_haber = Convert.ToInt32(bteCuenta2.Tag);
                }

                Obe.ctacc_numero_cuenta_contable = bteCuenta.Text;
                Obe.ctacc_numero_cuenta_contable_haber = bteCuenta2.Text;
                Obe.cipc_flag_estado = true;
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    Obe.cipc_icod_concepto_ingreso_plan = new BPlanillas().insertarIngreso(Obe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().modificarIngreso(Obe);
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
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    this.MiEvento(Obe.cipc_icod_concepto_ingreso_plan);
                    this.Close();
                }
            }
        }

        private bool verificarNombre(string strNombre)
        {
            try
            {
                bool exists = false;
                if (lstIngreso.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstIngreso.Where(x => x.cipc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim()).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstIngreso.Where(x => x.cipc_vdescripcion.Replace(" ", "").Trim() == strNombre.Replace(" ", "").Trim() && x.cipc_icod_concepto_ingreso_plan != Obe.cipc_icod_concepto_ingreso_plan).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool verificarCodigo(string strCodigo)
        {
            try
            {
                bool exists = false;
                if (lstIngreso.Count > 0)
                {
                    if (Status == BSMaintenanceStatus.CreateNew)
                    {
                        if (lstIngreso.Where(x => x.cipc_iid_concepto_ing_plan == strCodigo).ToList().Count > 0)
                            exists = true;
                    }
                    if (Status == BSMaintenanceStatus.ModifyCurrent)
                    {
                        if (lstIngreso.Where(x => x.cipc_iid_concepto_ing_plan == strCodigo && x.cipc_icod_concepto_ingreso_plan != Obe.cipc_icod_concepto_ingreso_plan).ToList().Count > 0)
                            exists = true;
                    }
                }
                return exists;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void frmMantePersonal_Load(object sender, EventArgs e)
        {
            txtMonto.Enabled = true;
            txtPorcentaje.Enabled = false;
            lkpTipoCalculo.Enabled = false;


            BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(5), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpPlanilla, new BPlanillas().listarComboTablaPlanillaDetalle(1), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpSituacion, new BPlanillas().listarComboTablaPlanillaDetalle(2), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpTipo, new BPlanillas().listarComboTablaPlanillaDetalle(3), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            BSControls.LoaderLook(lkpAccion, new BPlanillas().listarComboTablaPlanillaDetalle(4), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpPlanilla.EditValue = Obe.tbpc_icod_tipo_planilla;//--
                lkpSituacion.EditValue = Obe.tbpc_icod_situacion_concepto_plan;//--   
                lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
                lkpAccion.EditValue = Obe.tbpc_icod_tipo_accion_planilla;//--
                lkpTipo.EditValue = Obe.tbpc_icod_tipo_concepto_planilla;//--

            }
            if (Status == BSMaintenanceStatus.View)
            {
                txtMonto.Enabled = false;
                lkpPlanilla.EditValue = Obe.tbpc_icod_tipo_planilla;//--
                lkpSituacion.EditValue = Obe.tbpc_icod_situacion_concepto_plan;//--   
                lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
                lkpAccion.EditValue = Obe.tbpc_icod_tipo_accion_planilla;//--
                lkpTipo.EditValue = Obe.tbpc_icod_tipo_concepto_planilla;//--                     

                //txtCodigo.Enabled = false;
                if (lkpAccion.Text == "CALCULO")
                {

                    //txtMonto.Enabled = false;
                    //txtPorcentaje.Enabled = false;
                    //lkpTipoCalculo.Enabled = true;
                    BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(5), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
                    lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
                }
                else if (lkpAccion.Text == "MONTO")
                {

                    lkpTipoCalculo.EditValue = null;
                    //lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = false;
                    //txtPorcentaje.Enabled = false;
                }
                else if (lkpAccion.Text == "PORCENTAJE")
                {

                    lkpTipoCalculo.EditValue = null;
                    //lkpTipoCalculo.Enabled = false;
                    //txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = false;
                }

            }


            mlistCuenta = new BContabilidad().listarCuentaContable().Where(x => x.tablc_iid_tipo_cuenta == 2).ToList();

            mlista.ForEach(obe =>
            {
                this.bteCuenta.Properties.Mask.BeepOnError = true;
                this.bteCuenta.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta.Properties.Mask.ShowPlaceHolders = false;

                this.bteCuenta2.Properties.Mask.BeepOnError = true;
                this.bteCuenta2.Properties.Mask.EditMask = obe.parac_vmascara;
                this.bteCuenta2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                this.bteCuenta2.Properties.Mask.ShowPlaceHolders = false;
            });

        }
        public void LimpiarListas()
        {


            lkpPlanilla.EditValue = null;//--
            lkpPlanilla.Text = null;
            lkpSituacion.EditValue = null;//--  
            lkpSituacion.Text = null;
            lkpTipoCalculo.EditValue = null;//--
            lkpTipoCalculo.Text = null;
            lkpAccion.EditValue = null;//--  
            lkpAccion.Text = null;
            lkpTipo.EditValue = null;//--
            lkpTipo.Text = null;

        }
        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {


            if (Status == BSMaintenanceStatus.CreateNew)
            {
                cke5cat.Checked = true;
                lkpSituacion.Enabled = false;

                if (lkpAccion.Text == "CALCULO")
                {
                    if (cke5cat.Checked == false)
                    {
                        cke5cat.Checked = false;
                    }
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = false;
                    lkpTipoCalculo.Enabled = true;
                    BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(5), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
                }
                else if (lkpAccion.Text == "MONTO")
                {
                    if (cke5cat.Checked == false)
                    {
                        cke5cat.Checked = false;
                    }
                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = true;
                    txtPorcentaje.Enabled = false;
                }
                else if (lkpAccion.Text == "PORCENTAJE")
                {
                    if (cke5cat.Checked == false)
                    {
                        cke5cat.Checked = false;
                    }

                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = true;
                }

            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {


                txtCodigo.Enabled = false;
                if (lkpAccion.Text == "CALCULO")
                {
                    if (cke5cat.Checked == false)
                    {
                        cke5cat.Checked = false;
                    }
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = false;
                    lkpTipoCalculo.Enabled = true;
                    BSControls.LoaderLook(lkpTipoCalculo, new BPlanillas().listarComboTablaPlanillaDetalle(5), "tbpd_vdescripcion_detalle", "tbpd_icod_tabla_planilla_detalle", true);
                    lkpTipoCalculo.EditValue = Obe.tbpc_icod_tipo_calculo_planilla;//--
                }
                else if (lkpAccion.Text == "MONTO")
                {
                    if (cke5cat.Checked == false)
                    {
                        cke5cat.Checked = false;
                    }
                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = true;
                    txtPorcentaje.Enabled = false;
                }
                else if (lkpAccion.Text == "PORCENTAJE")
                {
                    if (cke5cat.Checked == false)
                    {
                        cke5cat.Checked = false;
                    }

                    lkpTipoCalculo.EditValue = null;
                    lkpTipoCalculo.Enabled = false;
                    txtMonto.Enabled = false;
                    txtPorcentaje.Enabled = true;
                }

            }


        }


        private void labelControl13_Click(object sender, EventArgs e)
        {

        }

        private void bteCuenta_EditValueChanged(object sender, EventArgs e)
        {
            List<ECuentaContable> aux = new List<ECuentaContable>();
            if (bteCuenta.Text == "")
            {
                txtCuentaDes.Text = null;
                return;
            }

            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCuenta.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes.Text = aux[0].ctacc_nombre_descripcion;
            }
        }

        private void bteCuenta_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarCuenta();
        }
        private void ListarCuenta()
        {

            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuenta.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes.Text = frm._Be.ctacc_nombre_descripcion;

                }
            }
        }

        private void bteCuenta2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuenta2.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuenta2.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaDes2.Text = frm._Be.ctacc_nombre_descripcion;
                }
            }
        }

        private void bteCuenta2_EditValueChanged(object sender, EventArgs e)
        {
            if (bteCuenta2.Text == "")
            {
                txtCuentaDes2.Text = null;
                return;
            }
            List<ECuentaContable> aux = new List<ECuentaContable>();
            aux = mlistCuenta.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(bteCuenta2.Text.Replace(".", ""))).ToList();


            if (aux.Count == 1)
            {
                bteCuenta2.Tag = aux[0].ctacc_icod_cuenta_contable;
                txtCuentaDes2.Text = aux[0].ctacc_nombre_descripcion;
            }

        }
    }
}