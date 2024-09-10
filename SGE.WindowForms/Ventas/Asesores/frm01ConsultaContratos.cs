using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.bVentas;
using SGE.WindowForms.Ventas.Operaciones;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NF;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas.Formatos.NI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SGE.Common.Codigos;

namespace SGE.WindowForms.Ventas.Asesores
{
    public partial class frm01ConsultaContratos : XtraForm
    {
        List<EContrato> lstContrato = new List<EContrato>();
        public bool nuevo = false;
        public frm01ConsultaContratos()
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

        private void filtrar()
        {
            viewContrato.Columns["cntc_vnumero_contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vnumero_contrato] LIKE '%" + txtNumContrato.Text + "%'");
            viewContrato.Columns["strNombreCompleto"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[strNombreCompleto] LIKE '%" + txtNomContratante.Text + "%'");
            viewContrato.Columns["cntc_vdocumento_contratante"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cntc_vdocumento_contratante] LIKE '%" + txtDNIContratante.Text + "%'");
        }

        private async void cargar(bool todos = false)
        {
            enableLoading(true);
            if (todos)
                lstContrato = await Task.Run(() => new BVentas().listarContratoNuevo(Parametros.intContrato));
            else
                lstContrato = await Task.Run(() => new BVentas().ContratoListarPorFechas(dteFechaIncio.DateTime, dteFechaFinal.DateTime, Parametros.intContrato));
            if (Valores.vendc_icod_vendedor > 0)
                lstContrato = lstContrato.Where(c => c.cntc_icod_vendedor == Valores.vendc_icod_vendedor).ToList();
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
                int posicicion = lstContrato.IndexOf(Obe);
                var modelModifed = new BVentas().listarContratoPorIcod(intIcod);
                lstContrato[posicicion] = modelModifed;

                grdContrato.DataSource = lstContrato;
                grdContrato.RefreshDataSource();
                grdContrato.Refresh();
            }


        }



        private void nuevotoolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmManteContratoWeb frm = new frmManteContratoWeb();
            frm.MiEvento += new frmManteContratoWeb.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
            frm.Obe.cntc_vorigen_registro = "ASESOR";
        }

        private void modificartoolStripMenuItem5_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;

            try
            {
                
                if (Obe.cntc_icod_situacion == (int)SituacionContrato.Anulado)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es ANULADO"));
                if (Obe.cntc_icod_situacion == (int)SituacionContrato.ConPagos)//ANULADO
                    throw new ArgumentException(String.Format("El Contrato no puede ser Modificado, su situación es CON PAGOS"));
                frmManteContratoWeb frm = new frmManteContratoWeb();
                frm.MiEvento += new frmManteContratoWeb.DelegadoMensaje(reload);
                frm.Obe = new BVentas().ContratoWebById(Obe.cntc_icod_contrato);
                frm.Obe.cntc_vorigen_registro = Obe.cntc_vorigen_registro;
                frm.SetModify();
                frm.Show();
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

        private void imprimirtoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            EContrato ObeCO = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (ObeCO == null)
                return;
            rptContratos rptNotaCredito = new rptContratos();
            rptNotaCredito.cargar(new BVentas().ContratoImpresion(ObeCO.cntc_icod_contrato));
            rptNotaCredito.ShowPreview();

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargar();
            filtrar();
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

        private void controlTitularesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmListarContratantes frm = new FrmListarContratantes();
            frm.MiEvento += new FrmListarContratantes.DelegadoMensaje(reload);
            frm.Text = "Contratantes de : " + Obe.cntc_vnumero_contrato;

            frm.cntc_icod_contrato = Obe.cntc_icod_contrato;
            frm.ShowDialog();
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
                Services.MessageInfo($"El Contrato N° {Obe.cntc_vnumero_contrato.Insert(4, "-")} no tiene Fallecidos.");
                return new EContratoFallecido();
            }
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
            rpt.Cargar(new BVentas().ContratoImpresion(obj.cntc_icod_contrato));
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var rpt = new rptinhumacionICENI();
            rpt.Cargar(new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato));
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
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            obj = new BVentas().listarContratoPorIcod(obj.cntc_icod_contrato);
            var rpt = new rptCompromisoDePago();
            rpt.Cargar(new BVentas().ContratoImpresion(obj.cntc_icod_contrato));
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
            rpt.Cargar(new BVentas().ContratoImpresion(obj.cntc_icod_contrato), fallecido);
        }

        private void aperturaInhumaciónFamiliarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            var fallecido = SelecFallecido(obj);
            if (fallecido is null) return;
            var rpt = new rptAutorizacioEspacioAperturaFamiliar();
            rpt.Cargar(new BVentas().ContratoImpresion(obj.cntc_icod_contrato), fallecido);
        }

        private void viewContrato_DoubleClick(object sender, EventArgs e)
        {
            var obj = viewContrato.GetFocusedRow() as EContrato;
            if (obj is null) return;
            frmManteContratoWeb frm = new frmManteContratoWeb();
            frm.MiEvento += new frmManteContratoWeb.DelegadoMensaje(reload);
            frm.Obe = new BVentas().ContratoWebById(obj.cntc_icod_contrato);
            frm.SetCancel();
            frm.Show();
        }

        private void txtfilters_EditValueChanged(object sender, EventArgs e) => filtrar();

        private void controlCuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EContrato Obe = (EContrato)viewContrato.GetRow(viewContrato.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteContratoCuotas frm = new frmManteContratoCuotas();
            frm.MiEvento += new frmManteContratoCuotas.DelegadoMensaje(reload);
            frm.Text = frm.Text + "-" + "Contrato : " + Obe.cntc_vnumero_contrato;
            frm.ObeC = Obe;
            frm.Show();
            frm.setValues();
        }
    }
}