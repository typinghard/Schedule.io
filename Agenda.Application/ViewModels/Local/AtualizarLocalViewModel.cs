using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class AtualizarLocalViewModel
    {
        public string Id { get; set; }
        [DisplayName("Id Externo")]
        public string IdentificadorExterno { get; set; }
        [DisplayName("Nome do Local")]
        public string NomeLocal { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Reservar Local")]
        public bool ReservaLocal { get; set; }
        [DisplayName("Lotal Máxima")]
        public int LotacaoMaxima { get; set; }
    }
}
