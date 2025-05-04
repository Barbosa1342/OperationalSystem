using System;
using System.Diagnostics;

namespace SistemasOperacionais
{
    static internal class Semaforo
    {
        static public List<int> Items = new List<int>();

        static public int mutex = 1;
        static public int empty = 0;
        static public int full = 0;
        static private List<Processo> processosDown = new List<Processo>();

        // Consumidor
        static public int RecebeDados(int indice, Processo processo, out int? item)
        {
            item = null;
            if (indice == 1)
            {
                return Down(ref full, processo);
            }
            else if (indice == 2)
            {
                return Down(ref mutex, processo);
            }
            else if (indice == 3)
            {
                item = Items[0];
                Items.RemoveAt(0);
            }
            else if (indice == 4)
            {
                Up(ref mutex);
            }
            else if (indice == 5)
            {
                Up(ref empty);

                return 0;
            }
            return 1;
        }

        // Produtor
        static public int InserirDados(int indice, Processo processo, int dados)
        {
            if (indice == 1)
            {
                return Down(ref empty, processo);
            }
            else if (indice == 2)
            {
                return Down(ref Semaforo.mutex, processo);
            }
            else if (indice == 3)
            {
                Items.Add(dados);
            }
            else if (indice == 4)
            {
                Up(ref mutex);
            }
            else if (indice == 5)
            {
                Up(ref full);

                return 0;
            }
            return 1;
        }

        static public void Up(ref int semaforo)
        {
            semaforo++;

            if (processosDown.Count > 0)
            {
                Random random = new Random();
                int indice = random.Next(processosDown.Count());
                processosDown[indice].Acao();

                // Tratar fluxo do processo com esse acao()
            }
        }

        static public int Down(ref int semaforo, Processo processo)
        {
            if (semaforo > 0)
            {
                semaforo--;
                return 1;

                processo.Estado = "Running";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);
            }
            else if(semaforo == 0)
            {
                processosDown.Add(processo);
                return -1;

                processo.Estado = "Waiting";
                Console.WriteLine(processo.ProcID + ": " + processo.Estado);
            }
            return 0;
        }
    }
}
