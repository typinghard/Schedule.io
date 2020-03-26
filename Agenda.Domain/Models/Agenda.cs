using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Core.Helpers;
using Agenda.Domain.Validations;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;


namespace Agenda.Domain.Models
{
    public class Agenda : Entity, IAggregateRoot
    {
        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public bool Publico { get; private set; }

        public Agenda(string titulo)
        {
            Titulo = titulo;

            var resultadoValidacao = this.NovaAgendaEhValida();
            if (!resultadoValidacao.IsValid)
                throw new DomainException(string.Join(", ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
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
            if (!descricao.ValidarTamanho(2, 500))
            {
                throw new ScheduleIoException(new List<string>() { "A descrição deve ter entre 2 e 500 caracteres." });
            }

            this.Descricao = descricao;
        }

        public void TornarAgendaPublica()
        {
            this.Publico = true;
        }

        public void TornarAgendaPrivado()
        {
            this.Publico = false;
        }

        public ValidationResult NovaAgendaEhValida()
        {
            return new NovaAgendaValidation().Validate(this);
        }

    }
}
