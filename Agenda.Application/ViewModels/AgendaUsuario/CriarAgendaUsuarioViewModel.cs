using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class CriarAgendaUsuarioViewModel
    {
        [DisplayName("Id Agenda")]
        public Guid AgendaId { get; set; }
        [DisplayName("Id Usuario")]
        public Guid UsuarioId { get; set; }
    }
}
