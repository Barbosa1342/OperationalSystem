using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    // Classe que representa a memória física
    public class MemoriaFisica
    {
        private List<MemoriaFisica> MemoriasFisicas; // Lista que armazena todas as memórias físicas
        public int Indice { get; private set; } // Índice único da memória física
        public int Tamanho { get; private set; } // Tamanho da memória física
        public Pagina ReferenciaMemoriaVirtual { get; private set; } // Referência à página de memória virtual associada

        // Construtor que inicializa a memória física com um índice e tamanho
        public MemoriaFisica(int indice, int tamanho)
        {
            Indice = indice; // Define o índice da memória física
            Tamanho = tamanho; // Define o tamanho da memória física
        }

        // Método para associar uma página de memória virtual a esta memória física
        public void AssociarMemoriaVirtual(Pagina pagina)
        {
            ReferenciaMemoriaVirtual = pagina; // Armazena a referência à página de memória virtual
        }

        // Método para exibir informações sobre a memória física
        public void MostrarInformacoes()
        {
            Console.WriteLine($"Memória Física - Índice: {Indice}, Tamanho: {Tamanho}");
            if (ReferenciaMemoriaVirtual != null) // Verifica se há uma página associada
            {
                Console.WriteLine($"Referência à Memória Virtual: Página {ReferenciaMemoriaVirtual.Numero}");
            }
            else
            {
                Console.WriteLine("Nenhuma memória virtual associada.");
            }
        }
        // Método que associa uma página de memória virtual a uma memória física específica
        public void AssociarMemoriaFisica(int indice, Pagina pagina)
        {
            if (indice >= 0 && indice < MemoriasFisicas.Count) // Verifica se o índice é válido
            {
                MemoriasFisicas[indice].AssociarMemoriaVirtual(pagina); // Associa a página à memória física
                Console.WriteLine($"Página {pagina.Numero} associada à memória física de índice {indice}.");
            }
            else
            {
                Console.WriteLine("Índice de memória física inválido."); // Mensagem de erro para índice inválido
            }
        }
        //WSClock
        public int WSClock()
        {
            int menorIndice = -1; // Inicializa o menor índice como -1 (não encontrado)
            foreach (var memoria in MemoriasFisicas)
            {
                if (memoria.ReferenciaMemoriaVirtual != null && !memoria.ReferenciaMemoriaVirtual.EmUso)
                {
                    // Se a página não está em uso, verifica se é o menor índice
                    if (menorIndice == -1 || memoria.Indice < menorIndice)
                    {
                        menorIndice = memoria.Indice; // Atualiza o menor índice
                    }
                }
            }

            if (menorIndice != -1)
            {
                Console.WriteLine($"Índice {menorIndice} selecionado para substituição.");
            }
            else
            {
                Console.WriteLine("Nenhuma memória física disponível para substituição.");
            }

            return menorIndice; // Retorna o menor índice encontrado ou -1 se nenhum foi encontrado
        }

        // Método para substituir a memória física usando o WSClock
        public void SubstituirMemoriaFisica(Pagina novaPagina)
        {
            int indiceParaSubstituir = WSClock(); // Obtém o índice para substituição usando o WSClock

            if (indiceParaSubstituir != -1)
            {
                MemoriasFisicas[indiceParaSubstituir].AssociarMemoriaVirtual(novaPagina); // Substitui a página
                Console.WriteLine($"Página {novaPagina.Numero} associada à memória física de índice {indiceParaSubstituir}.");
            }
            else
            {
                Console.WriteLine("Não foi possível substituir nenhuma memória física.");
            }
        }
       
        }
        
    }

