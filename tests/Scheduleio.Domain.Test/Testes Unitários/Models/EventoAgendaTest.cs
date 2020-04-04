using Bogus;
using Xunit;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Schedule.io.Core.Models;
using Schedule.io.Core.Core.DomainObjects;
using Schedule.io.Core.Enums;

namespace Schedule.io.Core.Test.Testes_Unitários.Models
{
    public class EventoAgendaTest
    {
        private EventoAgenda eventoAgenda;
        private TipoEvento tipoEvento;

        public EventoAgendaTest()
        {
            tipoEvento = new Faker<TipoEvento>("pt_BR")
                .CustomInstantiator((f) => new TipoEvento(f.Random.String(120, 'a', 'z'), f.Random.String(120, 'a', 'z')))
                .Generate(1)
                .First();

            eventoAgenda = new Faker<EventoAgenda>("pt_BR")
                .CustomInstantiator((f) => new EventoAgenda(f.Random.Guid().ToString(),
                                     f.Random.String(25, 'a', 'z'),
                                     f.Date.Soon(30),
                                     tipoEvento))
                .Generate(1)
                .First();
        }


        [Fact(DisplayName = "EventoAgenda - DefinirAgenda - Agenda deve ser alterado")]
        public void EventoAgenda_DefinirAgenda_AgendaDeveSerAlterada()
        {
            //Arrange
            var novaAgendaId = Guid.NewGuid().ToString();

            //Act
            eventoAgenda.DefinirAgenda(novaAgendaId);

            //Assert
            Assert.Equal(novaAgendaId, eventoAgenda.AgendaId);
        }
        [Fact(DisplayName = "EventoAgenda - DefinirAgenda - Agenda deve ser inválido por ser vazio")]
        public void EventoAgenda_DefinirAgenda_AgendaDeveSerInvalido()
        {
            //Arrange
            var novaAgendaId = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirAgenda(novaAgendaId));

