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

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListarFacImpoDet : DevExpress.XtraEditors.XtraForm
    {
        List<EFacturaCompraDet> lstFacImpDet = new List<EFacturaCompraDet>();
        public EDXPImportacion _Be { get; set; }
        public int IcodFacDet = 0;
        public decimal MontoSoles = 0;
        public frmListarFacImpoDet()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {

            lstFacImpDet = new BCompras().listarFacCompraImportacionDet(IcodFacDet);
            grdDXPImpDet.DataSource = lstFacImpDet;
            viewDXPImpDet.Focus();


        }        
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstFacImpDet.Count > 0)
            {
                _Be = (EDXPImportacion)viewDXPImpDet.GetRow(viewDXPImpDet.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            //grdProyectos.DataSource = lstDXPImpDet.Where(x =>
            //                                       x.pryc_vcorrelativo.ToString().Contains(txtCodigo.Text.ToUpper()) &&
            //                                       x.pryc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
            //                                 ).ToList();
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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
    }
}