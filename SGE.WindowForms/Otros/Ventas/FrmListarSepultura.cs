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
using SGE.WindowForms.Modules;
using System.Threading.Tasks;

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmListarSepultura : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EEspacios> lstSepultura = new List<EEspacios>();
        public EEspacios _Be { get; set; }

        public FrmListarSepultura()
        {
            InitializeComponent();
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EEspacios)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
            if (_Be != null)
            {
                _Be = new BVentas().EspaciosGetById(_Be.espac_iid_iespacios);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BuscarCriterio()
        {
            string platafoma = Convert.ToInt32(lkpPlataforma.EditValue) == 0 ? "" : lkpPlataforma.EditValue.ToString();
            string manzana = Convert.ToInt32(lkpManzana.EditValue) == 0 ? "" : lkpManzana.EditValue.ToString();
            string sepultura = Convert.ToInt32(lkpSepultura.EditValue) == 0 ? "" : lkpSepultura.EditValue.ToString();
            viewEspacios.Columns["espac_icod_iplataforma"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[espac_icod_iplataforma] LIKE '%" + platafoma + "%'");
            viewEspacios.Columns["espac_icod_imanzana"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[espac_icod_imanzana] LIKE '%" + manzana + "%'");
            viewEspacios.Columns["espac_isepultura"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[espac_isepultura] LIKE '%" + sepultura + "%'");
        }

        private async void FrmListarCliente_Load(object sender, EventArgs e)
        {

            var data = await Task.WhenAll(ObtenerDatosControles());
            BSControls.LoaderLookRepository(grdlkpTipoSepultura, data[0].Item1, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(grdlkpPlataforma, data[0].Item2, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(grdlkpManzana, data[0].Item3, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);

            var combo = data[0].Item1;
            combo.Insert(0, new ETablaVentaDet { tabvd_vdescripcion = "Todos", tabvd_iid_tabla_venta_det = 0 });
            BSControls.LoaderLook(lkpSepultura, combo, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            var combo2 = data[0].Item2;
            combo2.Insert(0, new ETablaVentaDet { tabvd_vdescripcion = "Todos", tabvd_iid_tabla_venta_det = 0 });
            BSControls.LoaderLook(lkpPlataforma, combo2, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            var combo3 = data[0].Item3;
            combo3.Insert(0, new ETablaVentaDet { tabvd_vdescripcion = "Todos", tabvd_iid_tabla_venta_det = 0 });
            BSControls.LoaderLook(lkpManzana, combo3, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            
            lstSepultura = data[0].Item4;
            grdEspacios.DataSource = lstSepultura;
            grdEspacios.RefreshDataSource();
        }

        public async Task<(List<ETablaVentaDet>, List<ETablaVentaDet>, List<ETablaVentaDet>, List<EEspacios>)> ObtenerDatosControles()
        {
            (List<ETablaVentaDet>, List<ETablaVentaDet>, List<ETablaVentaDet>, List<EEspacios>) valor = default;
            valor.Item1 = await Task.Run(() => new BGeneral().listarTablaVentaDet(12));
            valor.Item2 = await Task.Run(() => new BGeneral().listarTablaVentaDet(4));
            valor.Item3 = await Task.Run(() => new BGeneral().listarTablaVentaDet(5));
            valor.Item4 = await Task.Run(() => new BVentas().Espacios());
            return valor;
        }

        private void cargar()
        {
            //lstSepultura = new BVentas().listarEspacios();
            lstSepultura = new BVentas().Espacios();
            grdEspacios.DataSource = lstSepultura.ToList();
            viewEspacios.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstSepultura.FindIndex(x => x.espac_iid_iespacios == intIcod);
            viewEspacios.FocusedRowHandle = index;
            viewEspacios.Focus();
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
            if (viewEspacios.FocusedRowHandle == 0)
                viewEspacios.MoveLast();
            else
                viewEspacios.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewEspacios.FocusedRowHandle == viewEspacios.RowCount - 1)
                viewEspacios.MoveFirst();
            else
                viewEspacios.MoveNext();
        }




        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManteEspacios frm = new frmManteEspacios();
            frm.MiEvento += new frmManteEspacios.DelegadoMensaje(reload);
            if (lstSepultura.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:000000}", lstSepultura.Max(x => Convert.ToInt32(x.espac_icod_vespacios) + 1));
            else
                frm.txtCodigo.Text = "000001";
            frm.lstEspacios = lstSepultura;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEspacios Obe = (EEspacios)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteEspacios frm = new frmManteEspacios();
            frm.MiEvento += new frmManteEspacios.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstEspacios = lstSepultura;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }

        private void viewEspacios_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void lkpPlataforma_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void lkpSepultura_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private void lkpManzana_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }
    }
}