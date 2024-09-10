using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SGE.WindowForms.Otros.Generacion_de_Vouchers
{
    public partial class frmGenerarVoucher : DevExpress.XtraEditors.XtraForm
    {
        public string strProceso = "";
        public bool inProcess = true;
        public frmGenerarVoucher()
        {
            InitializeComponent();
        }
        
        private void frmGenerarVoucher_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "Generando vouchers contables de: " + strProceso +"...";
        }

        private void frmGenerarVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (inProcess)
                e.Cancel = true;
        }    
    }
}