using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace Comunicacao
{
    public class Memoria : Recurso
    {
        public int PosicoesOcupadas { get; private set; }

        public Memoria(double memoriaTotal) : base(memoriaTotal, 0)
        {
            PosicoesOcupadas = 0;
        }

        public override void Alocation(int quantidade)
        {
            if (ConsultarDisponibilidade(quantidade))
            {
                PosicoesOcupadas += quantidade;
                MemoriaTotal -= quantidade;
                Disponibilidade = MemoriaTotal > 0;

                Console.WriteLine($"Memória alocada: {quantidade} posições ocupadas.");
            }
            else
            {
                Console.WriteLine("Falha na alocação: Memória insuficiente.");
            }
        }

        public void MostrarMapaMemoria()
        {
            foreach (var pos in PosicoesMemoria)
            {
                Console.WriteLine($"Posição: {pos.Key}, Endereço: {pos.Value}");
            }
        }
    }
}
