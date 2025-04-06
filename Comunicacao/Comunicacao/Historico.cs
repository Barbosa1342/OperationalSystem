using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            for (int i = 0; i < listaResultados.Count; i++)
            {
                Console.WriteLine("Escalonador: " + listaResultados[i].TipoEscalonamento);
                Console.WriteLine("ID: " + listaResultados[i].IDProcessos);
            }
        }
    }
}
