using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmManteTipoPlanNombrePlan : XtraForm
    {
 
        public EPlanNecesidadSepultura obe = new EPlanNecesidadSepultura();
        public FrmManteTipoPlanNombrePlan() => InitializeComponent();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => SetSave();
        private void FrmManteTipoPlanNombrePlan_Load(object sender, EventArgs e) => CargarInicial();
        private void SetSave()
        {

            obe.icod_tipo_sepultura = Convert.ToInt32(lkpTipoSepultura.EditValue);
            obe.icod_tipo_plan = Convert.ToInt32(lkpCodigoPlan.EditValue);
            obe.icod_nombre_plan = Convert.ToInt32(lkpNombrePLan.EditValue);
            obe.nprecio_lista = Convert.ToDecimal(txtPrecioLista.Text);
            obe.ncuota_inicial = Convert.ToDecimal(txtCuotaInicial.Text);
            obe.nmonto_descuento = Convert.ToDecimal(txtDescuento.Text);
            if (new BVentas().PlanNecisidadSepulturaValidar(obe))
            {
                Services.MessageError("Ya existe registro con los mismo datos");
                return;
            }
            new BVentas().PlanNecisidadSepulturaGuardar(obe);
            this.DialogResult = DialogResult.OK;

        }

        private void SetValues()
        {
            lkpCodigoPlan.EditValue = obe.icod_tipo_plan;
            lkpNombrePLan.EditValue = obe.icod_nombre_plan;
            lkpTipoSepultura.EditValue = obe.icod_tipo_sepultura;
            txtPrecioLista.Text = obe.nprecio_lista.ToString();
            txtCuotaInicial.Text = obe.ncuota_inicial.ToString();
            txtDescuento.Text = obe.nmonto_descuento.ToString();
        }
        private void CargarInicial()
        {
            BSControls.LoaderLook(lkpCodigoPlan, new BGeneral().listarTablaVentaDet((int)CodigoPlan.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpNombrePLan, new BGeneral().listarTablaVentaDet((int)NombrePlan.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet((int)TipoSepultura.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            if (obe.id != 0) SetValues();
        }

        public void SetModify() {
            lkpCodigoPlan.ReadOnly = true;
            lkpNombrePLan.ReadOnly = true;
            lkpTipoSepultura.ReadOnly = true;
        }


    }
}