using AutoMapper;
using Patients.Domain.Entities;

namespace Patients.Application.Models
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientModel>()
                .ForMember(t => t.Name, o => o.MapFrom(s => new PatientNameModel
                {
                    Id = s.Id,
                    Use = s.Use,
                    Family = s.Family,
                    Given = s.Given
                }));
        }
    }
}