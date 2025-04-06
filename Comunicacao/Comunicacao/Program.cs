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

/*
EmLote p1 = new EmLote(1);
EmLote p2 = new EmLote(2);

Kernel.PopulateProcessQueue(p1);
Kernel.PopulateProcessQueue(p2);

SJF sjf = new SJF();
FCFS fcfs = new FCFS();

Kernel.Escalonar(sjf);
Kernel.Escalonar(fcfs);
*/

/* ----------- TESTE GABRIEL -----------
Resultado r1 = new Resultado();
Resultado r2 = new Resultado();

r1.IDProcessos = 1;
r2.IDProcessos = 2;
r1.TipoEscalonamento = "nome";
r2.TipoEscalonamento = "nome";

Historico.adicionarResultado(r1);
Historico.adicionarResultado(r2);

List<Resultado> lista = Historico.buscaPorID(1);

for (int i = 0; i < lista.Count; i++)
{
    Console.WriteLine(lista[i].IDProcessos);
}


List<Resultado> lista1 = Historico.buscaPorEscalonador("nome");

for (int i = 0; i < lista1.Count; i++)
{
    Console.WriteLine(lista1[i].TipoEscalonamento);
}
*/

class Program
{
    static void Main(string[] args)
    {
        GerenciadorDeMemoria gerenciador = new GerenciadorDeMemoria(1024);

        // Criando processos usando sua classe Processo
        Processo processo1 = new Produtor(1);
        Processo processo2 = new Produtor(2);
        Processo processo3 = new Consumidor(3);
        Processo processo4 = new Consumidor(4);

        // Alocar processos na mem�ria
        gerenciador.AlocarProcesso(processo1, 64);
        gerenciador.AlocarProcesso(processo2, 128);
        gerenciador.AlocarProcesso(processo3, 96);

        // Mostrar as p�ginas ap�s a aloca��o inicial
        gerenciador.MostrarPaginas();

        // Alocar um novo processo maior para for�ar substitui��o
        gerenciador.AlocarProcesso(processo4, 512);

        // Mostrar as p�ginas ap�s a substitui��o
        gerenciador.MostrarPaginas();
    }
}

