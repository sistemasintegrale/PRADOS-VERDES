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
    public partial class rptAdenda : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAdenda()
        {
            InitializeComponent();
        }

        public void Cargar(ContratoImpresion obj)
        {
            lblFecha.Text = $"{obj.FechaContrato.Value.Day} de {DateTimeFormatInfo.CurrentInfo.GetMonthName(obj.FechaContrato.Value.Month)} del {obj.FechaContrato.Value.Year}";
            lblSerie.Text = obj.NumeroContrato.Substring(0, 4);
            lblNumContrato.Text = obj.NumeroContrato.Substring(4);
            lblContrato.Text = obj.NumeroContrato.Insert(4, "-");
            lblNombreContratante.Text = $"{obj.NombreContratante} {obj.ApellidoPaternoContratante} {obj.ApellidoMaternoContratante}";
            lblDni.Text = obj.NumeroDocumentoContratante;
            lblDomicilio.Text = obj.DireccionContratante;
            lblEstadoCivil.Text = obj.EstadoCivilContratante;
            lblTelefono.Text = obj.TelefonoContratante;
            lblFechaNacimiento.Text = Services.GetFullDateNumber(obj.FechaNacimientoContratante);

            lblNombre2.Text = $"{obj.NombreContratante2} {obj.ApellidoPaternoContratante2} {obj.ApellidoMaternoContratante}2";
            lblDNI2.Text = obj.NumeroDocumentoContratante2;
            lblDIreccion2.Text = obj.DireccionContratante2;
            lblEstadiCivil2.Text = obj.EstadoCivilContratante2;
            lblTelefono2.Text = obj.TelefonoContratante2;
            lblFechaNacimiento2.Text = Services.GetFullDateNumber(obj.FechaNacimientoContratante2);
            this.ShowPreview();
        }

    }
}
