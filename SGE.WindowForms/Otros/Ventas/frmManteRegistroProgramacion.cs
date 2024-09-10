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



namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteRegistroProgramacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteEspacios));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public ERegistroProgramacion Obe = new ERegistroProgramacion();
        public List<ERegistroProgramacion> lstRegistroProgramacion = new List<ERegistroProgramacion>();
        public List<ERegistroProgramacionDet> lstDetalle = new List<ERegistroProgramacionDet>();
        public List<ERegistroProgramacionDet> lstDelete = new List<ERegistroProgramacionDet>();

        public frmManteRegistroProgramacion()
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
            txtNumero.Enabled = !Enabled;
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtNumero.Enabled = Enabled;
            }
            if (Status == BSMaintenanceStatus.CreateNew)
                txtNumero.Enabled = Enabled;
            
           
        }
        public void setValues()       
        {
            txtNumero.Text = string.Format("{0:000}", Obe.rp_inumero_registro_programacion);
            dtFecha.EditValue = Obe.rp_fecha;
            txtObservaciones.Text = Obe.rp_vobservaciones;
            btnPlantilla.Tag = Obe.plap_icod_plantilla_programacion;
            btnPlantilla.Text = string.Format("{0:000}", Obe.NumPlantilla);
            lstDetalle = new BVentas().listarRegistroProgramacionDet(Obe.rp_icod_registro_programacion);
            grdDetalle.DataSource = lstDetalle;
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
       
        private void cargar()
        {
            dtFecha.EditValue = DateTime.Now;
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;            
            
            try
            {               
                /*----------------------*/
                if (String.IsNullOrEmpty(txtNumero.Text))
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese código de la Funeraria");
                }

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    List<ERegistroProgramacion> lstValidacion = lstRegistroProgramacion.Where(x=> x.rp_fecha == Convert.ToDateTime(dtFecha.EditValue)).ToList();
                    if (lstValidacion.Count > 0)
                    {
                        throw new ArgumentException("Fecha ya existe en el Registro");
                    }
                }



            /*----------------------*/

                Obe.rp_inumero_registro_programacion = Convert.ToInt32(txtNumero.Text);
                Obe.rp_fecha = Convert.ToDateTime(dtFecha.EditValue);
                Obe.rp_vobservaciones = txtObservaciones.Text;
                Obe.plap_icod_plantilla_programacion = Convert.ToInt32(btnPlantilla.Tag);      
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                        Obe.plap_icod_plantilla_programacion = new BVentas().insertarRegistroProgramacion(Obe,lstDetalle);

                }
                else
                {
                        Obe.rp_icod_registro_programacion = Obe.rp_icod_registro_programacion;
                        new BVentas().modificarRegistroProgramacion(Obe,lstDetalle,lstDelete);

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
                    this.MiEvento(Obe.plap_icod_plantilla_programacion);
                    this.Close();
                }
            }
        }


      
        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetSave();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmManteFuneraria_Load(object sender, EventArgs e)
        {
            cargar();
        }


        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {

                using (frmManteRegistroProgramacionDet frm = new frmManteRegistroProgramacionDet())
                {
                    if (lstDetalle.Count > 0)
                        frm.txtOrden.Text = lstDetalle.Max(x=> Convert.ToInt32(x.rpd_iorden) + 1).ToString();
                    else
                        frm.txtOrden.Text = "1";
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        grdDetalle.DataSource = lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.Focus();
                    }
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
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERegistroProgramacionDet oBe_ = (ERegistroProgramacionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteRegistroProgramacionDet frm = new frmManteRegistroProgramacionDet())
            {
                frm.oBe = oBe_;
                frm.SetModify();
                frm.lstDetalle = lstDetalle;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.Focus();
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ERegistroProgramacionDet oBe_ = (ERegistroProgramacionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            try
            {

                lstDelete.Add(oBe_);
                lstDetalle.Remove(oBe_);
                viewDetalle.RefreshData();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnPlantilla_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                using (FrmListarPlantillaProgramacion frm = new FrmListarPlantillaProgramacion())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        btnPlantilla.Tag = frm._Be.plap_icod_plantilla_programacion;
                        btnPlantilla.Text = string.Format("{0:000}", frm._Be.plap_inumero_plantilla);
                    }
                    ListarPlanilla();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ListarPlanilla()
        {
            List<EPlantillaProgramacionDet> lstPlantillaDet = new List<EPlantillaProgramacionDet>();
            lstPlantillaDet = new BVentas().listarPlantillaProgramacionDet(Convert.ToInt32(btnPlantilla.Tag));
            grdDetalle.DataSource = 0;
            lstPlantillaDet.ForEach(x=>
            {
                ERegistroProgramacionDet EDet = new ERegistroProgramacionDet();
                EDet.rpd_iorden = x.plad_iorden;
                EDet.rpd_vhora_inicio = x.plad_vhora_inicial;
                EDet.rpd_vhora_final = x.plad_vhora_final;
                EDet.intUsuario = Valores.intUsuario;
                EDet.strPc = WindowsIdentity.GetCurrent().Name;
                lstDetalle.Add(EDet);

            });
            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
            btnPlantilla.Enabled = false;

        }
    }
}