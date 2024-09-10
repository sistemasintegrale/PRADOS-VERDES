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
using SGI.WindowsForm.Otros.Ventas;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteProyeccionVentas : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteVendedor));
        public delegate void DelegadoMensaje(int intIcod);
        public event DelegadoMensaje MiEvento;
        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public EProyeccionVendedor obj = new EProyeccionVendedor();
        public frmManteProyeccionVentas()
        {
            InitializeComponent();
        }

        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;

        }
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
            }
        }

        public void CargarControles() {
            BSControls.LoaderLook(lkpAnio, new BContabilidad().listarAnioEjercicio(), "anioc_iid_anio_ejercicio", "anioc_iid_anio_ejercicio", true);
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            BSControls.LoaderLook(lkpAsesor, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            setsave();
        }

        public  void SetValues()
        {
            lkpAnio.EditValue = obj.anioc_iid_anio_ejercicio;
            lkpMes.EditValue = obj.proyc_imes;
            lkpAsesor.EditValue = obj.vendc_icod_vendedor;
            txtCantidadEstimada.Text = obj.proyc_icantidad_estimada.ToString();
        }

        void setsave()
        {
            BaseEdit oBase = null;
            Boolean Flag = true;
            try
            {
                if (Convert.ToInt32(txtCantidadEstimada.Text) == 0)
                {
                    oBase = txtCantidadEstimada;
                    throw new ArgumentException("Ingrese la Cantidad Estimada");
                }

                if (Convert.ToInt32(lkpAsesor.EditValue) == 0)
                {
                    oBase = lkpAsesor;
                    throw new ArgumentException("Ingrese Asesor");
                }

                if (Convert.ToInt32(lkpAnio.EditValue) == 0)
                {
                    oBase = lkpAnio;
                    throw new ArgumentException("Ingrese el Año");
                }

                if (Convert.ToInt32(lkpMes.EditValue) == 0)
                {
                    oBase = lkpMes;
                    throw new ArgumentException("Ingrese el Mes");
                }

                obj.vendc_icod_vendedor = Convert.ToInt32(lkpAsesor.EditValue);
                obj.anioc_iid_anio_ejercicio = Convert.ToInt32(lkpAnio.EditValue);
                obj.proyc_imes = Convert.ToInt32(lkpMes.EditValue);
                obj.proyc_icantidad_estimada = Convert.ToInt32(txtCantidadEstimada.EditValue);
                obj.proyc_iusuario = Valores.intUsuario;
                obj.proyc_vpc = WindowsIdentity.GetCurrent().Name;
                if (Status == BSMaintenanceStatus.CreateNew)
                {
                    obj.proyc_icod_proyeccion = new BVentas().ProyeccionVentasInsertar(obj);
                }
                else
                {
                    new BVentas().ProyeccionVentasModificar(obj);
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
                    this.MiEvento(obj.proyc_icod_proyeccion);
                    this.Close();
                }
            }

        }

        private void frmManteProyeccionVentas_Load(object sender, EventArgs e)
        {
            if (Status == BSMaintenanceStatus.CreateNew)
            {
                var lstEjercicio = new BContabilidad().listarAnioEjercicio();

                BSControls.LoaderLook(lkpAnio, lstEjercicio, "anioc_iid_anio_ejercicio", "anioc_iid_anio_ejercicio", true);
                if (lstEjercicio.Where(x => x.anioc_iid_anio_ejercicio == DateTime.Now.Year).ToList().Count == 1)
                    lkpAnio.EditValue = DateTime.Now.Year;

                var lstMeses = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList();

                BSControls.LoaderLook(lkpMes, lstMeses, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
                lkpMes.EditValue = lstMeses.Where(x => x.tarec_icorrelativo_registro == DateTime.Now.Month).FirstOrDefault().tarec_iid_tabla_registro;
                BSControls.LoaderLook(lkpAsesor, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            }
        }
    }
}