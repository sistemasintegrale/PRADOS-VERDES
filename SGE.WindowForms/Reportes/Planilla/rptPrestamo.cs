using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;
using System.Globalization;

namespace SGE.WindowForms.Reportes.Planilla
{
    public partial class rptPrestamo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPrestamo()
        {
            InitializeComponent();
        }

        internal void Cargar(EPrestamo obj, List<EPrestamo> lista)
        {
            lblTexto.Text =
                 $"INVERSIONES Y DESARROLLO PRADOS VERDES SAC con RUC 20500662159, " +
                 $"domiciliada en Avenida Las Palmas N° 2020, Distrito de Pachacamac, " +
                 $"provincia y departamento de Lima, otorga un préstamo por el monto de " +
                 $"S/ {obj.prtpc_nmonto_prestamo} ({Convertir.ConvertNumeroEnLetras(obj.prtpc_nmonto_prestamo.ToString())} soles) a favor del " +
                 $"señor {obj.strNombrePersonal}, Identificado con DNI {obj.dniPersonal}, " +
                 $"el mismo que será cancelado en {obj.prtpc_inro_cuotas} meses, por el monto " +
                 $"de S/ {obj.prtpc_nmonto_cuota} ({Convertir.ConvertNumeroEnLetras(obj.prtpc_nmonto_cuota.ToString())} soles) cada una y será descontado " +
                 $"de manera {obj.strTipoPagoC} de su remuneración a partir del {obj.primerVencimiento.Value.Day} " +
                 $"de "+  obj.primerVencimiento.Value.ToString("MMMM", new CultureInfo("es-ES")) +$" {obj.primerVencimiento.Value.Year}.";

            this.DataSource = lista;
            lblItem.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(EPrestamo.prtpd_inro_cuota), "") });
            lblMonto.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(EPrestamo.strMontoCuota), "") });
            lblFechaDePago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(EPrestamo.prtpd_sfecha_cuota), "{0:dd/MM/yyyy}") });
            lblSolicitante.Text = obj.strNombrePersonal;
            this.ShowPreview();
        }
    }
}
