﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmTablaRegistro : DevExpress.XtraEditors.XtraForm
    {
        private List<ETablaRegistro> lstTabla = new List<ETablaRegistro>();
        public int intTabla = 0;
        public frmTablaRegistro()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTabla = new BGeneral().listarTablaRegistro(intTabla);
            grdTablaRegistro.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.tarec_iid_tabla_registro == intIcod);
            viewTablaRegistro.FocusedRowHandle = index;
            viewTablaRegistro.Focus();
        }
        private void nuevo()
        {
            frmManteTablaRegistro frm = new frmManteTablaRegistro();
            frm.MiEvento += new frmManteTablaRegistro.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            frm.SetInsert();
            frm.Show();

        }
      
        private void modificar()
        {
            ETablaRegistro Obe = (ETablaRegistro)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTablaRegistro frm = new frmManteTablaRegistro();
            frm.MiEvento += new frmManteTablaRegistro.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();
            
        }
        private void eliminar()
        {
            ETablaRegistro Obe = (ETablaRegistro)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {               
                new BAdministracionSistema().eliminarTablaRegistro(Obe);
                cargar();
            }

            
        }
        private void imprimir()
        { 
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void tablaDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        private void buscarCriterio()
        {
            grdTablaRegistro.DataSource = lstTabla.Where(obj =>
                                                   obj.tarec_icorrelativo_registro.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.tarec_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

    }
}