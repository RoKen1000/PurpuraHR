using AutoMapper;
using Purpura.Models.Entities;

namespace Purpura.MappingProfiles
{
    public class CompanyEmployeeMappingProfile : Profile
    {
        public CompanyEmployeeMappingProfile()
        {
            CreateMap<CompanyEmployee, CompanyEmployeeMappingProfile>().ReverseMap();
        }
    }
}
