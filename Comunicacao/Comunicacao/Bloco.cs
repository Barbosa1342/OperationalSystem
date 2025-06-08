using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que representa um bloco do disco
    public class Bloco
    {
        public int Id { get; } // Identificador único do bloco

        public string Data { get; set; } // Dados armazenados no bloco

        // Construtor que inicializa o bloco com um id e dados vazios
        public Bloco(int id)
        {
            Id = id; // Define o identificador do bloco
            Data = string.Empty; // Inicializa os dados como vazio
        }

        // Método para ler os dados do bloco
        public string Ler() => Data; // Retorna os dados armazenados

        // Método para escrever dados no bloco
        public void Escrever(string data) => Data = data; // Define os dados do bloco
    }
}
