namespace SistemasOperacionais
{
    abstract class Processo
    {
        protected string estado = "New";
        protected int procID;
        protected float tempoExecucao;
        protected int memoriaAlocada;
        protected int indice;

        protected List<int> acoes = new List<int>
        {
            1,
            2,
            3,
            4
        };


        public Processo(int procID)
        {
            this.procID = procID;
            Console.WriteLine(procID + ": " + estado);

            Kernel.PopulateProcessQueue(this);
        }

        public string Estado { get => estado; set => estado = value; }
        public int ProcID { get => procID; set => procID = value; }
        public float TempoExecucao { get => tempoExecucao; set => tempoExecucao = value; }
        public int MemoriaAlocada { get => memoriaAlocada; set => memoriaAlocada = value; }
        public int Indice { get => indice; set => indice = value; }

        abstract public int Acao(List<int> items);
    }
}
