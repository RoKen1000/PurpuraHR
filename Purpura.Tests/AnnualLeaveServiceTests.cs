using AutoFixture;
using AutoMapper;
using Moq;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services;
using Purpura.Services.Interfaces;
using System.Linq.Expressions;

namespace Purpura.Tests
{
    public class AnnualLeaveServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IAnnualLeaveService _annualLeaveService;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<IAnnualLeaveRepository> _annualLeaveRepositoryMock;
        private readonly Mock<IUserManagementRepository> _userManagementRepositoryMock;

        public AnnualLeaveServiceTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _annualLeaveRepositoryMock = new Mock<IAnnualLeaveRepository>();
            _userManagementRepositoryMock = new Mock<IUserManagementRepository>();

            _unitOfWorkMock.Setup(s => s.AnnualLeaveRepository).Returns(_annualLeaveRepositoryMock.Object);
            _unitOfWorkMock.Setup(s => s.UserManagementRepository).Returns(_userManagementRepositoryMock.Object);

            _annualLeaveService = new AnnualLeaveService(
                _mapperMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async void Edit_WithValidViewModel_ReturnsSuccessResult()
        {
            //arrange
            var externalReference = Guid.NewGuid().ToString();

            var annualLeaveViewModel = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, externalReference)
                .Create();

            var annualLeaveEntity = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, externalReference)
                .Create();

            _annualLeaveRepositoryMock.Setup(r => r.GetSingle(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    return func(annualLeaveEntity) ? annualLeaveEntity : null;
                });

            //act
            var editResult = await _annualLeaveService.Edit(annualLeaveViewModel);

            //assert
            Assert.True(editResult.IsSuccess == true);
        }

        [Fact]
        public async void Edit_WithInvalidDatesViewModel_ReturnsFailureResult()
        {
            //arrange
            //var annualLeaveViewModel = _fixture.Build<AnnualLeaveViewModel>()
            //    .With(p => p.StartDate, new DateTime(2025, 06, 15))
            //    .With(p => p.EndDate, new DateTime(2025, 06, 10))
            //    .Create();
            //var annualLeaveViewModel = new AnnualLeaveViewModel() ;
            //_annualLeaveServiceMock.Setup(s => s.Edit(It.IsAny<AnnualLeaveViewModel>())).ReturnsAsync(Result.Failure("End date can not be before the start date."));

            ////act
            //var editResult = await _annualLeaveService.Edit(annualLeaveViewModel);

            ////assert
            //Assert.True(editResult.IsSuccess == false);
        }
    }
}