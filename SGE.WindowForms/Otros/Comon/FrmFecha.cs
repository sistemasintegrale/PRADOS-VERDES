using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace SGE.WindowForms.Otros.Comon
{
    public partial class FrmFecha : DevExpress.XtraEditors.XtraForm
    {
        public DateTime FechaAnterior;
        public FrmFecha() => InitializeComponent();
        private void FrmFecha_Load(object sender, EventArgs e) { }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => this.Dispose();

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => Aceptar();

        void Aceptar()
        {
            if (string.IsNullOrEmpty(dtFecha.Text))
            {
                XtraMessageBox.Show("Ingrese La Fecha", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (FechaAnterior > dtFecha.DateTime)
            {
                XtraMessageBox.Show("La Fecha de Entrega no Puede ser Menor que la Fecha de Registro", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}