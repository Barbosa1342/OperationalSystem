namespace Comunicacao
{
    public abstract class Recurso
    {
        protected int recursoTotal;//Define Total de recurso
        protected bool disponibilidade; //Consulta de há disponibilidade de recurso e me retorna se verdadeiro ou falso

        public int RecursoTotal { get => recursoTotal; set => recursoTotal = value; }
        public bool Disponibilidade { get => disponibilidade; set => disponibilidade = value; }

        public Recurso(int recursoTotal)
        {
            this.recursoTotal = recursoTotal;
            disponibilidade = true;
        }

        public abstract void Alocacao(int quantidade);

        public bool ConsultarDisponibilidade(int quantidade)
        {
            disponibilidade = recursoTotal >= quantidade;
            return disponibilidade;
        }
    }
}
