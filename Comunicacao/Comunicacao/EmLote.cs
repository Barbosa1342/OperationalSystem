using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    internal class EmLote : Processo
    {
        int indiceInicial;
        bool emSemaforo = false;
        int dados;

        public EmLote(int procID) : base(procID)
        {
            Random random = new Random();
            tempoExecucao = random.Next(5, 21);
            memoriaAlocada = random.Next(5, 21);

            for (int i = 0; i < 10; i++)
            {
                acoes.Add(i);
            }
        }
        public EmLote(int procID, float tempoExecucao, int memoriaAlocada, List<int> acoes) : base(procID, tempoExecucao, memoriaAlocada, acoes)
        {
        }

        public override int Acao(out int? saida)
        {
            saida = null;

            if (indice < acoes.Count())
            {
                int retorno = 1;

                if (indice >= 0 && indice <= 4)
                {
                    // o acesso ao semaforo pode ocorrer na entrada n, porem o registro dentro e 0 a 4
                    if (!emSemaforo)
                    {
                        indiceInicial = indice;
                        emSemaforo = true;
                    }

                    retorno = Semaforo.RecebeDados((indice - indiceInicial), this, out saida);                    
                }
                if (indice >= 5 && indice <= 9)
                {
                    if (!emSemaforo)
                    {
                        indiceInicial = indice;
                        emSemaforo = true;
                    }

                    retorno = Semaforo.InserirDados((indice - indiceInicial), this, dados, out saida);
                }

                
                
                if (retorno == 0)
                {
                    emSemaforo = false;
                }
                else if (retorno == -1)
                {
                    return -1;
                }
                else if (retorno == 1)
                {
                    if (saida != null)
                    {
                        // dados
                        // pode ser adicionar a um arquivo                        
                        dados = (int)saida;
                        saida = null;
                        // o kernel nao precisa desse dado

                        //Console.WriteLine("Peguei dado: " + dados);
                    }
                }
                // se retorno == 2 ou == 0
                // e saida != nulo
                // entao saida == id

                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
