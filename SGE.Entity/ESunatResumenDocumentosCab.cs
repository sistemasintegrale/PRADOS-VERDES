﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity.FacturaElectronica
{
    public class ESunatResumenDocumentosCab
    {
        public int IdCabecera { get; set; }
        public string IdDocumento { get; set; }
        public string FechaEmision { get; set; }
        public string FechaGeneracion { get; set; }
        public string NroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreLegal { get; set; }
        public string NombreComercial { get; set; }
        public string Ubigeo { get; set; }
        public string Direccion { get; set; }
        public string Urbanizacion { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public int EstadoResumen { get; set; }
        public int doc_icod_documento { get; set; }
        public string EstadoSunat { get; set; }
        public bool indicador_check { get; set; }
        public string FEmisionPresentacion { get; set; }


        public int IdItems { get; set; }
        public int IdCabeceraE { get; set; }
        public int Id { get; set; }
        public string TipoDocumentoE { get; set; }
        public string IdDocumentoE { get; set; }
        public string Moneda { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal Gravadas { get; set; }
        public decimal Inafectas { get; set; }
        public string NroDoc { get; set; }

        public DateTime Fecha { get; set; }
        public string strTipoDoc { get; set; }
        public string NroTicket { get; set; }
        public string MensajeError { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdResponse { get; set; }


    }
}
