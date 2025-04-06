using System.Diagnostics;

namespace SistemasOperacionais
{
    internal class SJF : Escalonador
    {
        // Shortest Job First
        public override void Escalonar(List<Processo> listaProcessos)
        {
            if (listaProcessos.Count <= 0)
            {
                return;
            }

            listaReady = listaProcessos.OrderBy(p => p.TempoExecucao).ToList();

            listaReady.ForEach(p => { p.Estado = "Ready"; Console.WriteLine(p.ProcID + ": " + p.Estado); });

            //Console.WriteLine("Shortest Job First: ");
            //CalcularTempo("Shortest Job First");
            AlocarProximoProcesso();
        }
    }
}
