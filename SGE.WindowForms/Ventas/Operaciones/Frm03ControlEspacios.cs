using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.bVentas;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class Frm03ControlEspacios : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        private List<EEspaciosDet> Lista;
        
        public Frm03ControlEspacios()
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
            Lista = new BVentas().listarEspaciosConsultas();
            Lista.ForEach(x =>
            {
                x.espad_sfecha_nac_fallecido = x.espad_sfecha_nac_fallecido == null ? (DateTime?)null :   x.espad_sfecha_nac_fallecido.Value.Year == 1 ? (DateTime?)null : x.espad_sfecha_nac_fallecido;
                x.espad_sfecha_fallecido = x.espad_sfecha_fallecido == null ? (DateTime?)null  : x.espad_sfecha_fallecido.Value.Year == 1 ? (DateTime?)null : x.espad_sfecha_fallecido;
                x.espad_sfecha_entierro = x.espad_sfecha_entierro == null ? (DateTime?)null : x.espad_sfecha_entierro.Value.Year == 1 ? (DateTime?)null : x.espad_sfecha_entierro;
                x.NumContrato = x.NumContrato == null ? "" : x.NumContrato;
            });
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

        private void imprimirLista_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
            //    Lista = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            //    rpt.cargar(Lista.Where(x => (x.doxcc_nmonto_saldo_soles + x.doxcc_nmonto_saldo_dolares) != 0).ToList(), Parametros.intEjercicio.ToString(), "");
               
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirConDocumentos_Click(object sender, EventArgs e)
        {
            //List<EDocXCobrar> listaTempCliente = new List<EDocXCobrar>();
            //if (Lista.Count > 0)
            //{
            //    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
            //    listaTempCliente = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            //    if (listaTempCliente.Count > 0)
            //    {
            //        rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(), false, "");
            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    List<EDocXCobrar> mlistaDoxcobrar = new List<EDocXCobrar>();

            //    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
            //    mlistaDoxcobrar = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            //    if (mlistaDoxcobrar.Count > 0)
            //    {
            //        rpt.cargar(mlistaDoxcobrar.Where(ob => ob.tablc_iid_situacion_documento != 10).ToList(), Parametros.intEjercicio.ToString(), false, "");
            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void imprimirListaDudosa_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    List<ECliente> listaTempCliente = new List<ECliente>();
            //    listaTempCliente = new BCuentasPorCobrar().ListarClientesSaldosCobranzaDudosa(Parametros.intEjercicio);


            //    if (listaTempCliente.Count > 0)
            //    {
            //        rptEstadoCuentaClienteLista rpt = new rptEstadoCuentaClienteLista();
            //        rpt.cargar(listaTempCliente, Parametros.intEjercicio.ToString(),"");
            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirConDocumentosDudosa_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    List<EDocXCobrar> mlistCuentasDoc = new List<EDocXCobrar>();
            //    mlistCuentasDoc = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(duda => duda.idTipodocuemnto == 19).ToList();

          

            //    if (mlistCuentasDoc.Count > 0)
            //    {
            //        rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
            //        rpt.cargar(mlistCuentasDoc, Parametros.intEjercicio.ToString(), false,"");
            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirSóloPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Lista.Count > 0)
            //{
            //    List<EDocXCobrar> mlista = new List<EDocXCobrar>();
            //    mlista = new BCuentasPorCobrar().EstadoCuentaDocumentos(Parametros.intEjercicio).Where(dxc => dxc.idTipodocuemnto == 19).ToList();

            //    if (mlista.Count > 0)
            //    {
            //    rptEstadoCuentaDocumentos rpt = new rptEstadoCuentaDocumentos();
            //    rpt.cargar(mlista, Parametros.intEjercicio.ToString(), true,"");
            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtNombre_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtNombre.ContainsFocus)
            //{
                //List<ECliente> listaTempCliente = new List<ECliente>();
                //listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
                //dgr.DataSource = listaTempCliente;
            //}
        }

     

        private void txtcodigo_EditValueChanged(object sender, EventArgs e)
        {
            //List<ECliente> listaTempCliente = new List<ECliente>();
            //listaTempCliente = Lista.Where(ob => ob.cliec_vnombre_cliente.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd()) && ob.cliec_vcod_cliente.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            //dgr.DataSource = listaTempCliente;
        }

        private void CobranzaDudosa_Click(object sender, EventArgs e)
        {

        }


        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEspaciosDet oBe_ = (EEspaciosDet)view.GetRow(view.FocusedRowHandle);
            if (oBe_ == null)
                return;
            using (frmManteEspaciosDetConsulta frm = new frmManteEspaciosDetConsulta())
            {
                frm.oBe = oBe_;
                frm.SetModify();
                frm.lstDetalle = Lista;
                if (oBe_.espad_icod_isituacion != 14)
                {
                    frm.txtNomApe.Enabled = false;
                    frm.dteFechaFallecmiento.Enabled = false;
                    frm.txtHora.Enabled = false;
                    frm.txtSolicitante.Enabled = false;
                    frm.txtNroDocumento.Enabled = false;
                }
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Lista = frm.lstDetalle;
                    view.RefreshData();
                    view.Focus();
                    Cargar();
                    //int index = Lista.FindIndex(x => x.espac_iid_iespacios == intIcod);
                    //viewEspacios.FocusedRowHandle = index;
                    //viewEspacios.Focus();
                }
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            view.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            view.ClearColumnsFilter();
        }
        private void filtrar() 
        {
            dgr.DataSource = Lista.Where(x => x.strplataforma.Contains(txtPlataforma.Text)
            && x.strmanzana.Contains(txtManzana.Text.ToUpper()) && x.strsepultura.Contains(txtSepultura.Text.ToUpper()) && x.NumContrato.Contains(txtNumContrato.Text)).ToList();
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

        private void txtNumContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {


                dgr.DataSource = Lista;
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    dgr.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    dgr.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                //grdContrato.DataSource = null;
                sfdRuta.FileName = string.Empty;



            }
        }
    }
}