using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.bVentas;
using System.Linq;
using DevExpress.XtraEditors;
using SGE.WindowForms.Modules;
using SGI.WindowsForm.Otros.Ventas;
namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class FrmVendedor : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EVendedor> mlist = new List<EVendedor>();

        public FrmVendedor()
        {
            InitializeComponent();
        }


        private void FrmVendedor_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLookRepository(lkpgrdSituacion, new BGeneral().listarTablaRegistro(1), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            Carga();
        }

        private void Carga()
        {
            mlist = new BVentas().listarVendedor();
            dgrVendedor.DataSource = mlist.ToList();
        }

        private void BuscarCriterio()
        {
            if (Convert.ToInt32(LkpSituacion.EditValue) == 0)
            {
                dgrVendedor.DataSource = mlist.Where(obj =>
                                                       obj.vendc_iid_vendedor.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                       obj.vendc_vnombre_vendedor.ToUpper().Contains(txtNombre.Text.ToUpper())
                                                 ).ToList();
            }
            else
            {
                dgrVendedor.DataSource = mlist.Where(obj => obj.tablc_iid_situacion_vendedor == Convert.ToInt32(LkpSituacion.EditValue) &&
                                                       obj.vendc_iid_vendedor.ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                       obj.vendc_vnombre_vendedor.ToUpper().Contains(txtNombre.Text.ToUpper())
                                                 ).ToList();
            }
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }

        void form2_MiEvento()
        {
            Carga();
        }
        void Modify()
        {
            Carga();
            gridView1.FocusedRowHandle = xposition;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteVendedor frm = new FrmManteVendedor();
            frm.MiEvento += new FrmManteVendedor.DelegadoMensaje(reload);
            frm.oDetail = mlist;
            frm.SetInsert();
            int intMaxIdVendedor = mlist.Count == 0 ? 1 : (Convert.ToInt32(mlist.Max(x => x.vendc_iid_vendedor)) + 1);
            frm.txtIdVendedor.Text = intMaxIdVendedor >= 10 ? $"0{intMaxIdVendedor}" : $"00{intMaxIdVendedor}";
            frm.Show();
        }


        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EVendedor Obe = (EVendedor)gridView1.GetRow(gridView1.FocusedRowHandle);
            FrmManteVendedor frm = new FrmManteVendedor();
            frm.MiEvento += new FrmManteVendedor.DelegadoMensaje(reload);
            frm.oDetail = mlist;
            frm.oBe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();
            frm.txtIdVendedor.Enabled = false;
        }

        private void Datos()
        {
            FrmManteVendedor Vendedor = new FrmManteVendedor();
            Vendedor.MiEvento += new FrmManteVendedor.DelegadoMensaje(reload);
            Vendedor.oDetail = mlist;
            Vendedor.SetModify();
            Vendedor.Show();

            EVendedor Obe = (EVendedor)gridView1.GetRow(gridView1.FocusedRowHandle);
            Vendedor.Correlative = Obe.vendc_icod_vendedor;
            Vendedor.txtIdVendedor.Text = Obe.vendc_iid_vendedor;
            Vendedor.LkpSituacion.EditValue = Obe.tablc_iid_situacion_vendedor;
            Vendedor.txtNombre.Text = Obe.vendc_vnombre_vendedor;
            Vendedor.txtDireccion.Text = Obe.vendc_vdireccion;
            Vendedor.txtTelefono.Text = Obe.vendc_vnumero_telefono;
            Vendedor.txtDNI.Text = Obe.vendc_cnumero_dni;

            xposition = gridView1.FocusedRowHandle;
        }

        void reload(int intIcod)
        {
            Carga();
            int index = mlist.FindIndex(x => x.vendc_icod_vendedor == intIcod);
            gridView1.FocusedRowHandle = index;
            gridView1.Focus();
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                EVendedor Obe = (EVendedor)gridView1.GetRow(gridView1.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = gridView1.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Está seguro de eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {

                    //obl.EliminarPuntoVenta(ref eliminar, puvec_icod_punto_venta);
                    //lista = new BPuntoVenta().ListarPuntoVenta();
                    //dgr.DataSource = lista;
                    new BVentas().eliminarVendedor(Obe);
                    Carga();
                    if (mlist.Count >= index + 1)
                        gridView1.FocusedRowHandle = index;
                    else
                        gridView1.FocusedRowHandle = index - 1;
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                FrmManteVendedor Vendedor = new FrmManteVendedor();
                Vendedor.MiEvento += new FrmManteVendedor.DelegadoMensaje(reload);
                EVendedor Obe = (EVendedor)gridView1.GetRow(gridView1.FocusedRowHandle);
                Vendedor.Show();
                Vendedor.SetCancel();
                Vendedor.BtnGuardar.Enabled = false;
                Vendedor.Correlative = Obe.vendc_icod_vendedor;
                Vendedor.txtIdVendedor.Text = Obe.vendc_iid_vendedor;
                Vendedor.LkpSituacion.EditValue = Obe.tablc_iid_situacion_vendedor;
                Vendedor.txtNombre.Text = Obe.vendc_vnombre_vendedor;
                Vendedor.txtDireccion.Text = Obe.vendc_vdireccion;
                Vendedor.txtTelefono.Text = Obe.vendc_vnumero_telefono;
                Vendedor.txtDNI.Text = Obe.vendc_cnumero_dni;
            }
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
                nuevoToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F5)
                modificarToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.F9)
                eliminarToolStripMenuItem_Click(null, null);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<EVendedor> listado = (List<EVendedor>)dgrVendedor.DataSource;
            //if (listado.Count > 0)
            //{
            //    rptVendedor rpt = new rptVendedor();
            //    rpt.cargar(listado.OrderBy(or => or.vendc_vnombre_vendedor).ToList(), Valores.año.ToString());
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void porCódigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<EVendedor> listado = (List<EVendedor>)dgrVendedor.DataSource;
            //if (listado.Count > 0)
            //{
            //    rptVendedor rpt = new rptVendedor();
            //    rpt.cargar(listado.OrderBy(or => or.vendc_iid_vendedor).ToList(), Parametros.intPeriodo.ToString());
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void porNombresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<EVendedor> listado = (List<EVendedor>)dgrVendedor.DataSource;
            //if (listado.Count > 0)
            //{
            //    rptVendedor rpt = new rptVendedor();
            //    rpt.cargar(listado.OrderBy(or => or.vendc_vnombre_vendedor).ToList(), Parametros.intPeriodo.ToString());
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void LkpSituacion_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {

        }



    }
}