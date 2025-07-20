using AutoMapper;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;

namespace Purpura.MappingProfiles
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyViewModel>()
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Address))
                .AfterMap((src, dest) =>
                {
                    foreach(var employee in dest.CompanyEmployees)
                    {
                        employee.CompanyExternalReference = src.ExternalReference;
                    }
                });

            CreateMap<CompanyViewModel, Company>();
        }
    }
}
