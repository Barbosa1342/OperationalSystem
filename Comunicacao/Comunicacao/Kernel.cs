namespace SistemasOperacionais
{
    internal class Kernel
    {
        List<Processo> processos = new List<Processo>();

        public void PopulateProcessQueue(Processo processo)
        {
            processo.Estado = "Ready";
            Console.WriteLine(processo.ProcID + ": " + processo.Estado);
            processos.Add(processo);
        }

        public void ProdutorComunicacao(List<int> items, Semaforo semaforo, Processo processo, bool isUp)
        {
            if (!(processo is Produtor))
            {
                Console.WriteLine("Processo executado de maneira incorreta.");
                return;
            }

            while (true)
            {
                semaforo.Down(ref semaforo.mutex, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                semaforo.Down(ref semaforo.empty, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                processo.Acao(items);

                Processo? upProcess = null;
                if (!isUp)
                {
                    upProcess = semaforo.Up(ref semaforo.mutex, processos);
                }
             
                semaforo.Up(ref semaforo.full, processos);

                processo.Estado = "Terminated";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);

                if (upProcess != null)
                {
                    if (upProcess is Produtor)
                    {
                        ProdutorComunicacao(items, semaforo, upProcess, true);
                    }
                    else if (upProcess is Consumidor)
                    {
                        ConsumidorComunicacao(items, semaforo, upProcess, true);
                    }
                }
            }
        }

        public void ConsumidorComunicacao(List<int> items, Semaforo semaforo, Processo processo, bool isUp)
        {
            if (!(processo is Consumidor))
            {
                Console.WriteLine("Processo executado de maneira incorreta.");
                return;
            }

            while (true)
            {
                semaforo.Down(ref semaforo.full, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                semaforo.Down(ref semaforo.mutex, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                processo.Acao(items);

                Processo? upProcess = null;
                if (!isUp)
                {
                    upProcess = semaforo.Up(ref semaforo.mutex, processos);
                }

                semaforo.Up(ref semaforo.empty, processos);

                processo.Estado = "Terminated";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);

                if (upProcess != null)
                {
                    if (upProcess is Produtor)
                    {
                        ProdutorComunicacao(items, semaforo, upProcess, true);
                    }
                    else if (upProcess is Consumidor)
                    {
                        ConsumidorComunicacao(items, semaforo, upProcess, true);
                    }
                }
            }
        }

        static public Processo? GetRandomWaitingProcess(List<Processo> processos)
        {
            List<int> indexes = new List<int>();

            int index = 0;
            foreach (Processo processo in processos)
            {
                if (processo.Estado == "Ready")
                {
                    indexes.Add(index);
                }
                index++;
            }

            if (indexes.Count == 0)
            {
                return null;
            }

            Random random = new Random();
            int randomIndex = indexes[random.Next(indexes.Count())];

            return processos[randomIndex];
        }
    }
}
