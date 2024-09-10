using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using System.Linq;
using System.Security.Principal;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using SGE.WindowForms.Maintenance;

namespace SGE.WindowForms.Sistema
{
    public partial class FrmVerificarSaldoProveedores : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerificarSaldoProveedores));
        private List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        private int xposition = 0;
        #endregion

        #region "Eventos"

        public FrmVerificarSaldoProveedores()
        {
            InitializeComponent();
        }

        private void RegistroDeDocumentosPorPagar_Load(object sender, EventArgs e)
        {
           
            Carga();
       
        }

        private void Carga()
        {
            //Lista = new BCuentasPorPagar().ListarEDocPorPagar(objE_DocPorPagar).OrderBy(ord => ord.doxpc_viid_correlativo).Where(obe => obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList(); //ordenarlo por su correlativo y no mostrar los adelantos;
            Lista = new BCuentasPorPagar().BuscarDocumentosXPagarProveedorVerificar();
            dgr.DataSource = Lista;
        }


        #endregion

        #region "Metodos"

        void Modify(long Cab_icod_correlativo)
        {
            Carga();
            int index = Lista.FindIndex(obe => obe.doxpc_icod_correlativo == Cab_icod_correlativo);
            //dgr.FocusedRowHandle = index;
        }
        #endregion

        private void Pagos_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)view.GetRow(view.FocusedRowHandle);

                switch (Obe.tdocc_icod_tipo_doc)
                {
                    case 1:
                        FrmConsultarAdelantoPagos av = new FrmConsultarAdelantoPagos();
                        av.eDocXPagar = Obe;
                        av.doxcc_icod_correlativo_adelanto = Obe.doxpc_icod_correlativo;
                        av.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 86:
                        FrmConsultaPagosNotaCredito FrmNotaC = new FrmConsultaPagosNotaCredito();
                        FrmNotaC.eDocXPagar = Obe;
                        FrmNotaC.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        FrmNotaC.Show();
                        xposition = view.FocusedRowHandle;
                        FrmNotaC.mnu.Enabled = false;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXPagar a = new FrmConsultaPagosDocumentosXPagar();
                        a.eDocXPagar = Obe;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }

            }
        }

        private void actualizarMontoPagadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lista.ForEach(x =>
            {
                if (x.doxpc_nmonto_total_pagado != x.PagadoReal)
                {
                    //Actualizar Monto Pagado Saldo
                    new BTesoreria().ActualizarMontoDXPPagadoSaldo(x.doxpc_icod_correlativo, x.tablc_iid_tipo_moneda);
                }
            });
        }




     
        
    }
}