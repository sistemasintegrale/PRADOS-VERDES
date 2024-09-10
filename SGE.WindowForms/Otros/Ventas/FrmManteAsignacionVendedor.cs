using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteAsignacionVendedor : DevExpress.XtraEditors.XtraForm
    {
        public List<ECaja> lista = new List<ECaja>();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteAsignacionVendedor));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public FrmManteAsignacionVendedor()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public ECaja oBe = new ECaja();
        public List<EAsignacionVendedor> lstAsigVendedor = new List<EAsignacionVendedor>();
        public EAsignacionVendedor ObeAV = new EAsignacionVendedor();

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
            //txtCaja.Enabled = !Enabled;
            //TxtCodCaja.Enabled = !Enabled;
            //lkpPuntoVenta.Enabled = !Enabled;
            //if (Status == BSMaintenanceStatus.CreateNew)
            //{
            //    //TxtCodCaja.Enabled = !Enabled;
            //    //txtCaja.Enabled = !Enabled;
            //}
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //{
            //    txtCaja.Enabled = !Enabled;
            //    TxtCodCaja.Enabled = Enabled;
            //    lkpPuntoVenta.Enabled = Enabled;
            //}
            //txtCaja.Focus();
        }

        public void setValues()
        {
            txtCaja.Text = Convert.ToString(oBe.cajac_vdescripcion);
            TxtCodCaja.Text = oBe.cajac_vcod_caja.ToString();
            lkpTurno.EditValue = ObeAV.tablc_iid_turno;
            lkpVendedor.EditValue = ObeAV.vendc_icod_vendedor;
            lkpVendedor.Text = ObeAV.NomVendedor;
            txtClave.Text = ObeAV.asigc_vpassword_vendedor;
            
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
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    if (string.IsNullOrEmpty(txtCaja.Text))
                    {
                        oBase = txtCaja;
                        throw new ArgumentException("Ingresar Descripción");
                    }
                    if (lkpPuntoVenta.EditValue == null)
                    {
                        oBase = lkpPuntoVenta;
                        throw new ArgumentException("Ingresar Punto Venta");
                    }
                    if (lista.Count(ob => ob.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue) && ob.cajac_vcod_caja == Convert.ToInt32(TxtCodCaja.Text)) > 0)
                    {
                        oBase = TxtCodCaja;
                        throw new ArgumentException("El código Existe");
                    }
                }
                ObeAV.cajac_icod_caja = oBe.cajac_icod_caja;
                ObeAV.tablc_iid_turno = Convert.ToInt32(lkpTurno.EditValue);
                ObeAV.vendc_icod_vendedor = Convert.ToInt32(lkpVendedor.EditValue);
                ObeAV.asigc_vpassword_vendedor = txtClave.Text;
                ObeAV.intUsuario = Valores.intUsuario;
                ObeAV.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    ObeAV.asigc_icod_asignacion = new BVentas().insertarAsignacionVendedor(ObeAV);
                }
                else
                {
                    new BVentas().modificarAsignacionVendedor(ObeAV);
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
                  
                    this.MiEvento(ObeAV.asigc_icod_asignacion);
                    this.Close();
                }
            }
        }
        private void FrmManteCaja_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtCaja_KeyUp(object sender, KeyEventArgs e)
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

        private void FrmManteCaja_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.MiEvento();    
        }
        private void cargar()
        {
            //BSControls.LoaderLook(lkpPuntoVenta, new BVentas().listarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
            TxtCodCaja.Text = (new BVentas().ValidarCodigoCaja()).ToString();
            txtCaja.Text = "CAJA ";
            BSControls.LoaderLook(lkpVendedor, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpTurno, new BGeneral().listarTablaRegistro(89), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }

        private void lkpPuntoVenta_EditValueChanged(object sender, EventArgs e)
        {
            int contador=0;
            if (lista.Count > 0 && lista.Where(ob=>ob.puvec_icod_punto_venta==Convert.ToInt32(lkpPuntoVenta.EditValue)).Count()>0)
            {
                contador = lista.Where(ob => ob.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue)).Max(o => Convert.ToInt32(o.cajac_vcod_caja));
            }
            else
                contador = 0;
             TxtCodCaja.Text = (contador + 1).ToString();

        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrar.Checked == false)
            {
                txtClave.Properties.UseSystemPasswordChar = true;
            }
            else if (chkMostrar.Checked == true)
            {
                txtClave.Properties.UseSystemPasswordChar = false;
            }
        }
    }
}