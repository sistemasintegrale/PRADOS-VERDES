using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Globalization;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NI
{
    public partial class rptAutorizacioEspacioAperturaFamiliar : XtraReport
    {
        public rptAutorizacioEspacioAperturaFamiliar()
        {
            InitializeComponent();
        }

        public void Cargar(ContratoImpresion obj, EContratoFallecido objFall)
        {
            lblFecha.Text = $"{obj.FechaContrato.Value.Day} de {DateTimeFormatInfo.CurrentInfo.GetMonthName(obj.FechaContrato.Value.Month)} del {obj.FechaContrato.Value.Year}";
            lblSerie.Text = obj.NumeroContrato.Substring(0, 4);
            lblContrato.Text = obj.NumeroContrato.Substring(4);
            lblContratante.Text = $"{obj.NombreContratante} {obj.ApellidoPaternoContratante} {obj.ApellidoMaternoContratante}";
            lblDNI.Text = obj.NumeroDocumentoContratante;
            lblNumContrato.Text = obj.NumeroContrato.Insert(4,"-");
            lblFallecido.Text = objFall.NombreCompleto;
            lblTipoSepultura.Text = obj.TipoSepultura; 
            lblPlataforma.Text = obj.Plataforma;
            lblManzana.Text = obj.Manzana;
            lblSepultura.Text = obj.NumeroSepultura;
            lblNivel.Text = objFall.espad_vnivel;
            lblRelacion.Text = obj.ParentescoContratatante;
            lblNombre.Text = obj.NombreFallecido;
            lblApellidos.Text = objFall.cntc_vapellido_paterno_fallecido + " " + objFall.cntc_vapellido_materno_fallecido;
            lblFechaNacimiento.Text = Services.GetFullDateNumber(objFall.cntc_sfecha_nac_fallecido);
            lblFechaFallecimiento.Text = Services.GetFullDateNumber(objFall.cntc_sfecha_fallecimiento);
            lblTipoContrato.Text = obj.TipoContrato;
            lblTipoEspacio.Text = obj.TipoSepultura;
            this.ShowPreview();
        }

    }
}
