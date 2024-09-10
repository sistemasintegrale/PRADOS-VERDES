using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using SGE.WindowForms.Otros.bVentas;
using System.Security.Principal;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Ventas.Reporte;
using DevExpress.XtraReports.UI;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frm11NotaCreditoNoComercial : DevExpress.XtraEditors.XtraForm
    {
        
        List<ENotaCredito> lstNotaCredito = new List<ENotaCredito>();
        public frm11NotaCreditoNoComercial()
        {
            InitializeComponent();
        }

      

        private void cargar()
        {
           

            lstNotaCredito = new BVentas().listarNotaCreditoClienteCab(Parametros.intEjercicio).Where(OB=>OB.ncrec_tipo_nota_credito==2).ToList();//2 NO COMERCIAL
            grdNotaCredito.DataSource = lstNotaCredito;
            
        }       

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaCredito.FindIndex(x => x.ncrec_icod_credito == intIcod);
            viewNotaCredito.FocusedRowHandle = index;
            viewNotaCredito.Focus();
        }

        private void nuevo()
        {
            decimal nmonto_tipoCambio = new BContabilidad().getTipoCambioPorFecha(DateTime.Now);
            if (nmonto_tipoCambio != 0)
            {
                FrmManteNotaCreditoNoComercial frm = new FrmManteNotaCreditoNoComercial();
                frm.MiEvento += new FrmManteNotaCreditoNoComercial.DelegadoMensaje(reload);
                frm.lstCabeceras = lstNotaCredito;
                frm.getNroDoc();
                //if (lstNotaCredito.Count > 0)
                //    frm.txtNumero.Text = String.Format("{0:00000000}", lstNotaCredito.Max(x => Convert.ToInt32(x.ncrec_vnumero_credito.Substring(4, 8)) + 1));
                //else
                //    frm.txtNumero.Text = "00000001";
                frm.SetInsert();
                frm.Show();
                frm.lkpMoneda.EditValue = 3;//DOLARES
               
                
            }
            else
            {
                XtraMessageBox.Show("No existe el Tipo de Cambio a la Fecha, Ingrese el Tipo Cambio para seguir con este proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser modificada, su situación es {0}", oBe.strSituacion));
                List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParametro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "07").ToList();
                if (lstCab.Count > 0)
                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La N/C no puede ser modificada ,ya Fué Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                FrmManteNotaCreditoNoComercial frm = new FrmManteNotaCreditoNoComercial();
                frm.MiEvento += new FrmManteNotaCreditoNoComercial.DelegadoMensaje(reload);
                frm.oBe = oBe;
                frm.SetModify();
                frm.Show();
                frm.setValues();               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void eliminar()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            int index = viewNotaCredito.FocusedRowHandle;
            try
            {

                List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == oBe.doc_icod_documento && x.tipoDocumento == "07").ToList();
                if (lstCab.Count > 0)

                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                      
                        new BVentas().eliminarNotaCreditoNoComercialVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        new BVentas().eliminarNotaCreditoNoComercialElectronica(lstCab[0].IdCabecera);


                    }
                }


                if (oBe.ncrec_iid_situacion_credito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser eliminada, su situación es {0}", oBe.strSituacion));

                if (XtraMessageBox.Show(String.Format("¿Está seguro que desea eliminar la NC {0}?",oBe.ncrec_vnumero_credito), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarNotaCreditoClienteCab(oBe);
                    /**/
                    reload(oBe.ncrec_icod_credito);
                    /***********************************************/
                    if (lstNotaCredito.Count >= index + 1)
                        viewNotaCredito.FocusedRowHandle = index;
                    else
                        viewNotaCredito.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      
        }

        private void ver()
        {
            ENotaCredito oBe = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                //frmManteFacturaCompra frm = new frmManteFacturaCompra();
                //frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
                //frm.Obe = obe;
                //frm.SetCancel();
                //frm.Show();
                //frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void filtrar()
        {
            grdNotaCredito.DataSource = lstNotaCredito.Where(x => x.ncrec_vnumero_credito.Contains(textEdit1.Text) &&
                x.strDesCliente.Contains(textEdit2.Text.ToUpper())).ToList();
        }           

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            ver();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void frm11NotaCredito_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCredito ObeFC = (ENotaCredito)viewNotaCredito.GetRow(viewNotaCredito.FocusedRowHandle);
            List<EFacturaVentaElectronica> lstFE = new List<EFacturaVentaElectronica>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstFE = new BVentas().listarfacturaVentaElectronica(lstParametro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == ObeFC.ncrec_icod_credito && x.tipoDocumento == "07").ToList();
            Obe = lstFE.FirstOrDefault();

            mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);

            rptNotaCreditoElectronico rptNotaCredito = new rptNotaCreditoElectronico();

            if (Obe.tipoDocumento == "07")
            {

                rptNotaCredito.cargar(Obe, mlisdet);
                rptNotaCredito.ShowPreview();
            }
        }
       
    }
}