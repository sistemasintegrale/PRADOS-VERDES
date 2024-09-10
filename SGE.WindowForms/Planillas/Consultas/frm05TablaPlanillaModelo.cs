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
using SGE.WindowForms.Otros.Planillas;
using SGE.WindowForms.Reportes.Almacen.Registros;
using System.Globalization;

namespace SGE.WindowForms.Planillas.Consultas
{
    public partial class frm05TablaPlanillaModelo : DevExpress.XtraEditors.XtraForm
    {
        List<EPlanillaModeloCont> lstTablaPlanilla = new List<EPlanillaModeloCont>();
        public decimal sueldo_basic = 0;
        public decimal Basicodelmes;
        public decimal Comisiones_Mes;
        public decimal Gratificacion_Ordinaria;
        public decimal Remuneracion_Anteriores;
        public decimal Comisiones;
        public decimal txtHasta_5_UIT;
        public decimal asignacionFamiliar;
        public int essalud;
        public frm05TablaPlanillaModelo()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            //OBTIENE EL UIT
            var lt = new BAdministracionSistema().listarParametro();
            txtUIT.Text = Math.Round((Convert.ToDecimal(lt[0].pm_nuit_parametro)), 2).ToString();

            //OBTIENE EL PORCENTAJE DE ESSALUD
            var lt2 = new BPlanillas().listarParametroPlanilla();
            essalud = Convert.ToInt32(lt2[0].prpc_nporc_essalud);

            //OBTIENE EL MONTO DE LA ASIGNACION FAMILIAR

            asignacionFamiliar = Math.Round((Convert.ToDecimal(lt2[0].prpc_nasignacion_familiar)), 2);


            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);

