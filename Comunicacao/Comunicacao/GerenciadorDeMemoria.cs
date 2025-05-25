using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que gerencia a memória utilizando paginação e algoritmo de substituição de páginas (Clock)
    public class GerenciadorDeMemoria
    {
        protected int TamanhoPagina = 32; // Define o tamanho de cada página (32 posições de memória)
        protected int MemoriaTotal; // Armazena a quantidade total de memória disponível
        protected int NumeroPaginas; // Calcula o número total de páginas disponíveis na memória
        protected List<Pagina> Paginas; // Lista de todas as páginas disponíveis na memória
        protected Queue<Pagina> Relogio; // Fila de páginas usada pelo algoritmo de Clock

        // Construtor da classe que inicializa as páginas e o algoritmo de Clock
        public GerenciadorDeMemoria(int memoriaTotal)
        {
            MemoriaTotal = memoriaTotal; // Define a memória total do sistema
            NumeroPaginas = memoriaTotal / TamanhoPagina; // Calcula o número de páginas possíveis na memória
            Paginas = new List<Pagina>(); // Inicializa a lista de páginas vazia
            Relogio = new Queue<Pagina>(); // Inicializa a fila de páginas para o algoritmo de Clock

            // Cria cada página e a adiciona à lista
            for (int i = 0; i < NumeroPaginas; i++)
            {
                Paginas.Add(new Pagina(i, TamanhoPagina, i * TamanhoPagina)); // Cria páginas com endereços inicializados
            }

            Console.WriteLine($"Memória inicializada com {NumeroPaginas} páginas de {TamanhoPagina} endereços cada.");
        }

        // Método para alocar um processo na memória
        public void AlocarProcesso(Processo processo, int tamanho)
        {
            int paginasNecessarias = (int)Math.Ceiling(tamanho / (double)TamanhoPagina); // Calcula quantas páginas são necessárias para o processo
            Console.WriteLine($"Processo {processo.ProcID} requer {paginasNecessarias} páginas ({tamanho} posições de memória).");

            for (int i = 0; i < paginasNecessarias; i++)
            {
                Pagina pagina = ObterPaginaLivre(processo); // Procura uma página livre para o processo
                if (pagina != null)
                {
                    Relogio.Enqueue(pagina); // Adiciona a página na fila do relógio
                    Console.WriteLine($"Página {pagina.Numero} alocada para o processo {processo.ProcID}.");
                }
                else
                {
                    SubstituirPagina(processo); // Se não encontrar páginas livres, aplica o algoritmo de substituição
                }
            }
        }

        // Método para encontrar uma página livre na memória
        private Pagina ObterPaginaLivre(Processo processo)
        {
            foreach (var pagina in Paginas)
            {
                if (!pagina.EmUso) // Verifica se a página está livre
                {
                    pagina.EmUso = true; // Marca a página como em uso
                    pagina.ProcID = processo.ProcID; // Associa o processo atual à página
                    pagina.Referenciada = true; // Marca a página como referenciada (foi recentemente acessada)
                    return pagina; // Retorna a página encontrada
                }
            }
            return null; // Retorna nulo se não encontrar páginas livres
        }

        // Método que substitui uma página usando o algoritmo de Clock
        private void SubstituirPagina(Processo processo)
        {
            while (true)
            {
                Pagina paginaAtual = Relogio.Dequeue(); // Remove a primeira página da fila do relógio

                if (!paginaAtual.Referenciada) // Se a página não foi acessada recentemente
                {
                    Console.WriteLine($"Página {paginaAtual.Numero} substituída pelo processo {processo.ProcID}.");
                    paginaAtual.ProcID = processo.ProcID; // Atribui o processo atual à página substituída
                    paginaAtual.Referenciada = true; // Marca a página como referenciada novamente
                    paginaAtual.InicializarEnderecos(processo.ProcID); // Recria os endereços da página para o novo processo
                    Relogio.Enqueue(paginaAtual); // Reinsere a página na fila do relógio
                    return; // Finaliza o processo de substituição
                }
                else
                {
                    paginaAtual.Referenciada = false; // Reseta o bit de referência para falso
                    Relogio.Enqueue(paginaAtual); // Reinsere a página no final da fila do relógio
                }
            }
        }

        // Método que exibe todas as páginas e seus endereços na memória
        public void MostrarPaginas()
        {
            Console.WriteLine("\n--- Estado da Memória ---");
            foreach (var pagina in Paginas)
            {
                pagina.MostrarEnderecos(); // Exibe cada página individualmente
            }
            Console.WriteLine("-------------------------\n");
        }
    }
}
