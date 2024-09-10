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
    public partial class frmManteAutorizacionUso : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EEspaciosAutorizacionUso oBe = new EEspaciosAutorizacionUso();
        public List<EEspaciosAutorizacionUso> lstDetalle = new List<EEspaciosAutorizacionUso>();
        public int icodEspacio;
        public frmManteAutorizacionUso()
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
            txtCodigo.Text = string.Format("{0:000000}", oBe.espau_iid_autorizacion_uso);
            dteFecha.EditValue = oBe.espau_sfecha;
            txtNomApe.Text = oBe.espau_vnom_fallecido;
            txtApellidoPFallecido.Text = oBe.espau_vapellido_paterno_fallecido;
            txtApellidoMFallecido.Text = oBe.espau_vapellido_materno_fallecido;
            txtDNIFallecimiento.Text = oBe.espau_vdni_fallecido;
            dtFechaNacFallecido.EditValue = oBe.espau_sfecha_nac_fallecido;
            dteFechaFallecmiento.EditValue = oBe.espau_sfecha_fallecido;
            dtFechaEntierro.EditValue = oBe.espau_sfecha_entierro;
            lkpNacionalidad1.EditValue = oBe.espau_inacionalidad;
            btnContrato.Tag = oBe.cntc_icod_contrato;
            btnContrato.Text = oBe.NumContrato;
            icodEspacio = oBe.espac_iid_iespacios;
            lkpNiveles.EditValue = oBe.espad_iid_iespacios;
            //lkpNiveles.Text = oBe.espad_vnivel;
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



                oBe.espau_iid_autorizacion_uso = Convert.ToInt32(txtCodigo.Text);
                oBe.espau_sfecha = Convert.ToDateTime(dteFecha.EditValue);
                oBe.espau_vnom_fallecido = txtNomApe.Text;
                oBe.espau_vapellido_paterno_fallecido = txtApellidoPFallecido.Text;
                oBe.espau_vapellido_materno_fallecido = txtApellidoMFallecido.Text;
                oBe.espau_vdni_fallecido = txtDNIFallecimiento.Text;
                if (dtFechaNacFallecido.DateTime == null || dtFechaNacFallecido.Text == "" || dtFechaNacFallecido.Text == "01/01/0001")
                {
                    oBe.espau_sfecha_nac_fallecido = (DateTime?)null;
                }
                else
                {
                    oBe.espau_sfecha_nac_fallecido = Convert.ToDateTime(dtFechaNacFallecido.EditValue);
                }
                if (dteFechaFallecmiento.DateTime == null || dteFechaFallecmiento.Text == "" || dteFechaFallecmiento.Text == "01/01/0001")
                {
                    oBe.espau_sfecha_fallecido = (DateTime?)null;
                }
                else
                {
                    oBe.espau_sfecha_fallecido = dteFechaFallecmiento.DateTime;
                }
                if (dtFechaEntierro.DateTime == null || dtFechaEntierro.Text == "" || dtFechaEntierro.Text == "01/01/0001")
                {
                    oBe.espau_sfecha_entierro = (DateTime?)null;
                }
                else
                {
                    oBe.espau_sfecha_entierro = Convert.ToDateTime(dtFechaEntierro.EditValue);
                }
                oBe.espau_inacionalidad = Convert.ToInt32(lkpNacionalidad1.EditValue);
                oBe.cntc_icod_contrato = Convert.ToInt32(btnContrato.Tag);
                oBe.espad_iid_iespacios = Convert.ToInt32(lkpNiveles.EditValue);
                oBe.espac_iid_iespacios = icodEspacio;
                oBe.intUsuario = Valores.intUsuario;
                oBe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.espau_icod_autorizacion_uso = new BVentas().insertarEspaciosAutorizacionUso(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    //if (oBe.intTipoOperacion != 1)
                    //    oBe.intTipoOperacion = 2;

                    new BVentas().modificarEspaciosAutorizacionUso(oBe);

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
                    this.MiEvento(oBe.espau_icod_autorizacion_uso);
                    this.Close();
                }
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
            dteFecha.EditValue = DateTime.Now;
            List<EEspaciosDet> listNiveles = new List<EEspaciosDet>();
            listNiveles = new BVentas().listarEspaciosDet(Convert.ToInt32(oBe.espac_iid_iespacios));
            BSControls.LoaderLook(lkpNiveles, listNiveles, "espad_vnivel", "espad_iid_iespacios", true);
            BSControls.LoaderLook(lkpNacionalidad1, new BGeneral().listarTablaRegistro(95), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                lkpNacionalidad1.EditValue = oBe.espau_inacionalidad;
                lkpNiveles.EditValue = oBe.espad_iid_iespacios;
            }
            //if (Status == BSMaintenanceStatus.ModifyCurrent)
            //    setValues();
        }

        private void btnContrato_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarContrato();
        }
        private void listarContrato()
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
                    }
                    Niveles();
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