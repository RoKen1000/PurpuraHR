using AutoMapper;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories
{
    public class BookTimeOffRepository : IBookTimeOffRepository
    {
        private readonly IMapper _mapper;
        private readonly PurpuraDbContext _dbContext;

        public BookTimeOffRepository(IMapper mapper,
            PurpuraDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<bool> BookTimeOff(BookedTimeOffViewModel bookedTimePeriod)
        {
            try
            {
                var amountOfDays = (bookedTimePeriod.EndDate - bookedTimePeriod.StartDate).Days;
                var newEntities = new List<BookedTimeOff>();

                for(var i = amountOfDays; i != amountOfDays + 1; i++)
                {
                    var bookedTimeOffEntity = _mapper.Map<BookedTimeOff>(bookedTimePeriod);
                    bookedTimeOffEntity.DateCreated = DateTime.Now;
                    newEntities.Add(bookedTimeOffEntity);
                }

                _dbContext.AddRange(newEntities);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
