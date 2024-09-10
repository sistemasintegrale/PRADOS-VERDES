using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;
using System.Linq;
using SGE.WindowForms.Otros.Ventas;

namespace SGE.WindowForms.Reportes.Ventas
{
    public partial class rptVoucher : DevExpress.XtraReports.UI.XtraReport
    {
        public rptVoucher()
        {
            InitializeComponent();
        }

        public void cargar(EReciboCajaCabecera obe, List<EReciboCajaDetalle> lista, List<ETablaVentaDet> lstTipos)
        {
            if (obe.rc_isituacion != 8)
                lblAnulado.Visible = true;
            else
                lblAnulado.Visible = false;
            lista.ForEach(x=> x.strDescripcion = lstTipos.Where(y=> y.tabvd_iid_tabla_venta_det == x.rc_itipo_pago).FirstOrDefault().tabvd_vdescripcion + x.strDescripcion);

            lblNUmero.Text = obe.rc_vnumero.Insert(4," - ");

            lblFecha.Text = obe.rc_sfecha_recibo.ToString("dd/MM/yyyy");
            lblNombreCliente.Text = obe.strCliente;
            lblRucCliente.Text = string.IsNullOrEmpty(obe.strRucCliente) ? obe.rc_vnro_doc_cliente : obe.strRucCliente;
            lblDireccionCliente.Text = obe.rc_vdireccion_cliente;
            lblNumContrato.Text = obe.strNumContrato;

            this.DataSource = lista;

            lblCantidad.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "rcd_inro_item", "") });
            lblDescripcion.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "strDescripcion", "") });
            lblPrecioUnitario.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "rcd_dprecio_unit", "{0:n2}") });
            lblImporte.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "rcd_dprecio_total", "{0:n2}") });

            lblTotal.Text = obe.rc_dmonto_total.ToString();
            lblLetras.Text = Convertir.ConvertNumeroEnLetras(obe.rc_dmonto_total.ToString()) + " SOLES.";

            subReportefomas rpt = new subReportefomas();
            rpt.cargar(obe.rc_icod_contrato);
            subRptFomas.ReportSource = rpt;

            this.ShowPreview();
        }

    }
}
