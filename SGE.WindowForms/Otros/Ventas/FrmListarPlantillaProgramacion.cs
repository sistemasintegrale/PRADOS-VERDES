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
    public partial class FrmListarPlantillaProgramacion : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EPlantillaProgramacion> lstContrato = new List<EPlantillaProgramacion>();
        public EPlantillaProgramacion _Be { get; set; }

        public FrmListarPlantillaProgramacion()
        {
            InitializeComponent();
        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EPlantillaProgramacion)viewCliente.GetRow(viewCliente.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }


        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstContrato = new BVentas().listarPlantillaProgramacion();
            grd.DataSource = lstContrato;
            viewCliente.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstContrato.FindIndex(x => x.plap_icod_plantilla_programacion == intIcod);
            viewCliente.FocusedRowHandle = index;
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
            grd.DataSource = lstContrato.Where(x => x.plap_inumero_plantilla.ToString().Contains(txtNroPlantilla.Text)).ToList();
        }
    }
}