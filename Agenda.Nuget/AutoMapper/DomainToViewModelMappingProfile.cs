using AutoMapper;
using ScheduleIo.Nuget.DTO;

namespace ScheduleIo.Nuget.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Models.Agenda, NovaAgenda>();
        }
    }
}