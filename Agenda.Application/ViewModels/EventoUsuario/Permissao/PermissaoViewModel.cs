using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class PermissaoViewModel
    {
        [DisplayName("Modificar Evento")]
        public bool ModificaEvento { get; set; }
        [DisplayName("Convidar Usuário")]
        public bool ConvidaUsuario { get; set; }
        [DisplayName("Ver Lista de Convidados")]
        public bool VeListaDeConvidados { get; set; }
    }
}
