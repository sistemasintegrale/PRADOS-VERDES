using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos
{
    public partial class rptAutorizacioConfeccionLapida : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAutorizacioConfeccionLapida()
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
            lblNumCelular.Text = obj.cntc_vtelefono_contratante;
           
            lblManzana.Text = obj.strmanzana;
            lblSepultura.Text = obj.strsepultura;
            lblNivel.Text = obj.espad_vnivel;

            lblTipoEspacio.Text = obj.espac_icod_vespacios;
 
            this.ShowPreview();
        }

    }
}
