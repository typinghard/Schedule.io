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
        public string IdentificadorExterno { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public bool Publico { get; private set; }
        public string UsuarioIdCriador { get; private set; }
        public bool Editavel { get; private set; }
        public List<AgendaUsuario> Usuarios { get; private set; }
        public List<string> Eventos { get; private set; }

        public Agenda(string idUsuarioDono, string titulo)
        {
            UsuarioIdCriador = idUsuarioDono;
            Titulo = titulo;
            Usuarios = new List<AgendaUsuario>();
            Eventos = new List<string>();

            var resultadoValidacao = NovaAgendaEhValida();
            if (!resultadoValidacao.IsValid)
                throw new ScheduleIoException(resultadoValidacao.Errors.Select(x => x.ErrorMessage).ToList());

            Inicializar();
        }

        private Agenda()
        {
            Usuarios = new List<AgendaUsuario>();
            Eventos = new List<string>();
        }

        private void Inicializar()
        {
            TornarEditavel();
            AdicionarUsuarioCriador();
        }

        public void DefinirTitulo(string titulo)
        {
            if (!titulo.ValidarTamanho(2, 150))
                throw new ScheduleIoException(new List<string>() { "O título não pode ser vazio e deve ter entre 2 e 150 caracteres." });

            Titulo = titulo;
        }

        public void DefinirDescricao(string descricao)
        {
            if (!descricao.EhVazio() && !descricao.ValidarTamanho(2, 500))
                throw new ScheduleIoException(new List<string>() { "A descrição deve ter entre 2 e 500 caracteres." });

            Descricao = descricao;
        }

        public void DefinirUsuarioIdCriador(string usuarioId)
        {
            if (usuarioId.EhVazio())
                throw new ScheduleIoException(new List<string>() { "Por favor, certifique-se que digitou um usuarioId." });

            UsuarioIdCriador = usuarioId;
        }

        public void DefinirIdentificadorExterno(string identificadorId)
        {
            if (identificadorId.EhVazio())
                throw new ScheduleIoException(new List<string>() { "Por favor, certifique-se que digitou um Identificador Externo." });

            IdentificadorExterno = identificadorId;
        }

        public void TornarAgendaPublica()
        {
            Publico = true;
        }

        public void TornarAgendaPrivado()
        {
            Publico = false;
        }

        public void TornarEditavel()
        {
            Editavel = true;
        }

        public void NaoTornarEditavel()
        {
            Editavel = false;
        }

        public void AdicionarUsuario(AgendaUsuario usuario)
        {
            usuario.AssociarAgenda(Id);
            usuario.AgendaUsuarioEhValido();
            Usuarios.Add(usuario);
        }

        public void RemoverUsuario(AgendaUsuario usuario)
        {
            Usuarios.RemoveAll(x => x.UsuarioId == usuario.UsuarioId);
        }

        private ValidationResult NovaAgendaEhValida()
        {
            return new AgendaValidation().Validate(this);
        }

        private void AdicionarUsuarioCriador()
        {
            if (Usuarios.Any(x => x.UsuarioId == UsuarioIdCriador))
                return;

            var agendaUsuario = new AgendaUsuario(UsuarioIdCriador);
            AdicionarUsuario(agendaUsuario);
        }
    }
}
