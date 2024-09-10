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
    public partial class frmMantePlantillaProgramacion : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteEspacios));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EPlantillaProgramacion Obe = new EPlantillaProgramacion();
        public List<EPlantillaProgramacion> lstEspacios = new List<EPlantillaProgramacion>();
        public List<EPlantillaProgramacionDet> lstDetalle = new List<EPlantillaProgramacionDet>();
        public List<EPlantillaProgramacionDet> lstDelete = new List<EPlantillaProgramacionDet>();

        public frmMantePlantillaProgramacion()
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
            txtNumero.Text = string.Format("{0:000}", Obe.plap_inumero_plantilla);

            lstDetalle = new BVentas().listarPlantillaProgramacionDet(Obe.plap_icod_plantilla_programacion);
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

                }



            /*----------------------*/

                Obe.plap_inumero_plantilla = Convert.ToInt32(txtNumero.Text);              
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                        Obe.plap_icod_plantilla_programacion = new BVentas().insertarPlantillaProgramacion(Obe,lstDetalle);

                }
                else
                {
                        Obe.plap_icod_plantilla_programacion = Obe.plap_icod_plantilla_programacion;
                        new BVentas().modificarPlantillaProgramacion(Obe,lstDetalle,lstDelete);

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

                using (frmMantePlantillaProgramacionDet frm = new frmMantePlantillaProgramacionDet())
                {
                    if (lstDetalle.Count > 0)
                        frm.txtOrden.Text = lstDetalle.Max(x=> Convert.ToInt32(x.plad_iorden) + 1).ToString();
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
            EPlantillaProgramacionDet oBe_ = (EPlantillaProgramacionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmMantePlantillaProgramacionDet frm = new frmMantePlantillaProgramacionDet())
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
            EPlantillaProgramacionDet oBe_ = (EPlantillaProgramacionDet)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
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

    }
}