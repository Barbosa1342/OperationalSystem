using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace Comunicacao
{
    public abstract class Recurso
    {
        public double MemoriaTotal { get; protected set; } //Define Total de Memoria ram
        public double CPUTotal { get; protected set; } //Define Total de CPU
        public bool Disponibilidade { get; protected set; } //Consulta de há disponibilidade de recurso e me retorna se verdadeiro ou falso

        protected Dictionary<int, string> PosicoesMemoria;  // Mapeamento da posição para endereço

        public Recurso(double memoriaTotal, double cpuTotal)
        {
            MemoriaTotal = memoriaTotal;
            CPUTotal = cpuTotal;
            Disponibilidade = true;
            PosicoesMemoria = new Dictionary<int, string>();
        }

        public abstract void Alocation(int quantidade);

        public bool ConsultarDisponibilidade(int quantidade)
        {
            return MemoriaTotal >= quantidade;
        }

        public void DefineMemoriaTotal(int quantidade)
        {
            MemoriaTotal = quantidade;
            ConsultaPosicoes(quantidade);
            Console.WriteLine($"Memória total definida para {quantidade} posições.");
        }

        public void ConsultaPosicoes(int quantidade)
        {
            PosicoesMemoria.Clear();
            for (int i = 0; i < quantidade; i++)
            {
                string endereco = $"0x{i.ToString("X4")}";
                PosicoesMemoria.Add(i, endereco);
            }
            Console.WriteLine($"Memória inicializada com {quantidade} posições.");
        }

        public void DefineCPU(int quantidade)
        {
            Random rand = new Random();
            CPUTotal = rand.Next(1, quantidade + 1);
            Console.WriteLine($"CPU inicializada com {CPUTotal}% de uso.");
        }
    }
}
