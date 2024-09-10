using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Otros.Ventas;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos;
using SGI.WindowsForm.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Operaciones
{
    public partial class frm02RegistroContratos : DevExpress.XtraEditors.XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();
        public bool nuevo = false;
        public frm02RegistroContratos()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargarControles();


        }

        async void cargarControles()
        {
            dteFechaIncio.DateTime = new DateTime(Parametros.intEjercicio, 1, 1);
            dteFechaFinal.DateTime = DateTime.Now;
            cargar();
            var valorControles = await Task.WhenAll(ObtenerDatosControles());
            BSControls.LoaderLookRepository(lkpOrigenVenta, valorControles[0].Item1, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpCodigoPlan, valorControles[0].Item2, "tabvd_vdesc_abreviado", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpTipoSepultura, valorControles[0].Item3, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpPlataforma, valorControles[0].Item4, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpManzana, valorControles[0].Item5, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpVendedor, valorControles[0].Item6, "vendc_vnombre_vendedor", "vendc_icod_vendedor", true);
            BSControls.LoaderLookRepository(lkpNombrePlan, valorControles[0].Item7, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpSituacion, new BGeneral().listarTablaVentaDet(14), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpTipoPago, new BGeneral().listarTablaRegistro(97), "tarec_vdescripcion", "tarec_iid_tabla_registro", false);
            BSControls.LoaderLookRepository(lkpSepultura, new BGeneral().listarTablaVentaDet(12), "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", false);

        }


        private async Task<Tuple<object, object, object, object, object, object, object>> ObtenerDatosControles()
        {
            var uno = await Task.Run(() => new BGeneral().listarTablaVentaDet(1));
            var dos = await Task.Run(() => new BGeneral().listarTablaVentaDet(2));
            var tres = await Task.Run(() => new BGeneral().listarTablaVentaDet(3));
            var cuatro = await Task.Run(() => new BGeneral().listarTablaVentaDet(4));
            var cinco = await Task.Run(() => new BGeneral().listarTablaVentaDet(5));
            var seis = await Task.Run(() => new BVentas().listarVendedor());
            var siete = await Task.Run(() => new BGeneral().listarTablaVentaDet(13));
            return new Tuple<object, object, object, object, object, object, object>(uno, dos, tres, cuatro, cinco, seis, siete);
        }


        private async void cargar(bool todos = false)
        {
            enableLoading(true);
            if (todos)
                lstContrato = await Task.Run(() => new BVentas().listarContratoNuevo(Parametros.intContrato));
            else
                lstContrato = await Task.Run(() => new BVentas().ContratoListarPorFechas(dteFechaIncio.DateTime, dteFechaFinal.DateTime, Parametros.intContrato));
            if (lstContrato.Exists(x=>x.cntc_icod_contrato == 93258))
            {
                var s = lstContrato.FirstOrDefault(x => x.cntc_icod_contrato == 93258);
            }
            grdContrato.DataSource = lstContrato;
            viewContrato.Focus();
            enableLoading(false);
        }
        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            mnuContrato.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }
        void reload(int intIcod)
        {
            if (nuevo)
            {
                var newContarto = new BVentas().listarContratoPorIcod(intIcod);
                newContarto.cntc_vdocumento_contratante = string.IsNullOrEmpty(newContarto.cntc_vdocumento_contratante) ? "" : newContarto.cntc_vdocumento_contratante;
                newContarto.strNombreCompleto = string.IsNullOrEmpty(newContarto.strNombreCompleto) ? "" : newContarto.strNombreCompleto;
                lstContrato.Add(newContarto);
                grdContrato.DataSource = lstContrato;
                grdContrato.RefreshDataSource();
                grdContrato.Refresh();

                viewContrato.RefreshData();
                int index = lstContrato.FindIndex(x => x.cntc_icod_contrato == intIcod);
                viewContrato.FocusedRowHandle = index;
                viewContrato.Focus();
            }
            else
            {
                EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
                Obe = new BVentas().listarContratoPorIcod(intIcod);
                viewContrato.RefreshData();
            }


        }


        #region Zona
        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            nuevo = true;
            frmManteContrato frm = new frmManteContrato();
            frm.MiEvento += new frmManteContrato.DelegadoMensaje(reload);
            frm.lstContrato = lstContrato;
            frm.SetInsert();
            frm.txtSerie.Properties.ReadOnly = false;
            frm.txtSerie.Focus();
            frm.Show();
            frm.txtSerie.Focus();
            frm.cargando = false;
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            nuevo = false;
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            try
            {
                if (Obe.cntc_icod_situacion == 332)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es ANULADO "));

                frmManteContrato frm = new frmManteContrato();
                frm.MiEvento += new frmManteContrato.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstContrato = lstContrato;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.txtSerie.Focus();
                frm.btnEspacios.Enabled = false;
                frm.btnLimpiar.Enabled = true;
                frm.txtSerie.Enabled = false;
                frm.txtNumer.Enabled = false;
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



        private void imprimirtoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            EContrato ObeCO = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (ObeCO == null)
                return;

            rptContratos rptPrincipal = new rptContratos();

            rptPrincipal.cargar(new BVentas().ContratoImpresion(ObeCO.cntc_icod_contrato));
            rptPrincipal.CreateDocument();

            var rpt2 = new rptContratoPag2();
            rpt2.CreateDocument();

            rptPrincipal.Pages.AddRange(rpt2.Pages);
            rptPrincipal.PrintingSystem.ContinuousPageNumbering = true;
            rptPrincipal.ShowPreview();

        }



        private void filtrar()
        {
            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vdocumento_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdocumento_contratante] LIKE '%" + txtDNIContratante.Text + "%'");
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
            Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            frmManteContratoCuotas frm = new frmManteContratoCuotas();
            frm.MiEvento += new frmManteContratoCuotas.DelegadoMensaje(reload);
            frm.ObeC = Obe;
            //frm.SetInsert();
            frm.Show();
            frm.setValues();
        }

        private void viewContrato_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["cntc_icod_situacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;

                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void consultarMotivoAnulacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (obe == null)
                return;
            FrmDescripcionAnulacion frm = new FrmDescripcionAnulacion();
            frm.obj = new BVentas().listarContratoPorIcod(obe.cntc_icod_contrato);
            frm.SetModify();
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

        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void txtDNIContratante_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void atualizarFllecidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstContrato.ForEach(x =>
            {
                if (x.cntc_vnombre_fallecido != "")
                {
                    EContratoFallecido Obe = new EContratoFallecido();

                    Obe.cntc_icod_contrato = x.cntc_icod_contrato;
                    Obe.cntc_vnombre_fallecido = x.cntc_vnombre_fallecido;
                    Obe.cntc_vapellido_paterno_fallecido = x.cntc_vapellido_paterno_fallecido;
                    Obe.cntc_vapellido_materno_fallecido = x.cntc_vapellido_materno_fallecido;
                    Obe.cntc_vdni_fallecido = x.cntc_vdni_fallecido;
                    if (x.cntc_sfecha_nac_fallecido == null || x.cntc_sfecha_nac_fallecido.ToString() == "" || x.cntc_sfecha_nac_fallecido.ToString().Substring(0, 10) == "01/01/0001")
                    {
                        Obe.cntc_sfecha_fallecimiento = (DateTime?)null;
                    }
                    else
                    {
                        Obe.cntc_sfecha_nac_fallecido = x.cntc_sfecha_nac_fallecido;
                    }

                    Obe.cntc_inacionalidad = x.cntc_inacionalidad;
                    if (x.cntc_sfecha_fallecimiento == null || x.cntc_sfecha_fallecimiento.ToString() == "" || x.cntc_sfecha_fallecimiento.ToString().Substring(0, 10) == "01/01/0001")
                    {
                        Obe.cntc_sfecha_fallecimiento = (DateTime?)null;
                    }
                    else
                    {
                        Obe.cntc_sfecha_fallecimiento = x.cntc_sfecha_fallecimiento;
                    }
                    if (x.cntc_sfecha_entierro == null || x.cntc_sfecha_entierro.ToString() == "" || x.cntc_sfecha_entierro.ToString().Substring(0, 10) == "01/01/0001")
                    {
                        Obe.cntc_sfecha_entierro = (DateTime?)null;
                    }
                    else
                    {
                        Obe.cntc_sfecha_entierro = x.cntc_sfecha_entierro;
                    }
                    Obe.cntc_itipo_documento_fallecido = x.cntc_itipo_documento_fallecido;
                    Obe.cntc_vdocumento_fallecido = x.cntc_vdocumento_fallecido;
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    Obe.cntc_icod_indicador_espacios = x.cntc_icod_indicador_espacios;
                    Obe.cntc_vdireccion_fallecido = x.cntc_vdireccion_fallecido;

                    new BVentas().insertarContratoFallecido(Obe);
                }
            });
        }

        private void controlDeFallecidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmListarContratoFallecido frm = new FrmListarContratoFallecido();
            frm.icodContrato = Obe.cntc_icod_contrato;
            frm.icodEspacio = Obe.espac_iid_iespacios;
            frm.Show();
        }

        private void controlDeTitularesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmListarContratoTitular frm = new FrmListarContratoTitular();
            frm.icodContrato = Obe.cntc_icod_contrato;
            frm.Show();
        }

        private void exportarEToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void controlDeReprogramacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteContratoCuotas frm = new frmManteContratoCuotas();
            frm.MiEvento += new frmManteContratoCuotas.DelegadoMensaje(reload);
            frm.ObeC = Obe;
            //frm.SetInsert();
            frm.Show();
            frm.setValues();
        }

        private void controlDeContratantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            using (FrmListarContratantes frm = new FrmListarContratantes())
            {
                frm.MiEvento += new FrmListarContratantes.DelegadoMensaje(reload);
                frm.Text = "Contratantes de : " + Obe.cntc_vnumero_contrato;

                frm.cntc_icod_contrato = Obe.cntc_icod_contrato;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {

                }

            };


        }


        private void txtNomContratante_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNumContrato_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void viewContrato_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void certificadoDeUsoPerpetuoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            FrmManteCertificadoUsoPerpetuo frm = new FrmManteCertificadoUsoPerpetuo();
            frm.obj = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            frm.Text = $"Certificado del Contrato N° {Obe.cntc_vnumero_contrato}";
            frm.Show();

        }

        private void ckTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTodos.Checked)
                cargar(ckTodos.Checked);
            dteFechaIncio.Enabled = !ckTodos.Checked;
            dteFechaFinal.Enabled = !ckTodos.Checked;
            btnBuscar.Enabled = !ckTodos.Checked;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void mnuContrato_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            modificartoolStripMenuItem5.Visible = !(Obe.cntc_icod_situacion == 332);
            eliminartoolStripMenuItem6.Visible = !(Obe.cntc_icod_situacion == 332);
            anularToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            controlDeReprogramacionesToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            controlDeFallecidosToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            controlDeTitularesToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            controlDeContratantesToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            controlDeContratantesToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            certificadoDeUsoPerpetuoToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            controlCuotasToolStripMenuItem.Visible = !(Obe.cntc_icod_situacion == 332);
            actualizarContratoAnuladoToolStripMenuItem.Visible = Obe.cntc_icod_situacion == 332;
        }

        private void actualizarContratoAnuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo = false;
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            try
            {

                frmManteContrato frm = new frmManteContrato();
                frm.MiEvento += new frmManteContrato.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.lstContrato = lstContrato;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.txtSerie.Focus();
                frm.btnEspacios.Enabled = false;
                frm.btnLimpiar.Enabled = true;
                frm.txtSerie.Enabled = false;
                frm.txtNumer.Enabled = false;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}