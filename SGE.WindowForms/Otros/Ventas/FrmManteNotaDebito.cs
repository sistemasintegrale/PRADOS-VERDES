using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Operaciones;
using System.Linq;
using System.Security.Principal;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.Administracion_del_Sistema;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteNotaDebito : DevExpress.XtraEditors.XtraForm
    {        
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        /**/
        public ENotaDebito oBe = new ENotaDebito();
        List<ENotaDebitoDet> lstDetalle = new List<ENotaDebitoDet>();
        List<ENotaDebitoDet> lstDelete = new List<ENotaDebitoDet>();
        /**/
        public List<ENotaDebito> lstCabeceras = new List<ENotaDebito>();//este listado se utiliza para comparar si ya existe el nro. de nc que se esta registrando

        private void FrmManteNotaDebito_Load(object sender, EventArgs e)
        {
            cargar();
            grdND.DataSource = lstDetalle;
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
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = Enabled;
                txtNumero.Enabled = Enabled;
                dteFecha.Enabled = Enabled;
                lkpSituacion.Enabled = Enabled;
                txtIgv.Enabled = Enabled;
                lkpMoneda.Enabled = false;
            }
        }

        public void setValues()
        {
            #region Setting values (cabecera...)
            cbIncluyeIGV.Checked = Convert.ToBoolean(oBe.ndebc_bincluye_igv);
            if (oBe.ndebc_vnumero_debito.Length == 10)
            {
                txtSerie.Text = oBe.ndebc_vnumero_debito.Substring(0, 3);
                txtNumero.Text = oBe.ndebc_vnumero_debito.Substring(3, 7);
            }
            else
            {
                txtSerie.Text = oBe.ndebc_vnumero_debito.Substring(0, 4);
                txtNumero.Text = oBe.ndebc_vnumero_debito.Substring(4, 8);
            }


            dteFecha.EditValue = oBe.ndebc_sfecha_debito;            
            lkpSituacion.EditValue = oBe.ndebc_iid_situacion_debito;
            bteCliente.Tag = oBe.cliec_icod_cliente;
            bteCliente.Text = oBe.strDesCliente;
            txtRuc.Text = oBe.strRuc;
            bteNroDoc.Text = oBe.ndebc_vnumero_documento;
            lkpTipoDoc.EditValue = oBe.tdocc_icod_tipo_doc;
            dteFechaDoc.EditValue = oBe.ndebc_sfecha_documento;            
            lkpMoneda.EditValue = oBe.tablc_iid_tipo_moneda;
            txtIgv.Text = oBe.ndebc_npor_imp_igv.ToString();
            txtMontoNeto.Text = oBe.ndebc_nmonto_neto.ToString();
            txtMontoTotal.Text = oBe.ndebc_nmonto_total.ToString();
            lkpClaseDoc.EditValue = oBe.tdodc_iid_correlativo;
            #endregion
            lstDetalle = new BVentas().listarNotaDebitoClienteDet(oBe.ndebc_icod_debito);
            foreach (var _ve in lstDetalle)
            {
                string[] partes  = _ve.ddebc_vdescripcion.Split('@');
                for (int i = 0; i < partes.Length; i++)
                {
                    if (partes[i] != "")
                    {
                        _ve.strDesProductoPresentar = _ve.strDesProductoPresentar+" "+partes[i];
                    }
                }
            }
            grdND.DataSource = lstDetalle;
            viewND.RefreshData();
        }

        public FrmManteNotaDebito()
        {
            InitializeComponent();
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
            SetCancel();
        }
        private void cargar()
        {
            //BSControls.LoaderLook(lkpClaseDoc, new BGeneral().listarTablaRegistro(62), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpClaseDoc, new BAdministracionSistema().listarTipoDocumentoDetCta(36).Where(ob => ob.tdocd_iid_correlativo != 20).ToList(), "tdocd_descripcion", "tdocd_iid_correlativo", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaSituacionDocumento), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpMoneda, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaTipoMoneda), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            #region load tipo doc
            List<ETipoDocumento> lstTD = new List<ETipoDocumento>();
            for (int i = 0; i < 3; i++)
            {
                ETipoDocumento oBeTD = new ETipoDocumento();
                switch (i)
                {
                    case 0:
                        oBeTD.tdocc_icod_tipo_doc = 0;
                        oBeTD.tdocc_vabreviatura_tipo_doc = "";
                        break;
                    case 1:
                        oBeTD.tdocc_icod_tipo_doc = Parametros.intTipoDocFacturaVenta;
                        oBeTD.tdocc_vabreviatura_tipo_doc = "FAV";
                        break;
                    case 2:
                        oBeTD.tdocc_icod_tipo_doc = Parametros.intTipoDocBoletaVenta;
                        oBeTD.tdocc_vabreviatura_tipo_doc = "BOV";
                        break;
                }
                lstTD.Add(oBeTD); 
            }
            BSControls.LoaderLook(lkpTipoDoc, lstTD,"tdocc_vabreviatura_tipo_doc", "tdocc_icod_tipo_doc", true);
            #endregion
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                setFecha(dteFecha);
                getNroDoc();
               // txtIgv.Text = Parametros.strPorcIGV;
            }  
        }
        private void getNroDoc()
        {
            if (Convert.ToInt32(lkpTipoDoc.EditValue) == 26)
            {
                var lst = new BVentas().getCorrelativoRP(1);
                txtSerie.Text = lst[0].rgpmc_vserieF_nota_debito;
                txtNumero.Text = (Convert.ToInt32(lst[0].rgpmc_icorrelativo_nota_debito) + 1).ToString();
            }
            else if (Convert.ToInt32(lkpTipoDoc.EditValue) == 9)
            {
                var lst = new BVentas().getCorrelativoRP(1);
                txtSerie.Text = lst[0].rgpmc_vserieB_nota_debito;
                txtNumero.Text = (Convert.ToInt32(lst[0].rgpmc_icorrelativo_nota_debito) + 1).ToString();
            }
        }

        private void setFecha(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1).AddMonths(DateTime.Now.Month - 1);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (frmManteNotaCredDetalle frm = new frmManteNotaCredDetalle())
            //{
            //    frm.SetInsert();
            //    frm.lstDetalle = lstDetalle;
            //    frm.txtMoneda.Text = lkpMoneda.Text;
            //    frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        lstDetalle = frm.lstDetalle;
            //        grdNC.DataSource = lstDetalle;
            //        viewNC.RefreshData();
            //        grdNC.Refresh();
            //        viewNC.MoveLast();
                    
            //        setTotales();
            //    }
            //}
        }

        private void nuevoServicio()
        {
            using (frmManteNotaDebitoDetalle frm = new frmManteNotaDebitoDetalle())
            {
                frm.SetInsert();
                frm.lstDetalle = lstDetalle;
                
                frm.txtItem.Text = (lstDetalle.Count == 0) ? "001" : String.Format("{0:000}", lstDetalle.Count + 1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdND.DataSource = lstDetalle;
                    foreach (var _ve in lstDetalle)
                    {
                        string[] partes = _ve.ddebc_vdescripcion.Split('@');
                        for (int i = 0; i < partes.Length; i++)
                        {
                            if (partes[i] != "")
                            {
                                _ve.strDesProductoPresentar = _ve.strDesProductoPresentar + " " + partes[i];
                            }
                        }
                    }
                    viewND.RefreshData();
                    viewND.MoveLast();
                    setTotales();
                }
            }
        }
        
        private void modificarServicio()
        {
            ENotaDebitoDet oBeDet = (ENotaDebitoDet)viewND.GetRow(viewND.FocusedRowHandle);
            if (oBeDet == null)
                return;
            using (frmManteNotaDebitoDetalle frm = new frmManteNotaDebitoDetalle())
            {
                frm.oBe = oBeDet;
                frm.lstDetalle = lstDetalle;
                frm.SetModify();
                frm.setValues();
                
                frm.txtItem.Text = String.Format("{0:000}", oBeDet.ddebc_inro_item);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    grdND.DataSource = lstDetalle;
                    foreach (var _ve in lstDetalle)
                    {
                        string[] partes = _ve.ddebc_vdescripcion.Split('@');
                        for (int i = 0; i < partes.Length; i++)
                        {
                            if (partes[i] != "")
                            {
                                _ve.strDesProductoPresentar = _ve.strDesProductoPresentar + " " + partes[i];
                            }
                        }
                    }
                    viewND.RefreshData();
                    viewND.MoveLast();
                    setTotales();
                }
            }
        }      

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;      
            try 
            {
                if (txtSerie.Text == "")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Nro. de Serie de la N/C");
                }              

                if (Convert.ToInt32(txtNumero.Text) == 0)
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Nro. de la N/C");
                }

                if(Status == BSMaintenanceStatus.CreateNew)
                    if (lstCabeceras.Where(x => x.ndebc_vnumero_debito == String.Format("{0}{1}", txtSerie.Text, txtNumero.Text)).ToList().Count > 0)
                    {
                        oBase = txtNumero;
                        throw new ArgumentException("El Nro. de N/C, ya existe en los registros!");
                    }

                if (Convert.ToDateTime(dteFecha.Text).Year != Parametros.intEjercicio)
                {
                    oBase = dteFecha;
                    throw new ArgumentException("La fecha seleccionada esta fuera del rango del ejercicio");
                }

                if (Convert.ToInt32(bteCliente.Tag) == 0)
                {
                    oBase = bteCliente;
                    throw new ArgumentException("Seleccione el cliente");
                }

                if (lstDetalle.Count == 0)
                {
                    throw new ArgumentException("No ha ingresado ningun ítem en el detalle de la N/C!!!");
                }

                oBe.ndebc_vnumero_debito = String.Format("{0}{1}", txtSerie.Text, txtNumero.Text);
                oBe.ndebc_sfecha_debito = Convert.ToDateTime(dteFecha.EditValue);                
                oBe.ndebc_ianio = Parametros.intEjercicio;
                oBe.cliec_icod_cliente = Convert.ToInt32(bteCliente.Tag);
                oBe.tdocc_icod_tipo_doc = Convert.ToInt32(lkpTipoDoc.EditValue);
                //oBe.tdodc_iid_correlativo = 54;//nota de credito devolucion mercaderia
                oBe.tdodc_iid_correlativo = Convert.ToInt32(lkpClaseDoc.EditValue);//nota de credito devolucion mercaderia
                oBe.ndebc_vnumero_documento = bteNroDoc.Text;
                oBe.ndebc_sfecha_documento = Convert.ToDateTime(dteFechaDoc.EditValue);
                oBe.tablc_iid_tipo_moneda = Convert.ToInt32(lkpMoneda.EditValue);
                oBe.ndebc_nmonto_neto = Convert.ToDecimal(txtMontoNeto.Text);
                oBe.ndebc_npor_imp_igv = Convert.ToDecimal(txtIgv.Text);
                oBe.ndebc_nmonto_total = Convert.ToDecimal(txtMontoTotal.Text);
                oBe.ndebc_iid_situacion_debito = Convert.ToInt32(lkpSituacion.EditValue);                                
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;
                oBe.ndebc_bincluye_igv = cbIncluyeIGV.Checked;
                oBe.ndebc_tipo_nota_debito = 2;//NOTA DE CREDITO NO COMERCIAL

                oBe.nroDocumentoEmisior = Valores.strRUC;
                oBe.nombreLegalEmisor = "MJCGROUP SAC";
                oBe.nombreComercialEmisor = Valores.strNombreEmpresa;
                oBe.direccionEmisor = Valores.strDireccionFiscal;

                oBe.nroDocumentoReceptor = txtRuc.Text;
                oBe.nombreLegalReceptor = bteCliente.Text;
                //oBe.ncrec_iclase_doc = Convert.ToInt32(lkpClaseDoc.EditValue);
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.ndebc_icod_debito = new BVentas().insertarNotaDebitoClienteCab(oBe, lstDetalle);
                }
                else
                {
                    new BVentas().modificarNotaDebitoClienteCab(oBe, lstDetalle, lstDelete);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    MiEvento(oBe.ndebc_icod_debito);
                    Close();
                }
            }
        }

     
        private void bteCliente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void setTotales()
        {
            if (lstDetalle.Count > 0)
            {
                txtMontoTotal.Text = lstDetalle.Sum(x => x.ddebc_nmonto_total).ToString();
                txtMontoNeto.Text = Math.Round((Convert.ToDecimal(txtMontoTotal.Text)), 2).ToString();
                
            }
            else
            {
                txtMontoNeto.Text = "0.00";                
                txtMontoTotal.Text = "0.00";
            }

            if (lstDetalle.Count > 0)
            {
                //if (cbIncluyeIGV.Checked)
                //{
                //    decimal total = lstDetalle.Sum(x => x.ddebc_nmonto_total);
                //    decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
                //    decimal neto = Math.Round(total / Convert.ToDecimal("1." + txtIgv.Text.Replace(".", "")), 2);
                //    txtMontoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                //    txtMontoTotal.Text = Convertir.RedondearNumero(total).ToString();
                //    //txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
                //}
                //else
                //{
                //    decimal neto = lstDetalle.Sum(x => x.ddebc_nmonto_total);
                //    decimal igv = Convert.ToDecimal(String.Format("1.{0}", Parametros.strPorcIGV.Replace(".", ""), 2));
                //    decimal total = Math.Round(neto * Convert.ToDecimal("1." + txtIgv.Text.Replace(".", "")), 2);
                //    txtMontoNeto.Text = Convertir.RedondearNumero(neto).ToString();
                //    txtMontoTotal.Text = Convertir.RedondearNumero(total).ToString();
                //    //txtMontoIGV.Text = (Convert.ToDecimal(txtMontoTotal.Text) - Convert.ToDecimal(txtMontoNeto.Text)).ToString();
                //}
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaDebitoDet obe = (ENotaDebitoDet)viewND.GetRow(viewND.FocusedRowHandle);
            if (obe == null)
                return;
          
                modificarServicio();           
        }

       

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaDebitoDet obe = (ENotaDebitoDet)viewND.GetRow(viewND.FocusedRowHandle);
            if (obe == null)
                return;
            lstDelete.Add(obe);
            lstDetalle.Remove(obe);
            renumerar();
            grdND.DataSource = lstDetalle;

            viewND.RefreshData();
            setTotales();
        }

        private void renumerar()
        {
            Int16 intCont = 0;
            lstDetalle.ForEach(x => 
            {
                intCont += 1;
                x.ddebc_inro_item = intCont;
            });
        }

        private void nuevoServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoServicio();
        }

        private void bteNroDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarDocumentoReferencia();
        }

        private void bteCliente_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarCliente();
        }

        private void listarCliente()
        {
            try
            {
                using (FrmListarCliente frm = new FrmListarCliente())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bteCliente.Tag = frm._Be.cliec_icod_cliente;
                        bteCliente.Text = frm._Be.cliec_vnombre_cliente;
                        txtRuc.Text = frm._Be.cliec_cruc;
                        /**/
                        oBe.cliec_icod_cliente = frm._Be.cliec_icod_cliente;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listarDocumentoReferencia()
        {
            try
            {
                if (Convert.ToInt32(bteCliente.Tag) == 0)
                    throw new ArgumentException("Seleccione el cliente");

                using (FrmListarDocXCobrar frm = new FrmListarDocXCobrar())
                {
                    frm.intCliente = Convert.ToInt32(bteCliente.Tag);
                    frm.flagBovFav = true;
                    frm.intTipoDoc = Convert.ToInt32(lkpTipoDoc.EditValue);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lkpTipoDoc.EditValue = frm._Be.tdocc_icod_tipo_doc;
                        bteNroDoc.Text = frm._Be.doxcc_vnumero_doc;
                        bteNroDoc.Tag = frm._Be.doxcc_icod_correlativo;
                        dteFechaDoc.EditValue = frm._Be.doxcc_sfecha_doc;
                        oBe.tdodc_iid_correlativo = frm._Be.tdodc_iid_correlativo;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbIncluyeIGV_CheckedChanged(object sender, EventArgs e)
        {
            setTotales();
        }

        private void txtSerie_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bteClaseDocumento_Click(object sender, EventArgs e)
        {
          
        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            getNroDoc();
        }
    }    
}