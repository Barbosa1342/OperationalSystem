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
        private int tamanhoPagina; // Define o tamanho de cada página (32 posições de memória)
        //private int numeroPaginasLivre; // Calcula o número total de páginas disponíveis na memória

        //private int memorialTotalLivre;
        private List<Pagina> paginas;

        public MemoriaVirtual(int numeroPagina, int tamanhoPagina)
        {
            this.tamanhoPagina = tamanhoPagina;

            this.paginas = new List<Pagina>();
            
            for (int i = 0; i < numeroPagina; i++)
            {
                paginas.Add(new Pagina(i, tamanhoPagina, i * tamanhoPagina));
            }

            Console.WriteLine($"Memória Virtual inicializada com {numeroPagina} páginas de {tamanhoPagina} endereços cada.");
        }

        // Método para encontrar uma página livre na memória
        private List<Pagina> ObterPaginaLivre(int paginasNecessarias)
        {
            List<Pagina> paginasLivre = new List<Pagina>();

            foreach (var pagina in paginas)
            {
                if (!pagina.EmUso) // Verifica se a página está livre
                {
                    paginasLivre.Add(pagina);// armazena pagina livre em vetor de paginas
                    Console.WriteLine("pagina livre encontrada");//apenas teste de função

                    // reduz o numero de iteracoes
                    if (paginasLivre.Count >= paginasNecessarias)
                    {
                        break;
                    }
                }
            }
            return paginasLivre; // Retorna o numero de páginas livres
        }

        public bool AlocarProcesso(Processo processo)
        {
            int tamanho = processo.MemoriaAlocada;
            int paginasNecessarias = (int)Math.Ceiling(tamanho / (double)tamanhoPagina);
            Console.WriteLine($"Processo {processo.ProcID} requer {paginasNecessarias} páginas ({tamanho} posições de memória).");

            List<Pagina> paginasLivre = ObterPaginaLivre(paginasNecessarias);
            if (paginasNecessarias <= paginasLivre.Count)
            {
                for (int i = 0; i < paginasNecessarias; i++)
                {
                    paginasLivre[i].EmUso = true; // marca a página como em uso
                    paginasLivre[i].ProcID = processo.ProcID; // associa o processo atual à página.
                    paginasLivre[i].Referenciada = true; // marca a página como referenciada (foi recentemente acessada).
                }
                Console.WriteLine("Tamanho apropriado encontrado para processo.");
            }
            else
            {
                return false;
            }

            return true;
        }

        public List<int> BuscaProcesso(int procID)
        {
            List<int> paginasUsadas = new List<int>();
            foreach (var pag in paginas)
            {
                if (pag.ProcID == procID && pag.EmUso)
                {
                    paginasUsadas.Add(pag.Numero);
                }
            }

            return paginasUsadas;
        }

        public void DesalocarProcesso(int procID) // Libera espaço na memoria após finalização de processo.
        {
            foreach (var pagina in paginas)
            {
                if (pagina.ProcID == procID) // Compara ID do processo com o salvo na memoria. 
                {
                    pagina.EmUso = false; // Marca a página como em uso.
                    Console.WriteLine("Pagina Virtual " + pagina.Numero + " desalocada para o processo " + procID);
                }
            }
        }

        public void ExibirMemoria()
        {
            Console.WriteLine("Memoria Virtual: ");
            foreach (Pagina pag in paginas)
            {
                pag.MostrarEnderecos();
            }
        }
    }
}
