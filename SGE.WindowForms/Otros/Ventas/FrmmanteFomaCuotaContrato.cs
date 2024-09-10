using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.bVentas;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmmanteFomaCuotaContrato : XtraForm
    {
        public EContrato obe = new EContrato();
        public List<ECuotaFoma> lista = new List<ECuotaFoma>();
        public ECuotaFoma objCuota = new ECuotaFoma();
        ComponentResourceManager resources = new ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
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

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }
        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }
        private void StatusControl()
        {
        }
        public FrmmanteFomaCuotaContrato()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        public void Setvalues()
        {
            txtMontoPagado.Text = objCuota.ccf_nmonto_pagado.ToString();
            txtMontoPagar.Text = objCuota.ccf_nmonto_pagar.ToString();
            lkpNiveles.EditValue = objCuota.ccf_icod_nivel;
            dteFechaPago.EditValue = objCuota.cff_sfecha_pago;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToDecimal(txtMontoPagar.Text) == 0)
                {
                    oBase = txtMontoPagar;
                    throw new ArgumentException("Ingrese el Monto de la Foma");
                }
                if (Convert.ToDecimal(txtMontoPagado.Text) > Convert.ToDecimal(txtMontoPagar.Text))
                {
                    oBase = txtMontoPagado;
                    throw new ArgumentException("El monto pagado no puede ser menor al monto de la Foma");
                }
                if (Convert.ToInt32(lkpNiveles.EditValue) == 0)
                {
                    oBase = lkpNiveles;
                    throw new ArgumentException("Ingrese Niveles");
                }
                //Validar Existencia
                var listaExistente = new BVentas().CuotaFomaListar(obe.cntc_icod_contrato);
                if (listaExistente.Exists(x => x.ccf_icod_nivel == Convert.ToInt32(lkpNiveles.EditValue)))
                {

                    if (objCuota.ccf_icod_cuota == 0)
                    {
                        oBase = lkpNiveles;
                        throw new ArgumentException("Ya existe Foma del Nivel Ingresado");
                    }
                    var data = new BVentas().CuotaFomaGetById(listaExistente.Where(x => x.ccf_icod_nivel == Convert.ToInt32(lkpNiveles.EditValue)).First().ccf_icod_cuota);
                    if (objCuota.ccf_icod_cuota != data.ccf_icod_cuota)
                    {
                        oBase = lkpNiveles;
                        throw new ArgumentException("Ya existe Foma del Nivel Ingresado");
                    }
                }

                if (Convert.ToDecimal(txtMontoPagado.Text)!= 0)
                {
                    if (dteFechaPago.EditValue == null)
                    {
                        oBase = dteFechaPago;
                        throw new ArgumentException("Ingrese la Fecha del Pago");
                    }
                }

                if (dteFechaPago.EditValue != null)
                {
                    if (Convert.ToDecimal(txtMontoPagado.Text) == 0)
                    {
                        oBase = txtMontoPagado;
                        throw new ArgumentException("Ingrese la Monto del Pago");
                    }
                }

                objCuota.ccf_nmonto_pagado = Convert.ToDecimal(txtMontoPagado.Text);
                objCuota.ccf_nmonto_pagar = Convert.ToDecimal(txtMontoPagar.Text);
                objCuota.ccf_icod_nivel = Convert.ToInt32(lkpNiveles.EditValue);
                objCuota.cntc_icod_contrato = obe.cntc_icod_contrato;
                objCuota.intUsuario = Valores.intUsuario;
                objCuota.strPc = WindowsIdentity.GetCurrent().Name;
                objCuota.cff_sfecha_pago = (DateTime?)dteFechaPago.EditValue;
                objCuota.strNivel = lkpNiveles.Text;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    objCuota.ccf_icod_cuota = new BVentas().CuotaFomaInsertar(objCuota);
                }
                else
                {
                    new BVentas().CuotaFomaModificar(objCuota);
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
                    this.MiEvento(objCuota.ccf_icod_cuota);
                    this.Close();
                }

            }


        }


        private void FrmmanteFomaCuotaContrato_Load(object sender, EventArgs e)
        {
            if (txtMontoPagado.Visible == false && label3.Visible == false)
                this.Height = 166;
            else
                this.Height = 250;
            BSControls.LoaderLook(lkpNiveles, new BGeneral().listarTablaVentaDet(21), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                Setvalues();
        }
    }
}