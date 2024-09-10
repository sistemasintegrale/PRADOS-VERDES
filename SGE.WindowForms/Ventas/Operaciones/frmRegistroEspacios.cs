using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Reportes.Almacen.Registros;
using DevExpress.XtraReports.UI;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frmRegistroEspacios : DevExpress.XtraEditors.XtraForm
    {
        List<EEspacios> lstEspacios = new List<EEspacios>();

        public frmRegistroEspacios()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            cargarGridSize();
        }

        private void cargarGridSize()
        {
            //grdEspacios.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstEspacios = new BVentas().listarEspacios();
            grdEspacios.DataSource = lstEspacios;
            viewEspacios.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstEspacios.FindIndex(x => x.espac_iid_iespacios == intIcod);
            viewEspacios.FocusedRowHandle = index;
            viewEspacios.Focus();
        }




        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewEspacios.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewEspacios.ClearColumnsFilter();

        }

        private void viewCategoria_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }




        private void grdCategoria_Click(object sender, EventArgs e)
        {

        }

        private void filtrar()
        {
            grdEspacios.DataSource = lstEspacios.Where(x => x.strplataforma.Contains(txtPlataforma.Text)
            && x.strmanzana.Contains(txtManzana.Text.ToUpper()) && x.strsepultura.Contains(txtSepultura.Text.ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtPlataforma_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtSepultura_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                grdEspacios.DataSource = lstEspacios;
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdEspacios.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdEspacios.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
            }
        }
    }
}