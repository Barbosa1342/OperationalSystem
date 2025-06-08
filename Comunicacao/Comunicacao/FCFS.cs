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


            CalcularTempo(listaReady);
            //Console.WriteLine("First Come First Served: ");
            //CalcularTempo("First Come First Served");
            AlocarProximoProcesso();
        }
    }
}
