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
    public partial class frmListarAlmacen : DevExpress.XtraEditors.XtraForm
    {
        List<EAlmacen> lstAlmacenes = new List<EAlmacen>();
        public EAlmacen _Be { get; set; }
        public frmListarAlmacen()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {

            lstAlmacenes = new BAlmacen().listarAlmacenesGR();
            grdAlmacen.DataSource = lstAlmacenes;
            viewAlmacen.Focus();


        }        
       
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstAlmacenes.Count > 0)
            {
                _Be = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
      
        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.almac_iid_almacen.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.almac_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
        void reload(int intIcod)
        {
            cargar();
            int index = lstAlmacenes.FindIndex(x => x.almac_icod_almacen == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        } 
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            if (lstAlmacenes.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstAlmacenes.Max(x => x.almac_iid_almacen + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstAlmacenes = lstAlmacenes;
            frm.SetInsert();
            frm.Show();
            frm.txtNombre.Focus();

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAlmacen Obe = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            frm.lstAlmacenes = lstAlmacenes;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtNombre.Focus();
        }     
    }
}