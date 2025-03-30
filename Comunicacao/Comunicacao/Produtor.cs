namespace SistemasOperacionais
{
    internal class Produtor : Processo
    {
        public Produtor(int procID) : base(procID)
        {
        }
        public override void Acao(List<int> items)
        {
            items.Add(1);
        }
    }
}
