﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class Frm06ActualizacionDevoluciones : DevExpress.XtraEditors.XtraForm
    {
        private List<EKardexValorizadoCompras> mlist = new List<EKardexValorizadoCompras>();
        private string fecInicio;
        private string fecFin;
        public Frm06ActualizacionDevoluciones()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            fecInicio = dtmFechaInicio.Text;
            fecFin = dtmFechaFin.Text;
            lblMensaje.Text = "Espere mientras se cargan los datos...";
            mlist.Clear();
            viewCompras.RefreshData();
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void Cargar()
        {
            try
            {
                mlist = new BAlmacen().ListarKardexValorizadoDevoluciones(Parametros.intEjercicio, Convert.ToDateTime(fecInicio), Convert.ToDateTime(fecFin));                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Frm02ActualizacionDevoluciones_Load(object sender, EventArgs e)
        {
            dtmFechaInicio.EditValue = DateTime.Now;
            dtmFechaFin.EditValue = DateTime.Now;     
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                if (XtraMessageBox.Show("Se actualizará estos movimientos en el Kardex Contable \n\t\t\t\t\t\t¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lblMensaje.Text = "Espere mientras se actualizan los datos...";
                    controlEnable(true);
                    backgroundWorker2.RunWorkerAsync();
                }
            }
            else
                XtraMessageBox.Show("No hay registros por actualizar en el rango de fechas seleccionado", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

        }
        private void SetUpDate()
        {
            
            bool Flag = true;
            try
            {              
                new BAlmacen().EliminarKardexValorizadoActualizacion(Parametros.intEjercicio, Convert.ToDateTime(fecInicio), 
                    Convert.ToDateTime(fecFin), Parametros.intTipoActualizacionDevoluciones);
                new BAlmacen().KardexValorizadoDevolucionesIngresar(mlist, Valores.intUsuario);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;
            }
            //finally
            //{
            //    if (Flag)
            //    {
            //        XtraMessageBox.Show("El Kardex Contable ha sido actualizado satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        mlist.Clear();
            //        viewCompras.RefreshData();
            //    }
            //}
        }
        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            dtmFechaInicio.Enabled = !Enable;
            dtmFechaFin.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
            mnuDevoluciones.Enabled = !Enable;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Cargar();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            controlEnable(false);
            grdCompras.DataSource = mlist;
            viewCompras.GroupPanelText = "Resultado de la Búsqueda - Desde: " + fecInicio + " Hasta: " + fecFin;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            SetUpDate();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            controlEnable(false);
            XtraMessageBox.Show("El Kardex Contable ha sido actualizado satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mlist.Clear();
            viewCompras.RefreshData();
        }
    }
}