using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.MappingProfiles;
using Purpura.Repositories;
using Purpura.Services;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Tests.ServiceTests
{
    public class UserManagementServiceTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<IUserManagementRepository> _userManagementRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
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
                _userManager
            );
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
    }
}
