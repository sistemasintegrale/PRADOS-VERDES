using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.Entity;
using System;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmRegistroDetalleTipoPlpanNombrePlan : XtraForm
    {
        public frmRegistroDetalleTipoPlpanNombrePlan()
        {
            InitializeComponent();
        }

        public EPlanNecesidadSepulturaDetalle obe = new EPlanNecesidadSepulturaDetalle();

        private void barButtonItem1_ItemClick(object sender,  ItemClickEventArgs e) => Dispose();

        private void barButtonItem2_ItemClick(object sender,  ItemClickEventArgs e) => SetSave();


        private void SetSave()
        {
            obe.nmonto = Convert.ToDecimal(txtMonto.Text);
            obe.icantidad_cuotas = Convert.ToInt32(txtCantidad.Text);
            obe.intUsuario = Valores.intUsuario;
            new BVentas().PlanNecisidadSepulturaDetalleSave(obe);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmRegistroDetalleTipoPlpanNombrePlan_Load(object sender, EventArgs e)
        {
            if (obe.id != 0)
            {
                txtCantidad.Text = obe.icantidad_cuotas.ToString();
                txtMonto.Text = obe.nmonto.ToString();
            }
        }
    }
}