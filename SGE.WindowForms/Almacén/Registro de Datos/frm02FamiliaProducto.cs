using System;
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
using SGE.WindowForms.Reportes.Almacen.Registros;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm02FamiliaProducto : DevExpress.XtraEditors.XtraForm
    {
        List<ECategoriaFamilia> lstCategoriaFamilia = new List<ECategoriaFamilia>();
        List<EFamiliaCab> lstFamiliaCab = new List<EFamiliaCab>();
        List<EFamiliaDet> lstFamiliaDet = new List<EFamiliaDet>();

        public frm02FamiliaProducto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            cargarGridSize();
        }

        private void cargarGridSize()
        {
            grdFamilia.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstCategoriaFamilia = new BAlmacen().listarCategoriaFamilia();
            grdCategoria.DataSource = lstCategoriaFamilia;
            viewCategoria.Focus();
        }
        private void cargarFamilia(int intIcodCategoria)
        {
            lstFamiliaCab = new BAlmacen().listarFamiliaCab(intIcodCategoria);
            grdFamilia.DataSource = lstFamiliaCab;
            viewFamilia.Focus();
        }
        private void cargarSubFamilia(int intIcodFamilia)
        {
            lstFamiliaDet = new BAlmacen().listarFamiliaDet(intIcodFamilia).ToList();
          //  grdSubFamilia.DataSource = lstFamiliaDet;
           // viewSubFamilia.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstCategoriaFamilia.FindIndex(x => x.catf_icod_categoria == intIcod);
            viewCategoria.FocusedRowHandle = index;
            viewCategoria.Focus();
        }
        void reloadFamilia(int intIcod, int intIcodCategoria)
        {
            cargarFamilia(intIcodCategoria);
            int index = lstFamiliaCab.FindIndex(x => x.famic_icod_familia == intIcod);
            viewFamilia.FocusedRowHandle = index;
            viewFamilia.Focus();
        }
        void reloadSubFamilia(int intIcod, int intIcodFamilia)
        {
            cargarSubFamilia(intIcodFamilia);
            int index = lstFamiliaDet.FindIndex(x => x.famid_icod_familia == intIcod);
           // viewSubFamilia.FocusedRowHandle = index;
           // viewSubFamilia.Focus();
        }
        #region Categoria Familia
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteCategoriaFamilia frm = new frmManteCategoriaFamilia();
            frm.MiEvento += new frmManteCategoriaFamilia.DelegadoMensaje(reload);
            if (lstCategoriaFamilia.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstCategoriaFamilia.Max(x => x.catf_iid_categoria + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstCategoriaFamilia = lstCategoriaFamilia;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ECategoriaFamilia Obe = (ECategoriaFamilia)viewCategoria.GetRow(viewCategoria.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteCategoriaFamilia frm = new frmManteCategoriaFamilia();
            frm.MiEvento += new frmManteCategoriaFamilia.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstCategoriaFamilia = lstCategoriaFamilia;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ECategoriaFamilia Obe = (ECategoriaFamilia)viewCategoria.GetRow(viewCategoria.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BAlmacen().eliminarCategoriaFamilia(Obe);
                cargar();
                if (lstCategoriaFamilia.Count == 0)
                {
                    cargarFamilia(0);
                    cargarSubFamilia(0);
                }
            }
        }
        #endregion
        #region Familia Cab
        private void nuevoFamilia()
        {
            ECategoriaFamilia Obe = (ECategoriaFamilia)viewCategoria.GetRow(viewCategoria.FocusedRowHandle);
            if (Obe == null)
            {
                XtraMessageBox.Show("Para poder registrar Líneas de Productos, debe registrar una Categoria", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmManteFamiliaCab frm = new frmManteFamiliaCab();
            frm.MiEvento += new frmManteFamiliaCab.DelegadoMensaje(reloadFamilia);
            if (lstFamiliaCab.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstFamiliaCab.Max(x => x.famic_iid_familia + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.intIcodCategoria = Obe.catf_icod_categoria;
            frm.lstFamiliaCab = lstFamiliaCab;
            frm.SetInsert();
            frm.Text = String.Format("Mantenimiento - Registro de Sub Línea de {0}", Obe.catf_vdescripcion);
            frm.Show();
            frm.txtCodigo.Focus();

        }
        private void modificarFamilia()
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteFamiliaCab frm = new frmManteFamiliaCab();
            frm.MiEvento += new frmManteFamiliaCab.DelegadoMensaje(reloadFamilia);
            frm.Obe = Obe;
            frm.intIcodCategoria = Obe.catf_icod_categoria;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }
        private void eliminarFamilia()
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BAlmacen().eliminarFamiliaCab(Obe);
                cargarFamilia(Obe.catf_icod_categoria);
            }
        }      
        private void buscarCriterio()
        {
            grdCategoria.DataSource = lstCategoriaFamilia.Where(x =>
                                                   x.catf_vabreviatura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.catf_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();

            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia);
                ///grdSubFamilia.DataSource = lstFamiliaDet;
                ///viewSubFamilia.GroupPanelText = String.Format("Sub-Líneas de : {0} - {1}", Obe.famic_vabreviatura, Obe.famic_vdescripcion);
            }

            
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoFamilia();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificarFamilia();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminarFamilia();
        }    

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewFamilia.IsFocusedView)
                nuevoFamilia();
           /// if (viewSubFamilia.IsFocusedView)
                nuevoSubFamilia();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewFamilia.IsFocusedView)
                modificarFamilia();
            ///if (viewSubFamilia.IsFocusedView)
                modificarSubFamilia();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewFamilia.IsFocusedView)
                eliminarFamilia();
            ///if (viewSubFamilia.IsFocusedView)
                eliminarSubFamilia();
        }


        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
        #endregion
        #region Familia Det
        private void nuevoSubFamilia()
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
            {
                XtraMessageBox.Show("Para poder registrar Sub Líneas de Productos, debe registrar una Familia Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmManteFamiliaDet frm = new frmManteFamiliaDet();
            frm.MiEvento += new frmManteFamiliaDet.DelegadoMensaje(reloadSubFamilia);
            if (lstFamiliaDet.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstFamiliaDet.Max(x => x.famid_iid_familia + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.intIcodFamiliaCab = Obe.famic_icod_familia;
            frm.lstFamiliaDet = lstFamiliaDet;
            frm.SetInsert();
            frm.Text = String.Format("Mantenimiento - Registro de Sub Línea de {0}", Obe.famic_vdescripcion);
            frm.Show();
            frm.txtCodigo.Focus();
            frm.bteCuentaSerPropio.Enabled = false;
            
        }
        private void modificarSubFamilia()
        {

            //EFamiliaDet Obe = (EFamiliaDet)viewSubFamilia.GetRow(viewSubFamilia.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmManteFamiliaDet frm = new frmManteFamiliaDet();
            //frm.MiEvento += new frmManteFamiliaDet.DelegadoMensaje(reloadSubFamilia);
            //frm.Obe = Obe;
            //frm.intIcodFamiliaCab = Obe.famic_icod_familia;
            //frm.SetModify();
            //frm.Show();
            //frm.setValues();
            //frm.txtDescripcion.Focus();
            
        }
        private void eliminarSubFamilia()
        {
            //EFamiliaDet Obe = (EFamiliaDet)viewSubFamilia.GetRow(viewSubFamilia.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    Obe.intUsuario = Valores.intUsuario;
            //    Obe.strPc = WindowsIdentity.GetCurrent().Name;
            //    new BAlmacen().eliminarFamiliaDet(Obe);
            //    cargarSubFamilia(Obe.famic_icod_familia);
            //}
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nuevoSubFamilia();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            modificarSubFamilia();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            eliminarSubFamilia();
        }      
        
        #endregion

        private void impSubfamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptFamilia rpt = new rptFamilia();
            rpt.cargar("RELACIÓN DE LINEAS DE PRODUCTOS", "", lstFamiliaCab);
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewFamilia.ClearColumnsFilter();

            ///viewSubFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
           ///viewSubFamilia.ClearColumnsFilter();
        }

        private void viewCategoria_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ECategoriaFamilia Obe = (ECategoriaFamilia)viewCategoria.GetRow(viewCategoria.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaCab = new BAlmacen().listarFamiliaCab(Obe.catf_icod_categoria);
                grdFamilia.DataSource = lstFamiliaCab;
                viewFamilia.GroupPanelText = "Sub Linea : " + Obe.catf_vdescripcion;
                actualizaSubFamilia();
            }
            else
            {
                lstFamiliaCab.Clear();
                viewFamilia.RefreshData();
                actualizaSubFamilia();
            }
        }
        private void actualizaSubFamilia()
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia).ToList();
                ///grdSubFamilia.DataSource = lstFamiliaDet;
                ///viewSubFamilia.GroupPanelText = String.Format("Sub-Linea de : {0}", Obe.famic_vdescripcion);
                //txtMarca.Text = viewMarca.GetFocusedRowCellValue("marc_vdescripcion").ToString();
                //txtModelo.Text = viewModelo.GetFocusedRowCellValue("modc_vdescripcion").ToString();
            }
            else
            {
                lstFamiliaDet.Clear();
                ///viewSubFamilia.RefreshData();
                ///viewSubFamilia.GroupPanelText = "  ";
            }
        }

        private void viewFamilia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                actualizaSubFamilia();
            }
            else
            {
                lstFamiliaDet.Clear();
               /// viewSubFamilia.RefreshData();
                ///viewSubFamilia.GroupPanelText = "  ";
            }
        }

        private void imprimirtoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                List<ECategoriaFamilia> mlist = new List<ECategoriaFamilia>();
                mlist = new BAlmacen().listarCategoria_Famili_detalle_Todo();

                rptLineaProducto rpt = new rptLineaProducto();
                rpt.cargar(mlist, Obe);
            }
        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<ECategoriaFamilia> lstCalcularCategoria = new List<ECategoriaFamilia>();
            List<EFamiliaCab> lstCalcularFamilia = new List<EFamiliaCab>();
            List<EFamiliaDet> lstCalcularFamiliaDet = new List<EFamiliaDet>();

            lstCalcularCategoria = new BAlmacen().listarCategoriaFamilia();

            lstCalcularCategoria.ForEach(c=>
            {
                lstCalcularFamilia = new BAlmacen().listarFamiliaCab(c.catf_icod_categoria);
                int countF = 0;
                lstCalcularFamilia.ForEach(f =>
                {
                    countF++;
                    f.famic_iid_familia = countF;
                    new BAlmacen().modificarFamiliaCab(f);

                    lstCalcularFamiliaDet = new BAlmacen().listarFamiliaDet(f.famic_icod_familia);
                    int countFD = 0;
                    lstCalcularFamiliaDet.ForEach(fd=>
                    {
                        countFD++;
                        fd.famid_iid_familia = countFD;
                        new BAlmacen().modificarFamiliaDet(fd);
                    });

                });
            });
            cargar();
        


        }

        private void grdCategoria_Click(object sender, EventArgs e)
        {

        }
    }
}