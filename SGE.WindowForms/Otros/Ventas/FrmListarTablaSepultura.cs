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
    public partial class FrmListarTablaSepultura : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<ETablaVentaDet> lstTablaSepultura = new List<ETablaVentaDet>();
        public ETablaVentaDet _Be { get; set; }

        public FrmListarTablaSepultura()
        {
            InitializeComponent();
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (ETablaVentaDet)viewVendedor.GetRow(viewVendedor.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void BuscarCriterio()
        {
            string cod = txtCodigo.Text;
            grd.DataSource = lstTablaSepultura.Where(obe => obe.tabvd_vdescripcion.Contains(cod.ToUpper())).ToList();
            }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstTablaSepultura = new BGeneral().listarTablaVentaDet(12);
            grd.DataSource = lstTablaSepultura.ToList();
            viewVendedor.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstTablaSepultura.FindIndex(x => x.tabvd_iid_tabla_venta_det == intIcod);
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
                BuscarCriterio();
            }
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}