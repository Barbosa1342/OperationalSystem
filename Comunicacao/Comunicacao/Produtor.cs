namespace SistemasOperacionais
{
    internal class Produtor : Processo
    {
        public Produtor(int procID) : base(procID)
        {
        }
        public override int Acao(List<int> items)
        {
            items.Add(1);
            return 0;
        }
    }
}
