namespace SistemasOperacionais
{
    public abstract class Processo
    {
        protected string estado = "New";
        protected int procID;
        protected float tempoExecucao;
        protected int memoriaAlocada;

        public Processo(int procID)
        {
            this.procID = procID;
            Console.WriteLine(procID + ": " + estado);
        }

        public string Estado { get => estado; set => estado = value; }
        public int ProcID { get => procID; set => procID = value; }
        public float TempoExecucao { get => tempoExecucao; set => tempoExecucao = value; }
        public int MemoriaAlocada { get => memoriaAlocada; set => memoriaAlocada = value; }

        abstract public void Acao(List<int> items);
    }
}
