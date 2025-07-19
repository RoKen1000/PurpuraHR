using AutoFixture;
using AutoMapper;
using Moq;
using Purpura.Common.Results;
using Purpura.MappingProfiles;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services;
using Purpura.Services.Interfaces;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Tests.ServiceTests
{
    public class GoalServiceTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<IGoalRepository> _goalRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IMapper _mapper;
        private readonly IGoalService _goalService;

        public GoalServiceTests()
        {
            _fixture = new Fixture();
            _goalRepositoryMock = new Mock<IGoalRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(a => a.GoalRepository).Returns(_goalRepositoryMock.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GoalMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _goalService = new GoalService(
                _mapper,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_DatabaseSaveFailure_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Create<GoalViewModel>();

            _goalRepositoryMock.Setup(a => a.Create(It.IsAny<Goal>()));
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync()).ReturnsAsync(Result.Failure("Database save failed."));

            //act
            var result = await _goalService.CreateAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database save failed.", result.Error);
            _goalRepositoryMock.Verify(a => a.Create(It.IsAny<Goal>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_DatabaseSaveSuccess_ReturnsSuccessResult()
        {
            //arrange
            var viewModel = _fixture.Create<GoalViewModel>();

            _goalRepositoryMock.Setup(a => a.Create(It.IsAny<Goal>()));
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync()).ReturnsAsync(Result.Success());

            //act
            var result = await _goalService.CreateAsync(viewModel);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            _goalRepositoryMock.Verify(a => a.Create(It.IsAny<Goal>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_EntityNotFound_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Create<GoalViewModel>();

            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync((Goal?)null);

            //act
            var result = await _goalService.DeleteAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Entity not found.", result.Error);
            _goalRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DatabaseSaveFail_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Build<GoalViewModel>().Create();
            var entity = _fixture.Build<Goal>().Create();

            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync(entity);
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync()).ReturnsAsync(Result.Failure("Database save failed."));

            //act
            var result = await _goalService.DeleteAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database save failed.", result.Error);
            _goalRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DatabaseSaveSuccess_ReturnsSuccessResult()
        {
            //arrange
            var viewModel = _fixture.Build<GoalViewModel>().Create();
            var entity = _fixture.Build<Goal>().Create();

            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync(entity);
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync()).ReturnsAsync(Result.Success());

            //act
            var result = await _goalService.DeleteAsync(viewModel);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            _goalRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditAsync_EntityNotFound_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Create<GoalViewModel>();

            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync((Goal?)null);

            //act
            var result = await _goalService.EditAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Entity not found.", result.Error);
            _goalRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task EditAsync_DatabaseSaveFail_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Build<GoalViewModel>().Create();
            var entity = _fixture.Create<Goal>();

            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync(entity);
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync()).ReturnsAsync(Result.Failure("Database save failed."));

            //act
            var result = await _goalService.EditAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database save failed.", result.Error);
            _goalRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditAsync_DatabaseSaveSuccess_ReturnsSuccessResult()
        {
            //arrange
            var externalReference = Guid.NewGuid().ToString();
            var viewModel = _fixture.Build<GoalViewModel>()
                .With(a => a.ExternalReference, externalReference)
                .Create();
            var entity = _fixture.Build<Goal>()
                .With(a => a.ExternalReference, externalReference)
                .Create();

            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync(entity);
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync()).ReturnsAsync(Result.Success());

            //act
            var result = await _goalService.EditAsync(viewModel);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            _goalRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllGoalsByUserIdAsync_NoGoalsFound_ReturnsEmptyList()
        {
            //arrange
            _goalRepositoryMock.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync(Enumerable.Empty<Goal>);

            //act
            var emptyList = await _goalService.GetAllGoalsByUserIdAsync(Guid.NewGuid().ToString());

            //assert
            Assert.NotNull(emptyList);
            Assert.IsAssignableFrom<IEnumerable<GoalViewModel>>(emptyList);
            Assert.IsType<List<GoalViewModel>>(emptyList);
            Assert.Empty(emptyList);
            _goalRepositoryMock.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllGoalsByUserIdAsync_GoalsFound_ReturnsPopulatedList()
        {
            //arrange
            var goalList = _fixture.CreateMany<Goal>(3);

            _goalRepositoryMock.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync(goalList);

            //act
            var populatedList = await _goalService.GetAllGoalsByUserIdAsync(Guid.NewGuid().ToString());

            //assert
            Assert.NotNull(populatedList);
            Assert.IsAssignableFrom<IEnumerable<GoalViewModel>>(populatedList);
            Assert.IsType<List<GoalViewModel>>(populatedList);
            Assert.NotEmpty(populatedList);
            Assert.Equal(3, populatedList.Count);
            _goalRepositoryMock.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Goal, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetByExternalReferenceAsync_NoEntityFound_ReturnsNull()
        {
            //arrange
            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
                .ReturnsAsync((Goal?)null);

            //act
            var nullResult = await _goalService.GetByExternalReferenceAsync(Guid.NewGuid().ToString());

            //assert
            Assert.Null(nullResult);
        }

        [Fact]
        public async Task GetByExternalReferenceAsync_EntityFound_ReturnsViewModel()
        {
            //arrange
            _goalRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Goal, bool>>>()))
               .ReturnsAsync(_fixture.Create<Goal>());

            //act
            var result = await _goalService.GetByExternalReferenceAsync(Guid.NewGuid().ToString());

            //assert
            Assert.NotNull(result);
            Assert.IsType<GoalViewModel>(result);
        }
    }
}
