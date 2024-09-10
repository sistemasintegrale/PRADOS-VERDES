using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using System.Linq;
using System.Security.Principal;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Tesoreria.Ventas;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmListarAsignacionVendedor : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        public List<EAsignacionVendedor> lstAsigVendedor = new List<EAsignacionVendedor>();
        public ECaja _Be { get; set; }
        public ECaja oBe = new ECaja();
        public List<ECaja> lista = new List<ECaja>();
        public FrmListarAsignacionVendedor()
        {
            InitializeComponent();
        }

       



        private void BuscarCriterio()
        {
            //string cod = txtCodigo.Text, desc = txtRazon.Text;
            //grdAsigVendedor.DataSource = lista.Where(obe => ((cod != string.Empty) ? obe.cliec_vcod_cliente.ToUpper().Contains(cod.ToUpper()) : obe.cliec_vnombre_cliente.ToUpper().Contains(desc.ToUpper()))).ToList();
        }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstAsigVendedor = new BVentas().listarAsignacionVendedor(oBe.cajac_icod_caja);
            grdAsigVendedor.DataSource = lstAsigVendedor;
            viewAsigVendedor.Focus();
        }



        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewAsigVendedor.FocusedRowHandle == 0)
                viewAsigVendedor.MoveLast();
            else
                viewAsigVendedor.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewAsigVendedor.FocusedRowHandle == viewAsigVendedor.RowCount - 1)
                viewAsigVendedor.MoveFirst();
            else
                viewAsigVendedor.MoveNext();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCodigo.ContainsFocus)
            {
                txtRazon.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRazon.ContainsFocus)
            {
                txtCodigo.Text = string.Empty;
                BuscarCriterio();
            }
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstAsigVendedor.FindIndex(x => x.asigc_icod_asignacion == intIcod);
            viewAsigVendedor.FocusedRowHandle = index;
            viewAsigVendedor.Focus();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteAsignacionVendedor frm = new FrmManteAsignacionVendedor();
            frm.MiEvento += new FrmManteAsignacionVendedor.DelegadoMensaje(reload);
            frm.oBe = oBe;
            frm.Show();
            frm.SetInsert();
            frm.setValues();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAsignacionVendedor obeAV = (EAsignacionVendedor)viewAsigVendedor.GetRow(viewAsigVendedor.FocusedRowHandle);
            FrmManteAsignacionVendedor frm = new FrmManteAsignacionVendedor();
            frm.MiEvento += new FrmManteAsignacionVendedor.DelegadoMensaje(reload);
            frm.lstAsigVendedor = lstAsigVendedor;
            frm.lista = lista;
            frm.oBe = oBe;
            frm.ObeAV = obeAV;
            frm.Show();
            frm.SetModify();
            frm.setValues();

        }
     
    }
}