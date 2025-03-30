using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasOperacionais
{
    internal class Resultado
    {
        private string tipoEscalonamento;
        private int idProcessos;
        private float vazao; //soma do tempo de espera pelo tempo de execução
        private float tempoMinimo;
        private float tempoMedio;
        private float tempoMaximo;

        public string TipoEscalonamento { get => tipoEscalonamento; set => tipoEscalonamento = value; }
        public int IDProcessos { get => idProcessos; set => idProcessos = value; }
        public float Vazao { get => vazao; set => vazao = value; }
        public float TempoMinimo { get => tempoMinimo; set => tempoMinimo = value; }
        public float TempoMedio { get => tempoMedio; set => tempoMedio = value; }
        public float TempoMaximo { get => tempoMaximo; set => tempoMaximo = value; }
    }
}
