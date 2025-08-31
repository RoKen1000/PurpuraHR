using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.MappingProfiles;
using Purpura.Repositories;
using Purpura.Services;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Purpura.Tests.ServiceTests
{
    public class UserManagementServiceTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<IUserManagementRepository> _userManagementRepositoryMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IMapper _mapper;
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserManagementService _userManagementService;

        public UserManagementServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _userManagementRepositoryMock = new Mock<IUserManagementRepository>();
            _userManagerMock = CreateUserManagerMock();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(a => a.UserManagementRepository).Returns(_userManagementRepositoryMock.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationUserMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _userManagementService = new UserManagementService(
                _mapper,
                _unitOfWorkMock.Object,
                _userManagerMock.Object
            );
        }

        private Mock<UserManager<IdentityUser>> CreateUserManagerMock()
        {
            var userStore = new Mock<IUserStore<IdentityUser>>();

            var userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStore.Object,
                null,
                null,
                new List<IUserValidator<IdentityUser>>(),
                new List<IPasswordValidator<IdentityUser>>(),
                null,
                null,
                null,
                null
            );

            return userManagerMock;
        }

        [Fact]
        public async Task GetUser_NoUserFound_ReturnsNull()
        {
            //arrange
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((ApplicationUser?)null);

            //act
            var result = await _userManagementService.GetUser(a => a.Id == "");

            //assert
            Assert.Null(result);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetUser_UserFound_ReturnsUserViewModel()
        {
            //arrange
            var user = _fixture.Create<ApplicationUser>();

            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync(user);

            //act
            var result = await _userManagementService.GetUser(a => a.Id == "");

            //assert
            Assert.NotNull(result);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task AddUserCompanyReferenceClaimAsync_UserNotFound_ReturnsFailureResult()
        {
            //arrange
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((ApplicationUser?)null);

            //act
            var result = await _userManagementService.AddUserCompanyReferenceClaimAsync("", "", "");

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("User not found.", result.Error);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task AddUserCompanyReferenceClaimAsync_UserFoundButCompanyIdSetFails_ReturnsFailureResult()
        {
            //arrange
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync(new ApplicationUser());
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Failure("Database save failed."));

            //act
            var result = await _userManagementService.AddUserCompanyReferenceClaimAsync("", "", "1");

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("Database save failed.", result.Error);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddUserCompanyReferenceClaimAsync_ClaimAddFails_ReturnsFailureResult()
        {
            //arrange
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync(new ApplicationUser());
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());
            _userManagerMock.Setup(a => a.AddClaimAsync(It.IsAny<IdentityUser>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Failed());

            //act
            var result = await _userManagementService.AddUserCompanyReferenceClaimAsync("", "", "1");

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("Failed to add claim.", result.Error);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
            _userManagerMock.Verify(a => a.AddClaimAsync(It.IsAny<IdentityUser>(), It.IsAny<Claim>()), Times.Once);
        }

        [Fact]
        public async Task AddUserCompanyReferenceClaimAsync_ClaimSuccessfullyAdded_ReturnsSuccessResult()
        {
            //arrange
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync(new ApplicationUser());
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());
            _userManagerMock.Setup(a => a.AddClaimAsync(It.IsAny<IdentityUser>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success);

            //act
            var result = await _userManagementService.AddUserCompanyReferenceClaimAsync("", "", "1");

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
            _userManagerMock.Verify(a => a.AddClaimAsync(It.IsAny<IdentityUser>(), It.IsAny<Claim>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_()
        {
            //arrange
        }
    }
}
