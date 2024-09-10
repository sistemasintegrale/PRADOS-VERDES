using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;

namespace SGE.WindowForms.Reportes.Contabilidad.Consultas
{
    public partial class rptIntentarioResultado : DevExpress.XtraReports.UI.XtraReport
    {
        public rptIntentarioResultado()
        {
            InitializeComponent();
        }
        public void cargar(List<EVoucherContableDet> Lista, string strEjercicio, string strMes,string strDigitos,string strMoneda, string codigo)
        {
            xrlblTitulo.Text = (codigo == "<RFRM13>") ? String.Format("BALANCE DE COMPROBACI�N EN {0} - AL DIA {1}", strMoneda, strMes.ToUpper()) :
                String.Format("INVENTARIO Y RESULTADOS  EN {0} - AL MES DE {1} DE {2}", strMoneda, strMes.ToUpper(), strEjercicio);
            xrlblTitulo2.Text = String.Format("RESUMEN A {0} D�GITOS", strDigitos);
            xrlblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - A�O " + strEjercicio;
            lblRptCodigo.Text = codigo;

            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("cta_padre") });
            #region Header
            lblCtaPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cta_padre", "")});
            lblDescPadre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cta_vdespadre", "")});
            
            lblDebeGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblDeudorGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Deudor", "{0:N2}")});
            lblAcreedorGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Acreedor", "{0:N2}")});
            lblActivoGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Activo", "{0:N2}")});
            lblPasivoGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Pasivo", "{0:N2}")});
            lblNPerdidaGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "NPerdida", "{0:N2}")});
            lblNGananciaGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "NGanancia", "{0:N2}")});
            lblFPerdidaGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "FPerdida", "{0:N2}")});
            lblFGananciaGrupo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "FGanancia", "{0:N2}")}); 

            #endregion
            #region Detail            
            lblCta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strNroCuenta", "")});
            lblDescCta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strDesCuenta", "")});

            lblDebe.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaber.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblDeudor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Deudor", "{0:N2}")});
            lblAcreedor.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Acreedor", "{0:N2}")});

            lblActivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Activo", "{0:N2}")});
            lblPasivo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Pasivo", "{0:N2}")});
            lblNPerdida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "NPerdida", "{0:N2}")});
            lblNGanancia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "NGanancia", "{0:N2}")});
            lblFPerdida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "FPerdida", "{0:N2}")});
            lblFGanancia.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "FGanancia", "{0:N2}")}); 
            #endregion
            #region Footer
            lblDebeTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_debe_sol", "{0:N2}")});
            lblHaberTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "vcocd_nmto_tot_haber_sol", "{0:N2}")});
            lblDeudorTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Deudor", "{0:N2}")});
            lblAcreedorTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Acreedor", "{0:N2}")});
            lblActivoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Activo", "{0:N2}")});
            lblPasivoTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Pasivo", "{0:N2}")});
            lblNPerdidaTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "NPerdida", "{0:N2}")});
            lblNGananciaTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "NGanancia", "{0:N2}")});
            lblFPerdidaTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "FPerdida", "{0:N2}")});
            lblFGananciaTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "FGanancia", "{0:N2}")}); 
            #endregion


            this.ShowPreview();
        }

        private void lblActivoTotal_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblTGdebe.Text = Convert.ToDecimal(lblDebeTotal.Summary.GetResult()).ToString("N2");
            lblTGhaber.Text = Convert.ToDecimal(lblHaberTotal.Summary.GetResult()).ToString("N2");
            lblTGdeudor.Text = Convert.ToDecimal(lblDeudorTotal.Summary.GetResult()).ToString("N2");
            lblTGacreedor.Text = Convert.ToDecimal(lblAcreedorTotal.Summary.GetResult()).ToString("N2");
            #region Activo y Pasivo
            decimal subActivo, subPasivo, ResulActivo, ResulPasivo = 0;
            subActivo = Convert.ToDecimal(lblActivoTotal.Summary.GetResult());
            subPasivo = Convert.ToDecimal(lblPasivoTotal.Summary.GetResult());

            if (subActivo > subPasivo)
            {
                lblPasivoResultado.Text = Convert.ToDecimal(subActivo - subPasivo).ToString("N2");
                ResulPasivo = subPasivo + Convert.ToDecimal(lblPasivoResultado.Text);
                //--
                lblTGactivo.Text = subActivo.ToString("N2");
                lblTGpasivo.Text = ResulPasivo.ToString("N2");
            }
            else
            {
                lblActivoResultado.Text = Convert.ToDecimal(subPasivo - subActivo).ToString("N2");
                ResulActivo = subActivo + Convert.ToDecimal(lblActivoResultado.Text);
                //--
                lblTGpasivo.Text = subActivo.ToString("N2");
                lblTGactivo.Text = ResulActivo.ToString("N2");
            }
            #endregion

            #region Naturaleza Pedida y Ganancia
            decimal subNPerdida, subNGanancia, ResulNPerdida, ResulNGanancia = 0;
            subNPerdida = Convert.ToDecimal(lblNPerdidaTotal.Summary.GetResult());
            subNGanancia = Convert.ToDecimal(lblNGananciaTotal.Summary.GetResult());

            if (subNPerdida > subNGanancia)
            {
                lblNGananciaResultado.Text = Convert.ToDecimal(subNPerdida - subNGanancia).ToString("N2");
                ResulNGanancia = subNGanancia + Convert.ToDecimal(lblNGananciaResultado.Text);
                //--
                lblTGnperdida.Text = subNPerdida.ToString("N2");
                lblTGnganancia.Text = ResulNGanancia.ToString("N2");

            }
            else
            {
                lblNPerdidaResultado.Text = Convert.ToDecimal(subNGanancia - subNPerdida).ToString("N2");
                ResulNPerdida = subNPerdida + Convert.ToDecimal(lblNPerdidaResultado.Text);
                //--
                lblTGnganancia.Text = subNGanancia.ToString("N2");
                lblTGnperdida.Text = ResulNPerdida.ToString("N2");
            }
            #endregion

            #region Funci�n Pedida y Ganancia
            decimal subFPerdida, subFGanancia, ResulFPerdida, ResulFGanancia = 0;
            subFPerdida = Convert.ToDecimal(lblFPerdidaTotal.Summary.GetResult());
            subFGanancia = Convert.ToDecimal(lblFGananciaTotal.Summary.GetResult());

            if (subFPerdida > subFGanancia)
            {
                lblFGananciaResultado.Text = Convert.ToDecimal(subFPerdida - subFGanancia).ToString("N2");
                ResulFGanancia = subFGanancia + Convert.ToDecimal(lblFGananciaResultado.Text);
                //--
                lblTGfperdida.Text = subFPerdida.ToString("N2");
                lblTGfganancia.Text = ResulFGanancia.ToString("N2");


            }
            else
            {
                lblFPerdidaResultado.Text = Convert.ToDecimal(subFGanancia - subFPerdida).ToString("N2");
                ResulFPerdida = subFPerdida + Convert.ToDecimal(lblFPerdidaResultado.Text);
                //--
                lblTGfganancia.Text = subFGanancia.ToString("N2");
                lblTGfperdida.Text = ResulFPerdida.ToString("N2");
            }
            #endregion
        }
    }
}
