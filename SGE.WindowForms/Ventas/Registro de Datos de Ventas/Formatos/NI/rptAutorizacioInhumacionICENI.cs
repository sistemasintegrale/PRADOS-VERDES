using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NI
{
    public partial class rptAutorizacioEspacioICE : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAutorizacioEspacioICE()
        {
            InitializeComponent();
        }

        public void Cargar(EContrato obj, EContratoFallecido objFall)
        {
            lblFecha.Text = $"{obj.cntc_sfecha_contrato.Value.Day} de {DateTimeFormatInfo.CurrentInfo.GetMonthName(obj.cntc_sfecha_contrato.Value.Month)} del {obj.cntc_sfecha_contrato.Value.Year}";
            lblSerie.Text = obj.cntc_vnumero_contrato.Substring(0, 4);
            lblContrato.Text = obj.cntc_vnumero_contrato.Substring(4);
            lblContratante.Text = obj.strNombreCompleto;
            lblDNI.Text = obj.cntc_vdocumento_contratante;
            lblNumContrato.Text = obj.cntc_vnumero_contrato.Insert(4,"-");
            lblFallecido.Text = objFall.NombreCompleto;
            lblTipoSepultura.Text = obj.strtiposepultura; 
            lblPlataforma.Text = obj.strplataforma;
            lblManzana.Text = obj.strmanzana;
            lblSepultura.Text = obj.strsepultura;
            lblNivel.Text = objFall.espad_vnivel;
            var data = new BVentas().ContratoImpresion(obj.cntc_icod_contrato);
            lblRelacion.Text = data.ParentescoContratatante;
            lblNombre.Text = objFall.NombreCompleto;
            lblApellidos.Text = objFall.cntc_vapellido_paterno_fallecido + " " + objFall.cntc_vapellido_materno_fallecido;
            lblFechaNacimiento.Text = Services.GetFullDateNumber(objFall.cntc_sfecha_nac_fallecido);
            lblFechaFallecimiento.Text = Services.GetFullDateNumber(objFall.cntc_sfecha_fallecimiento);
            lblTipoContrato.Text = data.TipoContrato;
            lblTipoEspacio.Text = data.TipoSepultura;
            this.ShowPreview();
        }

    }
}
