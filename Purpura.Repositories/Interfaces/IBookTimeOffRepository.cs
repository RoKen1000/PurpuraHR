using Purpura.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories.Interfaces
{
    public interface IBookTimeOffRepository
    {
        Task<bool> BookTimeOff(BookedTimeOffViewModel bookedTimePeriod);
    }
}
