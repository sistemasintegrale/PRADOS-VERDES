using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Ventas;
using SGE.Entity;
using SGE.BusinessLogic;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Configuration;
using DevExpress.XtraReports.UI;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class Frm04Factura : DevExpress.XtraEditors.XtraForm
    {
        List<EFacturaCab> lstFacturas = new List<EFacturaCab>();
        public Frm04Factura()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstFacturas = new BVentas().listarFacturaCab(Parametros.intEjercicio);
            grdFactura.DataSource = lstFacturas;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstFacturas.FindIndex(x => x.favc_icod_factura == intIcod);
            viewFactura.FocusedRowHandle = index;
            viewFactura.Focus();
        }

       

      

        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void modificar()
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == obe.doc_icod_documento).ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (obe.tablc_iid_situacion != 8)//GENERADO
                    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));
                if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser modificada");

                FrmManteFactura frm = new FrmManteFactura();
                frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.CargarControles();
                frm.SetModify();
                frm.Show();
                frm.BtnGuiaRemision.Enabled = false;
                //frm.ChkIndArroz.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private bool verificarDocVentaPlanilla(int intIcodDoc)
        {
            bool flagExiste = false;
            var intFlag = new BVentas().verificarDocVentaPlanilla(Parametros.intTipoDocFacturaVenta, intIcodDoc);
            if (intFlag > 0)
                flagExiste = true;          

            return flagExiste;
 
        }
        private void anular() 
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == obe.doc_icod_documento).ToList();
                if (obe.favc_sfecha_factura > lstParamatro[0].pm_sfecha_inicio)
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
                if (obe.tablc_iid_situacion != 8)//GENERADO
                    throw new ArgumentException(String.Format("La factura no puede ser anulada, su situación es {0}", obe.strSituacion));
                if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser anulada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ANULAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (lstCab.Count > 0)
                    {
                        if (lstCab[0].EstadoFacturacion == 3)
                        {
                            new BVentas().AnularFacturaVenta(obe);
                            reload(obe.favc_icod_factura);
                        }
                        if (lstCab[0].EstadoFacturacion == 3)
                        {
                            AnularFacturaElectronica();
                            new BVentas().AnularFacturaVenta(obe);
                            reload(obe.favc_icod_factura);
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
            int intIcodE = 0;
            EFacturaCab Obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EFacturaDet> lstDet = new List<EFacturaDet>();
            lstDet = new BVentas().listarFacturaDetalle(Obe.favc_icod_factura);
            Obe.nroDocumentoEmisior = "20427486843";
            Obe.nombreLegalEmisor = "NOVA GLASS";
            Obe.nombreComercialEmisor = Valores.strNombreEmpresa;
            Obe.direccionEmisor = Valores.strDireccionFiscal;

            Obe.nroDocumentoReceptor = Obe.favc_vruc;
            Obe.nombreLegalReceptor = Obe.cliec_vnombre_cliente;
            Obe.doc_icod_documento = Obe.favc_icod_factura;
            intIcodE = new BVentas().insertarfacturaVentaElectronicaAnulado(Obe);
            foreach (var ob in lstDet)
            {
                ob.favc_icod_factura = intIcodE;
                new BVentas().insertarfacturaVentaElectronicaDetalle(ob);
            }
            new BVentas().actualizarFacturaElectronicaResponseAnulacion(intIcodE, (int)EstadoDocumento.bajasporEnviar /*(int)EstadoDocumento.enviadoSunat*/);
        }
        private void eliminar() 
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewFactura.FocusedRowHandle;
            try
            {
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

                        new BVentas().eliminarFacturaVentaElectronicaDetalle(lstCab[0].IdCabezera);
                        new BVentas().eliminarFacturaVentaElectronica(lstCab[0].IdCabezera);


                    }
                }
                if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarGenerado)
                {
                    if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarAnulado)
                    {
                        throw new ArgumentException(String.Format("La factura no puede ser eliminada, su situación es {0}", obe.strSituacion));
                    }
                }

                if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser eliminada");
                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la factura?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    new BVentas().EliminarFacturaVenta(obe);
                    reload(obe.favc_icod_factura);
                    /***********************************************/
                    if (lstFacturas.Count >= index + 1)
                        viewFactura.FocusedRowHandle = index;
                    else
                        viewFactura.FocusedRowHandle = index - 1;
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
           
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            anular();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdFactura.DataSource = lstFacturas.Where(x => x.favc_vnumero_factura.Trim().Contains(txtNumero.Text.Trim()) &&
                x.cliec_vnombre_cliente.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper())).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void viewFactura_DoubleClick(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                //if (obe.tablc_iid_situacion != Parametros.intSitDocCobrarGenerado)
                //    throw new ArgumentException(String.Format("La factura no puede ser modificada, su situación es ", obe.strSituacion));
                //if (verificarDocVentaPlanilla(obe.favc_icod_factura))
                //    throw new ArgumentException("La factura ha sido generada desde una Planilla de Venta, no puede ser modificada");

                FrmManteFactura frm = new FrmManteFactura();
                //frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.setValues();
                //frm.SetCancel();
                frm.Show();
                frm.mnu.Enabled = false;
                frm.btnGuardar.Enabled = false;
                frm.bteCliente.Enabled = false;
                frm.txtSerie.Properties.ReadOnly = true;
                frm.txtNumero.Properties.ReadOnly = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void actualizarDescripcionDeDXCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BVentas().ActualizarDescripcionDXCFAC(lstFacturas);
        }

        private void actualizarCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void facturaGrandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
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



            string total = Convertir.ConvertNumeroEnLetras(obe.favc_nmonto_total.ToString());
            var lstdet = new BVentas().listarFacturaDetalle(obe.favc_icod_factura);

            List<EFacturaDet> mlistDetalle = new List<EFacturaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                //string[] partes = partes = _BE.favd_nobservaciones.Split('@');
                //int cont_estapacios = 0;
                //for (int i = 0; i < partes.Length; i++)
                //{
                //    if (partes[i] == "")
                //    {
                //        cont_estapacios = cont_estapacios + 1;
                //    }
                //}
                //if (cont_estapacios != partes.Length)
                //{
                //    for (int i = 0; i < partes.Length; i++)
                //    {
                //        if (i == 0)
                //        {
                //            EFacturaDet __be = new EFacturaDet();
                //            __be.strDesProducto = "    " + partes[i];
                //            __be.OrdenItemImprimir = cont + 1;
                //            cont++;
                //            mlistDetalle.Add(__be);
                //        }
                //        else
                //        {
                //            if (partes[i] != "")
                //            {
                //                EFacturaDet __be = new EFacturaDet();
                //                __be.strDesProducto = "    " + partes[i];
                //                __be.OrdenItemImprimir = cont + 1;
                //                cont++;
                //                mlistDetalle.Add(__be);
                //            }
                //        }
                //    }
                //}

            }

            using (FrmElegirImpresora frmImpresora = new FrmElegirImpresora())
            {
                frmImpresora.cargar();
                frmImpresora.ckImpresora.Checked = true;
                if (frmImpresora.ShowDialog() == DialogResult.OK)
                {
                    rptFactura rpt = new rptFactura();
                    //rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);/*Falta Arreglar Por Modificar Planilla*/
                    //rpt.Print();
                    rpt.Print(frmImpresora.url_impresora);
                }
            }
            
        }

        private void facturaChicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFacturaCab obe = (EFacturaCab)viewFactura.GetRow(viewFactura.FocusedRowHandle);
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



            string total = Convertir.ConvertNumeroEnLetras(obe.favc_nmonto_total.ToString());
            var lstdet = new BVentas().listarFacturaDetalle(obe.favc_icod_factura);

            List<EFacturaDet> mlistDetalle = new List<EFacturaDet>();
            int cont = 1;
            foreach (var _BE in lstdet)
            {
                mlistDetalle.Add(_BE);

                string[] partes = partes = _BE.favd_nobservaciones.Split('@');
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
                            EFacturaDet __be = new EFacturaDet();
                            __be.strDesProducto = "    " + partes[i];
                            __be.OrdenItemImprimir = cont + 1;
                            cont++;
                            mlistDetalle.Add(__be);
                        }
                        else
                        {
                            if (partes[i] != "")
                            {
                                EFacturaDet __be = new EFacturaDet();
                                __be.strDesProducto = "    " + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                        }
                    }
                }

            }
            rptFacturaChica rpt = new rptFacturaChica();
            rpt.cargar(obe, mlistDetalle, total, Departamento, Provincia, Distrito);
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteFactura frm = new FrmManteFactura();
            frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
            frm.TipodeFactura = 1;
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();
           
        }

        private void mercaderiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteFactura frm = new FrmManteFactura();
            frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
            frm.TipodeFactura = 2;
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();
            
            //frm.lkpMoneda.EditValue = 4;//DOLARES
            //frm.txtSerie.Text = "001";
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteFactura frm = new FrmManteFactura();
            frm.MiEvento += new FrmManteFactura.DelegadoMensaje(reload);
            frm.TipodeFactura = 2;
            //frm.cbIncluyeIGV.Checked = true;
            frm.SetInsert();
            frm.Show();
            frm.CargarControles();
            var nro = lstFacturas.Max(a => Convert.ToInt32(a.favc_vnumero_factura.Substring(4, 8))) + 1;
            frm.txtNumero.Text = String.Format("{0:00000000}", nro);
        }

        private void actualizarIVAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstFacturas.ForEach(x =>
            {
                List<EFacturaDet> lstFacturaDet = new BVentas().listarFacturaDetalle(x.favc_icod_factura).Where(xx => xx.prdc_icod_producto == 20).ToList();
                lstFacturaDet.ForEach(d =>
                {                    
                    decimal SumaIVAP = 0;                
                    x.favc_npor_imp_igv = 0;
                    x.favc_npor_imp_ivap = 4;
                    x.favc_nmonto_imp = 0;
                    x.favc_bind_arroz = true;
                    SumaIVAP =SumaIVAP + d.favd_nmonto_imp_arroz;
                    x.favc_nmonto_ivap = SumaIVAP;
                    x.favc_nmonto_neto = x.favc_nmonto_total - x.favc_nmonto_ivap;
                    new BVentas().modificarFacturaVenta(x);
                });

            });
        }

        private void actualizarNumeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstFacturas.ForEach(x =>
            {
                if (x.favc_vnumero_factura.Length != 12)
                {
                    string Serie = "";
                    string Correlativo = "";
                    string Cero = "0";
                    string Numero = "";
                    Serie = x.favc_vnumero_factura.Substring(0, 3);
                    Correlativo = x.favc_vnumero_factura.Substring(3, 7);
                    Numero = Cero + Serie + Cero + Correlativo;
                    x.favc_vnumero_factura = Numero;
                }
     


                new BVentas().modificarFacturaNumero(x);

            });
        }



      

    }
}