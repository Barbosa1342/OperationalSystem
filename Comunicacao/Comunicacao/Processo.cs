namespace SistemasOperacionais
{
    public abstract class Processo
    {
        protected string estado;
        protected int procID;
        protected float tempoExecucao;
        protected int memoriaAlocada;
        protected int indice;

        protected List<int> acoes = new List<int>();

        public Processo(int procID)
        {
            estado = "New";
            this.procID = procID;
            Indice = 0;

            Console.WriteLine(procID + ": " + estado);

            Kernel.PopularFilaProcessos(this);
        }

        public Processo(int procID, float tempoExecucao, int memoriaAlocada, List<int> acoes)
        {
            estado = "New";
            this.procID = procID;
            this.tempoExecucao = tempoExecucao;
            this.memoriaAlocada = memoriaAlocada;

            foreach (int i in acoes)
            {
                this.acoes.Add(i);
            }
            
            Indice = 0;
        }

        public string Estado { get => estado; set => estado = value; }
        public int ProcID { get => procID; set => procID = value; }
        public float TempoExecucao { get => tempoExecucao; set => tempoExecucao = value; }
        public int MemoriaAlocada { get => memoriaAlocada; set => memoriaAlocada = value; }
        public int Indice { get => indice; set => indice = value; }
        public List<int> Acoes { get => acoes; set => acoes = value; }
        abstract public int Acao(out int? teste);
    }
}
