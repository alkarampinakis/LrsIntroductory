using AutoMapper;
using LrsIntroducotryApi.Business.Implementation;
using LrsIntroducotryApi.Mapping;
using LrsIntroducotryApi.Models.Entities.Custom;
using LrsIntroducotryApi.Repositories;
using LrsIntroducotryApi.Transfer.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace LrsIntroductionTests
{
    [TestClass]
    public class UserServiceTests
    {
        #region private members

        private UserService _testService;
        private static IMapper _mapper;
        private Mock<IUserRepository> _userRepository;

        #endregion

        #region test Start & finish

        [TestInitialize]
        public void Setup()
        {
            var cfg = new MapperConfiguration(x => x.AddProfile(new UserMappingProfile()));
            _mapper = new Mapper(cfg);

            _userRepository = new Mock<IUserRepository>();

            _testService = new UserService(
                _mapper,
                _userRepository.Object);
        }

        [TestCleanup]
        public void VerifyNoOtherCalls()
        {
            _userRepository.VerifyNoOtherCalls();
        }

        #endregion

        #region tests

        [TestMethod]
        [TestCategory("Success")]
        public async Task GetUserByIdAsync_Success()
        {
            //prepare
            var userId = 1;

            //execute
            await _testService.GetUserByIdAsync(userId).ConfigureAwait(false);

            //assert
            _userRepository.Verify(x => x.GetUserByIdAsync(userId), Times.Once);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task GetUserByIdAsync_InvalidUserIdTest()
        {
            //prepare
            var userId = -1;

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.GetUserByIdAsync(userId)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Success")]
        public async Task InsertUserAsync_Success()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            //execute
            await _testService.InsertUserAsync(user).ConfigureAwait(false);

            //assert
            _userRepository.Verify(x => x.InsertUserAsync(It.IsAny<UserWithTypeTitle>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task InsertUserAsync_EmptyUser()
        {
            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _testService.InsertUserAsync(null)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task InsertUserAsync_EmptyUserName()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.InsertUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task InsertUserAsync_EmptyUserSurname()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.InsertUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task InsertUserAsync_EmptyEmail()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.InsertUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task InsertUserAsync_InvalidUserTitleId()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = -1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.InsertUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task InsertUserAsync_InvalidUserTypeId()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = -2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.InsertUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Success")]
        public async Task UpdateUserAsync_Success()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            //execute
            await _testService.UpdateUserAsync(user).ConfigureAwait(false);

            //assert
            _userRepository.Verify(x => x.UpdateUserAsync(It.IsAny<UserWithTypeTitle>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task UpdateUserAsync_EmptyUser()
        {
            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => _testService.UpdateUserAsync(null)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task UpdateUserAsync_EmptyUserName()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.UpdateUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task UpdateUserAsync_EmptyUserSurname()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.UpdateUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task UpdateUserAsync_EmptyEmail()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.UpdateUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task UpdateUserAsync_InvalidUserTitleId()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = -1,
                UserTypeId = 2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.UpdateUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task UpdateUserAsync_InvalidUserTypeId()
        {
            //prepare
            var user = new UserWithTypeTitleDTO
            {
                Id = 1,
                Name = "nikos",
                Surname = "korompos",
                BirthDate = new DateTime(),
                EmailAddress = "asd@asd.gr",
                IsActive = true,
                UserTitleId = 1,
                UserTypeId = -2
            };

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.UpdateUserAsync(user)).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Success")]
        public async Task DeleteUser_Success()
        {
            //prepare
            var userId = 1;

            //execute
            await _testService.DeleteUser(userId).ConfigureAwait(false);

            //assert
            _userRepository.Verify(x => x.DeleteUser(userId), Times.Once);
        }

        [TestMethod]
        [TestCategory("Fail")]
        public async Task DeleteUser_InvalidUserIdTest()
        {
            //prepare
            var userId = -1;

            // execute + assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentException>(
                () => _testService.DeleteUser(userId)).ConfigureAwait(false);
        }
        #endregion
    }
}
