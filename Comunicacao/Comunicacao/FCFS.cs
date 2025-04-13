namespace SistemasOperacionais
{
    internal class FCFS : Escalonador
    {
        public FCFS()
        {
            tipoEscalonador = "First Come First Served";
        }

        // First Come First Served
        public override void Escalonar(List<Processo> listaProcessos)
        {
            if (listaProcessos.Count <= 0)
            {
                return;
            }

            Clonar(listaProcessos);
            listaReady.ForEach(p => { p.Estado = "Ready"; Console.WriteLine(p.ProcID + ": " + p.Estado); });

            CalcularTempo(listaReady);
            //Console.WriteLine("First Come First Served: ");
            //CalcularTempo("First Come First Served");
            AlocarProximoProcesso();
        }
    }
}
