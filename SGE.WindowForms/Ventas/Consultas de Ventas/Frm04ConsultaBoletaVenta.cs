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
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Ventas.Reporte;
using DevExpress.XtraReports.UI;

namespace SGE.WindowForms.Ventas.Consultas_de_Ventas
{
    public partial class Frm04ConsultaBoletaVenta : DevExpress.XtraEditors.XtraForm
    {

        public List<EBoletaCab> lista = new List<EBoletaCab>();
        public Frm04ConsultaBoletaVenta()
        {
            InitializeComponent();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        public void imprimir()
        {
            EBoletaCab ObeFC = (EBoletaCab)viewFacturas.GetRow(viewFacturas.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            string TD = "03";

            lstFE = new BVentas().FacturaVentaElectronicaObtenerDoc(ObeFC.bovc_icod_boleta ,TD).ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);


            rptBoletaElectronico rptBoleta = new rptBoletaElectronico();

            rptBoleta.cargar(Obe, mlisdet, Obe.Hora);
            rptBoleta.ShowPreview();
        }

        private void Frm03ConsultaFacturasVenta_Load(object sender, EventArgs e)
        {
            cargar();
        }

        void cargar()
        {
            lista = new BVentas().listarBoletaCabPlanilla(2022);
            grdFacturas.DataSource = lista;
            grdFacturas.Refresh();
        }

        void BuscarCriterio()
        {
            grdFacturas.DataSource = lista.Where(x => x.cntc_vnumero_contrato.Contains(txtContrato.Text)
                                     && x.cliec_vnumero_doc_cli.Contains(txtCodigo.Text)
                                     && x.clic_vcod_cliente.Contains(txtCodigo.Text)
                                     );
            grdFacturas.Refresh();
        }
    }
}