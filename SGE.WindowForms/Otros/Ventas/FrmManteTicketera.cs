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

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteTicketera : DevExpress.XtraEditors.XtraForm
    {
        public List<ETicketera> lista = new List<ETicketera>();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTicketera));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;

        public FrmManteTicketera()
        {
            this.KeyUp += new KeyEventHandler(cerrar_form);
            InitializeComponent();
        }
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public ETicketera oBe = new ETicketera();

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
            txtSerieImpresora.Enabled = !Enabled;
            txtSerie.Enabled = !Enabled;
            txtCorrelativo.Enabled = !Enabled;
            txtImpresora.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                txtImpresora.Enabled = !Enabled;
                txtSerieImpresora.Enabled = !Enabled;
            }
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {
                txtSerie.Enabled = !Enabled;
                txtCorrelativo.Enabled = !Enabled;
                txtSerieImpresora.Enabled = !Enabled;
                txtImpresora.Enabled = Enabled;
            }
            txtSerieImpresora.Focus();
        }

        public void setValues()
        {
            txtImpresora.Text = oBe.tckc_inumero_impresora.ToString();
            txtSerieImpresora.Text = Convert.ToString(oBe.tckc_vserie_impresora);
            txtNomLocal.Text = oBe.tckc_vnombre_local;
            txtNomLocal.Text = oBe.tckc_vdireccion;
            txtSerie.Text = oBe.tckc_vserie.ToString();
            txtCorrelativo.Text = oBe.tckc_vcorrelativo.ToString();

            
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
                    if (string.IsNullOrEmpty(txtSerieImpresora.Text))
                    {
                        oBase = txtSerieImpresora;
                        throw new ArgumentException("Ingresar Descripción");
                    }
                }

                oBe.tckc_inumero_impresora = Convert.ToInt32(txtImpresora.Text);
                oBe.tckc_vserie_impresora = txtSerieImpresora.Text;
                oBe.tckc_vnombre_local = txtNomLocal.Text;
                oBe.tckc_vdireccion = txtDireccion.Text;
                oBe.tckc_vserie = txtSerie.Text;
                oBe.tckc_vcorrelativo = txtCorrelativo.Text;



                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    oBe.tablc_iid_situacion = 0; // por definir
                    oBe.tckc_icod_ticketera = new BVentas().insertarTicketera(oBe);
                }
                else
                {
                    new BVentas().modificarTicketera(oBe);
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

                    this.MiEvento(oBe.tckc_icod_ticketera);
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
        }
    }
}