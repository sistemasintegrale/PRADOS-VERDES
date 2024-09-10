using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
//using SGE.BusinessLogic.Ventas;
//using SGI.BusinessLogic.Contabilidad;
using SGE.WindowForms.Modules;
using System.Linq;
using SGE.BusinessLogic;
namespace SGI.WindowsForm.Otros.Ventas
{
    public partial class FrmManteVendedor : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteVendedor));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public bool? exportador;
        public bool clave = false;
        public FrmManteVendedor()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();

        }

        public List<EVendedor> oDetail;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EVendedor oBe = new EVendedor();


        public int Correlative = 0;
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
            txtIdVendedor.Enabled = !Enabled;
            txtNombre.Enabled = !Enabled;
            txtDireccion.Enabled = !Enabled;
            txtTelefono.Enabled = !Enabled;
            txtDNI.Enabled = !Enabled;
            txtClave.Enabled = !Enabled;
            LkpTipoVenta.Enabled = !Enabled;
            LkpSituacion.Enabled = !Enabled;
            lkpPuntoVenta.Enabled = !Enabled;
            txtCorreo.Enabled = !Enabled;
            lkpZona.Enabled = !Enabled;
            if (clave)
            {
                lblClave.Visible = true;
                txtClave.Visible = true;
            }

            txtIdVendedor.Focus();
        }

        private void clearControl()
        {
            txtIdVendedor.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtDNI.Text = "";

            //txtClave.Text = "";
            //LkpTipoVenta.EditValue = null;
            LkpSituacion.EditValue = null;
            /* lkpPuntoVenta.EditValue = null*/
            ;
        }

        public void setValues()
        {
            Correlative = oBe.vendc_icod_vendedor;
            txtIdVendedor.Text = oBe.vendc_iid_vendedor;
            LkpSituacion.EditValue = oBe.tablc_iid_situacion_vendedor;
            txtNombre.Text = oBe.vendc_vnombre_vendedor;
            txtDireccion.Text = oBe.vendc_vdireccion;
            txtTelefono.Text = oBe.vendc_vnumero_telefono;
            txtDNI.Text = oBe.vendc_cnumero_dni;
            txtCorreo.Text = oBe.vendc_vcorreo;
            lkpZona.EditValue = oBe.zonc_icod_zona;
            txtCodigo.Text = oBe.vendc_vcod_vendedor;
            //LkpTipoVenta.EditValue = oBe.vendc_tipo_vendedor;
            //txtClave.Text = oBe.vendc_vpassword_vendedor;
        }

        private void FrmManteVendedor_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            BSControls.LoaderLook(lkpZona, new BVentas().listarZona(), "zonc_vdescripcion", "zonc_icod_zona", true);
            //BSControls.LoaderLook(LkpTipoVenta, new BGeneral().listarTablaRegistro(88), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(LkpSituacion, new BGeneral().listarTablaRegistro(1), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            //BSControls.LoaderLook(lkpPuntoVenta, new BAlmacen().listarAlmacenes(), "almac_vdescripcion", "almac_icod_almacen", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent || Status == BSMaintenanceStatus.View)
            {
                //LkpTipoVenta.EditValue = oBe.vendc_tipo_vendedor;
                LkpSituacion.EditValue = oBe.tablc_iid_situacion_vendedor;
                //LkpTipoVenta.EditValue = oBe.vendc_icod_pvt;
            }

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


            try
            {
                if (string.IsNullOrEmpty(txtIdVendedor.Text))
                {
                    oBase = txtIdVendedor;
                    throw new ArgumentException("Ingresar Código");
                }
                if (LkpSituacion.EditValue == null)
                {
                    oBase = LkpSituacion;
                    throw new ArgumentException("Ingrese Situación");
                }

                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    oBase = txtNombre;
                    throw new ArgumentException("Ingrese Nombre");
                }
                if (string.IsNullOrEmpty(txtDireccion.Text))
                {
                    oBase = txtDireccion;
                    throw new ArgumentException("Ingrese Dirección");
                }
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    var BuscarCodigo = oDetail.Where(oB => oB.vendc_iid_vendedor.ToUpper() == txtIdVendedor.Text.ToUpper()).ToList();
                    if (BuscarCodigo.Count > 0)
                    {
                        oBase = txtIdVendedor;
                        throw new ArgumentException("El Código Existe");
                    }
                }
                //if (txtClave.Properties.ReadOnly == false)
                //{
                //    if (txtClave.Text == "")
                //    {
                //        oBase = txtClave;
                //        throw new ArgumentException("Ingrese la Clave del Vendedor");
                //    }
                //}
                oBe.vendc_iid_vendedor = txtIdVendedor.Text;
                oBe.vendc_vnombre_vendedor = string.IsNullOrEmpty(txtNombre.Text) ? null : txtNombre.Text; ;
                oBe.vendc_vdireccion = string.IsNullOrEmpty(txtDireccion.Text) ? null : txtDireccion.Text; ;
                oBe.vendc_vnumero_telefono = string.IsNullOrEmpty(txtTelefono.Text) ? null : txtTelefono.Text; ;
                oBe.vendc_cnumero_dni = string.IsNullOrEmpty(txtDNI.Text) ? null : txtDNI.Text; ;
                oBe.tablc_iid_situacion_vendedor = Convert.ToInt32(LkpSituacion.EditValue);
                oBe.vendc_vcorreo = txtCorreo.Text;
                oBe.zonc_icod_zona = Convert.ToInt32(lkpZona.EditValue);
                oBe.vendc_vcod_vendedor = txtCodigo.Text;
                //oBe.vendc_tipo_vendedor = Convert.ToInt32(LkpTipoVenta.EditValue);
                //oBe.vendc_vpassword_vendedor = txtClave.Text;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.vendc_icod_vendedor = new BVentas().insertarVendedor(oBe);
                    //Obl.insertarVendedor(oBe);
                }
                else
                {
                    new BVentas().modificarVendedor(oBe);
                    //oBe.vendc_icod_vendedor = Correlative;
                    //Obl.modificarVendedor(oBe);
                }
            }
            catch (Exception ex)
            {
                oBase.Focus();
                oBase.ErrorText = ex.Message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Flag = false;

            }
            finally
            {
                if (Flag)
                {
                    //Status = BSMaintenanceStatus.View;
                    this.MiEvento(oBe.vendc_icod_vendedor);
                    this.Close();
                }
            }
        }

        private void BtnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void BtnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void txtdescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape)
            {
                cerrar_form(sender, e);
            }
        }
        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }
        private void LkpTipoVenta_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}