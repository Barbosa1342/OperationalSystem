namespace SistemasOperacionais
{
    internal class FCFS : Escalonador
    {
        // First Come First Served
        public override void Escalonar(List<Processo> listaProcessos)
        {
            if (listaProcessos.Count <= 0)
            {
                return;
            }

            listaOrdenada = listaProcessos.ToList();
            listaOrdenada.ForEach(p => { p.Estado = "Ready"; Console.WriteLine(p.ProcID + ": " + p.Estado); });

            Console.WriteLine("First Come First Served: ");
            CalcularTempo();
            ExecutarProcesso();
        }
    }
}
