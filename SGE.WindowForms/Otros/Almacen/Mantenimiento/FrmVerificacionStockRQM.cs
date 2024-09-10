﻿using System;
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

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class FrmVerificacionStockRQM : DevExpress.XtraEditors.XtraForm
    {
        public ERequerimientoMateriales oBe = new ERequerimientoMateriales();
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        List<ERequerimientoMaterialesDetalle> lstRequerimientoMaterialesDetalle = new List<ERequerimientoMaterialesDetalle>();
        List<ERequerimientoMaterialesDetalle> lstDelete = new List<ERequerimientoMaterialesDetalle>();
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public int IcodRubros = 0;
        public decimal rqmd_cantidad_aprobada = 0;
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
            if (Status == BSMaintenanceStatus.View)
            {
                txtNumero.Enabled = false;
                lkpTipo.Enabled = false;
                dteFecha.Enabled = false;
                txtObservaciones.Enabled = false;
                bteProyecto.Enabled = false;

            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNumero.Enabled = false;
                lkpTipo.Enabled = false;
                dteFecha.Enabled = false;
                txtObservaciones.Enabled = false;
                bteProyecto.Enabled = false;
         
            }
        }

        public void setValues()
        {
            txtNumero.Text = oBe.rqmc_numero_req_material;
            dteFecha.EditValue = oBe.rqmc_sfecha_req_material;
            lkpSituacion.EditValue = oBe.tablc_iid_situación_hc;
            txtObservaciones.Text = oBe.rqmc_vdescripcion;
            txtHCN.Text = oBe.NumHojaCosto;
            lkpTipo.EditValue = oBe.tablc_iid_tipo_requerimiento;
            bteProyecto.Tag = oBe.pryc_icod_proyecto;
            bteProyecto.Text =string.Format("{0:00000}",oBe.pryc_icod_proyecto);

            lstRequerimientoMaterialesDetalle = new BAlmacen().listarVerificacionStockRequerimientoMateriales(oBe.rqmc_icod_requerimiento_materiales);


        }

        public FrmVerificacionStockRQM()
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

            grdMateriales.DataSource = lstRequerimientoMaterialesDetalle;

           
        }
        public void CargarControles()
        {
         BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(74), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
         BSControls.LoaderLook(lkpTipo, new BGeneral().listarTablaRegistro(73), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);   
        }
        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;      
            try 
            {
                //if (Convert.ToInt32(txtSerie.Text) == 0)
                //{
                //    oBase = txtSerie;
                //    throw new ArgumentException("Ingrese Nro. de Serie de Factura");
                //}

                //if (txtSerie.Text == "000")
                //{
                //    oBase = txtSerie;
                //    throw new ArgumentException("N° de Serie no registrado, registrar N° serie en REGISTRO DE TIPOS DE DOCUMENTOS");
                //}

                //if (Convert.ToInt32(txtNumero.Text) == 0)
                //{
                //    oBase = txtNumero;
                //    throw new ArgumentException("Ingrese Nro. de Factura");
                //}

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }


                oBe.rqmc_numero_req_material = txtNumero.Text;
                oBe.rqmc_sfecha_req_material = Convert.ToDateTime(dteFecha.Text);
                oBe.tablc_iid_situación_hc = Convert.ToInt32(lkpSituacion.EditValue);
                oBe.tablc_iid_tipo_requerimiento = Convert.ToInt32(lkpTipo.EditValue);
                oBe.pryc_icod_proyecto = Convert.ToInt32(bteProyecto.Tag);
                oBe.rqmc_vdescripcion = txtObservaciones.Text;
                oBe.rqmc_flag_estado = true;
                oBe.rqmc_bautorizado = true;
                oBe.tablc_iid_situación_hc = 309;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.rqmc_icod_requerimiento_materiales = new BVentas().insertarRequerimientoMateriales(oBe, lstRequerimientoMaterialesDetalle);
                }
                else
                {
                    new BVentas().modificarRequerimientoMateriales(oBe, lstRequerimientoMaterialesDetalle,lstDelete);
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
                    MiEvento(oBe.rqmc_icod_requerimiento_materiales);
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
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            
                modificarItem();
                 
        }

        private void modificarItem()
        {

        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstRequerimientoMaterialesDetalle.Remove(obe);
            viewMateriales.RefreshData();
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
            ERequerimientoMaterialesDetalle obe = (ERequerimientoMaterialesDetalle)viewMateriales.GetRow(viewMateriales.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstRequerimientoMaterialesDetalle.Remove(obe);
            viewMateriales.RefreshData();
        
        }

        private void bteProyecto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ListarProyecto();
        }
        private void ListarProyecto()
        {
        }

        private void viewMateriales_RowStyle(object sender, RowStyleEventArgs e)
        {
            
        }

    
        
    }    
}