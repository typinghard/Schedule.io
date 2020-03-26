using AutoMapper;
using Agenda.Application.ViewModels;
using Agenda.Domain.Models;
using ScheduleIo.Nuget.DTO;

namespace ScheduleIo.Nuget.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<NovaAgenda, Models.Agenda>();
        }
    }
}
