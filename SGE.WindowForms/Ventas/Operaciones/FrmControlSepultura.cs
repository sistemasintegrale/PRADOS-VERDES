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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class FrmControlSepultura : DevExpress.XtraEditors.XtraForm
    {
        private List<EEspaciosDet> lstEspacios;

        public FrmControlSepultura()
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
            lstEspacios = new BVentas().Control_Sepultura_Listar();
            grdCepultura.DataSource = lstEspacios;
            viewSelpultura.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstEspacios.FindIndex(x => x.espac_iid_iespacios == intIcod);
            viewSelpultura.FocusedRowHandle = index;
            viewSelpultura.Focus();
        }




        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewSelpultura.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewSelpultura.ClearColumnsFilter();

        }

        private void viewCategoria_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }




        private void grdCategoria_Click(object sender, EventArgs e)
        {

        }

        private void filtrar()
        {
            filtarPlataforma();
            filtrarManzana();
            filtrarSepultura();
        }

        void filtarPlataforma()
        {
            ColumnView view = viewSelpultura;
            view.ActiveFilter.Add(view.Columns["strplataforma"],
            new ColumnFilterInfo("[strplataforma] Like '" + txtPlataforma.Text + "%'", ""));
        }

        void filtrarManzana()
        {
            ColumnView view = viewSelpultura;
            view.ActiveFilter.Add(view.Columns["strmanzana"],
            new ColumnFilterInfo("[strmanzana] Like '" + txtManzana.Text + "%'", ""));
        }

        void filtrarSepultura()
        {
            ColumnView view = viewSelpultura;
            view.ActiveFilter.Add(view.Columns["strsepultura"],
            new ColumnFilterInfo("[strsepultura] Like '" + txtSepultura.Text + "%'", ""));
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
                grdCepultura.DataSource = lstEspacios;
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdCepultura.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdCepultura.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
            }
        }
    }
}