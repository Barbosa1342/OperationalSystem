using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que gerencia a memória utilizando paginação e algoritmo de substituição de páginas (Clock)
    static public class GerenciadorDeMemoria
    {
        static private MemoriaVirtual memoriaVirtual;
        static private MemoriaFisica memoriaFisica;
        static private Dictionary<int, int> tabelaPaginacao; //num pagina fisica, num pagina virtual
        static private Dictionary<int, int> relogio; //num pagina fisica, wsclock
        static private int wsclock; // contagem

        static GerenciadorDeMemoria()
        {
            int numeroPagina = 32;
            int tamanhoPagina = 32;

            wsclock = 1;
            memoriaFisica = new MemoriaFisica(numeroPagina, tamanhoPagina);

            int numeroPaginaVirtual = numeroPagina * 2;
            memoriaVirtual = new MemoriaVirtual(numeroPaginaVirtual, tamanhoPagina);

            tabelaPaginacao = new Dictionary<int, int>();
            relogio = new Dictionary<int, int>();

            InicializarPaginacao(numeroPagina);

            for (int i = 0; i < numeroPagina; i++)
            {
                AtualizaRelogio(i);
            }
        }

        static private void InicializarPaginacao(int numeroPagina)
        {
            for (int i = 0; i < numeroPagina; i++)
            {
                // inicia a paginacao, com indice -1 para representar que ta livre
                tabelaPaginacao.Add(i, -1);
            }
        }

        static private void AtualizaRelogio(int numFisico)
        {
            if (!relogio.TryAdd(numFisico, wsclock))
            {
                relogio[numFisico] = wsclock;
            }
            wsclock++;
        }

        static public void AssociarMemoria(int numVirtual, int numFisico)
        {
            // Nao verifica pagina livre
            // A verificacao deve ser feita antes
            if (!tabelaPaginacao.TryAdd(numFisico, numVirtual))
            {
                tabelaPaginacao[numFisico] = numVirtual;
            }
            AtualizaRelogio(numFisico);

            Console.WriteLine($"Página {numVirtual} associada à memória física {numFisico}.");
        }

        static public void MostrarPaginacao()
        {
            string fisico = "Pagina Fisica";
            string mVirtual = "Pagina Virtual";
            Console.WriteLine($"{fisico, -20}{mVirtual, -20}");

            foreach (var pag in tabelaPaginacao)
            {
                Console.WriteLine($"{pag.Key,-20}{pag.Value,-20}");
            }
        }

        static public void ExibirPaginas()
        {
            memoriaVirtual.ExibirMemoria();
            memoriaFisica.ExibirMemoria();
        }

        static public int WSClock()
        {
            int menorClock = relogio[0];
            int numFisico = 0;

            foreach (var pag in relogio)
            {
                if (pag.Value < menorClock)
                {
                    menorClock = pag.Value;
                    numFisico = pag.Key;
                }
            }

            if (!memoriaFisica.PaginaReferenciada(numFisico))
            {
                return numFisico;
            }

            return WSClock();
        }

        static public void SubstituirPagina(int numVirtual, int procID)
        {
            int numFisico = WSClock();

            // caso haja uma pagina livre
            // o numFisico vai ser a pagina livre
            numFisico = memoriaFisica.AlocarProcesso(procID, numFisico);

            AssociarMemoria(numVirtual, numFisico);
        }

        /*
        Processo Alocado (Ready) -> Memoria Virtual
        Processo Aguardando (Waiting) -> Memoria Virtual
        Processo Executando (Running) -> Memoria Fisica
        Processo Novo (New) -> Disco
        Processo Terminando (Terminated) -> Desalocar das duas memorias (tirar o EmUso)
         */

        static public bool AlocarProcesso(Processo processo)
        {
            int procID = processo.ProcID;

            if (processo.Estado == "New")
            {
                // Processo New indo para Ready
                return memoriaVirtual.AlocarProcesso(processo);
            }

            if (processo.Estado == "Ready")
            {
                // Processo Ready indo para Running
                foreach (int numPag in memoriaVirtual.BuscaProcesso(procID))
                {
                    SubstituirPagina(numPag, procID);
                }
                
                return true;
            }
            return false;
        }

        static public bool DesalocarMemoriaFisica(Processo processo)
        {        
            memoriaFisica.DesalocarProcesso(processo.ProcID);

            return true;
        }

        static public bool DesalocarMemoria(Processo processo)
        {
            memoriaFisica.DesalocarProcesso(processo.ProcID);
            memoriaVirtual.DesalocarProcesso(processo.ProcID);

            return true;
        }
    }
}
