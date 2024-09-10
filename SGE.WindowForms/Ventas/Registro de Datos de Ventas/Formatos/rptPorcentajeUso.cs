using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos
{
    public partial class rptinhumacionICENI : DevExpress.XtraReports.UI.XtraReport
    {
        public rptinhumacionICENI()
        {
            InitializeComponent();
        }

        public void Cargar(EContrato obj)
        {
            lblFecha.Text = $"{obj.cntc_sfecha_contrato.Value.Day} de {DateTimeFormatInfo.CurrentInfo.GetMonthName(obj.cntc_sfecha_contrato.Value.Month)} del {obj.cntc_sfecha_contrato.Value.Year}";
            lblSerie.Text = obj.cntc_vnumero_contrato.Substring(0, 4);
            lblContrato.Text = obj.cntc_vnumero_contrato.Substring(4);
            lblContratante.Text = obj.strNombreCompleto;
            lblDNI.Text = obj.cntc_vdocumento_contratante;
            lblNumContrato.Text = obj.cntc_vnumero_contrato.Insert(4, "-");

            lblTipoSepultura.Text = obj.strtiposepultura;



            this.ShowPreview();
        }

    }
}
