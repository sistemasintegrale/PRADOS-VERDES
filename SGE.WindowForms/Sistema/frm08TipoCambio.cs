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

namespace SGE.WindowForms.Sistema
{
    public partial class frm08TipoCambio : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoCambio> lstTipoCambio = new List<ETipoCambio>();
        public frm08TipoCambio()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstTipoCambio = new BAdministracionSistema().listarTipoCambio();
            grdTipoCambio.DataSource = lstTipoCambio;
            viewTipoCambio.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTipoCambio.FindIndex(x => x.ticac_icod_tipo_cambio == intIcod);
            viewTipoCambio.FocusedRowHandle = index;
            viewTipoCambio.Focus();   
        }        
        private void nuevo()
        {
            frmManteTipoCambio frm = new frmManteTipoCambio();
            frm.MiEvento += new frmManteTipoCambio.DelegadoMensaje(reload);
            frm.lstTipoCambio = lstTipoCambio;
            frm.SetInsert();
            frm.dteFecha.EditValue = DateTime.Now;
            frm.ShowDialog();            
        }
        private void modificar()
        {
            ETipoCambio Obe = (ETipoCambio)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoCambio frm = new frmManteTipoCambio();
            frm.MiEvento += new frmManteTipoCambio.DelegadoMensaje(reload);
            frm.lstTipoCambio = lstTipoCambio;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();    
            frm.ShowDialog();
        
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            ETipoCambio Obe = (ETipoCambio)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoCambio frm = new frmManteTipoCambio();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.setValues();
            frm.ShowDialog();

        }
        private void eliminar()
        {
            ETipoCambio Obe = (ETipoCambio)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BAdministracionSistema().eliminarTipoCambio(Obe);
                cargar();
            }
        }
        private void imprimir()
        {
           
        }
        
        private void buscarCriterio()
        {           
            grdTipoCambio.DataSource = lstTipoCambio.Where(x =>
                                                   x.ticac_fecha_tipo_cambio.ToShortDateString().StartsWith(txtCodigo.Text.ToUpper())).ToList();
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

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }       
    }
}