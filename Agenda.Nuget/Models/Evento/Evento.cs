using Agenda.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class Evento
    {
        public Guid Id { get; internal set; }
        public DateTime CriadoAs { get; internal set; }
        public DateTime AtualizadoAs { get; internal set; }
        public Guid AgendaId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<Convite> Convites { get;  set; }
        public Guid? Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataLimiteConfirmacao { get; set; }
        public int QuantidadeMinimaDeUsuarios { get; set; }
        public bool OcupaUsuario { get; set; }
        public bool Publico { get; set; }
        public TipoEvento Tipo { get; set; }
        public EnumFrequencia Frequencia { get; set; }
    }

    public class TipoEvento
    {
        public Guid Id { get; internal set; }
        public DateTime CriadoAs { get; internal set; }
        public DateTime AtualizadoAs { get; internal set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class Convite
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public Guid UsuarioId { get; set; }
        public EnumStatusConviteEvento Status { get; set; }
        public PermissoesConvite Permissoes { get; set; }

    }


    public class PermissoesConvite
    {
        public Guid Id { get; set; }
        public bool ModificaEvento { get; set; }
        public bool ConvidaUsuario { get; set; }
        public bool VeListaDeConvidados { get; set; }
    }
}
