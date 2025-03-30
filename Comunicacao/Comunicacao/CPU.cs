namespace Comunicacao
{
    public class CPU : Recurso
    {
        private double Ocupacao;

        public CPU(int cpuTotal) : base(cpuTotal)
        {
            Ocupacao = 0;
        }

        public override void Alocacao(int quantidade)
        {
            Random rand = new Random();
            Ocupacao = rand.NextDouble() * 100; // Randomiza a ocupação da CPU entre 0% e 100%

            if (ConsultarDisponibilidade(quantidade))
            {
                Console.WriteLine($"CPU alocada com uso de {Ocupacao:F2}%.");
            }
            else
            {
                Console.WriteLine("Falha na alocação da CPU. Uso excedente.");
            }
        }

        public void DefineCPU(int quantidade)
        {
            Random rand = new Random();
            recursoTotal = rand.Next(1, quantidade + 1);
            Console.WriteLine($"CPU inicializada com {recursoTotal}% de uso.");
        }
    }
}

