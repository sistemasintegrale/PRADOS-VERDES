using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using System.Security.Principal;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmListarFomaFinanciamiento : DevExpress.XtraEditors.XtraForm
    {
        List<EPagoFomaFinanciamiento> lstpagos = new List<EPagoFomaFinanciamiento>();
        public int cntc_icod_contrato;
        public EContrato Obe = new EContrato();
        public frmListarFomaFinanciamiento()
        {
            InitializeComponent();
        }

        private void frmListarFomaFinanciamiento_Load(object sender, EventArgs e)
        {
            lstpagos = new BVentas().listarFomaFinanciamiento(cntc_icod_contrato, Obe);
            if (lstpagos.Count == 0)
            {

                EPagoFomaFinanciamiento obj = new EPagoFomaFinanciamiento();
                obj.pgs_icod_contrato = Obe.cntc_icod_contrato;
                obj.pgs_itipo = Parametros.intTipoFinanciamiento;
                obj.pgs_sfecha_pago = (DateTime?)null;
                obj.pgs_nmonto_pagado = 0;
                obj.intusuario = Valores.intUsuario;
                obj.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                obj.pgs_icod_pagos = new BVentas().FomaFinanciamientoInsertar(obj);

                //VOLVEMOS A CARGAR
                lstpagos = new BVentas().listarFomaFinanciamiento(cntc_icod_contrato, Obe);
            }
            grdLista.DataSource = lstpagos.Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento).ToList();
            grdLista.RefreshDataSource();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPagoFomaFinanciamiento obj = (EPagoFomaFinanciamiento)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (obj == null)
                return;
            frmManteFomaFinanciamiento frm = new frmManteFomaFinanciamiento();
            frm.obj = obj;
            frm.Obe = Obe;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frmListarFomaFinanciamiento_Load(sender, e);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            EPagoFomaFinanciamiento obj = (EPagoFomaFinanciamiento)viewLista.GetRow(viewLista.FocusedRowHandle);
            if (obj == null)
                return;
            if (!string.IsNullOrWhiteSpace(obj.rc_vnumero))
            {
                modificarToolStripMenuItem.Enabled = false;
            }
            else
            {
                modificarToolStripMenuItem.Enabled = true;
            }
        }
    }
}