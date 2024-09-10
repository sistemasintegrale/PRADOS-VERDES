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


namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frm11NotaDebito : DevExpress.XtraEditors.XtraForm
    {
        List<ENotaDebito> lstNotaDebito = new List<ENotaDebito>();
        public frm11NotaDebito()
        {
            InitializeComponent();
        }

      

        private void cargar()
        {
            
            lstNotaDebito = new BVentas().listarNotaDebitoClienteCab(Parametros.intEjercicio).Where(OB=>OB.ndebc_tipo_nota_debito==2).ToList();//2 NO COMERCIAL

            grdNotaDebito.DataSource = lstNotaDebito;            
        }       

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaDebito.FindIndex(x => x.ndebc_icod_debito == intIcod);
            viewNotaDebito.FocusedRowHandle = index;
            viewNotaDebito.Focus();
        }

        private void nuevo()
        {
            decimal nmonto_tipoCambio = new BContabilidad().getTipoCambioPorFecha(DateTime.Now);
            if (nmonto_tipoCambio != 0)
            {
                FrmManteNotaDebito frm = new FrmManteNotaDebito();
                frm.MiEvento += new FrmManteNotaDebito.DelegadoMensaje(reload);
                frm.lstCabeceras = lstNotaDebito;
                frm.SetInsert();
                frm.Show();
                frm.lkpMoneda.EditValue = 4;//DOLARES
                frm.txtSerie.Text = "001";
            }
            else
            {
                XtraMessageBox.Show("No existe el Tipo de Cambio a la Fecha, Ingrese el Tipo Cambio para seguir con este proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            ENotaDebito oBe = (ENotaDebito)viewNotaDebito.GetRow(viewNotaDebito.FocusedRowHandle);
            if (oBe == null)
                return;
            try
            {
                if (oBe.ndebc_iid_situacion_debito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser modificada, su situación es {0}", oBe.strSituacion));

                FrmManteNotaDebito frm = new FrmManteNotaDebito();
                frm.MiEvento += new FrmManteNotaDebito.DelegadoMensaje(reload);
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
            ENotaDebito oBe = (ENotaDebito)viewNotaDebito.GetRow(viewNotaDebito.FocusedRowHandle);
            if (oBe == null)
                return;
            int index = viewNotaDebito.FocusedRowHandle;
            try
            {

                List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
                List<EFacturaVentaElectronica> lstCab = new BVentas().listarfacturaVentaElectronica(lstParamatro[0].pm_sfecha_inicio).Where(x => x.doc_icod_documento == oBe.doc_icod_documento).ToList();
                if (lstCab.Count > 0)

                {
                    if (lstCab[0].EstadoFacturacion != 4)
                    {
                        XtraMessageBox.Show("La Factura de Venta fue Enviada a SUNAT", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {

                        new BVentas().eliminarNotaDebitoVentaElectronicaDetalle(lstCab[0].IdCabecera);
                        new BVentas().eliminarNotaDebitolElectronica(lstCab[0].IdCabecera);


                    }
                }



                if (oBe.ndebc_iid_situacion_debito != Parametros.intSitDocCobrarGenerado)
                    throw new ArgumentException(String.Format("La N/C no puede ser eliminada, su situación es {0}", oBe.strSituacion));

                if (XtraMessageBox.Show(String.Format("¿Está seguro que desea eliminar la NC {0}?",oBe.ndebc_vnumero_debito), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    oBe.intUsuario = Valores.intUsuario;
                    oBe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarNotaDebitoClienteCab(oBe);
                    /**/
                    reload(oBe.ndebc_icod_debito);
                    /***********************************************/
                    if (lstNotaDebito.Count >= index + 1)
                        viewNotaDebito.FocusedRowHandle = index;
                    else
                        viewNotaDebito.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      
        }

        private void ver()
        {
            ENotaDebito oBe = (ENotaDebito)viewNotaDebito.GetRow(viewNotaDebito.FocusedRowHandle);
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
            grdNotaDebito.DataSource = lstNotaDebito.Where(x => x.ndebc_vnumero_debito.Contains(textEdit1.Text) &&
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
            List<ENotaDebitoDet> lstDetalle = new List<ENotaDebitoDet>();
            ENotaDebito oBe = (ENotaDebito)viewNotaDebito.GetRow(viewNotaDebito.FocusedRowHandle);
            if (oBe == null)
                return;

            lstDetalle = new BVentas().listarNotaDebitoClienteDet(oBe.ndebc_icod_debito);

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
            lUbicacion.Where(oB => oB.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion).ToList().ForEach(oB =>
            {
                tipo = oB.tablc_iid_tipo_ubicacion;
                padre = oB.ubicc_icod_ubicacion_padre;
            });
            switch (tipo)
            {
                case 4:
                    Dios = oBe.ubicc_icod_ubicacion;
                    break;
                case 3:
                    foreach (var _BBE in lUbicacion)
                    {
                        if (_BBE.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion)
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
                        if (_BBE.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion)
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
                        if (_BBE.ubicc_icod_ubicacion == oBe.ubicc_icod_ubicacion)
                        {
                            Distrito = _BBE.ubicc_vnombre_ubicacion;
                        }
                    }
                    break;

            }

            List<ENotaDebitoDet> lstDetalleImprimir = new List<ENotaDebitoDet>();

            foreach (var _ve in lstDetalle)
            {
                string[] partes = partes = _ve.ddebc_vdescripcion.Split('@');
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] != "")
                    {
                        ENotaDebitoDet _ce = new ENotaDebitoDet();
                        _ce.strDesProducto = partes[i];
                        if (i == 0)
                            _ce.ddebc_vmonto_item = _ve.ddebc_nmonto_item.ToString();
                        lstDetalleImprimir.Add(_ce);
                    }
                }
            }

            string total = Convertir.ConvertNumeroEnLetras(oBe.ndebc_nmonto_total.ToString());

            rptNotaDebito rpt = new rptNotaDebito();
           rpt.cargar(oBe, lstDetalleImprimir, total, Departamento, Provincia, Distrito);
        }
       
    }
}