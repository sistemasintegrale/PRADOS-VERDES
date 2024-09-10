using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class subReportefomas : DevExpress.XtraReports.UI.XtraReport
    {
        public subReportefomas()
        { 
            InitializeComponent();
        }
        public void cargar(int contrato) {
            List<ECuotaFoma> lista = new BVentas().CuotaFomaListar(contrato);
            this.DataSource = lista;
            lblMontoPagado.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "ccf_nmonto_pagado", "{0:n2}") });
            lblMontoPagar.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "ccf_nmonto_pagar", "{0:n2}") });
            lblNivel.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "strNivel", "") });
            lblRecibo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "strNumRecibo", "") });
            lblFechaPago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "cff_sfecha_pago", "{0:dd/MM/yyyy}") });
            lblSituacion.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, "strEstado", "") });

        }

    }
}
