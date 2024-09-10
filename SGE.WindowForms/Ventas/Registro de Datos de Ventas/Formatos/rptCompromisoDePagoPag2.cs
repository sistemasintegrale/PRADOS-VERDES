using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos
{
    public partial class rptCompromisoDePagoPag2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCompromisoDePagoPag2()
        {
            InitializeComponent();
        }

        public void Cargar(EContrato Obe, List<EContratoCuotas> lista)
        {
            lista = lista.Where(x => x.cntc_icod_situacion != 6437).ToList();//SE FILTRAN LOS REPROGRAMADOS


            lista = lista.OrderBy(x => x.cntc_itipo_cuota).ThenBy(x => x.cntc_inro_cuotas).Where(x => x.cntc_inro_cuotas > 0).ToList();
            this.DataSource = lista;
            lblFOMA.Text = $"FOMA S/. {Obe.cntc_naporte_fondo} (POR NIVEL-VARIABLE)";
            lblNumeroCuota.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_inro_cuotas", "") });
            lblFechaDePagos.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_sfecha_cuota", "{0:dd/MM/yyyy}") });
            lblMontoPago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cntc_nmonto_cuota", "{0:N2}") });










        }

    }
}
