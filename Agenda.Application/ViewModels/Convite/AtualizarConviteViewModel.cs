using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class AtualizarConviteViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Id Evento")]
        public Guid EventoId { get; set; }
        [DisplayName("Id Usuario")]
        public Guid UsuarioId { get; set; }
        //ver sobre esse
        //public EnumStatusConviteEvento Status { get; private set; }

        [DisplayName("Permissões")]
        public PermissaoConviteViewModel PermissoesViewModel { get; set; }
    }
}
