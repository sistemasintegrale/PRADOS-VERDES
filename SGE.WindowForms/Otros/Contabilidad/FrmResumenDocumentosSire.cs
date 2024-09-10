using Newtonsoft.Json;
using SGE.BusinessLogic;
using SGE.Entity.Sire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmResumenDocumentosSire : DevExpress.XtraEditors.XtraForm
    {
        public SireRequest request;
        public List<ResumenDto> lista = new List<ResumenDto>();


        public FrmResumenDocumentosSire()
        {
            InitializeComponent();
        }

        private void FrmResumenDocumentosSire_Load(object sender, System.EventArgs e) => ConsultarResumenDocumentos();

        private async Task ConsultarResumenDocumentos()
        {
            try
            {
                var responseBody = await SireServiceImpl.ConsultarResumenDocumentos(request);
                var lstPropuesta = JsonConvert.DeserializeObject<ResumenDocumentos>(responseBody.Data);

                if (request.operacion == "COMPRA")
                    lstPropuesta.Registros.ForEach(x =>
                    {
                        lista.Add(new ResumenDto()
                        {
                            tipoDoc = x.CodTipoCdp + " - " + x.DesCodTipoCdp,
                            totalDocumentos = Convert.ToInt32(x.CntDocumentos),
                            totalGravado = x.MtoBiGravadoDg,
                            totalIGV = x.MtoIgvIpmDg,
                            totalDestinonoGravado = x.MtoValorAdqNg,
                            totalOtrosTributos = x.MtoOtrosTrib,
                            totalCP = x.MtoTotalCp
                        });

                    });
                else
                    lstPropuesta.Registros.ForEach(x =>
                    {
                        lista.Add(new ResumenDto()
                        {
                            tipoDoc = x.CodTipoCdp + " - " + x.DesCodTipoCdp,
                            totalDocumentos = Convert.ToInt32(x.CntDocumentos),
                            totalGravado = x.MtoTotalBiGravada,
                            totalIGV = x.MtoTotalIgv,
                            totalDestinonoGravado = 0,
                            totalOtrosTributos = x.MtoOtrosTrib,
                            totalCP = x.MtoTotalCp

                        });

                    });


                grdLista.DataSource = lista;
                grdLista.RefreshDataSource();
                viewLista.BestFitColumns();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var ruta = saveFileDialog.FileName.Contains(".xlsx") ? saveFileDialog.FileName : saveFileDialog.FileName + ".xlsx";
                grdLista.ExportToXlsx(ruta);
                Process.Start(ruta);
            }
        }
    }
}