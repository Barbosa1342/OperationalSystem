using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que representa um arquivo no sistema
    public class Arquivo
    {
        public string Nome { get; } // Nome do arquivo

        public int Tamanho { get; } // Tamanho do arquivo em caracteres

        public int BlocoInicial { get; } // Índice do bloco inicial na FAT

        // Construtor que inicializa o arquivo
        public Arquivo(string nome, int tamanho, int blocoInicial)
        {
            Nome = nome; // Define o nome do arquivo
            Tamanho = tamanho; // Define o tamanho do arquivo
            BlocoInicial = blocoInicial; // Define o bloco inicial
        }
    }
}
    // Classe que representa um arquivo no sistema


