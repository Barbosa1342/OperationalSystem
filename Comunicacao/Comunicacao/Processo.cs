namespace SistemasOperacionais
{
    abstract class Processo
    {
        protected string estado = "New";
        protected int procID;

        public Processo(int procID)
        {
            this.procID = procID;
            Console.WriteLine(procID + ": " + estado);
        }

        public string Estado { get => estado; set => estado = value; }
        public int ProcID { get => procID; set => procID = value; }

        abstract public void Acao(List<int> items);
    }
}
