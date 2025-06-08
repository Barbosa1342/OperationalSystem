using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    public class Pagina
    {
        private int tamanho; // cada pagina vai ter 32 linhas
        private int numero;  // numero da pagina
        private int procID; // dado do processo que esta usando a pagina
        private bool emUso;
        private bool referenciada; // usado no WClock
        private Dictionary<int, string> enderecos;

        public Pagina(int numero, int tamanhoPagina, int enderecoInicial)
        {
            this.tamanho = tamanhoPagina;
            this.numero = numero;
            this.procID = -1; // comeca sem processo
            this.emUso = false;
            this.referenciada = false;
            this.enderecos = new Dictionary<int, string>();

            InicializarEnderecos(enderecoInicial);
        }

        public int Tamanho { get => tamanho; set => tamanho = value; }
        public int Numero { get => numero; set => numero = value; }
        public int ProcID { get => procID; set => procID = value; }
        public bool EmUso { get => emUso; set => emUso = value; }
        public bool Referenciada { get => referenciada; set => referenciada = value; }
        public Dictionary<int, string> Enderecos { get => enderecos; set => enderecos = value; }

        public void InicializarEnderecos(int enderecoInicial)
        {
            // inicializa os enderecos internos da pagina
            
            // objetivo:
            // 0x1000
            // 0x1032

            // 0x2000
            // 0x2032

            // 0x3000
            // 0x3032

            enderecos.Clear();
            for (int i = 0; i < tamanho; i++)
            {
                string endereco = $"0x{(enderecoInicial * 1000 + i).ToString("X4")}";
                enderecos.Add(i, endereco);
            }
        }

        public void MostrarEnderecos()
        {
            Console.WriteLine($"\nPágina {numero} - Processo: {(procID == -1 ? "Nenhum" : procID.ToString())} - Em Uso: {emUso}");
            Console.WriteLine("Endereços: ");

            foreach (var endereco in enderecos)
            {
                Console.WriteLine($"Posição {endereco.Key}: {endereco.Value} (Processo ID: {procID})");
            }
        }
    }
}