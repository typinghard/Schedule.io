using Bogus;
using Xunit;
using System.Linq;
using System;
using Schedule.io.Core.DomainObjects;
using Schedule.io.Models.AggregatesRoots;
using Schedule.io.Enums;
using Schedule.io.Models.ValueObjects;

namespace Schedule.io.Test.Testes_Unitários.Models.AggregatesRoots
{
    public class EventoTests
    {
        private Evento evento;

        public EventoTests()
        {
            evento = new Faker<Evento>("pt_BR")
                .CustomInstantiator((f) => new Evento(f.Random.Guid().ToString(),
                                     f.Random.Guid().ToString(),
                                     f.Random.String(25, 'a', 'z'),
                                     f.Date.Soon(30)))
                .Generate(1)
                .First();
        }


        [Fact(DisplayName = "Evento - DefinirAgenda - Agenda deve ser alterado")]
        public void Evento_DefinirAgenda_AgendaDeveSerAlterada()
        {
            //Arrange
            var novaAgendaId = Guid.NewGuid().ToString();

            //Act
            evento.DefinirAgenda(novaAgendaId);

            //Assert
            Assert.Equal(novaAgendaId, evento.AgendaId);
        }

        [Fact(DisplayName = "Evento - DefinirAgenda - Agenda deve ser inválido por ser vazio")]
        public void Evento_DefinirAgenda_AgendaDeveSerInvalido()
        {
            //Arrange
            var novaAgendaId = string.Empty;

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirAgenda(novaAgendaId)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que escolheu uma agenda."));
        }


        [Fact(DisplayName = "Evento - DefinirIdentificadorExterno - Identificador Externo deve ser alterado")]
        public void Evento_DefinirIdentificadorExterno_IdentificadorExternoDeveSerAlterado()
        {
            //Arrange
            var novoIdentificadorExterno = new Faker().Random.String(100, 'a', 'z');

            //Act
            evento.DefinirIdentificadorExterno(novoIdentificadorExterno);

            //Assert
            Assert.Equal(novoIdentificadorExterno, evento.IdentificadorExterno);
        }

        [Fact(DisplayName = "Evento - DefinirIdentificadorExterno - Identificador Externo deve ser alterado mesmo vazio")]
        public void Evento_DefinirIdentificadorExterno_IdentificadorExternoDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var novoIdentificadorExterno = string.Empty;

            //Act
            evento.DefinirIdentificadorExterno(novoIdentificadorExterno);

            //Assert
            Assert.Equal(novoIdentificadorExterno, evento.IdentificadorExterno);
        }


        [Fact(DisplayName = "Evento - DefinirTitulo - Título deve ser alterado")]
        public void Evento_DefinirTitulo_TituloDeveSerAlterado()
        {
            //Arrange
            var novoTitulo = new Faker().Random.String(150, 'a', 'z');

            //Act
            evento.DefinirTitulo(novoTitulo);

            //Assert
            Assert.Equal(novoTitulo, evento.Titulo);
        }

        [Fact(DisplayName = "Evento - DefinirTitulo - Título deve ser invalido por ser vazio")]
        public void Evento_DefinirTitulo_TituloDeveSerInvalidoPorSerVazio()
        {
            //Arrange
            var novoTitulo = string.Empty;

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirTitulo(novoTitulo)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que digitou um título."));

        }

