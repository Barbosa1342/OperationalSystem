using System.Diagnostics;

namespace SistemasOperacionais
{
    abstract class Escalonador
    {
        // Processo é criado (Interação do Usuario)
        // Kernel Armazena na lista de processos
        // Escalonador ordena a lista
        // Pega o primeiro item da lista
        // e solicita tempo de processamento (CPU) para o kernel
        // Verificar se tem CPU livre
        // Se não, processo dorme
        // CPU pode solicitar dados
        // Processos com recursos executa
        // Recursos são devolvidos
        // Processo Terminated
        // Proximo Processo

        // Novo processo criado
        // Kernel Armazena na lista de processos
        // Vai para o Escalonador
        // Escalonador compara com Historico
        // se não há processo com ID e tipoEscalonador iguais
        // processo deve ser escalonado
        // lista reordenada
        protected List<Processo> listaOrdenada = new List<Processo>();
        protected Processo processoAtual;

        abstract public void Escalonar(List<Processo> listaProcessos);

        public void ExecutarProcesso()
        {
            if (listaOrdenada.Count <= 0)
            {
                return;
            }

            processoAtual = listaOrdenada[0];
            listaOrdenada.Remove(processoAtual);

            processoAtual.Estado = "Running";
            Console.WriteLine(processoAtual.ProcID + ": " + processoAtual.Estado);

            Kernel.ExecutarProcesso(processoAtual, this);
        }

        public void InterromperProcesso()
        {
            listaOrdenada.Add(processoAtual);
            processoAtual.Estado = "Ready";
            Console.WriteLine(processoAtual.ProcID + ": " + processoAtual.Estado);

            ExecutarProcesso();
        }

        public void TerminarProcesso()
        {
            Kernel.TerminarProcesso(processoAtual, this);

            processoAtual.Estado = "Terminated";
            Console.WriteLine(processoAtual.ProcID + ": " + processoAtual.Estado);

            ExecutarProcesso();
        }

        public void CalcularTempo()
        {
            float tempoEsperaTotal = 0.0f;
            float tempoProcessamentoTotal = 0.0f;
            //float tempoExecucaoTotal = 0.0f;

            foreach (Processo processo in listaOrdenada)
            {
                tempoEsperaTotal += tempoProcessamentoTotal;
                tempoProcessamentoTotal += processo.TempoExecucao;

                Console.WriteLine("Tempo de Espera: " + tempoEsperaTotal);
                Console.WriteLine("Tempo de Processamento: " + processo.TempoExecucao);
            }
            //tempoExecucaoTotal = tempoEsperaTotal + tempoProcessamentoTotal;
            Console.WriteLine("Tempo de Execucao Total: " + tempoProcessamentoTotal);

            // TO-DO: Implementar Resultado e Historico
            // para calcular media e vazao
        }
    }

}
