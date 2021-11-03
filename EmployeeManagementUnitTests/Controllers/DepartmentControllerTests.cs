using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Service.Interface;
using Moq;
using ServiceLibrary.Service.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementUnitTests.Controllers
{
    public class DepartmentControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IDepartmentService> mockDepartmentService;
        private Mock<IMapper> mockMapper;
        private Mock<IUserService> mockUserService;

        public DepartmentControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDepartmentService = this.mockRepository.Create<IDepartmentService>();
            this.mockMapper = this.mockRepository.Create<IMapper>();
            this.mockUserService = this.mockRepository.Create<IUserService>();
        }

        private DepartmentController CreateDepartmentController()
        {
            return new DepartmentController(
                this.mockDepartmentService.Object,
                this.mockMapper.Object,
                this.mockUserService.Object);
        }

        [Fact]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();

            // Act
            var result = departmentController.Index();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void CreateDepartment_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();

            // Act
            var result = departmentController.CreateDepartment();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CreateDepartment_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();
            DepartmentViewModel departamentViewModel = null;

            // Act
            var result = await departmentController.CreateDepartment(
                departamentViewModel);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();
            int id = 0;

            // Act
            var result = await departmentController.Delete(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Edit_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();
            int id = 0;

            // Act
            var result = departmentController.Edit(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Edit_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();
            DepartmentViewModel departmentViewModel = null;

            // Act
            var result = await departmentController.Edit(
                departmentViewModel);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void CheckName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var departmentController = this.CreateDepartmentController();
            string DepartmentName = null;

            // Act
            var result = departmentController.CheckName(
                DepartmentName);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
