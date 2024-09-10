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
using System.Security.Principal;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class Frm08AutorizacionUso : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EEspaciosAutorizacionUso> Lista;
        
        public Frm08AutorizacionUso()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Cargar();
        }

        private void FrmEstadoCuentaClientes_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }

        private void Cargar()
        {
            Lista = new BVentas().listarEspaciosAutorizacionUso();
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


        private void CobranzaDudosa_Click(object sender, EventArgs e)
        {

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
            List<EEspaciosDet> listado = (List<EEspaciosDet>)dgr.DataSource;

            if (listado.Count > 0)
            {
                rptControlSepultura rpt = new rptControlSepultura();
                rpt.cargar(listado, Parametros.intEjercicio.ToString());
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void reload(int intIcod)
        {
            Cargar();
            int index = Lista.FindIndex(x => x.espau_icod_autorizacion_uso == intIcod);
            view.FocusedRowHandle = index;
            view.Focus();
        }
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteAutorizacionUso frm = new frmManteAutorizacionUso();
            frm.MiEvento += new frmManteAutorizacionUso.DelegadoMensaje(reload);
            if (Lista.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:000000}", Lista.Max(x => Convert.ToInt32(x.espau_iid_autorizacion_uso) + 1));
            else
                frm.txtCodigo.Text = "000001";
            frm.lstDetalle = Lista;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EEspaciosAutorizacionUso Obe = (EEspaciosAutorizacionUso)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {

                frmManteAutorizacionUso frm = new frmManteAutorizacionUso();
                frm.MiEvento += new frmManteAutorizacionUso.DelegadoMensaje(reload);
                frm.oBe = Obe;
                frm.lstDetalle = Lista;
                frm.lkpNiveles.Enabled = false;
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EEspaciosAutorizacionUso Obe = (EEspaciosAutorizacionUso)view.GetRow(view.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {

                List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_isituacion == 14).ToList().Where(x => x.espad_iid_iespacios == Obe.espad_iid_iespacios).ToList();

                if (lstEspacioDet.Count > 0)
                {
                    throw new ArgumentException(String.Format("No puede ser Elminado, su Situacion es CON CONTRATO"));
                }


                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarEspaciosAutorizacionUso(Obe);
                    List<EEspaciosDet> lstNivelesDetMod = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x=> x.espad_iid_iespacios == Obe.espad_iid_iespacios).ToList();
                    lstNivelesDetMod.ForEach(x =>
                    {


                            x.espad_vnom_fallecido = "";
                            x.espad_vapellido_paterno_fallecido = "";
                            x.espad_vapellido_materno_fallecido = "";
                            x.espad_vdni_fallecido = "";
                            x.espad_sfecha_nac_fallecido = null;
                            x.espad_sfecha_fallecido = null;
                            x.espad_sfecha_entierro = null;
                            x.espad_inacionalidad = 0;
                            x.espad_icod_iestado = 15;
                            new BVentas().modificarEspaciosDetConsultas(x);

                    });

                    Cargar();
                    if (Lista.Count == 0)
                    {
                        Cargar();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}