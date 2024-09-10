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
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using DevExpress.XtraBars;
using DevExpress.XtraBars.ViewInfo;
using SGE.WindowForms.Otros.Planillas;
using SGE.WindowForms.Almacén.Consultas;

namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class frm11RegistroInasistenciasPersonal : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm03NotaIngresoPorFecha));
        private List<EInasistencia> lstNotaIngreso = new List<EInasistencia>();
        DateTime f1, f2;
        public frm11RegistroInasistenciasPersonal()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            setFechas();
        }

        private void setFechas()
        {
            if (Parametros.intEjercicio == DateTime.Now.Year)
            {
                int dia = DateTime.Now.Day;
                dteFechaDesde.EditValue = DateTime.Now.AddDays(-(dia-1));
                dteFechaHasta.EditValue = DateTime.Now;
            }
            else
            {
                dteFechaDesde.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                dteFechaHasta.EditValue = Convert.ToDateTime("31/12/" + Parametros.intEjercicio.ToString());
            }
            buscar();
        }

        private void buscar()
        {
            BaseEdit oBase = null;
            try
            {
                
                if (string.IsNullOrEmpty(dteFechaDesde.Text) && string.IsNullOrEmpty(dteFechaDesde.Text))
                {
                    lstNotaIngreso = new BPlanillas().ListarInasistenciaPersonal();
                }
                else
                {
                    f1 = (DateTime)dteFechaDesde.EditValue;
                    f2 = (DateTime)dteFechaHasta.EditValue;
                    if (f1.Year != Parametros.intEjercicio)
                    {
                        oBase = dteFechaDesde;
                        throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                    }

                    if (f2.Year != Parametros.intEjercicio)
                    {
                        oBase = dteFechaHasta;
                        throw new ArgumentException("La fecha no esta dentro el año de ejercicio");
                    }

                    if (Convert.ToDateTime(f2.ToShortDateString()) < Convert.ToDateTime(f1.ToShortDateString()))
                    {
                        oBase = dteFechaHasta;
                        throw new ArgumentException("La fecha inicial no puede ser mayor que fecha final");
                    }
                    lstNotaIngreso = new BPlanillas().ListarInasistenciaPersonal();
                    lstNotaIngreso = lstNotaIngreso.Where(x => x.peric_sfecha_anasist >= f1.AddDays(-1)).ToList();
                    lstNotaIngreso = lstNotaIngreso.Where(x => x.peric_sfecha_anasist <= f2.AddDays(1)).ToList();
                    
                }
                grdInasistencias.DataSource = lstNotaIngreso.OrderBy(x=>x.perc_iid_personal);



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewInasistencias.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewInasistencias.ClearColumnsFilter();
        }

        private void barSubItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Link == null)
                OpenSubItem(e.Item);
        }

        void OpenSubItem(BarItem item)
        {
            if (!(item is BarSubItem) || item.Links.Count == 0) return;
            BarSelectionInfo info;
            info = item.Manager.InternalGetService(typeof(BarSelectionInfo)) as BarSelectionInfo;
            if (info != null)
                info.PressLink(item.Links[0]);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
            buscar();
            int index = viewInasistencias.FocusedRowHandle;
            if (lstNotaIngreso.Count >= index + 1)
                viewInasistencias.FocusedRowHandle = index;
            else
                viewInasistencias.FocusedRowHandle = index - 1;
        }

        private void nuevo()
        {
            frmManteInasistenciaPersonal frm = new frmManteInasistenciaPersonal();
            frm.SetInsert();
            frm.ShowDialog();

        }

        private void modificar()
        {

            EInasistencia Obe = (EInasistencia)viewInasistencias.GetRow(viewInasistencias.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteInasistenciaPersonal frm = new frmManteInasistenciaPersonal();
            frm.SetInsert();
            frm.SetModify();
            frm.oBe = Obe;
            frm.setValues();
            frm.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void eliminar()
        {
            try
            {
                EInasistencia Obe = (EInasistencia)viewInasistencias.GetRow(viewInasistencias.FocusedRowHandle);
                if (Obe == null)
                    return;

                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Inasistencia?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.peric_iusuario_elimina = Valores.intUsuario;
                    Obe.peric_vpc_elimina = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().EliminarInasistenciaPersonal(Obe);
                    buscar();
                    int index = viewInasistencias.FocusedRowHandle;
                    if (lstNotaIngreso.Count >= index + 1)
                        viewInasistencias.FocusedRowHandle = index;
                    else
                        viewInasistencias.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportarExcel();
            buscar();
        }

        private void exportarExcel()
        {
            EInasistencia obe = (EInasistencia)viewInasistencias.GetRow(viewInasistencias.FocusedRowHandle);
            if (obe != null)
            {
                try
                {

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        grdInasistencias.DataSource = lstNotaIngreso.OrderBy(x=>x.perc_iid_personal);
                        string fileName = sfd.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdInasistencias.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdInasistencias.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdInasistencias.DataSource = null;
                        sfd.FileName = string.Empty;
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void grdInasistencias_Click(object sender, EventArgs e)
        {

        }

    }
}