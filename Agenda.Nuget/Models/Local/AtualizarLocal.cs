using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class AtualizarLocal
    {
        public Guid Id { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Reserva { get; private set; }
        public int LotacaoMaxima { get; private set; }

        public AtualizarLocal(Guid id, string identificadorExterno, string nome, string descricao, bool reserva, int lotacaoMaxima)
        {
            Id = id;
            IdentificadorExterno = identificadorExterno;
            Nome = nome;
            Descricao = descricao;
            Reserva = reserva;
            LotacaoMaxima = lotacaoMaxima;
        }
    }
}
