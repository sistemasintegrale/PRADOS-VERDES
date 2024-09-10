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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Sire
{
    public partial class Frm03RegistroCompras : DevExpress.XtraEditors.XtraForm
    {

        private List<ERegistroCompras> Lista = new List<ERegistroCompras>();
        private List<ERegistroCompras> ListaGeneral = new List<ERegistroCompras>();

        private List<ERegistroCompras> ListaND = new List<ERegistroCompras>();
        string mes;
        string rutaTXT;
        string rutaTXTND;
        public Frm03RegistroCompras()
        {
            InitializeComponent();
        }

        private void frmRegistroCompras_Load(object sender, EventArgs e)
        {
            var lstMeses = new BGeneral().listarTablaRegistro(4);
            BSControls.LoaderLook(lkpMes, lstMeses.Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", false);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
        }

        private async void Carga()
        {
            int index = 0;
            try
            {
                await Task.WhenAll(CargaMultiple(Convert.ToInt32(lkpMes.EditValue)));

                var ersult = RegistroComprasConvert.Convertir(ListaGeneral, Valores.strRUC, Valores.strNombreEmpresa, $"{Parametros.intEjercicio}{Convert.ToInt32(lkpMes.EditValue).ToString("D2")}");
                grd.DataSource = ersult;
                viewLista.BestFitColumns();
            }
            catch (Exception ex)
            {
                index = index;
                throw ex;
            }
        }

        async Task CargaMultiple(int mes)
        {
            await CargarListaGeneral(mes);
            await CargaLista(mes);
            await NoDomiciliados(mes);
        }

        async Task CargarListaGeneral(int mes)
        {
            var task = new Task<List<ERegistroCompras>>(() => new BContabilidad().ListarRegistroDeComprasGeneral(Parametros.intEjercicio, mes).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).OrderBy(ord => ord.doxpc_viid_correlativo).ToList());
            task.Start();
            ListaGeneral = await task;
        }

        async Task CargaLista(int mes)
        {
            var task = new Task<List<ERegistroCompras>>(() => new BContabilidad().ListarRegistroDeCompras(Parametros.intEjercicio, mes).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).OrderBy(ord => ord.doxpc_viid_correlativo).ToList());
            task.Start();
            Lista = await task;
        }

        async Task NoDomiciliados(int mes)
        {
            var task = new Task<List<ERegistroCompras>>(() => new BContabilidad().ListarRegistroDeComprasNoDomic(Parametros.intEjercicio, mes).Where(ob => ob.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).OrderBy(ord => ord.doxpc_viid_correlativo).ToList());
            task.Start();
            ListaND = await task;
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



        private void exportarATXTToolStripMenuItem_Click(object sender, EventArgs e) => ExpotarTXT(abrir: true);

        private void ExpotarTXT(bool abrir)
        {
            try
            {
                string ConSinInfo = Lista.Count() == 0 ? "2" : "1";
                mes = lkpMes.EditValue.ToString().Trim();
                mes = mes.Length == 0 ? "0" + mes : mes;
                sfdTXT.FileName = "LE" + Valores.strRUC + Parametros.intEjercicio.ToString() + mes + "00" + "080400" + "02" + "1" + ConSinInfo + "1" + "2"; ;
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = sfdTXT.FileName.Contains(".txt") ? sfdTXT.FileName : sfdTXT.FileName + ".txt";
                    ExportarATXT(fileName);
                    if (abrir)
                        Process.Start(fileName);
                    rutaTXT = fileName;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ExportarATXT(string ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string _separador = "|";
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = Lista.Count;
                foreach (ERegistroCompras item in Lista)
                {
                    cont++;
                    error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxpc_vnumero_doc;

                    columna = "1";
                    sw.Write(Valores.strRUC + _separador); //RUC

                    columna = "2";
                    sw.Write(Valores.strNombreEmpresa + _separador); //ID

                    columna = "3";
                    sw.Write(Parametros.intEjercicio + mes + _separador);//PERIODO

                    columna = "4";
                    sw.Write(_separador);//CAR

                    columna = "5";
                    sw.Write(item.doxpc_sfecha_doc.Value.ToString("dd/MM/yyyy") + _separador);//FECHA EMISION

                    columna = "6";
                    sw.Write((item.doxpc_sfecha_vencimiento_doc == null ? _separador : item.doxpc_sfecha_vencimiento_doc.Value.ToString("dd/MM/yyyy")) + _separador); //FECHA VCTO/PAGO

                    columna = "7";
                    sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + _separador); //TIPO CP/DOC

                    columna = "8";
                    if (item.doxpc_numdoc_tipo == 1)
                    {

                        if (item.tdocc_icod_tipo_doc == 5)//Boleto Aerio
                        {
                            //if (item.doxpc_itipo_bol_avion == 643)
                            //{
                            //    sw.Write("1" + "|"); // 7 
                            //}
                            //else if (item.doxpc_itipo_bol_avion == 644)
                            //{
                            //    sw.Write("2" + "|"); // 7 
                            //}
                            //else if (item.doxpc_itipo_bol_avion == 645)
                            //{
                            //    sw.Write("3" + "|"); // 7 
                            //}
                            //else if (item.doxpc_itipo_bol_avion == 646)
                            //{
                            //    sw.Write("4" + "|"); // 7 
                            //}

                        }
                        else
                        {
                            sw.Write(item.doxpc_vnumero_serio + "|"); // 7
                        }

                    }
                    else
                    {

                        if (item.tdocc_vcodigo_tipo_doc_sunat == "50")
                        {
                            sw.Write(item.doxpc_vnumero_doc.Substring(0, 3) + "|");
                        }
                        else if (item.tdocc_icod_tipo_doc == 5)
                        {
                            sw.Write("1" + "|");
                        }
                        else
                        {
                            sw.Write("" + "|"); // 7
                        }
                    }

                    columna = "9";
                    sw.Write(_separador); //AÑO

                    columna = "10";
                    if (item.doxpc_numdoc_tipo == 1)
                    {
                        if (item.tdocc_icod_tipo_doc == 5)//Boleto Aerio
                        {

                            if (item.doxpc_vnumero_serio == "" || item.doxpc_vnumero_serio == null)
                            {
                                sw.Write(item.doxpc_vnumero_serio + item.doxpc_vnumero_correlativo + "|");
                            }
                            else
                            {
                                sw.Write(item.doxpc_vnumero_serio.Substring(1) + item.doxpc_vnumero_correlativo + "|");
                            }


                        }
                        else
                        {

                            sw.Write(item.doxpc_vnumero_correlativo.TrimStart('0') + "|"); // 9
                        }
                    }
                    else
                    {
                        if (item.tdocc_icod_tipo_doc == 119 || item.tdocc_icod_tipo_doc == 49)
                        {
                            sw.Write(item.doxpc_vnumero_doc + "|");
                        }
                        else if (item.tdocc_icod_tipo_doc == 5)
                        {
                            sw.Write(item.doxpc_vnumero_doc + "|");
                        }
                        else
                        {
                            sw.Write(item.doxpc_vnumero_doc.Substring(9) + "|");
                        }

                    }

                    columna = "11";
                    sw.Write(_separador);

                    columna = "12";
                    sw.Write(item.tip_doc_proveedor + _separador); // 

                    columna = "13";
                    sw.Write(item.num_doc_proveedor.TrimEnd().TrimStart() + _separador); // 

                    columna = "14";
                    sw.Write(item.proc_vnombrecompleto + _separador); // 

                    columna = "15";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_gravado) + "|"); // 14
                    columna = "16";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_gravado) + "|"); // 15
                    columna = "17";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_mixto) + "|"); // 16
                    columna = "18";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_mixto) + "|"); // 17
                    columna = "19";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_destino_nogravado) + "|"); // 18
                    columna = "20";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_imp_destino_nogravado) + "|"); // 19
                    columna = "21";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_nogravado) + "|"); // 20
                    columna = "22";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_isc) + "|"); // 21
                    columna = "23";
                    sw.Write(string.Format("{0:0.00}", ("0"/*item.doxpc_otros_impuestos*/)) + "|"); // 22
                    columna = "24";
                    sw.Write(string.Format("{0:0.00}", 0) + "|"); // 22
                    columna = "25";
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|"); // 23
                    columna = "26";
                    if (item.tablc_iid_tipo_moneda == 3)
                    {
                        sw.Write("PEN" + "|"); // 24
                    }
                    else
                    {
                        sw.Write("USD" + "|"); // 24
                    }
                    columna = "27";
                    if (item.tablc_iid_tipo_moneda == 3)
                        sw.Write("|"); // 24
                    else
                        sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio), 3)) + "|"); // 24
                    columna = "28";
                    sw.Write(((item.doxpc_sfecha_emision_referencia != null) ? Convert.ToDateTime(item.doxpc_sfecha_emision_referencia).ToString("dd/MM/yyyy") : "") + "|"); // 25
                    columna = "29";
                    sw.Write(item.nc_dxc_tipodoc + "|"); // 
                    columna = "30";
                    sw.Write(item.doxpc_num_serie_referencia + "|"); //                
                    columna = "31";
                    sw.Write("|");
                    columna = "32";
                    sw.Write((item.doxpc_num_comprobante_referencia.TrimStart('0').Length == 0 ? "0" : item.doxpc_num_comprobante_referencia.TrimStart('0')) + "|"); // 
                    columna = "33";
                    sw.Write("0" + "|"); // 33//tipo adquisicion
                    columna = "34";
                    sw.Write("" + "|"); // 
                    columna = "35";
                    sw.Write("0" + "|"); //
                    columna = "36";
                    sw.Write("0" + "|"); //
                    columna = "37";
                    sw.Write("" + "|"); //
                    columna = "38";
                    sw.Write("" + "|"); //
                    columna = "39";
                    sw.Write("" + "|"); //  
                    columna = "40";
                    sw.Write("" + "|"); //  
                    columna = "41";
                    sw.Write("0" + "|"); //                  

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

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exportarZIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpotarTXT(abrir: false);
            string rutaZip = rutaTXT.Replace("txt", "zip");
            Ziperar.CrearArchivoZip(archivoPlano: rutaTXT, rutaArchivoZip: rutaZip);
            File.Delete(rutaTXT);
            Process.Start(fileName: Path.GetDirectoryName(rutaZip));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) => ExportarTxtND();

        public void ExportarTxtND(bool abrir = true)
        {

            try
            {
                string ConSinInfo = ListaND.Count > 0 ? "1" : "0";
                mes = lkpMes.EditValue.ToString().Trim();
                mes = mes.Length == 0 ? "0" + mes : mes;
                string Nombre = "LE" + Valores.strRUC + Parametros.intEjercicio.ToString() + mes + "00" + "080500" + "00" + "1" + ConSinInfo + "1" + "2";
                sfdTXT.FileName = Nombre;
                if (sfdTXT.ShowDialog(this) == DialogResult.OK)
                {
                    rutaTXTND = sfdTXT.FileName.Contains(".txt") ? sfdTXT.FileName : sfdTXT.FileName + ".txt";
                    ExportarATXTNoDomi();
                    if (abrir)
                        Process.Start(rutaTXTND);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXTNoDomi()
        {
            StreamWriter sw = new StreamWriter(rutaTXTND);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                int totFilas = ListaND.Count;
                foreach (ERegistroCompras item in ListaND)
                {
                    cont++;
                    error = item.tdocc_vabreviatura_tipo_doc + " " + item.doxpc_vnumero_doc;
                    columna = "1";// Periodo
                    sw.Write(Parametros.intEjercicio + mes +"|");
                    columna = "2";// CAR     
                    sw.Write("|");                    
                    columna = "3";// F.Emision Comprbante de Pago o Documento
                    sw.Write(Convert.ToDateTime(item.doxpc_sfecha_doc).ToString("dd/MM/yyyy") + "|");
                    columna = "4"; // Tipo Comprobante de Pago
                    sw.Write(item.tdocc_vcodigo_tipo_doc_sunat + "|");

                    columna = "5";// NO SE APLICA
                    sw.Write("|");

                    columna = "6";// Numero de Comprobante de Pago
                    if (item.doxpc_numdoc_tipo == 1)
                    {
                        if (item.tdocc_icod_tipo_doc == 5)
                        {
                            sw.Write(item.doxpc_vnumero_serio.Substring(1) + item.doxpc_vnumero_correlativo + "|"); // 9
                        }
                        else
                        {
                            sw.Write(item.doxpc_vnumero_correlativo + "|"); // 9
                        }
                    }
                    else
                    {
                        sw.Write(item.doxpc_vnumero_doc + "|"); // 9
                    }
                    columna = "7";// Valor de las Adquisiciones
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|");
                    columna = "8";// Otros Conceptos Adicionales
                    sw.Write("0.00" + "|");
                    columna = "9";// Total Importe Adquisiciones
                    sw.Write(String.Format("{0:0.00}", item.doxpc_nmonto_total_documento) + "|");
                    columna = "10";// Tipo Comprobante
                    sw.Write("50" + "|");
                    columna = "11";// Serie De Comprobante de Pago o Doc
                    sw.Write(item.doxpc_codigo_aduana + "|");
                    columna = "12";// Anio Emisio de la DUA 
                    sw.Write(item.doxpc_anio + "|");
                    columna = "13";// Numero Comprobante de Pago o Doc/ Num de Orden de Formulario
                    sw.Write(item.doxpc_numero_declaracion + "|");
                    columna = "14";// Monto Retencion IGV
                    sw.Write("0.00" + "|");
                    columna = "15";// Tipo Moneda
                    if (item.tablc_iid_tipo_moneda == 3)
                    {
                        sw.Write("PEN" + "|");
                    }
                    else
                    {
                        sw.Write("USD" + "|");
                    }
                    columna = "16";//Tipo de Cambio
                    sw.Write(String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio), 3)) + "|");
                    columna = "17";// Pais de la Residencia del Sujeto no Domiciliado
                    sw.Write(item.proc_pais_nodomic + "|");
                    columna = "18";// Apellidos, Nombres, Denominacion o razon social del sujeto NoDomi
                    sw.Write(item.proc_vnombrecompleto + "|");
                    columna = "19";// Domicilio En el Extranjero del Sujeto NoDomi
                    sw.Write(item.proc_vdireccion + "|");
                    columna = "20";// Numero de Identificacion del Sujeto NoDomi
                    sw.Write(item.proc_vdni + "|");
                    columna = "21";// Pagos
                    sw.Write("|");
                    columna = "22";// Apellidos, Nombres, Denominacion o razon social del Beneficiario Efectivo d elos Pagos
                    sw.Write("|");
                    columna = "23";// Pais de la Residencia del Beneficiario Efectivo de los Pagos
                    sw.Write("|");
                    columna = "24";// Vinculo entre Contribuyente y el Residente en el Extranjero
                    sw.Write("|");
                    columna = "25";// Renta Bruta
                    sw.Write("0.00" + "|");
                    columna = "26";// Deducion/ Costo de Enajeracion de bienes de capital
                    sw.Write("0.00" + "|");
                    columna = "27";// Renta Neta
                    sw.Write("0.00" + "|");
                    columna = "28";// Tasa de Retencion
                    sw.Write("0.00" + "|");
                    columna = "29";// Impuesto Retenido
                    sw.Write("0.00" + "|");
                    columna = "30";// Convenios para evitar Doble Imposicion
                    sw.Write("00" + "|");
                    columna = "31";// Exoneracion Aplicada
                    sw.Write("|");
                    columna = "32";// Tipo Renta
                    sw.Write("00" + "|");
                    columna = "33";// Modalidad de Servicio Prestado por el NoDomic
                    sw.Write("|");
                    columna = "34";// Aplicacion de penultimo parrafo del Art 76° de la ley Impuesto a la Rebta
                    sw.Write("|");
                    columna = "35";// Estado que Identifica la Oportunidad de la Anotacion 
                    sw.Write("0" + "|");
                    columna = "36";// Campos de Libre Utilizacion 
                    sw.Write("|");
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

        private void exportarZIPNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportarTxtND(abrir: false);
            string rutaZip = rutaTXTND.Replace("txt", "zip");
            Ziperar.CrearArchivoZip(archivoPlano: rutaTXTND, rutaArchivoZip: rutaZip);
            File.Delete(rutaTXTND);
            Process.Start(fileName: Path.GetDirectoryName(rutaZip));
        }
    }
}