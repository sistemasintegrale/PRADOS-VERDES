using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmFacCompraPresupuesto : DevExpress.XtraEditors.XtraForm
    {
        public List<EFacturaCompra> lstFacCompra = new List<EFacturaCompra>();
        public int prep_cod_presupuesto = 0;

        public frmFacCompraPresupuesto()
        {
            InitializeComponent();
        }

        private void frm08DocCompra_Load(object sender, EventArgs e)
        {
            //cargar();
        }

        public void cargar()
        {
          
            grdDocCompra.DataSource = lstFacCompra;
          
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstFacCompra.FindIndex(x => x.fcoc_icod_doc == intIcod);
            viewDocCompra.FocusedRowHandle = index;
            viewDocCompra.Focus();
        }

        private void filtrar()
        {
                grdDocCompra.DataSource = lstFacCompra.Where(
                    x => x.fcoc_num_doc.Contains(txtNroDoc.Text)
                        && x.strProveedor.Contains(txtProveedor.Text.ToUpper())
                    ).ToList();
        }      

        private void txtNroDoc_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
            
        }
        private void txtProveedor_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewDocCompra.FocusedRowHandle = 1;
            txtNroDoc.Focus();
            this.DialogResult = DialogResult.OK;
        }

       
    }
}