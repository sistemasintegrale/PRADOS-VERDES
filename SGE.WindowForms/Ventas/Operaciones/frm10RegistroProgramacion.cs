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
    public partial class frm10RegistroProgramacion : DevExpress.XtraEditors.XtraForm
    {
        List<ERegistroProgramacion> lstEspacios = new List<ERegistroProgramacion>();

        public frm10RegistroProgramacion()
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
            grdEspacios.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstEspacios = new BVentas().listarRegistroProgramacion();
            grdEspacios.DataSource = lstEspacios;
            viewEspacios.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstEspacios.FindIndex(x => x.plap_icod_plantilla_programacion == intIcod);
            viewEspacios.FocusedRowHandle = index;
            viewEspacios.Focus();
        }


        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteRegistroProgramacion frm = new frmManteRegistroProgramacion();
            frm.MiEvento += new frmManteRegistroProgramacion.DelegadoMensaje(reload);
            if (lstEspacios.Count > 0)
                frm.txtNumero.Text = String.Format("{0:0000}", lstEspacios.Max(x => Convert.ToInt32(x.rp_inumero_registro_programacion) + 1));
            else
                frm.txtNumero.Text = "0001";
            frm.lstRegistroProgramacion = lstEspacios;
            frm.SetInsert();
            frm.Show();
            frm.txtNumero.Focus();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ERegistroProgramacion Obe = (ERegistroProgramacion)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {


                frmManteRegistroProgramacion frm = new frmManteRegistroProgramacion();
                frm.MiEvento += new frmManteRegistroProgramacion.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstRegistroProgramacion = lstEspacios;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.btnPlantilla.Enabled = false;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ERegistroProgramacion Obe = (ERegistroProgramacion)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                //List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_iestado == 16).ToList();
                //if (lstEspacioDet.Count > 0)
                //{
                //    throw new ArgumentException(String.Format("El Espacio no puede ser Eliminado, su Estado es OCUPADO"));
                //}

                //List<EEspaciosDet> lstEspacioDetSit = new BVentas().listarEspaciosDet(Obe.espac_iid_iespacios).Where(x => x.espad_icod_isituacion == 14).ToList();
                //if (lstEspacioDetSit.Count > 0)
                //{
                //    throw new ArgumentException(String.Format("El Espacio no puede ser Eliminado, su Situacion es CON CONTRATO"));
                //}

                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarRegistroProgramacion(Obe);
                    cargar();
                    if (lstEspacios.Count == 0)
                    {
                        cargar();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion



        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewEspacios.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewEspacios.ClearColumnsFilter();
        }


        private void imprimirtoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            EContrato ObeCO = (EContrato)viewEspacios.GetRow(viewEspacios.FocusedRowHandle);


            rptContratos rptNotaCredito = new rptContratos();

            rptNotaCredito.cargar(new BVentas().ContratoImpresion(ObeCO.cntc_icod_contrato));
            rptNotaCredito.ShowPreview();

        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {

            List<EZona> lstCalcularZona = new List<EZona>();
            List<EDistritoZona> lstCalcularDistrito = new List<EDistritoZona>();
            //List<EFamiliaDet> lstCalcularFamiliaDet = new List<EFamiliaDet>();

            lstCalcularZona = new BVentas().listarZona();
            cargar();



        }

        private void grdCategoria_Click(object sender, EventArgs e)
        {

        }

        private void filtrar()
        {
            //grdEspacios.DataSource = lstEspacios.Where(x => x.strplataforma.Contains(txtPlataforma.Text)
            //&& x.strmanzana.Contains(txtManzana.Text.ToUpper()) && x.strsepultura.Contains(txtSepultura.Text.ToUpper())).ToList();
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
    }
}