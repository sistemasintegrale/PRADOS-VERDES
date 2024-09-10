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

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmConsultaVentasDaotDetalle : DevExpress.XtraEditors.XtraForm
    {
        public FrmConsultaVentasDaotDetalle()
        {
            InitializeComponent();
        }

        List<EVentasDaotDetalle> Lista = new List<EVentasDaotDetalle>();
        public long icod_clie;
        public string nombre_clie;

        private void FrmConsultaVentasDaotDetalle_Load(object sender, EventArgs e)
        {
            this.Text += nombre_clie;
            Carga();
            grd.DataSource = Lista;
        }

        public void Carga()
        {
            Lista = new BVentas().ListarVentasDaotDetallexCliente(icod_clie,Parametros.intEjercicio);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}