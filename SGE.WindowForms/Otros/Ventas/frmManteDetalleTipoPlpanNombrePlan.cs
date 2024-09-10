using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmManteDetalleTipoPlpanNombrePlan : DevExpress.XtraEditors.XtraForm
    {
        public EPlanNecesidadSepultura obe;
        public List<EPlanNecesidadSepulturaDetalle> lista = new List<EPlanNecesidadSepulturaDetalle>();
        public frmManteDetalleTipoPlpanNombrePlan() => InitializeComponent();
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e) => Modificar();
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e) => Nuevo();
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) => Eliminar();
        private void frmManteDetalleTipoPlpanNombrePlan_Load(object sender, EventArgs e) => Cargar();

        public void Cargar()
        {
            lista = new BVentas().PlanNecisidadSepulturaDetalleListar(obe.id);
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }

        private void Nuevo()
        {

            var frm = new frmRegistroDetalleTipoPlpanNombrePlan();
            frm.obe.id_cab = obe.id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Cargar();
            }
        }
        private void Modificar()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepulturaDetalle;
            if (select is null) return;
            var frm = new frmRegistroDetalleTipoPlpanNombrePlan();
            frm.obe = select;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Cargar();
            }

        }

        private void Eliminar()
        {
            var select = viewLista.GetFocusedRow() as EPlanNecesidadSepulturaDetalle;
            if (select is null) return;
            select.intUsuario = Valores.intUsuario;
            new BVentas().PlanNecisidadSepulturaDetalleEliminar(select);
            Cargar();
        }


    }
}