using SistemasOperacionais;

/*
int n = 100;
List<int> items = new();

Semaforo semaforo = new();
semaforo.empty = n;

Produtor produtor = new Produtor(1);
Consumidor consumidor = new Consumidor(2);

kernel.PopulateProcessQueue(produtor);
kernel.PopulateProcessQueue(consumidor);

kernel.ProdutorComunicacao(items, semaforo, produtor, false);
Kernel.ProdutorComunicacao(items, semaforo, produtor, false);
*/

SJF sjf = new SJF();
FCFS fcfs = new FCFS();

int ultimoId = 0;

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
        Console.WriteLine("8 - Encerrar");
        Console.Write("Escolha: ");

        string? opcaoStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(opcaoStr))
        {
            bool sucesso = Int32.TryParse(opcaoStr, out opcao);
        }
        Console.Clear();
    } while (opcao < 1 || opcao > 8);

    if (opcao == 1)
    {
        EmLote p1 = new EmLote(ultimoId);
        ultimoId++;
    }
    else if (opcao == 2)
    {
        Kernel.Escalonar(sjf);
    }
    else if (opcao == 3)
    {
        Kernel.Escalonar(fcfs);
    }
    else if(opcao == 4)
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
        break;
    }

    Console.ReadKey();
    Console.Clear();
}