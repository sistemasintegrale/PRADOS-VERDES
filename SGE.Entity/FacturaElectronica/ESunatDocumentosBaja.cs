﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity.FacturaElectronica
{
    public class ESunatDocumentosBaja
    {
        public int IdCabecera { get; set; }
        public int Id { get; set; }
        public string FechaEmision { get; set; }
        public string FEmisionPresentacion { get; set; }
        public string TipoDocumento { get; set; }
        public string StrTipoDoc { get; set; }
        public string Serie { get; set; }
        public string Correlativo { get; set; }
        public string MotivoBaja { get; set; }
        public int contador { get; set; }
        public int EstadoFacturacion { get; set; }
        public string EstadoSunat { get; set; }
        public int Dias { get; set; }
        public bool indicador_check { get; set; }
        public string Mensaje { get; set; }


    }
}
