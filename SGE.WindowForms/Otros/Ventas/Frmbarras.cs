using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Windows.Forms.DataVisualization.Charting;

using SGE.Entity;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class Frmbarras : DevExpress.XtraEditors.XtraForm
    {
        public List<EProyeccionVendedor> lista = new List<EProyeccionVendedor>();
        public Frmbarras()
        {
            InitializeComponent();
        }

        private void Frmbarras_Load(object sender, EventArgs e)
        {
            var obj = lista.Where(x => x.vendc_vcod_vendedor == "AJ-01").FirstOrDefault();

            string[] series = { "cant_necesidad_futura", "cant_necesidad_inmediata", "cant_credito", "cant_contado", "proyc_icantidad_estimada" };
            int[] puntos = { obj.cant_necesidad_futura, obj.cant_necesidad_inmediata, obj.cant_credito, obj.cant_contado, obj.proyc_icantidad_estimada };

            chart1.Titles.Add("Ventas por Mes");

            for (int i = 0; i < series.Length; i++)
            {
                Series serie = chart1.Series.Add(series[i]);
                serie.Label = puntos[i].ToString();
                serie.Points.Add(puntos[i]);
            }

        }
    }
}