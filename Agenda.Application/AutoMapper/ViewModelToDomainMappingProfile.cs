using AutoMapper;
using Agenda.Application.ViewModels;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;

namespace Agenda.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CriarAgendaViewModel, Agenda.Domain.Models.Agenda>();
            CreateMap<AtualizarAgendaViewModel, Agenda.Domain.Models.Agenda>();
            CreateMap<DeletarAgendaViewModel, Agenda.Domain.Models.Agenda>();
            CreateMap<DetalhesAgendaViewModel, Agenda.Domain.Models.Agenda>();

            CreateMap<CriarUsuarioViewModel, Usuario>();
            CreateMap<AtualizarUsuarioViewModel, Usuario>();
            CreateMap<DeletarUsuarioViewModel, Usuario>();
            CreateMap<DetalhesUsuarioViewModel, Usuario>();

            CreateMap<CriarAgendaUsuarioViewModel, AgendaUsuario>();
            CreateMap<AtualizarAgendaUsuarioViewModel, AgendaUsuario>();
            CreateMap<DeletarAgendaUsuarioViewModel, AgendaUsuario>();
            CreateMap<DetalhesAgendaUsuarioViewModel, AgendaUsuario>();

            CreateMap<CriarEventoAgendaViewModel, EventoAgenda>();
            CreateMap<AtualizarEventoAgendaViewModel, EventoAgenda>();
                //.ConstructUsing(c => new EventoAgenda(c.Titulo, c.Descricao, c.Pessoas, c.Endereco, c.DataInicio, c.DataFinal, TipoEvento.Outros));
            CreateMap<DeletarEventoAgendaViewModel, EventoAgenda>();
            CreateMap<DetalhesEventoAgendaViewModel, EventoAgenda>();


            CreateMap<CriarConviteViewModel, Convite>();
            CreateMap<AtualizarConviteViewModel, Convite>();
            CreateMap<DeletarConviteViewModel, Convite>();
            CreateMap<DetalhesConviteViewModel, Convite>();


            CreateMap<CriarLocalViewModel, Local>();
            CreateMap<AtualizarLocalViewModel, Local>();
            CreateMap<DeletarLocalViewModel, Local>();
            CreateMap<DetalhesLocalViewModel, Local>();           
        }
    }
}
