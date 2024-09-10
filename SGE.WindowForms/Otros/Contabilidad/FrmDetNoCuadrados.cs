﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using SGE.Entity;
using SGE.BusinessLogic;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmDetNoCuadrados : DevExpress.XtraEditors.XtraForm
    {
        List<EComprobanteDetalle> mlistDetail = new List<EComprobanteDetalle>();
        public int code;
        public FrmDetNoCuadrados()
        {
            InitializeComponent();
        }

        private void FrmDetNoCuadrados_Load(object sender, EventArgs e)
        {
            mlistDetail = new BContabilidad().ListarComprobanteDetalle(code);
            //grd.DataSource = mlistDetail.Where(oBe => oBe.ctacc_iid_cuenta_contable_ref == null).ToList();
            grd.DataSource = mlistDetail;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}