using DevExpress.XtraGrid.Columns;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Comon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class Frm11EntregaCertificadoPerpetuo : DevExpress.XtraEditors.XtraForm
    {
        public List<ECertificadoUsoPerpetuo> lista = new List<ECertificadoUsoPerpetuo>();
        public Frm11EntregaCertificadoPerpetuo() => InitializeComponent();
        private void Frm11EntregaCertificadoPerpetuo_Load(object sender, EventArgs e) => Iniciar();
        private void autorizarToolStripMenuItem_Click(object sender, EventArgs e) => autorizar();
        private void entregarToolStripMenuItem_Click(object sender, EventArgs e) => Entregar();
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) => ModificarAccion();
        private void txtNumero_EditValueChanged(object sender, EventArgs e) => Filtrar();
        private void simpleButton1_Click(object sender, EventArgs e) => refresh();
        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e) => Excel();
        private void anularToolStripMenuItem_Click(object sender, EventArgs e) => Anular();
        private void activarToolStripMenuItem_Click(object sender, EventArgs e) => Activar();
        public void Cargar()
        {
            lista = new BVentas().CetificadoUsoPerpetuolistarConContrato();
            grdLista.DataSource = lista;
            grdLista.RefreshDataSource();
        }

        public void Iniciar()
        {
            BSControls.LoaderLookRepository(lkpTipoSepultura, new BGeneral().listarTablaVentaDet(3), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpPlataforma, new BGeneral().listarTablaVentaDet(4), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpManzana, new BGeneral().listarTablaVentaDet(5), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpSepultura, new BGeneral().listarTablaVentaDet(12), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);
            BSControls.LoaderLookRepository(lkpSituacion, new BGeneral().listarTablaRegistro(101), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            Cargar();
        }

        void Reload(int icod)
        {
            Cargar();
            int index = lista.FindIndex(x => x.cup_icod == icod);
            viewLista.FocusedRowHandle = index;
            viewLista.Focus();
        }

        private void autorizar()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;
            Obe.cup_bautorizado = !Obe.cup_bautorizado;
            Obe.cup_isituacion = Obe.cup_bautorizado ? Parametros.Autorizado : Parametros.Generado;
            Obe.intUsuario = Valores.intUsuario;
            new BVentas().CetificadoUsoPerpetuoModificar(Obe);
            Reload(Obe.cup_icod);
        }


        public void Entregar()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;
            FrmFecha frm = new FrmFecha();
            frm.FechaAnterior = Obe.cup_sfecha_emision;
            frm.Text = $"Fecha de Entrega del CP N° {Obe.cup_vnumero}";
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            Obe.cup_sfecha_entrega = (DateTime?)frm.dtFecha.EditValue;
            Obe.cup_isituacion = Parametros.Entregado;
            new BVentas().CetificadoUsoPerpetuoModificar(Obe);
            Reload(Obe.cup_icod);
        }


        public void ModificarAccion()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;
            autorizarToolStripMenuItem.Text = Obe.cup_bautorizado ? "Desautorizar" : "Autorizar";
            entregarToolStripMenuItem.Enabled = Obe.cup_bautorizado;
            autorizarToolStripMenuItem.Enabled = Obe.cup_isituacion != Parametros.Entregado ? true : false;
            if (Obe.cup_isituacion != Parametros.Anulado)
            {
                anularToolStripMenuItem.Visible = true;
                activarToolStripMenuItem.Visible = false;
                autorizarToolStripMenuItem.Visible = true;
                entregarToolStripMenuItem.Visible = true;
            }
            else
            {
                anularToolStripMenuItem.Visible = false;
                activarToolStripMenuItem.Visible = true;
                autorizarToolStripMenuItem.Visible = false;
                entregarToolStripMenuItem.Visible = false;
            }
        }

        public void Filtrar()
        {
            viewLista.Columns["cup_vnumero"].FilterInfo = new ColumnFilterInfo("[cup_vnumero] LIKE '%" + txtNumero.Text + "%'");
        }
        public void refresh()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null)
                Cargar();
            else
                Reload(Obe.cup_icod);
        }


        private void Excel()
        {
           

            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog(this) == DialogResult.OK)
            {
                gridColumn14.FieldName = "strAutorizado";
                grdLista.DataSource = lista;
                grdLista.RefreshDataSource();
                if (saveFile.FileName.Contains(".xlsx"))
                {
                    grdLista.ExportToXlsx(saveFile.FileName);
                    Process.Start(saveFile.FileName);
                }
                else
                {
                    grdLista.ExportToXlsx(saveFile.FileName + ".xlsx");
                    Process.Start(saveFile.FileName + ".xlsx");

                }
                gridColumn14.FieldName = "cup_bautorizado";
                grdLista.DataSource = lista;
                grdLista.RefreshDataSource();
            }

            
        }

     
        private void Anular()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;
            Obe.cup_isituacion = Parametros.Anulado;
            Obe.intUsuario = Valores.intUsuario;
            new BVentas().CetificadoUsoPerpetuoModificar(Obe);
            Reload(Obe.cup_icod);
        }

        
        private void Activar()
        {
            var Obe = (ECertificadoUsoPerpetuo)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (Obe == null) return;            
            Obe.cup_isituacion = Obe.cup_bautorizado ? Parametros.Autorizado : Parametros.Generado;
            Obe.intUsuario = Valores.intUsuario;
            new BVentas().CetificadoUsoPerpetuoModificar(Obe);
            Reload(Obe.cup_icod);
        }
    }
}