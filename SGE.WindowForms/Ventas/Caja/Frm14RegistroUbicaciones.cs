using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;

namespace SGE.WindowForms.Ventas.Caja
{
    public partial class Frm14RegistroUbicaciones : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> listContratos = new List<EContrato>();
        public DateTime inicio = new DateTime(Parametros.intEjercicio, 1, 1);
        public DateTime final = DateTime.Today;
        public Frm14RegistroUbicaciones()
        {
            InitializeComponent();
        }

        private void Frm14RegistroUbicaciones_Load(object sender, EventArgs e)
        {
            dteInicio.DateTime = new DateTime(Parametros.intEjercicio, 1, 1);
            dteFinal.DateTime = DateTime.Today;
            CargarControles();
            Cargar();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null)
                Cargar();
            else
                reload(Obe.cntc_icod_contrato_fallecido);
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrar();
        }
        void CargarControles()
        {
            var lstVendedor = new BVentas().listarVendedor().ToList();
            lstVendedor.Add(new EVendedor() { vendc_icod_vendedor = 0, vendc_vnombre_vendedor = "Todos..." });

            BSControls.LoaderLook(lkpAsesor, lstVendedor, "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            lkpAsesor.EditValue = 0;
            BSControls.LoaderLookRepository(lkpgrdAtendio, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLookRepository(lkpgrdTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpgrdMZ, new BGeneral().listarTablaVentaDet(5), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpgrdTamaLap, new BGeneral().listarTablaVentaDet(27), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpgrdNsep, new BGeneral().listarTablaVentaDet(12), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);

            BSControls.LoaderLookRepository(lkpGrdEspacios, new BVentas().listarEspacios(), "espac_icod_vespacios", "espac_iid_iespacios", true);
            BSControls.LoaderLookRepository(lkpGrdPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
        }

        void Cargar()
        {
            inicio = dteInicio.DateTime;
            final = dteFinal.DateTime;
            listContratos = new BVentas().listar_ubicaciones_fallecidos(dteInicio.DateTime, dteFinal.DateTime);
            grdLista.DataSource = listContratos;
            grdLista.RefreshDataSource();
        }

        void reload(int icod)
        {
            Cargar();
            int index = listContratos.FindIndex(x => x.cntc_icod_contrato_fallecido == icod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }

        void BuscarCriterio()
        {
            string asesor = Convert.ToInt32(lkpAsesor.EditValue) == 0 ? "" : lkpAsesor.Text;
            viewLista.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtcontrato.Text + "%'");
            viewLista.Columns["strNombreCompletoFallecido"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompletoFallecido] LIKE '%" + txtFallecido.Text + "%'");
            viewLista.Columns["cntc_icod_vendedor"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_icod_vendedor] LIKE '%" + asesor + "%'");
        }

        void Registrar()
        {

            frmManteUbicaciones frm = new frmManteUbicaciones();
            frm.MiEvento += new frmManteUbicaciones.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.ShowDialog();
        }

        private void exportarAExelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdRuta = new SaveFileDialog();
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                gridColumn20.Visible = false;
                gridColumn21.Visible = false;
                gridColumn22.Visible = false;

                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdLista.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdLista.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }

                gridColumn20.Visible = true;
                gridColumn21.Visible = true;
                gridColumn22.Visible = true;


            }
        }

        private void dteInicio_EditValueChanged(object sender, EventArgs e)
        {
            dteFinal.DateTime = final;
            Cargar();
            BuscarCriterio();
        }

        private void dteFinal_EditValueChanged(object sender, EventArgs e)
        {
            Cargar();
            BuscarCriterio();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null)
                return;

            var Obecontrato = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            frmManteUbicaciones frm = new frmManteUbicaciones();
            frm.MiEvento += new frmManteUbicaciones.DelegadoMensaje(reload);
            frm.icodContrato = Obe.cntcc_icod_contratante;
            frm.numContrato = Obe.cntc_vnumero_contrato;
            frm.asesor = Convert.ToInt32(Obe.cntc_icod_vendedor);
            frm.icodEspacio = Obe.espac_iid_iespacios;
            frm.Obe = new BVentas().ObnterFallecido(Obe.cntc_icod_contrato_fallecido);
            frm.Text = $"Fallecido del Contrato N° {Obe.cntc_vnumero_contrato}";
            frm.obeContrato = Obecontrato;
            frm.SetModify();
            frm.btnLimpiar.Enabled = true;
            frm.ShowDialog();            
        }

        private void viewLista_DoubleClick(object sender, EventArgs e)
        {

            EContrato Obe = (EContrato)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null)
                return;


            frmManteUbicaciones frm = new frmManteUbicaciones();
            frm.MiEvento += new frmManteUbicaciones.DelegadoMensaje(reload);
            frm.icodContrato = Obe.cntcc_icod_contratante;
            frm.numContrato = Obe.cntc_vnumero_contrato;
            frm.asesor = Convert.ToInt32(Obe.cntc_icod_vendedor);
            frm.icodEspacio = Obe.espac_iid_iespacios;
            frm.Obe = new BVentas().ObnterFallecido(Obe.cntc_icod_contrato_fallecido);
            frm.SetCancel();
            frm.Text = $"Fallecido del Contrato N° {Obe.cntc_vnumero_contrato}";
            frm.ShowDialog();
        }

        private void grdLista_Click(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}