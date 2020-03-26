using ScheduleIo.Nuget.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class AtualizarEvento
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<AtualizarConvite> Convites { get; set; }
        public Guid? Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataLimiteConfirmacao { get; set; }
        public int QuantidadeMinimaDeUsuarios { get; set; }
        public bool OcupaUsuario { get; set; }
        public bool Publico { get; set; }
        public AtualizarTipoEvento Tipo { get; set; }
        public EnumFrequencia Frequencia { get; set; }

        public AtualizarEvento(Guid id, Guid agendaId, string identificadorExterno, string titulo, string descricao,
                          IList<AtualizarConvite> convites, Guid? local,
                          DateTime dataInicio, DateTime dataFinal, DateTime dataLimiteConfirmacao,
                          int quantidadeMinimaUsuarios, bool ocupaUsuario, bool publico,
                          AtualizarTipoEvento tipo, EnumFrequencia frequencia)
        {
            Id = id;
            AgendaId = agendaId;
            IdentificadorExterno = identificadorExterno;
            Titulo = titulo;
            Descricao = descricao;
            Convites = convites;
            Local = local;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            DataLimiteConfirmacao = dataLimiteConfirmacao;
            QuantidadeMinimaDeUsuarios = quantidadeMinimaUsuarios;
            OcupaUsuario = ocupaUsuario;
            Publico = publico;
            Tipo = tipo;
            Frequencia = frequencia;
        }
    }

    public class AtualizarTipoEvento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public AtualizarTipoEvento(Guid id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }

    public class AtualizarConvite
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public Guid UsuarioId { get; set; }
        public EnumStatusConviteEvento Status { get; set; }
        public AtualizarPermissoesConvite Permissoes { get; set; }

        public AtualizarConvite(Guid id, Guid eventoId, Guid usuarioId, AtualizarPermissoesConvite permissoesConvite)
        {
            Id = id;
            EventoId = eventoId;
            UsuarioId = usuarioId;
            Status = EnumStatusConviteEvento.Aguardando_Confirmacao;
            Permissoes = permissoesConvite;
        }
    }


    public class AtualizarPermissoesConvite
    {
        public Guid Id { get; set; }
        public bool ModificaEvento { get; set; }
        public bool ConvidaUsuario { get; set; }
        public bool VeListaDeConvidados { get; set; }

        public AtualizarPermissoesConvite(bool modificaEvento, bool convidaUsuario, bool veListaDeConvidados)
        {
            ModificaEvento = modificaEvento;
            ConvidaUsuario = convidaUsuario;
            VeListaDeConvidados = veListaDeConvidados;
        }
    }
}
