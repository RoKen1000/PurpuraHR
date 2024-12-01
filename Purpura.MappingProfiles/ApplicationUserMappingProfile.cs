using AutoMapper;
using Purpura.Models.ViewModels;
using PurpuraWeb.Models.Entities;

namespace Purpura.MappingProfiles
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}