            if (string.IsNullOrEmpty(btnPersonal.Text))
            {
                lkpMes.EditValue = 0;
                lkpMes.Text = "";
            }



        }
        bool obtenerAsignacionFamiliar()
        {
            var lstPersonal = new BPlanillas().listarPersonal();
            lstPersonal = lstPersonal.Where(x => x.perc_icod_personal == Convert.ToInt32(btnPersonal.Tag)).ToList();
            EPersonal objPersonal = lstPersonal.FirstOrDefault();
            return Convert.ToBoolean(objPersonal.perc_basig_familiar);

        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTablaPlanilla.FindIndex(x => x.plcc_pland_icod == intIcod);
            viewPlanilla.FocusedRowHandle = index;
            viewPlanilla.Focus();
        }
        private void nuevo()
        {
            frmRegistroTablaPlanillaModelo frm = new frmRegistroTablaPlanillaModelo();
            frm.MiEvento += new frmRegistroTablaPlanillaModelo.DelegadoMensaje(reload);
            if (lstTablaPlanilla.Count > 0)
                frm.txtCodigo.Text = String.Format("{00:000}", lstTablaPlanilla.Max(x => Convert.ToInt32(x.plcd_iid) + 1));
            else
                frm.txtCodigo.Text = "001";

            frm.lstTablaPlanilla = lstTablaPlanilla;
            frm.SetInsert();
            frm.ShowDialog();
            frm.txtNombre.Focus();

        }
        private void modificar()
        {
            EPlanillaModeloCont Obe = (EPlanillaModeloCont)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (Obe == null)
                return;
            frmRegistroTablaPlanillaModelo frm = new frmRegistroTablaPlanillaModelo();
            frm.MiEvento += new frmRegistroTablaPlanillaModelo.DelegadoMensaje(reload);
            frm.lstTablaPlanilla = lstTablaPlanilla;
            frm.Obe = Obe;
            frm.SetModify();
            frm.setValues();
            frm.ShowDialog();
            frm.txtNombre.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {

        }

        private void eliminar()
        {
            int usuario = Valores.intUsuario;
            string pc = WindowsIdentity.GetCurrent().Name;
            try
            {
                ETablaPlanilla Obe = (ETablaPlanilla)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewPlanilla.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la tabla Planilla " + Obe.tbpc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BPlanillas().eliminarTablaPlanilla(Obe);
                    new BPlanillas().eliminarDetalleTablaPlanilla(Obe.tbpc_icod_tabla_planilla, usuario, pc);
                    cargar();
                    if (lstTablaPlanilla.Count >= index + 1)
                        viewPlanilla.FocusedRowHandle = index;
                    else
                        viewPlanilla.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buscarCriterio()
        {

        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }



        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }



        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void grdAlmacen_DoubleClick(object sender, EventArgs e)
        {
            //ETablaPlanilla Obe = (ETablaPlanilla)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            //if (Obe == null)
            //    return;
            //frmRegistroTablaPlanilla frm = new frmRegistroTablaPlanilla();
            //frm.Obe = Obe;
            //frm.SetCancel();
            //frm.setValues();
            //frm.ShowDialog();

        }

        private void PorcentajesFIjosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PorcentajesFijos();
        }


        private void PorcentajesFijos()
        {
            ETablaPlanilla Obe = (ETablaPlanilla)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (Obe == null)
                return;
            int index = viewPlanilla.FocusedRowHandle;
            frmTablaPlanillaDetalle frm = new frmTablaPlanillaDetalle();

            frm.intIcodFondosPensiones = Obe.tbpc_icod_tabla_planilla;
            frm.Text = String.Format("Relacion de Tabla: {0} - {1}", Obe.tbpc_iid_vcodigo_tabla_planilla, Obe.tbpc_vdescripcion);
            frm.Show();

        }

        private void PorcentajesMixtos()
        {

        }

        private void viewAlmacen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //EFondosPensiones Obe = (EFondosPensiones)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            //if (Obe != null)
            //{
            //    if (Obe.tablc_iid_tipo_fondo_pensiones == false)
            //    {
            //        PorcentajesFIjosToolStripMenuItem.Visible = false;
            //        PorcentajesMixtosToolStripMenuItem1.Visible = false;
            //    }
            //    else
            //    {
            //        PorcentajesFIjosToolStripMenuItem.Visible = true;
            //        PorcentajesMixtosToolStripMenuItem1.Visible = true;
            //    }
            //}
        }

        private void PorcentajesMixtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EPlanillaModeloCont obe = (EPlanillaModeloCont)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (obe != null)
            {
                try
                {

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        string fileName = sfd.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdplanilla.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grdplanilla.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdplanilla.DataSource = null;
                        sfd.FileName = string.Empty;
                    }

                    else
                        throw new ArgumentException("No hay registros para exportar");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grdplanilla_Click(object sender, EventArgs e)
        {

        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            frmListarPersonalSimple frm = new frmListarPersonalSimple();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnPersonal.Text = frm._Be.ApellNomb;
                btnPersonal.Tag = frm._Be.perc_icod_personal;
                sueldo_basic = Convert.ToDecimal(frm._Be.perc_nmont_basico);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnPersonal.Text))
            {
                XtraMessageBox.Show("Ingrese Personal", "Informacions del Sistema");
                return;
            }
            viewPlanilla.GroupPanelText = String.Format("Planilla de : {0}", btnPersonal.Text);
            lstTablaPlanilla = new BPlanillas().listarPlanillaModelo();
            lstTablaPlanilla = IngresarValores(lstTablaPlanilla);

            for (int i = 1; i <= 12; i++)
            {
                lstTablaPlanilla = insertarDatosPlanilla(lstTablaPlanilla, i);
            }

            //OCULTANDO COLUMNAS 

            if (!string.IsNullOrEmpty(lkpMes.Text))
            {
                ocultarCeldas();
            }




            grdplanilla.DataSource = lstTablaPlanilla;
            viewPlanilla.Focus();
            viewPlanilla.ViewCaption = String.Format("Planilla de : {0}", btnPersonal.Text);
        }

        private List<EPlanillaModeloCont> IngresarValores(List<EPlanillaModeloCont> lstTablaPlanilla)
        {
            decimal UIT = Convert.ToDecimal(txtUIT.Text);
            lstTablaPlanilla[11].strValores = "Hasta " + Math.Round((UIT * 5)).ToString("N", new CultureInfo("en-US"));
            lstTablaPlanilla[12].strValores = "De " + Math.Round((UIT * 5)).ToString("N", new CultureInfo("en-US")) + " Hasta " + Math.Round((UIT * 20)).ToString("N", new CultureInfo("en-US"));
            lstTablaPlanilla[13].strValores = "De " + Math.Round((UIT * 20)).ToString("N", new CultureInfo("en-US")) + " Hasta " + Math.Round((UIT * 35)).ToString("N", new CultureInfo("en-US"));
            lstTablaPlanilla[14].strValores = "De " + Math.Round((UIT * 35)).ToString("N", new CultureInfo("en-US")) + " Hasta " + Math.Round((UIT * 45)).ToString("N", new CultureInfo("en-US"));
            lstTablaPlanilla[15].strValores = "Desde " + Math.Round((UIT * 45)).ToString("N", new CultureInfo("en-US"));
            return lstTablaPlanilla;
        }

        private void ocultarCeldas()
        {
            switch (Convert.ToInt32(lkpMes.EditValue))
            {
                case 1:
                    gr1.Visible = true; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 2:
                    gr1.Visible = false; gr2.Visible = true; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 3:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = true; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 4:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = true; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 5:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = true; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 6:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = true;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 7:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = true; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 8:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = true; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 9:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = true; gr10.Visible = false; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 10:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = true; gr11.Visible = false; gr12.Visible = false;
                    break;
                case 11:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = true; gr12.Visible = false;
                    break;
                case 12:
                    gr1.Visible = false; gr2.Visible = false; gr3.Visible = false; gr4.Visible = false; gr5.Visible = false; gr6.Visible = false;
                    gr7.Visible = false; gr8.Visible = false; gr9.Visible = false; gr10.Visible = false; gr11.Visible = false; gr12.Visible = true;
                    break;
                default:
                    break;
            }

            viewPlanilla.OptionsView.ColumnAutoWidth = true;

        }

        private List<EPlanillaModeloCont> insertarDatosPlanilla(List<EPlanillaModeloCont> lstTablaPlanilla, int mes)
        {


            decimal[] montos = new decimal[lstTablaPlanilla.Count + 1];

            EplanillaContDetalle ojb = new EplanillaContDetalle();
            var lt = new BAdministracionSistema().listarParametro();
            decimal UIT = Convert.ToDecimal(lt[0].pm_nuit_parametro);
            if (obtenerAsignacionFamiliar())
                ojb.Basicodelmes = Math.Round(Convert.ToDecimal(sueldo_basic) + asignacionFamiliar, 2);
            else
                ojb.Basicodelmes = Math.Round(Convert.ToDecimal(sueldo_basic), 2);
            montos[1] = ojb.Basicodelmes;
            ojb.Comisiones_Mes = 0;
            montos[2] = ojb.Comisiones_Mes;
            ojb.Remuneracion_Mensual = Math.Round(ojb.Basicodelmes + Convert.ToDecimal(ojb.Comisiones_Mes), 2);
            montos[3] = ojb.Remuneracion_Mensual;
            ojb.Meses_Faltan = Convert.ToDecimal(12 - (Convert.ToInt32(mes) - 1));
            montos[4] = ojb.Meses_Faltan;
            ojb.Remuneracion_proyectada = Math.Round(ojb.Remuneracion_Mensual * ojb.Meses_Faltan, 2);
            montos[5] = ojb.Remuneracion_proyectada;


            ojb.Gratificacion_Ordinaria = Math.Round((((ojb.Basicodelmes + 0) + (ojb.Basicodelmes * Convert.ToDecimal(("0.0" + essalud).ToString()))) * 2), 2);


            montos[6] = ojb.Gratificacion_Ordinaria;

            ojb.Remuneracion_Anteriores = 0;
            montos[7] = ojb.Remuneracion_Anteriores;
            ojb.Comisiones = 0;
            montos[8] = ojb.Comisiones;
            ojb.Remuneracion_Anual = Math.Round(Convert.ToDecimal(ojb.Remuneracion_proyectada + ojb.Gratificacion_Ordinaria + ojb.Remuneracion_Anteriores + ojb.Comisiones), 2);
            montos[9] = ojb.Remuneracion_Anual;
            ojb.Menos_7UIT = Convert.ToDecimal("-" + (Convert.ToDecimal((UIT * 7))).ToString());
            montos[10] = ojb.Menos_7UIT;
            ojb.Renta_Neta_Global_Anual = ojb.Remuneracion_Anual + ojb.Menos_7UIT;
            montos[11] = ojb.Renta_Neta_Global_Anual;
            //Distribuccion de la RNGA (Art. 53 LIR)

            if (ojb.Renta_Neta_Global_Anual > Convert.ToDecimal(UIT * 5))
            {
                ojb.Hasta_5_UIT = Math.Round((Convert.ToDecimal(UIT * 5)), 2);
            }
            else
            {
                ojb.Hasta_5_UIT = 0;
            }


            montos[12] = ojb.Hasta_5_UIT;
            if (ojb.Hasta_5_UIT >= (5 * UIT) && ojb.Hasta_5_UIT <= (20 * UIT))
            {
                ojb.Esceso_5_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Hasta_5_UIT, 2);
            }
            else
            {
                ojb.Esceso_5_UIT = 0;
            }
            montos[13] = ojb.Esceso_5_UIT;

            if (ojb.Esceso_5_UIT >= (20 * UIT) && ojb.Esceso_5_UIT <= (35 * UIT))
            {
                ojb.Esceso_20_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Esceso_5_UIT, 2);
            }
            else
            {
                ojb.Esceso_20_UIT = 0;
            }
            montos[14] = ojb.Esceso_20_UIT;

            if (ojb.Esceso_20_UIT >= (35 * UIT) && ojb.Esceso_20_UIT <= (45 * UIT))
            {
                ojb.Esceso_35_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Esceso_20_UIT, 2);
            }
            else
            {
                ojb.Esceso_35_UIT = 0;
            }
            montos[15] = ojb.Esceso_35_UIT;

            if (ojb.Esceso_35_UIT >= (45 * UIT))
            {
                ojb.Esceso_45_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Esceso_35_UIT, 2);
            }
            else
            {
                ojb.Esceso_45_UIT = 0;
            }
            montos[16] = ojb.Esceso_45_UIT;

            ojb.Renta_Neta_Global_Anual_RNGA = Math.Round(ojb.Esceso_45_UIT + ojb.Esceso_35_UIT + ojb.Esceso_20_UIT + ojb.Esceso_5_UIT + ojb.Hasta_5_UIT, 2);
            montos[17] = ojb.Renta_Neta_Global_Anual_RNGA;

            //Tasas del IR (Aplicación)

            ojb.Hasta_5 = Math.Round(ojb.Hasta_5_UIT * Convert.ToDecimal(0.08), 2);
            montos[18] = ojb.Hasta_5;
            ojb.Hasta_20 = Math.Round(ojb.Esceso_5_UIT * Convert.ToDecimal(0.14), 2);
            montos[19] = ojb.Hasta_20;
            ojb.Hasta_35 = Math.Round(ojb.Esceso_20_UIT * Convert.ToDecimal(0.17), 2);
            montos[20] = ojb.Hasta_35;
            ojb.Hasta_45 = Math.Round(ojb.Esceso_35_UIT * Convert.ToDecimal(0.20), 2);
            montos[21] = ojb.Hasta_45;
            ojb.Mas_45 = Math.Round(ojb.Esceso_45_UIT * Convert.ToDecimal(0.30), 2);
            montos[22] = ojb.Mas_45;
            ojb.Impuesto_Resultante = Math.Round(ojb.Hasta_5 + ojb.Hasta_20 + ojb.Hasta_35 + ojb.Hasta_45 + ojb.Mas_45, 2);
            montos[23] = ojb.Impuesto_Resultante;
            ojb.Menos_Retenciones_IR = 0;
            montos[24] = ojb.Menos_Retenciones_IR;
            ojb.Impuesto_pagar = Math.Round(ojb.Impuesto_Resultante + ojb.Menos_Retenciones_IR, 2);
            montos[25] = ojb.Impuesto_pagar;
            ojb.meses = Convert.ToInt32(ojb.Meses_Faltan);
            montos[26] = ojb.meses;
            if (ojb.meses < 10)
            {
                ojb.Retencion_mensual = Math.Round(ojb.Impuesto_pagar / ojb.meses, 2);// Convert.ToDecimal(("0.0" + (ojb.meses).ToString()).ToString());
            }
            else
            {
                ojb.Retencion_mensual = Math.Round(ojb.Impuesto_pagar / ojb.meses, 2);// * Convert.ToDecimal(("0." + (ojb.meses).ToString()).ToString());
            }
            montos[27] = ojb.Retencion_mensual;

            ojb.r_5ta = Math.Round(ojb.Retencion_mensual * Convert.ToDecimal(0.5), 2);
            montos[28] = ojb.r_5ta;



            for (int i = 0; i < lstTablaPlanilla.Count; i++)
            {
                switch (mes)
                {
                    case 1:
                        lstTablaPlanilla[i].plcd_montos_enero = montos[i + 1];
                        break;
                    case 2:
                        lstTablaPlanilla[i].plcd_montos_febrero = montos[i + 1];
                        break;
                    case 3:
                        lstTablaPlanilla[i].plcd_montos_marzo = montos[i + 1];
                        break;
                    case 4:
                        lstTablaPlanilla[i].plcd_montos_abril = montos[i + 1];
                        break;
                    case 5:
                        lstTablaPlanilla[i].plcd_montos_mayo = montos[i + 1];
                        break;
                    case 6:
                        lstTablaPlanilla[i].plcd_montos_junio = montos[i + 1];
                        break;
                    case 7:
                        lstTablaPlanilla[i].plcd_montos_julio = montos[i + 1];
                        break;
                    case 8:
                        lstTablaPlanilla[i].plcd_montos_agosto = montos[i + 1];
                        break;
                    case 9:
                        lstTablaPlanilla[i].plcd_montos_setiembre = montos[i + 1];
                        break;
                    case 10:
                        lstTablaPlanilla[i].plcd_montos_octubre = montos[i + 1];
                        break;
                    case 11:
                        lstTablaPlanilla[i].plcd_montos_noviembre = montos[i + 1];
                        break;
                    case 12:
                        lstTablaPlanilla[i].plcd_montos_diciembre = montos[i + 1];
                        break;
                    default:
                        break;
                }

            }
            return lstTablaPlanilla;
        }

        private void cambiarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EPlanillaModeloCont obe = (EPlanillaModeloCont)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (obe == null)
            {
                return;
            }

            frmRegistroTablaPlanillaModeloDatos frm = new frmRegistroTablaPlanillaModeloDatos();
            frm.MiEvento += new frmRegistroTablaPlanillaModeloDatos.DelegadoMensaje(reload);
            frm.lstTablaPlanilla = lstTablaPlanilla;
            frm.SetInsert();
            frm.txtBasicodelmes.Text = lstTablaPlanilla[0].plcd_montos_enero.ToString();
            frm.txtComisiones_Mes.Text = lstTablaPlanilla[1].plcd_montos_enero.ToString();
            frm.txtGratificacion_Ordinaria.Text = lstTablaPlanilla[5].plcd_montos_enero.ToString();
            frm.txtRemuneracion_Anteriores.Text = lstTablaPlanilla[6].plcd_montos_enero.ToString();
            frm.txtComisiones.Text = lstTablaPlanilla[7].plcd_montos_enero.ToString();
            frm.txtHasta_5_UIT.Text = lstTablaPlanilla[11].plcd_montos_enero.ToString();
            frm.txtBasicodelmes.Focus();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Basicodelmes = frm.Obe.Basicodelmes;
                Comisiones_Mes = frm.Obe.Comisiones_Mes;
                Gratificacion_Ordinaria = frm.Obe.Gratificacion_Ordinaria;
                Remuneracion_Anteriores = frm.Obe.Remuneracion_Anteriores;
                Comisiones = frm.Obe.Comisiones;
                txtHasta_5_UIT = frm.Obe.txtHasta_5_UIT;

                for (int i = 1; i <= 12; i++)
                {
                    lstTablaPlanilla = recalcular(lstTablaPlanilla, i);
                }
                if (!string.IsNullOrEmpty(lkpMes.Text))
                {
                    ocultarCeldas();
                }
                grdplanilla.DataSource = lstTablaPlanilla;
                grdplanilla.RefreshDataSource();
                grdplanilla.Refresh();
                viewPlanilla.RefreshData();
                viewPlanilla.Focus();
                viewPlanilla.ViewCaption = String.Format("Planilla de : {0}", btnPersonal.Text);

            }

        }

        private List<EPlanillaModeloCont> recalcular(List<EPlanillaModeloCont> lstTablaPlanilla, int mes)
        {
            decimal[] montos = new decimal[lstTablaPlanilla.Count + 1];

            EplanillaContDetalle ojb = new EplanillaContDetalle();
            var lt = new BAdministracionSistema().listarParametro();
            decimal UIT = Convert.ToDecimal(lt[0].pm_nuit_parametro);
            ojb.Basicodelmes = Math.Round(Convert.ToDecimal(Basicodelmes), 2);
            montos[1] = ojb.Basicodelmes;
            ojb.Comisiones_Mes = Comisiones_Mes;
            montos[2] = ojb.Comisiones_Mes;
            ojb.Remuneracion_Mensual = Math.Round(ojb.Basicodelmes + Convert.ToDecimal(ojb.Comisiones_Mes), 2);
            montos[3] = ojb.Remuneracion_Mensual;
            ojb.Meses_Faltan = Convert.ToDecimal(12 - (Convert.ToInt32(mes) - 1));
            montos[4] = ojb.Meses_Faltan;
            ojb.Remuneracion_proyectada = Math.Round(ojb.Remuneracion_Mensual * ojb.Meses_Faltan, 2);
            montos[5] = ojb.Remuneracion_proyectada;
            ojb.Gratificacion_Ordinaria = Math.Round(Convert.ToDecimal(Gratificacion_Ordinaria), 2);
            montos[6] = ojb.Gratificacion_Ordinaria;
            ojb.Remuneracion_Anteriores = Remuneracion_Anteriores;
            montos[7] = ojb.Remuneracion_Anteriores;
            ojb.Comisiones = Comisiones;
            montos[8] = ojb.Comisiones;
            ojb.Remuneracion_Anual = Math.Round(Convert.ToDecimal(ojb.Remuneracion_proyectada + ojb.Gratificacion_Ordinaria + ojb.Remuneracion_Anteriores + ojb.Comisiones), 2);
            montos[9] = ojb.Remuneracion_Anual;
            ojb.Menos_7UIT = Convert.ToDecimal("-" + (Convert.ToDecimal((UIT * 7))).ToString());
            montos[10] = ojb.Menos_7UIT;
            ojb.Renta_Neta_Global_Anual = ojb.Remuneracion_Anual + ojb.Menos_7UIT;
            montos[11] = ojb.Renta_Neta_Global_Anual;
            //Distribuccion de la RNGA (Art. 53 LIR)
            if (ojb.Renta_Neta_Global_Anual > Convert.ToDecimal(UIT * 5))
            {
                ojb.Hasta_5_UIT = Math.Round((Convert.ToDecimal(UIT * 5)), 2);
            }
            else
            {
                ojb.Hasta_5_UIT = 0;
            }
            //ojb.Hasta_5_UIT = txtHasta_5_UIT;
            montos[12] = ojb.Hasta_5_UIT;
            if (ojb.Hasta_5_UIT >= (5 * UIT) && ojb.Hasta_5_UIT > 2300 && ojb.Hasta_5_UIT <= (20 * UIT) && ojb.Hasta_5_UIT <= 92000)
            {
                ojb.Esceso_5_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Hasta_5_UIT, 2);
            }
            else
            {
                ojb.Esceso_5_UIT = 0;
            }
            montos[13] = ojb.Esceso_5_UIT;

            if (ojb.Esceso_5_UIT >= (20 * UIT) && ojb.Esceso_5_UIT > 92000 && ojb.Esceso_5_UIT <= (35 * UIT) && ojb.Esceso_5_UIT <= 161000)
            {
                ojb.Esceso_20_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Esceso_5_UIT, 2);
            }
            else
            {
                ojb.Esceso_20_UIT = 0;
            }
            montos[14] = ojb.Esceso_20_UIT;

            if (ojb.Esceso_20_UIT >= (35 * UIT) && ojb.Esceso_20_UIT > 161000 && ojb.Esceso_20_UIT <= (45 * UIT) && ojb.Esceso_20_UIT <= 20700)
            {
                ojb.Esceso_35_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Esceso_20_UIT, 2);
            }
            else
            {
                ojb.Esceso_35_UIT = 0;
            }
            montos[15] = ojb.Esceso_35_UIT;

            if (ojb.Esceso_35_UIT >= (45 * UIT) && ojb.Esceso_35_UIT > 20700)
            {
                ojb.Esceso_45_UIT = Math.Round(ojb.Renta_Neta_Global_Anual - ojb.Esceso_35_UIT, 2);
            }
            else
            {
                ojb.Esceso_45_UIT = 0;
            }
            montos[16] = ojb.Esceso_45_UIT;

            ojb.Renta_Neta_Global_Anual_RNGA = Math.Round(ojb.Esceso_45_UIT + ojb.Esceso_35_UIT + ojb.Esceso_20_UIT + ojb.Esceso_5_UIT + ojb.Hasta_5_UIT, 2);
            montos[17] = ojb.Renta_Neta_Global_Anual_RNGA;

            //Tasas del IR (Aplicación)

            ojb.Hasta_5 = Math.Round(ojb.Hasta_5_UIT * Convert.ToDecimal(0.08), 2);
            montos[18] = ojb.Hasta_5;
            ojb.Hasta_20 = Math.Round(ojb.Esceso_5_UIT * Convert.ToDecimal(0.14), 2);
            montos[19] = ojb.Hasta_20;
            ojb.Hasta_35 = Math.Round(ojb.Esceso_20_UIT * Convert.ToDecimal(0.17), 2);
            montos[20] = ojb.Hasta_35;
            ojb.Hasta_45 = Math.Round(ojb.Esceso_35_UIT * Convert.ToDecimal(0.20), 2);
            montos[21] = ojb.Hasta_45;
            ojb.Mas_45 = Math.Round(ojb.Esceso_45_UIT * Convert.ToDecimal(0.30), 2);
            montos[22] = ojb.Mas_45;
            ojb.Impuesto_Resultante = Math.Round(ojb.Hasta_5 + ojb.Hasta_20 + ojb.Hasta_35 + ojb.Hasta_45 + ojb.Mas_45, 2);
            montos[23] = ojb.Impuesto_Resultante;
            ojb.Menos_Retenciones_IR = 0;
            montos[24] = ojb.Menos_Retenciones_IR;
            ojb.Impuesto_pagar = Math.Round(ojb.Impuesto_Resultante + ojb.Menos_Retenciones_IR, 2);
            montos[25] = ojb.Impuesto_pagar;
            ojb.meses = Convert.ToInt32(ojb.Meses_Faltan);
            montos[26] = ojb.meses;
            if (ojb.meses < 10)
            {
                ojb.Retencion_mensual = Math.Round(ojb.Impuesto_pagar / ojb.meses, 2);// Convert.ToDecimal(("0.0" + (ojb.meses).ToString()).ToString());
            }
            else
            {
                ojb.Retencion_mensual = Math.Round(ojb.Impuesto_pagar / ojb.meses, 2);// * Convert.ToDecimal(("0." + (ojb.meses).ToString()).ToString());
            }
            montos[27] = ojb.Retencion_mensual;

            ojb.r_5ta = Math.Round(ojb.Retencion_mensual / 2, 2);
            montos[28] = ojb.r_5ta;



            for (int i = 0; i < lstTablaPlanilla.Count; i++)
            {
                switch (mes)
                {
                    case 1:
                        lstTablaPlanilla[i].plcd_montos_enero = montos[i + 1];
                        break;
                    case 2:
                        lstTablaPlanilla[i].plcd_montos_febrero = montos[i + 1];
                        break;
                    case 3:
                        lstTablaPlanilla[i].plcd_montos_marzo = montos[i + 1];
                        break;
                    case 4:
                        lstTablaPlanilla[i].plcd_montos_abril = montos[i + 1];
                        break;
                    case 5:
                        lstTablaPlanilla[i].plcd_montos_mayo = montos[i + 1];
                        break;
                    case 6:
                        lstTablaPlanilla[i].plcd_montos_junio = montos[i + 1];
                        break;
                    case 7:
                        lstTablaPlanilla[i].plcd_montos_julio = montos[i + 1];
                        break;
                    case 8:
                        lstTablaPlanilla[i].plcd_montos_agosto = montos[i + 1];
                        break;
                    case 9:
                        lstTablaPlanilla[i].plcd_montos_setiembre = montos[i + 1];
                        break;
                    case 10:
                        lstTablaPlanilla[i].plcd_montos_octubre = montos[i + 1];
                        break;
                    case 11:
                        lstTablaPlanilla[i].plcd_montos_noviembre = montos[i + 1];
                        break;
                    case 12:
                        lstTablaPlanilla[i].plcd_montos_diciembre = montos[i + 1];
                        break;
                    default:
                        break;
                }

            }
            return lstTablaPlanilla;
        }

        private void actualizarRentaDelPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EPlanillaModeloCont obe = (EPlanillaModeloCont)viewPlanilla.GetRow(viewPlanilla.FocusedRowHandle);
            if (obe == null)
            {
                return;
            }
            try
            {
                lstTablaPlanilla.ForEach(x =>
                {
                    x.plcd_iusuario_crea = Valores.intUsuario;
                    x.plcd_iusuario_modifica = Valores.intUsuario;
                    x.plcd_vpc_crea = WindowsIdentity.GetCurrent().Name;
                    x.plcd_vpc_modifica = WindowsIdentity.GetCurrent().Name;
                    x.rnt_5ta_icod = 0;
                    x.plcd_icod_personal = Convert.ToInt32(btnPersonal.Tag);
                });
                new BPlanillas().ActualizarRentaPersonal(lstTablaPlanilla);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            var list = new List<EPlanillaModeloCont>();
            grdplanilla.DataSource = list;
            grdplanilla.RefreshDataSource();
            viewPlanilla.RefreshData();
            grdplanilla.Refresh();
        }
    }
}