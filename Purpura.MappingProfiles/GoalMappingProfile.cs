using AutoMapper;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;

namespace Purpura.MappingProfiles
{
    public class GoalMappingProfile : Profile
    {
        public GoalMappingProfile()
        {
            CreateMap<Goal, GoalViewModel>().ReverseMap();
        }
    }
}
