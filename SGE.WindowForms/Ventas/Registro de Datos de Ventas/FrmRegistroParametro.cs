using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.bVentas;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class FrmRegistroParametro : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        public List<ERegistroParametro> lista = new List<ERegistroParametro>();

        void Carga()
        {
            lista = new BVentas().listarRegistroParametro();
            dgr.DataSource = lista;
        }

        private void BuscarCriterio()
        {
            dgr.DataSource = lista.Where(obj =>
                                                   string.Format("{0:00}", obj.rgpmc_vcod_registro_parametro).Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.rgpmc_vdescripcion.ToUpper().Contains(txtGiro.Text.ToUpper())
                                             ).ToList();
        }

        public FrmRegistroParametro()
        {
            InitializeComponent();
        }

        private void FrmPuntoVenta_Load(object sender, EventArgs e)
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


            FrmManteRegistroParametro frm = new FrmManteRegistroParametro();
            frm.MiEvento += new FrmManteRegistroParametro.DelegadoMensaje(reload);
            if (lista.Count > 0)
                frm.txtCodigoPuntoVenta.Text = String.Format("{0:00}", lista.Max(x => Convert.ToInt32(x.rgpmc_vcod_registro_parametro) + 1));
            else
                frm.txtCodigoPuntoVenta.Text = "01";
            frm.lista = lista;
            frm.SetInsert();
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
        }
        private void Datos()
        {
            ERegistroParametro Obe = (ERegistroParametro)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (Obe != null)
            {

                FrmManteRegistroParametro frm = new FrmManteRegistroParametro();
                frm.MiEvento += new FrmManteRegistroParametro.DelegadoMensaje(reload);
                frm.lista = lista;
                frm.oBe = Obe;
                frm.Show();
                frm.setValues();
                frm.SetModify();

            }
        }
        void reload(int intIcod)
        {
            Carga();
            int index = lista.FindIndex(x => x.rgpmc_icod_registro_parametro == intIcod);
            gridView1.FocusedRowHandle = index;
            gridView1.Focus();
        }

        private void eliminar()
        {
            //VeificarMovimientos oblVeriMov = new VeificarMovimientos();
            int contador = 0;
            int? eliminar = 0;
            if (lista.Count > 0)
            {
                ERegistroParametro Obe = (ERegistroParametro)gridView1.GetRow(gridView1.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = gridView1.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Está seguro de eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {

                    //obl.EliminarPuntoVenta(ref eliminar, puvec_icod_punto_venta);
                    //lista = new BPuntoVenta().ListarPuntoVenta();
                    //dgr.DataSource = lista;
                    new BVentas().eliminarRegistroParametro(Obe);
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                FrmManteRegistroParametro PuntoVenta = new FrmManteRegistroParametro();
                PuntoVenta.MiEvento += new FrmManteRegistroParametro.DelegadoMensaje(reload);
                ERegistroParametro Obe = (ERegistroParametro)gridView1.GetRow(gridView1.FocusedRowHandle);
                PuntoVenta.Show();
                PuntoVenta.SetCancel();
                PuntoVenta.btnGuardar.Enabled = false;
                PuntoVenta.Correlative = Obe.rgpmc_icod_registro_parametro;
                PuntoVenta.txtCodigoPuntoVenta.Text = (Obe.rgpmc_vcod_registro_parametro).ToString();
                PuntoVenta.lkpsituacion.EditValue = Obe.tabl_iid_situacion;
                PuntoVenta.txtPuntoVenta.Text = Obe.rgpmc_vdescripcion;
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
            imprimir();
        }
        private void imprimir()
        {
            //if (lista.Count > 0)
            //{
            //    rptPuntoVenta rpt = new rptPuntoVenta();
            //    rpt.cargar(lista);
            //}
        }

        private void brnagregar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnmosdificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Datos();
        }

        private void btneliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void btnimprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }
    }
}