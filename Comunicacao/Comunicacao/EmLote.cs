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

        public EmLote(int procID) : base(procID)
        {
            Random random = new Random();
            tempoExecucao = random.Next(5, 21);
            memoriaAlocada = random.Next(5, 21);

            for (int i = 0; i < tempoExecucao; i++)
            {
                acoes.Add(i);
            }
        }
        public EmLote(int procID, float tempoExecucao, int memoriaAlocada, List<int> acoes) : base(procID, tempoExecucao, memoriaAlocada, acoes)
        {
        }

        public override int Acao(List<int> items)
        {
            int? teste;

            if (indice < acoes.Count())
            {
                if (!emSemaforo)
                {
                    indiceInicial = indice;
                    emSemaforo = true;
                }
                return Semaforo.RecebeDados(indice - indiceInicial, this, out teste);
            }
            else
            {
                return 0;
            }
        }

        public override int Acao()
        {
            throw new NotImplementedException();
        }
    }
}
