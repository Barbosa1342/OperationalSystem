using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    public class Pagina
    {
        public int Numero { get; private set; }
        public int ProcID { get; set; } // Associado ao processo que usa a página
        public bool EmUso { get; set; }
        public bool Referenciada { get; set; }
        private Dictionary<int, string> Enderecos;

        public Pagina(int numero, int tamanhoPagina, int enderecoInicial)
        {
            Numero = numero;
            ProcID = -1;
            EmUso = false;
            Referenciada = false;
            Enderecos = new Dictionary<int, string>();

            InicializarEnderecos(enderecoInicial);
        }

        public void InicializarEnderecos(int procID)
        {
            Enderecos.Clear();
            for (int i = 0; i < 32; i++)
            {
                string endereco = $"0x{(procID * 1000 + i).ToString("X4")}";
                Enderecos.Add(i, endereco);
            }
        }

        public void MostrarEnderecos()
        {
            Console.WriteLine($"\nPágina {Numero} - Processo: {(ProcID == -1 ? "Nenhum" : ProcID.ToString())} - Em Uso: {EmUso}");
            Console.WriteLine("Endereços: ");
            foreach (var endereco in Enderecos)
            {
                Console.WriteLine($"Posição {endereco.Key}: {endereco.Value} (Processo ID: {ProcID})");
            }
        }
    }
}