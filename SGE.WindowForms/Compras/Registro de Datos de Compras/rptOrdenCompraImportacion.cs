using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Compras.Registro_de_Datos_de_Compras
{
    public partial class rptOrdenCompraImportacion : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrdenCompraImportacion()
        {
            InitializeComponent();
        }
        public void cargar(EOrdenCompraImportacion _BE, List<EOrdenCompraImportacion> Lista, List<ERegistroFirmas> lstFirmas)
        {
            string strTipoMoneda = "";
            string SimbTipoMoneda = "";
            string strTipoPago = "";
            if (_BE.tablc_iid_tipo_moneda == 3)
            {
                 strTipoMoneda = "SOLES ";
                 SimbTipoMoneda = "S/";
            }
            else {
                strTipoMoneda = "DOLARES ";
                SimbTipoMoneda = "$ ";
            }
            //-----------------------------////
                //if (_BE.tablc_iforma_pago==116)
                //{
                //strTipoPago = "CONTADO";
                //}else if (_BE.tablc_iforma_pago==117)
                //{
                //strTipoPago = "CREDITO";
                //}else if (_BE.tablc_iforma_pago==118)
                //{
                //strTipoPago = "LETRA";
                //}else{
                //strTipoPago = "OTROS";
                //}

            lblOCI.Text = _BE.ocic_vnumero_oci.Insert(4, "-");
           
            lblProveedor.Text = _BE.proc_vnombrecompleto;

            lblDireccion.Text = _BE.proc_vdireccion;

            lblContacto.Text = _BE.ocic_vcontacto;

            lblFecha.Text = String.Format("{0:dd/MM/yyyy}", _BE.ocic_sfecha_oci);

            lblCotizacion.Text = _BE.ocic_vcotizacion;

            lblReferencia.Text = _BE.ocic_vreferencia;

            lblSolicitante.Text = _BE.ocic_vsolicitante;

            lblfIncoterm.Text = _BE.ocic_vincoterm;

            lblfFormaPago.Text =_BE.ocic_vforma_pago;
            lblfLugarEntrega.Text = _BE.ocic_vlugar_entrega;
            lblfFechEntrega.Text = String.Format("{0:dd/MM/yyyy}", _BE.ocic_sfecha_entrega);
            //------------------------------------//
            //lblfGarantia.Text = _BE.ocid_vdescripcion;
            //lblfNota.Text = _BE.ococ_vnota_ocl;
            
            

            this.DataSource = Lista;

            //GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("cpn_icod_concepto_nacional") });

            //lblrubros.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "cpn_vdescripcion_concepto_nacional", "")});
            //Enlaces con el Detalle

            lblmItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocid_iitem", "")});

            lblmCantidad.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocid_ncantidad", "")});

            lblmUnit.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strAbrevUniMed", "")});

            lblmDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocid_vdescripcion", "")});

            lblmCaracteristicas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocid_vcaracteristicas", "")});
            

            lblmPrecioUnit.DataBindings.AddRange(new XRBinding[] {
            new XRBinding( "Text", Lista, "ocid_ncunitario", "{0:n4}")});
                      
            lblmTipoMoneda3.Text = SimbTipoMoneda ;
            lblmTipoMoneda4.Text = SimbTipoMoneda;

            lblmDescto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocid_ndescuento_item", "{0:n2}")});

            lblTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocid_nmonto_total", "{0:n2}")});

      
            //-------------------------------------------------------------------//



            LblMTotalTipoMoneda.Text = strTipoMoneda;
            //lblIGV.Text = String.Format("{0:n2}", _BE.ococ_nmonto_imp);
            lblmTotal.Text =  String.Format("{0:n2}", _BE.ocic_nmonto_total);
            //lblmSon.Text = total + " " + strTipoMoneda;

            lblAutorizado1.Text = lstFirmas[0].regf_oci_aprobado1;
            lblAutorizado2.Text = lstFirmas[0].regf_oci_aprobado2;
            lblAutorizado3.Text = lstFirmas[0].regf_oci_aprobado3;
            lblAutorizado4.Text = lstFirmas[0].regf_oci_aprobado4;

            this.ShowPreview();
        }
    }
}
