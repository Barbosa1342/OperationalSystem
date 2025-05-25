using System;
using System.Diagnostics;

namespace SistemasOperacionais
{
    static internal class Semaforo
    {
        static public List<int> Items = new List<int>() { 16, 4, 67 };

        static public int mutex = 1;
        static public int empty = 97;
        static public int full = 3;
        static private List<Processo> processosDown = new List<Processo>();
        // down confere se o valor e maior que 0
        // se for, decresce o valor e continua
        // se for 0, o processo dorme

        // up incrementa o valor
        // se houver um processo dormindo, ele completa uma acao down (ignorando o zero)
        // acoes up e down sao atomicas, elas devem ser completas antes do processo chavear

        // Consumidor
        static public int RecebeDados(int indice, Processo processo, out int? saida)
        {
            saida = null;
            if (indice == 0)
            {
                return Down(ref full, processo);
            }
            else if (indice == 1)
            {
                return Down(ref mutex, processo);
            }
            else if (indice == 2)
            {
                if (Items.Count > 0)
                {
                    saida = Items[0];
                    Items.RemoveAt(0);
                }
            }
            else if (indice == 3)
            {
                return Up(ref mutex, out saida);
            }
            else if (indice == 4)
            {
                Up(ref empty, out saida);
                return 0;
            }
            return 1;
        }

        // Produtor
        static public int InserirDados(int indice, Processo processo, int dados, out int? saida)
        {
            saida = null;

            if (indice == 0)
            {
                return Down(ref empty, processo);
            }
            else if (indice == 1)
            {
                return Down(ref mutex, processo);
            }
            else if (indice == 2)
            {
                Items.Add(dados);
                /*foreach(int dado in Items)
                {
                    Console.WriteLine("Dados: " + dado);
                }*/                
            }
            else if (indice == 3)
            {
                return Up(ref mutex, out saida);
            }
            else if (indice == 4)
            {             
                Up(ref full, out saida);
                return 0;
            }
            return 1;
        }

        static public int Up(ref int semaforo, out int? id)
        {
            id = null;
            semaforo++;

            if (processosDown.Count > 0)
            {
                Random random = new Random();
                int indice = random.Next(processosDown.Count());
                id = processosDown[indice].ProcID;

                // evita conflitos com o retorno
                return 2;             
            }            
            return 1;
        }

        static public int Down(ref int semaforo, Processo processo)
        {
            if (semaforo > 0)
            {
                semaforo--;
                return 1;
            }
            else if(semaforo == 0)
            {
                processosDown.Add(processo);
                return -1;

                // o retorno -1 sobe para o processo
                // depois para o kernel
                // e depois para o escalonador
                // com o retorno, o escalonador decide o que deve ser feito

                // -1 dormir
                // 0 finalizar
                // 1 continuar
            }
            return 0;
        }
    }
}
