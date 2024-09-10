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
using SGE.WindowForms.Maintenance;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmManteEntregasFormularioVarios : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public List<EEntregaFormulario> lstVarios = new List<EEntregaFormulario>();
        public int codigo;
        public FrmManteEntregasFormularioVarios()
        {
            InitializeComponent();
        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
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

        private void FrmManteEntregasFormulario_Load(object sender, EventArgs e)
        {
            cargarcontroles();
        }

        public void cargarcontroles()
        {
            dteFechaEntrega.DateTime = DateTime.Today;
            BSControls.LoaderLook(lkpAsesor, new BVentas().listarVendedor().Where(x => x.tablc_iid_situacion_vendedor == 6).ToList(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLook(lkpEstadoEntregaFormulario, new BGeneral().listarTablaVentaDet(24), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);

        }



        public void setsave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                List<EEntregaFormularioDetalle> lstdetalle = new List<EEntregaFormularioDetalle>();
                lstVarios.ForEach(obj =>
                {

                    obj.entf_sfecha_entrega = Convert.ToDateTime(dteFechaEntrega.DateTime);
                    obj.entf_icod_vendedor = Convert.ToInt32(lkpAsesor.EditValue);
                    obj.entf_vobservaciones = txtObervaciones.Text;
                    obj.entf_iusuario_crea = Valores.intUsuario;
                    obj.entf_vpc_crea = WindowsIdentity.GetCurrent().Name;
                    obj.entf_iusuario_modifica = Valores.intUsuario;
                    obj.entf_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                    
                    codigo = new BVentas().insertarEntregaFormulario(obj, lstdetalle);
                });

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
                    this.MiEvento(codigo);
                    this.Close();
                }
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManteDetalleEntregaFormulario frm = new FrmManteDetalleEntregaFormulario())
            {
                frm.SetInsert();
                frm.lstobj = lstVarios;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstVarios.Add(frm.obj);
                    grdDetalle.DataSource = lstVarios;
                    grdDetalle.Refresh();
                    grdDetalle.RefreshDataSource();
                }
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntregaFormulario obe = (EEntregaFormulario)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            using (FrmManteDetalleEntregaFormulario frm = new FrmManteDetalleEntregaFormulario())
            {
                frm.obj = obe;
                frm.lstobj = lstVarios;
                frm.SetModify();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    obe = frm.obj;
                    grdDetalle.DataSource = lstVarios;
                    grdDetalle.Refresh();
                    grdDetalle.RefreshDataSource();

                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEntregaFormulario obe = (EEntregaFormulario)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (obe == null)
                return;
            lstVarios.Remove(obe);
            grdDetalle.DataSource = lstVarios;
            grdDetalle.RefreshDataSource();
            grdDetalle.Refresh();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setsave();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }
    }
}