﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.Entity;
using SGE.BusinessLogic;


namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    public partial class FrmConsultaPagosDocumentosXCobrarAUnaFecha : DevExpress.XtraEditors.XtraForm
    {
        private List<EDocXCobrarPago> Lista = new List<EDocXCobrarPago>();

        //public long doxcc_icod_correlativo;
        public EDocXCobrar eDocXCobrar;
        public DateTime sfecha;

        public FrmConsultaPagosDocumentosXCobrarAUnaFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultaPagosDocumentosXCobrarAUnaFecha_Load(object sender, EventArgs e)
        {
            this.Text = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            grv.GroupPanelText = "PAGOS EFECTUADOS POR " + eDocXCobrar.Abreviatura + " - " + eDocXCobrar.doxcc_vnumero_doc;
            Carga();
        }
        private void Carga()
        {
            Lista = new BCuentasPorCobrar().ListarPagoDocumentoXCobrarXIdDocXCobrarAUnaFecha(eDocXCobrar.doxcc_icod_correlativo, Parametros.intEjercicio, sfecha);
            grd.DataSource = Lista;
        }

        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}