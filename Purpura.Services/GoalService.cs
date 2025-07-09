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
        private readonly IUserManagementRepository _userManagementRepository;

        public GoalService(IMapper mapper, 
            IUnitOfWork unitOfWork,
            IUserManagementRepository userManagementRepository) : base(mapper, unitOfWork)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task<Result> Create(GoalViewModel viewModel)
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

        public async Task<List<GoalViewModel>> GetAllGoalsByUserId(string userId)
        {
            var goals = await _unitOfWork.GoalRepository.GetAll(g => g.UserId == userId);

            return _mapper.Map<List<GoalViewModel>>(goals);
        }
    }
}
