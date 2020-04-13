using System;
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
        public IReadOnlyCollection<AgendaUsuario> Usuarios { get { return _usuarios; } }
        private List<AgendaUsuario> _usuarios;
        public IReadOnlyCollection<string> Eventos { get { return _eventos; } }
        private List<string> _eventos;

        public Agenda(string id, string titulo) : base(id)
        {
            Titulo = titulo;

            var resultadoValidacao = this.AgendaEhValida();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        public void DefinirTitulo(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
            {
                throw new ScheduleIoException(new List<string>() { "Por favor, certifique-se que digitou um título." });
            }

            if (!titulo.ValidarTamanho(2, 150))
            {
                throw new ScheduleIoException(new List<string>() { "O título deve ter entre 2 e 150 caracteres." });

            }

            this.Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao) && !descricao.ValidarTamanho(2, 500))
            {
                throw new ScheduleIoException(new List<string>() { "A descrição deve ter entre 2 e 500 caracteres." });
            }

            this.Descricao = descricao;
        }

        public void DefinirUsuarioIdCriador(string usuarioId)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                throw new ScheduleIoException(new List<string>() { "Por favor, certifique-se que digitou um usuarioId." });
            }

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

        private ValidationResult AgendaEhValida()
        {
            return new AgendaValidation().Validate(this);
        }
    }
}
