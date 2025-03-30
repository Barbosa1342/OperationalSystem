namespace Comunicacao
{
    public class Memoria : Recurso
    {
        private int posicoesOcupadas;
        private Dictionary<string, int> posicoesMemoria = new();  // Mapeamento da posição para endereço

        public int PosicoesOcupadas { get => posicoesOcupadas; set => posicoesOcupadas = value; }
        public Dictionary<string, int> PosicoesMemoria { get => posicoesMemoria; set => posicoesMemoria = value; }

        public Memoria(int memoriaTotal) : base(memoriaTotal)
        {
            PosicoesOcupadas = 0;
            DefineMemoriaTotal(memoriaTotal);
        }

        public override void Alocacao(int quantidade)
        {
            if (ConsultarDisponibilidade(quantidade))
            {
                PosicoesOcupadas += quantidade;
                RecursoTotal -= quantidade;

                Console.WriteLine($"Memória alocada: {quantidade} posições ocupadas.");
            }
            else
            {
                Console.WriteLine("Falha na alocação: Memória insuficiente.");
            }
        }

        public void MostrarMapaMemoria()
        {
            foreach (var pos in PosicoesMemoria)
            {
                Console.WriteLine($"Endereco: {pos.Key}, Dado: {pos.Value}");
            }
        }

        public void InicializaMemoria(int quantidade)
        {
            PosicoesMemoria.Clear();
            for (int i = 0; i < quantidade; i++)
            {
                string endereco = $"0x{i.ToString("X4")}";
                PosicoesMemoria.Add(endereco, 0);
            }
            Console.WriteLine($"Memória inicializada com {quantidade} posições.");
        }

        public void DefineMemoriaTotal(int quantidade)
        {
            recursoTotal = quantidade;
            InicializaMemoria(quantidade);
            Console.WriteLine($"Memória total definida para {quantidade} posições.");
        }
    }
}
