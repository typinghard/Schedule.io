using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;


namespace Agenda.Domain.Events
{
    public class EventoAgendaRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string AgendaId { get; set; }
        public string UsuarioId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<Convite> Convites { get; set; }
        public string? Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataLimiteConfirmacao { get; set; }
        public int QuantidadeMinimaDeUsuarios { get; set; }
        public bool OcupaUsuario { get; set; }
        public bool Publico { get; set; }
        public TipoEvento Tipo { get; set; }
        public EnumFrequencia Frequencia { get; set; }


        public EventoAgendaRegistradoEvent(string id, string agendaId, string usuarioId, string identificadorExterno, string titulo, string descricao, IList<Convite> convites, string? local, DateTime dataInicio, DateTime? dataFinal,
            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocupaUsuario, bool publico, TipoEvento tipoEvento, EnumFrequencia frequencia)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Convites = convites;
            this.Local = local;
            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
            this.QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
            this.OcupaUsuario = ocupaUsuario;
            this.Publico = publico;
            this.Tipo = tipoEvento;
            this.Frequencia = frequencia;
        }
    }
}
