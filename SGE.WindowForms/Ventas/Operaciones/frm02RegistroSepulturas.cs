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
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Reportes.Almacen.Registros;
using DevExpress.XtraReports.UI;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frm02RegistroSepulturas : DevExpress.XtraEditors.XtraForm
    {
        List<EEspacios> lstEspacios = new List<EEspacios>();

        public frm02RegistroSepulturas()
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
            grdEspacios.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstEspacios = new BVentas().listarEspacios();
            grdEspacios.DataSource = lstEspacios;
            viewEspacios.Focus();
        }        

        void reload(int intIcod)
        {
            cargar();
            int index = lstEspacios.FindIndex(x => x.espac_iid_iespacios == intIcod);
            viewEspacios.FocusedRowHandle = index;
            viewEspacios.Focus();
        }
        
       
        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteEspacios frm = new frmManteEspacios();
            frm.MiEvento += new frmManteEspacios.DelegadoMensaje(reload);
            if (lstEspacios.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:000000}", lstEspacios.Max(x=> Convert.ToInt32(x.espac_icod_vespacios) + 1) );
            else
                frm.txtCodigo.Text = "000001";
            frm.lstEspacios = lstEspacios;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EEspacios Obe = (EEspacios)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {

                //List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_iestado == 16).ToList();
                //if (lstEspacioDet.Count > 0)
                //{
                //    throw new ArgumentException(String.Format("El Espacio no puede ser Modificado, su Estado es OCUPADO"));
                //}

                //List<EEspaciosDet> lstEspacioDetSit = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_isituacion == 14).ToList();
                //if (lstEspacioDetSit.Count > 0)
                //{
                //    throw new ArgumentException(String.Format("El Espacio no puede ser Modificado, su Situacion es CON CONTRATO"));
                //}


                frmManteEspacios frm = new frmManteEspacios();
                frm.MiEvento += new frmManteEspacios.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstEspacios = lstEspacios;
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EEspacios Obe = (EEspacios)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_iestado == 16).ToList();
                if (lstEspacioDet.Count > 0)
                {
                    throw new ArgumentException(String.Format("El Espacio no puede ser Eliminado, su Estado es OCUPADO"));
                }

                List<EEspaciosDet> lstEspacioDetSit = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_isituacion == 14).ToList();
                if (lstEspacioDetSit.Count > 0)
                {
                    throw new ArgumentException(String.Format("El Espacio no puede ser Eliminado, su Situacion es CON CONTRATO"));
                }

                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarEspacios(Obe);
                    cargar();
                    if (lstEspacios.Count == 0)
                    {
                        cargar();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            viewEspacios.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewEspacios.ClearColumnsFilter();

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
            EContrato ObeCO = (EContrato)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
             

            rptContratos rptNotaCredito = new rptContratos();

                rptNotaCredito.cargar(new BVentas().ContratoImpresion(ObeCO.cntc_icod_contrato));
                rptNotaCredito.ShowPreview();
            
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

        private void filtrar()
        {
            grdEspacios.DataSource = lstEspacios.Where(x => x.strplataforma.Contains(txtPlataforma.Text)
            && x.strmanzana.Contains(txtManzana.Text.ToUpper()) && x.strsepultura.Contains(txtSepultura.Text.ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtPlataforma_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtSepultura_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {


                grdEspacios.DataSource = lstEspacios;
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdEspacios.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdEspacios.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                //grdContrato.DataSource = null;
                sfdRuta.FileName = string.Empty;



            }
        }
    }
}