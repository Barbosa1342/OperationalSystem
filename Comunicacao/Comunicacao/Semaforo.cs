namespace SistemasOperacionais
{
    static internal class Semaforo
    {
        static public int mutex = 1;
        static public int empty = 0;
        static public int full = 0;

        static public Processo? Up(ref int semaforo, List<Processo> processos)
        {
            semaforo++;

            return Kernel.GetRandomWaitingProcess(processos);
        }

        static public void Down(ref int semaforo, Processo processo)
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
