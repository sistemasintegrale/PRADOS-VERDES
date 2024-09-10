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
using DevExpress.XtraGrid.Views.Grid;
using System.Transactions;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class frmManteRegistroReprogramaciones : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContratoCuotas));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EContratoCuotas Obe = new EContratoCuotas();

        public List<EContratoCuotas> lstDetalle = new List<EContratoCuotas>();
        public List<EContratoCuotas> lstDelete = new List<EContratoCuotas>();
        public List<EContratoCuotas> lstDetalleAnterior = new List<EContratoCuotas>();
        public List<EReprogramaciones> lista = new List<EReprogramaciones>();


        public EContrato ObeC = new EContrato();

        public frmManteRegistroReprogramaciones()
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
            txtNroReprogamacion.Enabled = Enabled;

            //lkpNivel.Enabled = !Enabled;
            //lkpSituacion.Enabled = !Enabled;           
            btnGuardar.Enabled = !Enabled;
            if (Status == BSMaintenanceStatus.ModifyCurrent)
                txtNroReprogamacion.Enabled = Enabled;


            if (Status == BSMaintenanceStatus.CreateNew)
                txtNroReprogamacion.Enabled = Enabled;


        }
        public void setValues()

        {

           
            if (lstDetalle.Count == 0)
            {
                Status = BSMaintenanceStatus.CreateNew;
            }
            else
            {
                Status = BSMaintenanceStatus.ModifyCurrent;
                btnGenerar.Enabled = true;
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

        private void cargar()
        {
            lista = new BVentas().ListarReprogramaciones(ObeC.cntc_icod_contrato);

            grdDetalle.DataSource = lista;
            grdDetalle.RefreshDataSource();
            grdDetalle.Refresh();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                /*----------------------*/
                if (String.IsNullOrEmpty(txtNroReprogamacion.Text))
                {
                    oBase = txtNroReprogamacion;
                    throw new ArgumentException("Ingrese código de la Funeraria");
                }


                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;


                new BVentas().insertarCCuotas(lstDetalle);

                new BVentas().modificarCCuotas(lstDetalleAnterior, lstDelete);




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
                    this.MiEvento(ObeC.cntc_icod_contrato);
                    this.Close();
                }
            }
        }
        public int espacio_codigo { get; set; }
        int[] x = new int[8];

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

            frmManteContratoCuotasReprogramaciones frm = new frmManteContratoCuotasReprogramaciones();
            frm.MiEvento += new frmManteContratoCuotasReprogramaciones.DelegadoMensaje(reload);
            frm.ObeC.cntc_icod_contrato = ObeC.cntc_icod_contrato;
            frm.obj.cntcr_icod_situacion = 1;
            frm.SetInsert();
            frm.ShowDialog();
           // frm.setValues();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReprogramaciones obj = (EReprogramaciones)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obj == null)
                return;

            new BVentas().ActualizarReprogramacion(obj.cntcr_icod_reprogracion, ObeC.cntc_icod_contrato, obj.cntcr_iid_reprogramacion);
            obj = new BVentas().ListarReprogramaciones(ObeC.cntc_icod_contrato).Where(x => x.cntcr_icod_reprogracion == obj.cntcr_icod_reprogracion).FirstOrDefault();
            if (obj.cntcr_icod_situacion == 2)
            {
                XtraMessageBox.Show("No se Puede Modificar , Su Situación es REPROGRAMADO", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reload(obj.cntcr_iid_reprogramacion);
                return;
            }

            frmManteContratoCuotasReprogramaciones frm = new frmManteContratoCuotasReprogramaciones();
            frm.MiEvento += new frmManteContratoCuotasReprogramaciones.DelegadoMensaje(reload);
            frm.ObeC = Obe;
            frm.ObeC.cntc_icod_contrato = ObeC.cntc_icod_contrato; 
            frm.obj = obj;
            frm.modificar = true;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.btnRefresh.Visible = true;
            frm.btnGenerar.Enabled = false;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReprogramaciones obj = (EReprogramaciones)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obj == null)
                return;

            new BVentas().ActualizarReprogramacion(obj.cntcr_icod_reprogracion, ObeC.cntc_icod_contrato, obj.cntcr_iid_reprogramacion);

            obj = new BVentas().ListarReprogramaciones(ObeC.cntc_icod_contrato).Where(x=>x.cntcr_icod_reprogracion == obj.cntcr_icod_reprogracion).FirstOrDefault();

            if (obj.cntcr_icod_situacion != 1)
            {
                XtraMessageBox.Show(string.Format("No se Puede ELIMINAR , su Situación es {0}", obj.strSituacion), "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reload(obj.cntcr_iid_reprogramacion);
                return;
            }
            else
            {
                try
                {
                    using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                    {
                        new BVentas().EliminarReprogramacion(obj);

                        //cambiar el estado de reprogramacion a las cuotas generadas anteriormente
                        int reprogramacionAnterior = (obj.cntcr_iid_reprogramacion - 1);
                        new BVentas().Actualizar_Cuotas_Reprogramacion_Eliminada(reprogramacionAnterior, ObeC.cntc_icod_contrato);

                        //cambiar el estado de reprogramacion generada anteriormente
                        if (reprogramacionAnterior != 0)
                            new BVentas().ActualizarReprogramacion(reprogramacionAnterior, ObeC.cntc_icod_contrato, obj.cntcr_iid_reprogramacion);


                        reload(obj.cntcr_iid_reprogramacion);
                        tx.Complete();
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
                 
            }
            viewDetalle.RefreshData();
        }

        DateTime FechaAnterior;

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtNroCuotas.Text) == 0)
            {
                XtraMessageBox.Show("Ingrese el Número de Cuotas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToDecimal(txtMontoCuotas.Text) == 0)
            {
                XtraMessageBox.Show("Ingrese el Monto de las Cuotas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            grdDetalle.DataSource = 0;
            lstDetalle.Clear();
            int NroCuotas = (Convert.ToInt32(txtNroCuotas.Text));
            FechaAnterior = dtFechaCuota.DateTime.AddMonths(-1);
            for (int y = 1; y <= NroCuotas; y++)
            {
                EContratoCuotas EDet = new EContratoCuotas();

                

                EDet.cntc_icod_contrato = ObeC.cntc_icod_contrato;
                EDet.cntc_inro_cuotas = y;
                EDet.cntc_sfecha_cuota = FechaAnterior.AddMonths(1);
                EDet.cntc_icod_tipo_cuota = 337;
                EDet.strTipo = "CUOTA";
                EDet.cntc_nmonto_cuota = Convert.ToDecimal(txtMontoCuotas.Text);
                EDet.cntc_icod_situacion = 338;
                EDet.strSituacion = "PENDIENTE";
                EDet.intUsuario = Valores.intUsuario;
                EDet.strPc = WindowsIdentity.GetCurrent().Name;
                EDet.cntc_itipo_cuota = Convert.ToInt32(txtNroReprogamacion.Text);
                lstDetalle.Add(EDet);
                FechaAnterior = EDet.cntc_sfecha_cuota;
            }


            grdDetalle.DataSource = lstDetalle;
            viewDetalle.RefreshData();
            btnGenerar.Enabled = false;



        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_icod_documento == 0)
            {
                if (obe.cntc_flag_situacion == true)
                {
                    obe.cntc_icod_situacion = 338;
                    obe.cntc_flag_situacion = false;
                    //new BVentas().modificarContratoCuotas(obe);
                    cargar();
                }
                else
                {
                    obe.cntc_icod_situacion = 340;
                    obe.cntc_flag_situacion = true;
                    //new BVentas().modificarContratoCuotas(obe);
                    cargar();
                }
            }
            else
            {
                if (obe.cntc_flag_situacion == true)
                {
                    obe.cntc_flag_situacion = true;

                    viewDetalle.RefreshData();
                    grdDetalle.Refresh();
                }
                else
                {
                    obe.cntc_flag_situacion = false;
                    viewDetalle.RefreshData();
                    grdDetalle.Refresh();
                }
            }

        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_flag_situacion == true)
            {
                obe.cntc_icod_situacion = 338;
                obe.cntc_flag_situacion = false;
                obe.strSituacion = "PENDIENTE";
                //new BVentas().modificarContratoCuotas(obe);
                cargar();
            }
            else
            {
                obe.cntc_icod_situacion = 340;
                obe.cntc_flag_situacion = true;
                obe.strSituacion = "CANCELADO";
                //new BVentas().modificarContratoCuotas(obe);
                cargar();
            }
        }

        private void viewDetalle_Click(object sender, EventArgs e)
        {
            //EContratoCuotas obe = (EContratoCuotas)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            //if (obe == null)
            //    return;
            //if (obe.cntc_icod_documento > 0)
            //{
            //    seleccionarToolStripMenuItem.Enabled = false;
            //}
            //else
            //{
            //    seleccionarToolStripMenuItem.Enabled = true;
            //}
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lista.FindIndex(x => x.cntcr_iid_reprogramacion == intIcod);
            viewDetalle.FocusedRowHandle = index;
            viewDetalle.Focus();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EReprogramaciones obj = (EReprogramaciones)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obj == null)
                return;

            frmManteContratoCuotasReprogramaciones frm = new frmManteContratoCuotasReprogramaciones();
            frm.MiEvento += new frmManteContratoCuotasReprogramaciones.DelegadoMensaje(reload);
            frm.ObeC = Obe;
            frm.ObeC.cntc_icod_contrato = ObeC.cntc_icod_contrato;
            frm.obj = obj;
            frm.modificar = true;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.setview();
            frm.btnRefresh.Visible = true;
        }
    }
}