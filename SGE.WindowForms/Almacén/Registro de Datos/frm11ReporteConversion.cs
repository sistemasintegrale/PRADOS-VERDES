using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Security.Principal;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Reportes.Almacen.Registros;

namespace SGE.WindowForms.Almacén.Registro_de_Datos 
{
    public partial class frm11ReporteConversion : DevExpress.XtraEditors.XtraForm
    {
        private List<EReporteConversionCab> lstNotaIngreso = new List<EReporteConversionCab>();

        public frm11ReporteConversion()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstNotaIngreso = new BAlmacen().ReporteConversionListar();
            grdNotaIngreso.DataSource = lstNotaIngreso;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaIngreso.FindIndex(x => x.rcc_icod_reporte_conversion == intIcod);
            viewNotaIngreso.FocusedRowHandle = index;
            viewNotaIngreso.Focus();
        }      
      
        private void viewNotaIngreso_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                EReporteConversionCab Obe = (EReporteConversionCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteReporteConversion frm = new frmManteReporteConversion();
                frm.MiEvento += new frmManteReporteConversion.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotaIngreso;
                frm.oBe = Obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
               
                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nuevo()
        {
            try
            {
                frmManteReporteConversion frm = new frmManteReporteConversion();
                frm.MiEvento += new frmManteReporteConversion.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotaIngreso;
                frm.Show();
                frm.SetInsert();
                frm.lkpTipo.EditValue = 240;
                if (lstNotaIngreso.Count == 0)
                {
                    frm.txtNumero.Text = "0001";
                }
                else
                    frm.txtNumero.Text = string.Format("{0:0000}", (lstNotaIngreso.Max(ob => Convert.ToInt32(ob.rcc_vnuemro_reporte_conversion)) + 1));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            try
            {

                EReporteConversionCab Obe = (EReporteConversionCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteReporteConversion frm = new frmManteReporteConversion();
                frm.MiEvento += new frmManteReporteConversion.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotaIngreso;
                frm.oBe = Obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void eliminar()
        {
            try
            {
                EReporteConversionCab Obe = (EReporteConversionCab)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewNotaIngreso.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar este registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().ReporteConversionEliminar(Obe);
                    cargar();
                    if (lstNotaIngreso.Count >= index + 1)
                        viewNotaIngreso.FocusedRowHandle = index;
                    else
                        viewNotaIngreso.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            EEmpaquePlantilla obe = (EEmpaquePlantilla)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
            if (obe == null)
                return;
            var lstDetalle = new BAlmacen().EmpaquePlantillaDetListar(obe.plemc_iid);
            //rptNotaIngreso rpt = new rptNotaIngreso();
            //rpt.cargar(String.Format("PLANILLA EMPAQUE N° {0}", string.Format("{0:0000}",obe.plemc_icod)), lstDetalle, obe);
            
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

        private void lkpAlmacen_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewNotaIngreso.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewNotaIngreso.ClearColumnsFilter();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
        private void buscarCriterio()
        {
            grdNotaIngreso.DataSource = lstNotaIngreso.Where(x =>
                                                   x.rcc_vnuemro_reporte_conversion.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.prdc_vdescripcion_larga.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
    }
}