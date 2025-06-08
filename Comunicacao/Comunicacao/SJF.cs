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
            listaNew = listaNew.OrderBy(p => p.TempoExecucao).ToList();

            // ToList cria uma copia
            // caso contrario, excecao por remover elementos da lista iterada
            foreach (Processo processo in listaNew.ToList())
            {
                if (Kernel.AlocarProcesso(processo))
                {
                    processo.Estado = "Ready";
                    Console.WriteLine(processo.ProcID + ": " + processo.Estado);
                    listaReady.Add(processo);
                    listaNew.Remove(processo);
                }
            }

            //Console.WriteLine("Shortest Job First: ");
            //CalcularTempo("Shortest Job First");
            CalcularTempo(listaReady);
            AlocarProximoProcesso();
        }

        
    }
}
