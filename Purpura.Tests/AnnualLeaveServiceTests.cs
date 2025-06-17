using AutoFixture;
using AutoMapper;
using Moq;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services;
using Purpura.Services.Interfaces;
using PurpuraWeb.Models.Entities;
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

        const string userId = "89F99893-34FB-4E34-AF37-60B1F711F7B6";
        const string annualLeaveExtRef = "38D0FE84-529D-48F5-8997-E501CBCE5A38";

        public AnnualLeaveServiceTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _annualLeaveRepositoryMock = new Mock<IAnnualLeaveRepository>();
            _userManagementRepositoryMock = new Mock<IUserManagementRepository>();

            _unitOfWorkMock.Setup(s => s.AnnualLeaveRepository).Returns(_annualLeaveRepositoryMock.Object);
            _unitOfWorkMock.Setup(s => s.UserManagementRepository).Returns(_userManagementRepositoryMock.Object);

            var annualLeaveEntity = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .Create();
            var userEntity = _fixture.Build<ApplicationUser>()
                .With(a => a.Id, userId)
                .With(a => a.AnnualLeaveDays, 10)
                .Create();


            _annualLeaveRepositoryMock.Setup(r => r.GetSingle(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    return func(annualLeaveEntity) ? annualLeaveEntity : null;
                });
            _userManagementRepositoryMock.Setup(r => r.GetSingle(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((Expression<Func<ApplicationUser, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    return func(userEntity) ? userEntity : null;
                });

            _annualLeaveService = new AnnualLeaveService(
                _mapperMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async void Edit_WithMissingOrInvalidAnnualLeaveExtRef_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveWithRandomRef = _fixture.Create<AnnualLeaveViewModel>();
            var annualLeaveWithNoRef = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, (string)null)
                .Create();

            //act
            var resultWithRandomRef = await _annualLeaveService.Edit(annualLeaveWithRandomRef);
            var resultWithNoRef = await _annualLeaveService.Edit(annualLeaveWithNoRef);

            //assert
            Assert.True(resultWithRandomRef.IsSuccess == false);
            Assert.Equal("Annual Leave not found.", resultWithRandomRef.Error);

            Assert.True(resultWithNoRef.IsSuccess == false);
            Assert.Equal("Annual Leave not found.", resultWithNoRef.Error);
        }

        [Fact]
        public async void Edit_WithInvalidUserUserId_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveViewModelWithRandomUserId = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .Create();
            var annualLeaveViewModelWithNoUserId = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, (string)null)
                .Create();

            //act
            var randomUserIdResult = await _annualLeaveService.Edit(annualLeaveViewModelWithRandomUserId);
            var noUserIdResult = await _annualLeaveService.Edit(annualLeaveViewModelWithNoUserId);

            //assert
            Assert.True(randomUserIdResult.IsSuccess == false);
            Assert.Equal("User not found.", randomUserIdResult.Error);

            Assert.True(noUserIdResult.IsSuccess == false);
            Assert.Equal("User not found.", randomUserIdResult.Error);
        }


        [Fact]
        public async void Edit_WithInvalidDates_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveWithEndBeforeStart = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 12))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();

            var annualLeaveWithSameStartAndEnd = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 17))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();

            //act
            var invalidDateResult = await _annualLeaveService.Edit(annualLeaveWithEndBeforeStart);
            var sameDayResult = await _annualLeaveService.Edit(annualLeaveWithSameStartAndEnd);

            //assert
            Assert.True(invalidDateResult.IsSuccess == false);
            Assert.True(sameDayResult.IsSuccess == false);
        }

        [Fact]
        public async void Edit_WithValidViewModel_ReturnsSuccessResult()
        {
            //arrange
            var annualLeaveViewModel = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .With(a => a.StartDate, new DateTime(2025, 6, 17))
                .With(a => a.EndDate, new DateTime(2025, 6, 20))
                .Create();

            _mapperMock.Setup(m => m.Map<AnnualLeaveViewModel, AnnualLeave>(It.IsAny<AnnualLeaveViewModel>(), It.IsAny<AnnualLeave>()))
                .Returns(new AnnualLeave());
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            //act
            var editResult = await _annualLeaveService.Edit(annualLeaveViewModel);

            //assert
            Assert.True(editResult.IsSuccess == true);
        }
    }
}