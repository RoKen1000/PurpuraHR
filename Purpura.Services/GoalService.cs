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

        public GoalService(IMapper mapper, 
            IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {

        }

        public async Task<Result> CreateAsync(GoalViewModel viewModel)
        {
            var newEntity = _mapper.Map<Goal>(viewModel);
            newEntity.ExternalReference = Guid.NewGuid().ToString();
            newEntity.DateCreated = DateTime.Now;

            _unitOfWork.GoalRepository.Create(newEntity);

            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
                return Result.Success();
            else
                return Result.Failure("Create failed.");
        }

        public async Task<Result> EditAsync(GoalViewModel viewModel)
        {
            var goalEntity = await _unitOfWork.GoalRepository.GetSingle(g => g.ExternalReference == viewModel.ExternalReference);

            if(goalEntity == null)
            {
                return Result.Failure("Entity not found");
            }

            _mapper.Map<GoalViewModel, Goal>(viewModel, goalEntity);
            _unitOfWork.GoalRepository.Update(goalEntity);

            var result = await _unitOfWork.SaveChangesAsync();

            if(result == 0)
            {
                return Result.Failure("Update failed.");
            }

            return Result.Success();
        }

        public async Task<List<GoalViewModel>> GetAllGoalsByUserIdAsync(string userId)
        {
            var goals = await _unitOfWork.GoalRepository.GetAll(g => g.UserId == userId);

            return _mapper.Map<List<GoalViewModel>>(goals);
        }

        public async Task<GoalViewModel?> GetByExternalReferenceAsync(string goalReference)
        {
            var goal = await _unitOfWork.GoalRepository.GetSingle(g => g.ExternalReference == goalReference);

            if(goal != null)
            {
                return _mapper.Map<GoalViewModel>(goal);
            }

            return null;
        }
    }
}
