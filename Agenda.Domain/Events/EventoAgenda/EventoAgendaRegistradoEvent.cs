﻿using Agenda.Domain.Core.Messages;
using Agenda.Domain.Enums;
using Agenda.Domain.Models;
using System;
using System.Collections.Generic;


namespace Agenda.Domain.Events
{
    public class EventoAgendaRegistradoEvent : Event
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<Guid> Usuarios { get; set; }
        public Guid? Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataLimiteConfirmacao { get; set; }
        public int QuantidadeMinimaDeUsuarios { get; set; }
        public bool OcupaUsuario { get; set; }
        public bool Publico { get; set; }
        public TipoEvento Tipo { get; set; }
        public EnumFrequencia Frequencia { get; set; }


        public EventoAgendaRegistradoEvent(Guid id, Guid agendaId, string identificadorExterno, string titulo, string descricao, IList<Guid> usuarios, Guid? local, DateTime dataInicio, DateTime? dataFinal, DateTime? dataLimiteConfirmacao, int qtdeMaximadeUsuarios, bool ocupaUsuario, bool publico, TipoEvento tipoEvento, EnumFrequencia frequencia)
        {
            this.Id = id;
            this.AggregateId = id;
            this.AgendaId = agendaId;
            this.IdentificadorExterno = identificadorExterno;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Usuarios = usuarios;
            this.Local = local;
            this.DataInicio = dataInicio;
            this.DataFinal = dataFinal;
            this.DataLimiteConfirmacao = dataLimiteConfirmacao;
            this.QuantidadeMinimaDeUsuarios = qtdeMaximadeUsuarios;
            this.OcupaUsuario = ocupaUsuario;
            this.Publico = publico;
            this.Tipo = Tipo;
            this.Frequencia = frequencia;
        }
    }
}
