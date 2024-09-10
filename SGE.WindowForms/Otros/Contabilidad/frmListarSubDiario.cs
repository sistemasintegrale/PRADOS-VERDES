using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using System.Linq;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmListarSubDiario : DevExpress.XtraEditors.XtraForm
    {
        List<ESubDiario> lstSubDiario = new List<ESubDiario>();
        public ESubDiario _Be { get; set; }
        public frmListarSubDiario()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstSubDiario = new BContabilidad().listarSubDiario();
            grdSubDiario.DataSource = lstSubDiario;
            viewSubDiario.Focus();
        }
        private void returnSeleccion()
        {
            if (lstSubDiario.Count > 0)
            {
                _Be = (ESubDiario)viewSubDiario.GetRow(viewSubDiario.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }       
        private void buscarCriterio()
        {
            grdSubDiario.DataSource = lstSubDiario.Where(x =>
                                                   x.subdi_icod_subdiario.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.subdi_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }      

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }     
    }
}