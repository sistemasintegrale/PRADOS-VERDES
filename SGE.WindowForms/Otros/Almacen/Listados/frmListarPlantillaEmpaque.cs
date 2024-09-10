﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class frmListarPlantillaEmpaque : DevExpress.XtraEditors.XtraForm
    {
        List<EEmpaquePlantilla> lstAlmacenes = new List<EEmpaquePlantilla>();
        public EEmpaquePlantilla _Be { get; set; }
        public frmListarPlantillaEmpaque()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstAlmacenes = new BAlmacen().EmpaquePlantillaListar();
            grdNotaIngreso.DataSource = lstAlmacenes;
            viewNotaIngreso.Focus();
        }        
       
       
        private void returnSeleccion()
        {
            if (lstAlmacenes.Count > 0)
            {
                _Be = (EEmpaquePlantilla)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            grdNotaIngreso.DataSource = lstAlmacenes.Where(x =>
                                                   x.plemc_vcod.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.prdc_vdescripcion_larga.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
       

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void viewNotaIngreso_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }     
    }
}