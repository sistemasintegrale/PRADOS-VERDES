using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGE.Entity
{
    public class EplanillaContDetalle:EAuditoria
    {
        public decimal Basicodelmes                     { get; set; }
        public decimal Comisiones_Mes                   { get; set; }
        public decimal Remuneracion_Mensual             { get; set; }
        public decimal Meses_Faltan                     { get; set; }
        public decimal Remuneracion_proyectada          { get; set; }
        public decimal Gratificacion_Ordinaria          { get; set; }
        public decimal Remuneracion_Anteriores          { get; set; }
        public decimal Comisiones                       { get; set; }
        public decimal Remuneracion_Anual               { get; set; }
        public decimal Menos_7UIT                       { get; set; }
        public decimal Renta_Neta_Global_Anual          { get; set; }
        public decimal Hasta_5_UIT                      { get; set; }
        public decimal Esceso_5_UIT                     { get; set; }
        public decimal Esceso_20_UIT                    { get; set; }
        public decimal Esceso_35_UIT                    { get; set; }
        public decimal Esceso_45_UIT                    { get; set; }
        public decimal Renta_Neta_Global_Anual_RNGA     { get; set; }
        public decimal Hasta_5                          { get; set; }
        public decimal Hasta_20                         { get; set; }
        public decimal Hasta_35                         { get; set; }
        public decimal Hasta_45                         { get; set; }
        public decimal Mas_45                           { get; set; }
        public decimal Impuesto_Resultante              { get; set; }
        public decimal Menos_Retenciones_IR             { get; set; }
        public decimal Impuesto_pagar                   { get; set; }
        public int     meses                            { get; set; }
        public decimal Retencion_mensual                { get; set; }
        public decimal r_5ta                            { get; set; }

    }
}
