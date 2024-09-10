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

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frm02RegistroFunerarias : DevExpress.XtraEditors.XtraForm
    {
        List<EFunerarias> lstFuneraria = new List<EFunerarias>();
        public EFunerarias _Be { get; set; }
        public frm02RegistroFunerarias()
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
            grdFuneraria.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstFuneraria = new BVentas().listarFuneraria();
            grdFuneraria.DataSource = lstFuneraria;
            viewFuneraria.Focus();
        }        

        void reload(int intIcod)
        {
            cargar();
            int index = lstFuneraria.FindIndex(x => x.func_icod_funeraria == intIcod);
            viewFuneraria.FocusedRowHandle = index;
            viewFuneraria.Focus();
        }
        
       
        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteFunerarias frm = new frmManteFunerarias();
            frm.MiEvento += new frmManteFunerarias.DelegadoMensaje(reload);
            if (lstFuneraria.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:0000}", lstFuneraria.Max(x => Convert.ToInt32(x.func_iid_funeraria) + 1));
            else
                frm.txtCodigo.Text = "1";
            frm.lstFuneraria = lstFuneraria;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EFunerarias Obe = (EFunerarias)viewFuneraria.GetRow(viewFuneraria.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteFunerarias frm = new frmManteFunerarias();
            frm.MiEvento += new frmManteFunerarias.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstFuneraria = lstFuneraria;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            
        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EFunerarias Obe = (EFunerarias)viewFuneraria.GetRow(viewFuneraria.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().eliminarFuneraria(Obe);
                cargar();
                int count = 0;
                lstFuneraria.ForEach(x =>
                {
                    count++;
                    x.func_iid_funeraria = count;
                    new BVentas().modificarFuneraria(x);
                });
                cargar();
                if (lstFuneraria.Count == 0)
                {
                    cargar();
                }
            }
        }
        #endregion

        private void viewFuneraria_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
        private void DgAcept()
        {
            _Be = (EFunerarias)viewFuneraria.GetRow(viewFuneraria.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewFuneraria.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewFuneraria.ClearColumnsFilter();
        }
        private void filtrar()
        {
            grdFuneraria.DataSource = lstFuneraria.Where(x =>
                                                   x.func_iid_funeraria.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.func_vrazon_social.Contains(txtDescripcion.Text.ToUpper()) &&
                                                   x.func_cnumero_docum_fun.Contains(txtRUC.Text.ToUpper().Trim()) &&
                                                   x.func_vruc.Contains(txtDNI.Text.ToUpper())
                                             ).ToList();
        }
        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void imprimirtoolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            lstFuneraria.ForEach(x=> 
            {
                count++;
                x.func_iid_funeraria = count;
                new BVentas().modificarFuneraria(x);
            });
            cargar();
        }

        private void txtRUC_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtDNI_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {


                grdFuneraria.DataSource = lstFuneraria;
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdFuneraria.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdFuneraria.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                //grdContrato.DataSource = null;
                sfdRuta.FileName = string.Empty;



            }
        }
    }
}