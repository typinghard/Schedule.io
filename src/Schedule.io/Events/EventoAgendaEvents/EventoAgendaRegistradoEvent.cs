using Schedule.io.Core.Messages;
using Schedule.io.Enums;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;


namespace Schedule.io.Events.EventoAgendaEvents
{
    public class EventoRegistradoEvent : Event
    {
        public string Id { get; set; }
        public string AgendaId { get; set; }
        public string UsuarioId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<Convite> Convites { get; set; }
        public string LocalId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataLimiteConfirmacao { get; set; }
        public int QuantidadeMinimaDeUsuarios { get; set; }
        public bool OcupaUsuario { get; set; }
        public bool Publico { get; set; }
        public string TipoEventoId { get; set; }
        public EnumFrequencia Frequencia { get; set; }


        public EventoRegistradoEvent(string id, string agendaId, string usuarioId, string identificadorExterno, string titulo, string descricao, IEnumerable<Convite> convites, string localId, DateTime dataInicio, DateTime? dataFinal,
            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocupaUsuario, bool publico, string tipoEventoId, EnumFrequencia frequencia)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.UsuarioId = usuarioId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Convites = convites;
            this.LocalId = localId;
            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
            this.QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
            this.OcupaUsuario = ocupaUsuario;
            this.Publico = publico;
            this.TipoEventoId = tipoEventoId;
            this.Frequencia = frequencia;
        }
    }
}
