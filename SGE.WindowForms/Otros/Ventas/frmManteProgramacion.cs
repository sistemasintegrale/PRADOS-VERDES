using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.BusinessLogic;
using System.Linq;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteProgramacion : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EProgramacion oBe = new EProgramacion();
        public List<EProgramacion> lstDetalle = new List<EProgramacion>();
        
        public frmManteProgramacion()
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
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    bteNroDocumento.Enabled = Enabled;
        }


        public void setValues()
        {

            txtOrden.Text = oBe.rpd_iorden.ToString();
            txtHoraInicial.Text = oBe.rpd_vhora_inicio;
            txtHoraFinal.Text = oBe.rpd_vhora_final;

            txtNomApe.Text = oBe.rpd_vnombre_fallecido;
            btnContrato.Tag = oBe.cntc_icod_contrato;
            btnContrato.Text = oBe.NumContrato;
            lkpNiveles.EditValue = oBe.espad_iid_iespacios;
            lkpTipoSepultura.EditValue = oBe.rpd_itipo_sepultura;
            lkpDeceso.Tag = oBe.rpd_icod_deceso;
            lkpNombreIEC.Tag = oBe.rpd_icod_vendedor;

            btnFunerarios.Tag = oBe.rpd_icod_funeraria;
            btnFunerarios.Text = oBe.strFuneraria;

            txtPlataforma.Text = oBe.strplataforma;
            txtManzana.Text = oBe.strmanzana;
            txtSepultura.Text = oBe.strsepultura;

            txtContrato.Text = oBe.rpd_vcontrato;
            txtFuneraria.Text = oBe.rpd_vfuneraria;
            txtContratante.Text = oBe.rpd_vcontratante;
            txtObservaciones.Text = oBe.rpd_observaciones;
            lkpSituacion.EditValue = oBe.rpd_icod_situacion;


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
            setValues();
        }

        private void setSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try 
            {
                //if (txtNomApe.Text != "")
                //{
                //    if (Convert.ToDateTime(dteFechaFallecmiento.EditValue) == null)
                //    {
                //        oBase = dteFechaFallecmiento;
                //        throw new ArgumentException("Ingrese Fecha");
                //    }
                //}
                //else
                //{
                //    dteFechaFallecmiento.EditValue = "";
                //}





                oBe.rpd_vnombre_fallecido = txtNomApe.Text;
                oBe.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                oBe.espad_iid_iespacios = Convert.ToInt32(lkpNiveles.EditValue);
                oBe.rpd_itipo_sepultura = Convert.ToInt32(lkpTipoSepultura.EditValue);
                oBe.rpd_icod_deceso = Convert.ToInt32(lkpDeceso.Tag);
                oBe.rpd_icod_vendedor = Convert.ToInt32(lkpNombreIEC.Tag);
                oBe.rpd_icod_funeraria = Convert.ToInt32(btnFunerarios.Tag);
                oBe.rpd_vcontrato = txtContrato.Text;
                oBe.rpd_vfuneraria = txtFuneraria.Text;
                oBe.rpd_vcontratante = txtContratante.Text;
                oBe.rpd_observaciones = txtObservaciones.Text;
                oBe.rpd_icod_situacion = Convert.ToInt32(lkpSituacion.EditValue);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {

                    new BVentas().modificarProgramacion(oBe);

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
                    this.DialogResult = DialogResult.OK;                
            }
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setSave(); 
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmMantePercepcionDetalle_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNombreIEC, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpDeceso, new BGeneral().listarTablaVentaDet(17), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpSituacion, new BGeneral().listarTablaVentaDet(18), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpNiveles.EditValue = oBe.espad_iid_iespacios;
                lkpTipoSepultura.EditValue = oBe.rpd_itipo_sepultura;
                lkpDeceso.Tag = oBe.rpd_icod_deceso;
                lkpNombreIEC.Tag = oBe.rpd_icod_vendedor;
                lkpSituacion.EditValue = oBe.rpd_icod_situacion;
            }
            if (Valores.vendc_icod_vendedor > 0)
            {
                lkpNombreIEC.Enabled = false;
                lkpNombreIEC.Tag = Valores.vendc_icod_vendedor;
            }
        }
        public int icodEspacio;
        private void btnContrato_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarContrato frm = new FrmListarContrato())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnContrato.Tag = frm._Be.cntc_icod_contrato;
                        btnContrato.Text = frm._Be.cntc_vnumero_contrato;
                        icodEspacio = frm._Be.espac_iid_iespacios;
                        txtPlataforma.Text = frm._Be.strplataforma;
                        txtManzana.Text = frm._Be.strmanzana;
                        txtSepultura.Text = frm._Be.strsepultura;
                    }
                    Niveles();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFunerarios_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (frmListarFunerarias frm = new frmListarFunerarias())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnFunerarios.Tag = frm._Be.func_icod_funeraria;
                        btnFunerarios.Text = frm._Be.func_vnombre_comercial;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Niveles()
        {
            List<EEspaciosDet> listNiveles = new List<EEspaciosDet>();
            listNiveles = new BVentas().listarEspaciosDet(Convert.ToInt32(icodEspacio));
            BSControls.LoaderLook(lkpNiveles, listNiveles, "espad_vnivel", "espad_iid_iespacios", true);

        }
    }
}