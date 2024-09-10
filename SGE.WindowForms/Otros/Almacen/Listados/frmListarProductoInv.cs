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
    public partial class frmListarProductoInv : DevExpress.XtraEditors.XtraForm
    {
        public List<EProducto> lstProductos = new List<EProducto>();
        public EProducto _Be { get; set; }
        
        public frmListarProductoInv()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();            
        }

        private void cargar()
        {
            //lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio).Where(x => x.prdc_isituacion && x.tarec_icorrelativo_registro_tipo != 3).ToList();            
            //grdProducto.DataSource = lstProductos;
            //viewProducto.Focus();
        }

      
        private void buscarCriterio()
        {
            grdProducto.DataSource = lstProductos.Where(x =>
                                                   x.prdc_vcode_producto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
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
            viewProducto.MoveLast();
            viewProducto.MoveFirst();
            txtCodigo.Focus();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }      
      
    }
}