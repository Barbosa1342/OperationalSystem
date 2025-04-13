using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SistemasOperacionais
{
    internal class SJF : Escalonador
    {
        public SJF(){
            tipoEscalonador = "Shortest Job First";
        }
        // Shortest Job First
        public override void Escalonar(List<Processo> listaProcessos)
        {
            if (listaProcessos.Count <= 0)
            {
                return;
            }

            Clonar(listaProcessos);
            listaReady = listaReady.OrderBy(p => p.TempoExecucao).ToList();

            listaReady.ForEach(p => { p.Estado = "Ready"; Console.WriteLine(p.ProcID + ": " + p.Estado); });

            //Console.WriteLine("Shortest Job First: ");
            //CalcularTempo("Shortest Job First");
            CalcularTempo(listaReady);
            AlocarProximoProcesso();
        }

        
    }
}
