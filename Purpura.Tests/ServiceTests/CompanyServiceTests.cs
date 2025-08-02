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
using System.Linq.Expressions;

namespace Purpura.Tests.ServiceTests
{
    public class CompanyServiceTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public CompanyServiceTests()
        {
            _fixture = new Fixture();

            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(a => a.CompanyRepository).Returns(_companyRepositoryMock.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CompanyMappingProfile>();
                cfg.AddProfile<CompanyEmployeeMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _companyService = new CompanyService(
                _mapper,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_SaveToDatabaseFail_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Create<CompanyViewModel>();

            _companyRepositoryMock.Setup(a => a.Create(It.IsAny<Company>()));
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Failure("Entity Creation Failed"));

            //act
            var result = await _companyService.CreateAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Entity Creation Failed", result.Error);
            Assert.Null(result.DataList);
            _companyRepositoryMock.Verify(a => a.Create(It.IsAny<Company>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_SaveToDatabaseSuccess_ReturnsSuccessResultWithRequiredData()
        {
            //arrange
            var viewModel = _fixture.Create<CompanyViewModel>();

            _companyRepositoryMock.Setup(a => a.Create(It.IsAny<Company>()));
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var result = await _companyService.CreateAsync(viewModel);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.NotNull(result.DataList);
            Assert.True(Guid.TryParse(result.DataList.ElementAt(0), out Guid guid));
            Assert.True(int.TryParse(result.DataList.ElementAt(1), out int intResult));
            _companyRepositoryMock.Verify(a => a.Create(It.IsAny<Company>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditAsync_NoEntityFound_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Create<CompanyViewModel>();

            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync((Company?)null);

            //act
            var result = await _companyService.EditAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Entity not found.", result.Error);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task EditAsync_DatabaseSaveFailed_ReturnsFailureResult()
        {
            //arrange
            var viewModel = _fixture.Create<CompanyViewModel>();

            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(new Company());
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Failure("Database save failed."));

            //act
            var result = await _companyService.EditAsync(viewModel);

            //assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Database save failed.", result.Error);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditAsync_DatabaseSaveSuccess_ReturnsSuccessResult()
        {
            //arrange
            var viewModel = _fixture.Create<CompanyViewModel>();

            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(new Company());
            _unitOfWorkMock.Setup(a => a.SaveChangesAsync())
                .ReturnsAsync(Result.Success());

            //act
            var result = await _companyService.EditAsync(viewModel);

            //assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
            _unitOfWorkMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByExternalReferenceAsync_NoEntityFound_ReturnsNull()
        {
            //arrange
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync((Company?)null);

            //act
            var result = await _companyService.GetByExternalReferenceAsync(Guid.NewGuid().ToString());

            //assert
            Assert.Null(result);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetByExternalReferenceAsync_EntityFound_ReturnsViewModel()
        {
            //arrange
            var companyEntity = _fixture.Build<Company>()
                .With(a => a.Address, "123 Some Street, Some Business Estate, London, ABC 123")
                .Without(a => a.Employees)
                .Create();

            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(companyEntity);

            //act
            var result = await _companyService.GetByExternalReferenceAsync(Guid.NewGuid().ToString());

            //assert
            Assert.NotNull(result);
            Assert.IsType<CompanyViewModel>(result);
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetByExternalReferenceWithCompanyEmployeesAsync_NoEntityFound_ReturnsNull()
        {
            //arrange
            _companyRepositoryMock.Setup(a => a.GetByExternalReferenceWithEmployeesAsync(It.IsNotNull<string>()))
                .ReturnsAsync((Company?)null);

            //act
            var result = await _companyService.GetByExternalReferenceWithCompanyEmployeesAsync(Guid.NewGuid().ToString());

            //assert
            Assert.Null(result);
            _companyRepositoryMock.Verify(a => a.GetByExternalReferenceWithEmployeesAsync(It.IsNotNull<string>()), Times.Once);
        }

        [Fact]
        public async Task GetByExternalReferenceWithCompanyEmployeesAsync_EntityFound_ReturnsViewModelWithCompanyEmployeesList()
        {
            //arrange + act 1
            var companyEntityWithEmployees = _fixture.Build<Company>()
               .With(a => a.Employees, new List<CompanyEmployee>()
               {
                   new CompanyEmployee()
               })
               .Create();
            _companyRepositoryMock.Setup(a => a.GetByExternalReferenceWithEmployeesAsync(It.IsNotNull<string>()))
                .ReturnsAsync(companyEntityWithEmployees);

            var resultWithCompanyEmployee = await _companyService.GetByExternalReferenceWithCompanyEmployeesAsync(Guid.NewGuid().ToString());

            //arrange + act 2
            var companyEntityWithNoEmployees = _fixture.Build<Company>()
               .Without(a => a.Employees)
               .Create();
            _companyRepositoryMock.Setup(a => a.GetByExternalReferenceWithEmployeesAsync(It.IsNotNull<string>()))
                .ReturnsAsync(companyEntityWithNoEmployees);

            var resultWithNoCompanyEmployee = await _companyService.GetByExternalReferenceWithCompanyEmployeesAsync(Guid.NewGuid().ToString());

            //assert
            Assert.NotNull(resultWithCompanyEmployee);
            Assert.Single(resultWithCompanyEmployee.Employees);

            Assert.NotNull(resultWithNoCompanyEmployee);
            Assert.Empty(resultWithNoCompanyEmployee.Employees);
            _companyRepositoryMock.Verify(a => a.GetByExternalReferenceWithEmployeesAsync(It.IsNotNull<string>()), Times.AtLeast(2));
        }

        [Fact]
        public async Task GetExternalReferenceByIdAsync_NoEntityFound_ReturnsEmptyString()
        {
            //arrange
            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync((Company?)null);

            //act
            var result = await _companyService.GetExternalReferenceByIdAsync(1);

            //assert
            Assert.True(string.IsNullOrEmpty(result));
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GetExternalReferenceByIdAsync_EntityFound_ReturnsExternalReference()
        {
            //arrange
            var externalReference = Guid.NewGuid().ToString();
            var companyEntity = _fixture.Build<Company>()
                .With(a => a.ExternalReference, externalReference)
                .Without(a => a.Employees)
                .Create();

            _companyRepositoryMock.Setup(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                .ReturnsAsync(companyEntity);

            //act
            var result = await _companyService.GetExternalReferenceByIdAsync(1);

            //assert
            Assert.False(string.IsNullOrEmpty(result));
            Assert.True(Guid.TryParse(result, out Guid guid));
            _companyRepositoryMock.Verify(a => a.GetSingleAsync(It.IsAny<Expression<Func<Company, bool>>>()), Times.Once);
        }
    }
}
