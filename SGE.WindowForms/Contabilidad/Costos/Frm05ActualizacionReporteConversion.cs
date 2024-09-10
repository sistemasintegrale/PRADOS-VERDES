using System;
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
using SGE.WindowForms.Otros.Administracion_del_Sistema;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class Frm05ActualizacionReporteConversion : DevExpress.XtraEditors.XtraForm
    {
        private List<EKardexValorizadoCompras> mlist = new List<EKardexValorizadoCompras>();
        private string fecInicio;
        private string fecFin;
        private bool Flag;

        public Frm05ActualizacionReporteConversion()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {TimeSpan ts = dtmFechaFin.DateTime - dtmFechaInicio.DateTime;
            int differenceInDays = ts.Days;

            if (differenceInDays >= 0)
            {
                fecInicio = dtmFechaInicio.Text;
                fecFin = dtmFechaFin.Text;
                lblMensaje.Text = "Espere mientras se cargan los datos...";
                mlist.Clear();
                viewCompras.RefreshData();
                controlEnable(true);
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                XtraMessageBox.Show("El rango de fechas es incorrecto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Cargar()
        {
            try
            {
                mlist = new BAlmacen().ListarKardexValorizadoReporteConversion(Parametros.intEjercicio, Convert.ToDateTime(fecInicio), Convert.ToDateTime(fecFin));
              
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Frm04ActualizacionVentas_Load(object sender, EventArgs e)
        {
            dtmFechaInicio.EditValue = DateTime.Now;
            dtmFechaFin.EditValue = DateTime.Now;      
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                //if (VerificarFechaBloqueo())
                //{
                    if (XtraMessageBox.Show("Se actualizará estos movimientos en el Kardex Contable \n\t\t\t\t\t\t¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        lblMensaje.Text = "Espere mientras se actualizan los datos...";
                        controlEnable(true);
                        backgroundWorker2.RunWorkerAsync();
                    }
                //}
            }
            else
                XtraMessageBox.Show("El rango de fechas es incorrecto", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        //private bool VerificarFechaBloqueo()
        //{
        //    bool opc = true;
        //    try
        //    {
        //        DateTime? fechaBloq = null;
        //        if (!new BFechaRegistro().VerificarFechaBloqueo(dtmFechaInicio.DateTime, ref fechaBloq, Parametros.intBloqFecActTrans))
        //        {
        //            XtraMessageBox.Show("La fecha de actualización debe ser mayor al " + Convert.ToDateTime(fechaBloq).ToString("dd/MM/yyyy"), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            opc = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        opc = false;
        //        throw ex;
        //    }
        //    return opc;
        //}

        private void SetUpDate()
        {
            BAlmacen obl = new BAlmacen();
            try
            {
                obl.EliminarKardexValorizadoActualizacion(Parametros.intEjercicio, Convert.ToDateTime(fecInicio), Convert.ToDateTime(fecFin), Parametros.intTipoActualizacionConversion);
                //obl.KardexValorizadoTransformacionesIngresar(mlist, Valores.CodeUser);               
                //obl.insertarKardexValorizadoPorTipoActualizacion(Parametros.intPeriodo, Convert.ToDateTime(fecInicio), Convert.ToDateTime(fecFin), Parametros.intTipoActualizacionTransformaciones);
                /*Falta Completar*/
                obl.insertarKardexValorizadoTransformaciones(Parametros.intEjercicio, Convert.ToDateTime(fecInicio), Convert.ToDateTime(fecFin), Parametros.intTipoActualizacionConversion, mlist);
                Flag = true;
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
            mnuVentas.Enabled = !Enable;
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
            if (Flag)
            {
                XtraMessageBox.Show("El Kardex Contable ha sido actualizado satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mlist.Clear();
                viewCompras.RefreshData();
            }
        }
    }
}