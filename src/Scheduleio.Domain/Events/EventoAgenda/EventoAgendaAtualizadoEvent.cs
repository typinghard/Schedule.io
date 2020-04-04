using Schedule.io.Core.Core.Messages;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;

namespace Schedule.io.Core.Events.EventoAgenda
{
   public class EventoAgendaAtualizadoEvent : Event
    {
        public string Id { get; set; }
        public string AgendaId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get;  set; }
        public string Descricao { get;  set; }
        public IList<string> Pessoas { get;  set; }
        public string Local { get;  set; }
        public DateTime DataInicio { get;  set; }
        public DateTime? DataFinal { get;  set; }
        public DateTime? DataLimiteConfirmacao { get;  set; }
        public int QuantidadeMinimaDeUsuarios { get;  set; }
        public bool OcupaUsuario { get;  set; }
        public bool EventoPublico { get;  set; }
        public TipoEvento TipoEvento { get;  set; }
        public EnumFrequencia EnumFrequencia { get;  set; }

        public EventoAgendaAtualizadoEvent(string id, string agendaId, string identificadorExterno, string titulo, string descricao, IList<string> pessoas, string local, DateTime dataInicio, DateTime? dataFinal,
            DateTime? dataLimiteConfirmacao, int quantidadeMinimaDeUsuarios, bool ocupaUsuario, bool eventoPublico, TipoEvento tipoEvento, EnumFrequencia enumFrequencia)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            //this.Usuarios = usuarios;
            this.Local = local;
            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
            //this.QuantidadeMinimaDeUsuarios = qtdeMaximadeUsuarios;
            this.OcupaUsuario = ocupaUsuario;
            //this.Publico = publico;
            //this.Tipo = Tipo;
            //this.Frequencia = frequencia;
        }

    }
}
