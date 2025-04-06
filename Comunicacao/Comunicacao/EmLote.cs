using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    internal class EmLote : Processo
    {
        public EmLote(int procID) : base(procID)
        {
            Random random = new Random();
            tempoExecucao = random.Next(5, 21);
            memoriaAlocada = random.Next(5, 21);

            Indice = 0;
        }

        public override int Acao(List<int> items)
        {
            if (Indice < acoes.Count)
            {
                items.Add(Indice);

                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
