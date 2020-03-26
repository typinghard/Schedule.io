using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class NovoLocal
    {
        public string IdentificadorExterno { get; set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Reserva { get; private set; }
        public int LotacaoMaxima { get; private set; }

        public NovoLocal(string identificadorExterno, string nome, string descricao, bool reserva, int lotacaoMaxima)
        {
            IdentificadorExterno = identificadorExterno;
            Nome = nome;
            Descricao = descricao;
            Reserva = reserva;
            LotacaoMaxima = lotacaoMaxima;
        }
    }
}
