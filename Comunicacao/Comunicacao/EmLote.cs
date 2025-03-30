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
        }

        public override void Acao(List<int> items)
        {
            items.Add(0);

            Console.WriteLine("\nExecucao: ");
            for (int i = 0; i < tempoExecucao; i++)
            {
                Console.WriteLine(i + 1);
            }
            Console.WriteLine("");
        }
    }
}
