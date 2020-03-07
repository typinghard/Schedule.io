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
            CreateMap<Agenda.Domain.Models.Agenda, DetalhesAgendaViewModel>();
            CreateMap<Agenda.Domain.Models.Agenda, CriarAgendaViewModel>();
            CreateMap<Agenda.Domain.Models.Agenda, AtualizarAgendaViewModel>();
            CreateMap<Agenda.Domain.Models.Agenda, DeletarAgendaViewModel>();

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

            CreateMap<EventoUsuario, DetalhesEventoUsuarioViewModel>();
            CreateMap<EventoUsuario, CriarEventoUsuarioViewModel>();
            CreateMap<EventoUsuario, AtualizarEventoUsuarioViewModel>();
            CreateMap<EventoUsuario, DeletarEventoUsuarioViewModel>();

            CreateMap<Local, DetalhesLocalViewModel>();
            CreateMap<Local, CriarLocalViewModel>();
            CreateMap<Local, AtualizarLocalViewModel> ();
            CreateMap<Local, DeletarLocalViewModel>();
        }
    }
}
