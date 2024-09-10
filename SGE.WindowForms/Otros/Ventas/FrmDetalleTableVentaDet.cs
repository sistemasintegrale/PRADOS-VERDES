using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmDetalleTableVentaDet : XtraForm
    {
        public ETablaVentaDet Obe;
        private List<EPlanNecesidadSepultura> lista = new List<EPlanNecesidadSepultura>();
        public FrmDetalleTableVentaDet() => InitializeComponent();
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => Eliminar();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Modificar();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => Nuevo();
        private void FrmDetalleTableVentaDet_Load(object sender, EventArgs e) => CargaIncial();

        private void Nuevo()
        {
            var frm = new FrmManteTipoPlanNombrePlan();
            frm.Text = $"Nuevo Registro - {Obe.tabvd_vdescripcion}";
        
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Cargar();
            }
        }

        private void Modificar()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            if (select is null) return;
            var frm = new FrmManteTipoPlanNombrePlan();
            frm.Text = $"Modificar Registro - {Obe.tabvd_vdescripcion}";
            frm.obe = select;
         
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Cargar();
            }
        }

        private void Eliminar()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            if (select is null) return;
            if (Services.MessageQuestion("Está seguro de eliminar el registro?") == DialogResult.No) return;
            new BVentas().PlanNecisidadSepulturaEliminar(select);
            Cargar();
        }

        private void CargaIncial()
        {
            BSControls.LoaderLookRepository(lkpGrdCodigoPlan, new BGeneral().listarTablaVentaDet((int)CodigoPlan.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpGrdNombrePlan, new BGeneral().listarTablaVentaDet((int)NombrePlan.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            Cargar();
        }
        private void Cargar()
        {
            lista = new BVentas().PlanNecisidadSepulturaListar(Obe.tabvd_iid_tabla_venta_det);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }
    }
}