using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
using System.Security.Claims;

namespace Purpura.Tests.ServiceTests
{
    public class CompanyEmployeeServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<ICompanyEmployeeRepository> _companyEmployeeRepositoryMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<IUserManagementRepository> _userManagementRepositoryMock;

        private readonly ICompanyEmployeeService _companyEmployeeService;

        public CompanyEmployeeServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _companyEmployeeRepositoryMock = new Mock<ICompanyEmployeeRepository>();
            _userManagerMock = CreateUserManagerMock();
            _userManagementRepositoryMock = new Mock<IUserManagementRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CompanyEmployeeMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _companyEmployeeService = new CompanyEmployeeService(
                _mapper,
                _unitOfWorkMock.Object,
                _companyEmployeeRepositoryMock.Object,
                _companyRepositoryMock.Object,
                _userManagementRepositoryMock.Object,
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
        public async Task AssignUserToCompanyEmployeeAsync_NoUserOrCompanyEmployeeFound_ReturnsFailureResult()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync((CompanyEmployee?)null);
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((ApplicationUser?)null);

            var errorMessage = "No company employee found with this email. No user found with this email.";

            //act
            var result = await _companyEmployeeService.AssignUserToCompanyEmployeeAsync("");

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal(result.Error, errorMessage);

            _companyEmployeeRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()), Times.Once);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task AssignUserToCompanyEmployeeAsync_CompanyEmployeeAndUserFound_SetsRequiredDataAndReturnsSuccessResult()
        {
            //arrange
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString() };
            var companyEmployee = new CompanyEmployee { Id = 1, CompanyId = 2 };
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync(companyEmployee);
            _companyEmployeeRepositoryMock.Setup(a => a.Update(companyEmployee)).Verifiable();
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync(user);
            _userManagementRepositoryMock.Setup(a => a.Update(user)).Verifiable();
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(new Company { ExternalReference = Guid.NewGuid().ToString() });
            _userManagerMock.Setup(a => a.AddClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>()))
                .ReturnsAsync(new IdentityResult());


            //act
            var result = await _companyEmployeeService.AssignUserToCompanyEmployeeAsync("");

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.NotNull(user.CompanyId);
            Assert.Equal(user.CompanyId, companyEmployee.CompanyId);
            Assert.NotNull(companyEmployee.ApplicationUser);
            Assert.Equal(companyEmployee.ApplicationUser.Id, user.Id);

            _companyEmployeeRepositoryMock.VerifyAll();
            _userManagementRepositoryMock.VerifyAll();
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            _userManagerMock.Verify(a => a.AddClaimAsync(It.IsAny<ApplicationUser>(), It.IsAny<Claim>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CompanyNotFound_ReturnsFailureResult()
        {
            //arrange
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync((Company?)null);

            var errorMessage = "Company not found.";

            //act
            var result = await _companyEmployeeService.CreateAsync(new CompanyEmployeeViewModel());

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal(result.Error, errorMessage);

            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_UserNotFoundWhenEmailExists_ReturnsFailureResult()
        {
            //arrange
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(new Company());
            _userManagementRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .ReturnsAsync((ApplicationUser?)null);

            var errorMessage = "User not found.";

            //act
            var result = await _companyEmployeeService.CreateAsync(new CompanyEmployeeViewModel { EmailExists = true });

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal(result.Error, errorMessage);

            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            _userManagementRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_OnSuccessfulCreate_ReturnsSuccessResult()
        {
            //arrange
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(new Company());
            _companyEmployeeRepositoryMock.Setup(a => a.Create(It.IsAny<CompanyEmployee>())).Verifiable();
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var result = await _companyEmployeeService.CreateAsync(new CompanyEmployeeViewModel());

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);

            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            _companyEmployeeRepositoryMock.Verify(a => a.Create(It.IsAny<CompanyEmployee>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditAsync_NoEntityFound_ReturnsFailureResult()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync((CompanyEmployee?)null);
            var errorMessage = "Employee not found.";

            //act
            var result = await _companyEmployeeService.EditAsync(new CompanyEmployeeViewModel());

            //assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal(result.Error, errorMessage);

            _companyEmployeeRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task EditAsync_EntityFound_ReturnsSuccessResult()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync(new CompanyEmployee());
            _companyEmployeeRepositoryMock.Setup(a => a.Update(It.IsAny<CompanyEmployee>())).Verifiable();
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var result = await _companyEmployeeService.EditAsync(new CompanyEmployeeViewModel());

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);

            _companyEmployeeRepositoryMock.VerifyAll();
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAsync_NoEntityFound_ReturnsNull()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync((CompanyEmployee?)null);

            //act
            var result = await _companyEmployeeService.GetAsync(a => a.Id == 1);

            //assert
            Assert.Null(result);

            _companyEmployeeRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_EntityFound_ReturnsViewModel()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync(new CompanyEmployee());

            //act
            var result = await _companyEmployeeService.GetAsync(a => a.Id == 1);

            //assert
            Assert.NotNull(result);
            Assert.IsNotType<CompanyEmployee>(result);
            Assert.IsType<CompanyEmployeeViewModel>(result);

            _companyEmployeeRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetExternalReferenceAsync_NoEntityFound_ReturnsNull()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync((CompanyEmployee?)null);

            //act
            var result = await _companyEmployeeService.GetByExternalReferenceAsync("");

            //assert
            Assert.Null(result);

            _companyEmployeeRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetExternalReferenceAsync_EntityFound_ReturnsViewModel()
        {
            //arrange
            _companyEmployeeRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()))
                .ReturnsAsync(new CompanyEmployee());
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync((Company?)null);

            //act
            var result = await _companyEmployeeService.GetByExternalReferenceAsync("");

            //assert
            Assert.NotNull(result);
            Assert.IsNotType<CompanyEmployee>(result);
            Assert.IsType<CompanyEmployeeViewModel>(result);

            _companyEmployeeRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<CompanyEmployee, bool>>>()), Times.Once);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }
    }
}
