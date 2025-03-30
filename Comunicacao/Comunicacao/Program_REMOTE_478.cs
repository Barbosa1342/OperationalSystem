using System.Runtime.InteropServices;
using System.Security.Cryptography;
using SistemasOperacionais;

/*int n = 100;
    List<int> items = new();

Semaforo semaforo = new();
semaforo.empty = n;

Produtor produtor = new Produtor(1);
Consumidor consumidor = new Consumidor(2);

Kernel kernel = new();

kernel.PopulateProcessQueue(produtor);
kernel.PopulateProcessQueue(consumidor);

kernel.ProdutorComunicacao(items, semaforo, produtor, false);*/

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
