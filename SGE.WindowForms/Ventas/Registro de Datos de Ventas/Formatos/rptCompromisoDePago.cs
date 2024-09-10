using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos
{
    public partial class rptCompromisoDePago : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCompromisoDePago()
        {
            InitializeComponent();
        }

        public void Cargar(ContratoImpresion obj)
        {
            lblFecha.Text = $"{obj.FechaContrato.Value.Day} de {DateTimeFormatInfo.CurrentInfo.GetMonthName(obj.FechaContrato.Value.Month)} del {obj.FechaContrato.Value.Year}";
            lblSerie.Text = obj.NumeroContrato.Substring(0, 4);
            lblContrato.Text = obj.NumeroContrato.Substring(4);
            lblContratante.Text = $"{obj.NombreContratante} {obj.ApellidoPaternoContratante} {obj.ApellidoMaternoContratante}";
            lblDNI.Text = obj.NumeroDocumentoContratante;


            lblDomicilio.Text = obj.DireccionContratante;
            lblTelefono.Text = obj.TelefonoContratante;
            lblCorreo.Text = obj.CorreoContratante;
            lblPrecioTotal.Text = obj.PrecioTotal.Value.ToString("n2");
            lblPagoInicial.Text = obj.PagoInicial.Value.ToString("n2");
            lblSaldo.Text = obj.Saldo.HasValue ? obj.Saldo.Value.ToString("n2") : "00.00";
            lblCuotas.Text = $"último monto en {obj.NumeroCuotas.Value.ToString("d2")} cuotas, detalladas en el siguiente cronograma de pagos.";
            lblTipoSepultura.Text = obj.TipoSepultura;
            lblNombreTitular2.Text = $"{obj.NombreContratante2} {obj.ApellidoPaternoContratante2} {obj.ApellidoMaternoContratante2}";
            lblDni2.Text = obj.NumeroDocumentoContratante2;
            lblTelefono2.Text = obj.TelefonoContratante2;
            lblSaldo.Text = obj.Saldo.ToString();
           
        }

    }
}
