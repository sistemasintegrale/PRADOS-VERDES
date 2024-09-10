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
    public partial class frm02RegistroZonas : DevExpress.XtraEditors.XtraForm
    {
        List<EZona> lstZona = new List<EZona>();
        List<EDistritoZona> lstDistritoZona = new List<EDistritoZona>();
        List<EFamiliaDet> lstFamiliaDet = new List<EFamiliaDet>();

        public frm02RegistroZonas()
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
            
        }
        private void cargar()
        {
            lstZona = new BVentas().listarZona();
            grdZonas.DataSource = lstZona;
            viewZona.Focus();
        }
        private void cargarDistrito(int intIcodZona)
        {
            lstDistritoZona = new BVentas().listarDistritoZona(intIcodZona);
            grdDistritos.DataSource = lstDistritoZona;
            viewDistrito.Focus();
        }
        

        void reload(int intIcod)
        {
            cargar();
            int index = lstZona.FindIndex(x => x.zonc_icod_zona == intIcod);
            viewZona.FocusedRowHandle = index;
            viewZona.Focus();
        }
        void reloadDistrito(int intIcod, int intIcodZona)
        {
            cargarDistrito(intIcodZona);
            int index = lstDistritoZona.FindIndex(x => x.disd_icod_distrito_zona == intIcod);
            viewDistrito.FocusedRowHandle = index;
            viewDistrito.Focus();
        }
       
        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteZonas frm = new frmManteZonas();
            frm.MiEvento += new frmManteZonas.DelegadoMensaje(reload);
            if (lstZona.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstZona.Max(x => x.zonc_iid_zona + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstZona = lstZona;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EZona Obe = (EZona)viewZona.GetRow(viewZona.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteZonas frm = new frmManteZonas();
            frm.MiEvento += new frmManteZonas.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstZona = lstZona;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EZona Obe = (EZona)viewZona.GetRow(viewZona.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().eliminarZona(Obe);
                cargar();
                if (lstZona.Count == 0)
                {
                    cargarDistrito(0);
                    
                }
            }
        }
        #endregion
        #region Distrito
        private void nuevoDistrito()
        {
            EZona Obe = (EZona)viewZona.GetRow(viewZona.FocusedRowHandle);
            if (Obe == null)
            {
                XtraMessageBox.Show("Para poder agregar Distritos, debe registrar una Zona", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmManteDistritos frm = new frmManteDistritos();
            frm.MiEvento += new frmManteDistritos.DelegadoMensaje(reloadDistrito);
            if (lstDistritoZona.Count > 0)
                frm.intIcodZona = Convert.ToInt32(String.Format("{0:00}", lstDistritoZona.Max(x => x.disd_icod_distrito_zona + 1)));
            else
                frm.intIcodZona = 01;
            frm.intIcodZona = Obe.zonc_icod_zona;
            //frm.intDistrito = Obe.disc_icod_distrito;
            frm.lstDistritoZona = lstDistritoZona;
            frm.SetInsert();
            frm.Show();

        }
        
        private void eliminarDistrito()
        {
            EDistritoZona Obe = (EDistritoZona)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().eliminarDistritoZona(Obe);
                cargarDistrito(Obe.zonc_icod_zona);
            }
        }      
        private void buscarCriterio()
        {
            //grdZonas.DataSource = lstZona.Where(x =>
            //                                       x.catf_vabreviatura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
            //                                       x.catf_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
            //                                 ).ToList();

            EDistritoZona Obe = (EDistritoZona)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
            if (Obe != null)
            {
                //lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia);
                ///grdSubFamilia.DataSource = lstFamiliaDet;
                ///viewSubFamilia.GroupPanelText = String.Format("Sub-Líneas de : {0} - {1}", Obe.famic_vabreviatura, Obe.famic_vdescripcion);
            }

            
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoDistrito();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminarDistrito();
        }    

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewDistrito.IsFocusedView)
                nuevoDistrito();
           /// if (viewSubFamilia.IsFocusedView)
                //nuevoSubFamilia();
        }


        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewDistrito.IsFocusedView)
                eliminarDistrito();
            ///if (viewSubFamilia.IsFocusedView)
                //eliminarSubFamilia();
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
    

        private void impSubfamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rptFamilia rpt = new rptFamilia();
            //rpt.cargar("RELACIÓN DE LINEAS DE PRODUCTOS", "", lstFamiliaCab);
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewDistrito.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewDistrito.ClearColumnsFilter();

            ///viewSubFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
           ///viewSubFamilia.ClearColumnsFilter();
        }

        private void viewCategoria_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EZona Obe = (EZona)viewZona.GetRow(viewZona.FocusedRowHandle);
            if (Obe != null)
            {
                lstDistritoZona = new BVentas().listarDistritoZona(Obe.zonc_icod_zona); 
                grdDistritos.DataSource = lstDistritoZona;
                viewDistrito.GroupPanelText = "Sub Linea : " + Obe.zonc_vdescripcion;
                actualizaSubFamilia();
            }
            else
            {
                lstDistritoZona.Clear();
                viewDistrito.RefreshData();
                actualizaSubFamilia();
            }
        }
        private void actualizaSubFamilia()
        {
            EDistritoZona Obe = (EDistritoZona)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.disd_icod_distrito_zona).ToList();
                ///grdSubFamilia.DataSource = lstFamiliaDet;
                ///viewSubFamilia.GroupPanelText = String.Format("Sub-Linea de : {0}", Obe.famic_vdescripcion);
                //txtMarca.Text = viewMarca.GetFocusedRowCellValue("marc_vdescripcion").ToString();
                //txtModelo.Text = viewModelo.GetFocusedRowCellValue("modc_vdescripcion").ToString();
            }
            else
            {
                //lstFamiliaDet.Clear();
                ///viewSubFamilia.RefreshData();
                ///viewSubFamilia.GroupPanelText = "  ";
            }
        }

        private void viewFamilia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EDistritoZona Obe = (EDistritoZona)viewDistrito.GetRow(viewDistrito.FocusedRowHandle);
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

            lstCalcularZona.ForEach(c=>
            {
                lstCalcularDistrito = new BVentas().listarDistritoZona(c.zonc_icod_zona);
                int countF = 0;
                lstCalcularDistrito.ForEach(f =>
                {
                    countF++;
                    f.disd_icod_distrito_zona = countF;
                    new BVentas().modificarDistritoZona(f);

                    //lstCalcularFamiliaDet = new BAlmacen().listarFamiliaDet(f.famic_icod_familia);
                    //int countFD = 0;
                    //lstCalcularFamiliaDet.ForEach(fd=>
                    //{
                    //    countFD++;
                    //    fd.famid_iid_familia = countFD;
                    //    new BAlmacen().modificarFamiliaDet(fd);
                    //});

                });
            });
            cargar();
        


        }

        private void grdCategoria_Click(object sender, EventArgs e)
        {

        }
    }
}