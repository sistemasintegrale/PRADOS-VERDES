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
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Tesoreria.Caja
{
    public partial class FrmListarCajaTesoreria : DevExpress.XtraEditors.XtraForm
    {
        private List<ECajaChica> mlist = new List<ECajaChica>();
        public ECajaChica _Be { get; set; }
        public FrmListarCajaTesoreria()
        {
            InitializeComponent();
        }
        private void Carga()
        {
            mlist = new BTesoreria().ListarCajaChica();

            grdCajaChica.DataSource = mlist;
        }
        private void FrmListarCajaTesoreria_Load(object sender, EventArgs e)
        {
            Carga();
        }
        private void BuscarCriterio()
        {
            grdCajaChica.DataSource = mlist.Where(obj =>
                                                   obj.vdescrip_caja_liquida.ToUpper().Contains(txtDescripcion.Text.ToUpper()) &&
                                                   obj.vnro_caja_liquida.ToUpper().Contains(txtNroCaja.Text.ToUpper())
                                             ).ToList();

        }

        private void txtNroCaja_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarCriterio();
        }
        private void DgAcept()
        {
            _Be = (ECajaChica)viewCajaChica.GetRow(viewCajaChica.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }
        private void viewCajaChica_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void viewCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DgAcept();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}