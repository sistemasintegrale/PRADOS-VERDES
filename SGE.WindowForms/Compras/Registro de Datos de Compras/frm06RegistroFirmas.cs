using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Otros.Compras;

namespace SGE.WindowForms.Compras.Registro_de_Datos_de_Compras
{
    public partial class frm06RegistroFirmas : DevExpress.XtraEditors.XtraForm
    {
        public frm06RegistroFirmas()
        {
            InitializeComponent();
        }

        private void rptFrm01ParametroContable_Load(object sender, EventArgs e)
        {            
             
        }

        private void rptFrm01ParametroContable_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteRegistroFirmas frm = new FrmManteRegistroFirmas();
            frm.Show();           
        }

        private void rptFrm01ParametroContable_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            FrmManteRegistroFirmas frm = new FrmManteRegistroFirmas();
            frm.Show();           
        }
    }
}