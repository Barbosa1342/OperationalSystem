namespace SistemasOperacionais
{
    static class Historico
    {

        static private List<Resultado> listaResultados = new List<Resultado>();

        static public void adicionarResultado(Resultado R)
        {
            listaResultados.Add(R);
        }

        static public List<Resultado> buscaPorID(int ID)
        {
            List<Resultado> resultadosEncontrados = new List<Resultado>();

            foreach (Resultado R in listaResultados)
            {
                if (R.IDProcessos == ID)
                {
                    resultadosEncontrados.Add(R);
                }
            }
            return resultadosEncontrados;
        }

        static public List<Resultado> buscaPorEscalonador(string escalonador)
        {
            List<Resultado> resultadosEncontrados = new List<Resultado>();

            foreach (Resultado R in listaResultados)
            {
                if (R.TipoEscalonamento == escalonador)
                {
                    resultadosEncontrados.Add(R);
                }
            }
            return resultadosEncontrados;
        }

        static public void Imprimir()
        {
            if (listaResultados.Count == 0)
            {
                Console.WriteLine("Nenhum item foi encontrado!");
                return;
            }

            for (int i = 0; i < listaResultados.Count; i++)
            {
                Console.WriteLine("Escalonador: " + listaResultados[i].TipoEscalonamento);
                Console.WriteLine("ID: " + listaResultados[i].IDProcessos);
                Console.WriteLine("Tempo de Execucao: " + listaResultados[i].TempoExecucao);
                Console.WriteLine("Tempo de Espera: " + listaResultados[i].TempoEspera);
            }
        }

        static public void Imprimir(string escalonador)
        {
            List<Resultado> resultadosEncontrados = buscaPorEscalonador(escalonador);

            if (resultadosEncontrados.Count == 0)
            {
                Console.WriteLine("Nenhum item foi encontrado!");
                return;
            }

            Console.WriteLine("Escalonador - " + escalonador);
            for (int i = 0; i < resultadosEncontrados.Count; i++)
            {   
                Console.WriteLine("ID: " + resultadosEncontrados[i].IDProcessos);
                Console.WriteLine("Tempo de Execucao: " + resultadosEncontrados[i].TempoExecucao);
                Console.WriteLine("Tempo de Espera: " + resultadosEncontrados[i].TempoEspera);
            }
        }

        static public void LimparResultados(string escalonador)
        {
            if (listaResultados.Count == 0)
            {
                return;
            }

            foreach (Resultado res in listaResultados)
            {
                if (res.TipoEscalonamento == escalonador)
                {
                    listaResultados.Remove(res);
                }
            }
        }
    }
}
