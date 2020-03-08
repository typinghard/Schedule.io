using Agenda.Domain.Models;
using Bogus;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Agenda.Domain.Enums;

namespace Agenda.Domain.Test
{
    public class EventoAgendaTest
    {
        private EventoAgenda eventoAgenda;
        private TipoEvento tipoEvento;

        public EventoAgendaTest()
        {
            tipoEvento = new Faker<TipoEvento>("pt_BR")
                .CustomInstantiator((t) => new TipoEvento(t.Random.String(120, 'a', 'z'), t.Random.String(500, 'a', 'z')))
                .Generate(1)
                .First();


            //var guidList = Enumerable.Range(1, 5)
            //              .Select(_ => new Faker("pt_BR").Random.Guid())
            //              .ToList();

            List<Guid> guidList = new List<Guid>();

            eventoAgenda = new Faker<EventoAgenda>("pt_BR")
                .CustomInstantiator((f) => new EventoAgenda(f.Random.Guid(),
                                     f.Random.String(25, 'a', 'z'),
                                     f.Random.String(120, 'a', 'z'),
                                     f.Random.String(500, 'a', 'z'),
                                     f.Random.ListItems<Guid>(guidList),
                                     f.Random.Guid(),
                                     f.Date.Recent(1),
                                     f.Date.Soon(30),
                                     f.Date.Soon(25),
                                     f.Random.Int(0),
                                     f.Random.Bool(),
                                     f.Random.Bool(),
                                     tipoEvento,
                                     f.Random.Enum<EnumFrequencia>()))
                .Generate(1)
                .First();
        }


        [Fact(DisplayName = "EventoAgenda - DefinirAgenda - Agenda deve ser alterado")]
        public void EventoAgenda_DefinirAgenda_AgendaDeveSerAlterada()
        {
            //Arrange
            var novaAgendaId = Guid.NewGuid();

            //Act
            eventoAgenda.DefinirAgenda(novaAgendaId);

            //Assert
            Assert.Equal(novaAgendaId, eventoAgenda.AgendaId);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirIdentificadorExterno - Identificador Externo deve ser alterado")]
        public void EventoAgenda_DefinirIdentificadorExterno_IdentificadorExternoDeveSerAlterado()
        {
            //Arrange
            var novoIdentificadorExterno = "podeserqualquerconvertidoemstring";

            //Act
            eventoAgenda.DefinirIdentificadorExterno(novoIdentificadorExterno);

            //Assert
            Assert.Equal(novoIdentificadorExterno, eventoAgenda.IdentificadorExterno);
        }


        [Fact(DisplayName = "EventoAgenda - DefinirTitulo - Título deve ser alterado")]
        public void EventoAgenda_DefinirTitulo_TituloDeveSerAlterado()
        {
            //Arrange
            var novoTitulo = "Nova título do evento da agenda";

            //Act
            eventoAgenda.DefinirTitulo(novoTitulo);

            //Assert
            Assert.Equal(novoTitulo, eventoAgenda.Titulo);
        }


        [Fact(DisplayName = "EventoAgenda - DefinirDescrição - Descrição deve ser alterado")]
        public void EventoAgenda_DefinirDescricao_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = "Nova descrição do evento agenda";

            //Act
            eventoAgenda.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, eventoAgenda.Descricao);
        }

