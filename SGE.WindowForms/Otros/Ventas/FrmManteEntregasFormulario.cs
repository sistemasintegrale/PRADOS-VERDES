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
    public partial class FrmManteEntregasFormulario : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteContrato));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        private BSMaintenanceStatus mStatus;
        public EEntregaFormulario obj = new EEntregaFormulario();
        public List<EEntregaFormularioDetalle> lstdetalle = new List<EEntregaFormularioDetalle>();
        public List<EEntregaFormularioDetalle> lstdetalleElimina = new List<EEntregaFormularioDetalle>();
        public string numeroFormato;
        public FrmManteEntregasFormulario()
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
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                var parametros = new BVentas().listarRegistroParametro().First();
                txtSerie.Text = parametros.rgpmc_vserie_contrato;
            }

        }

        public void setvalues()
        {
            txtObervaciones.Text = obj.entf_vobservaciones;
            lkpAsesor.EditValue = obj.entf_icod_vendedor;
            dteFechaEntrega.EditValue = obj.entf_sfecha_entrega;
            lkpEstadoEntregaFormulario.EditValue = obj.entf_icod_estado;
            txtSerie.Text = obj.entf_vnumero_formulario.Substring(0, 4);
            txtNumero.Text = obj.entf_vnumero_formulario.Substring(4);
        }

        public void setsave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;

            try
            {
                if (String.IsNullOrEmpty(txtSerie.Text))
                {
                    oBase = txtSerie;
                    throw new ArgumentException("Ingrese Serie de Formato");
                }


                if (txtSerie.Text != "D003" && txtSerie.Text != "0002")
                {
                    oBase = txtSerie;
                    throw new ArgumentException("La Serie debe ser D003 o 0002");
                }



                if (String.IsNullOrEmpty(txtNumero.Text))
                {
                    oBase = txtNumero;
                    throw new ArgumentException("Ingrese Número de Formato");
                }
                bool existe;
                string mensaje ;
                new BVentas().ExistenciaSerieEnEntregas(string.Format($"{txtSerie.Text}{txtNumero.Text}"),out existe, out mensaje);

                if (existe && numeroFormato != string.Format("{0}{1}", txtSerie.Text, txtNumero.Text))
                {
                    oBase = txtNumero;
                    throw new ArgumentException(mensaje);
                }

                ;

                existe = new BVentas().ObtenerExistenciaSerie(string.Format("{0}{1}", txtSerie.Text, txtNumero.Text));
                if (existe)
                {
                    if (XtraMessageBox.Show("Ya Existe Contrato con el Número Ingresado, ¿Desea Constinuar?", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    {
                        return;
                    }
                    lkpEstadoEntregaFormulario.EditValue = 7442;


                }



                obj.entf_sfecha_entrega = Convert.ToDateTime(dteFechaEntrega.DateTime);
                obj.entf_icod_vendedor = Convert.ToInt32(lkpAsesor.EditValue);
                obj.entf_vobservaciones = txtObervaciones.Text;
                obj.entf_iusuario_crea = Valores.intUsuario;
                obj.entf_vpc_crea = WindowsIdentity.GetCurrent().Name;
                obj.entf_iusuario_modifica = Valores.intUsuario;
                obj.entf_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                obj.entf_icod_estado = Convert.ToInt32(lkpEstadoEntregaFormulario.EditValue);
                obj.entf_vnumero_formulario = string.Format("{0}{1}", txtSerie.Text, txtNumero.Text);

                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.entf_icod_entrega = new BVentas().insertarEntregaFormulario(obj, lstdetalle);


                }
                else if (Status == BSMaintenanceStatus.ModifyCurrent)
                {
                    new BVentas().modificarEntregaFormulario(obj, lstdetalle, lstdetalleElimina);

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
                    this.MiEvento(obj.entf_icod_entrega);
                    this.Close();
                }
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) { }

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