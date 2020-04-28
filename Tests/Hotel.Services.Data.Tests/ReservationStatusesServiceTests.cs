namespace Hotel.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;
    using Hotel.Data.Repositories;
    using Hotel.Services.Data.Tests.Common;
    using Hotel.Services.Data.Tests.Common.Seeders;
    using Hotel.Web.ViewModels.ReservationStatuses;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ReservationStatusesServiceTests
    {
        [Fact]
        public async Task AddReservationStatusAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "ReservationsService AddReservationStatusAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var reservationStatusModel = new ReservationStatus
            {
                Name = "Test",
            };

            // Act
            var result = await reservationStatusesService.AddReservationStatusAsync(reservationStatusModel);

            // Assert
            Assert.True(result, errorMessage + " " + "Returns false.");
        }

        [Fact]
        public async Task AddReservationStatusAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "ReservationService AddReservationStatusAsync() method does not work properly.";

            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var reservationStatusModel = new ReservationStatus
            {
                Name = "Test",
            };

            // Act
            await reservationStatusesService.AddReservationStatusAsync(reservationStatusModel);
            var actualResult = reservationStatusRepository.All().First();
            var expectedResult = reservationStatusModel;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task AddReservationStatusAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            var reservationStatusModel = new ReservationStatus
            {
                Name = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationStatusesService.AddReservationStatusAsync(reservationStatusModel);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "ReservationStatusesService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            var reservationStatus = context.ReservationStatuses.First();

            var model = new EditReservationStatusViewModel
            {
                Id = reservationStatus.Id,
                Name = "Test-1-Edited",
            };

            // Act
            var result = await reservationStatusesService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessage + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            var model = new EditReservationStatusViewModel
            {
                Id = nonExistentId,
                Name = "Test-1-Edited",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationStatusesService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            var reservationStatus = context.ReservationStatuses.First();

            var model = new EditReservationStatusViewModel
            {
                Id = reservationStatus.Id,
                Name = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationStatusesService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessagePrefix = "ReservationStatusesService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            var reservationStatus = context.ReservationStatuses.First();

            var model = new EditReservationStatusViewModel
            {
                Id = reservationStatus.Id,
                Name = "Test-1-Edited",
            };

            // Act
            await reservationStatusesService.EditAsync(model);
            var actualResult = reservationStatusRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationStatusesService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            var reservationStatusId = reservationStatusRepository.All().First().Id;

            // Act
            var result = await reservationStatusesService.DeleteByIdAsync(reservationStatusId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "ReservationStatusesService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            var reservationStatusId = reservationStatusRepository.All().First().Id;

            // Act
            var reservationStatusCount = reservationStatusRepository.All().Count();
            await reservationStatusesService.DeleteByIdAsync(reservationStatusId);
            var actualResult = reservationStatusRepository.All().Count();
            var expectedResult = reservationStatusCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Reservation ststuses count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationStatusesService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetReservationStatusByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationStatusesService GetReservationStatusByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            var reservationStatusId = reservationStatusRepository.All().First().Id;

            // Act
            var actualResult = await reservationStatusesService.GetReservationStatusByIdAsync(reservationStatusId);
            var expectedResult = await reservationStatusRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == reservationStatusId);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetReservationStatusByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationStatusesService.GetReservationStatusByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllReservationStatusesCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationStatusesService GetAllReservationStatusesCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            // Act
            var actualResult = await reservationStatusesService.GetAllReservationStatusesCountAsync();
            var expectedResult = reservationStatusRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "ReservationStatusesService GetAllReservationStatusesCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationStatusesService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            var reservationStatusId = reservationStatusRepository.All().First().Id;

            // Act
            var actualResult = await reservationStatusesService.GetViewModelByIdAsync<EditReservationStatusViewModel>(reservationStatusId);
            var expectedResult = new EditReservationStatusViewModel
            {
                Id = reservationStatusId,
                Name = reservationStatusRepository.All().First().Name,
            };

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationStatusesService.GetViewModelByIdAsync<EditReservationStatusViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllReservationStatuses_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationStatusesService GetAllReservationStatuses() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            // Act
            var actualResult = reservationStatusesService.GetAllReservationStatuses<DetailsReservationStatusViewModel>().ToList();
            var expectedResult = new DetailsReservationStatusViewModel[]
            {
                new DetailsReservationStatusViewModel
                {
                     Id = reservationStatusRepository.All().First().Id,
                     Name = reservationStatusRepository.All().First().Name,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].Name == actualResult[0].Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetAllReservationStatuses_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            // Act
            var actualResult = reservationStatusesService.GetAllReservationStatuses<DetailsReservationStatusViewModel>().ToList();
            var expectedResult = new DetailsReservationStatusViewModel[]
            {
                new DetailsReservationStatusViewModel
                {
                     Id = reservationStatusRepository.All().First().Id,
                     Name = reservationStatusRepository.All().First().Name,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task CreateAllAsync_ShouldShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "ReservationStatusesService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            // Act
            var result = await reservationStatusesService.CreateAllAsync(new string[] { "Test-1", "Test-2", "Test-3" });

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task CreateAllAsync_ShouldReturnCorrectCount()
        {
            var errorMessage = "ReservationStatusesService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            // Act
            var reservationStatusesCount = reservationStatusRepository.All().Count();
            await reservationStatusesService.CreateAllAsync(new string[] { "Test-1" });
            var actualResult = reservationStatusRepository.All().Count();
            var expectedResult = reservationStatusesCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage + " " + "Reservation statuses count is not reduced.");
        }

        [Fact]
        public async Task CreateAllAsync_ShouldReturnCorrectResult()
        {
            var errorMessage = "ReservationStatusesService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusAsync(context);

            // Act
            await reservationStatusesService.CreateAllAsync(new string[] { "Test-1" });
            var actualResult = new ReservationStatus { Name = "Test-1" };
            var expectedResult = reservationStatusRepository.All().First();

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessage + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetReservationStatusByNamec_WithExistentName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationStatusesService GetReservationStatusByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);
            var seeder = new ReservationStatusesServiceTestsSeeder();
            await seeder.SeedReservationStatusesAsync(context);

            var reservationStatusName = reservationStatusRepository.All().First().Name;

            // Act
            var actualResult = reservationStatusesService.GetReserVationStatusByName(reservationStatusName);
            var expectedResult = await reservationStatusRepository
                .All()
                .SingleOrDefaultAsync(x => x.Name == reservationStatusName);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetReservationStatusByName_WithNonExistentName_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            var nonExistentName = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                reservationStatusesService.GetReserVationStatusByName(nonExistentName);
            });
        }

        [Fact]
        public async Task GetReservationStatusByName_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationStatusRepository = new EfDeletableEntityRepository<ReservationStatus>(context);
            var reservationStatusesService = this.GetReservationStatusesService(reservationStatusRepository, context);

            string name = null;

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                reservationStatusesService.GetReserVationStatusByName(name);
            });
        }

        private ReservationStatusesService GetReservationStatusesService(EfDeletableEntityRepository<ReservationStatus> reservationStatusRepository, HotelDbContext context)
        {
            var reservationStatusesService = new ReservationStatusesService(reservationStatusRepository);

            return reservationStatusesService;
        }
    }
}
