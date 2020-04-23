using Bogus;
using Xunit;
using System.Linq;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.ValueObjects;
using System;

namespace Schedule.io.Test.Testes_Unitários.Models.AggregatesRoots
{
    public class AgendaTests
    {
        private Agenda agenda;

        public AgendaTests()
        {
            agenda = new Faker<Agenda>("pt_BR")
                .CustomInstantiator((f) => new Agenda(f.Random.String(150, 'a', 'z'),
                                                      f.Random.String(150, 'a', 'z')))
                .Generate(1)
                .First();
        }

        [Fact(DisplayName = "Agenda - DefinirTítulo - Título deve ser alterado")]
        public void Agenda_DefinirTitulo_TituloDeveSerAlterado()
        {
            //Arrange
            var novoTitulo = new Faker().Random.String(150, 'a', 'z');

            //Act
            agenda.DefinirTitulo(novoTitulo);

            //Assert
            Assert.Equal(novoTitulo, agenda.Titulo);
        }

        [Fact(DisplayName = "Agenda - DefinirTítulo - Título deve ser inválido por ser vazio")]
        public void Agenda_DefinirTitulo_TituloDeveSerInValidoPorSerVazio()
        {
            //Arrange
            var novoTituloInvalido = string.Empty;

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => agenda.DefinirTitulo(novoTituloInvalido)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("O título não pode ser vazio e deve ter entre 2 e 150 caracteres."));
        }

        [Fact(DisplayName = "Agenda - DefinirTítulo - Título deve ser inválido pelo tamanho")]
        public void Agenda_DefinirTitulo_TituloDeveSerInValidoPeloTamanho()
        {
            //Arrange
            var novoTitulo = new Faker().Random.String(151, 'a', 'z');


            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => agenda.DefinirTitulo(novoTitulo)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("O título não pode ser vazio e deve ter entre 2 e 150 caracteres."));
        }

        [Fact(DisplayName = "Agenda - DefinirDescricao - Descrição deve ser alterada")]
        public void Agenda_DefinirDescricao_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novoDescricao = new Faker().Random.String(500, 'a', 'z');

            //Act
            agenda.DefinirDescricao(novoDescricao);

            //Assert
            Assert.Equal(novoDescricao, agenda.Descricao);
        }

        [Fact(DisplayName = "Agenda - DefinirDescricao - Descrição deve ser alterada mesmo vazio")]
        public void Agenda_DefinirDescricao_DescricaoDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var novoDescricao = string.Empty;

            //Act
            agenda.DefinirDescricao(novoDescricao);

            //Assert
            Assert.Equal(novoDescricao, agenda.Descricao);
        }

        [Fact(DisplayName = "Agenda - DefinirDescricao - Descrição deve ser inválido pelo tamanho")]
        public void Agenda_DefinirDescricao_DescricaoDeveSerInValidoPeloTamanho()
        {
            //Arrange
            var novaDescricaoInvalida = new Faker().Random.String(501, 'a', 'z');

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => agenda.DefinirDescricao(novaDescricaoInvalida)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("A descrição deve ter entre 2 e 500 caracteres."));
        }

        [Fact(DisplayName = "Agenda - TornarAgendaPublica - Agenda Deve Ser Pública")]
        public void Agenda_TornarAgendaPublica_AgendaDeveSerPublica()
        {
            //Act
            agenda.TornarAgendaPublica();

            //Assert
            Assert.True(agenda.Publico);
        }

        [Fact(DisplayName = "Agenda - TornarAgendaPrivada - Agenda Deve Ser Privada")]
        public void Agenda_TornarAgendaPrivada_AgendaDeveSerPrivada()
        {
            //Act
            agenda.TornarAgendaPrivado();

            //Assert
            Assert.False(agenda.Publico);
        }

        [Fact(DisplayName = "Agenda - AdicionarAgendaDoUsuario - AgendaUsuario deve ser adicionado")]
        public void Agenda_AdicionarAgendaDoUsuario_AgendaUsuarioDeveSerAdicionado()
        {
            //Arrange
            var agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
                                                .CustomInstantiator((f) => new AgendaUsuario(agenda.UsuarioIdCriador))
                                                .Generate(1)
                                                .First();

            //Act
            agenda.AdicionarUsuario(agendaUsuario);

            //Assert
            Assert.Contains(agenda.Usuarios, x => x.AgendaId == agendaUsuario.AgendaId && x.UsuarioId == agendaUsuario.UsuarioId);
        }

        [Fact(DisplayName = "Agenda - AdicionarAgendaDoUsuario - AgendaUsuario deve ser inválido")]
        public void Agenda_AdicionarAgendaDoUsuario_AgendaUsuarioDeveSerInvalido()
        {
            //Arrange
            var validacao = Assert.Throws<ScheduleIoException>(() => new Faker<AgendaUsuario>("pt_BR")
                                                                .CustomInstantiator((f) => new AgendaUsuario(""))
                                                                .Generate(1)
                                                                .First()).ScheduleIoMessages;



            //Assert
            Assert.Contains(validacao, x => x.Contains("UsuarioId da Agenda do Usuario não informado."));
        }


        [Fact(DisplayName = "Agenda - AdicionarAgendaDoUsuario - AgendaUsuario deve ser removido")]
        public void Agenda_AdicionarAgendaDoUsuario_AgendaUsuarioDeveSerRemovido()
        {
            var agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
                                        .CustomInstantiator((f) => new AgendaUsuario(agenda.UsuarioIdCriador))
                                        .Generate(1)
                                        .First();

            agenda.AdicionarUsuario(agendaUsuario);

            //Act
            agenda.RemoverUsuario(agendaUsuario);

            //Assert
            Assert.DoesNotContain(agenda.Usuarios, x => x.AgendaId == agendaUsuario.AgendaId && x.UsuarioId == agendaUsuario.UsuarioId);
        }

        [Fact(DisplayName = "Agenda - AdicionarAgendaDoUsuario - Remoção da AgendaUsuario deve ser inválido")]
        public void Agenda_AdicionarAgendaDoUsuario_RemocaoAgendaUsuarioDeveSerInvalido()
        {
            var agendaUsuario = new Faker<AgendaUsuario>("pt_BR")
                                        .CustomInstantiator((f) => new AgendaUsuario(agenda.UsuarioIdCriador))
                                        .Generate(1)
                                        .First();
            //Act
            agenda.RemoverUsuario(agendaUsuario);

            //Assert
            Assert.DoesNotContain(agenda.Usuarios, x => x.AgendaId == agendaUsuario.AgendaId && x.UsuarioId == agendaUsuario.UsuarioId);
        }


        [Fact(DisplayName = "Agenda - NovaAgendaEhValida - Deve Ser Valido")]
        public void Agenda_NovaAgendaEhValida_DeveSerValido()
        {
            agenda = new Faker<Agenda>("pt_BR")
                 .CustomInstantiator((f) => new Agenda(f.Random.String(150, 'a', 'z'),
                                                       f.Random.String(150, 'a', 'z')))
                 .Generate(1)
                 .First();
        }

        [Fact(DisplayName = "Agenda - NovaAgendaEhValida - Deve Ser Inválido")]
        public void Agenda_NovaAgendaEhValida_DeveSerInvalido()
        {
            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => agenda = new Faker<Agenda>("pt_BR")
                                                             .CustomInstantiator((f) => new Agenda("", ""))
                                                             .Generate(1)
                                                             .First()).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Titulo não informado."));
            Assert.Contains(validacao, x => x.Contains("UsuarioIdCriador não informado."));
        }
    }
}
