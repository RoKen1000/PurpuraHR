using AutoMapper;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;

namespace Purpura.Services
{
    public class GoalService : BaseService<Goal>, IGoalService
    {
        public GoalService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {

        }

        public async Task<Result> Create(GoalViewModel viewModel)
        {
             _unitOfWork.GoalRepository.Create(_mapper.Map<Goal>(viewModel));

            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
                return Result.Success();
            else
                return Result.Failure("Create failed.");
        }
    }
}
