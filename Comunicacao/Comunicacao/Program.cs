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


EmLote p1 = new EmLote(1);
EmLote p2 = new EmLote(2);

SJF sjf = new SJF();
FCFS fcfs = new FCFS();

// Kernel.Escalonar(sjf);
// Kernel.Escalonar(fcfs);

Historico.Imprimir();