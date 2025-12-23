using AutoMapper;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;

namespace Purpura.MappingProfiles
{
    public class CompanyEmployeeMappingProfile : Profile
    {
        public CompanyEmployeeMappingProfile()
        {
            CreateMap<CompanyEmployee, CompanyEmployeeViewModel>();
            CreateMap<CompanyEmployeeViewModel, CompanyEmployee>()
                .ForMember(dest => dest.Company, src => src.Ignore());
        }
    }
}
