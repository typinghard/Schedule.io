using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
  public class AtualizarAgendaUsuarioViewModel
    {
        public string Id { get; set; }
        [DisplayName("Id Agenda")]
        public string AgendaId { get; set; }
        [DisplayName("Id Usuario")]
        public string UsuarioId { get; set; }
    }
}
