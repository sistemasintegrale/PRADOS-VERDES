using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos
{
    public partial class rptProgramacionEntierro : DevExpress.XtraReports.UI.XtraReport
    {
        public rptProgramacionEntierro()
        {
            InitializeComponent();
        }

        public void Cargar(ContratoImpresion obj, EContratoFallecido objFall)
        {
            lblFecha.Text = $"{obj.FechaContrato.Value.Day} de {DateTimeFormatInfo.CurrentInfo.GetMonthName(obj.FechaContrato.Value.Month)} del {obj.FechaContrato.Value.Year}";
            lblSerie.Text = obj.NumeroContrato.Substring(0, 4);
            lblContrato.Text = obj.NumeroContrato.Substring(4);

            lblFallecido.Text = objFall.NombreCompleto;
            lblFechaInhumacion.Text = objFall.cntc_sfecha_entierro.Value.ToString("dd/MM/yyyy"); 
            lblHorarioProgramado.Text = objFall.cntc_sfecha_entierro.Value.ToString("dd/MM/yyyy");
            lblTipoEspacio.Text = objFall.espac_icod_vespacios;
            lblReligion.Text = objFall.strReligiones;
            lblTipoDeceso.Text = objFall.strTipoDeceso;
            lblTipoEspacio.Text = obj.TipoSepultura;
            lblFirmado.Text = $"Firmado en Lima, a los {Services.GetDayNumber(DateTime.Now)} días del mes de {Services.GetMonthName(DateTime.Now)} del {Services.GetYearNumber(DateTime.Now)}.";
            this.ShowPreview();
        }

    }
}
