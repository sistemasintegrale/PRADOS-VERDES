using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Ventas.Factura_Electronica;
using SGE.WindowForms.Ventas.Reporte;
using SGI.WindowsForm.Otros.Ventas;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmMantePlanillaCab : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public EPlanillaCobranzaCab ObePlnCab = new EPlanillaCobranzaCab();

        List<EPlanillaCobranzaDet> lstPlanillas = new List<EPlanillaCobranzaDet>();
        private List<string> mensajeRespuesta;
        //public int IcodVendedor = 0;
        bool flag_close = false;
        public frmMantePlanillaCab()
        {
            InitializeComponent();
        }

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }

        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    dteFecha.Enabled = false;
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;

        }

        private void frm07VentaPorDia_Load(object sender, EventArgs e)
        {
            setFecha();

            cargar();
            viewPlanilla.Focus();
        }

        private void cargar()
        {
            lstPlanillas = new BVentas().listarPlanillaCobranzaDetalle(ObePlnCab.plnc_icod_planilla);
            grdPlanilla.DataSource = lstPlanillas;
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(43), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            setTotales();
            if (lstPlanillas.Count == 0)
                dteFecha.Enabled = true;
            else
                dteFecha.Enabled = false;
        }
        void reload(int intIcod, EPlanillaCobranzaCab oBePlnCabReload)
        {
            ObePlnCab = oBePlnCabReload;
            if (ObePlnCab.plnc_icod_planilla > 0)
                SetModify();
            cargar();

            int index = lstPlanillas.FindIndex(x => x.plnd_icod_detalle == intIcod);
            viewPlanilla.FocusedRowHandle = index;
            viewPlanilla.Focus();

            setTotales();

            modificarPlanillaCab();
        }

        private void setTotales()
        {
            /**FACTURACION**/
            txtImporte.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnFacturacion && x.intSituacionFavBov != 4).ToList().Sum(x => x.plnd_nmonto).ToString();
            txtMonto.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnFacturacion && x.intSituacionFavBov != 4).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();

            /**PAGOS**/
            txtPagosTotalSol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnPago && x.tablc_iid_tipo_moneda == 1).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();
            txtPagosTotalDol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnPago && x.tablc_iid_tipo_moneda == 2).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();

            /**ANTICIPOS**/
            txtAnticipoTotalSol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnAnticipo && x.tablc_iid_tipo_moneda == 1).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();
            txtAnticipoTotalDol.Text = lstPlanillas.Where(x => x.tablc_iid_tipo_mov == Parametros.intPlnAnticipo && x.tablc_iid_tipo_moneda == 2).ToList().Sum(x => x.plnd_nmonto_pagado).ToString();

        }

        public void setValues()
        {
            txtPlanilla.Text = ObePlnCab.plnc_vnumero_planilla;
            txtDesripción.Text = ObePlnCab.plnc_vobservaciones;
            txtImporte.Text = ObePlnCab.plnc_nmonto_importe.ToString();
            txtMonto.Text = ObePlnCab.plnc_nmonto_pagado.ToString();
            dteFecha.EditValue = ObePlnCab.plnc_sfecha_planilla;
        }


        private void setFecha()
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {

                if (DateTime.Now.Year == Parametros.intEjercicio)
                    dteFecha.EditValue = DateTime.Now;
                else
                    dteFecha.EditValue = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
            }
        }
        bool ValidarExistenciaPlanillaDelDia()
        {
            var planillas = new BVentas().listarPlanillaCobranzaCab(Parametros.intEjercicio);
            return planillas.Exists(x => Convert.ToDateTime(x.plnc_sfecha_planilla.ToString().Substring(0, 10)) == Convert.ToDateTime(dteFecha.DateTime.ToString().Substring(0, 10)) && x.plnc_icod_planilla != ObePlnCab.plnc_icod_planilla);
        }
        private void nuevo(int intOpcion)
        {
            try
            {
                if (ValidarExistenciaPlanillaDelDia())
                {
                    Services.MessageError("Ya Existe PVD con la misma Fecha");
                    return;
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    ObePlnCab.plnc_vnumero_planilla = txtPlanilla.Text;
                    ObePlnCab.plnc_sfecha_planilla = Convert.ToDateTime(dteFecha.EditValue);
                    ObePlnCab.plnc_vobservaciones = txtDesripción.Text;
                    ObePlnCab.intUsuario = Valores.intUsuario;
                    ObePlnCab.strPc = WindowsIdentity.GetCurrent().Name;
                    ObePlnCab.tblc_iid_situacion = Convert.ToInt32(lkpSituacion.EditValue);
                    ObePlnCab.plnc_icod_pvt = Valores.rgpmc_icod_registro_parametro;
                }
                if (intOpcion == 1) //FATURACION
                {
                    frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                    frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                    frm.ObePlnCab = ObePlnCab;
                    frm.SetInsert();
                    frm.dtFechaDoc.EditValue = dteFecha.EditValue;
                    frm.dtFechaVenc.EditValue = dteFecha.EditValue;
                    frm.ShowDialog();

                }
                else if (intOpcion == 2) //PAGO
                {
                    frmPlanillaCabPago frm = new frmPlanillaCabPago();
                    frm.MiEvento += new frmPlanillaCabPago.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.SetInsert();
                    frm.dteFecha.EditValue = dteFecha.EditValue;
                    frm.ShowDialog();
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    frmManteAnticipo frm = new frmManteAnticipo();
                    frm.MiEvento += new frmManteAnticipo.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.SetInsert();
                    frm.dteFecha.EditValue = dteFecha.EditValue;
                    frm.ShowDialog();
                    frm.txtNroAnticipo.Text = (lstPlanillas.Where(x => x.tablc_iid_tipo_mov == 3).ToList().Count + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar(int intOpcion)
        {
            try
            {
                if (intOpcion == 1) // FACTURACION
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                    frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                    frm.ObePlnCab = ObePlnCab;
                    frm.oBePln = oBe;
                    frm.SetModify();
                    frm.setValues();
                    frm.Show();
                    if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                    {
                        frm.txtNombre.Enabled = false;
                        frm.txtDireccion.Enabled = false;
                        frm.txtDNI.Enabled = false;
                    }
                    else
                    {
                        frm.txtNombre.Enabled = true;
                        frm.txtDireccion.Enabled = true;
                        frm.txtDNI.Enabled = true;
                    }
                    frm.rbFactura.Enabled = false;
                    frm.rbBoleta.Enabled = false;
                }
                else if (intOpcion == 2) //PAGO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmPlanillaCabPago frm = new frmPlanillaCabPago();
                    frm.MiEvento += new frmPlanillaCabPago.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmManteAnticipo frm = new frmManteAnticipo();
                    frm.MiEvento += new frmManteAnticipo.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetModify();
                    frm.Show();
                    frm.setValues();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar(int intOpcion)
        {
            try
            {
                int index = viewPlanilla.FocusedRowHandle;

                if (intOpcion == Parametros.intPlnFacturacion)//FACTURACION
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    if (oBe == null)
                        return;

                    if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                    {
                        EFacturaCab obeFav = new EFacturaCab();
                        obeFav.dxcc_iid_doc_por_cobrar = Convert.ToInt64(oBe.intIcodDxc);
                        obeFav.favc_icod_factura = Convert.ToInt32(oBe.plnd_icod_documento);
                        obeFav.intUsuario = Valores.intUsuario;
                        obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                        EFacturaCab _faccc = new EFacturaCab();
                        _faccc = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                        obeFav.orpc_iid_orden_trabajo = _faccc.orpc_iid_orden_trabajo;
                        obeFav.strNroOrdenTrabajo = _faccc.strNroOrdenTrabajo;
                        /***********************************************/
                        new BVentas().eliminarFactura(obeFav);
                        reload(oBe.plnd_icod_detalle, ObePlnCab);
                    }
                    else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                    {
                        EBoletaCab obeBov = new EBoletaCab();
                        obeBov.dxcc_iid_doc_por_cobrar = Convert.ToInt64(oBe.intIcodDxc);
                        obeBov.bovc_icod_boleta = Convert.ToInt32(oBe.plnd_icod_documento);
                        obeBov.intUsuario = Valores.intUsuario;
                        obeBov.strPc = WindowsIdentity.GetCurrent().Name;
                        /***********************************************/
                        new BVentas().eliminarBoleta(obeBov);
                        reload(oBe.plnd_icod_detalle, ObePlnCab);
                    }

                }
                else if (intOpcion == 2) //PAGO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    //
                    EPagoDocVenta oBePgo = new EPagoDocVenta();
                    oBePgo.pgoc_dxc_icod_pago = Convert.ToInt64(oBe.pgoc_dxc_icod_pago);
                    oBePgo.pgoc_icod_pago = Convert.ToInt32(oBe.pgoc_icod_pago);
                    oBePgo.intUsuario = Valores.intUsuario;
                    oBePgo.strPc = WindowsIdentity.GetCurrent().Name;
                    //
                    new BVentas().eliminarPagoPln(ObePlnCab, oBe, oBePgo);
                    reload(0, ObePlnCab);
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    //
                    EAnticipo oBeAntc = new EAnticipo();
                    oBeAntc.antc_icod_anticipo = Convert.ToInt32(oBe.antc_icod_anticipo);
                    oBeAntc.antc_icod_adelanto_cliente = Convert.ToInt32(oBe.plnd_icod_documento);
                    oBeAntc.intUsuario = Valores.intUsuario;
                    oBeAntc.strPc = WindowsIdentity.GetCurrent().Name;
                    //
                    new BVentas().eliminarAnticipoPln(ObePlnCab, oBe, oBeAntc);
                    reload(0, ObePlnCab);
                }
                /***********************************************/
                if (lstPlanillas.Count >= index + 1)
                    viewPlanilla.FocusedRowHandle = index;
                else
                    viewPlanilla.FocusedRowHandle = index - 1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void view(int intOpcion)
        {
            try
            {
                if (intOpcion == 1) // FACTURACION
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                    frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                    frm.ObePlnCab = ObePlnCab;
                    frm.oBePln = oBe;
                    frm.SetCancel();
                    frm.Show();
                    frm.setValues();
                }
                else if (intOpcion == 2) //PAGO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmPlanillaCabPago frm = new frmPlanillaCabPago();
                    frm.MiEvento += new frmPlanillaCabPago.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetCancel();
                    frm.Show();
                    frm.setValues();
                }
                else if (intOpcion == 3) //ANTICIPO
                {
                    EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                    frmManteAnticipo frm = new frmManteAnticipo();
                    frm.MiEvento += new frmManteAnticipo.DelegadoMensaje(reload);
                    frm.oBePlnCab = ObePlnCab;
                    frm.oBePlnDet = oBe;
                    frm.SetCancel();
                    frm.Show();
                    frm.setValues();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
            {
                XtraMessageBox.Show("El documento se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (oBe.tablc_iid_tipo_mov == 1)
            {
                //var i = new BCuentasPorCobrar().getSituacionDocPorCobrar(Convert.ToInt64(oBe.pgoc_dxc_icod_documento));
                //if (i != 1)
                //{
                //    XtraMessageBox.Show("El documento no puede se modificado, su situación es diferente de generado ", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    view(oBe.tablc_iid_tipo_mov);
                //}
                //else
                string TD;
                if (oBe.plnd_icod_tipo_doc == 26)
                {
                    TD = "01";
                }
                else
                {
                    TD = "03";
                }

                List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(oBe.plnd_icod_documento.Value,TD).ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {

                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }
                modificar(oBe.tablc_iid_tipo_mov);

            }
            else if (oBe.tablc_iid_tipo_mov == 3)// MOVIMIENTO DE ANTICIPO
            {
                //CUANDO ES ANTICIPO SE DEBE VERIFICAR SU SITUACION SI YA ESTA PAGADO O NO....
                int intSituacionADC = new BCuentasPorCobrar().getSituacionDocPorCobrar(Convert.ToInt64(oBe.intIcodDxc));
                if (intSituacionADC != Parametros.intSitDocCobrarGenerado)
                {
                    string strSituacion = (intSituacionADC == Parametros.intSitDocCobrarPagadoParcial) ? "PARC. PAGADO" : (intSituacionADC == Parametros.intSitDocCobrarCancelado) ? "CANCELADO" : "";
                    XtraMessageBox.Show(String.Format("El anticipo NO puede ser modificado, su situación es {0}", strSituacion), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    DateTime dtHoy = Convert.ToDateTime("27/03/2014");
                    if (DateTime.Now.ToShortDateString() == dtHoy.ToShortDateString())
                        modificar(oBe.tablc_iid_tipo_mov);
                    else
                        view(oBe.tablc_iid_tipo_mov);
                }
                else
                {
                    modificar(oBe.tablc_iid_tipo_mov);
                }

            }
            else
                modificar(oBe.tablc_iid_tipo_mov);

        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(1);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            //if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
            //{
            //    XtraMessageBox.Show("El documento se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            if (oBe.tablc_iid_tipo_mov == 3)// MOVIMIENTO DE ANTICIPO
            {
                //CUANDO ES ANTICIPO SE DEBE VERIFICAR SU SITUACION SI YA ESTA PAGADO O NO....
                int intSituacionADC = new BCuentasPorCobrar().getSituacionDocPorCobrar(Convert.ToInt64(oBe.intIcodDxc));


                if (intSituacionADC != Parametros.intSitDocCobrarGenerado)
                {
                    string strSituacion = (intSituacionADC == Parametros.intSitDocCobrarPagadoParcial) ? "PARC. PAGADO" : (intSituacionADC == Parametros.intSitDocCobrarCancelado) ? "CANCELADO" : "";
                    XtraMessageBox.Show(String.Format("El anticipo NO puede ser eliminado, su situación es {0}", strSituacion), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    if (XtraMessageBox.Show("Esta seguro que desea ELIMINAR el anticipo", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        eliminar(oBe.tablc_iid_tipo_mov);
                        //ACTUALIZAR  PAGOS
                        if (oBe.tablc_iid_tipo_mov == 4)
                        {

                        }


                    }
                }
            }
            else
            {
                if (oBe.plnd_icod_tipo_doc == 26)
                {
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(oBe.plnd_icod_documento.Value,"01").ToList();
                    if (lstCab.Count > 0)
                    {
                        if (lstCab[0].EstadoFacturacion != 4)
                        {
                            XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (XtraMessageBox.Show("Esta seguro que desea ELIMINAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        eliminar(oBe.tablc_iid_tipo_mov);
                        List<EContratoCuotas> lstContrato = new List<EContratoCuotas>();



                        EFacturaCab oBeFav = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento)).FirstOrDefault();
                        EBoletaCab oBeBov = new BVentas().getBoletaCab(Convert.ToInt32(oBe.plnd_icod_documento)).FirstOrDefault();
                        if (oBeFav != null)
                        {
                            lstContrato = new BVentas().listarPagos(Convert.ToInt32(oBeFav.cntc_icod_contrato)).Where(x => (x.cntc_icod_documento == oBe.plnd_icod_documento)).ToList();
                        }
                        else
                        {
                            lstContrato = new BVentas().listarPagos(Convert.ToInt32(oBeBov.cntc_icod_contrato)).Where(x => (x.cntc_icod_documento == oBe.plnd_icod_documento)).ToList();
                        }

                        lstContrato = lstContrato.Where(x => x.pgc_icod_pago > 0).ToList();
                        lstContrato.ForEach(objp =>
                        {
                            new BVentas().Elimina_Pago(objp.pgc_icod_pago);
                            new BVentas().Actualizar_Pagos(objp.cntc_icod_contrato_cuotas, objp.pgc_icod_pago);
                        });
                        new BVentas().ActualizarContrato(oBeFav.cntc_icod_contrato);
                    }
                    eliminarSUNAT(oBe);
                }
                else
                {
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(oBe.plnd_icod_documento.Value ,"03").ToList();
                    if (lstCab.Count > 0)
                    {
                        if (lstCab[0].EstadoFacturacion != 4)
                        {
                            XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (XtraMessageBox.Show("Esta seguro que desea ELIMINAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {


                        eliminar(oBe.tablc_iid_tipo_mov);

                        List<EContratoCuotas> lstContrato = new List<EContratoCuotas>();



                        EFacturaCab oBeFav = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento)).FirstOrDefault();
                        EBoletaCab oBeBov = new BVentas().getBoletaCab(Convert.ToInt32(oBe.plnd_icod_documento)).FirstOrDefault();
                        if (oBeFav != null)
                        {
                            lstContrato = new BVentas().listarPagos(Convert.ToInt32(oBeFav.cntc_icod_contrato)).Where(x => (x.cntc_icod_documento == oBe.plnd_icod_documento)).ToList();
                        }
                        else
                        {
                            lstContrato = new BVentas().listarPagos(Convert.ToInt32(oBeBov.cntc_icod_contrato)).Where(x => (x.cntc_icod_documento == oBe.plnd_icod_documento)).ToList();
                        }

                        lstContrato = lstContrato.Where(x => x.pgc_icod_pago > 0).ToList();
                        lstContrato.ForEach(objp =>
                        {
                            new BVentas().Elimina_Pago(objp.pgc_icod_pago);
                            new BVentas().Actualizar_Pagos(objp.cntc_icod_contrato_cuotas, objp.pgc_icod_pago);
                        });
                        new BVentas().ActualizarContrato(oBeBov.cntc_icod_contrato);
                    }
                    eliminarSUNAT(oBe);
                }

            }

        }

        

        public void eliminarSUNAT(EPlanillaCobranzaDet oBe)
        {
            List<EFacturaVentaElectronica> lstCab = new List<EFacturaVentaElectronica>();
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            if (oBe.plnd_icod_tipo_doc == 26)
            {
                lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(oBe.plnd_icod_documento.Value, "01").ToList();
            }
            else
            {
                lstCab = new BVentas().FacturaVentaElectronicaObtenerDoc(oBe.plnd_icod_documento.Value, "03").ToList();
            }
            int IdCabecera = lstCab[0].IdCabecera;
            new BVentas().eliminarFacturaVentaElectronica(IdCabecera);
            if (lstCab.Count > 0)
            {
                new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabecera);
            }



        }
        private void pagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(2);
        }

        private void anticipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo(3);
        }

        private void frmMantePlanillaCab_FormClosing(object sender, FormClosingEventArgs e)
        {
            modificarPlanillaCab();
            MiEvento(ObePlnCab.plnc_icod_planilla);
        }

        private void modificarPlanillaCab()
        {
            if (ObePlnCab.plnc_icod_planilla == 0)
                return;
            ObePlnCab.plnc_sfecha_planilla = Convert.ToDateTime(dteFecha.EditValue);
            ObePlnCab.plnc_vobservaciones = txtDesripción.Text;
            ObePlnCab.plnc_nmonto_importe = Convert.ToDecimal(txtImporte.Text);
            ObePlnCab.plnc_nmonto_pagado = Convert.ToDecimal(txtMonto.Text);
            new BVentas().modificarPlanillaCab(ObePlnCab);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void viewPlanilla_DoubleClick(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            view(oBe.tablc_iid_tipo_mov);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;

            if (oBe.tablc_iid_tipo_mov != Parametros.intPlnFacturacion)
            {
                XtraMessageBox.Show("La impresión solo esta disponible para FACTURAS y BOLETAS", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
            {
                XtraMessageBox.Show("El documento se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
            {
                var oBeCab = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                //
                string total = Convertir.ConvertNumeroEnLetras(oBeCab.favc_nmonto_total.ToString());

                decimal monto_toal_doc = 0;
                var lstdet = new BVentas().listarFacturaDetalle(oBeCab.favc_icod_factura);
                foreach (var _ve in lstdet)
                {
                    monto_toal_doc = monto_toal_doc + _ve.favd_nprecio_total_item;
                }
                rptFactura rpt = new rptFactura();
                rpt.cargar(oBeCab, lstdet, total, monto_toal_doc);
            }
            else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
            {
                var oBeCab = new BVentas().getBoletaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                //
                string total = Convertir.ConvertNumeroEnLetras(oBeCab.bovc_nmonto_total.ToString());

                decimal monto_toal_doc = 0;
                var lstdet = new BVentas().listarBoletaDetalle(oBeCab.bovc_icod_boleta);

                rptBoleta rpt = new rptBoleta();
                rpt.cargar(oBeCab, lstdet, total);
            }

        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet oBe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                if (oBe.tablc_iid_tipo_mov != Parametros.intPlnFacturacion)
                {
                    XtraMessageBox.Show("La anulación solo esta disponible para FACTURAS y BOLETAS", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (oBe.tablc_iid_tipo_mov == Parametros.intPlnFacturacion)
                {
                    List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                    List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == oBe.plnd_icod_documento).ToList();
                    if (oBe.plnd_sfecha_doc > lstParamatro[0].pm_sfecha_inicio)
                    {
                        if (lstCab.Count > 0)
                        {
                            if (lstCab[0].EstadoFacturacion == 4)
                            {
                                XtraMessageBox.Show("La Factura de Venta aun no fue enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    if (oBe.intSituacionFavBov == Parametros.intSitDocCobrarAnulado)
                    {
                        XtraMessageBox.Show("El documento ya se encuentra ANULADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (lstCab.Count > 0)
                    {
                        if (lstCab[0].EstadoFacturacion == 3)
                        {
                            if (XtraMessageBox.Show("Esta seguro que desea ANULAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                                {
                                    EFacturaCab obeFav = new EFacturaCab();
                                    obeFav.favc_icod_factura = Convert.ToInt32(oBe.plnd_icod_documento);
                                    obeFav.intUsuario = Valores.intUsuario;
                                    obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                                    EFacturaCab _faccc = new EFacturaCab();
                                    _faccc = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                                    obeFav.orpc_iid_orden_trabajo = _faccc.orpc_iid_orden_trabajo;
                                    obeFav.strNroOrdenTrabajo = _faccc.strNroOrdenTrabajo;

                                    new BVentas().anularFactura(obeFav);
                                    reload(oBe.plnd_icod_detalle, ObePlnCab);
                                }
                                else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                                {
                                    EBoletaCab obeBov = new EBoletaCab();
                                    obeBov.bovc_icod_boleta = Convert.ToInt32(oBe.plnd_icod_documento);
                                    obeBov.intUsuario = Valores.intUsuario;
                                    obeBov.strPc = WindowsIdentity.GetCurrent().Name;

                                    new BVentas().anularBoleta(obeBov);
                                    reload(oBe.plnd_icod_detalle, ObePlnCab);
                                }
                            }
                        }
                        if (lstCab[0].EstadoFacturacion == 2)
                        {
                            if (XtraMessageBox.Show("Esta seguro que desea ANULAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {



                                if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                                {
                                    FrmComunicacionBaja frm = new FrmComunicacionBaja();
                                    frm.obj = oBe;
                                    frm.SetInsert();
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        flag_close = frm.flag_close;
                                    }
                                    if (flag_close == true)
                                    {
                                        EFacturaCab obeFav = new EFacturaCab();
                                        obeFav.favc_icod_factura = Convert.ToInt32(oBe.plnd_icod_documento);
                                        obeFav.intUsuario = Valores.intUsuario;
                                        obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                                        EFacturaCab _faccc = new EFacturaCab();
                                        _faccc = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                                        obeFav.orpc_iid_orden_trabajo = _faccc.orpc_iid_orden_trabajo;
                                        obeFav.strNroOrdenTrabajo = _faccc.strNroOrdenTrabajo;

                                        new BVentas().anularFactura(obeFav);
                                        reload(oBe.plnd_icod_detalle, ObePlnCab);
                                    }
                                }
                                else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                                {
                                    FrmComunicacionBajaBoleta frm = new FrmComunicacionBajaBoleta();
                                    frm.obj = oBe;
                                    frm.SetInsert();
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        flag_close = frm.flag_close;
                                    }
                                    if (flag_close == true)
                                    {

                                        EBoletaCab obeBov = new EBoletaCab();
                                        obeBov.bovc_icod_boleta = Convert.ToInt32(oBe.plnd_icod_documento);
                                        obeBov.intUsuario = Valores.intUsuario;
                                        obeBov.strPc = WindowsIdentity.GetCurrent().Name;

                                        new BVentas().anularBoleta(obeBov);
                                        reload(oBe.plnd_icod_detalle, ObePlnCab);
                                    }
                                }



                            }
                        }

                    }
                    else
                    {
                        if (XtraMessageBox.Show("Esta seguro que desea ANULAR el registro", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                            {
                                EFacturaCab obeFav = new EFacturaCab();
                                obeFav.favc_icod_factura = Convert.ToInt32(oBe.plnd_icod_documento);
                                obeFav.intUsuario = Valores.intUsuario;
                                obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                                EFacturaCab _faccc = new EFacturaCab();
                                _faccc = new BVentas().getFacturaCab(Convert.ToInt32(oBe.plnd_icod_documento))[0];
                                obeFav.orpc_iid_orden_trabajo = _faccc.orpc_iid_orden_trabajo;
                                obeFav.strNroOrdenTrabajo = _faccc.strNroOrdenTrabajo;

                                new BVentas().anularFactura(obeFav);
                                reload(oBe.plnd_icod_detalle, ObePlnCab);
                            }
                            else if (oBe.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                            {
                                EBoletaCab obeBov = new EBoletaCab();
                                obeBov.bovc_icod_boleta = Convert.ToInt32(oBe.plnd_icod_documento);
                                obeBov.intUsuario = Valores.intUsuario;
                                obeBov.strPc = WindowsIdentity.GetCurrent().Name;

                                new BVentas().anularBoleta(obeBov);
                                reload(oBe.plnd_icod_detalle, ObePlnCab);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void AnularFacturaElectronica()
        {
            //int intIcodE = 0;
            //EPlanillaCobranzaDet Obe = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //List<EFacturaVentaCab> lstDet = new List<EFacturaVentaCab>();
            //lstDet = new BVentas().listarfacturaVentaDetalle(Obe.facv_icod_fac_venta);
            //Obe.nroDocumentoEmisior = "20492971732";
            //Obe.nombreLegalEmisor = "NOVA GLASS";
            //Obe.nombreComercialEmisor = Valores.strNombreEmpresa;
            //Obe.direccionEmisor = Valores.strDireccionFiscal;

            //Obe.nroDocumentoReceptor = Obe.cliec_cruc;
            //Obe.nombreLegalReceptor = Obe.cliec_vnombre_cliente;
            //Obe.doc_icod_documento = Obe.facv_icod_fac_venta;
            //intIcodE = new BVentas().insertarfacturaVentaElectronicaAnulado(Obe);
            //foreach (var ob in lstDet)
            //{
            //    ob.facvd_icod_fac_venta = intIcodE;
            //    new BVentas().insertarfacturaVentaElectronicaDetalle(ob);
            //}
            //new BVentas().actualizarFacturaElectronicaResponseAnulacion(intIcodE, (int)EstadoDocumento.bajasporEnviar /*(int)EstadoDocumento.enviadoSunat*/);
        }
        private void viewPlanilla_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strSituacionFavBov"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            lstPlanillas.ForEach(x =>
            {
                if (x.tablc_iid_tipo_mov == 1)
                {
                    if (x.strSituacionFavBov != "ANULADO")
                    {
                        frmMantePlanillaCobranza frm = new frmMantePlanillaCobranza();
                        frm.MiEvento += new frmMantePlanillaCobranza.DelegadoMensaje(reload);
                        frm.ObePlnCab = ObePlnCab;
                        frm.oBePln = x;
                        frm.SetModify();
                        frm.Show();
                        frm.setValues();

                    }
                    else if (x.strSituacionFavBov == "ANULADO")
                    {
                        if (x.plnd_icod_tipo_doc == Parametros.intTipoDocFacturaVenta)
                        {
                            EFacturaCab obeFav = new EFacturaCab();
                            obeFav.favc_icod_factura = Convert.ToInt32(x.plnd_icod_documento);
                            obeFav.intUsuario = Valores.intUsuario;
                            obeFav.strPc = WindowsIdentity.GetCurrent().Name;

                            new BVentas().anularFactura(obeFav);
                            //reload(x.plnd_icod_detalle, ObePlnCab);
                        }
                        else if (x.plnd_icod_tipo_doc == Parametros.intTipoDocBoletaVenta)
                        {
                            EBoletaCab obeBov = new EBoletaCab();
                            obeBov.bovc_icod_boleta = Convert.ToInt32(x.plnd_icod_documento);
                            obeBov.intUsuario = Valores.intUsuario;
                            obeBov.strPc = WindowsIdentity.GetCurrent().Name;

                            new BVentas().anularBoleta(obeBov);
                            //reload(oBe.plnd_icod_detalle, ObePlnCab);
                        }
                    }
                }

            });
        }

        private void imprimirElectronicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaCobranzaDet ObeFC = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);

            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            string TD;
            if (ObeFC.plnd_icod_tipo_doc == 26)
            {
                TD = "01";
            }
            else
            {
                TD = "03";
            }
            List<EFacturaVentaElectronica> lstFE = new BVentas().FacturaVentaElectronicaObtenerDoc(ObeFC.plnd_icod_documento.Value, TD).ToList();

            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);


            rptFacturaElectronico rptFcatura = new rptFacturaElectronico();
            rptBoletaElectronico rptBoleta = new rptBoletaElectronico();
            if (Obe.tipoDocumento == "01")
            {
                rptFcatura.cargar(Obe, mlisdet, Obe.Hora);
                rptFcatura.ShowPreview();
            }
            if (Obe.tipoDocumento == "03")
            {
                rptBoleta.cargar(Obe, mlisdet, Obe.Hora);
                rptBoleta.ShowPreview();
            }
        }

        private void enviarSunatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnviarSunat();
        }
        private async void EnviarSunat()
        {
            Boolean Flag = true;
            if (XtraMessageBox.Show("¿Esta seguro de enviar a la sunat?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                Flag = false;
                return;
            }
            EPlanillaCobranzaDet ObeF = (EPlanillaCobranzaDet)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (ObeF == null)
                return;
            ObeF.plnd_vnumero_doc = ObeF.plnd_vnumero_doc.Remove(4, 8) + '-' + ObeF.plnd_vnumero_doc.Remove(0, 4);


            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();


            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            Obe = new BVentas().listarfacturaVentaElectronica(lstParametro[0].pm_sfecha_inicio).Where(p => p.idDocumento == ObeF.plnd_vnumero_doc && p.tipoDocumento == "01").FirstOrDefault();
            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);
            response = await EviarFacturaElectronica(Obe, mlisdet);
            mensajeRespuesta = new List<string>();

            if (response.Exito)
            {
                mensajeRespuesta.Add($"{Obe.idDocumento}; se envio correctamente.");
            }
            else
            {
                mensajeRespuesta.Add($"{Obe.idDocumento}; ocurrio un error para mas detalle filtar por la opcion rechazados.");
            }

            response.IdCabezera = Obe.IdCabecera;
            int idResponse = new BVentas().insertarFacturaElectronicaResponse(response);
            if (idResponse == 0)
            {
                new BVentas().modificarFacturaElectronicaResponse(Obe.IdCabecera);
                new BVentas().insertarFacturaElectronicaResponse(response);
            }
            if (response.Exito)
            {
                new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.enviadoSunat);
                int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                if (codigoRespuesta <= 0)
                {
                    new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.aprobado);
                }
            }
            else
            {
                int estadoSunat = 0;

                int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                if (codigoRespuesta >= (int)EstadoDocumento.rangoExcepcionMin &&
                    codigoRespuesta <= (int)EstadoDocumento.rangoExcepcionMax)
                {
                    estadoSunat = (int)EstadoDocumento.rechazado;
                    /*estadoEnvio = (int)EstadoDocumento.PendienteObservadoEnvio;*/
                }
                else if (codigoRespuesta >= (int)EstadoDocumento.rangoRechazadoMin &&
                    codigoRespuesta <= (int)EstadoDocumento.rangoRechazadoMax)
                {
                    estadoSunat = (int)EstadoDocumento.rechazado;
                }
                else if (codigoRespuesta >= (int)EstadoDocumento.rangoObservadoMax)
                {
                    estadoSunat = (int)EstadoDocumento.rechazado;
                    /* estadoEnvio = (int)EstadoDocumento.PendienteObservadoEnvio;*/
                }

                new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, estadoSunat);
            }
            RespuestaSunat form = new RespuestaSunat();
            form.ListaMensajeRespuesta = this.mensajeRespuesta;
            form.ShowDialog();
            cargar();

        }
        public async Task<EFacturaElectronicaResponse> EviarFacturaElectronica(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();

            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.DatosGuiaTransportista = null;
            objdocumento.DatoAdicionales = new List<DatoAdicional>();
            objdocumento.Relacionados = new List<DocumentoRelacionado>();
            objdocumento.OtrosDocumentosRelacionados = new List<DocumentoRelacionado>();
            objdocumento.Discrepancias = new List<Discrepancia>();

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);

            EFacturaElectronicaResponse response = await model.FacturaElectronica(objdocumento);

            return response;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void grdPlanilla_Click(object sender, EventArgs e)
        {

        }
    }
}