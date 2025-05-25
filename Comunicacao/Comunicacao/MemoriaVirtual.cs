using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    internal class MemoriaVirtual
    {
        private int TamanhoPagina = 32; // Define o tamanho de cada página (32 posições de memória)
        private int MemoriaTotal; // Armazena a quantidade total de memória disponível
        private int NumeroPaginas; // Calcula o número total de páginas disponíveis na memória
        private List<Pagina> Paginas; // Lista de todas as páginas disponíveis na memória
        public MemoriaVirtual(int memoriaTotal) {
            MemoriaTotal = memoriaTotal; // Define a memória total do sistema
            NumeroPaginas = memoriaTotal / TamanhoPagina; // Calcula o número de páginas possíveis na memória
            Paginas = new List<Pagina>(); // Inicializa a lista de páginas vazia

            // Cria cada página e a adiciona à lista
            for (int i = 0; i < NumeroPaginas; i++)
            {
                Paginas.Add(new Pagina(i, TamanhoPagina, i * TamanhoPagina)); // Cria páginas com endereços inicializados
            }

            Console.WriteLine($"Memória Virtual inicializada com {NumeroPaginas} páginas de {TamanhoPagina} endereços cada.");

        }

        // Método para encontrar uma página livre na memória
        private List<Pagina> ObterPaginaLivre()
        {
            List<Pagina> PaginasLivre = new List<Pagina>();

            foreach (var pagina in Paginas)
            {
                
                if (!pagina.EmUso) // Verifica se a página está livre
                {
                    PaginasLivre.Add(pagina);// armazena pagina livre em vetor de paginas
                    Console.WriteLine("pagina livre encontrada");//apenas teste de função
                }
            }
            return PaginasLivre; // Retorna o numero de páginas livres
        }

        public void AlocarProcesso(Processo processo, int tamanho)
        {
            int paginasNecessarias = (int)Math.Ceiling(tamanho / (double)TamanhoPagina); // Calcula quantas páginas são necessárias para o processo
            Console.WriteLine($"Processo {processo.ProcID} requer {paginasNecessarias} páginas ({tamanho} posições de memória).");

            List<Pagina>PaginasLivre = new List<Pagina>(); // Lista de paginas livres local
            PaginasLivre = ObterPaginaLivre(); // Salva retorno de lista de paginas livres
            if (paginasNecessarias <= PaginasLivre.Count) // Verifica se ha espaço na memoria Virtual
            {
                foreach (Pagina pagina in PaginasLivre) {
                    pagina.EmUso = true; // Marca a página como em uso
                    pagina.ProcID = processo.ProcID; // Associa o processo atual à página.
                    pagina.Referenciada = true; // Marca a página como referenciada (foi recentemente acessada).
                }
                Console.WriteLine("tamanho apropriado encontrado para processo."); // Teste de função.
            }
        }

        public void LiberarMemoria(Processo processo, List<Pagina>Paginas) // Libera espaço na memoria após finalização de processo.
        {
            foreach (var pagina in Paginas) {
                if (pagina.ProcID == processo.ProcID) // Compara ID do processo com o salvo na memoria. 
                {
                    pagina.EmUso = false; // Marca a página como em uso.
                }
            }
        }
    }
}
