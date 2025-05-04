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
        // Se não, processo volta pro final da lista "Ready"
        // Se sim, processo executa primeira parte
        // Processo é salvo
        // Volta pro final da lista "Running"
        // Verifica se há processos "Running"
        // Se sim, executa

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
        protected List<Processo> listaReady = new List<Processo>();
        protected List<Processo> listaRunning = new List<Processo>();
        protected List<Processo> listaWaiting = new List<Processo>();
        protected Processo processoAtual;
        protected string tipoEscalonador;

        abstract public void Escalonar(List<Processo> listaProcessos);

        public void AlocarProximoProcesso()
        {
            if (listaReady.Count <= 0)
            {
                ExecutarProximoProcesso();
                return;
            }

            processoAtual = listaReady[0];
            if (Kernel.AlocarProcesso(processoAtual))
            {
                listaReady.Remove(processoAtual);
                listaRunning.Add(processoAtual);
                processoAtual.Estado = "Running";

                Console.WriteLine(processoAtual.ProcID + ": Alocando - " + processoAtual.Estado);
                AlocarProximoProcesso();
            }
            else
            {
                // Processo vai para o final da lista do escalonador
                listaReady.Remove(processoAtual);
                listaReady.Add(processoAtual);
                ExecutarProximoProcesso();
            }
        }

        public void ExecutarProximoProcesso()
        {
            if (listaRunning.Count <= 0)
            {
                if (listaReady.Count > 0)
                {
                    AlocarProximoProcesso();
                }
                return;
            }

            processoAtual = listaRunning[0];
            listaRunning.Remove(processoAtual);

            int resultado = Kernel.ExecutarProcesso(processoAtual, this);
            if (resultado == 1)
            {
                listaRunning.Add(processoAtual);
            }
            else if (resultado == -1)
            {
                listaReady.Add(processoAtual);
                //listaWaiting.Add(processoAtual);
            }
            AlocarProximoProcesso();
        }

        public void InterromperProcesso()
        {
            listaReady.Add(processoAtual);
            processoAtual.Estado = "Ready";
            Console.WriteLine(processoAtual.ProcID + ": " + processoAtual.Estado);

            AlocarProximoProcesso();
        }

        public void AguardarProcesso()
        {
            listaWaiting.Add(processoAtual);
        }

        public void TerminarProcesso()
        {
            Kernel.TerminarProcesso(processoAtual, this);

            processoAtual.Estado = "Terminated";
            Console.WriteLine(processoAtual.ProcID + ": " + processoAtual.Estado);

            AlocarProximoProcesso();
        }

        public void CalcularTempo(List<Processo> listaProcessos)
        {
            float tempoEsperaTotal = 0.0f;
            float tempoProcessamentoTotal = 0.0f;
            //float tempoExecucaoTotal = 0.0f;

            // Futuramente:
            // Buscar processos com o mesmo tipo de escalonador
            // e realizar os calculos sem a necessidade de uma lista
            foreach (Processo processo in listaProcessos)
            {
                tempoEsperaTotal += tempoProcessamentoTotal;
                tempoProcessamentoTotal += processo.TempoExecucao;

                Resultado resultado = new Resultado(tipoEscalonador, processo.ProcID, tempoProcessamentoTotal, tempoEsperaTotal);

                Historico.adicionarResultado(resultado);
                //Console.WriteLine("Tempo de Espera: " + tempoEsperaTotal);
                //Console.WriteLine("Tempo de Processamento: " + processo.TempoExecucao);
            }
            //tempoExecucaoTotal = tempoEsperaTotal + tempoProcessamentoTotal;
            //Console.WriteLine("Tempo de Execucao Total: " + tempoProcessamentoTotal);
        }

        public void CalcularTempoEscalonador()
        {
            List<Resultado> resultados = Historico.buscaPorEscalonador(tipoEscalonador);

            if (resultados.Count == 0)
            {
                return;
            }

            int contagem = resultados.Count();
            float tempoEsperaMaximo = 0;
            float tempoEsperaMinimo = resultados[contagem - 1].TempoEspera;
            float tempoExecucaoMaximo = 0;

            foreach (Resultado res in resultados)
            {
                float tempoEspera = res.TempoEspera;
                if (tempoEspera < tempoEsperaMinimo && tempoEspera != 0)
                {
                    tempoEsperaMinimo = tempoEspera;
                }

                if (tempoEspera > tempoEsperaMaximo)
                {
                    tempoEsperaMaximo = tempoEspera;
                }

                if (res.TempoExecucao > tempoExecucaoMaximo)
                {
                    tempoExecucaoMaximo = res.TempoExecucao;
                }
            }
            
            float tempoMedio = tempoEsperaMaximo / contagem;
            float vazao = contagem / tempoExecucaoMaximo;

            Console.WriteLine("Resultados do Escalonador " + tipoEscalonador);
            Console.WriteLine("Tempo Minimo de Espera: " + tempoEsperaMinimo);
            Console.WriteLine("Tempo Maximo de Espera: " + tempoEsperaMaximo);
            Console.WriteLine("Tempo Medio de Espera: " + tempoMedio);
            Console.WriteLine("Vazao: " + vazao);
        }

        public void Clonar(List<Processo> listaProcessos)
        {
            foreach (Processo p in listaProcessos)
            {
                Processo temp = new EmLote(p.ProcID, p.TempoExecucao, p.MemoriaAlocada, p.Acoes);
                listaReady.Add(temp);
            }
        }
    }

}
