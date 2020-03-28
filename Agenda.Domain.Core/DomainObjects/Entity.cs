using System;

namespace Agenda.Domain.Core.DomainObjects
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime CriadoAs { get; protected set; }
        public DateTime AtualizadoAs { get; protected set; }

        public bool Inativo { get; protected set; }

        public Entity(string id)
        {
            Id = id;
        }

        public void Inativar()
        {
            DefinirDataAtualizacao();
            Inativo = true;
        }

        public void DefinirDataCriacao()
        {
            CriadoAs = AtualizadoAs = DateTime.Now;
        }
        public void DefinirDataAtualizacao()
        {
            AtualizadoAs = DateTime.Now;
        }
    }
}
