using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Validations.TipoEventoValidation;
using System.Linq;

namespace Schedule.io.Models.AggregatesRoots
{
    public class TipoEvento : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public TipoEvento(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;

            var resultadoValidacao = NovoTipoEventoEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(resultadoValidacao.Errors.Select(x => x.ErrorMessage).ToList());
        }

        private TipoEvento() { }

        public void DefinirNome(string nome)
        {
            if (!nome.ValidarTamanho(2, 120))
                throw new ScheduleIoException("O nome do tipo do evento deve ter entre 2 e 120 caracteres.");

            Nome = nome;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.EhVazio() && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException("A descrição deve ter entre 2 e 500 caracteres.");

            Descricao = descricao;
        }

        private ValidationResult NovoTipoEventoEhValido()
        {
            return new TipoEventoValidation().Validate(this);
        }
    }
}
