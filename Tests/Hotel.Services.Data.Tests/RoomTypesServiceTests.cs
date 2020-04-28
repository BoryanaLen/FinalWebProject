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
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RoomTypesServiceTests
    {
        [Fact]
        public async Task AddRoomTypeAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypesService AddRoomTypeAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeModel = new RoomType
            {
                Name = "Test-1",
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test1.jpg",
                Description = "Description1",
            };

            // Act
            var result = await roomTypesService.AddRoomTypeAsync(roomTypeModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddRoomTypeAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "RoomTypeService AddRoomTypeAsync() method does not work properly.";

            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeModel = new RoomType
            {
                Name = "Test-1",
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test1.jpg",
                Description = "Description1",
            };

            // Act
            await roomTypesService.AddRoomTypeAsync(roomTypeModel);
            var actualResult = roomTypeRepository.All().First();
            var expectedResult = roomTypeModel;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Price == actualResult.Price, errorMessagePrefix + " " + "Price is not returned properly.");
            Assert.True(expectedResult.CapacityAdults == actualResult.CapacityAdults, errorMessagePrefix + " " + "Capacity Adults is not returned properly.");
            Assert.True(expectedResult.CapacityKids == actualResult.CapacityKids, errorMessagePrefix + " " + "Capacity Kids is not returned properly.");
            Assert.True(expectedResult.Image == actualResult.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
        }

        [Fact]
        public async Task AddRoomTypeAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypeAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeModel = new RoomType
            {
                Name = null,
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test1.jpg",
                Description = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomTypesService.AddRoomTypeAsync(roomTypeModel);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "RoomTypesService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypeAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomType = context.RoomTypes.First();

            var model = new EditRoomTypeViewModel
            {
                Id = roomType.Id,
                Name = "Test-1",
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test1.jpg",
                Description = "Description1",
            };

            // Act
            var result = await roomTypesService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessage + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            var model = new EditRoomTypeViewModel
            {
                Id = nonExistentId,
                Name = "Test-1",
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test1.jpg",
                Description = "Description1",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomTypesService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypeAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomType = context.RoomTypes.First();

            var model = new EditRoomTypeViewModel
            {
                Id = roomType.Id,
                Name = null,
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = null,
                Description = "Description1",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomTypesService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessagePrefix = "RoomTypeService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypeAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomType = context.RoomTypes.First();

            var model = new EditRoomTypeViewModel
            {
                Id = roomType.Id,
                Name = "Test-2-Edited",
                Price = 130,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test2-edited.jpg",
                Description = "Description2-edited",
            };

            // Act
            await roomTypesService.EditAsync(model);
            var actualResult = roomTypeRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Price == actualResult.Price, errorMessagePrefix + " " + "Price is not returned properly.");
            Assert.True(expectedResult.CapacityAdults == actualResult.CapacityAdults, errorMessagePrefix + " " + "Capacity Adults is not returned properly.");
            Assert.True(expectedResult.CapacityKids == actualResult.CapacityKids, errorMessagePrefix + " " + "Capacity Kids is not returned properly.");
            Assert.True(expectedResult.Image == actualResult.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypeService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypesAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeId = roomTypeRepository.All().First().Id;

            // Act
            var result = await roomTypesService.DeleteByIdAsync(roomTypeId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "RoomTypesService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypesAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeId = roomTypeRepository.All().First().Id;

            // Act
            var roomTypesCount = roomTypeRepository.All().Count();
            await roomTypesService.DeleteByIdAsync(roomTypeId);
            var actualResult = roomTypeRepository.All().Count();
            var expectedResult = roomTypesCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Room types count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomTypesService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetRoomTypeByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypesService GetRoomTypeByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypesAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeId = roomTypeRepository.All().First().Id;

            // Act
            var actualResult = await roomTypesService.GetRoomTypeByIdAsync(roomTypeId);
            var expectedResult = await roomTypeRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == roomTypeId);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Price == actualResult.Price, errorMessagePrefix + " " + "Price is not returned properly.");
            Assert.True(expectedResult.CapacityAdults == actualResult.CapacityAdults, errorMessagePrefix + " " + "Capacity Adults is not returned properly.");
            Assert.True(expectedResult.CapacityKids == actualResult.CapacityKids, errorMessagePrefix + " " + "Capacity Kids is not returned properly.");
            Assert.True(expectedResult.Image == actualResult.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
        }

        [Fact]
        public async Task GetRoomTypeByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomTypesService.GetRoomTypeByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllRoomTypesCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypesService GetAllRoomTypesCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypesAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            // Act
            var actualResult = await roomTypesService.GetAllRoomTypesCountAsync();
            var expectedResult = roomTypeRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "RoomTypesService GetAllRoomTypesCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypesService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypesAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeId = roomTypeRepository.All().First().Id;

            // Act
            var actualResult = await roomTypesService.GetViewModelByIdAsync<EditRoomTypeViewModel>(roomTypeId);
            var expectedResult = new EditRoomTypeViewModel
            {
                Id = roomTypeId,
                Name = roomTypeRepository.All().First().Name,
                CapacityAdults = roomTypeRepository.All().First().CapacityAdults,
                CapacityKids = roomTypeRepository.All().First().CapacityKids,
                Image = roomTypeRepository.All().First().Image,
                Description = roomTypeRepository.All().First().Description,
                Price = roomTypeRepository.All().First().Price,
            };

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Price == actualResult.Price, errorMessagePrefix + " " + "Price is not returned properly.");
            Assert.True(expectedResult.CapacityAdults == actualResult.CapacityAdults, errorMessagePrefix + " " + "Capacity Adults is not returned properly.");
            Assert.True(expectedResult.CapacityKids == actualResult.CapacityKids, errorMessagePrefix + " " + "Capacity Kids is not returned properly.");
            Assert.True(expectedResult.Image == actualResult.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await roomTypesService.GetViewModelByIdAsync<EditRoomTypeViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllRoomTypesAsyn_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypesService GetAllRoomTypesAsyn() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypeAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            // Act
            var actualResult = roomTypesService.GetAllRoomTypes<DetailsRoomTypeViewModel>().ToList();
            var expectedResult = new DetailsRoomTypeViewModel[]
            {
                new DetailsRoomTypeViewModel
                {
                     Id = roomTypeRepository.All().First().Id,
                     Name = roomTypeRepository.All().First().Name,
                     CapacityAdults = roomTypeRepository.All().First().CapacityAdults,
                     CapacityKids = roomTypeRepository.All().First().CapacityKids,
                     Image = roomTypeRepository.All().First().Image,
                     Description = roomTypeRepository.All().First().Description,
                     Price = roomTypeRepository.All().First().Price,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].Name == actualResult[0].Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult[0].Price == actualResult[0].Price, errorMessagePrefix + " " + "Price is not returned properly.");
            Assert.True(expectedResult[0].CapacityAdults == actualResult[0].CapacityAdults, errorMessagePrefix + " " + "Capacity Adults is not returned properly.");
            Assert.True(expectedResult[0].CapacityKids == actualResult[0].CapacityKids, errorMessagePrefix + " " + "Capacity Kids is not returned properly.");
            Assert.True(expectedResult[0].Image == actualResult[0].Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResult[0].Description == actualResult[0].Description, errorMessagePrefix + " " + "Description is not returned properly.");
        }

        [Fact]
        public async Task GetAllRoomTypesAsyn_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypeAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            // Act
            var actualResult = roomTypesService.GetAllRoomTypes<DetailsRoomTypeViewModel>().ToList();
            var expectedResult = new DetailsRoomTypeViewModel[]
            {
                new DetailsRoomTypeViewModel
                {
                     Id = roomTypeRepository.All().First().Id,
                     Name = roomTypeRepository.All().First().Name,
                     CapacityAdults = roomTypeRepository.All().First().CapacityAdults,
                     CapacityKids = roomTypeRepository.All().First().CapacityKids,
                     Image = roomTypeRepository.All().First().Image,
                     Description = roomTypeRepository.All().First().Description,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task GetRoomTypeByNamec_WithExistentName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "RoomTypesService GetRoomTypeByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new RoomTypesServiceTestsSeeder();
            await seeder.SeedRoomTypesAsync(context);
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var roomTypeName = roomTypeRepository.All().First().Name;

            // Act
            var actualResult = roomTypesService.GetRoomTypeByName(roomTypeName);
            var expectedResult = await roomTypeRepository
                .All()
                .SingleOrDefaultAsync(x => x.Name == roomTypeName);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Price == actualResult.Price, errorMessagePrefix + " " + "Price is not returned properly.");
            Assert.True(expectedResult.CapacityAdults == actualResult.CapacityAdults, errorMessagePrefix + " " + "Capacity Adults is not returned properly.");
            Assert.True(expectedResult.CapacityKids == actualResult.CapacityKids, errorMessagePrefix + " " + "Capacity Kids is not returned properly.");
            Assert.True(expectedResult.Image == actualResult.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
        }

        [Fact]
        public async Task GetRoomTypeByName_WithNonExistentName_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            var nonExistentName = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                roomTypesService.GetRoomTypeByName(nonExistentName);
            });
        }

        [Fact]
        public async Task GetRoomTypeByName_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var roomTypeRepository = new EfDeletableEntityRepository<RoomType>(context);
            var roomTypesService = this.GetRoomTypesService(roomTypeRepository, context);

            string name = null;

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                roomTypesService.GetRoomTypeByName(name);
            });
        }

        private RoomTypesService GetRoomTypesService(EfDeletableEntityRepository<RoomType> roomTypeRepository, HotelDbContext context)
        {
            var roomTypesService = new RoomTypesService(roomTypeRepository);

            return roomTypesService;
        }
    }
}
