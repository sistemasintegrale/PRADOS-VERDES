using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmRegistroTipoSepulturaPlan : DevExpress.XtraEditors.XtraForm
    {
        public EContrato Obe = new EContrato();
        public FrmRegistroTipoSepulturaPlan()
        {
            InitializeComponent();
        }
        public void cargar()
        {
            BSControls.LoaderLook(lkpNombrePlan, new BGeneral().listarTablaVentaDet(13), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLook(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            txtNombreIEC.Text = Obe.strNombreIEC;
            txtNombre.Text = Obe.cntc_vnombre_contratante;
            txtApellidoPat.Text = Obe.cntc_vapellido_paterno_contratante;
            txtApellidoMat.Text = Obe.cntc_vapellido_materno_contratante;
            lkpNombrePlan.EditValue = Obe.cntc_icod_nombre_plan;
            lkpTipoSepultura.EditValue = Obe.cntc_itipo_sepultura;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Obe.cntc_icod_nombre_plan = Convert.ToInt32(lkpNombrePlan.EditValue);
            Obe.cntc_itipo_sepultura = Convert.ToInt32(lkpTipoSepultura.EditValue);
            new BVentas().SGEV_INGRESAR_PLAN_TIPO_SEPULTURA(Obe);
            DialogResult = DialogResult.OK;
        }
    }


}