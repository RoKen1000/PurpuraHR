using AutoMapper;
using Purpura.Models.Entities;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;

namespace Purpura.Services
{
    public class GoalService : BaseService<Goal>, IGoalService
    {
        public GoalService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
    }
}
