using AutoFixture;
using AutoMapper;
using Moq;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.MappingProfiles;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Services;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Tests.ServiceTests
{
    public class AnnualLeaveServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IAnnualLeaveService _annualLeaveService;
        private readonly IMapper _mapper;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<IAnnualLeaveRepository> _annualLeaveRepositoryMock;
        private readonly Mock<IUserManagementRepository> _userManagementRepositoryMock;

        const string userId = "89F99893-34FB-4E34-AF37-60B1F711F7B6";
        const string annualLeaveExtRef = "38D0FE84-529D-48F5-8997-E501CBCE5A38";

        public AnnualLeaveServiceTests()
        {
            _fixture = new Fixture();
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

            _annualLeaveRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    return func(annualLeaveEntity) ? annualLeaveEntity : null;
                });
            _userManagementRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((Expression<Func<ApplicationUser, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    return func(userEntity) ? userEntity : null;
                });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AnnualLeaveMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _annualLeaveService = new AnnualLeaveService(
                _mapper,
                _unitOfWorkMock.Object);
        }

        #region EditAsync

        [Fact]
        public async void EditAsync_WithMissingOrInvalidAnnualLeaveExtRef_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveWithRandomRef = _fixture.Create<AnnualLeaveViewModel>();
            var annualLeaveWithNoRef = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, (string?)null)
                .Create();

            //act
            var resultWithRandomRef = await _annualLeaveService.EditAsync(annualLeaveWithRandomRef);
            var resultWithNoRef = await _annualLeaveService.EditAsync(annualLeaveWithNoRef);

            //assert
            Assert.True(resultWithRandomRef.IsSuccess == false);
            Assert.Equal("Annual Leave not found.", resultWithRandomRef.Error);

            Assert.True(resultWithNoRef.IsSuccess == false);
            Assert.Equal("Annual Leave not found.", resultWithNoRef.Error);
        }

        [Fact]
        public async void EditAsync_WithInvalidUserUserId_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveViewModelWithRandomUserId = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .Create();
            var annualLeaveViewModelWithNoUserId = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, (string?)null)
                .Create();

            //act
            var randomUserIdResult = await _annualLeaveService.EditAsync(annualLeaveViewModelWithRandomUserId);
            var noUserIdResult = await _annualLeaveService.EditAsync(annualLeaveViewModelWithNoUserId);

            //assert
            Assert.True(randomUserIdResult.IsSuccess == false);
            Assert.Equal("User not found.", randomUserIdResult.Error);

            Assert.True(noUserIdResult.IsSuccess == false);
            Assert.Equal("User not found.", randomUserIdResult.Error);
        }


        [Fact]
        public async void EditAsync_WithInvalidDates_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveWithEndBeforeStart = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 12))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();

            var noDaysRef = Guid.NewGuid().ToString();
            var withDaysRef = Guid.NewGuid().ToString();
            var userEntityWithNoDays = _fixture.Build<ApplicationUser>()
                .With(a => a.Id, noDaysRef)
                .With(a => a.AnnualLeaveDays, 0)
                .Create();
            var userEntityWithDays = _fixture.Build<ApplicationUser>()
                .With(a => a.Id, withDaysRef)
                .With(a => a.AnnualLeaveDays, 5)
                .Create();
            var userEntity = _fixture.Build<ApplicationUser>()
                .With(a => a.Id, userId)
                .With(a => a.AnnualLeaveDays, 10)
                .Create();

            var validAnnualLeaveForNoDays = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 27))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, noDaysRef)
                .Create();
            var validAnnualLeaveExceedingDays = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 27))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, withDaysRef)
                .Create();
            var annualLeaveWithInvalidDatesAndExceedingDays = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 12))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, noDaysRef)
                .Create();

            _userManagementRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((Expression<Func<ApplicationUser, bool>> predicate) =>
                {
                    var func = predicate.Compile();

                    if (func(userEntityWithNoDays))
                        return userEntityWithNoDays;
                    if (func(userEntityWithDays))
                        return userEntityWithDays;
                    if (func(userEntity))
                        return userEntity;

                    return null;
                });

            //act
            var invalidDateResult = await _annualLeaveService.EditAsync(annualLeaveWithEndBeforeStart);
            var noDaysResult = await _annualLeaveService.EditAsync(validAnnualLeaveForNoDays);
            var exceedsDaysResult = await _annualLeaveService.EditAsync(validAnnualLeaveExceedingDays);
            var noDaysAndInvalidDatesResult = await _annualLeaveService.EditAsync(annualLeaveWithInvalidDatesAndExceedingDays);

            //assert
            Assert.True(invalidDateResult.IsSuccess == false);
            Assert.Equal("End date can not be before the start date.", invalidDateResult.Error);
            Assert.True(noDaysResult.IsSuccess == false);
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take.", noDaysResult.Error);
            Assert.True(exceedsDaysResult.IsSuccess == false);
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take.", exceedsDaysResult.Error);
            Assert.True(noDaysAndInvalidDatesResult.IsSuccess == false);
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take. End date can not be before the start date.", noDaysAndInvalidDatesResult.Error);
        }

        [Fact]
        public async void EditAsync_WithValidViewModel_ReturnsSuccessResult()
        {
            //arrange
            var annualLeaveViewModel = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .With(a => a.StartDate, new DateTime(2025, 6, 17))
                .With(a => a.EndDate, new DateTime(2025, 6, 20))
                .Create();

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var editResult = await _annualLeaveService.EditAsync(annualLeaveViewModel);

            //assert
            Assert.True(editResult.IsSuccess == true);
            Assert.Null(editResult.Error);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void EditAsync_WhenUpdateFails_ReturnsFalureResult()
        {
            //arrange
            var annualLeaveViewModel = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .With(a => a.StartDate, new DateTime(2025, 6, 17))
                .With(a => a.EndDate, new DateTime(2025, 6, 20))
                .Create();

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(Result.Failure("Database save failed."));


            //act
            var editResult = await _annualLeaveService.EditAsync(annualLeaveViewModel);

            //assert
            Assert.True(editResult.IsSuccess == false);
            Assert.Equal("Database save failed.", editResult.Error);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        #endregion

        #region CreateAsync

        [Fact]
        public async void CreateAsync_WithInvalidUser_ReturnsFailureResult()
        {
            var annualLeaveWithRandomUserRef = _fixture.Create<AnnualLeaveViewModel>();
            var annualLeaveWithNoUserRef = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.UserId, (string?)null)
                .Create();

            var randomUserResult = await _annualLeaveService.CreateAsync(annualLeaveWithRandomUserRef);
            var noUserRefResult = await _annualLeaveService.CreateAsync(annualLeaveWithNoUserRef);

            Assert.True(randomUserResult.IsSuccess == false);
            Assert.Equal("User not found.", randomUserResult.Error);
            Assert.True(noUserRefResult.IsSuccess == false);
            Assert.Equal("User not found.", noUserRefResult.Error);
        }

        [Fact]
        public async void CreateAsync_WithInvalidLeaveDays_ReturnsFailureResult()
        {
            //arrange
            var annualLeaveWithEndBeforeStart = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 12))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();
            var annualLeaveExceedingUserLeaveTotal = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 15))
                .With(a => a.EndDate, new DateTime(2025, 06, 30))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();

            //act
            var invalidDateResult = await _annualLeaveService.CreateAsync(annualLeaveWithEndBeforeStart);
            var exceededTotalResult = await _annualLeaveService.CreateAsync(annualLeaveExceedingUserLeaveTotal);

            //assert
            //Truncated tests for results. See Edit method tests for full testing of error messages.
            Assert.False(invalidDateResult.IsSuccess);
            Assert.False(exceededTotalResult.IsSuccess);
        }

        [Fact]
        public async void CreateAsync_WithValidAnnualLeave_AssignsProprtiesAndReturnsSuccessResult()
        {
            //arrange
            var validAnnualLeave = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 20))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();
            var userEntity = _fixture.Build<ApplicationUser>()
                .With(a => a.Id, userId)
                .With(a => a.AnnualLeaveDays, 10)
                .Create();

            var currentDateTime = DateTime.Now;

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var validResult = await _annualLeaveService.CreateAsync(validAnnualLeave);

            //assert
            _annualLeaveRepositoryMock.Verify(r => r.Create(It.Is<AnnualLeave>(
                al =>
                al.DateCreated <= currentDateTime.AddSeconds(5) && al.DateCreated >= currentDateTime.AddSeconds(-5)
                && al.ExternalReference != null
                && al.User.Id == userEntity.Id
                )), Times.Once);
            Assert.True(validResult.IsSuccess == true);
            Assert.Null(validResult.Error);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void CreateAsync_WhenDatabaseUpdateFails_ReturnsFailureResult()
        {
            //arrange
            var validAnnualLeave = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.StartDate, new DateTime(2025, 06, 17))
                .With(a => a.EndDate, new DateTime(2025, 06, 20))
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.UserId, userId)
                .Create();

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(Result.Failure("Database save failed."));

            //act
            var createResult = await _annualLeaveService.CreateAsync(validAnnualLeave);

            //assert
            Assert.True(createResult.IsSuccess == false);
            Assert.Equal("Database save failed.", createResult.Error);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        #endregion

        #region CheckForLeaveOverlapsAsync

        [Fact]
        public async void CheckForLeaveOverlapsAsync_WithEndDateBeforeStartDate_ReturnsOverlap()
        {
            //arrange
            var startDate = new DateTime(2025, 6, 21);
            var endDate = new DateTime(2025, 6, 18);

            //act
            var endBeforeStartResult = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, annualLeaveExtRef);

            //assert
            Assert.True(endBeforeStartResult.HasOverlap == true);
            Assert.Equal("End date can not be before or the same day as the start date.", endBeforeStartResult.Error);
        }

        [Fact]
        public async void CheckForLeaveOverlapsAsync_WithNoLeave_ReturnsNoOverlap()
        {
            //arrange
            var startDate = new DateTime(2025, 6, 21);
            var endDate = new DateTime(2025, 6, 22);

            //act
            var noOverlapResult = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, annualLeaveExtRef);
            var noOverlapResultWithNoLeaveRef = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, null);

            //assert
            Assert.True(noOverlapResult.HasOverlap == false);
            Assert.Null(noOverlapResult.Error);
            Assert.True(noOverlapResultWithNoLeaveRef.HasOverlap == false);
            Assert.Null(noOverlapResultWithNoLeaveRef.Error);
        }

        [Fact]
        public async void CheckForLeaveOverlapsAsync_HavingLeaveWithNoOverlaps_ReturnsNoOverlap()
        {
            //arrange
            var startDate = new DateTime(2025, 6, 21);
            var endDate = new DateTime(2025, 6, 23);

            var annualLeavelist = new List<AnnualLeave>();

            var annualLeaveEntity1 = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, Guid.NewGuid().ToString())
                .With(a => a.StartDate, new DateTime(2025, 6, 5))
                .With(a => a.EndDate, new DateTime(2025, 6, 5))
                .With(a => a.UserId, userId)
                .Create();
            var annualLeaveEntity2 = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.StartDate, new DateTime(2025, 6, 8))
                .With(a => a.EndDate, new DateTime(2025, 6, 11))
                .With(a => a.UserId, userId)
                .Create();

            annualLeavelist.Add(annualLeaveEntity1);
            annualLeavelist.Add(annualLeaveEntity2);

            _annualLeaveRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    var list = annualLeavelist.Where(func);
                    return list;
                });

            //act
            var noOverlapResult = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, annualLeaveExtRef);
            var noOverlapResultWithNoLeaveRef = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, null);

            //assert
            Assert.True(noOverlapResult.HasOverlap == false);
            Assert.Null(noOverlapResult.Error);
            Assert.True(noOverlapResultWithNoLeaveRef.HasOverlap == false);
            Assert.Null(noOverlapResultWithNoLeaveRef.Error);
        }

        [Fact]
        public async void CheckForLeaveOverlapsAsync_HavingLeaveWithOverlaps_ReturnsOverlap()
        {
            //arrange
            var startDate = new DateTime(2025, 6, 21);
            var endDate = new DateTime(2025, 6, 23);

            var annualLeavelist = new List<AnnualLeave>();

            var annualLeaveEntity1 = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, Guid.NewGuid().ToString())
                .With(a => a.StartDate, new DateTime(2025, 6, 21))
                .With(a => a.EndDate, new DateTime(2025, 6, 22))
                .With(a => a.UserId, userId)
                .Create();
            var annualLeaveEntity2 = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .With(a => a.StartDate, new DateTime(2025, 6, 5))
                .With(a => a.EndDate, new DateTime(2025, 6, 5))
                .With(a => a.UserId, userId)
                .Create();

            annualLeavelist.Add(annualLeaveEntity1);
            annualLeavelist.Add(annualLeaveEntity2);

            _annualLeaveRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    var list = annualLeavelist.Where(func);
                    return list;
                });

            //act
            var overlapResult = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, annualLeaveExtRef);
            var overlapResultWithNoLeaveRef = await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, null);

            //assert
            Assert.True(overlapResult.HasOverlap == true);
            Assert.NotNull(overlapResult.Error);
            Assert.Equal("Current selection would cause an overlap in already-booked annual leave!", overlapResult.Error);
            Assert.True(overlapResultWithNoLeaveRef.HasOverlap == true);
            Assert.NotNull(overlapResultWithNoLeaveRef.Error);
            Assert.Equal("Current selection would cause an overlap in already-booked annual leave!", overlapResultWithNoLeaveRef.Error);
        }

        #endregion

        #region DeleteAsync

        [Fact]
        public async void DeleteAsync_WithAnnualLeaveNotFound_ReturnsFailure()
        {
            //arrange
            var annualLeave = _fixture.Create<AnnualLeaveViewModel>();

            //act
            var failureResult = await _annualLeaveService.DeleteAsync(annualLeave);

            //assert
            Assert.True(failureResult.IsSuccess == false);
            Assert.Equal("Entity not found.", failureResult.Error);
        }

        [Fact]
        public async void DeleteAsync_WithAnnualLeaveFound_ReturnsSuccess()
        {
            //arrange
            var annualLeaveViewModel = _fixture.Build<AnnualLeaveViewModel>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .Create();
            var annualLeaveEntity = _fixture.Build<AnnualLeave>()
                .With(a => a.ExternalReference, annualLeaveExtRef)
                .Create();

            _annualLeaveRepositoryMock.Setup(a => a.Delete(It.IsAny<AnnualLeave>()));
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var successResult = await _annualLeaveService.DeleteAsync(annualLeaveViewModel);

            //assert
            Assert.True(successResult.IsSuccess == true);
            Assert.Null(successResult.Error);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        #endregion

        #region GetBookedLeaveByUserIdAsync

        [Fact]
        public async void GetBookedLeaveByUserIdAsync_NoLeaveFound_ReturnsEmptyList()
        {
            //arrange
            var annualLeaveList = new List<AnnualLeave>();

            _annualLeaveRepositoryMock.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    var list = annualLeaveList.Where(func);
                    return list;
                }); ;

            //act
            var emptyList = await _annualLeaveService.GetBookedLeaveByUserIdAsync(userId);

            //assert
            Assert.Empty(emptyList);
        }

        [Fact]
        public async void GetBookedLeaveByUserIdAsync_LeaveFound_ReturnsPopulatedList()
        {
            //arrange
            var annualLeaveList = new List<AnnualLeave>();

            var annualLeave1 = _fixture.Build<AnnualLeave>()
                .With(a => a.UserId, userId)
                .Create();
            var annualLeave2 = _fixture.Build<AnnualLeave>()
                .With(a => a.UserId, userId)
                .Create();

            annualLeaveList.Add(annualLeave1);
            annualLeaveList.Add(annualLeave2);

            _annualLeaveRepositoryMock.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<AnnualLeave, bool>>>()))
                .ReturnsAsync((Expression<Func<AnnualLeave, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    var list = annualLeaveList.Where(func);
                    return list;
                }); ;

            //act
            var populatedList = await _annualLeaveService.GetBookedLeaveByUserIdAsync(userId);

            //assert
            Assert.NotNull(populatedList);
            Assert.NotEmpty(populatedList);
            Assert.Equal(2, populatedList.Count());
        }

        #endregion

        #region GetByExternalReferenceAsync

        [Fact]
        public async void GetByExternalReference_NoEntityFound_ReturnsNull()
        {
            //arrange
            var extRef = Guid.NewGuid().ToString();

            //act
            var result = await _annualLeaveService.GetByExternalReferenceAsync(extRef);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetByExternalReferenceAsync_EntityFound_ReturnsViewModel()
        {
            //act
            var returnedViewModel = await _annualLeaveService.GetByExternalReferenceAsync(annualLeaveExtRef);

            //assert
            Assert.NotNull(returnedViewModel);
            Assert.Equal(annualLeaveExtRef, returnedViewModel.ExternalReference);
            Assert.IsType<AnnualLeaveViewModel>(returnedViewModel);
        }

        #endregion

        #region GetUserAnnualLeaveCountAsync

        [Fact]
        public async void GetUserAnnualLeaveCountAsync_NoUserFound_Returns0()
        {
            //arrange
            var randomUserExtRef = Guid.NewGuid().ToString();

            //act
            var result = await _annualLeaveService.GetUserAnnualLeaveCountAsync(randomUserExtRef);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async void GetUserAnnualLeaveCountAsync_UserWithNegativeLeaveDayCount_ReturnsZero()
        {
            //arrange
            var userEntity = _fixture.Build<ApplicationUser>()
                .With(a => a.Id, userId)
                .With(a => a.AnnualLeaveDays, -2)
                .Create();
            _userManagementRepositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((Expression<Func<ApplicationUser, bool>> predicate) =>
                {
                    var func = predicate.Compile();
                    return func(userEntity) ? userEntity : null;
                });

            //act
            var negativeCountResult = await _annualLeaveService.GetUserAnnualLeaveCountAsync(userId);

            //assert
            Assert.Equal(0, negativeCountResult);
            Assert.True(negativeCountResult != -2);
            Assert.False(negativeCountResult < 0);
        }

        [Fact]
        public async void GetUserAnnualLeaveCountAsync_FoundUserWithAnnualLeave_ReturnsCount()
        {
            //arrange - not required
            //act
            var annualLeaveCount = await _annualLeaveService.GetUserAnnualLeaveCountAsync(userId);

            //assert
            Assert.Equal(10, annualLeaveCount);
        }

        #endregion
    }
}