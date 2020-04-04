using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Models
{
    public class Usuario
    {
        public string Id { get; internal set; }
        public DateTime CriadoAs { get; internal set; }
        public DateTime AtualizadoAs { get; internal set; }
        public string Email { get;  set; }

        public string AgendaId { get; set; }
    }
}