        [Fact(DisplayName = "Evento - DefinirTitulo - Título deve ser ser invalido pelo tamanho")]
        public void Evento_DefinirTitulo_TituloDeveSerAlteradoInvalidoPeloTamanho()
        {
            //Arrange
            var novoTitulo = new Faker().Random.String(151, 'a', 'z');

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirTitulo(novoTitulo)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("O título deve ter entre 2 e 150 caracteres."));
        }

        [Fact(DisplayName = "Evento - DefinirDescrição - Descrição deve ser alterado")]
        public void Evento_DefinirDescricao_DescricaoDeveSerAlterado()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(500, 'a', 'z');

            //Act
            evento.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, evento.Descricao);
        }

        [Fact(DisplayName = "Evento - DefinirDescrição - Descrição deve ser alterado mesmo vazio")]
        public void Evento_DefinirDescricao_DescricaoDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var novaDescricao = string.Empty;

            //Act
            evento.DefinirDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, evento.Descricao);
        }

        [Fact(DisplayName = "Evento - DefinirDescrição - Descrição deve ser invalido pelo tamanho")]
        public void Evento_DefinirDescricao_DescricaoDeveSerInvalidoPeloTamanho()
        {
            //Arrange
            var novaDescricao = new Faker().Random.String(501, 'a', 'z');

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirDescricao(novaDescricao)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("A descrição deve ter entre 2 e 500 caracteres."));

        }

        [Fact(DisplayName = "Evento - AdicionarConvite - Convite deve ser adicionado")]
        public void Evento_AdicionarConvite_PessoaDeveSerAlterado()
        {
            //Arrange
            var convite = new Faker<Convite>("pt_BR")
                                .CustomInstantiator((f) => new Convite(evento.Id, f.Random.Guid().ToString()))
                                .Generate(1)
                                .First();

            //Act
            evento.AdicionarConvite(convite);

            //Assert
            Assert.Contains(evento.Convites, x => x.EventoId == convite.EventoId && x.UsuarioId == convite.UsuarioId);
        }

        [Fact(DisplayName = "Evento - RemoverConvite - Convite deve ser removido")]
        public void Evento_RemoverConvite_ConviteDeveSerRemovido()
        {
            //Arrange
            var convite = new Faker<Convite>("pt_BR")
                                .CustomInstantiator((f) => new Convite(evento.Id, f.Random.Guid().ToString()))
                                .Generate(1)
                                .First();

            evento.AdicionarConvite(convite);

            //Act
            evento.RemoverConvite(convite);

            //Assert
            Assert.DoesNotContain(evento.Convites, x => x.EventoId == convite.EventoId && x.UsuarioId == convite.UsuarioId);
        }

        [Fact(DisplayName = "Evento - RemoverConvite - Remoção do convite deve ser inválido")]
        public void Evento_RemoverConvite_RemocaoConviteDeveSerInvalido()
        {
            //Arrange
            var convite = new Faker<Convite>("pt_BR")
                                .CustomInstantiator((f) => new Convite(evento.Id, f.Random.Guid().ToString()))
                                .Generate(1)
                                .First();

            //Act
            evento.RemoverConvite(convite);

            //Assert
            Assert.DoesNotContain(evento.Convites, x => x.EventoId == convite.EventoId && x.UsuarioId == convite.UsuarioId);
        }

        [Fact(DisplayName = "Evento - DefinirLocal - Local deve ser alterado")]
        public void Evento_DefinirLocal_LocalDeveSerAlterado()
        {
            //Arrange
            var novoLocalId = Guid.NewGuid().ToString();

            //Act
            evento.DefinirLocal(novoLocalId);

            //Assert
            Assert.Equal(novoLocalId, evento.LocalId);
        }

        [Fact(DisplayName = "Evento - DefiniDefinirTipoEventorLocal - Local deve ser valido mesmo vazio")]
        public void Evento_DefinirLocal_LocalDeveSerValidoMesmo()
        {
            //Arrange
            var novoLocalId = string.Empty;

            //Act
            evento.DefinirLocal(novoLocalId);

            //Assert
            Assert.Equal(novoLocalId, evento.LocalId);
        }

        [Fact(DisplayName = "Evento - DefinirTipoEvento - TipoEventoId deve ser alterado")]
        public void Evento_DefinirTipoEvento_TipoEventoIdDeveSerAlterado()
        {
            //Arrange
            var tipoEventoId = Guid.NewGuid().ToString();

            //Act
            evento.DefinirTipoEvento(tipoEventoId);

            //Assert
            Assert.Equal(tipoEventoId, evento.IdTipoEvento);
        }

        [Fact(DisplayName = "Evento - DefinirTipoEvento - TipoEventoId deve ser alterado mesmo vazio")]
        public void Evento_DefinirTipoEvento_TipoEventoIdDeveSerAlteradoMesmoVazio()
        {
            //Arrange
            var tipoEventoId = string.Empty;

            //Act
            evento.DefinirTipoEvento(tipoEventoId);

            //Assert
            Assert.Equal(tipoEventoId, evento.IdTipoEvento);
        }

        [Fact(DisplayName = "Evento - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser alterado")]
        public void Evento_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerAlterado()
        {
            //Arrange
            DateTime dataLimite = evento.DataInicio.AddDays(5);

            //Act
            evento.DefinirDataLimiteConfirmacao(dataLimite);

            //Assert
            Assert.Equal(dataLimite, evento.DataLimiteConfirmacao);
        }

        [Fact(DisplayName = "Evento - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser valida por não ter valor")]
        public void Evento_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerValidaPorNaoTerValor()
        {
            //Arrange
            DateTime dataLimite = DateTime.MinValue;

            //Act
            evento.DefinirDataLimiteConfirmacao(dataLimite);

            //Assert
            Assert.Equal(dataLimite, evento.DataLimiteConfirmacao);
        }

        [Fact(DisplayName = "Evento - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser valida mesmo nulo")]
        public void Evento_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerValidaMesmoNulo()
        {
            //Arrange
            DateTime? dataLimite = null;

            //Act
            evento.DefinirDataLimiteConfirmacao(dataLimite);

            //Assert
            Assert.Equal(dataLimite, evento.DataLimiteConfirmacao);
        }

        [Fact(DisplayName = "Evento - DefinirDataLimiteConfirmacao - Data Limite Confirmação deve ser invalida por ser menor que a DataInicio")]
        public void Evento_DefinirDataLimiteConfirmacao_DataLimiteConfirmacaoDeveSerInvalidaPorSerMenorQueDataInicio()
        {
            //Arrange
            DateTime dataLimite = evento.DataInicio.AddDays(-5);

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirDataLimiteConfirmacao(dataLimite)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por certifique-se de que a data limite é maior que a data inicio do evento."));
        }

        [Fact(DisplayName = "Evento - DefinirDatas - Datas de Inicio e Final deve ser alterado")]
        public void Evento_DefinirDatas_DatasInicioEFinalDeveSerAlterado()
        {
            //Arrange
            DateTime dataInicio = DateTime.Now.Date.AddDays(1);
            DateTime dataFinal = DateTime.Now.Date.AddDays(30);

            //Act
            evento.DefinirDatas(dataInicio, dataFinal);

            //Assert
            Assert.Equal(dataInicio, evento.DataInicio);
            Assert.Equal(dataFinal, evento.DataFinal);
        }

        [Fact(DisplayName = "Evento - DefinirDatas - Datas Inválidas, data inicial inválida")]
        public void Evento_DefinirDatas_DatasInicioEFinalDeveSerInvalidoPorDaTaInicialSerInvalida()
        {
            //Arrange
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFinal = DateTime.Now.Date.AddDays(30);

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirDatas(dataInicio, dataFinal)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, escolha a data e hora inicial do evento."));
        }

        [Fact(DisplayName = "Evento - DefinirDatas - Datas Inválidas, data final maior que dara inicial")]
        public void Evento_DefinirDatas_DatasInvalidasDataFinalMaiorQueInicial()
        {
            //Arrange
            DateTime dataInicio = DateTime.Now.Date.AddDays(10);
            DateTime dataFinal = DateTime.Now.Date.AddDays(8);

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirDatas(dataInicio, dataFinal)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por certifique-se de que a data inicial é maior que a data final do evento."));
        }

        [Fact(DisplayName = "Evento - DefinirQuantidadeMinimaDeUsuarios - Quantidade Mínima de Usuários deve ser alterado")]
        public void Evento_DefinirQuantidadeMinimaDeUsuarios_QuantidadeMinimaDeUsuariosDeveSerAlterado()
        {
            //Arrange
            int quantidadeMinimaDeUsuarios = new Faker().Random.Int(0, 1000);

            //Act
            evento.DefinirQuantidadeMinimaDeUsuarios(quantidadeMinimaDeUsuarios);

            //Assert
            Assert.Equal(quantidadeMinimaDeUsuarios, evento.QuantidadeMinimaDeUsuarios);
        }

        [Fact(DisplayName = "Evento - DefinirQuantidadeMinimaDeUsuarios - Quantidade Mínima de Usuários deve ser invalido menor que 0")]
        public void Evento_DefinirQuantidadeMinimaDeUsuarios_QuantidadeMinimaDeUsuariosDeveSerInvalidoMenorQue0()
        {
            //Arrange
            int quantidadeMinimaDeUsuarios = new Faker().Random.Int(-1000, -1);

            //Act
            var validacao = Assert.Throws<ScheduleIoException>(() => evento.DefinirQuantidadeMinimaDeUsuarios(quantidadeMinimaDeUsuarios)).ScheduleIoMessages;

            //Assert
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se qua a quantidade mínima de usuários para o evento não é menor que 0."));
        }


        [Fact(DisplayName = "Evento - OcuparUsuario - Ocupar Usuário deve ser alterado")]
        public void Evento_OcuparUsuario_OcuparUsuarioDeveSerAlterado()
        {
            //Arrange
            var ocupar = true;

            //Act
            evento.OcuparUsuario();

            //Assert
            Assert.Equal(ocupar, evento.OcupaUsuario);
        }

        [Fact(DisplayName = "Evento - DesocuparUsuario - Desocupar Usuário deve ser alterado")]
        public void Evento_DesocuparUsuario_DesocuparUsuarioDeveSerAlterado()
        {
            //Arrange
            var ocupar = false;

            //Act
            evento.DesocuparUsuario();

            //Assert
            Assert.Equal(ocupar, evento.OcupaUsuario);
        }


        [Fact(DisplayName = "Evento - TornarEventoPublico - Evento Público deve ser alterado")]
        public void Evento_TornarEventoPublico_EventoPublicoDeveSerAlterado1()
        {
            //Arrange
            var eventopublico = true;

            //Act
            evento.TornarEventoPublico();

            //Assert
            Assert.Equal(eventopublico, evento.Publico);
        }

        [Fact(DisplayName = "Evento - TornarEventoPrivado - Evento Privado deve ser alterado")]
        public void Evento_TornarEventoPrivado_EventoPrivadoDeveSerAlterado1()
        {
            //Arrange
            var eventopublico = false;

            //Act
            evento.TornarEventoPrivado();

            //Assert
            Assert.Equal(eventopublico, evento.Publico);
        }


        [Fact(DisplayName = "Evento - DefinirFrequencia - Frequência do Evento deve ser alterado")]
        public void Evento_DefinirFrequencia_FrequenciaDoEventoDeveSerAlterado1()
        {
            //Arrange
            var novaFrequencia = new Faker().Random.Enum<EnumFrequencia>();

            //Act
            evento.DefinirFrequencia(novaFrequencia);

            //Assert
            Assert.Equal(novaFrequencia, evento.Frequencia);
        }



        [Fact(DisplayName = "Evento - Nova Agenda EhValida - Deve Ser Valido")]
        public void Evento_NovoEventoEhValido_DeveSerValido()
        {
            evento = new Faker<Evento>("pt_BR")
                         .CustomInstantiator((f) => new Evento(f.Random.Guid().ToString(),
                                              f.Random.Guid().ToString(),
                                              f.Random.String(25, 'a', 'z'),
                                              f.Date.Soon(30)))
                         .Generate(1)
                         .First();
        }

        [Fact(DisplayName = "Evento - Nova Agenda EhValida - Deve Ser Inválido")]
        public void Evento_NovoEventoEhValido_DeveSerInvalido()
        {

            var validacao = Assert.Throws<ScheduleIoException>(() => evento = new Faker<Evento>("pt_BR")
                                                                                    .CustomInstantiator((f) => new Evento("",
                                                                                    "", "", DateTime.MinValue))
                                                                                    .Generate(1)
                                                                                    .First()).ScheduleIoMessages;


            //Assert
            Assert.Contains(validacao, x => x.Contains("Id da Agenda não informado!"));
            Assert.Contains(validacao, x => x.Contains("Id do Usuario dono da agenda não informado!"));
            Assert.Contains(validacao, x => x.Contains("Por favor, certifique-se que digitou um Titulo."));
            Assert.Contains(validacao, x => x.Contains("O título deve ter entre 2 e 150 caracteres"));
            Assert.Contains(validacao, x => x.Contains("'Data Inicio' deve ser informado."));
            Assert.Contains(validacao, x => x.Contains("Por favor, escolha a data e hora inicial do evento."));
        }
    }
}