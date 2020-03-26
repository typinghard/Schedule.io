using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class DetalhesEventoAgendaViewModel
    {
        public string Id { get; set; }
        [DisplayName("Agenda")]
        public string AgendaId { get; set; }
        [DisplayName("Identificador Externo")]
        public string IdentificadorExterno { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Pessoas")]
        public IList<string> Pessoas { get; set; }
        [DisplayName("Endereço")]
        public Guid? Local { get; set; }
        [DisplayName("Data do Evento")]
        public DateTime DataInicio { get; set; }
        [DisplayName("Data Final Evento")]
        public DateTime? DataFinal { get; set; }
        [DisplayName("Data Limite de Confirmação")]
        public DateTime? DataLimiteConfirmacao { get; set; }
        [DisplayName("Quantidade mínima de usuarios para o evento")]
        public int QuantidadeMinimaDeUsuarios { get; set; }
        [DisplayName("Ocupar usuario")]
        public bool OcupaUsuario { get; set; }
        [DisplayName("Evento Público")]
        public bool EventoPublico { get; set; }
        [DisplayName("Tipo Evento")]
        public TipoEvento TipoEvento { get; set; }
        [DisplayName("Frequencia do Evento")]
        public EnumFrequencia EnumFrequencia { get; set; }
    }
}
