using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class subRptRepProdCosto : DevExpress.XtraReports.UI.XtraReport
    {
        public subRptRepProdCosto()
        {
            InitializeComponent();
        }

        public void Cargar(List<ECostoReporteProduccion> ListaCosto)
        {
            this.DataSource = ListaCosto;
            GroupHeader1.GroupFields.Add(new GroupField("TipoCosto"));

            #region Cabecera Grupo
            lblTipoCosto.DataBindings.Add(new XRBinding("Text", ListaCosto, "TipoCosto", ""));
            #endregion

            #region Detalle
            lblProveedor.DataBindings.Add(new XRBinding("Text", ListaCosto, "proc_vnombrecompleto", ""));
            lblDocumento.DataBindings.Add(new XRBinding("Text", ListaCosto, "doxpc_vnumero_doc", ""));
            lblFecha.DataBindings.Add(new XRBinding("Text", ListaCosto, "doxpc_sfecha_doc", ""));
            lblMoneda.DataBindings.Add(new XRBinding("Text", ListaCosto, "Moneda", ""));
            lblMonto.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago", "{0:n2}"));
            lblTC.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_tipo_cambio", "{0:n4}"));
            lblMontoS.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago_soles", "{0:n2}"));
            lblMontoD.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago_dolares", "{0:n2}"));
            #endregion

            #region Pie Grupo
            lblMontoST.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago_soles", "{0:n2}"));
            lblMontoDT.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago_dolares", "{0:n2}"));
            #endregion

            #region Pie Reporte
            lblMontoSTT.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago_soles", "{0:n2}"));
            lblMontoDTT.DataBindings.Add(new XRBinding("Text", ListaCosto, "crp_nmonto_pago_dolares", "{0:n2}"));
            #endregion
        }

    }
}
