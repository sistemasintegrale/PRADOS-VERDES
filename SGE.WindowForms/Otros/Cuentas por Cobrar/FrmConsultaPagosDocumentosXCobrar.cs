using System;
using System.Collections.Generic;
using SGE.Entity;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosDocumentosXCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaPagosDocumentosXCobrar));


        private List<EDocXCobrarPago> Lista = new List<EDocXCobrarPago>();        
                
        //public long doxcc_icod_correlativo;
        public EDocXCobrar eDocXCobrar;

        public FrmConsultaPagosDocumentosXCobrar()
        {
            InitializeComponent();
        }
                
        private void DocumentosXCobrar_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            Carga();
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarPagoDirectoDocumentoXCobrar(eDocXCobrar.doxcc_icod_correlativo);
            grd.DataSource = Lista;
            Lista.ForEach(x=>
            {
                if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)//adelanto
                {
                    new BTesoreria().ActualizarMontoPagadoAdelantoCliente(x.doxcc_icod_correlativo_adelanto, 0);
                }
                else if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente) //nota de crédito
                {
                    new BTesoreria().ActualizarMontoPagadoSaldoNotaCreditoCliente(x.doxcc_icod_correlativo_nota_credito, 0);
                }
                else if (x.tdocc_icod_tipo_doc == 26) //Facturas
                {
                    new BTesoreria().ActualizarMontoDXCPagadoSaldo(x.doxcc_icod_correlativo, 0);
                }
            });
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


    }
}