namespace Hotel.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Data.Repositories;
    using Hotel.Services.Data.Tests.Common;
    using Hotel.Services.Data.Tests.Common.Seeders;
    using Hotel.Web.ViewModels.UserRequests;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UserRequestsServiceTests
    {
        [Fact]
        public async Task AddUserRequestAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "UserRequestsService AddUserRequestAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);

            var model = new UserRequest
            {
                Title = "Title",
                Content = "Content",
                Email = "email",
                RequestDate = DateTime.Now,
            };

            // Act
            var result = await userRequestsService.AddUserRequestAsync(model);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddUserRequestAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "UserRequestsService AddUserRequestAsync() method does not work properly.";

            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);

            var model = new UserRequest
            {
                Title = "Title",
                Content = "Content",
                Email = "email",
                RequestDate = DateTime.Now,
            };

            // Act
            await userRequestsService.AddUserRequestAsync(model);
            var actualResult = userRequestRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.Title == actualResult.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResult.Email == actualResult.Email, errorMessagePrefix + " " + "Email is not returned properly.");
            Assert.True(expectedResult.Content == actualResult.Content, errorMessagePrefix + " " + "Content is not returned properly.");
        }

        [Fact]
        public async Task AddUserRequestAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);

            var model = new UserRequest
            {
                Title = null,
                Content = null,
                Email = "email",
                RequestDate = DateTime.Now,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userRequestsService.AddUserRequestAsync(model);
            });
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypeService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);
            var seeder = new UserRequestsTestsSeeder();
            await seeder.SeedUserRequestsAsync(context);

            var userRequestId = userRequestsService.All().First().Id;

            // Act
            var result = await userRequestsService.DeleteByIdAsync(userRequestId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "UserRequestsService DeleteByIdAsync() method does not work properly.";

            // Arrange
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);
            var seeder = new UserRequestsTestsSeeder();
            await seeder.SeedUserRequestsAsync(context);

            var userRequestId = userRequestsService.All().First().Id;

            // Act
            var userRequestCount = userRequestRepository.All().Count();
            await userRequestsService.DeleteByIdAsync(userRequestId);
            var actualResult = userRequestRepository.All().Count();
            var expectedResult = userRequestCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Room types count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userRequestsService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetUserRequestByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "UserRequestService GetUserRequestByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);
            var seeder = new UserRequestsTestsSeeder();
            await seeder.SeedUserRequestsAsync(context);

            var userRequestId = userRequestsService.All().First().Id;

            // Act
            var actualResult = await userRequestsService.GetUserRequestByIdAsync(userRequestId);
            var expectedResult = await userRequestRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == userRequestId);

            // Assert
            Assert.True(expectedResult.Title == actualResult.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResult.Email == actualResult.Email, errorMessagePrefix + " " + "Email is not returned properly.");
            Assert.True(expectedResult.Content == actualResult.Content, errorMessagePrefix + " " + "Content is not returned properly.");
        }

        [Fact]
        public async Task GetRoomTypeByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userRequestsService.GetUserRequestByIdAsync(nonExistentId);
            });
        }

        public async Task GetAllUserRequests_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "UserRequestsService  GetAllUserRequests() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);
            var seeder = new UserRequestsTestsSeeder();
            await seeder.SeedUserRequestsAsync(context);

            // Act
            var actualResult = userRequestsService.GetAllUserRequests<DetailsUserRequestViewModel>().ToList();
            var expectedResult = new DetailsUserRequestViewModel[]
            {
                new DetailsUserRequestViewModel
                {
                     Id = userRequestRepository.All().First().Id,
                     Email = userRequestRepository.All().First().Email,
                     Content = userRequestRepository.All().First().Content,
                     Title = userRequestRepository.All().First().Title,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].Title == actualResult[0].Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResult[0].Content == actualResult[0].Content, errorMessagePrefix + " " + "Conteny is not returned properly.");
            Assert.True(expectedResult[0].Email == actualResult[0].Email, errorMessagePrefix + " " + "Email is not returned properly.");
        }

        [Fact]
        public async Task GetAllUserRequests_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);
            var seeder = new UserRequestsTestsSeeder();
            await seeder.SeedUserRequestAsync(context);

            // Act
            var actualResult = userRequestsService.GetAllUserRequests<DetailsUserRequestViewModel>().ToList();
            var expectedResult = new DetailsUserRequestViewModel[]
            {
                new DetailsUserRequestViewModel
                {
                     Id = userRequestRepository.All().First().Id,
                     Email = userRequestRepository.All().First().Email,
                     Content = userRequestRepository.All().First().Content,
                     Title = userRequestRepository.All().First().Title,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task GetUnseenRequests_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var userRequestRepository = new EfDeletableEntityRepository<UserRequest>(context);
            var userRequestsService = this.GetUserRequestService(userRequestRepository);
            var seeder = new UserRequestsTestsSeeder();
            await seeder.SeedUserRequestAsync(context);

            // Act
            var actualResult = userRequestsService.GetUnseenRequests().Count();
            var expectedResult = context.UserRequests.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        private UserRequestsService GetUserRequestService(EfDeletableEntityRepository<UserRequest> userRequestRepository)
        {
            var userRequestService = new UserRequestsService(userRequestRepository);

            return userRequestService;
        }

    }
}
