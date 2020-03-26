using AutoMapper;
using Agenda.Application.ViewModels;
using Agenda.Domain.Core.DomainObjects;
using Agenda.Domain.Models;

namespace Agenda.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Models.Agenda, DetalhesAgendaViewModel>();
            CreateMap<Domain.Models.Agenda, CriarAgendaViewModel>();
            CreateMap<Domain.Models.Agenda, AtualizarAgendaViewModel>();
            CreateMap<Domain.Models.Agenda, DeletarAgendaViewModel>();

            CreateMap<Usuario, DetalhesUsuarioViewModel>();
            CreateMap<Usuario, CriarUsuarioViewModel>();
            CreateMap<Usuario, AtualizarUsuarioViewModel>();
            CreateMap<Usuario, DeletarUsuarioViewModel>();

            CreateMap<AgendaUsuario, DetalhesAgendaUsuarioViewModel>();
            CreateMap<AgendaUsuario, CriarAgendaUsuarioViewModel>();
            CreateMap<AgendaUsuario, AtualizarAgendaUsuarioViewModel>();
            CreateMap<AgendaUsuario, DeletarAgendaUsuarioViewModel>();

            CreateMap<EventoAgenda, DetalhesEventoAgendaViewModel>();
            CreateMap<EventoAgenda, CriarEventoAgendaViewModel>();
            CreateMap<EventoAgenda, AtualizarEventoAgendaViewModel>();
            CreateMap<EventoAgenda, DeletarEventoAgendaViewModel>();

            CreateMap<Convite, DetalhesConviteViewModel>();
            CreateMap<Convite, CriarConviteViewModel>();
            CreateMap<Convite, AtualizarConviteViewModel>();
            CreateMap<Convite, DeletarConviteViewModel>();

            CreateMap<Local, DetalhesLocalViewModel>();
            CreateMap<Local, CriarLocalViewModel>();
            CreateMap<Local, AtualizarLocalViewModel> ();
            CreateMap<Local, DeletarLocalViewModel>();
        }
    }
}
