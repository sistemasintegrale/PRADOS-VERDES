using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NF
{
    public partial class rptAutorizacioEspacioPersonalNF : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAutorizacioEspacioPersonalNF()
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
            lblTipoContrato.Text = data.TipoContrato;
            lkpTipoDeceso.Text = data.TipoDeceso;
            this.ShowPreview();
        }

    }
}
