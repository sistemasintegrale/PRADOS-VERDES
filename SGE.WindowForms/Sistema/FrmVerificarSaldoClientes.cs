using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Security.Principal;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Otros.Generacion_de_Vouchers;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;

namespace SGE.WindowForms.Sistema
{
    public partial class FrmVerificarSaldoClientes : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerificarSaldoClientes));

        private List<EDocXCobrar> Lista = new List<EDocXCobrar>();        
        private int xposition = 0;

        public FrmVerificarSaldoClientes()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            //viewDXC.FocusedRowHandle = xposition;
        }

        void AddEvent()
        {
            //this.viewDXC.DoubleClick += new System.EventHandler(this.grv_DoubleClick);
        }

        private void DocumentosXCobrar_Load(object sender, EventArgs e)
        {

            Carga();
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().BuscarDocumentosXCobrarClienteVerificar();
            dgr.DataSource = Lista;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            frmGenerarVoucher frm = new frmGenerarVoucher();
            frm.Show();
        }

        private void viewDXC_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Situacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void generarVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea ejecutar el proceso de Contabilización para el periodo de {0}?", lkpMes.Text.ToUpper()), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;
            //controlEnable(true);
            //backgroundWorker1.RunWorkerAsync();
        }

        private void generarVouchers()
        {
            try
            {
                //new BContabilidad().generarVouchersVentas(Convert.ToInt32(lkpMes.EditValue), Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema");
            }    
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                generarVouchers();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {            
            controlEnable(false);            
                   
            XtraMessageBox.Show("El proceso de GENERACIÓN DE VOUCHER ha sido ejecutado satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            //txtNumDoc.Enabled = !Enable;
            //txtCliente.Enabled = !Enable;
            //mnu.Enabled = !Enable;
            dgr.Enabled = !Enable;
        }

        private void actualizarTipoDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea ejecutar el proceso de Actualización de Tipo Cambio para todos los documentos?", lkpMes.Text.ToUpper()), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    return;
            new BCuentasPorCobrar().ActualizarTipoCambio("DXC");
            XtraMessageBox.Show("Actualización realizada", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void viewDXC_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void Pagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)view.GetRow(view.FocusedRowHandle);
                switch (Obe.tdocc_icod_tipo_doc)
                {
                    case 1: //adelanto
                        FrmConsultaPagosAdelantos a = new FrmConsultaPagosAdelantos();
                        a.eDocXCobrar = Obe;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 36: //nota de crédito
                        FrmConsultaPagosNotaCredito nc = new FrmConsultaPagosNotaCredito();
                        nc.eDocXCobrar = Obe;
                        nc.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXCobrar p = new FrmConsultaPagosDocumentosXCobrar();
                        p.eDocXCobrar = Obe;
                        p.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void actualizarMontoPagadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lista.ForEach(x => 
            {
                if (x.doxcc_nmonto_pagado != x.PagadoReal)
                {
                     //Actualizar Monto Pagado Saldo
                     new BTesoreria().ActualizarMontoDXCPagadoSaldo(x.doxcc_icod_correlativo, x.tablc_iid_tipo_moneda);
                }
            });
        }
    }
}