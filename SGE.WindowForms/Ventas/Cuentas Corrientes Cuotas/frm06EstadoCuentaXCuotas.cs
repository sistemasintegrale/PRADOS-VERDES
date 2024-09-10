using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;
using System.Linq;
using SGE.WindowForms.Modules;
using System.Security.Principal;
using SGE.WindowForms.Otros.bVentas;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using SGI.WindowsForm.Otros.Ventas;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas;
using SGE.WindowForms.Ventas.Reporte;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes_Cuotas
{
    public partial class frm06EstadoCuentaXCuotas : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();

        public frm06EstadoCuentaXCuotas()
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
            grdContrato.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }
        private void cargar()
        {
            lstContrato = new BVentas().listarContrato(Parametros.intContrato);
            grdContrato.DataSource = lstContrato;
            viewContrato.Focus();
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstContrato.FindIndex(x => x.cntc_icod_contrato == intIcod);
            viewContrato.FocusedRowHandle = index;
            viewContrato.Focus();
        }


        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmMantePreContrato frm = new frmMantePreContrato();
            frm.MiEvento += new frmMantePreContrato.DelegadoMensaje(reload);
            frm.lstContrato = lstContrato;
            frm.SetInsert();
            frm.Show();
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es ANULADO "));

                frmMantePreContrato frm = new frmMantePreContrato();
                frm.MiEvento += new frmMantePreContrato.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstContrato = lstContrato;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.txtSerie.Focus();
                frm.btnEspacios.Enabled = false;
                frm.btnLimpiar.Enabled = true;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void eliminartoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Eliminado, su situación es ANULADO "));


                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarContrato(Obe);
                    cargar();
                    if (lstContrato.Count == 0)
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


        private void impSubfamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rptFamilia rpt = new rptFamilia();
            //rpt.cargar("RELACIÓN DE LINEAS DE PRODUCTOS", "", lstFamiliaCab);
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewContrato.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewContrato.ClearColumnsFilter();

            ///viewSubFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            ///viewSubFamilia.ClearColumnsFilter();
        }







        private void filtrar()
        {

            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
            viewContrato.Columns["strNombreContratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreContratante] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vdni_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdni_contratante] LIKE '%" + txtDNIContratante.Text + "%'");
        }

        private void txtNumContrato_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void txtNomContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                if (obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Anulado, su situación es ANULADO "));

                List<EEspaciosDet> lstEspacioDet = new BVentas().listarEspaciosDet(obe.espac_iid_iespacios).Where(x => x.espad_icod_iestado == 16).ToList();

                if (lstEspacioDet.Count > 0)
                {
                    throw new ArgumentException(String.Format("El Contrato no puede ser Anulado, su Estado es OCUPADO"));
                }
                if (XtraMessageBox.Show("¿Esta seguro que desea Anular Contrato?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    FrmDescripcionAnulacion frm = new FrmDescripcionAnulacion();
                    frm.obj = obe;
                    frm.SetInsert();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //flag_close = frm.flag_close;
                    }


                    obe.cntc_icod_situacion = 332;//Anulado
                    new BVentas().anularContrato(obe);
                    List<EEspaciosDet> lstNivelesDetMod = new BVentas().listarEspaciosDet(obe.espac_iid_iespacios);
                    lstNivelesDetMod.ForEach(x =>
                    {
                        if (x.espad_icod_isituacion == 14)// CON CONTATO
                        {
                            x.espad_icod_isituacion = 13;
                            new BVentas().actualizarEspaciosDet(x);
                        }
                    });
                    reload(obe.cntc_icod_contrato);
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void controlCuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteContratoCuotasConsulta frm = new frmManteContratoCuotasConsulta();
            frm.MiEvento += new frmManteContratoCuotasConsulta.DelegadoMensaje(reload);
            frm.ObeC = Obe;
            frm.txtDocumento.Text = Obe.cntc_vdocumento_contratante;
            frm.lblContratante.Text = Obe.strNombreContratante;
            frm.Show();
            frm.setValues();
            frm.btnGenerar.Enabled = false;

        }

        private void viewContrato_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string Indicador = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strIndicador"]);
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["strSituacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
                if (Indicador == "2")
                {
                    //modificartoolStripMenuItem5.Enabled = false;
                    //eliminartoolStripMenuItem6.Enabled = false;
                }
                else
                {
                    //modificartoolStripMenuItem5.Enabled = true;
                    //eliminartoolStripMenuItem6.Enabled = true;
                }
            }
        }

        private void consultarMotivoAnulacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (obe == null)
                return;
            FrmDescripcionAnulacion frm = new FrmDescripcionAnulacion();
            frm.obj = obe;
            frm.SetCancel();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //flag_close = frm.flag_close;
            }
        }

        private void txtDNIContratante_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            EContrato obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (obe == null)
                return;
            if (obe.cntc_flag_verificacion == true)
            {
                obe.cntc_flag_verificacion = false;
                new BVentas().modificarContratoVerificacion(obe);
                cargar();
            }
            else
            {
                obe.cntc_flag_verificacion = true;
                new BVentas().modificarContratoVerificacion(obe);
                cargar();
            }
        }

        private void viewContrato_DoubleClick(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es ANULADO "));

                frmMantePreContrato frm = new frmMantePreContrato();
                frm.MiEvento += new frmMantePreContrato.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstContrato = lstContrato;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
                frm.txtSerie.Focus();
                frm.btnEspacios.Enabled = false;
                frm.btnLimpiar.Enabled = true;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewContrato_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    if (Obe.strIndicador == "2")
            //    {
            //        modificartoolStripMenuItem5.Enabled = false;
            //        eliminartoolStripMenuItem6.Enabled = false;
            //    }
            //    else
            //    {
            //        modificartoolStripMenuItem5.Enabled = true;
            //        eliminartoolStripMenuItem6.Enabled = true;
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {


                grdContrato.DataSource = lstContrato;
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grdContrato.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grdContrato.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                //grdContrato.DataSource = null;
                sfdRuta.FileName = string.Empty;



            }
        }

        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EContrato> listado = (List<EContrato>)grdContrato.DataSource;

            if (listado.Count > 0)
            {
                rptEstadoCuentaXCuotas rpt = new rptEstadoCuentaXCuotas();
                rpt.cargar(lstContrato, Parametros.intEjercicio.ToString());
            }
            else
                XtraMessageBox.Show("No hay registro por Reportar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirCuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            List<EContratoCuotas> mlisdet = new BVentas().listarContratoCuotas(Obe.cntc_icod_contrato);

            var lstFomaFinanciamiento = Obetenerlstpagos(Obe.cntc_icod_contrato, Obe);

            

            //Obe.cntc_nmonto_foma = Obe.cntc_nmonto_foma - (lstFomaFinanciamiento.Where(x => x.pgs_itipo == Parametros.intTipoFoma)).FirstOrDefault().pgs_nmonto_pagado;
            //Obe.cntc_nfinanciamientro = Obe.cntc_nfinanciamientro - (lstFomaFinanciamiento.Where(x => x.pgs_itipo == Parametros.intTipoFinanciamiento)).FirstOrDefault().pgs_nmonto_pagado;

            mlisdet.ForEach(x =>
            {
                
                Tuple<string, string> tupla = new BVentas().ObtenerDocumentos(x.cntc_icod_contrato_cuotas);

                x.strfechaDocumentos = tupla.Item2;
                x.plnd_vnumero_doc = tupla.Item1;
                x.tipo = x.cntc_inro_cuotas == 0 ? "CI" : x.cntc_itipo_cuota > 0 ? "RP" + x.cntc_itipo_cuota : "P";
                x.tipo = x.cntc_icod_tipo_cuota == 5430 ? "P" : x.tipo;
            });

            var listaFomas = new BVentas().CuotaFomaListar(Obe.cntc_icod_contrato).FirstOrDefault();
            if (listaFomas != null)
            {
                listaFomas.intUsuario = Valores.intUsuario;
                listaFomas.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().CuotaFomaModificar(listaFomas);
                Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato); 
            }

            var Reprogramacion = new BVentas().ListarReprogramaciones(Obe.cntc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
            Obe.cntc_nfinanciamientro = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : Obe.cntc_nfinanciamientro;
            rptEstadoCuentaXCuotasDet rpt = new rptEstadoCuentaXCuotasDet();
            rpt.cargar(Obe, mlisdet);
            rpt.ShowPreview();
        }

        public List<EPagoFomaFinanciamiento> Obetenerlstpagos(int cntc_icod_contrato, EContrato Obe)
        {
            List<EPagoFomaFinanciamiento> lstpagos = new List<EPagoFomaFinanciamiento>();

            lstpagos = new BVentas().listarFomaFinanciamiento(cntc_icod_contrato, Obe);
            if (lstpagos.Count == 0)
            {
                //CREAMOS LA LISTA 

                var listTipoPagos = new BGeneral().listarTablaVentaDet(26);

                foreach (var item in listTipoPagos)
                {
                    EPagoFomaFinanciamiento obj = new EPagoFomaFinanciamiento();
                    obj.pgs_icod_contrato = Obe.cntc_icod_contrato;
                    obj.pgs_itipo = item.tabvd_iid_tabla_venta_det;
                    obj.pgs_sfecha_pago = (DateTime?)null;
                    obj.pgs_nmonto_pagado = 0;
                    obj.intusuario = Valores.intUsuario;
                    obj.pgs_vpc = WindowsIdentity.GetCurrent().Name;
                    obj.pgs_icod_pagos = new BVentas().FomaFinanciamientoInsertar(obj);
                }
                //VOLVEMOS A CARGAR
                lstpagos = Obetenerlstpagos(cntc_icod_contrato, Obe);
            }
            return lstpagos;

        }

        private void txtNumContrato_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void consultaNivelesFomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmMantePagoFomaMantenimiento frm = new FrmMantePagoFomaMantenimiento();
            frm.Text = string.Format("Foma/Financiamiento del Contrato : {0}", Obe.cntc_vnumero_contrato);
            frm.Obe = Obe;
            frm.cargarControles();
            frm.cargaLista();
            frm.SetValues();
            frm.setView();
            frm.ShowDialog();
        }

        private void grdContrato_Click(object sender, EventArgs e)
        {

        }
    }
}