using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Contabilidad;

namespace SGE.WindowForms.Contabilidad.Mantenimiento
{
    public partial class frm01ParametroContable : DevExpress.XtraEditors.XtraForm
    {
        public frm01ParametroContable()
        {
            InitializeComponent();
        }

        private void rptFrm01ParametroContable_Load(object sender, EventArgs e)
        {            
             
        }

        private void rptFrm01ParametroContable_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteParametrosContables frm = new FrmManteParametrosContables();
            frm.Show();           
        }

        private void rptFrm01ParametroContable_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteParametrosContables frm = new FrmManteParametrosContables();
            frm.Show();           
        }
    }
}