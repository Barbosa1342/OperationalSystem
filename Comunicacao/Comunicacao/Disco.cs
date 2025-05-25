using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que representa o disco e gerencia os blocos e a FAT
    public class Disco
    {
        public List<Bloco> Blocos { get; } // Lista de blocos do disco

        public int[] FAT { get; } // Estrutura FAT: cada posição indica o próximo bloco do arquivo ou -1 (livre), -2 (fim)

        // Propriedade que retorna a quantidade total de blocos
        public int QuantidadeBlocos => Blocos.Count;

        // Construtor que inicializa os blocos e a FAT
        public Disco(int quantidadeBlocos)
        {
            Blocos = new List<Bloco>(); // Inicializa a lista de blocos
            FAT = new int[quantidadeBlocos]; // Inicializa a FAT com o tamanho especificado
            for (int i = 0; i < quantidadeBlocos; i++)
            {
                Blocos.Add(new Bloco(i)); // Cria cada bloco com id único
                FAT[i] = -1; // Marca todos os blocos como livres
            }
        }

        // Aloca uma quantidade de blocos livres e retorna seus índices
        public List<int> AlocarBlocos(int quantidade)
        {
            var livres = new List<int>(); // Lista para armazenar índices de blocos livres
            for (int i = 0; i < FAT.Length && livres.Count < quantidade; i++)
                if (FAT[i] == -1) livres.Add(i); // Adiciona bloco livre

            if (livres.Count < quantidade) return null; // Não há blocos suficientes

            for (int i = 0; i < livres.Count - 1; i++)
                FAT[livres[i]] = livres[i + 1]; // Encadeia os blocos na FAT
            FAT[livres[^1]] = -2; // Último bloco aponta para -2 (fim do arquivo)

            return livres; // Retorna os índices dos blocos alocados
        }

        // Libera os blocos de um arquivo a partir do bloco inicial
        public void LiberarBlocos(int blocoInicial)
        {
            int idx = blocoInicial; // Começa pelo bloco inicial
            while (idx != -2 && idx != -1) // Percorre os blocos do arquivo
            {
                int prox = FAT[idx]; // Salva o próximo bloco
                FAT[idx] = -1; // Marca como livre
                Blocos[idx].Data = string.Empty; // Limpa os dados do bloco
                idx = prox; // Vai para o próximo bloco
            }
        }
    }
}
