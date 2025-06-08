using SistemasOperacionais;

namespace SistemasOperacionais
{
    class Program
    {
        static int ultimoId = 0;
        static void Main(string[] args)
        {
            SJF sjf = new SJF();
            FCFS fcfs = new FCFS();

            while (true)
            {
                int opcao = 0;
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine("Windows 9 - Interface Beta 0.1");
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                do
                {
                    Console.WriteLine("Escolha uma das opcoes: ");
                    Console.WriteLine("1 - Criar novo processo");
                    Console.WriteLine("2 - Escalonar SJF");
                    Console.WriteLine("3 - Escalonar FCFS");
                    Console.WriteLine("4 - Exibir tempos SJF");
                    Console.WriteLine("5 - Exibir tempos FCFS");
                    Console.WriteLine("6 - Exibir Tempos Escalonadores");
                    Console.WriteLine("7 - Exibir Historico Completo");
                    Console.WriteLine("8 - Exibir Paginacao Completo");
                    Console.WriteLine("9 - Exibir Memoria Completa");
                    Console.WriteLine("10 - Encerrar");
                    Console.Write("Escolha: ");

                    string? opcaoStr = Console.ReadLine();
                    if (!string.IsNullOrEmpty(opcaoStr))
                    {
                        bool sucesso = Int32.TryParse(opcaoStr, out opcao);
                    }
                    Console.Clear();
                } while (opcao < 1 || opcao > 10);

                if (opcao == 1)
                {
                    MenuCriacao();
                }
                else if (opcao == 2)
                {
                    Kernel.Escalonar(sjf);
                }
                else if (opcao == 3)
                {
                    Kernel.Escalonar(fcfs);
                }
                else if (opcao == 4)
                {
                    Historico.Imprimir("Shortest Job First");
                    sjf.CalcularTempoEscalonador();
                }
                else if (opcao == 5)
                {
                    Historico.Imprimir("First Come First Served");
                    fcfs.CalcularTempoEscalonador();
                }
                else if (opcao == 6)
                {
                    Historico.Imprimir("Shortest Job First");
                    sjf.CalcularTempoEscalonador();

                    Console.WriteLine();

                    Historico.Imprimir("First Come First Served");
                    fcfs.CalcularTempoEscalonador();
                }
                else if (opcao == 7)
                {
                    Historico.Imprimir();
                }
                else if (opcao == 8)
                {
                    GerenciadorDeMemoria.MostrarPaginacao();
                }
                else if (opcao == 9)
                {
                    GerenciadorDeMemoria.ExibirPaginas();
                }
                else if (opcao == 10)
                {
                    break;
                }

                if (opcao != 1)
                {
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static private void MenuCriacao()
        {
            EmLote p1 = new EmLote(ultimoId);
            ultimoId++;

            int opcao = 0;

            do
            {
                Console.WriteLine("1 - Criar novo processo");
                Console.WriteLine("2 - Sair");
                Console.Write("Escolha: ");

                string? opcaoStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(opcaoStr))
                {
                    bool sucesso = Int32.TryParse(opcaoStr, out opcao);
                }
                Console.Clear();
            } while (opcao < 1 || opcao > 2);

            if (opcao == 1)
            {
                MenuCriacao();
            }
        }

    }
}

/*
    // Inicializa o gerenciador de arquivos com 8 blocos
    var gerenciador = new GerenciadorArquivo(8);

    Console.WriteLine("== Teste de Fragmentação do Sistema de Arquivos ==");

    // 1. Cria arquivos pequenos
    gerenciador.CriarArquivo("a1", "12345678"); // ocupa 1 bloco
    gerenciador.CriarArquivo("a2", "abcdefgh"); // ocupa 1 bloco
    gerenciador.CriarArquivo("a3", "87654321"); // ocupa 1 bloco
    gerenciador.Status();

    // 2. Exclui um arquivo do meio
    gerenciador.ExcluirArquivo("a2");
    gerenciador.Status();

    // 3. Cria um arquivo maior (precisa de 3 blocos)
    gerenciador.CriarArquivo("a4", "11112222333344445555"); // 20 chars, ocupa 3 blocos
    gerenciador.Status();

    // 4. Lê os arquivos
    gerenciador.LerArquivo("a1");
    gerenciador.LerArquivo("a3");
    gerenciador.LerArquivo("a4");

    // 5. Exclui outro arquivo e cria mais um para aumentar a fragmentação
    gerenciador.ExcluirArquivo("a1");
    gerenciador.CriarArquivo("a5", "zzzzzzzzzzzzzzzzzzzz"); // 20 chars, ocupa 3 blocos
    gerenciador.Status();

    Console.WriteLine("== Fim do teste automático ==");

    // Permite uso interativo após os testes
    gerenciador.RunConsole();
}
*/
