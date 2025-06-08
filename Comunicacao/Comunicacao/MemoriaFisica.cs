using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que representa a memória física
    public class MemoriaFisica
    {
        private int tamanhoPagina; // Define o tamanho de cada página (32 posições de memória)
        //private int numeroPaginasLivre; // Calcula o número total de páginas disponíveis na memória

        //private int memorialTotalLivre;
        private List<Pagina> paginas;

        public MemoriaFisica(int numeroPagina, int tamanhoPagina)
        {
            this.tamanhoPagina = tamanhoPagina;

            this.paginas = new List<Pagina>();

            for (int i = 0; i < numeroPagina; i++)
            {
                paginas.Add(new Pagina(i, tamanhoPagina, i * tamanhoPagina));
            }

            Console.WriteLine($"Memória Fisica inicializada com {numeroPagina} páginas de {tamanhoPagina} endereços cada.");
        }

        public bool PaginaReferenciada(int numeroPagina)
        {
            bool temp = paginas[numeroPagina].Referenciada;

            if (temp)
            {
                paginas[numeroPagina].Referenciada = false;
            }

            return temp;
        }

        private Pagina? ObterPaginaLivre()
        {
            foreach (var pagina in paginas)
            {
                if (!pagina.EmUso) // Verifica se a página está livre
                {
                    return pagina;
                }
            }
            return null; // Retorna o numero de páginas livres
        }

        public int AlocarProcesso(int procID, int numPagina)
        {
            Pagina? paginaLivre = ObterPaginaLivre();
            if (paginaLivre != null)
            {
                paginaLivre.EmUso = true; // marca a página como em uso
                paginaLivre.ProcID = procID; // associa o processo atual à página.
                paginaLivre.Referenciada = true; // marca a página como referenciada (foi recentemente acessada).
                return paginaLivre.Numero;
            }
            else
            {
                paginas[numPagina].EmUso = true;
                paginas[numPagina].ProcID = procID;
                paginas[numPagina].Referenciada = true;
                return numPagina;
            }
        }

        public void DesalocarProcesso(int procID) // Libera espaço na memoria após finalização de processo.
        {
            foreach (var pagina in paginas)
            {
                if (pagina.ProcID == procID && pagina.EmUso) // Compara ID do processo com o salvo na memoria. 
                {
                    pagina.EmUso = false; // Marca a página como em uso.
                    Console.WriteLine("Pagina Fisica " + pagina.Numero + " desalocada para o processo " + procID);
                }
            }
        }
        public void ExibirMemoria()
        {
            Console.WriteLine("Memoria Fisica: ");
            foreach (Pagina pag in paginas)
            {
                pag.MostrarEnderecos();
            }
        }
    }
        
}