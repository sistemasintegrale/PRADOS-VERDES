using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class ResultadoComparacion : DocumentoComparacion
    {
        public string Origen { get; set; }
        public bool Importar { get; set; }


        public ResultadoComparacion(DocumentoComparacion obj, string Origen)
        {
            Ruc = obj.Ruc;
            TipoDocumento = obj.TipoDocumento;
            Serie = obj.Serie;
            Correlativo = obj.Correlativo;
            Proveedor = obj.Proveedor;
            Fecha = obj.Fecha;
            BaseImponible = obj.BaseImponible;
            Impuesto = obj.Impuesto;
            NoGravado = obj.NoGravado;
            MontoTotal = obj.MontoTotal;
            this.Origen = Origen;
            Importar = false;
            RucProveedor = obj.RucProveedor;
            FechaVencimiento = obj.FechaVencimiento;
            Moneda = obj.Moneda;
            TipoDocumentoReferencia = obj.TipoDocumentoReferencia;
            SerieDocumentoReferencia = obj.SerieDocumentoReferencia;
            NumeroDocumentoReferencia = obj.NumeroDocumentoReferencia;
            OtrosImpuestos = obj.OtrosImpuestos;
        }
    }
}
