using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Otros.Ventas;
using SGE.WindowForms.Ventas.Operaciones;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NF;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Asesores
{
    public partial class frm02ImpresionFormatos : XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();
        bool nuevo = false;
        public frm02ImpresionFormatos()
        {
            InitializeComponent();
        }

        private async void frmAlamcen_Load(object sender, EventArgs e)
        {

            dteFechaIncio.DateTime = new DateTime(Parametros.intEjercicio, 1, 1);
            dteFechaFinal.DateTime = DateTime.Now;
            var valorControles = await Task.WhenAll(CargarControles());
            BSControls.LoaderLookRepository(lkpGrdCodigoPlan, valorControles[0].Item1, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpTipoNecesidad, valorControles[0].Item2, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            BSControls.LoaderLookRepository(lkpGrdTipoSepultura, valorControles[0].Item3, "tabvd_vdescripcion", "tabvd_iid_tabla_venta_det", true);
            cargar();


        }

        private async Task<Tuple<object, object, object>> CargarControles()
        {
            var uno = await Task.Run(() => new BGeneral().listarTablaVentaDet((int)Codigos.CodigoPlan.Id));
            var dos = await Task.Run(() => new BGeneral().listarTablaVentaDet((int)Codigos.NombrePlan.Id));
            var tres = await Task.Run(() => new BGeneral().listarTablaVentaDet((int)Codigos.TipoSepultura.Id));
            return new Tuple<object, object, object>(uno, dos, tres);
        }


        private async void cargar(bool todos = false)
        {
            enableLoading(true);
            if (todos)
                lstContrato = await Task.Run(() => new BVentas().listarContrato(Parametros.intContrato));
            else
                lstContrato = await Task.Run(() => new BVentas().listarContratoPorFechas(dteFechaIncio.DateTime, dteFechaFinal.DateTime, Parametros.intContrato));
            grdContrato.DataSource = lstContrato;
            viewContrato.Focus();
            viewContrato.BestFitColumns();
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
                int index = lstContrato.FindIndex(x => x.cntc_icod_contrato == intIcod);
                lstContrato.Remove(Obe);
                var newObe = new BVentas().listarContratoPorIcod(intIcod);
                lstContrato.Insert(index, newObe);
                grdContrato.DataSource = lstContrato;
                grdContrato.RefreshDataSource();
                viewContrato.FocusedRowHandle = index;
                viewContrato.Focus();

            }

        }

        private void filtrar()
        {
            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vdocumento_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdocumento_contratante] LIKE '%" + txtDNIContratante.Text + "%'");

        }

        private void viewContrato_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

            }
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
            frmMantePreContrato frm = new frmMantePreContrato();
            frm.MiEvento += new frmMantePreContrato.DelegadoMensaje(reload);
            frm.Obe = new BVentas().listarContratoPorIcod(Obe.cntc_icod_contrato);
            frm.lstContrato = lstContrato;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
            frm.txtSerie.Focus();
            frm.btnEspacios.Enabled = false;
            frm.btnLimpiar.Enabled = true;
        }

        private void txtFiltrosChanged(object sender, EventArgs e)
        {
            filtrar();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void ckTodos_CheckedChanged(object sender, EventArgs e)
        {

            if (ckTodos.Checked)
                cargar(ckTodos.Checked);
            dteFechaIncio.Enabled = !ckTodos.Checked;
            dteFechaFinal.Enabled = !ckTodos.Checked;
            btnBuscar.Enabled = !ckTodos.Checked;

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




        private void mnuContrato_Opening(object sender, CancelEventArgs e)
        {
            //var select = viewContrato.GetFocusedRow() as EContrato;
            //if (select is null) return;
            //if (select.cntc_icod_nombre_plan == (int)NombrePlan.NecesidadInmediata)
            //{
            //    if (select.cntc_itipo_sepultura == (int)TipoSepultura.EspacioPersonal)
            //    {
            //        autorizaciónDeEspacioICToolStripMenuItem.Enabled = true;
            //        programaciónEntierroToolStripMenuItem.Enabled = true;
            //        lapidaToolStripMenuItem.Enabled = true;
            //        autorizaciónDeEspacioPersonalToolStripMenuItem.Enabled = true;
            //        compromisoDePagoToolStripMenuItem.Enabled = true;
            //        adendaToolStripMenuItem.Enabled = false;
            //        autorizaciónDeEstacioICEToolStripMenuItem.Enabled = false;
            //        autorizaciónDeAperturaToolStripMenuItem.Enabled = false;
            //        porcentajeDeUsoToolStripMenuItem.Enabled = false;
            //    }
            //    if (true)
            //    {

            //    }
            //    if (true)
            //    {

            //    }
            //}
            //if (select.cntc_icod_nombre_plan == (int)NombrePlan.NecesidadFutura)
            //{
            //    if (true)
            //    {

            //    }
            //    if (true)
            //    {

            //    }
            //    if (true)
            //    {

            //    }
            //}

        }

        private EContratoFallecido SelecFallecido(EContrato Obe)
        {
            var fallecidos = new BVentas().listarContratoFallecido(Obe.cntc_icod_contrato);
            if (fallecidos.Count == 1)
            {
                return fallecidos.First();
            }
            if (fallecidos.Count > 1)
            {
                FrmListarContratoFallecido frm = new FrmListarContratoFallecido();
                frm.icodContrato = Obe.cntc_icod_contrato;
                frm.icodEspacio = Obe.espac_iid_iespacios;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    return frm._Be;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Service.MessageInfo($"El Contrato N° {Obe.cntc_vnumero_contrato.Insert(4, "-")} no tiene Fallecidos.");
                return new EContratoFallecido();
            }


        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void aperturaInhumaciónFamiliarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioAperturaFamiliarNF();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumacionICToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioICNF();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumaciónICEToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioInhumacionICENF();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumacionPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioPersonalNF();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);

        }

        private void adendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var rpt = new rptAdenda();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            obj = new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato);
            var rpt = new rptCompromisoDePago();
            rpt.Cargar(obj);
            rpt.CreateDocument();
            List<EContratoCuotas> mlisdet = new BVentas().listarContratoCuotas(obj.cntc_icod_contrato);
            var lstFomaFinanciamiento = Obetenerlstpagos(obj.cntc_icod_contrato, obj);
            mlisdet.ForEach(x =>
            {

                Tuple<string, string> tupla = new BVentas().ObtenerDocumentos(x.cntc_icod_contrato_cuotas);

                x.strfechaDocumentos = tupla.Item2;
                x.plnd_vnumero_doc = tupla.Item1;
                x.tipo = x.cntc_inro_cuotas == 0 ? "CI" : x.cntc_itipo_cuota > 0 ? "RP" + x.cntc_itipo_cuota : "P";
                x.tipo = x.cntc_icod_tipo_cuota == 5430 ? "P" : x.tipo;
            });

            var listaFomas = new BVentas().CuotaFomaListar(obj.cntc_icod_contrato).FirstOrDefault();
            if (listaFomas != null)
            {
                listaFomas.intUsuario = Valores.intUsuario;
                listaFomas.strPc = WindowsIdentity.GetCurrent().Name;
                new BVentas().CuotaFomaModificar(listaFomas);
                obj = new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato);
            }

            var Reprogramacion = new BVentas().ListarReprogramaciones(obj.cntc_icod_contrato).OrderByDescending(x => x.cntcr_iid_reprogramacion).FirstOrDefault();
            obj.cntc_nfinanciamientro = Reprogramacion != null ? Reprogramacion.cntcr_nmonto_financiamiento : obj.cntc_nfinanciamientro;

            var rpt2 = new rptCompromisoDePagoPag2();
            rpt2.Cargar(obj, mlisdet);
            rpt2.CreateDocument();

            rpt.Pages.AddRange(rpt2.Pages);
            rpt.PrintingSystem.ContinuousPageNumbering = true;
            rpt.ShowPreview();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var rpt = new rptinhumacionICENI();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato));
        }

        private void inhumacionICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioICE();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumaciónPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioPersonal();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumacionICToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioInhumacionICNI();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumaciónPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioInhumacionPCNI();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void inhumaciónPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioInhumacionPPNI();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void programaciónEntierroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptProgramacionEntierro();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void aperturaInhumaciónFamiliarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioAperturaFamiliar();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato), fallecido);
        }

        private void controlTitularesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            using (FrmListarContratantes frm = new FrmListarContratantes())
            {
                frm.MiEvento += new FrmListarContratantes.DelegadoMensaje(reload);
                frm.Text = "Contratantes de : " + Obe.cntc_vnumero_contrato;

                frm.cntc_icod_contrato = Obe.cntc_icod_contrato;
                frm.Show();

            };
        }

        private void controlDeFallecidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = viewContrato.GetFocusedRow() as EContrato;
            if (Obe == null)
                return;
            FrmListarContratoFallecido frm = new FrmListarContratoFallecido();
            frm.icodContrato = Obe.cntc_icod_contrato;
            frm.icodEspacio = Obe.espac_iid_iespacios;
            frm.Show();
        }
    }
}