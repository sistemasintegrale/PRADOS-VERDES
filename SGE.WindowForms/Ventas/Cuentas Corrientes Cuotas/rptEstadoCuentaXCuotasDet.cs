using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using QRCoder;
using System.Linq;
using SGE.WindowForms.Otros.Ventas;
using System.Security.Principal;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptEstadoCuentaXCuotasDet : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEstadoCuentaXCuotasDet()
        {
            InitializeComponent();
        }
        public void cargar(EContrato Obe, List<EContratoCuotas> lista)
        {
            lista = lista.Where(x => x.cntc_icod_situacion != 6437).ToList();//SE FILTRAN LOS REPROGRAMADOS
            //CABEZERA
            lblNumeroContrato.Text = Obe.cntc_vnumero_contrato;
            lblNombres.Text = Obe.strNombreContratante;
            lblTipoDeEspacio.Text = Obe.strtiposepultura;

            lblFecha.Text = Obe.cntc_sfecha_contrato.Value.ToString("dd/MM/yyyy");
            lblPrecio.Text = "S/ " + Obe.cntc_nprecio_lista.Value.ToString("N2");
            lblCuotaInicial.Text = "S/ " + Obe.cntc_ncuota_inicial.Value.ToString("N2");
            lblNumeroCuotas.Text = Obe.cntc_inro_cuotas.ToString();
            lblCuotas.Text = "S/ " + Obe.cntc_nmonto_cuota.Value.ToString("N2");
            lblFomaCab.Text = "S/ " + (Obe.cntc_nmonto_foma == 0 ? Obe.cntc_naporte_fondo.Value.ToString("N2") : Obe.cntc_nmonto_foma.ToString("N2"));
            lblCrediticioCab.Text = "S/ " + Obe.cntc_nfinanciamientro.ToString("N2");
            //lblObservaciones.Text = Obe.cntc_vobservaciones;
            lblNombrePlan.Text = Obe.strNombreplan;
            lista = lista.OrderBy(x => x.cntc_itipo_cuota).ThenBy(x => x.cntc_inro_cuotas).ToList();
            this.DataSource = lista;

            lblNumeroCuota.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_inro_cuotas", "") });
            lbltipo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "tipo", "") });
            lblFechaDePagos.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_sfecha_cuota", "{0:dd/MM/yyyy}") });
            lblMontoPago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_cuota", "{0:N2}") });
            lblFechaDelPago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_sfecha_pago_cuota", "{0:dd/MM/yyyy}") });
            lblMontoPagado.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_npagado", "{0:N2}") });
            lblMora.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_mora", "{0:N2}") });
            lblSaldo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nsaldo", "{0:N2}") });
            lblEstado.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "strSituacion", "") });
            lblMontoMoraPagado.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_mora_pago", "{0:N2}") });


            //Footer
            lblMontoPagoSum.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_cuota", "{0:N2}") });
            lblMontoPagadoSum.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_npagado", "{0:N2}") });
            lblMoraSum.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_mora", "{0:N2}") });
            lblSaldoSum.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nsaldo", "{0:N2}") });
            lblMontoMoraPagadoF.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_mora_pago", "{0:N2}") });


            var lstFomaFinanciamiento = Obetenerlstpagos(Obe.cntc_icod_contrato, Obe);
            //Montos Footer
            decimal TotalMontoDeCuota = Math.Round(lista.Sum(x => x.cntc_nmonto_cuota), 2);
            decimal MontoTotalVencido = Math.Round(lista.Where(x => x.cntc_sfecha_cuota < DateTime.Now.Date && x.cntc_icod_situacion == 338).Sum(x => x.cntc_nmonto_cuota), 2);
            decimal Penalidad = Math.Round(Math.Round(lista.Sum(x => x.cntc_nmonto_mora), 2) - Math.Round(lista.Sum(x => x.cntc_nmonto_mora_pago), 2), 2);
            decimal CuotasVencidas = lista.Where(x => x.cntc_sfecha_cuota < DateTime.Now.Date && x.cntc_icod_situacion == 338).Count(x => x.cntc_nmonto_cuota > 0);
            decimal TotalConPenalidad = Math.Round(MontoTotalVencido + Penalidad, 2);
            decimal FomaResta = Convert.ToDecimal(Obe.cntc_nmonto_total_foma) - Convert.ToDecimal(Obe.cntc_nmonto_total_foma_pagado);
            decimal Crediticio = Obe.cntc_nfinanciamientro - (lstFomaFinanciamiento.Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento)).FirstOrDefault().pgs_nmonto_pagado;
            decimal TotalDeber = Math.Round(TotalConPenalidad + FomaResta + Crediticio, 2);

            lblTotalMontoDeCuota.Text = TotalMontoDeCuota.ToString("N2");
            lblMontoTotalVencido.Text = MontoTotalVencido.ToString("N2");
            lblPenalidadF.Text = Penalidad.ToString("N2");
            lblTotalConPenalidad.Text = TotalConPenalidad.ToString("N2");
            lblFoma.Text = FomaResta.ToString("N2");
            lblCrediticio.Text = Crediticio.ToString("N2");
            lblTotalDeber.Text = TotalDeber.ToString("N2");

            subReportefomas rpt = new subReportefomas();
            rpt.cargar(Obe.cntc_icod_contrato);
            subRptFomas.ReportSource = rpt;

            this.ShowPreview();
        }
        public List<EPagoFomaFinanciamiento> Obetenerlstpagos(int cntc_icod_contrato, EContrato Obe)
        {
            List<EPagoFomaFinanciamiento> lstpagos = new List<EPagoFomaFinanciamiento>();

            lstpagos = new BVentas().listarFomaFinanciamiento(cntc_icod_contrato, Obe);
            if (lstpagos.Count == 0)
            {
                //CREAMOS LA LISTA 

                var listTipoPagos = new BGeneral().listarTablaVentaDet(26);

                foreach (var item in listTipoPagos)
                {
                    EPagoFomaFinanciamiento obj = new EPagoFomaFinanciamiento();
                    obj.pgs_icod_contrato = Obe.cntc_icod_contrato;
                    obj.pgs_itipo = item.tabvd_iid_tabla_venta_det;
                    obj.pgs_sfecha_pago = (DateTime?)null;
                    obj.pgs_nmonto_pagado = 0;
                    obj.intusuario = Valores.intUsuario;
                    obj.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                    obj.pgs_icod_pagos = new BVentas().FomaFinanciamientoInsertar(obj);
                }
                //VOLVEMOS A CARGAR
                lstpagos = Obetenerlstpagos(cntc_icod_contrato, Obe);
            }
            return lstpagos;

        }
    }
}
