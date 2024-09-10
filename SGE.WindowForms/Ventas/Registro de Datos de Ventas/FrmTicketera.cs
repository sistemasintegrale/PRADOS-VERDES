using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;


namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class FrmTicketera : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        public List<ETicketera> lista = new List<ETicketera>();

        public FrmTicketera()
        {
            InitializeComponent();
        }
         void Carga()
        {
            lista = new BVentas().listarTicketera();
            dgrCaja.DataSource = lista;
        }
         void BuscarCriterio()
        {
            dgrCaja.DataSource = lista.Where(obj =>
                                                  string.Format("{0:00}", obj.tckc_vserie_impresora).Contains(txtCodigo.Text)).ToList();
        }


        private void FrmCaja_Load(object sender, EventArgs e)
        {
            Carga(); 
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
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
        private void nuevo()
        {
            FrmManteTicketera frm = new FrmManteTicketera();
            frm.MiEvento += new FrmManteTicketera.DelegadoMensaje(reload);
            frm.lista = lista;
            frm.SetInsert();
            if (lista.Count == 0)
            {
                frm.txtImpresora.Text = "001";
            }
            else
            {
                frm.txtImpresora.Text = String.Format("{0:000}", (lista.Max(x => Convert.ToInt32(x.tckc_vserie_impresora)) + 1));
            }
            frm.Show();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();       
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                Datos();
            }
            else
            {
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Datos()
        {
            ETicketera Obe = (ETicketera)gridView1.GetRow(gridView1.FocusedRowHandle);
            FrmManteTicketera frm = new FrmManteTicketera();
            frm.MiEvento += new FrmManteTicketera.DelegadoMensaje(reload);
            frm.lista = lista;
            frm.oBe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();
        }

        void reload(int intIcod)
        {
            Carga();
            int index = lista.FindIndex(x => x.tckc_icod_ticketera == intIcod);
            gridView1.FocusedRowHandle = index;
            gridView1.Focus();
        }

        private void eliminar()
        {
            if (lista.Count > 0)
            {
                ECaja Obe = (ECaja)gridView1.GetRow(gridView1.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = gridView1.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Está seguro de eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    new BVentas().eliminarCaja(Obe);
                    Carga();
                    if (lista.Count >= index + 1)
                        gridView1.FocusedRowHandle = index;
                    else
                        gridView1.FocusedRowHandle = index - 1;
                }
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                FrmManteTicketera Caja = new FrmManteTicketera();
                Caja.MiEvento += new FrmManteTicketera.DelegadoMensaje(reload);
                ETicketera Obe = (ETicketera)gridView1.GetRow(gridView1.FocusedRowHandle);
                Caja.Show();
                Caja.SetCancel();
                Caja.btnGuardar.Enabled = false;
                Caja.Correlative = Obe.tckc_icod_ticketera;
                Caja.txtImpresora.Text = Obe.tckc_inumero_impresora.ToString();
                Caja.txtSerieImpresora.Text = Convert.ToString(Obe.tckc_vserie_impresora);
                Caja.txtSerie.Text = Obe.tckc_vserie.ToString();
                Caja.txtCorrelativo.Text = Obe.tckc_vcorrelativo.ToString();

            }
            else
            {
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }
        private void imprimir()
        {
            //if (lista.Count > 0)
            //{
            //    rptCaja rptCa = new rptCaja();
            //    rptCa.cargar(lista);
            //}
        }

       

        private void btnagregar_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmodificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }


    }
}