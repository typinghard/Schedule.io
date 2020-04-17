using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Models.ValueObjects;
using Schedule.io.Validations.AgendaValidations;

namespace Schedule.io.Models.AggregatesRoots
{
    public class Agenda : Entity, IAggregateRoot
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public bool Publico { get; private set; }
        public string UsuarioIdCriador { get; private set; }
        public IReadOnlyCollection<AgendaUsuario> AgendasUsuarios { get { return _agendasUsuarios; } }
        private List<AgendaUsuario> _agendasUsuarios;
        public IReadOnlyCollection<string> Eventos { get { return _eventos; } }
        private List<string> _eventos;

        public Agenda(string idUsuarioDono, string titulo)
        {
            UsuarioIdCriador = idUsuarioDono;
            Titulo = titulo;
            _agendasUsuarios = new List<AgendaUsuario>();
            _eventos = new List<string>();

            var resultadoValidacao = this.NovaAgendaEhValida();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join("## ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));

            var agendaUsuario = new AgendaUsuario(Id, idUsuarioDono);
            AdicionarAgendaDoUsuario(agendaUsuario);
        }

        private Agenda()
        {
            _agendasUsuarios = new List<AgendaUsuario>();
            _eventos = new List<string>();
        }

        public void DefinirTitulo(string titulo)
        {
            if (titulo.EhVazio())
                throw new ScheduleIoException(new List<string>() { "Por favor, certifique-se que digitou um título." });

            if (!titulo.ValidarTamanho(2, 150))
                throw new ScheduleIoException(new List<string>() { "O título deve ter entre 2 e 150 caracteres." });

            this.Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.EhVazio() && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException(new List<string>() { "A descrição deve ter entre 2 e 500 caracteres." });

            this.Descricao = descricao;
        }

        public void DefinirUsuarioIdCriador(string usuarioId)
        {
            if (usuarioId.EhVazio())
                throw new ScheduleIoException(new List<string>() { "Por favor, certifique-se que digitou um usuarioId." });

            this.UsuarioIdCriador = usuarioId;
        }

        public void TornarAgendaPublica()
        {
            this.Publico = true;
        }

        public void TornarAgendaPrivado()
        {
            this.Publico = false;
        }

        public void AdicionarAgendaDoUsuario(AgendaUsuario agendaUsuario)
        {
            agendaUsuario.AgendaUsuarioEhValido();
            _agendasUsuarios.Add(agendaUsuario);
        }

        public void RemoverAgendasDoUsuario(AgendaUsuario agendaUsuario)
        {
            foreach (var au in _agendasUsuarios)
                if (au.AgendaId == agendaUsuario.AgendaId && au.UsuarioId == agendaUsuario.UsuarioId)
                {
                    _agendasUsuarios.Remove(agendaUsuario);
                    break;
                }
        }

        public bool AgendaEhValida()
        {
            return NovaAgendaEhValida().IsValid;
        }

        private ValidationResult NovaAgendaEhValida()
        {
            return new AgendaValidation().Validate(this);
        }
    }
}
