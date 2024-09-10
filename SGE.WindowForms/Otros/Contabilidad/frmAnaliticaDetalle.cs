using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmAnaliticaDetalle : DevExpress.XtraEditors.XtraForm
    {
        List<EAnaliticaDetalle> lstAnaliticaDetalle = new List<EAnaliticaDetalle>();
        public int intTipoAnalitica = 0;
        public frmAnaliticaDetalle()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstAnaliticaDetalle = new BContabilidad().listarAnaliticaDetalle(intTipoAnalitica);
            grdAnalitica.DataSource = lstAnaliticaDetalle;
            viewAnalitica.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstAnaliticaDetalle.FindIndex(x => x.anad_icod_analitica == intIcod);
            viewAnalitica.FocusedRowHandle = index;
            viewAnalitica.Focus();
        }     
        private void nuevo()
        {
            frmManteAnaliticaDetalle frm = new frmManteAnaliticaDetalle();
            frm.MiEvento += new frmManteAnaliticaDetalle.DelegadoMensaje(reload);
            frm.intTipoAnalitica = intTipoAnalitica;
            frm.lstAnaliticaDetalle = lstAnaliticaDetalle;
            frm.SetInsert();
            frm.Show();
        }
        private void modificar()
        {
            EAnaliticaDetalle Obe = (EAnaliticaDetalle)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAnaliticaDetalle frm = new frmManteAnaliticaDetalle();
            frm.MiEvento += new frmManteAnaliticaDetalle.DelegadoMensaje(reload);
            frm.intTipoAnalitica = intTipoAnalitica;
            frm.lstAnaliticaDetalle = lstAnaliticaDetalle;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();     
        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            EAnaliticaDetalle Obe = (EAnaliticaDetalle)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAnaliticaDetalle frm = new frmManteAnaliticaDetalle();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            EAnaliticaDetalle Obe = (EAnaliticaDetalle)viewAnalitica.GetRow(viewAnalitica.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BContabilidad().eliminarAnaliticaDetalle(Obe);
                cargar();
            }
        }
        private void imprimir()
        { 
        }
        private void buscarCriterio()
        {
            grdAnalitica.DataSource = lstAnaliticaDetalle.Where(x =>
                                                   x.anad_iid_analitica.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.anad_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }    

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void generarAnaliticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAnaliticaDetalle objE_AnaliticaDetalle = new EAnaliticaDetalle();
            ECliente prov = new ECliente();
            int i = 0;

            //i = lstPanalitica.Max(ob => ob.anad_icod_analitica);

            List<ECliente> lstProv = new List<ECliente>();
            lstProv = new BVentas().ListarCliente();    
            foreach (var item in lstProv)
            {               
                //objE_AnaliticaDetalle.anad_icod_analitica = Convert.ToInt32(i) + 1; ;
                objE_AnaliticaDetalle.anad_iid_analitica = item.cliec_vcod_cliente;
                objE_AnaliticaDetalle.anad_vdescripcion = item.cliec_vnombre_cliente;
                objE_AnaliticaDetalle.intUsuario = Valores.intUsuario;
                objE_AnaliticaDetalle.tarec_icorrelativo_tipo_analitica = Parametros.intTipoAnaliticaClientes;
                objE_AnaliticaDetalle.anad_nombre = item.cliec_vnombres;
                objE_AnaliticaDetalle.anad_apepaterno = item.cliec_vapellido_paterno;
                objE_AnaliticaDetalle.anad_apematerno = item.cliec_vapellido_materno;
                //objE_AnaliticaDetalle.tarec_icorrelativo_tipo_persona = item.iid_tipo_persona;
                new BContabilidad().insertarAnaliticaDetalle(objE_AnaliticaDetalle);
                prov.cliec_icod_cliente = item.cliec_icod_cliente;
                List<EAnaliticaDetalle> lstPanalitica = new List<EAnaliticaDetalle>();
                lstPanalitica = new BContabilidad().listarAnaliticaDetalleTodo();
                prov.anac_icod_analitica =lstPanalitica.Max(x=>x.anad_icod_analitica);
                new BVentas().ActualizarClienteAnalitica(prov);
                generarAnaliticaToolStripMenuItem.Enabled = false;

            }
        }
    }
}