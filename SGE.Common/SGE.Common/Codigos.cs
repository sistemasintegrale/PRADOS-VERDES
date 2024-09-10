namespace SGE.Common
{
    public static class Codigos
    {
        public enum CodigoPlan
        {
            Id = 2,
        }



        public enum TipoSepultura
        {
            Id = 3,
            EspacioPersonal = 4,
            IndividualCompartidoJumbo = 23,
            EspacioPersonalPreferencial = 24,
            FamiliarDoble = 25,
            FamiliarTriple = 26,
            IndividualCompartido = 27,
            ParvuloPersonal = 28,
            ParvulitoCompartido = 29,
            FamiliarCuadruple = 30,
            FamiliarQuintuple = 31,
            AmpliacionPersonal = 329,
            Ampliacion23456 = 330,
            ParvuloCompartido = 341,
            FamiliarSextuplePreferencial = 353,
            Doble = 356,
            FamiliarDoblePreferencial = 358,
            IndividualCompartidoEspecial = 372,
            FamiliarDobleEleccion = 382,
            FamiliarTripleEleccion = 2377,
            FamiliarTriplePreferencial = 3385,
            FamiliarSextupleEleccion = 3387,
            FamiliarQuintupleEleccion = 3402,
            IndividualCompartidoPromocional = 4403,
            FamiliarTriplePromocional = 4404,
            FamiliarCuadruplePromocional = 4406,
            FamiliarQuintuplePromocional = 4408,
            Ampliacion23 = 4411,
            FamiliarDoblePromocional = 5416,
            FamiliarQuintuplePreferencial = 5429,
            IndCompEspecialPromocional = 6429,
            AmpliacionParvulitoPersonal = 6439,
            AmpliacionDoble = 6440,
            AmpliacionFamiliar = 6441,
            AmpliacionParvuloPersonal = 7440,
            ParvulitoPersonal = 7455,
            OsarioCinerarioPersonal = 8453,
            AmpliacionPersonalJumbo = 8454,
            EspacioPersonalJumbo = 10466,
            ParvulitoCompPromocional = 11477,
            FamiliarTriplePromocionalPreferencial = 13532,
            AmpliacionNivel3 = 24572,
            OsarioCinerarioDoble = 25575,
            AmpliacionNivel2 = 26582,
            FamiliarSextuple = 26589,
            ParvuloParvulitoOsarioCinerario = 26597,
            AmpliacionTriple = 27598,
        }
        public enum NombrePlan
        {
            Id = 13,
            NecesidadInmediata = 326,
            NecesidadFutura = 327,
            NecesidadInmediataProcional = 26599,
        }
        public enum SituacionContrato
        {
            Id = 14,
            Generado = 331,
            Anulado = 332,
            ConPagos = 25582
        }

        public enum TipoPago
        {
            Id = 97,
            Credito = 674,
        }

    }
}
