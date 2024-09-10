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

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frm02RegistroDistritos : DevExpress.XtraEditors.XtraForm
    {
        List<EDistritos> lstDistritos = new List<EDistritos>();
        //List<EDistritoZona> lstDistrito = new List<EDistritoZona>();
        //List<EFamiliaDet> lstFamiliaDet = new List<EFamiliaDet>();

        public frm02RegistroDistritos()
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
            grdDistritos.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstDistritos = new BVentas().listarDistrito();
            grdDistritos.DataSource = lstDistritos;
            viewZona.Focus();
        }        

        void reload(int intIcod)
        {
            cargar();
            int index = lstDistritos.FindIndex(x => x.disc_icod_distrito == intIcod);
            viewZona.FocusedRowHandle = index;
            viewZona.Focus();
        }
        
       
        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteDistrito frm = new frmManteDistrito();
            frm.MiEvento += new frmManteDistrito.DelegadoMensaje(reload);
            if (lstDistritos.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstDistritos.Max(x => x.disc_iid_distrito + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstDistritos = lstDistritos;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EDistritos Obe = (EDistritos)viewZona.GetRow(viewZona.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteDistrito frm = new frmManteDistrito();
            frm.MiEvento += new frmManteDistrito.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstDistritos = lstDistritos;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EDistritos Obe = (EDistritos)viewZona.GetRow(viewZona.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().eliminarDistrito(Obe);
                cargar();
                if (lstDistritos.Count == 0)
                {
                    cargar();
                }
            }
        }
        #endregion
    

        private void impSubfamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rptFamilia rpt = new rptFamilia();
            //rpt.cargar("RELACIÓN DE LINEAS DE PRODUCTOS", "", lstFamiliaCab);
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            //viewDistrito.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            //viewDistrito.ClearColumnsFilter();

            ///viewSubFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
           ///viewSubFamilia.ClearColumnsFilter();
        }

        private void viewCategoria_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //EZona Obe = (EZona)viewZona.GetRow(viewZona.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    lstDistrito = new BVentas().listarDistrito(Obe.zonc_icod_zona);
            //    grdDistritos.DataSource = lstDistrito;
            //    viewDistrito.GroupPanelText = "Sub Linea : " + Obe.zonc_vdescripcion;
            //    actualizaSubFamilia();
            //}
            //else
            //{
            //    lstDistrito.Clear();
            //    viewDistrito.RefreshData();
            //    actualizaSubFamilia();
            //}
        }
        private void actualizaSubFamilia()
        {
            //EDistritoZona Obe = (EDistritoZona)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.disd_icod_distrito).ToList();
            //    ///grdSubFamilia.DataSource = lstFamiliaDet;
            //    ///viewSubFamilia.GroupPanelText = String.Format("Sub-Linea de : {0}", Obe.famic_vdescripcion);
            //    //txtMarca.Text = viewMarca.GetFocusedRowCellValue("marc_vdescripcion").ToString();
            //    //txtModelo.Text = viewModelo.GetFocusedRowCellValue("modc_vdescripcion").ToString();
            //}
            //else
            //{
            //    //lstFamiliaDet.Clear();
            //    ///viewSubFamilia.RefreshData();
            //    ///viewSubFamilia.GroupPanelText = "  ";
            //}
        }

        private void viewFamilia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //EDistritoZona Obe = (EDistritoZona)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    actualizaSubFamilia();
            //}
            //else
            //{
            //    lstFamiliaDet.Clear();
            //   /// viewSubFamilia.RefreshData();
            //    ///viewSubFamilia.GroupPanelText = "  ";
            //}
        }

        private void imprimirtoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            //EFamiliaCab Obe = (EFamiliaCab)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    List<ECategoriaFamilia> mlist = new List<ECategoriaFamilia>();
            //    mlist = new BAlmacen().listarCategoria_Famili_detalle_Todo();

            //    rptLineaProducto rpt = new rptLineaProducto();
            //    rpt.cargar(mlist, Obe);
            //}
        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<EZona> lstCalcularZona = new List<EZona>();
            List<EDistritoZona> lstCalcularDistrito = new List<EDistritoZona>();
            //List<EFamiliaDet> lstCalcularFamiliaDet = new List<EFamiliaDet>();

            lstCalcularZona = new BVentas().listarZona();

            //lstCalcularZona.ForEach(c=>
            //{
            //    lstCalcularDistrito = new BVentas().listarDistrito(c.zonc_icod_zona);
            //    int countF = 0;
            //    lstCalcularDistrito.ForEach(f =>
            //    {
            //        countF++;
            //        f.disd_iid_distrito = countF;
            //        new BVentas().modificarDistrito(f);

            //        //lstCalcularFamiliaDet = new BAlmacen().listarFamiliaDet(f.famic_icod_familia);
            //        //int countFD = 0;
            //        //lstCalcularFamiliaDet.ForEach(fd=>
            //        //{
            //        //    countFD++;
            //        //    fd.famid_iid_familia = countFD;
            //        //    new BAlmacen().modificarFamiliaDet(fd);
            //        //});

            //    });
            //});
            cargar();
        


        }

        private void grdCategoria_Click(object sender, EventArgs e)
        {

        }
    }
}