using System;
using System.Runtime.InteropServices;

namespace ProjetoTCC.Functions
{
    [ComVisible(true)]
    public class Functions
    {
        public static void CalculaPeriodoEVencimento(string PeriodoI, string PeriodoF, out DateTime VencimentoInicial, out int f)
        {
            // Separa o ano do mês do Periodo inicial
            string MesIni = PeriodoI.Substring(4, 2);
            string AnoIni = PeriodoI.Substring(0, 4);

            int AnoIniInt = Convert.ToInt32(AnoIni);
            int MesIniInt = Convert.ToInt32(MesIni);

            // Separa o ano do mês do Periodo final
            string MesFin = PeriodoF.Substring(4, 2);
            string AnoFin = PeriodoF.Substring(0, 4);

            int AnoFinInt = Convert.ToInt32(AnoFin);
            int MesFinInt = Convert.ToInt32(MesFin);

            // Primeiro vencimento da mensalidade é o dia 5 correspondendo ao período inicial
            VencimentoInicial = new DateTime(AnoIniInt, MesIniInt, 5);

            int a = (AnoFinInt - AnoIniInt) * 12;
            int b = Math.Abs(MesFinInt - MesIniInt);

            // Se o mês inicial for menor que o mês final, faz a soma, senão, faz subtração
            f = (MesIniInt < MesFinInt ? a + b : a - b);
        }
    }
}