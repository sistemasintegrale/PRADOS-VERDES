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
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Reportes.Almacen.Registros;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frm02RegistroReportesVentas : DevExpress.XtraEditors.XtraForm
    {
        List<EReporteVentas> lstReporte = new List<EReporteVentas>();

        public frm02RegistroReportesVentas()
        {
            InitializeComponent();
        }

        private void cargarGridSize()
        {
            grdReporteVentas.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstReporte = new BVentas().listarReporteVentas();
            grdReporteVentas.DataSource = lstReporte;
            viewReporteVentas.Focus();
        }        

        void reload(int intIcod)
        {
            cargar();
            int index = lstReporte.FindIndex(x => x.revec_icod_reporte_ventas == intIcod);
            viewReporteVentas.FocusedRowHandle = index;
            viewReporteVentas.Focus();
        }
        
       
        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteReporteVentas frm = new frmManteReporteVentas();
            frm.MiEvento += new frmManteReporteVentas.DelegadoMensaje(reload);
            if (lstReporte.Count > 0)
                frm.txtNroReporte.Text = String.Format("{0:00}", lstReporte.Max(x => x.revec_iid_reporte_ventas + 1));
            else
                frm.txtNroReporte.Text = "01";
            frm.lstReporteVentas = lstReporte;
            frm.SetInsert();
            frm.Show();
            frm.txtNroReporte.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EReporteVentas Obe = (EReporteVentas)viewReporteVentas.GetRow(viewReporteVentas.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteReporteVentas frm = new frmManteReporteVentas();
            frm.MiEvento += new frmManteReporteVentas.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstReporteVentas = lstReporte;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            
        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EReporteVentas Obe = (EReporteVentas)viewReporteVentas.GetRow(viewReporteVentas.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().eliminarReporteVentas(Obe);
                cargar();
                if (lstReporte.Count == 0)
                {
                    cargar();
                }
            }
        }
        #endregion

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            cargar();
            cargarGridSize();
        }
    }
}