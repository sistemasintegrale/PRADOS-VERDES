using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using QRCoder;
using System.Linq;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptCuotasPorContrato : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCuotasPorContrato()
        {
            InitializeComponent();
        }
        public void cargar(EContrato Obe, List<EContratoCuotas> mlisdet)
        {
            // Datos de cliente 

            lblNroContrato.Text = Obe.cntc_vnumero_contrato.ToUpper();
            lblSituacionContrato.Text = Obe.cntc_inro_cuotas.ToString();
            lblNombreContratante.Text = Obe.cntc_vnombre_contratante;
            lblSituacionContrato.Text = Obe.strSituacion;
            lblNumDoc.Text = Obe.cntc_vdocumento_contratante;
            //Datos de emisor


            lblNombreEmisor.Text = "INVERSIONES Y DESARROLLO PRADOS VERDES S.A.C ";
            lblDireccion.Text = "Dirección: Av. Las Palmas N° 2020";
            lblDireccion1.Text = "Lima - Lima - Pachacamac";
            lblDireccion2.Text = "Telf. 675 4239 | 932 823 115 | 932 822 164";
            lblDireccion3.Text = "E-mail. info@parquesdelparaiso.com";

            //Datos de documento

            lblFechaCuota.Text = Convert.ToDateTime(Obe.cntc_sfecha_cuota).ToString("dd/MM/yyyy");
            //Totalizacion de la boleta


            //Detalle de la boleta

            this.DataSource = mlisdet; 

            lblTipo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strTipo", "")});

            lbltp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "tipo", "")});

            lblFecha.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cntc_sfecha_cuota", "{0:dd/MM/yyyy}")});


            lblNumero.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cntc_inro_cuotas", "")});

            lblMonto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cntc_nmonto_cuota", "{0:n2}")});


            lblSituacion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strSituacion", "")});

            lblDocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "plnd_vnumero_doc", "")});

            lblFechaDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "plnd_sfecha_doc", "{0:dd/MM/yyyy}")});

            lblMora.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "cntc_nmonto_mora", "{0:n2}")});


            lblTotalMonto.Text = mlisdet.Where(x=>x.cntc_icod_situacion != 6437).Sum(x=>x.cntc_nmonto_cuota).ToString("N2");
            lblTotalPenalidad.Text = mlisdet.Sum(x => x.cntc_nmonto_mora).ToString();

            this.ShowPreview();
        }
    }
}