        [Fact(DisplayName = "EventoAgenda - AdicionarPessoa - Pessoa deve ser alterado")]
        public void EventoAgenda_AdicionarPessoa_PessoaDeveSerAlterado()
        {
            //Arrange
            IList<Guid> guidList = Enumerable.Range(1, 2)
               .Select(_ => new Faker("pt_BR").Random.Guid())
               .ToList();

            //Act
            foreach (Guid item in guidList)
                eventoAgenda.AdicionarPessoa(item);

            //Assert
            Assert.Equal(guidList, eventoAgenda.Pessoas);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirLocal - Local deve ser alterado")]
        public void EventoAgenda_DefinirLocal_LocalDeveSerAlterado()
        {
            //Arrange
            Guid novoLocalId = Guid.NewGuid();

            //Act
            eventoAgenda.DefinirLocal(novoLocalId);

            //Assert
            Assert.Equal(novoLocalId, eventoAgenda.Local);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirDatas - Datas de Inicio e Final deve ser alterado")]
        public void EventoAgenda_DefinirDatas_DatasInicioEFinalDeveSerAlterado()
        {
            //Arrange
            DateTime dataInicio = DateTime.Now.Date.AddDays(1);
            DateTime dataFinal = DateTime.Now.Date.AddDays(30);

            //Act
            eventoAgenda.DefinirDatas(dataInicio, dataFinal);

            //Assert
            Assert.Equal(dataInicio, eventoAgenda.DataInicio);
            Assert.Equal(dataFinal, eventoAgenda.DataFinal);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser alterado")]
        public void EventoAgenda_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerAlterado()
        {
            //Arrange
            DateTime dataLimite = DateTime.Now.Date.AddDays(25);

            //Act
            eventoAgenda.DefinirDataLimiteConfirmacao(dataLimite);

            //Assert
            Assert.Equal(dataLimite, eventoAgenda.DataLimiteConfirmacao);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirQuantidadeMinimaDeUsuarios - Quantidade Mínima de Usuários deve ser alterado")]
        public void EventoAgenda_DefinirQuantidadeMinimaDeUsuarios_QuantidadeMinimaDeUsuariosDeveSerAlterado()
        {
            //Arrange
            int quantidadeMinimaDeUsuarios = 2;

            //Act
            eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(quantidadeMinimaDeUsuarios);

            //Assert
            Assert.Equal(quantidadeMinimaDeUsuarios, eventoAgenda.QuantidadeMinimaDeUsuarios);
        }

        [Fact(DisplayName = "EventoAgenda - OcuparUsuario - Ocupar Usuário deve ser alterado")]
        public void EventoAgenda_OcuparUsuario_OcuparUsuarioDeveSerAlterado1()
        {
            //Arrange
            var ocupar = true;

            //Act
            eventoAgenda.OcuparUsuario();

            //Assert
            Assert.Equal(ocupar, eventoAgenda.OcupaUsuario);
        }

        [Fact(DisplayName = "EventoAgenda - DesocuparUsuario - Desocupar Usuário deve ser alterado")]
        public void EventoAgenda_DesocuparUsuario_DesocuparUsuarioDeveSerAlterado1()
        {
            //Arrange
            var ocupar = false;

            //Act
            eventoAgenda.DesocuparUsuario();

            //Assert
            Assert.Equal(ocupar, eventoAgenda.OcupaUsuario);
        }


        [Fact(DisplayName = "EventoAgenda - TornarEventoPublico - Evento Público deve ser alterado")]
        public void EventoAgenda_TornarEventoPublico_EventoPublicoDeveSerAlterado1()
        {
            //Arrange
            var eventopublico = true;

            //Act
            eventoAgenda.TornarEventoPublico();

            //Assert
            Assert.Equal(eventopublico, eventoAgenda.EventoPublico);
        }

        [Fact(DisplayName = "EventoAgenda - TornarEventoPrivado - Evento Privado deve ser alterado")]
        public void EventoAgenda_TornarEventoPrivado_EventoPrivadoDeveSerAlterado1()
        {
            //Arrange
            var eventopublico = false;

            //Act
            eventoAgenda.TornarEventoPrivado();

            //Assert
            Assert.Equal(eventopublico, eventoAgenda.EventoPublico);
        }


        [Fact(DisplayName = "EventoAgenda - DefinirFrequencia - Frequência do Evento deve ser alterado")]
        public void EventoAgenda_DefinirFrequencia_FrequenciaDoEventoDeveSerAlterado1()
        {
            //Arrange
            var novaFrequencia = EnumFrequencia.TodosOsMeses;

            //Act
            eventoAgenda.DefinirFrequencia(novaFrequencia);

            //Assert
            Assert.Equal(novaFrequencia, eventoAgenda.EnumFrequencia);
        }

        [Fact(DisplayName = "EventoAgenda - TipoEvento - Definir Nome - Nome do Tipo Evento deve ser alterado")]
        public void EventoAgenda_TipoEvento_DefinirNome_NomeTiipoEventoDeveSerAlterado()
        {
            //Arrange
            var novoNome = "Nova nome do tipo evento";

            //Act
            eventoAgenda.TipoEvento.DefinirNome(novoNome);

            //Assert
            Assert.Equal(novoNome, eventoAgenda.TipoEvento.Nome);
        }

        [Fact(DisplayName = "EventoAgenda - TipoEvento - DefinirDescricaoTipoEvento - Descrição do Tipo Evento deve ser alterado")]
        public void EventoAgenda_DefinirDescricaoTipoEvento_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = "Nova descrição do tipo evento";

            //Act
            eventoAgenda.TipoEvento.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, eventoAgenda.TipoEvento.Descricao);
        }


        [Fact(DisplayName = "EventoAgenda - Nova Agenda EhValida - Deve Ser Valido")]
        public void EventoAgenda_NovoEventoAgendaEhValido_DeveSerValido()
        {
            //Act
            var ehValido = eventoAgenda.NovoEventoAgendaEhValido().IsValid;

            //Assert
            Assert.True(ehValido);
        }

        [Fact(DisplayName = "EventoAgenda - Nova Agenda EhValida - Deve Ser Inválido")]
        public void EventoAgenda_NovoEventoAgendaEhValido_DeveSerInvalido()
        {
            //Arrange
            tipoEvento = new Faker<TipoEvento>("pt_BR")
                .CustomInstantiator((t) => new TipoEvento(t.Random.String(121, 'a', 'z'), ""))
                .Generate(1)
                .First();

            eventoAgenda = new Faker<EventoAgenda>("pt_BR")
                .CustomInstantiator((f) => new EventoAgenda(f.Random.Guid(),
                                     "",
                                     "",
                                     "",
                                     null,
                                     null,
                                     DateTime.MinValue,
                                     null,
                                     null,
                                     -550,
                                     false,
                                     false,
                                     tipoEvento,
                                     EnumFrequencia.NaoRepete))
                .Generate(1)
                .First();

            //Act
            var ehValido = eventoAgenda.NovoEventoAgendaEhValido().IsValid;

            //Assert
            Assert.False(ehValido);
        }
    }
}
