using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class Frm13RegistroPrecioLista : XtraForm
    {
        private List<EPlanNecesidadSepultura> lista = new List<EPlanNecesidadSepultura>();
        private List<EPlanNecesidadSepulturaDetalle> detalle = new List<EPlanNecesidadSepulturaDetalle>();
        public Frm13RegistroPrecioLista() => InitializeComponent();
        private void Frm13RegistroPrecioLista_Load(object sender, EventArgs e) => CargaInicial();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => ModificarCabecera();
        private void exportarExeclToolStripMenuItem_Click(object sender, EventArgs e) => Services.ExportarExcel(grdReporte);
        private void viewLista_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e) => CargarDetalle();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => NuevoCabecera();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => EliminarCabecera();
        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e) => NuevoDetalle();
        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e) => ModificarDetalle();
        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e) => EliminarDetalle();
        private void viewLista_CellValueChanged(object sender, CellValueChangedEventArgs e) => GuardarCambiosCabecera();
        private void simpleButton1_Click(object sender, EventArgs e) => Reload();
        private void viewDetalle_CellValueChanged(object sender, CellValueChangedEventArgs e) => GuardarCambios();
        private void CargaInicial()
        {
            grdDetalle.Height = this.Height / 2;
            grdLista.Height = this.Height / 2;
            this.Refresh();
            BSControls.LoaderLookRepository(lkpgrdTipoSepultura, new BGeneral().listarTablaVentaDet((int)TipoSepultura.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpGrdTipoPlan, new BGeneral().listarTablaVentaDet((int)CodigoPlan.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpGrdNombrePlan, new BGeneral().listarTablaVentaDet((int)NombrePlan.Id), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            CargarLista();
            CargarDetalle();
            CargarReporte();
        }

        private void Reload()
        {
            CargarLista();
            CargarDetalle();
            CargarReporte();
        }

        private void CargarReporte()
        {
            DataTable data =  new BVentas().PlanNecisidadSepulturaReporte();
            grdReporte.DataSource = data;
            viewReporte.CustomColumnDisplayText += GridView1_CustomColumnDisplayText;
            viewReporte.BestFitColumns();
        }

        private void GridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            // Aquí puedes aplicar el formato específico a las columnas que desees
            GridView view = sender as GridView;
            if (view == null)
                return;

            if (e.Column.FieldName != "TipoSepultura" && e.Column.FieldName != "TipoPlan" && e.Column.FieldName != "NombrePlan")
            {
                e.Column.DisplayFormat.FormatString = "n2";
                e.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            }
        }
        private void CargarLista()
        {
            lista = new BVentas().PlanNecisidadSepulturaListar(0);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }
        private void NuevoCabecera()
        {
            var frm = new FrmManteTipoPlanNombrePlan();
            frm.Text = "Nuevo Registro";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarLista();
            }
        }
        private void ModificarCabecera()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            if (select is null) return;
            var frm = new FrmManteTipoPlanNombrePlan();
            frm.SetModify();
            frm.obe = select;
            frm.Text = "Nuevo Registro";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarLista();
            }
        }

        private void EliminarCabecera()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            if (select is null) return;
            select.intUsuario = Valores.intUsuario;
            new BVentas().PlanNecisidadSepulturaEliminar(select);
            Reload();
        }

        private void CargarDetalle()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            if (select is null) return;
            detalle = new BVentas().PlanNecisidadSepulturaDetalleListar(select.id);
            grdDetalle.DataSource = detalle;
            grdDetalle.RefreshDataSource();
        }

        private void NuevoDetalle()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            var frm = new frmRegistroDetalleTipoPlpanNombrePlan();
            frm.obe.id_cab = select.id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarDetalle();
            }
        }

        private void ModificarDetalle()
        {
            var select = viewDetalle.GetFocusedRow() as EPlanNecesidadSepulturaDetalle;
            if (select is null) return;
            var frm = new frmRegistroDetalleTipoPlpanNombrePlan();
            frm.obe = select;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarDetalle();
            }
        }

        private void EliminarDetalle()
        {
            var select = viewDetalle.GetFocusedRow() as EPlanNecesidadSepulturaDetalle;
            if (select is null) return;
            select.intUsuario = Valores.intUsuario;
            new BVentas().PlanNecisidadSepulturaDetalleEliminar(select);
            CargarDetalle();
        }

        private void GuardarCambiosCabecera()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepultura;
            if (select is null) return;
            select.intUsuario = Valores.intUsuario;
            new BVentas().PlanNecisidadSepulturaGuardar(select);
        }

        private void GuardarCambios()
        {

            var select = viewDetalle.GetFocusedRow() as EPlanNecesidadSepulturaDetalle;
            if (select is null) return;
            select.intUsuario = Valores.intUsuario;
            new BVentas().PlanNecisidadSepulturaDetalleSave(select);
        }


    }
}