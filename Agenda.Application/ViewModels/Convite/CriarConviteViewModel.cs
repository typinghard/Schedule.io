using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class CriarConviteViewModel
    {

        [DisplayName("Id Evento")]
        public string EventoId { get; set; }
        [DisplayName("Id Usuario")]
        public string UsuarioId { get; set; }
        //ver sobre esse
        //public EnumStatusConviteEvento Status { get; private set; }

        [DisplayName("Permissões")]
        public PermissaoViewModel PermissoesViewModel { get; set; }
    }

}
