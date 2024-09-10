using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Consultas_de_Ventas
{
    public partial class frm06ConsultaContratoFallecido : XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();
        
        public frm06ConsultaContratoFallecido() => InitializeComponent();
        private void frmAlamcen_Load(object sender, EventArgs e) => cargarControles();
        async void cargarControles()
        {
            dteFechaIncio.DateTime = new DateTime(Parametros.intEjercicio, 1, 1);
            dteFechaFinal.DateTime = DateTime.Now;
            cargar();
            var valorControles = await Task.WhenAll(ObtenerDatosControles());
            BSControls.LoaderLookRepository(lkpOrigenVenta, valorControles[0].Item1, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpCodigoPlan, valorControles[0].Item2, "tabvd_vdesc_abreviado", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpTipoSepultura, valorControles[0].Item3, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpPlataforma, valorControles[0].Item4, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpManzana, valorControles[0].Item5, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpVendedor, valorControles[0].Item6, "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLookRepository(lkpNombrePlan, valorControles[0].Item7, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpSituacion, new BGeneral().listarTablaVentaDet(14), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpTipoPago, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLookRepository(lkpSepultura, new BGeneral().listarTablaVentaDet(12), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);

        }


        private async Task<Tuple<object, object, object, object, object, object, object>> ObtenerDatosControles()
        {
            var uno = await Task.Run(() => new BGeneral().listarTablaVentaDet(1));
            var dos = await Task.Run(() => new BGeneral().listarTablaVentaDet(2));
            var tres = await Task.Run(() => new BGeneral().listarTablaVentaDet(3));
            var cuatro = await Task.Run(() => new BGeneral().listarTablaVentaDet(4));
            var cinco = await Task.Run(() => new BGeneral().listarTablaVentaDet(5));
            var seis = await Task.Run(() => new BVentas().listarVendedor());
            var siete = await Task.Run(() => new BGeneral().listarTablaVentaDet(13));
            return new Tuple<object, object, object, object, object, object, object>(uno, dos, tres, cuatro, cinco, seis, siete);
        }


        private async void cargar(bool todos = false)
        {
            enableLoading(true);
            if (todos)
                lstContrato = await Task.Run(() => new BVentas().listarContratoNuevo(Parametros.intContrato));
            else
                lstContrato = await Task.Run(() => new BVentas().ContratoFallecidoListarPorFechas(dteFechaIncio.DateTime, dteFechaFinal.DateTime, Parametros.intContrato));
            if (lstContrato.Exists(x=>x.cntc_icod_contrato == 93258))
            {
                var s = lstContrato.FirstOrDefault(x => x.cntc_icod_contrato == 93258);
            }
            grdContrato.DataSource = lstContrato;
            viewContrato.Focus();
            enableLoading(false);
        }
        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            mnuContrato.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }
      
        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            //viewContrato.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            //viewContrato.ClearColumnsFilter();
        }
        //private void filtrar()
        //{
        //    viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
        //    viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
        //    viewContrato.Columns["cntc_vdocumento_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdocumento_contratante] LIKE '%" + txtDNIContratante.Text + "%'");
        //}

        //private void txtNumContrato_KeyUp(object sender, KeyEventArgs e)=> filtrar();
        //private void txtNomContratante_KeyUp(object sender, KeyEventArgs e)=> filtrar();


        private void viewContrato_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["cntc_icod_situacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;

                }
            }
        }

        

        //private void txtDNIContratante_KeyUp(object sender, KeyEventArgs e)=> filtrar();

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            EContrato obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_flag_verificacion == true)
            {
                obe.cntc_flag_verificacion = false;
                new BVentas().modificarContratoVerificacion(obe);
                cargar();
            }
            else
            {
                obe.cntc_flag_verificacion = true;
                new BVentas().modificarContratoVerificacion(obe);
                cargar();
            }
        }

        private void button1_Click(object sender, EventArgs e)=> cargar();
        private void ckTodos_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckTodos.Checked)
            //    cargar(ckTodos.Checked);
            //dteFechaIncio.Enabled = !ckTodos.Checked;
            //dteFechaFinal.Enabled = !ckTodos.Checked;
            //btnBuscar.Enabled = !ckTodos.Checked;
        }

        private void btnBuscar_Click(object sender, EventArgs e) => cargar();
        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e) => Services.ExportarExcel(grd: grdContrato);
    }
}