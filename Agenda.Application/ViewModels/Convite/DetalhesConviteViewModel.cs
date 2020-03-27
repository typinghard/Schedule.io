using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class DetalhesConviteViewModel
    {
        public string Id { get; set; }
        [DisplayName("Id Usuario")]
        public string UsuarioId { get; set; }
        [DisplayName("Confirmação")]
        public bool Confirmacao { get; set; }

        [DisplayName("Permissões")]
        public PermissaoViewModel PermissaoViewModel { get; set; }
    }
}
