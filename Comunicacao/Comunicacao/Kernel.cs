namespace SistemasOperacionais
{
    static class Kernel
    {
        // recursos serao classees separadas
        // para fins de teste esta assim
        static float cpu = 300;
        static float memoria = 300;

        // espaco de armazenamento compartilhado
        static List<int> items = new();

        static List<Processo> processos = new List<Processo>();

        static public void Escalonar(Escalonador escalonador)
        {
            escalonador.Escalonar(processos);
        }

        static public void PopulateProcessQueue(Processo processo)
        {
            processos.Add(processo);
        }

        static public bool AlocarProcesso(Processo processo)
        {
            if (ChecarCPU(processo, cpu) && ChecarMemoria(processo, memoria))
            {
                AlocarCPU(processo, cpu);
                AlocarMemoria(processo, memoria);

                return true;
            }

            return false;
        }

        static public int ExecutarProcesso(Processo processo, Escalonador escalonador)
        {
            
            Console.WriteLine();
            Console.WriteLine(processo.ProcID + ": Executando - " + processo.Indice);
            if (processo.Acao(items) == 0)
            {
                escalonador.TerminarProcesso();
                return 0;
            }
            else
            {
                Console.WriteLine(processo.ProcID + ": Salvando - " + processo.Indice);
                // Atualiza o estado e salva
                processo.Indice += 1;
                return 1;
            }        
        }

        static public void TerminarProcesso(Processo processo, Escalonador escalonador)
        {
            DesalocarCPU(processo, cpu);
            DesalocarMemoria(processo, memoria);
        }

        static public bool ChecarMemoria(Processo processo, float memoria)
        {
            if (processo.MemoriaAlocada < memoria)
            {
                return true;
            }
            return false;
        }

        static public bool ChecarCPU(Processo processo, float cpu)
        {
            if (processo.TempoExecucao < cpu)
            {
                return true;
            }
            return false;
        }

        static public void AlocarMemoria(Processo processo, float memoria)
        {
            memoria -= processo.MemoriaAlocada;
        }

        static public void AlocarCPU(Processo processo, float cpu)
        {
            cpu -= processo.TempoExecucao;
        }

        static public void DesalocarMemoria(Processo processo, float memoria)
        {
            memoria += processo.MemoriaAlocada;
        }

        static public void DesalocarCPU(Processo processo, float cpu)
        {
            cpu += processo.TempoExecucao;
        }

        static public void ProdutorComunicacao(List<int> items, Processo processo, bool isUp)
        {
            if (!(processo is Produtor))
            {
                Console.WriteLine("Processo executado de maneira incorreta.");
                return;
            }

            while (true)
            {
                Semaforo.Down(ref Semaforo.mutex, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                Semaforo.Down(ref Semaforo.empty, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                processo.Acao(items);

                Processo? upProcess = null;
                if (!isUp)
                {
                    upProcess = Semaforo.Up(ref Semaforo.mutex, processos);
                }
             
                Semaforo.Up(ref Semaforo.full, processos);

                processo.Estado = "Terminated";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);

                if (upProcess != null)
                {
                    if (upProcess is Produtor)
                    {
                        ProdutorComunicacao(items, upProcess, true);
                    }
                    else if (upProcess is Consumidor)
                    {
                        ConsumidorComunicacao(items, upProcess, true);
                    }
                }
            }
        }

        static public void ConsumidorComunicacao(List<int> items, Processo processo, bool isUp)
        {
            if (!(processo is Consumidor))
            {
                Console.WriteLine("Processo executado de maneira incorreta.");
                return;
            }

            while (true)
            {
                Semaforo.Down(ref Semaforo.full, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                Semaforo.Down(ref Semaforo.mutex, processo);

                if (processo.Estado == "Ready")
                {
                    return;
                }

                processo.Acao(items);

                Processo? upProcess = null;
                if (!isUp)
                {
                    upProcess = Semaforo.Up(ref Semaforo.mutex, processos);
                }

                Semaforo.Up(ref Semaforo.empty, processos);

                processo.Estado = "Terminated";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);

                if (upProcess != null)
                {
                    if (upProcess is Produtor)
                    {
                        ProdutorComunicacao(items, upProcess, true);
                    }
                    else if (upProcess is Consumidor)
                    {
                        ConsumidorComunicacao(items, upProcess, true);
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
