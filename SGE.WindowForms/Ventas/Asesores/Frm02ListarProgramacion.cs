using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Modules;

namespace SGE.WindowForms.Ventas.Asesores
{
    public partial class Frm02ListarProgramacion : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EProgramacion> Lista;
        
        public Frm02ListarProgramacion()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Cargar();
        }

        private void FrmEstadoCuentaClientes_Load(object sender, EventArgs e)
        {
            dtFecha.EditValue = DateTime.Now;
            this.Cargar();
        }

        private void Cargar()
        {
            //Lista = new BVentas().listarProgramacion().Where(x=> x.rp_fecha == Convert.ToDateTime(dtFecha.EditValue)).ToList();
            Lista = new BVentas().listarProgramacion(Convert.ToDateTime(dtFecha.EditValue));
            dgr.DataSource = Lista;
        }
        

        private void todos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);
                
                FrmConsultarDocXCobrarCliente dxc = new FrmConsultarDocXCobrarCliente();
                dxc.MiEvento += new FrmConsultarDocXCobrarCliente.DelegadoMensaje(form2_MiEvento);
                dxc.eCliente = eCliente;
                dxc.filtro = false;
                dxc.mnu.Items[1].Visible = true;
                dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = view.FocusedRowHandle;                
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);            
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                ECliente eCliente = (ECliente)view.GetRow(view.FocusedRowHandle);
                FrmConsultarDocXCobrarCliente dxc = new FrmConsultarDocXCobrarCliente();
                dxc.MiEvento += new FrmConsultarDocXCobrarCliente.DelegadoMensaje(form2_MiEvento);
                dxc.eCliente = eCliente;
                dxc.filtro = true;
                dxc.mnu.Items[1].Visible = true;
                dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = view.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void EstadoCuenta_Click(object sender, EventArgs e)
        {
           
        }



        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EProgramacion oBe_ = (EProgramacion)view.GetRow(view.FocusedRowHandle);
            if (oBe_ == null)
                return;
            try
            {
                if (oBe_.rpd_icod_vendedor > 0)
                {                
                    if (oBe_.rpd_icod_vendedor != Valores.vendc_icod_vendedor)
                    {
                        throw new ArgumentException(String.Format("Acceso denegado para realizar programacion"));
                    }
                }

                using (frmManteProgramacion frm = new frmManteProgramacion())
                {
                    frm.oBe = oBe_;
                    frm.SetModify();
                    frm.lstDetalle = Lista;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Lista = frm.lstDetalle;
                        view.RefreshData();
                        view.Focus();
                        Cargar();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            view.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            view.ClearColumnsFilter();
        }
        private void filtrar()
        {
            //dgr.DataSource = Lista.Where(x => x.strplataforma.Contains(txtPlataforma.Text)
            //&& x.strmanzana.Contains(txtManzana.Text.ToUpper()) && x.strsepultura.Contains(txtSepultura.Text.ToUpper())).ToList();
        }
        private void txtPlataforma_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtManzana_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtSepultura_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<EEspaciosDet> listado = (List<EEspaciosDet>)dgr.DataSource;

            //if (listado.Count > 0)
            //{
            //    rptControlSepultura rpt = new rptControlSepultura();
            //    rpt.cargar(listado, Parametros.intEjercicio.ToString());
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dtFecha_EditValueChanged(object sender, EventArgs e)
        {
            Cargar();
        }

        private void dtFecha_Click(object sender, EventArgs e)
        {

        }
    }
}