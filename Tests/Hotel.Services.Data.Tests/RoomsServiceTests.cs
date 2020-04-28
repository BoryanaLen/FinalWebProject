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
    using Hotel.Web.ViewModels.Rooms;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RoomsServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomsService AddRoomAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForAddAsyncMethodAsync(context);
            var roomRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomRepository, roomTypeRepository, context);

            var roomModel = new Room
            {
                RoomNumber = "Test-1",
                RoomTypeId = context.RoomTypes.First().Id,
                Description = "TestDescription",
            };

            // Act
            var result = await roomsService.AddRoomAsync(roomModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "RoomsService AddRoomAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForAddAsyncMethodAsync(context);
            var roomRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomRepository, roomTypeRepository, context);

            var roomModel = new Room
            {
                RoomNumber = "Test-1",
                RoomTypeId = context.RoomTypes.First().Id,
                Description = "TestDescription",
            };

            // Act
            await roomsService.AddRoomAsync(roomModel);
            var actualResult = roomRepository.All().First();
            var expectedResult = roomModel;

            // Assert
            Assert.True(expectedResult.RoomNumber == actualResult.RoomNumber, errorMessage + " " + "RoomNumer is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessage + " " + "Description are not returned properly.");
            Assert.True(expectedResult.RoomTypeId == actualResult.RoomTypeId, errorMessage + " " + "RoomTypeId is not returned properly.");
            Assert.True(actualResult.RoomTypeId == context.RoomTypes.First().Id, errorMessage + " " + "RoomTypeId is not set properly.");
        }

        [Fact]
        public async Task AddAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForAddAsyncMethodAsync(context);
            var roomRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomRepository, roomTypeRepository, context);

            var roomModel = new Room
            {
                RoomNumber = null,
                RoomType = null,
                Description = "TestDescription",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomsService.AddRoomAsync(roomModel);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomsService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForEditAsyncMethodAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);

            var room = context.Rooms.First();

            var model = new EditRoomViewModel
            {
                Id = room.Id,
                RoomNumber = "Test-Edited",
                RoomTypeId = roomTypesRepository.All().Skip(1).Take(1).Select(x => x.Id).FirstOrDefault(),
                Description = "TestDescription-Edited",
            };

            // Act
            var result = await roomsService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExisterntId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForEditAsyncMethodAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            var editModel = new EditRoomViewModel
            {
                Id = nonExistentId,
                RoomNumber = "Test-Edited",
                RoomTypeId = roomTypesRepository.All().First().Id,
                Description = "TestDescription-Edited",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomsService.EditAsync(editModel);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForEditAsyncMethodAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);

            var room = context.Rooms.First();

            var model = new EditRoomViewModel
            {
                Id = room.Id,
                RoomNumber = null,
                RoomTypeId = roomTypesRepository.All().Skip(1).Take(1).Select(x => x.Id).FirstOrDefault(),
                Description = "TestDescription-Edited",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomsService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessagePrefix = "RecipeService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedDataForEditAsyncMethodAsync(context);
            var roomRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomRepository, roomTypesRepository, context);

            var room = context.Rooms.First();

            var editModel = new EditRoomViewModel
            {
                Id = room.Id,
                RoomNumber = "Test-Edited",
                RoomTypeId = roomTypesRepository.All().Skip(1).Take(1).Select(x => x.Id).FirstOrDefault(),
                Description = "TestDescription-Edited",
            };

            // Act
            await roomsService.EditAsync(editModel);
            var actualResult = roomRepository.All().First();
            var expectedResult = editModel;

            // Assert
            Assert.True(expectedResult.RoomNumber == actualResult.RoomNumber, errorMessagePrefix + " " + "RoomNumer is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description are not returned properly.");
            Assert.True(expectedResult.RoomTypeId == actualResult.RoomTypeId, errorMessagePrefix + " " + "RoomTypeId is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RecipeService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var roomId = roomsRepository.All().First().Id;

            // Act
            var result = await roomsService.DeleteByIdAsync(roomId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "RecipeService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var roomId = roomsRepository.All().First().Id;

            // Act
            var roomsCount = roomsRepository.All().Count();
            await roomsService.DeleteByIdAsync(roomId);
            var actualResult = roomsRepository.All().Count();
            var expectedResult = roomsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Recipes count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomsService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetRoomByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomService GetByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var roomId = roomsRepository.All().First().Id;

            // Act
            var actualResult = await roomsService.GetRoomByIdAsync(roomId);
            var expectedResult = await roomsRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == roomId);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.RoomNumber == actualResult.RoomNumber, errorMessagePrefix + " " + "RoomNumer is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description are not returned properly.");
            Assert.True(expectedResult.RoomTypeId == actualResult.RoomTypeId, errorMessagePrefix + " " + "RoomTypeId is not returned properly.");
        }

        [Fact]
        public async Task GetRoomByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomsService.GetRoomByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllRoomsCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomService GetAllRoomsCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomsAsync(context);
            var roomRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomRepository, roomTypesRepository, context);

            // Act
            var actualResult = await roomsService.GetAllRoomsCountAsync();
            var expectedResult = roomRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "RoomService GetAllRoomsCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomsService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var roomId = roomsRepository.All().First().Id;

            // Act
            var actualResult = await roomsService.GetViewModelByIdAsync<EditRoomViewModel>(roomId);
            var expectedResult = new EditRoomViewModel
            {
                Id = roomsRepository.All().First().Id,
                RoomNumber = "Test-1",
                RoomTypeId = roomTypesRepository.All().First().Id,
                Description = "TestDescription",
            };

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.RoomNumber == actualResult.RoomNumber, errorMessagePrefix + " " + "RoomNumer is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description are not returned properly.");
            Assert.True(expectedResult.RoomTypeId == actualResult.RoomTypeId, errorMessagePrefix + " " + "RoomTypeId is not returned properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);
            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomsService.GetViewModelByIdAsync<EditRoomViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllRooms_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomsService GetAllRooms() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);

            // Act
            var actualResult = roomsService.GetAllRooms<AvailableRoomViewModel>().ToList();
            var expectedResult = new AvailableRoomViewModel[]
            {
                new AvailableRoomViewModel
                {
                     Id = roomsRepository.All().First().Id,
                     RoomNumber = "Test-1",
                     Description = "TestDescription",
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].RoomNumber == actualResult[0].RoomNumber, errorMessagePrefix + " " + "RoomNumer is not returned properly.");
            Assert.True(expectedResult[0].Description == actualResult[0].Description, errorMessagePrefix + " " + "Description are not returned properly.");
        }

        [Fact]
        public async Task GetAllRooms_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomsServiceTestsSeeder();
            await seeder.SeedRoomAsync(context);
            var roomsRepository = new EfDeletableEntityRepository<Room>(context);
            var roomTypesRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomsService = this.GetRoomsService(roomsRepository, roomTypesRepository, context);

            // Act
            var actualResult = roomsService.GetAllRooms<AvailableRoomViewModel>().ToList();
            var expectedResult = new AvailableRoomViewModel[]
            {
                new AvailableRoomViewModel
                {
                     Id = roomsRepository.All().First().Id,
                     RoomNumber = "Test-1",
                     Description = "TestDescription",
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        private RoomsService GetRoomsService(EfDeletableEntityRepository<Room> roomRepository, EfDeletableEntityRepository<RoomType> roomTypeRepository, HotelDbContext context)
        {
            var roomsService = new RoomsService(
                roomRepository,
                roomTypeRepository);

            return roomsService;
        }
    }
}
