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

namespace SGE.WindowForms.Otros.bVentas
{
    public partial class FrmListarGuiaRemision : DevExpress.XtraEditors.XtraForm
    {
        List<EGuiaRemision> lstFacturas = new List<EGuiaRemision>();
        public EGuiaRemision _Be { get; set; }

        public FrmListarGuiaRemision()
        {
            InitializeComponent();
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EGuiaRemision)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void BuscarCriterio()
        {
            
            grd.DataSource = lstFacturas.Where(x =>
                                                  x.remic_vnumero_remision.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                  x.remic_vnombre_destinatario.Contains(txtRazon.Text.ToUpper())
                                            ).ToList();
        }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstFacturas = new BVentas().listarGuiaRemision(Parametros.intEjercicio).Where(ob=>ob.tablc_iid_situacion_remision==218).ToList();//TODOS LOS GENERADOS
            grd.DataSource = lstFacturas;
            viewCliente.Focus();
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
            if (viewCliente.FocusedRowHandle == 0)
                viewCliente.MoveLast();
            else
                viewCliente.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewCliente.FocusedRowHandle == viewCliente.RowCount - 1)
                viewCliente.MoveFirst();
            else
                viewCliente.MoveNext();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCodigo.ContainsFocus)
            {
                txtRazon.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRazon.ContainsFocus)
            {
                txtCodigo.Text = string.Empty;
                BuscarCriterio();
            }
        }
     
    }
}