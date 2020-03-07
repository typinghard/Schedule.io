using System;
using System.Collections.Generic;
using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;

namespace Agenda.Domain.Events
{
   public class EventoAgendaAtualizadoEvent : Event
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public IList<Guid> Pessoas { get;  set; }
        public Guid? Local { get;  set; }
        public DateTime DataInicio { get;  set; }
        public DateTime? DataFinal { get;  set; }
        public DateTime? DataLimiteConfirmacao { get;  set; }
        public int QuantidadeMinimaDeUsuarios { get;  set; }
        public bool OcupaUsuario { get;  set; }
        public bool EventoPublico { get;  set; }
        public TipoEvento TipoEvento { get;  set; }
        public EnumFrequencia EnumFrequencia { get;  set; }

        public EventoAgendaAtualizadoEvent(Guid id, Guid agendaId, string identificadorExterno, string titulo, string descricao, IList<Guid> pessoas, Guid? local, DateTime dataInicio, DateTime? dataFinal,
            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocupaUsuario, bool eventoPublico, TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Pessoas = pessoas;
            this.Local = local;
            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
            this.QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
            this.OcupaUsuario = ocupaUsuario;
            this.EventoPublico = eventoPublico;
            this.TipoEvento = tipoEvento;
            this.EnumFrequencia = enumFrequencia;
        }

    }
}
