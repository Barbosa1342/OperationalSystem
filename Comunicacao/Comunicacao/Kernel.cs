namespace SistemasOperacionais
{
    static class Kernel
    {
        static List<Processo> processos = new List<Processo>();

        static public void Escalonar(Escalonador escalonador)
        {
            escalonador.Escalonar(processos);
        }

        static public void PopularFilaProcessos(Processo processo)
        {
            processos.Add(processo);
        }

        static public int ExecutarProcesso(Processo processo, Escalonador escalonador)
        {
            
            Console.WriteLine();
            Console.WriteLine(processo.ProcID + ": Executando - " + processo.Indice);

            int? saida;
            int resultado = processo.Acao(out saida);
            if (resultado == 0)
            {
                escalonador.TerminarProcesso();
                return 0;
            }else if(resultado == -1)
            {
                Console.WriteLine(processo.ProcID + ": Salvando - " + processo.Indice);
                // a acao nao foi finalizada
                // o indice nao deve ser atualizado

                // Remover da lista de execução e adicionado a lista waiting
                return -1;
            }
            else
            {
                if (saida != null)
                {
                    escalonador.AcordarProcesso((int)saida);
                }

                Console.WriteLine(processo.ProcID + ": Salvando - " + processo.Indice);
                // Atualiza o estado e salva
                processo.Indice += 1;
                return 1;
            }        
        }
        static public bool AlocarProcesso(Processo processo)
        {
            return GerenciadorDeMemoria.AlocarProcesso(processo);
        }
        static public bool DesalocarMemoria(Processo processo)
        {
            return GerenciadorDeMemoria.DesalocarMemoria(processo);
        }
        static public bool DesalocarMemoriaFisica(Processo processo)
        {
            return GerenciadorDeMemoria.DesalocarMemoriaFisica(processo);
        }
    }
}
