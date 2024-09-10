using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    public partial class frmFondo : DevExpress.XtraEditors.XtraForm
    {
        public frmFondo()
        {
            InitializeComponent();
        }

        private void frmFondo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
    }
}