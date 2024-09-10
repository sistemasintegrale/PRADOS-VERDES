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
    public partial class Frm03ConsultaFacturasVenta : DevExpress.XtraEditors.XtraForm
    {

        public List<EFacturaCab> lista = new List<EFacturaCab>();
        public Frm03ConsultaFacturasVenta()
        {
            InitializeComponent();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        public void imprimir()
        {
            EFacturaCab ObeFC = (EFacturaCab)viewFacturas.GetRow(viewFacturas.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            string TD = "01";

            lstFE = new BVentas().FacturaVentaElectronicaObtenerDoc(ObeFC.favc_icod_factura,TD).ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);


            rptFacturaElectronico rptFcatura = new rptFacturaElectronico();

            rptFcatura.cargar(Obe, mlisdet, Obe.Hora);
            rptFcatura.ShowPreview();


        }

        private void Frm03ConsultaFacturasVenta_Load(object sender, EventArgs e)
        {
            cargar();
        }

        void cargar()
        {
            lista = new BVentas().listarFacturaCabPlanilla(2022);
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