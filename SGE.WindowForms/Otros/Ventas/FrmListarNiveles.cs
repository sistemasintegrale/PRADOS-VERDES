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
    public partial class FrmListarNiveles : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EEspaciosDet> Lista = new List<EEspaciosDet>();
        public EEspaciosDet _Be { get; set; }
        public int icodcontrato;
        public int icodEspacio;
        public int icodPlataforma;
        public int icodManzana;
        public int icodNroSepultura;
        public bool fromUbicaciones = false;

        public FrmListarNiveles()
        {
            InitializeComponent();
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EEspaciosDet)view.GetRow(view.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }


        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            if (fromUbicaciones)
            {
                Lista = new BVentas().listarEspaciosConsultas().Where(x => x.espac_iid_iespacios == icodEspacio).ToList();
                Lista = Lista.Where(x => x.espad_icod_iestado == 15 && (x.cntc_icod_contrato == icodcontrato || x.cntc_icod_contrato == 0)).ToList();
            }
            else
            {
                Lista = new BVentas().listarEspaciosConsultasIcodContrato(icodcontrato);
            }
            dgr.DataSource = Lista;
            view.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = Lista.FindIndex(x => x.cntc_icod_contrato == intIcod);
            view.FocusedRowHandle = index;
            view.Focus();
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
            if (view.FocusedRowHandle == 0)
                view.MoveLast();
            else
                view.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (view.FocusedRowHandle == view.RowCount - 1)
                view.MoveFirst();
            else
                view.MoveNext();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            //grd.DataSource = Lista.Where(x => x.cntc_vnumero_contrato.Contains(txtNroContrato.Text)
            //&& x.cntc_vnombre_contratante.Contains(txtContratante.Text.ToUpper())).ToList();
        }

        private void view_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}