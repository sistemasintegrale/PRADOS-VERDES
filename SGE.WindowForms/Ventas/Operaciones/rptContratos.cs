using SGE.Entity;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class rptContratos : DevExpress.XtraReports.UI.XtraReport
    {
        public rptContratos()
        {
            InitializeComponent();
        }
        public void cargar(ContratoImpresion Obe)
        {

            lblSerie.Text = Obe.NumeroContrato.Insert(4, "-");
            lblIEC.Text = Obe.strNombreIEC;
            lblOrigenVenta.Text = Obe.OrigenVenta;
            ckICO.Checked = false;
            ckMA.Checked = false;
            ckNIN.Checked = false;
            lblNombreContratante.Text = Obe.NombreContratante;
            lblApellidoPaternoContratante.Text = Obe.ApellidoPaternoContratante;
            lblApellidoMaternoContratante.Text = Obe.ApellidoMaternoContratante;
            lblNumeroDocumentoContratante.Text = Obe.NumeroDocumentoContratante;
            lblFechaNacimientoContratante.Text = Services.GetFullDateNumber(Obe.FechaNacimientoContratante);
            lblEstadoCivilContratante.Text = Obe.EstadoCivilContratante;
            lblParentescoContratante.Text = Obe.ParentescoContratatante;
            lblNacionalidadContratante.Text = Obe.NacionalidadContratante;
            lblTelefonoContrante.Text = Obe.TelefonoContratante;
            lblDireccionContratante.Text = Obe.DireccionContratante;

            lblNombreContratante2.Text = Obe.NombreContratante2;
            lblApellidoPaternoContratante2.Text = Obe.ApellidoPaternoContratante2;
            lblApellidoMaternoContratante2.Text = Obe.ApellidoMaternoContratante2;
            lblNumeroDocumentoContratante2.Text = Obe.NumeroDocumentoContratante2;
            lblFechaNacimientoContratante2.Text = Services.GetFullDateNumber(Obe.FechaNacimientoContratante2);
            lblEstadoCivilContratante2.Text = Obe.EstadoCivilContratante2;
            lblParentescoContratante2.Text = Obe.ParentescoContratatante2;
            lblNacionalidadContratante2.Text = Obe.NacionalidadContratante2;
            lblTelefonoContrante2.Text = Obe.TelefonoContratante2;
            lblDireccionContratante2.Text = Obe.DireccionContratante2;

            lblNombreBeneficiario.Text = Obe.NombreBeneficiario;
            lblApellidoPaternoBeneficiario.Text = Obe.ApellidoPaternoBeneficiario;
            lblApellidoMaternoBeneficiario.Text = Obe.ApellidoMaternoBeneficiario;
            lblNumeroDocumentoBeneficiario.Text = Obe.DocumentoBeneficiario;
            lblDireccionBeneficiario.Text = Obe.DireccionBeneficiario;
            lblFechaNacimientoBeneficiario.Text = Services.GetFullDateNumber(Obe.FechaNacimientoBeneficiario);
            lblNombreFallecido.Text = Obe.NombreFallecido;
            lblApellidoPaternoFallecido.Text = Obe.ApellidoPaternoFallecido;
            lblApellidoMaternoFallecido.Text = Obe.ApellidoMaternoFallecido;
            lblNumeroDocumentoFallecido.Text = Obe.DocumentoFallecido;
            lblFechaNacimientoFallecido.Text = Services.GetFullDateNumber(Obe.FechaNacimientoFallecido);
            lblFechaFallecimientoFallecido.Text = Services.GetFullDateNumber(Obe.FechaFallecimientoFallecido);
            lblFechaEntierroFallecido.Text = Services.GetFullDateNumber(Obe.FechaEntierroFallecido);
            lblNacionalidadFallecido.Text = Obe.NacionalidadFallecido;
            lblCodigoPlan.Text = Obe.CodigoPlan;
            lblNombrePlan.Text = Obe.NombrePlan;
            lblTipoSepultura.Text = Obe.TipoSepultura;
            lblCapacidadContrato.Text = Obe.CapacidadContratada;
            lblCapacidadTotal.Text = Obe.CapacidadTotal;
            lblUrnas.Text = Obe.Urnas;
            lblServicoInhumacion.Text = Obe.ServicioInhumacion;
            lblPlataforma.Text = Obe.Plataforma;
            lblManzana.Text = Obe.Manzana;
            lblNumSepultura.Text = Obe.NumeroSepultura;
            lblCodigoSepultura.Text = Obe.CodigoSepultura;
            lblNumeroRecerva.Text = Obe.NumeroReserva;
            lblPrecioLista.Text = Obe.PrecioLista.Value.ToString("n2");
            lblDescuento.Text = Obe.Descuento.Value.ToString("n2");
            lblInhumacion.Text = Obe.Inhumacion.Value.ToString("n2");
            lblPrecioTotal.Text = Obe.PrecioTotal.Value.ToString("n2");
            lblFOMA.Text = Obe.Foma.Value.ToString("n2");
            lblPagoInicial.Text = Obe.PagoInicial.Value.ToString("n2");
            lblSaldo.Text = Obe.Saldo.Value.ToString("n2");
            lblNumeroCuotas.Text = Obe.NumeroCuotas.ToString();
            lblSistemaPago.Text = Obe.SistemaPago;
            lblValorCuota.Text = Obe.MontoCuota.Value.ToString("n2");
            lblPrimerVencimiento.Text = Services.GetFullDateNumber(Obe.FechaCuota);
            lblUltimoVencimiento.Text = Services.GetFullDateNumber(Obe.UltimoVencimiento);
            if (Obe.SistemaPago != null)
            {
                if (Obe.SistemaPago == "CREDITO")
                {
                    lblPeriodicidadPago.Text = "MENSUAL";
                }
            }

            if (Obe.DocumentoBeneficiario != null)
            {
                if (Obe.DocumentoFinanciado.ToUpper() == "LETRA")
                {
                    ckLetra.Checked = true;
                }

                if (Obe.DocumentoFinanciado.ToUpper() == "PAGARE")
                {
                    ckPagare.Checked = true;
                }
            }

            if (Obe.Comprobante != null)
            {
                if (Obe.Comprobante.ToUpper() == "BOLETA")
                {
                    ckBoleta.Checked = true;
                }
                if (Obe.Comprobante.ToUpper() == "FACTURA")
                {
                    ckFact.Checked = true;
                }
            }

            lblDiaContrato.Text = Services.GetDayNumber(Obe.FechaContrato);
            lblMesContrato.Text = Services.GetMonthName(Obe.FechaContrato);
            lblAnioContrato.Text = Services.GetYearNumber(Obe.FechaContrato);
        }
    }
}
