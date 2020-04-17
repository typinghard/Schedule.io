using FluentValidation.Results;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Core.Helpers;
using Schedule.io.Validations.TipoEventoValidation;
using System.Linq;

namespace Schedule.io.Models.AggregatesRoots
{
    public class TipoEvento : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public TipoEvento(string nome, string descricao)
        {
            this.Nome = nome;
            this.Descricao = descricao;

            var resultadoValidacao = this.NovoTipoEventoEhValido();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(string.Join("## ", resultadoValidacao.Errors.Select(x => x.ErrorMessage)));
        }

        private TipoEvento()
        {

        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ScheduleIoException("Por favor, certifique-se que digitou um nome para o tipo do evento.");

            if (!nome.ValidarTamanho(2, 120))
                throw new ScheduleIoException("O nome do tipo do evento deve ter entre 2 e 120 caracteres.");

            this.Nome = nome;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!string.IsNullOrEmpty(descricao) && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException("A descrição deve ter entre 2 e 500 caracteres.");

            this.Descricao = descricao;
        }
        public bool TipoEventoEhValido()
        {
            return NovoTipoEventoEhValido().IsValid;
        }

        private ValidationResult NovoTipoEventoEhValido()
        {
            return new TipoEventoValidation().Validate(this);
        }
    }
}
