
using ScheduleIo.Nuget.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class NovoEvento
    {
        public Guid AgendaId { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        //public IList<Guid> Usuarios { get; set; }
        public IList<NovoConvite> Convites { get; set; }
        public Guid? Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public DateTime? DataLimiteConfirmacao { get; set; }
        public int QuantidadeMinimaDeUsuarios { get; set; }
        public bool OcupaUsuario { get; set; }
        public bool Publico { get; set; }
        public NovoTipoEvento Tipo { get; set; }
        public EnumFrequencia Frequencia { get; set; }

        public NovoEvento(Guid agendaId, string identificadorExterno, string titulo, string descricao,
                          IList<NovoConvite> convites, Guid? local,
                          DateTime dataInicio, DateTime dataFinal, DateTime dataLimiteConfirmacao,
                          int quantidadeMinimaUsuarios, bool ocupaUsuario, bool publico,
                          NovoTipoEvento tipo, EnumFrequencia frequencia)
        {
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
    public class NovoTipoEvento
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public NovoTipoEvento(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }

    public class NovoConvite
    {
        public Guid EventoId { get; set; }
        public Guid UsuarioId { get; set; }
        public EnumStatusConviteEvento Status { get; set; }
        public NovoPermissoesConvite Permissoes { get; set; }

        public NovoConvite(Guid usuarioId, NovoPermissoesConvite permissoesConvite)
        {
            UsuarioId = usuarioId;
            Status = EnumStatusConviteEvento.Aguardando_Confirmacao;
            Permissoes = permissoesConvite;
        }
    }


    public class NovoPermissoesConvite
    {
        public bool ModificaEvento { get; set; }
        public bool ConvidaUsuario { get; set; }
        public bool VeListaDeConvidados { get; set; }

        public NovoPermissoesConvite(bool modificaEvento, bool convidaUsuario, bool veListaDeConvidados)
        {
            ModificaEvento = modificaEvento;
            ConvidaUsuario = convidaUsuario;
            VeListaDeConvidados = veListaDeConvidados;
        }

    }
}
