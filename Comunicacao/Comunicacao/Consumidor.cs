namespace SistemasOperacionais
{
    internal class Consumidor : Processo
    {
        private List<Delegate> funcoes = new List<Delegate>();

        public delegate void Down(ref int semaforo, Processo processo);
        
        public Consumidor(int procID) : base(procID)
        {
            Down teste = new Down(Semaforo.Down);
            funcoes.Add(teste);

            //funcoes[0](Semaforo.mutex, this);
        }
        public override int Acao(List<int> items)
        {
            int item = items[0];
            items.RemoveAt(0);

            item += 1; // faz algo com o item
            return 0;
        }
    }
}
