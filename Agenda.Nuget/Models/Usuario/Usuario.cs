using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class Usuario
    {
        public Guid Id { get; internal set; }
        public DateTime CriadoAs { get; internal set; }
        public DateTime AtualizadoAs { get; internal set; }
        public string Email { get;  set; }

        public Guid AgendaId { get; set; }
    }
}
