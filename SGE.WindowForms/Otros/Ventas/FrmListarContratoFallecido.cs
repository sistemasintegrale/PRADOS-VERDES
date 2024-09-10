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
    public partial class FrmListarContratoFallecido : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EContratoFallecido> lstDetalle = new List<EContratoFallecido>();
        public EContratoFallecido _Be { get; set; }
        public int icodEspacio;
        public int icodContrato;
        public FrmListarContratoFallecido()
        {
            InitializeComponent();
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EContratoFallecido)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }


        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstDetalle = new BVentas().listarContratoFallecido(icodContrato);
            grdDetalle.DataSource = lstDetalle;
            viewDetalle.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstDetalle.FindIndex(x => x.cntc_icod_contrato == intIcod);
            viewDetalle.FocusedRowHandle = index;
            viewDetalle.Focus();   
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
            if (viewDetalle.FocusedRowHandle == 0)
                viewDetalle.MoveLast();
            else
                viewDetalle.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewDetalle.FocusedRowHandle == viewDetalle.RowCount - 1)
                viewDetalle.MoveFirst();
            else
                viewDetalle.MoveNext();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseEdit oBase = null;
            try
            {

                using (frmManteContratosFallecido frm = new frmManteContratosFallecido())
                {
                    frm.SetInsert();
                    frm.lstDetalle = lstDetalle;
                    frm.icodContrato = icodContrato;
                    frm.icodEspacio = icodEspacio;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        lstDetalle = frm.lstDetalle;
                        grdDetalle.DataSource = lstDetalle;
                        viewDetalle.RefreshData();
                        viewDetalle.Focus();
                        
                    }
                    cargar();
                }
            }
            catch (Exception ex)
            {
                if (oBase != null)
                {
                    oBase.Focus();
                    oBase.ErrorText = ex.Message;
                    oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoFallecido oBe_ = (EContratoFallecido)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteContratosFallecido frm = new frmManteContratosFallecido())
            {
                frm.Obe = oBe_;
                frm.SetModify();
                frm.lstDetalle = lstDetalle;
                frm.icodContrato = icodContrato;
                frm.icodEspacio = icodEspacio;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstDetalle = frm.lstDetalle;
                    viewDetalle.RefreshData();
                    viewDetalle.Focus();
                }
            }
        }

        private void txtNroContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }
        private void filtrar()
        {
            //grdDetalle.DataSource = lstContratoFallecido.Where(x => x.cntc_vnumero_contrato.Contains(txtNroContrato.Text)
            //&& x.cntc_vnombre_contratante.Contains(txtContratante.Text.ToUpper())).ToList();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContratoFallecido oBe_ = (EContratoFallecido)viewDetalle.GetRow(viewDetalle.FocusedRowHandle);
            if (oBe_ == null)
                return;
            new BVentas().eliminarContratoFallecido(oBe_);
            viewDetalle.RefreshData();
            cargar();
        }
    }
}