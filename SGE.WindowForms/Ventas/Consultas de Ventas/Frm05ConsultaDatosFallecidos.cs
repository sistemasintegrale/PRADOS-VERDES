using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Consultas_de_Ventas
{
    public partial class Frm05ConsultaDatosFallecidos : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> listContratos = new List<EContrato>();
        DateTime fechaInicio = new DateTime(Parametros.intEjercicio, 1, 1);
        DateTime fechaFin = DateTime.Now.Date;
        bool indicador = false;
        public Frm05ConsultaDatosFallecidos()
        {
            InitializeComponent();
        }

        private void Frm05ConsultaDatosFallecidos_Load(object sender, EventArgs e)
        {
            dteFechaInicio.DateTime = fechaInicio;
            dteFechaFin.DateTime = fechaFin;
            indicador = true;
            BSControls.LoaderLookRepository(lkpGrdTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpGrdAsesor, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);;
            cargar();
        }

        async void cargar()
        {
            fechaInicio = dteFechaInicio.DateTime;
            fechaFin = dteFechaFin.DateTime;
            Task<List<EContrato>> taskFallecidos = new Task<List<EContrato>>(ObtenerLista);
            taskFallecidos.Start();
            listContratos = await taskFallecidos;
            grdLista.DataSource = listContratos;
            grdLista.RefreshDataSource();
            viewLista.BestFitColumns();

        }

        List<EContrato> ObtenerLista() { return listContratos = new BVentas().ConsultaDatosFallecido(fechaInicio, fechaFin); }

        private void dteFechaInicio_EditValueChanged(object sender, EventArgs e)
        {
            if(indicador)
            cargar();
        }

        private void dteFechaFin_EditValueChanged(object sender, EventArgs e)
        {
            if (indicador)
                cargar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdRuta = new SaveFileDialog();
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
              
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
            }
        }

        private void grdLista_Click(object sender, EventArgs e)
        {

        }
    }
}