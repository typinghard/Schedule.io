using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Models
{
    public class Agenda
    {
        public string Id { get; internal set; }
        public DateTime CriadoAs { get; internal set; }
        public DateTime AtualizadoAs { get; internal set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Publico { get; set; }
        public Usuario Usuario { get; set; }
    }
}