            //Assert
            Assert.Equal("Por favor, certifique-se que escolheu uma agenda.", exception.Message);
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
        [Fact(DisplayName = "EventoAgenda - DefinirTitulo - Título deve ser invalido por ser vazio")]
        public void EventoAgenda_DefinirTitulo_TituloDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoTitulo = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirTitulo(novoTitulo));

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um título.", exception.Message);
        }
        [Fact(DisplayName = "EventoAgenda - DefinirTitulo - Título deve ser ser invalido pelo tamanho")]
        public void EventoAgenda_DefinirTitulo_TituloDeveSerAlteradoInvalidoPeloTamanho()
        {
            //Arrange
            var novoTitulo = new Faker().Random.String(151, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirTitulo(novoTitulo));

            //Assert
            Assert.Equal("O título deve ter entre 2 e 150 caracteres.", exception.Message);
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

        [Fact(DisplayName = "EventoAgenda - DefinirDescrição - Descrição deve ser invalido pelo tamanho")]
        public void EventoAgenda_DefinirDescricao_DescricaoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(501, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirDescricao(novaDescricao));

            //Assert
            Assert.Equal("A descrição deve ter entre 2 e 500 caracteres.", exception.Message);
        }

        [Fact(DisplayName = "EventoAgenda - AdicionarPessoa - Pessoa deve ser alterado")]
        public void EventoAgenda_AdicionarPessoa_PessoaDeveSerAlterado()
        {
            //Arrange
            var id = new Faker("pt_BR").Random.Guid().ToString();

            //Act
            eventoAgenda.AdicionarPessoa(id);

            //Assert
            Assert.Contains(eventoAgenda.Pessoas, x => x == id);
        }

        [Fact(DisplayName = "EventoAgenda - AdicionarPessoa - Pessoa deve ser invalido por ser vazio")]
        public void EventoAgenda_AdicionarPessoa_PessoaDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var id = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.AdicionarPessoa(id));

            //Assert
            Assert.Equal("Por favor, certifique-se que adicinou uma pessoa.", exception.Message);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirLocal - Local deve ser alterado")]
        public void EventoAgenda_DefinirLocal_LocalDeveSerAlterado()
        {
            //Arrange
            var novoLocalId = Guid.NewGuid().ToString();

            //Act
            eventoAgenda.DefinirLocal(novoLocalId);

            //Assert
            Assert.Equal(novoLocalId, eventoAgenda.Local);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirLocal - Local deve ser invalido por ser vazio")]
        public void EventoAgenda_DefinirLocal_LocalDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoLocalId = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirLocal(novoLocalId));

            //Assert
            Assert.Equal("Por favor, certifique-se que adicionou um local.", exception.Message);
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

        [Fact(DisplayName = "EventoAgenda - DefinirDatas - Datas de Inicio e Final deve ser alterado")]
        public void EventoAgenda_DefinirDatas_DatasInicioEFinalDeveSerInvalidoPorDaTaInicialSerInvalida()
        {
            //Arrange
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFinal = DateTime.Now.Date.AddDays(30);

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirDatas(dataInicio, dataFinal));

            //Assert
            Assert.Equal("Por favor, escolha a data e hora inicial do evento.", exception.Message);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirDatas - Datas Inválidas, data final maior que dara inicial")]
        public void EventoAgenda_DefinirDatas_DatasInvalidasDataFinalMaiorQueInicial()
        {
            //Arrange
            DateTime dataInicio = DateTime.Now.Date.AddDays(10);
            DateTime dataFinal = DateTime.Now.Date.AddDays(8);

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirDatas(dataInicio, dataFinal));

            //Assert
            Assert.Equal("Por certifique-se de que a data inicial é maior que a data final do evento.", exception.Message);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirDatas - Datas de Inicio deve ser alterado")]
        public void EventoAgenda_DefinirDatas_DatasInicialDeveSerAlterado()
        {
            //Arrange
            DateTime dataInicio = DateTime.Now.Date.AddDays(10);

            //Act
            eventoAgenda.DefinirDataInicial(dataInicio);

            //Assert
            Assert.Equal(dataInicio, eventoAgenda.DataInicio);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirDatas - Datas de Inicio deve ser inválido")]
        public void EventoAgenda_DefinirDatas_DatasInicioDeveSerInvalido()
        {
            //Arrange
            DateTime dataInicio = DateTime.MinValue;

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirDataInicial(dataInicio));

            //Assert
            Assert.Equal("Por favor, escolha a data e hora inicial do evento.", exception.Message);
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

        [Fact(DisplayName = "EventoAgenda - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser invalida por não ter valor")]
        public void EventoAgenda_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerInvalidaPorNaoTerValor()
        {
            //Arrange
            DateTime dataLimite = DateTime.MinValue;

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirDataLimiteConfirmacao(dataLimite));

            //Assert
            Assert.Equal("Por favor, certifique-se que informou uma data limite.", exception.Message);
        }

        [Fact(DisplayName = "EventoAgenda - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser invalida por ser menor que a DataInicio")]
        public void EventoAgenda_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerInvalidaPorSerMenorQueDataInicio()
        {
            //Arrange
            DateTime dataLimite = DateTime.Now.Date.AddDays(2);

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirDataLimiteConfirmacao(dataLimite));

            //Assert
            Assert.Equal("Por certifique-se de que a data limite é maior que a data inicio do evento.", exception.Message);
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

        [Fact(DisplayName = "EventoAgenda - DefinirQuantidadeMinimaDeUsuarios - Quantidade Mínima de Usuários deve ser invalido menor que 0")]
        public void EventoAgenda_DefinirQuantidadeMinimaDeUsuarios_QuantidadeMinimaDeUsuariosDeveSerInvalidoMenorQue0()
        {
            //Arrange
            int quantidadeMinimaDeUsuarios = -1;

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.DefinirQuantidadeMinimaDeUsuarios(quantidadeMinimaDeUsuarios));

            //Assert
            Assert.Equal("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0.", exception.Message);
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
            Assert.Equal(eventopublico, eventoAgenda.Publico);
        }

        [Fact(DisplayName = "EventoAgenda - TornarEventoPrivado - Evento Privado deve ser alterado")]
        public void EventoAgenda_TornarEventoPrivado_EventoPrivadoDeveSerAlterado1()
        {
            //Arrange
            var eventopublico = false;

            //Act
            eventoAgenda.TornarEventoPrivado();

            //Assert
            Assert.Equal(eventopublico, eventoAgenda.Publico);
        }


        [Fact(DisplayName = "EventoAgenda - DefinirFrequencia - Frequência do Evento deve ser alterado")]
        public void EventoAgenda_DefinirFrequencia_FrequenciaDoEventoDeveSerAlterado1()
        {
            //Arrange
            var novaFrequencia = new Faker().Random.Enum<EnumFrequencia>();

            //Act
            eventoAgenda.DefinirFrequencia(novaFrequencia);

            //Assert
            Assert.Equal(novaFrequencia, eventoAgenda.Frequencia);
        }

        [Fact(DisplayName = "EventoAgenda - TipoEvento - Definir Nome - Nome do Tipo Evento deve ser alterado")]
        public void EventoAgenda_TipoEvento_DefinirNome_NomeTiipoEventoDeveSerAlterado()
        {
            //Arrange
            var novoNome = "Nova nome do tipo evento";

            //Act
            eventoAgenda.Tipo.DefinirNome(novoNome);

            //Assert
            Assert.Equal(novoNome, eventoAgenda.Tipo.Nome);
        }

        [Fact(DisplayName = "EventoAgenda - TipoEvento - Definir Nome - Nome do Tipo Evento deve ser invalido por ser vazio")]
        public void EventoAgenda_TipoEvento_DefinirNome_NomeTiipoEventoDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoNome = "";

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.Tipo.DefinirNome(novoNome));

            //Assert
            Assert.Equal("Por favor, certifique-se que digitou um nome para o tipo do evento.", exception.Message);
        }

        [Fact(DisplayName = "EventoAgenda - TipoEvento - Definir Nome - Nome do Tipo Evento deve ser invalido por ser vazio")]
        public void EventoAgenda_TipoEvento_DefinirNome_NomeTiipoEventoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novoNome = new Faker().Random.String(1, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.Tipo.DefinirNome(novoNome));

            //Assert
            Assert.Equal("O nome do tipo do evento deve ter entre 2 e 120 caracteres.", exception.Message);
        }


        [Fact(DisplayName = "EventoAgenda - TipoEvento - DefinirDescricaoTipoEvento - Descrição do Tipo Evento deve ser alterado")]
        public void EventoAgenda_DefinirDescricaoTipoEvento_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = "Nova descrição do tipo evento";

            //Act
            eventoAgenda.Tipo.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, eventoAgenda.Tipo.Descricao);
        }


        [Fact(DisplayName = "EventoAgenda - TipoEvento - DefinirDescricaoTipoEvento - Descrição do Tipo Evento deve ser invalido pelo tamanho")]
        public void EventoAgenda_DefinirDescricaoTipoEvento_DescricaoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(501, 'a', 'z');

            //Act
            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda.Tipo.DefinirDescricao(novaDescricao));

            //Assert
            Assert.Equal("A descrição deve ter entre 2 e 500 caracteres.", exception.Message);
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

            var exception = Assert.Throws<ScheduleIoException>(() => eventoAgenda = new Faker<EventoAgenda>("pt_BR")
                                                                                    .CustomInstantiator((f) => new EventoAgenda("",
                                                                                    "", DateTime.MinValue, tipoEvento))
                                                                                    .Generate(1)
                                                                                    .First());

            //Act
            var validacao = exception.Message.Split("##").ToList();


            //Assert
            Assert.Contains(validacao, x => x.Contains("Id da Agenda não pode ser vazio!"));
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que digitou um título."));
            Assert.Contains(validacao, x => x.Contains("O título deve ter entre 2 e 150 caracteres."));
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que digitou um título."));
            Assert.Contains(validacao, x => x.Contains("'Data Inicio' deve ser informado."));
            Assert.Contains(validacao, x => x.Contains("Por favor, escolha a data e hora inicial do evento."));
            Assert.Contains(validacao, x => x.Contains("O Nome do Tipo do Evento deve ter entre 2 e 120 caracteres."));
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que digitou uma Descrição para o Tipo do Evento."));
            Assert.Contains(validacao, x => x.Contains("A Descrição do Tipo do Evento deve ter entre 2 e 500 caracteres."));
        }
    }
}
