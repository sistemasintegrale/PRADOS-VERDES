using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Contabilidad;


namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmManteProductoAntiguo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteProductoAntiguo));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EProducto Obe = new EProducto();
        public EFamiliaCab obeF = new EFamiliaCab();
        public List<EProducto> lstProductos = new List<EProducto>();
        List<ECategoriaFamilia> lstCategoriaFamilia = new List<ECategoriaFamilia>();
        List<EFamiliaCab> lstFamilia = new List<EFamiliaCab>();
        //List<EFamiliaDet> lstSubFamilia = new List<EFamiliaDet>();
        public int CodCategoria = 0;
        public int CodLinea = 0;
        public int CodSubLenea = 0;
        public string Categoria = "";
        public string Linea = "";
        public string SubLenea = "";
        public List<ECodigoBarra> lstCodigoBarra = new List<ECodigoBarra>();
        public List<ECodigoBarra> lstDeleteCodigoBarra = new List<ECodigoBarra>();
        public int Indicador = 0 ;
        public int prdc_icod_producto = 0;
        public frmManteProductoAntiguo()
        {
            InitializeComponent();
        }
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
            //bteFamilia.Enabled = !Enabled;
            txtDesLarga.Enabled = !Enabled;
           /// txtDesCorta.Enabled = !Enabled;
            lkpUnidadMedida.Enabled = !Enabled;
            lkpUnidadMedidaVenta.Enabled = !Enabled;
            txtStockMin.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            rbActivo.Enabled = !Enabled;
            rbInactivo.Enabled = !Enabled;
            txtPesoUnitario.Enabled = !Enabled;
           /// chkIVAP.Enabled = !Enabled;
            ///chkISC.Enabled = !Enabled;
            txtFactConversion.Enabled = !Enabled;
            chkIGV.Enabled = !Enabled;
            //txtPorcentajeIVAP.Enabled = !Enabled;
            //txtPorcentajeISC.Enabled = !Enabled;

            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                bteFamilia.Enabled = Enabled;
               /// bteSubFamilia.Enabled = Enabled;

            }

        }
        public void setValues()
        {
            bteCategoria.Tag = Obe.catf_icod_categoria;
            bteFamilia.Tag = Obe.famic_icod_familia;
            ///bteSubFamilia.Tag = Obe.famid_icod_familia;
            bteCategoria.Text = Obe.strDesCategoriaFamilia;
            bteFamilia.Text = Obe.strDesFamilia;
           /// bteSubFamilia.Text = Obe.strDesSubFamilia;
            CodCategoria = Obe.CodCategoriaFamilia;
            CodLinea = Obe.CodFamilia;
            //CodSubLenea = Obe.CodSubFamilia;
            /*--------------------------------------------------------------------*/
            if (Obe.prdc_vcode_producto == "")
            {
                bteFamilia.Enabled = true;
            }
            else
            {
                txtCorrelativo.Text = Obe.prdc_vcode_producto.Substring((Obe.prdc_vcode_producto.Length - 3));
            }
            //txtCorrelativo.Text = Obe.prdc_vcode_producto.ToString();
            rbActivo.Checked = Obe.prdc_isituacion;
            rbInactivo.Checked = !Obe.prdc_isituacion;
            txtDesLarga.Text = Obe.prdc_vdescripcion_larga;
            ///txtDesCorta.Text = Obe.prdc_vdescripcion_corta;
            lkpUnidadMedida.EditValue = Obe.unidc_icod_unidad_medida;
            lkpUnidadMedidaVenta.EditValue = Obe.unidc_icod_unidad_medida_venta;
            LkpContable.EditValue = Obe.clasc_icod_clasificacion;
            txtStockMin.Text = Obe.prdc_stock_minimo.ToString();
            txtPesoUnitario.Text = Obe.prdc_npeso_unitario.ToString();
            ///chkIVAP.Checked = Convert.ToBoolean(Obe.AfectoIVAP);
            ///chkISC.Checked = Convert.ToBoolean(Obe.prdc_afecto_isc);
            ///txtPorcentajeIVAP.Text = Obe.PorcentajeIVAP.ToString();
            ///txtPorcentajeISC.Text = Obe.prdc_nporcentaje_isc.ToString();
            txtFactConversion.Text = Obe.prdc_ifact_conversion.ToString();
            lstCodigoBarra = new BAlmacen().listarCodigoBarra(Obe.prdc_icod_producto);
            chkIGV.Checked = Convert.ToBoolean(Obe.prdc_afecto_igv);
            txtPorcentajeIGV.Text = Obe.prdc_nporcentaje_igv.ToString();

        
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            EListaPrecio ObeListaPrecio = new EListaPrecio();
            try
            {
                if (Convert.ToInt32(bteFamilia.Tag) == 0)
                {
                    oBase = bteFamilia;
                    throw new ArgumentException("Código de Familia inválido");
                }

               ///if (Convert.ToInt32(bteSubFamilia.Tag) == 0)
                //{
                //    oBase = bteSubFamilia;
                //    throw new ArgumentException("Código de Sub-Familia inválido");
                ///}

                int? nullVall = null;
                Obe.catf_icod_categoria = Convert.ToInt32(bteCategoria.Tag);
                Obe.famic_icod_familia = Convert.ToInt32(bteFamilia.Tag);
                //Obe.famid_icod_familia = Convert.ToInt32(bteSubFamilia.Tag);

                Categoria = String.Format("{0:000}", CodCategoria);
                Linea = String.Format("{0:000}", CodLinea);
                //SubLenea = String.Format("{0:000}", CodSubLenea);

                Obe.prdc_isituacion = rbActivo.Checked;
                Obe.prdc_vcode_producto = String.Format("{0}-{1}-{2}", Categoria, Linea, txtCorrelativo.Text.Trim());
                Obe.prdc_vdescripcion_larga = txtDesLarga.Text;
                ///Obe.prdc_vdescripcion_corta = txtDesCorta.Text;
                Obe.prdc_stock_minimo = Convert.ToDecimal(txtStockMin.Text);
                Obe.prdc_npeso_unitario = Convert.ToDecimal(txtPesoUnitario.Text);
                ///Obe.prdc_afecto_ivap = chkIVAP.Checked;
                ///Obe.prdc_afecto_isc = chkISC.Checked;
                ///Obe.prdc_nporcentaje_ivap = Convert.ToDecimal(txtPorcentajeIVAP.Text);
                ///Obe.prdc_nporcentaje_isc = Convert.ToDecimal(txtPorcentajeISC.Text);
                Obe.prdc_ifact_conversion = Convert.ToInt32(txtFactConversion.Text);
                Obe.clasc_icod_clasificacion = Convert.ToInt32(LkpContable.EditValue);
                Obe.prdc_afecto_igv = chkIGV.Checked;
                Obe.prdc_nporcentaje_igv = Convert.ToDecimal(txtPorcentajeIGV.Text);

                Obe.prdc_flag_estado = true;

                Obe.unidc_icod_unidad_medida_venta = Convert.ToInt32(lkpUnidadMedidaVenta.EditValue);

                if (Convert.ToInt32(lkpUnidadMedida.EditValue) == 0)
                {
                    Obe.unidc_icod_unidad_medida = null;
                }
                else
                    Obe.unidc_icod_unidad_medida = Convert.ToInt32(lkpUnidadMedida.EditValue);


                Obe.prdc_stock_minimo = Convert.ToDecimal(txtStockMin.Text);
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    ObeListaPrecio.prdc_icod_producto = prdc_icod_producto;
                    ObeListaPrecio.tablc_iid_tipo_moneda = 3;
                    ObeListaPrecio.lprecc_nmonto_unitario = 0;
                    ObeListaPrecio.lprecc_indicador_rango = false;
                    ObeListaPrecio.intUsuario = Valores.intUsuario;
                    ObeListaPrecio.strPc = WindowsIdentity.GetCurrent().Name;

                    Obe.prdc_icod_producto = new BAlmacen().insertarProducto(Obe, lstCodigoBarra, ObeListaPrecio);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BAlmacen().modificarProducto(Obe, lstCodigoBarra, lstDeleteCodigoBarra);
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorIcon = ((System.Drawing.Image)(resources.GetObject("Warning")));
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    if (Obe.prdc_icod_producto > 0)
                        this.MiEvento(Obe.prdc_icod_producto);
                    this.Close();
                }
            }
        }
        //-----


        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        /*                 
         * /*/

        private void frmManteProducto_Load(object sender, EventArgs e)
        {

            /*--------------------------------------------------------*/
            lstCategoriaFamilia = new BAlmacen().listarCategoriaFamilia();
            if (Status != BSMaintenanceStatus.CreateNew)
                lstFamilia = new BAlmacen().listarFamiliaCab(Convert.ToInt32(Obe.catf_icod_categoria));
            if (Status != BSMaintenanceStatus.CreateNew)
             ///   lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(Obe.famic_icod_familia));

            /*--------------------------------------------------------*/
            if (lstCategoriaFamilia.Count == 0)
            {
                XtraMessageBox.Show("Debe registrar Linea y Sub Linea - Linea para continuar con el registro de productos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            /*--------------------------------------------------------*/
            /*-----------------------------------------------------------------------------------------------------------------------------------------------*/



            BSControls.LoaderLook(lkpUnidadMedida, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            BSControls.LoaderLook(lkpUnidadMedidaVenta, new BAlmacen().listarUnidadMedida(), "unidc_vdescripcion", "unidc_icod_unidad_medida", true);
            BSControls.LoaderLook(LkpContable, new BContabilidad().listarClasificacionProducto(), "clasc_vdescripcion", "clasc_icod_clasificacion", true);
            rbActivo.Checked = true;


        }
        private void generarCodigo()
        {
            string strCodigo = "0";

            if (Convert.ToInt32(bteFamilia.Tag) != 0  && Convert.ToInt32(bteCategoria.Tag) != 0)
            strCodigo = String.Format("{0:0000}", new BAlmacen().getCorrelativoProducto(Convert.ToInt32(bteCategoria.Tag), Convert.ToInt32(bteFamilia.Tag)));
            txtCorrelativo.Text = strCodigo;
        }
        private void generarDesLarga()
        {
             string strDesLarga = "";
            strDesLarga = String.Format("{0} {1}",
            (Convert.ToInt32(bteFamilia.Tag) == 0) ? "" : lstFamilia.Where(x => x.famic_icod_familia == Convert.ToInt32(bteFamilia.Tag)).ToList()[0].famic_vdescripcion);
             //(Convert.ToInt32(bteSubFamilia.Tag) == 0) ? "" : lstSubFamilia.Where(x => x.famid_icod_familia == Convert.ToInt32(bteSubFamilia.Tag)).ToList()[0].famid_vdescripcion);
        }


        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           SetSave();
        }
        private void bteCategoria_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCategoriaFamilia frm = new frmListarCategoriaFamilia())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCategoria.Tag = frm._Be.catf_icod_categoria;
                    bteCategoria.Text = frm._Be.catf_vdescripcion;
                    CodCategoria = frm._Be.catf_iid_categoria;
                    lstFamilia = new BAlmacen().listarFamiliaCab(Convert.ToInt32(bteCategoria.Tag));
                    bteFamilia.Enabled = true;
                    generarCodigo();
                    lblMsj.Visible = false;
                    bteFamilia.Focus();


                }

            }
        }
        private void bteFamilia_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarFamilia frm = new frmListarFamilia())
            {
                frm.intCategoria = Convert.ToInt32(bteCategoria.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteFamilia.Tag = frm._Be.famic_icod_familia;
                    bteFamilia.Text = frm._Be.famic_vdescripcion;
                    CodLinea = frm._Be.famic_iid_familia;
                 //   lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag));
                   /// bteSubFamilia.Enabled = true;
                    generarCodigo();
                    lblMsj.Visible = false;
                    bteCategoria.Enabled = false;
                   /// bteSubFamilia.Focus();


                }

            }
        }

        private void bteSubFamilia_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarSubFamilia frm = new frmListarSubFamilia())
            {
                frm.intFamilia = Convert.ToInt32(bteFamilia.Tag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ///bteSubFamilia.Tag = frm._Be.famid_icod_familia;
                    ///bteSubFamilia.Text = frm._Be.famid_vdescripcion;
                    CodSubLenea = frm._Be.famid_iid_familia;
                    generarCodigo();
                    lblMsj.Visible = false;
                    bteFamilia.Enabled = false;
                }

            }
        }
        private void bteCategoria_EditValueChanged(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            var lstAux = lstCategoriaFamilia.Where(x => x.catf_vdescripcion.Trim() == bteCategoria.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                bteFamilia.Tag = lstAux[0].catf_icod_categoria;
                lstFamilia = new BAlmacen().listarFamiliaCab(Convert.ToInt32(bteCategoria.Tag));
                bteFamilia.Enabled = true;
                generarCodigo();
                lblMsj.Visible = false;


            }
            else
            {
                lblMsj.Visible = true;
                bteFamilia.Enabled = false;
                bteCategoria.Tag = null;
                lstFamilia.Clear();
                generarCodigo();


            }
        }
        private void bteFamilia_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Status != BSMaintenanceStatus.CreateNew)
                return;
            if (Convert.ToInt32(bteCategoria.Tag) == 0)
                return;
            var lstAux = lstFamilia.Where(x => x.famic_vdescripcion.Trim() == bteFamilia.Text.Trim()).ToList();
            if (lstAux.Count > 0)
            {
                bteFamilia.Tag = lstAux[0].famic_icod_familia;
               // lstSubFamilia = new BAlmacen().listarFamiliaDet(Convert.ToInt32(bteFamilia.Tag));
              ///  bteSubFamilia.Enabled = true;
                generarCodigo();
                lblMsj.Visible = false;
                bteCategoria.Enabled = false;

            }
            else
            {
                lblMsj.Visible = true;
                bteFamilia.Tag = null;
                generarCodigo();
                bteCategoria.Enabled = false;

            }
            if (bteFamilia.Text == "")
            {
                bteCategoria.Enabled = true;
                lblMsj.Visible = false;
            }
        }

        //private void bteSubFamilia_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    if (Status != BSMaintenanceStatus.CreateNew)
        //        return;
        //    if (Convert.ToInt32(bteFamilia.Tag) == 0)
        //        return;

        //    //var lstAux = lstSubFamilia.Where(x => x.famid_vdescripcion.Trim() == bteSubFamilia.Text.Trim()).ToList();
        //    //if (lstAux.Count > 0)
        //    //{
        //        //bteSubFamilia.Tag = lstAux[0].famid_icod_familia;
        //        //generarCodigo();
        //        //lblMsj.Visible = false;
        //        //bteFamilia.Enabled = false;
        //    //}
        //    else
        //    {
        //        lblMsj.Visible = true;
        //       // bteSubFamilia.Tag = null;
        //        generarCodigo();
        //        bteFamilia.Enabled = false;
        //    }
        //    //if (bteSubFamilia.Text == "")
        //    //{
        //    //    bteFamilia.Enabled = true;
        //    //    lblMsj.Visible = false;
        //    //}
        //}

        private void chkIVAP_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkIVAP.Checked == true)
            //{
            //    txtPorcentajeIVAP.Enabled = true;
            //    chkISC.Checked = false;                        mofificado
            //    chkIGV.Checked = false;
            //}
            //else
            //{
            //    txtPorcentajeIVAP.Enabled = false;
            //}
        }

        private void chkISC_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkISC.Checked == true)
            //{
            //    txtPorcentajeISC.Enabled = true;
            //    chkIVAP.Checked = false;              modificado
            //    chkIGV.Checked = false;
            //}
            //else
            //{
            //    txtPorcentajeISC.Enabled = false;
            //}
        }


        //private void btnVerDoc_Click(object sender, EventArgs e)
        //{
        //    using (frmListarCodigoBarra frm = new frmListarCodigoBarra())
        //    {
        //        frm.lstCodigoBarra = lstCodigoBarra;
        //        frm.lstDeleteCodigoBarra = lstDeleteCodigoBarra;
        //        frm.prdc_icod_producto = Obe.prdc_icod_producto;
        //        frm.Indicador = Indicador;
        //        frm.returnSeleccion();
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            lstCodigoBarra = frm.lstCodigoBarra;
        //            lstDeleteCodigoBarra = frm.lstDeleteCodigoBarra;

        //        }
        //    }
        //}

        private void chkIGV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIGV.Checked == true)
            {
                txtPorcentajeIGV.Enabled = true;
                //chkIVAP.Checked = false;
                ///chkISC.Checked = false;
            }
            else
            {
                txtPorcentajeIGV.Enabled = false;
            }
        }

        private void txtCorrelativo_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}