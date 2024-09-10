using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Sire
{
    public partial class Frm03RegistroVentas : DevExpress.XtraEditors.XtraForm
    {
        private List<ERegistroVentas> Lista = new List<ERegistroVentas>();
        private string rutaTXT;
        public Frm03RegistroVentas()
        {
            InitializeComponent();
        }

        private void frmRegistroVentas_Load(object sender, EventArgs e)
        {
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses.Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void Carga()
        {
            Lista = new BContabilidad().ListarRegistroDeVentas(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoCliente).ToList();
            Lista = Lista.OrderBy(ord => ord.tdocc_vabreviatura_tipo_doc).ThenBy(ord => ord.doxcc_vnumero_doc).ToList();
            grd.DataSource = Lista;
            grv.BestFitColumns();
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
        }
        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                 
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grd.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grd.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }             

            }
        }
        string Mes;

        private void tXTToolStripMenuItem_Click(object sender, EventArgs e) => ExpotarTXT(abrir: true);

        private void ExpotarTXT(bool abrir) {
            try
            {
                string ConSinInfo;
                if (Lista.Count > 0)
                {
                    ConSinInfo = "1";
                }
                else
                {
                    ConSinInfo = "2";
                }
                if (lkpMes.EditValue.ToString().Trim().Length == 1)
                {
                    Mes = "0" + lkpMes.EditValue.ToString();
                }
                else
                {
                    Mes = lkpMes.EditValue.ToString();
                }
                string Nombre = "LE" + Valores.strRUC + Parametros.intEjercicio.ToString() + Mes + "00" + "140400" + "02" + "1" + ConSinInfo + "1" + "2";
                sfdTXT.FileName = Nombre;
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {                  
                    string fileName = sfdTXT.FileName.Contains(".txt") ? sfdTXT.FileName : sfdTXT.FileName + ".txt";
                    ExportarATXT(fileName);
                    if (abrir)
                        Process.Start(fileName);
                    rutaTXT = fileName;
                }
                sfdTXT.FileName = string.Empty;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXT(String ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Lista.Count;
                foreach (ERegistroVentas item in Lista)
                {
                    cont++;
                    error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxcc_vnumero_doc;
                    string _separador = "|";

                    columna = "1";
                    sw.Write(Valores.strRUC + _separador); // 1

                    columna = "2";
                    sw.Write(Valores.strNombreEmpresa + _separador);

                    columna = "3";
                    sw.Write(Parametros.intEjercicio + Mes + _separador);//PERIODO

                    columna = "4";
                    sw.Write(_separador); // 4

                    columna = "5";
                    sw.Write((item.doxcc_sfecha_doc?.ToString("dd/MM/yyyy") ?? "") + _separador); // 5

                    columna = "6";
                    sw.Write((item.doxcc_sfecha_vencimiento_doc?.ToString("dd/MM/yyyy") ?? "") + _separador); // 6

                    columna = "7";
                    sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + _separador); // 7

                    columna = "8";
                    sw.Write(item.doxcc_vnumero_doc.Substring(0, 4) + _separador); // 8

                    columna = "9";
                    sw.Write(item.doxcc_vnumero_doc.Substring(4) + _separador); // 9

                    columna = "10";
                    sw.Write(_separador); // 10

                    columna = "11";
                    sw.Write(item.tip_doc_cliente_2 + _separador); // 1

                    columna = "12";
                    sw.Write(item.num_doc_cliente_2.TrimEnd().TrimStart() + "|"); // 1

                    columna = "13";
                    sw.Write(item.cliec_vnombre_cliente_2 + "|");

                    columna = "14";
                    sw.Write("0.00" + _separador);

                    columna = "15";
                    sw.Write((item.doxcc_nmonto_afecto?.ToString("0.00") ?? "0.00") + _separador); // 14

                    columna = "16";
                    sw.Write((item.valor_ex?.ToString("0.00") ?? "0.00") + _separador); // 15

                    columna = "17";
                    sw.Write((item.doxcc_nmonto_impuesto?.ToString("0.00") ?? "0.00") + _separador); // 18

                    columna = "18";
                    sw.Write((item.valor_ex?.ToString("0.00") ?? "0.00") + _separador); // 15

                    columna = "19";
                    sw.Write((item.valor_ex?.ToString("0.00") ?? "0.00") + _separador); // 15

                    columna = "20";
                    sw.Write("0.00" + _separador); // 16

                    columna = "21";
                    //sw.Write(String.Format("{0:0.00}", item.doxcc_nmonto_isc) + "|"); // 17
                    sw.Write("0.00" + _separador);
                    columna = "22";
                    sw.Write((item.base_imp_ivap?.ToString("0.00") ?? "0.00") + _separador); // 19
                    columna = "23";
                    sw.Write(("0.00") + _separador); // 20
                    //sw.Write("0.00" + "|");
                    columna = "24";
                    sw.Write("0.00" + _separador); // 21
                    columna = "25";
                    sw.Write("0.00" + _separador); // 21
                    columna = "26";
                    sw.Write((item.doxcc_nmonto_total?.ToString("0.00") ?? "0.00") + _separador); // 22
                    //sw.Write(((item.doxcc_sfecha_emision_referencia != null) ? Convert.ToDateTime(item.doxcc_sfecha_emision_referencia).ToString("dd/MM/yyyy") : "") + "|"); // 25
                    columna = "27";
                    string Moneda = "";
                    if (item.tablc_iid_tipo_moneda == 3)
                    {
                        Moneda = "PEN";
                    }
                    else
                    {
                        Moneda = "USD";
                    }
                    sw.Write(Moneda + "|"); // 22
                    columna = "28";
                    sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxcc_nmonto_tipo_cambio), 3)) + "|"); // 23
                    columna = "29";
                    sw.Write((item.doxcc_sfecha_emision_referencia?.ToString("dd/MM/yyyy") ?? "") + "|"); // 27
                    columna = "30";
                    string indicador = "";
                    if (item.tablc_iid_situacion_documento == 11)
                    {
                        indicador = "";
                    }
                    else
                    {
                        if (item.doxcc_tipo_comprobante_referencia == 26)
                        {
                            indicador = "01";
                        }
                        else if (item.doxcc_tipo_comprobante_referencia == 9)
                        {
                            indicador = "03";
                        }
                        else
                        {
                            indicador = "";
                        }
                    }

                    sw.Write(indicador + "|"); // 29
                    columna = "31";
                    sw.Write(item.doxcc_num_serie_referencia + "|"); // 29
                    // 30 NO SE APLICA
                    columna = "32";
                    sw.Write(item.doxcc_num_comprobante_referencia + "|"); // 30
                    columna = "33";
                    sw.Write("" + "|"); // 31
                    if (totFilas != cont)
                    {
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
            }
            finally
            {
                sw.Close();
            }
        }

        private void exportarZipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpotarTXT(abrir: false);
            string rutaZip = rutaTXT.Replace("txt", "zip");
            Ziperar.CrearArchivoZip(archivoPlano: rutaTXT, rutaArchivoZip: rutaZip);
            File.Delete(rutaTXT);
            Process.Start(fileName: Path.GetDirectoryName(rutaZip));
        }
    }
}