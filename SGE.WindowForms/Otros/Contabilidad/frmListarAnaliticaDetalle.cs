using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Linq;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmListarAnaliticaDetalle : DevExpress.XtraEditors.XtraForm
    {
        private List<EAnaliticaDetalle> lstAnaliticaDetalle = new List<EAnaliticaDetalle>();
        public EAnaliticaDetalle _Be { get; set; }
        public int intTipoAnalitica = 0;
        
        public frmListarAnaliticaDetalle()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstAnaliticaDetalle = new BContabilidad().listarAnaliticaDetalle(intTipoAnalitica);
            grdAnaliticaDetalle.DataSource = lstAnaliticaDetalle;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstAnaliticaDetalle.FindIndex(x => x.anad_icod_analitica == intIcod);
            viewAnaliticaDetalle.FocusedRowHandle = index;
            viewAnaliticaDetalle.Focus();
        }

        private void returnSeleccion()
        {
            if (lstAnaliticaDetalle.Count > 0)
            {
                _Be = (EAnaliticaDetalle)viewAnaliticaDetalle.GetRow(viewAnaliticaDetalle.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void viewUsuario_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }

        private void buscarCriterio()
        {
            grdAnaliticaDetalle.DataSource = lstAnaliticaDetalle.Where(x =>
                                                   x.anad_iid_analitica.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.anad_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
    }
}