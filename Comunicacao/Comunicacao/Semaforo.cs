namespace SistemasOperacionais
{
    internal class Semaforo
    {
        public int mutex = 1;
        public int empty = 0;
        public int full = 0;

        public Processo? Up(ref int semaforo, List<Processo> processos)
        {
            semaforo++;

            return Kernel.GetRandomWaitingProcess(processos);
        }

        public void Down(ref int semaforo, Processo processo)
        {
            if (semaforo > 0)
            {
                semaforo--;
                processo.Estado = "Running";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);
            }
            else if(semaforo == 0)
            {
                processo.Estado = "Ready";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);
            }
        }
    }
}
