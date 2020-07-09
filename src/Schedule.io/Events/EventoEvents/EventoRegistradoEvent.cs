using Schedule.io.Core.Messages;
using Schedule.io.Enums;
using Schedule.io.Models.ValueObjects;
using System;
using System.Collections.Generic;


namespace Schedule.io.Events.EventoAgendaEvents
{
    public class EventoRegistradoEvent : Event
    {
        public string Id { get; private set; }
        public string AgendaId { get; private set; }
        public string UsuarioId { get; private set; }
        public string IdentificadorExterno { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public IEnumerable<Convite> Convites { get; private set; }
        public string LocalId { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFinal { get; private set; }
        public DateTime? DataLimiteConfirmacao { get; private set; }
        public int QuantidadeMinimaDeUsuarios { get; private set; }
        public bool OcupaUsuario { get; private set; }
        public bool Publico { get; private set; }
        public string TipoEventoId { get; private set; }
        public EnumFrequencia Frequencia { get; private set; }


        public EventoRegistradoEvent(string id, string agendaId, string usuarioId, string identificadorExterno, string titulo, string descricao, IEnumerable<Convite> convites, string localId, DateTime dataInicio, DateTime? dataFinal,
            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocupaUsuario, bool publico, string tipoEventoId, EnumFrequencia frequencia)
        {
            AggregateId = id;
            Id = id;
            AgendaId = agendaId;
            UsuarioId = usuarioId;
            IdentificadorExterno = identificadorExterno;
            Titulo = titulo;
            Descricao = descricao;
            Convites = convites;
            LocalId = localId;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            DataLimiteConfirmacao = dataLimiteConfirmacao;
            QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
            OcupaUsuario = ocupaUsuario;
            Publico = publico;
            TipoEventoId = tipoEventoId;
            Frequencia = frequencia;
        }
    }
}
