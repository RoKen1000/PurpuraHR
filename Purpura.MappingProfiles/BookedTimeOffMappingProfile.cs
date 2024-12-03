using AutoMapper;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.MappingProfiles
{
    public class BookedTimeOffMappingProfile : Profile
    {
        public BookedTimeOffMappingProfile()
        {
            CreateMap<BookedTimeOffViewModel, BookedTimeOff>();
            CreateMap<BookedTimeOff, BookedTimeOffViewModel>();
        }
    }
}
