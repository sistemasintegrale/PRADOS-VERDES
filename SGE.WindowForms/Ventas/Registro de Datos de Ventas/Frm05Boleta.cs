using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Ventas;
using SGE.Entity;
using SGE.BusinessLogic;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm05Boleta : DevExpress.XtraEditors.XtraForm
    {
        List<EBoletaCab> lstBoletas = new List<EBoletaCab>();
        public Frm05Boleta()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstBoletas = new BVentas().listarBoletaCab(Parametros.intEjercicio);
            grdBoleta.DataSource = lstBoletas;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstBoletas.FindIndex(x => x.bovc_icod_boleta == intIcod);
            viewBoleta.FocusedRowHandle = index;
            viewBoleta.Focus();
        }


        private void nuevo()
        {
            FrmManteBoleta frm = new FrmManteBoleta();
            frm.MiEvento += new FrmManteBoleta.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();

            //var nro = lstBoletas.Max(a => Convert.ToInt32(a.bovc_vnumero_boleta.Substring(4, 8))) + 1;
            //frm.txtNumero.Text = String.Format("{0:00000000}", nro);
            //frm.lkpMoneda.EditValue = 4;//DOLARES
            //frm.txtSerie.Text = "001";
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void modificar()
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                if (obe.tablc_iid_situacion != 8)
                    throw new ArgumentException(String.Format("La boleta no puede ser modificada, su situación es ", obe.strSituacion));
                if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                    throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser anulada");
                FrmManteBoleta frm = new FrmManteBoleta();
                frm.MiEvento += new FrmManteBoleta.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.CargarControles();
                frm.SetModify();
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private bool verificarDocVentaPlanilla(int intIcodDoc)
        {
            bool flagExiste = false;
            var intFlag = new BVentas().verificarDocVentaPlanilla(Parametros.intTipoDocBoletaVenta, intIcodDoc);
            if (intFlag > 0)
                flagExiste = true;

            return flagExiste;

        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();       
        }

        private void anular() 
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParametro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == obe.doc_icod_documento).ToList();
                if (obe.bovc_sfecha_boleta > lstParametro[0].pm_sfecha_inicio)
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


                if (obe.bovc_sfecha_boleta > lstParametro[0].pm_sfecha_inicio)
                {
                    if (lstCab.Count > 0)
                    {
                        if (lstCab[0].EstadoFacturacion == 3)
                        {
                            if (obe.tablc_iid_situacion != 8)
                                throw new ArgumentException(String.Format("La boleta no puede ser anulada, su situación es {0}", obe.strSituacion));
                            if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                                throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser anulada");
                            if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                new BVentas().AnularBoletaVenta(obe);
                                reload(obe.bovc_icod_boleta);

                            }
                        }
                        if (lstCab[0].EstadoFacturacion == 2)
                        {
                            if (obe.tablc_iid_situacion != 8)
                                throw new ArgumentException(String.Format("La boleta no puede ser anulada, su situación es {0}", obe.strSituacion));
                            if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                                throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser anulada");
                            if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                AnularFacturaElectronica();
                                new BVentas().AnularBoletaVenta(obe);
                                reload(obe.bovc_icod_boleta);

                            }
                        }
                    }
                    else
                    {
                        if (obe.tablc_iid_situacion != 8)
                            throw new ArgumentException(String.Format("La boleta no puede ser anulada, su situación es {0}", obe.strSituacion));
                        if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                            throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser anulada");
                        if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            new BVentas().AnularBoletaVenta(obe);
                            reload(obe.bovc_icod_boleta);

                        }
                    }
                }
                else
                {
                    if (obe.tablc_iid_situacion != 8)
                        throw new ArgumentException(String.Format("La boleta no puede ser anulada, su situación es {0}", obe.strSituacion));
                    if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                        throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser anulada");
                    if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        new BVentas().AnularBoletaVenta(obe);
                        reload(obe.bovc_icod_boleta);

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
            int intIcodE = 0;
            EBoletaCab Obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EBoletaDet> lstDet = new List<EBoletaDet>();
            lstDet = new BVentas().listarBoletaDetalle(Obe.bovc_icod_boleta);
            ESunatResumenDet obeSR = new ESunatResumenDet();

            //obeSR.IdCabecera = 0;
            obeSR.IdDocumento = Obe.bovc_vnumero_boleta.Remove(4, 8) + '-' + Obe.bovc_vnumero_boleta.Remove(0, 4);
            obeSR.TipoDocumentoReceptor = "6";
            obeSR.NroDocumentoReceptor = Valores.strRUC;
            obeSR.CodigoEstadoItem = 3;
            obeSR.CorrelativoInicio = 0;
            obeSR.CorrelativoFin = 0;
            if (Obe.tablc_iid_tipo_moneda == 3)
            {
                obeSR.Moneda = "PEN";
            }
            else
            {
                obeSR.Moneda = "USD";
            }
            obeSR.TotalVenta = Obe.bovc_nmonto_total;
            obeSR.TotalDescuentos = Convert.ToDecimal(0.00);
            obeSR.TotalIgv = Obe.bovc_nmonto_imp;
            obeSR.TotalIsc = Convert.ToDecimal(0.00);
            obeSR.TotalOtrosImpuestos = Convert.ToDecimal(0.00);
            obeSR.Gravadas = Obe.bovc_nmonto_neto;
            obeSR.Exoneradas = Convert.ToDecimal(0.00);
            obeSR.Inafectas = Convert.ToDecimal(0.00);
            obeSR.Exportacion = Convert.ToDecimal(0.00);
            obeSR.Gratuitas = Convert.ToDecimal(0.00);
            obeSR.Id = 1;
            obeSR.TipoDocumento = "03";
            obeSR.Serie = Obe.bovc_vnumero_boleta.Remove(0, 4);
            new BVentas().insertarSunatResumenDet(obeSR);
        }
        private void eliminar() 
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewBoleta.FocusedRowHandle;
            try
            {

                List<ENotaCredito> lstNotaCreditoRefresh = new List<ENotaCredito>();
                lstNotaCreditoRefresh = new BVentas().listarNotaCreditoClienteCab(Parametros.intEjercicio).Where(x => x.ncrec_icod_credito == obe.bovc_icod_boleta).ToList();
                //if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)

                List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == obe.doc_icod_documento).ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {

                        new BVentas().eliminarBoletaElectronicaDetalle(lstCab[0].IdCabezera);
                        new BVentas().eliminarBoletaElectronica(lstCab[0].IdCabezera);


                    }
                }





                if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarGenerado)
                {
                    if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarAnulado)
                    {
                        throw new ArgumentException(String.Format("La boleta no puede ser eliminada, su situación es {0}", obe.strSituacion));
                    }
                }
                if (verificarDocVentaPlanilla(obe.bovc_icod_boleta))
                    throw new ArgumentException("La boleta ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la boleta?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    new BVentas().eliminarBoletaVenta(obe);
                    reload(obe.bovc_icod_boleta);
                    /***********************************************/
                    if (lstBoletas.Count >= index + 1)
                        viewBoleta.FocusedRowHandle = index;
                    else
                        viewBoleta.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anular();      
        }

        private void viewFactura_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strSituacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnAnular_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            anular();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdBoleta.DataSource = lstBoletas.Where(x => x.bovc_vnumero_boleta.Trim().Contains(txtNumero.Text.Trim()) &&
                x.cliec_vnombre_cliente.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void actualizarDescripcionDeDXCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BVentas().ActualizarDescripcionDXCBov(lstBoletas);
        }

        private void viewBoleta_DoubleClick(object sender, EventArgs e)
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                
                FrmManteBoleta frm = new FrmManteBoleta();
                frm.MiEvento += new FrmManteBoleta.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.SetModify();
                frm.Show();
                frm.txtMontoIGV.Properties.ReadOnly = true;
                frm.mnu.Enabled = false;
                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void imprimirBoletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;

            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? Dios = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;

            string pais;
            string Departamento = "";
            string Provincia = "";
            string Distrito = "";
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Dios = obe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Departamento = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }

                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Provincia = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Provincia = oB.ubicc_vnombre_ubicacion;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Distrito = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;

            }


            string total = Convertir.ConvertNumeroEnLetras(obe.bovc_nmonto_total.ToString());

            var lstdet = new BVentas().listarBoletaDetalle(obe.bovc_icod_boleta);

            List<EBoletaDet> mlistDetalle = new List<EBoletaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.bolvd_vobservaciones.Split('@');
                int cont_estapacios = 0;
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] == "")
                    {
                        cont_estapacios = cont_estapacios + 1;
                    }
                }
                if (cont_estapacios != partes.Length)
                {
                    for (int i = 0; i < partes.Length; i++)
                    {
                        if (i == 0)
                        {
                            EBoletaDet __be = new EBoletaDet();
                            __be.strDesProducto = "    " + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EBoletaDet __be = new EBoletaDet();
                                __be.strDesProducto = "    " + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }


            rptBoleta rpt = new rptBoleta();
            //rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);/*Falta Arreglar Por Modificar Planilla*/
        }

        private void imprimirBoletaChicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab obe = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            if (obe == null)
                return;

            List<EUbicacion> lUbicacion = new BVentas().ListarUbicacion();
            int? tipo = null;
            int? Dios = null;
            int? padre = null;
            int? abuelo = null;
            int? bisabuelo = null;

            string pais;
            string Departamento = "";
            string Provincia = "";
            string Distrito = "";
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Dios = obe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Departamento = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }

                    break;
                case 2:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Provincia = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;
                case 1:
                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == padre).ToList().ForEach(oB =>
                    {
                        abuelo = oB.ubicc_icod_ubicacion_padre;
                        Provincia = oB.ubicc_vnombre_ubicacion;
                    });

                    lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == abuelo).ToList().ForEach(oB =>
                    {
                        bisabuelo = oB.ubicc_icod_ubicacion_padre;
                        Departamento = oB.ubicc_vnombre_ubicacion;
                    });
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == obe.ubicc_icod_ubicacion)
                        {
                            Distrito = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;

            }


            string total = Convertir.ConvertNumeroEnLetras(obe.bovc_nmonto_total.ToString());

            var lstdet = new BVentas().listarBoletaDetalle(obe.bovc_icod_boleta);

            List<EBoletaDet> mlistDetalle = new List<EBoletaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.bolvd_vobservaciones.Split('@');
                int cont_estapacios = 0;
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] == "")
                    {
                        cont_estapacios = cont_estapacios + 1;
                    }
                }
                if (cont_estapacios != partes.Length)
                {
                    for (int i = 0; i < partes.Length; i++)
                    {
                        if (i == 0)
                        {
                            EBoletaDet __be = new EBoletaDet();
                            __be.strDesProducto = "    " + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EBoletaDet __be = new EBoletaDet();
                                __be.strDesProducto = "    " + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }


            rptBoletaChica rpt = new rptBoletaChica();
            rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);
        }

        private void afectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab Obea = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            BVentas afecto = new BVentas();
            Obea.bfavc_bafecto_igv = true;
                afecto.ActualizarAfecto(Obea.bovc_icod_boleta, Obea.bfavc_bafecto_igv);
           
            cargar();
            int index = lstBoletas.FindIndex(x => x.bovc_icod_boleta == Obea.bovc_icod_boleta);
                viewBoleta.FocusedRowHandle = index; 
            
        }

        private void inafectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBoletaCab Obea = (EBoletaCab)viewBoleta.GetRow(viewBoleta.FocusedRowHandle);
            BVentas afecto = new BVentas();
            Obea.bfavc_bafecto_igv = false;
            afecto.ActualizarAfecto(Obea.bovc_icod_boleta, Obea.bfavc_bafecto_igv);

            cargar();
            int index = lstBoletas.FindIndex(x => x.bovc_icod_boleta == Obea.bovc_icod_boleta);
            viewBoleta.FocusedRowHandle = index; 
        }

        private void actualizarNumeroToolStripMenuItem_Click(object sender, EventArgs e)
        {

            lstBoletas.ForEach(x =>
            {
                if (x.bovc_vnumero_boleta.Length != 12)
                {
                    string Serie = "";
                    string Correlativo = "";
                    string Cero = "0";
                    string Numero = "";
                    Serie = x.bovc_vnumero_boleta.Substring(0, 3);
                    Correlativo = x.bovc_vnumero_boleta.Substring(3, 7);
                    Numero = Cero + Serie + Cero + Correlativo;
                    x.bovc_vnumero_boleta = Numero;
                }
 
                new BVentas().modificarBoletaNumero(x);

            });
        }

        private void actualizarIVAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstBoletas.ForEach(x => 
            {
                List<EBoletaDet> lstBoletaDet = new BVentas().listarBoletaDetalle(x.bovc_icod_boleta).Where(xx=> xx.prdc_icod_producto == 20).ToList();
                lstBoletaDet.ForEach(d => 
                {
                    decimal SumaIVAP = 0;
                    x.bovc_npor_imp_igv = 0;
                    x.bovc_npor_imp_ivap = 4;
                    x.bovc_nmonto_imp = 0;
                    x.bovc_bind_arroz = true;
                    SumaIVAP = SumaIVAP + d.bovd_nmonto_imp_arroz;
                    x.bovc_nmonto_ivap = SumaIVAP;
                    x.bovc_nmonto_neto = x.bovc_nmonto_total - x.bovc_nmonto_ivap;
                    new BVentas().modificarBoletaVenta(x);
                });

            });
        }
    }
}