using FluentValidation;
using System;

namespace Agenda.Domain.Core.DomainObjects
{
    public abstract class EntityValidation<T> : AbstractValidator<T> where T : Entity
    {
        public EntityValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id não informado");

            RuleFor(c => c.CriadoAs)
                .Must(DataSuperiorAMinima)
                .WithMessage("Data de Criação não informada");

            RuleFor(c => c.AtualizadoAs)
                .Must(DataSuperiorAMinima)
                .WithMessage("Data de Atualização não informado");
        }

        protected static bool DataSuperiorAMinima(DateTime data)
        {
            return data >= DateTime.MinValue;
        }
    }
}
