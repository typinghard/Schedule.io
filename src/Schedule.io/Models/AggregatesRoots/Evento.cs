using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Enums;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Validations.EventoAgendaValidations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Evento : Entity, IAggregateRoot
    {
        public string AgendaId { get; private set; }
        public string UsuarioIdCriador { get; private set; }
        public string IdTipoEvento { get; private set; }
        public string IdentificadorExterno { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public IReadOnlyCollection<Convite> Convites { get { return _convites; } }
        private List<Convite> _convites { get; set; }
        public string LocalId { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFinal { get; private set; }
        public DateTime? DataLimiteConfirmacao { get; private set; }
        public int QuantidadeMinimaDeUsuarios { get; private set; }
        public bool OcupaUsuario { get; private set; }
        public bool Publico { get; private set; }
        public EnumFrequencia Frequencia { get; private set; }

        public Evento(string agendaId, string usuarioIdCriador, string titulo, DateTime dataInicio)
        {
            AgendaId = agendaId;
            UsuarioIdCriador = usuarioIdCriador;
            Titulo = titulo;
            DataInicio = dataInicio;
            Frequencia = EnumFrequencia.Nao_Repete;

            _convites = new List<Convite>();

            var resultadoValidacao = NovoEventoEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join("## ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));

            AdicionarConviteDoDono();
        }

        private Evento()
        {
            _convites = new List<Convite>();
        }

        public void DefinirAgenda(string agendaId)
        {
            if (agendaId.EhVazio())
                throw new ScheduleIoException("Por favor, certifique-se que escolheu uma agenda.");

            AgendaId = agendaId;
        }

        public void DefinirIdentificadorExterno(string idExterno)
        {
            IdentificadorExterno = idExterno;
        }

        public void DefinirTitulo(string titulo)
        {
            if (!titulo.ValidarTamanho(2, 150))
                throw new ScheduleIoException("O título não pode ser vazio e deve ter entre 2 e 150 caracteres.");

            Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.EhVazio() && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException("A descrição deve ter entre 2 e 500 caracteres.");

            Descricao = descricao;
        }

        public void AdicionarConvite(Convite convite)
        {
            convite.ConviteEhValido();
            _convites.Add(convite);
        }

        public void RemoverConvite(Convite convite)
        {
            foreach (var c in _convites)
                if (c.EventoId == convite.EventoId && c.UsuarioId == convite.UsuarioId)
                {
                    _convites.Remove(convite);
                    break;
                }
        }

        public void DefinirLocal(string localId)
        {
            LocalId = localId;
        }

        public void DefinirTipoEvento(string tipoEventoId)
        {
            IdTipoEvento = tipoEventoId;
        }

        public void DefinirDatas(DateTime dataInicio, DateTime? dataFinal = null)
        {
            if (dataInicio == DateTime.MinValue)
                throw new ScheduleIoException("Por favor, escolha a data e hora inicial do evento.");

            if (dataFinal.HasValue && dataFinal < dataInicio)
                throw new ScheduleIoException("Por certifique-se de que a data inicial é maior que a data final do evento.");

            DataInicio = dataInicio;
            DataFinal = dataFinal;
        }

        public void DefinirDataLimiteConfirmacao(DateTime? dataLimiteConfirmacao)
        {
            if ((dataLimiteConfirmacao.HasValue && dataLimiteConfirmacao.Value != DateTime.MinValue) && dataLimiteConfirmacao < DataInicio)
                throw new ScheduleIoException("Por certifique-se de que a data limite é maior que a data inicio do evento.");

            DataLimiteConfirmacao = dataLimiteConfirmacao;
        }

        public void DefinirQuantidadeMinimaDeUsuarios(int quantidadeMinimaDeUsuarios)
        {
            if (quantidadeMinimaDeUsuarios < 0)
                throw new ScheduleIoException("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.");

            QuantidadeMinimaDeUsuarios = quantidadeMinimaDeUsuarios;
        }

        public void OcuparUsuario()
        {
            OcupaUsuario = true;
        }

        public void DesocuparUsuario()
        {
            OcupaUsuario = false;
        }

        public void TornarEventoPublico()
        {
            Publico = true;
        }

        public void TornarEventoPrivado()
        {
            Publico = false;
        }

        public void DefinirFrequencia(EnumFrequencia frequencia)
        {
            Frequencia = frequencia;
        }

        private ValidationResult NovoEventoEhValido()
        {
            return new EventoValidation().Validate(this);
        }

        private void AdicionarConviteDoDono()
        {
            Convite convite = Convites.FirstOrDefault(x => x.UsuarioId == UsuarioIdCriador);
            if (convite == null)
                convite = new Convite(UsuarioIdCriador);

            convite.AtualizarStatusConvite(EnumStatusConviteEvento.Sim);
            convite.Permissoes.PodeConvidar();
            convite.Permissoes.PodeModificarEvento();
            convite.Permissoes.PodeVerListaDeConvidados();

            if (!Convites.Any(x => x.UsuarioId == UsuarioIdCriador))
                AdicionarConvite(convite);
        }
    }


}


