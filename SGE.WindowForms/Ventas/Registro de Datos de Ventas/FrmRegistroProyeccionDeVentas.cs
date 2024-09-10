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
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System.Security.Principal;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class FrmRegistroProyeccionDeVentas : DevExpress.XtraEditors.XtraForm
    {
        List<EProyeccionVendedor> lista = new List<EProyeccionVendedor>();
        public int Mes;
        public int Anio;
        public FrmRegistroProyeccionDeVentas()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteProyeccionVentas frm = new frmManteProyeccionVentas();
            frm.MiEvento += new frmManteProyeccionVentas.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.ShowDialog();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProyeccionVendedor obe = (EProyeccionVendedor)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (obe == null)
                return;
            frmManteProyeccionVentas frm = new frmManteProyeccionVentas();
            frm.MiEvento += new frmManteProyeccionVentas.DelegadoMensaje(reload);
            frm.CargarControles();
            frm.obj = obe;
            frm.SetValues();
            frm.SetModify();
            frm.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProyeccionVendedor obe = (EProyeccionVendedor)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (obe == null)
                return;
            obe.proyc_iusuario = Valores.intUsuario;
            obe.proyc_vpc = WindowsIdentity.GetCurrent().Name;
            if (XtraMessageBox.Show($"¿Está Seguro de Eliminar el Registro ?", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                new BVentas().ProyeccionVentasEliminar(obe);
                reload(obe.proyc_icod_proyeccion - 1);
            }
            
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Mes = Convert.ToInt32(lkpMes.EditValue);
            Anio = Convert.ToInt32(lkpAnio.EditValue);
            cargar();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Mes = Convert.ToInt32(lkpMes.EditValue);
            Anio = Convert.ToInt32(lkpAnio.EditValue);
            cargar();
        }

        private void FrmRegistroProyeccionDeVentas_Load(object sender, EventArgs e)
        {
            
            var lstEjercicio = new BContabilidad().listarAnioEjercicio();

            BSControls.LoaderLook(lkpAnio, lstEjercicio, "anioc_iid_anio_ejercicio", "anioc_iid_anio_ejercicio", true);
            if (lstEjercicio.Where(x => x.anioc_iid_anio_ejercicio == DateTime.Now.Year).ToList().Count == 1)
                lkpAnio.EditValue = DateTime.Now.Year;

            var lstMeses = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList();

            BSControls.LoaderLook(lkpMes, lstMeses, "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            lkpMes.EditValue = lstMeses.Where(x=>x.tarec_icorrelativo_registro == DateTime.Now.Month).FirstOrDefault().tarec_iid_tabla_registro;
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit1, new BVentas().listarVendedor(), "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLookRepository(repositoryItemLookUpEdit2, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_iid_tabla_registro != 43 && x.tarec_iid_tabla_registro != 56).ToList(), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            cargar();
        }

        void cargar()
        {
            Mes = Convert.ToInt32(lkpMes.EditValue);
            Anio = Convert.ToInt32(lkpAnio.EditValue);
            lista = new BVentas().ProyeccionVentasListar(Anio, Mes);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lista.FindIndex(x => x.proyc_icod_proyeccion == intIcod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }
    }
}