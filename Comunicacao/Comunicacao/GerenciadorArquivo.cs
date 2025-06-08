using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemasOperacionais
{
    // Classe que gerencia a criação, leitura e exclusão de arquivos
    public class GerenciadorArquivo
    {
        private Disco disco; // Disco do sistema

        private List<Arquivo> arquivos; // Lista de arquivos criados

        // Construtor que inicializa o disco e a lista de arquivos
        public GerenciadorArquivo(int blocos)
        {
            disco = new Disco(blocos); // Cria o disco com a quantidade de blocos
            arquivos = new List<Arquivo>(); // Inicializa a lista de arquivos
        }

        // Cria um arquivo, alocando blocos e escrevendo o conteúdo
        public void CriarArquivo(string nome, string conteudo)
        {
            int blocosNecessarios = (int)Math.Ceiling(conteudo.Length / 8.0); // Calcula blocos necessários
            var alocados = disco.AlocarBlocos(blocosNecessarios); // Aloca blocos
            if (alocados == null)
            {
                Console.WriteLine("Espaço insuficiente.");
                return;
            }
            for (int i = 0; i < alocados.Count; i++)
            {
                int inicio = i * 8; // Posição inicial do conteúdo para este bloco
                int len = Math.Min(8, conteudo.Length - inicio); // Tamanho do trecho
                disco.Blocos[alocados[i]].Escrever(conteudo.Substring(inicio, len)); // Escreve no bloco
            }
            arquivos.Add(new Arquivo(nome, conteudo.Length, alocados[0])); // Adiciona o arquivo à lista
            Console.WriteLine($"Arquivo '{nome}' criado.");
        }

        // Lê o conteúdo de um arquivo seguindo os blocos na FAT
        public void LerArquivo(string nome)
        {
            var arq = arquivos.FirstOrDefault(a => a.Nome == nome); // Busca o arquivo
            if (arq == null)
            {
                Console.WriteLine("Arquivo não encontrado.");
                return;
            }
            int idx = arq.BlocoInicial; // Começa pelo bloco inicial
            string conteudo = "";
            while (idx != -2 && idx != -1) // Percorre os blocos do arquivo
            {
                conteudo += disco.Blocos[idx].Ler(); // Lê o conteúdo do bloco
                idx = disco.FAT[idx]; // Vai para o próximo bloco
            }
            Console.WriteLine($"Conteúdo de '{nome}': {conteudo}");
        }

        // Exclui um arquivo e libera seus blocos
        public void ExcluirArquivo(string nome)
        {
            var arq = arquivos.FirstOrDefault(a => a.Nome == nome); // Busca o arquivo
            if (arq == null)
            {
                Console.WriteLine("Arquivo não encontrado.");
                return;
            }
            disco.LiberarBlocos(arq.BlocoInicial); // Libera os blocos na FAT
            arquivos.Remove(arq); // Remove o arquivo da lista
            Console.WriteLine($"Arquivo '{nome}' excluído.");
        }

        // Exibe o estado da FAT, arquivos e blocos
        public void Status()
        {
            Console.WriteLine("FAT:");
            for (int i = 0; i < disco.FAT.Length; i++)
                Console.Write($"{i}:{disco.FAT[i]} "); // Mostra a FAT
            Console.WriteLine("\nArquivos:");
            foreach (var a in arquivos)
                Console.WriteLine($"Arquivo: {a.Nome}, Bloco inicial: {a.BlocoInicial}"); // Mostra arquivos
            Console.WriteLine("\nBlocos:");
            for (int i = 0; i < disco.Blocos.Count; i++)
            {
                string status = disco.FAT[i] == -1 ? "LIVRE" : "OCUPADO"; // Verifica se o bloco está livre
                string data = string.IsNullOrEmpty(disco.Blocos[i].Data) ? "(vazio)" : disco.Blocos[i].Data; // Dados do bloco
                Console.WriteLine($"Bloco {i}: {status} | Dados: {data}"); // Mostra o bloco
            }
        }

        // Interface de console para comandos do usuário
        public void RunConsole()
        {
            while (true)
            {
                Console.Write("> "); // Prompt
                var input = Console.ReadLine(); // Lê comando
                if (input == null) continue;
                var parts = input.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries); // Divide comando
                if (parts.Length == 0) continue;
                switch (parts[0].ToLower())
                {
                    case "create":
                        if (parts.Length < 3)
                            Console.WriteLine("Uso: create <nome> <conteudo>");
                        else
                            CriarArquivo(parts[1], parts[2]);
                        break;
                    case "read":
                        if (parts.Length < 2)
                            Console.WriteLine("Uso: read <nome>");
                        else
                            LerArquivo(parts[1]);
                        break;
                    case "delete":
                        if (parts.Length < 2)
                            Console.WriteLine("Uso: delete <nome>");
                        else
                            ExcluirArquivo(parts[1]);
                        break;
                    case "status":
                        Status();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Comando desconhecido.");
                        break;
                }
            }
        }
    }
}
