using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class Local
    {
        public string Id { get; internal set; }
        public DateTime CriadoAs { get; internal set; }
        public DateTime AtualizadoAs { get; internal set; }
        public string IdentificadorExterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Reserva { get; set; }
        public int LotacaoMaxima { get; set; }

    }
}
