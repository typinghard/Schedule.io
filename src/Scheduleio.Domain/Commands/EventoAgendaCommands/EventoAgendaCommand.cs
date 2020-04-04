﻿using Schedule.io.Core.Core.Messages;
using Schedule.io.Core.Enums;
using Schedule.io.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.io.Core.Commands.EventoAgendaCommands
{
    public class EventoAgendaCommand : Command
    {
        public string Id { get; protected set; }
        public string AgendaId { get; protected set; }
        public string UsuarioId { get; protected set; }
        public string IdentificadorExterno { get; protected set; }
        public string Titulo { get; protected set; }
        public string Descricao { get; protected set; }
        public IList<Convite> Convites { get; protected set; }
        public string LocalId { get; protected set; }
        public DateTime DataInicio { get; protected set; }
        public DateTime? DataFinal { get; protected set; }
        public DateTime? DataLimiteConfirmacao { get; protected set; }
        public int QuantidadeMinimaDeUsuarios { get; protected set; }
        public bool OcupaUsuario { get; protected set; }
        public bool Publico { get; protected set; }
        public TipoEvento Tipo { get; protected set; }
        public EnumFrequencia Frequencia { get; protected set; }
    }
}