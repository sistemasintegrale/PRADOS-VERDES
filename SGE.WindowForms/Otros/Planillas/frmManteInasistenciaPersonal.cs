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
using SGE.WindowForms;

namespace SGE.WindowForms.Otros.Planillas
{
    public partial class frmManteInasistenciaPersonal : DevExpress.XtraEditors.XtraForm
    {
        public int id_tipo_moneda = 0;
        public int id_proveedor = 0;
        private BSMaintenanceStatus mStatus;
        public EInasistencia oBe = new EInasistencia();
        public List<EInasistencia> lstDetalle = new List<EInasistencia>();
        public int prtpd_icod_prestamo;
        public frmManteInasistenciaPersonal()
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
            btnPersonal.Tag = oBe.peric_icod_personal;
            btnPersonal.Text = oBe.strNombrePersonal;
            txtObservaciones.Text = oBe.peric_vobservaciones;
            dtFechaFalta.EditValue = oBe.peric_sfecha_anasist;
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

                if (string.IsNullOrEmpty(btnPersonal.Text))
                {
                    oBase = btnPersonal;
                    throw new Exception("Ingrese Personal");
                }
                //if (string.IsNullOrEmpty(txtObservaciones.Text))
                //{
                //    oBase = txtObservaciones;
                //    throw new Exception("");
                //}
                if (Convert.ToDateTime(dtFechaFalta.EditValue).Year != Parametros.intEjercicio)
                {
                    oBase = dtFechaFalta;
                    throw new Exception("La Fecha Ingresada no Corresponde al Año de Ejecicio");
                }

                
                oBe.peric_icod_personal = Convert.ToInt32(btnPersonal.Tag);
                oBe.strNombrePersonal = btnPersonal.Text;
                oBe.peric_vobservaciones = txtObservaciones.Text;
                oBe.peric_sfecha_anasist = Convert.ToDateTime(dtFechaFalta.EditValue);
                oBe.peric_flag_estado = true;
                oBe.peric_iusuario_crea = Valores.intUsuario;
                oBe.peric_sfecha_crea = DateTime.Today;	
                oBe.peric_vpc_crea = WindowsIdentity.GetCurrent().Name;
                oBe.peric_iusuario_modifica = Valores.intUsuario;
                oBe.peric_sfecha_modifica = DateTime.Today;
                oBe.peric_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                oBe.peric_iusuario_elimina = Valores.intUsuario;
                oBe.peric_sfecha_elimina = DateTime.Today;
                oBe.peric_vpc_elimina = WindowsIdentity.GetCurrent().Name;


                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    int codigo = new BPlanillas().PersonalInasistenciasInsertar(oBe);
                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BPlanillas().PersonalInasistenciasModificar(oBe);
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
                    this.Close();
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
            
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                dtFechaFalta.EditValue = DateTime.Today;

            }
            else
            {
                
            }

        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            frmListarPersonalSimple frm = new frmListarPersonalSimple();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnPersonal.Text = frm._Be.ApellNomb;
                btnPersonal.Tag = frm._Be.perc_icod_personal;
            }
        }
    }
}