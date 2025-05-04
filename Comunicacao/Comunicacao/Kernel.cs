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

            int resultado = processo.Acao(items);
            if (resultado == 0)
            {
                escalonador.TerminarProcesso();
                return 0;
            }else if(resultado == -1)
            {
                Console.WriteLine(processo.ProcID + ": Salvando - " + processo.Indice);
                // Remover da lista de execução e retornar a lista ready
                return -1;
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
    }
}
