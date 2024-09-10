﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;


namespace SGE.WindowForms.Sistema
{
    public partial class frm06Parametro : DevExpress.XtraEditors.XtraForm
    {
        List<EParametro> lstParametro = new List<EParametro>();
        public frm06Parametro()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }       
       
        private void cargar()
        {
            lstParametro = new BAdministracionSistema().listarParametro();
            grdTipoDocumento.DataSource = lstParametro;
            viewTipoDocumento.Focus();
            /**/
            Parametros.strPorcIGV = Convert.ToDecimal(lstParametro[0].pm_nigv_parametro).ToString();
            Parametros.strPorcRenta4taCat = Convert.ToDecimal(lstParametro[0].pm_ncategoria_parametro).ToString();
            /**/
            SGE.BusinessLogic.Parametros.strPorcIGV = Convert.ToDecimal(lstParametro[0].pm_nigv_parametro).ToString();
            SGE.BusinessLogic.Parametros.strPorcRenta4taCat = Convert.ToDecimal(lstParametro[0].pm_ncategoria_parametro).ToString();
        }
        void reload()
        {
            cargar();           
        }        
        private void modificar()
        {
            EParametro Obe = (EParametro)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteParametro frm = new frmManteParametro();
            frm.MiEvento += new frmManteParametro.DelegadoMensaje(reload);
            frm.Show();
            frm.oBe = Obe;
            frm.txtIGV.Text = Obe.pm_nigv_parametro.ToString();
            frm.txt4taCategoria.Text = Obe.pm_ncategoria_parametro.ToString();
            frm.txtUIT.Text = Obe.pm_nuit_parametro.ToString();
            frm.txtEmpresa.Text = Obe.pm_nombre_empresa;
            frm.txtDireccion.Text = Obe.pm_direccion_empresa;
            frm.txtCorrelativoOR.Text = Obe.pm_correlativo_OR.ToString();
            frm.txtRuc.Text = Obe.pm_vruc;
            frm.txtTipoCambio.Text = Obe.pm_ntipo_cambio.ToString();
            frm.txtServicioFE.Text = Obe.urlServicioFacturaElectronica;
            frm.txtServicioNC.Text = Obe.urlServicioNotaCredito;
            frm.txtServicioND.Text = Obe.urlServicioNotaDebito;
            frm.txtRucFE.Text = Obe.Ruc;
            frm.txtUsuarioSol.Text = Obe.UsuarioSol;
            frm.txtClaveSol.Text = Obe.ClaveSol;
            frm.txtURLPrueba.Text = Obe.EndPointUrlPrueba;
            frm.txtURLDesarrollo.Text = Obe.EndPointUrlDesarrollo;
            frm.txtPwCertificado.Text = Obe.PasswordCertificado;
            frm.txtCertificadoDigital.Text = Obe.CertificadoDigital;
            frm.txtServicioEnviarDoc.Text = Obe.urlServicioEnviarDocumento;
            frm.txtServicioFirma.Text = Obe.urlServicioFirma;
            frm.txtValidacion.Text = Obe.IdServiceValidacion;
            frm.dtmFechaInicio.EditValue = Obe.pm_sfecha_inicio;
            frm.txtDireccionXML.Text = Obe.DirecciónXML;
            frm.txtServicioER.Text = Obe.urlServicioEnvioResumen;
            frm.txtServicioGR.Text = Obe.urlServicoGenerarResumen;
            frm.txtValidacionResumen.Text = Obe.IdServiceValidacionResumen;
            frm.txtDireccionResumen.Text = Obe.pm_vruta_resumen;
            frm.txtServicioCT.Text = Obe.ServiceConsultaTiket;

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }                
    }
}