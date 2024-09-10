using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;
using System.Linq;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Compras.Cuentas_Corrientes
{
    public partial class rptSaldoInicialDocPagar : DevExpress.XtraReports.UI.XtraReport
    {
        public rptSaldoInicialDocPagar()
        {
            InitializeComponent();
        }
        public void cargar(List<EDocPorPagar> lista, string strTitulo) 
        {
            lblTitulo1.Text = strTitulo;
            lblTitulo2.Text = "";
            lblEmpresa.Text = Valores.strNombreEmpresa + "- A�O " + Parametros.intEjercicio;

            this.DataSource = lista;

            //Enlace al grupo Cliente
            //{0:dd/MM/yyyy}
            xrlCorrelativo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_viid_correlativo", "") });
            xrlDocumento.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "tdocc_vabreviatura_tipo_doc", "") });
            xrlClase.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "clase_viid_correlativo", "") });
            xrlNroDocumento.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_vnumero_doc", "") });
            xrlSituacion.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "vSituacion", "") });
            xrlFecha.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_sfecha_doc", "{0:dd/MM/yyyy}") });
            xrlProveedor.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "proc_vnombrecompleto", "") });
            xrlTc.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_tipo_cambio", "") });
            //xrlValorCif.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_referencial_cif", "{0:n}") });
            xrlMDNoGra.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_destino_gravado", "") });
            //xrlMServicio.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_servicio_no_domic", "{0:n}") });
            xrlMon.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "vMoneda", "") });
            xrlMDNoGra.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_destino_nogravado", "{0:n}") });
            xrlMDestMixto.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_destino_mixto", "") });
            xrlValorCompra.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_total_documento", "{0:n}") });
            xrlImpDestGrav.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_imp_destino_gravado", "{0:n}") });
            //xrlMontoISC.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_isc", "{0:n}") });
            xrlMTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_total_documento", "{0:n}") });
            xrlMPagado.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}") });
            //xrlMa.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_total_saldo", "{0:n}") });
            xrlMTotalTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_total_documento", "{0:n}") });
            xrlPagadoTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "doxpc_nmonto_total_pagado", "{0:n}") });
            this.ShowPreview();
        }
    }
}
