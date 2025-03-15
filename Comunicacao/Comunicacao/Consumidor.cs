namespace Comunicacao
{
    internal class Consumidor : Processo
    {
        public Consumidor(int procID) : base(procID)
        {
        }
        public override void Acao(List<int> items)
        {
            int item = items[0];
            items.RemoveAt(0);

            item += 1; // faz algo com o item
        }
    }
}
