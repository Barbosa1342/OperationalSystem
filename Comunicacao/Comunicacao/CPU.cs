using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Comunicacao
{
    public class CPU : Recurso
    {
        public double Ocupacao { get; private set; }

        public CPU(double cpuTotal) : base(0, cpuTotal)
        {
            Ocupacao = 0;
        }

        public override void Alocation(int quantidade)
        {
            Random rand = new Random();
            Ocupacao = rand.NextDouble() * 100; // Randomiza a ocupação da CPU entre 0% e 100%

            if (Ocupacao <= quantidade)
            {
                Disponibilidade = true;
                Console.WriteLine($"CPU alocada com uso de {Ocupacao:F2}%.");
            }
            else
            {
                Disponibilidade = false;
                Console.WriteLine("Falha na alocação da CPU. Uso excedente.");
            }
        }
    }
}

