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
    public partial class FrmListarVendedor : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EVendedor> lstVendedor = new List<EVendedor>();
        public EVendedor _Be { get; set; }

        public FrmListarVendedor()
        {
            InitializeComponent();
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EVendedor)viewVendedor.GetRow(viewVendedor.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void BuscarCriterio()
        {
            string cod = txtCodigo.Text, desc = txtRazon.Text;
            grd.DataSource = lstVendedor.Where(obe => ((cod != string.Empty) ? obe.vendc_iid_vendedor.ToUpper().Contains(cod.ToUpper()) : obe.vendc_vnombre_vendedor.ToUpper().Contains(desc.ToUpper()))).ToList();
        }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstVendedor = new BVentas().listarVendedor();
            grd.DataSource = lstVendedor.ToList();
            viewVendedor.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstVendedor.FindIndex(x => x.vendc_icod_vendedor == intIcod);
            viewVendedor.FocusedRowHandle = index;
            viewVendedor.Focus();   
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewVendedor.FocusedRowHandle == 0)
                viewVendedor.MoveLast();
            else
                viewVendedor.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewVendedor.FocusedRowHandle == viewVendedor.RowCount - 1)
                viewVendedor.MoveFirst();
            else
                viewVendedor.MoveNext();
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
    }
}