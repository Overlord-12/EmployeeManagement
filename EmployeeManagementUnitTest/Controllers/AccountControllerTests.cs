using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementUnitTest.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IMapper> mockMapper;
        private Mock<IUserService> mockUserService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockMapper = this.mockRepository.Create<IMapper>();
            this.mockUserService = this.mockRepository.Create<IUserService>();
        }

        private AccountController CreateAccountController()
        {
            return new AccountController(
                this.mockMapper.Object,
                this.mockUserService.Object);
        }

        [TestMethod]
        public void Login_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accountController = this.CreateAccountController();

            // Act
            var result = accountController.Login();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task Login_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var accountController = this.CreateAccountController();
            UserViewModel model = null;

            // Act
            var result = await accountController.Login(
                model);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
