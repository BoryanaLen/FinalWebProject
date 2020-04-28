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
    using Hotel.Web.ViewModels.PaymentTypes;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PaymentTypesServicesTests
    {
        [Fact]
        public async Task AddPaymentTypeAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentTypesService AddPaymentTypeAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var paymentTypeModel = new PaymentType
            {
                Name = "Test",
            };

            // Act
            var result = await paymentTypesService.AddPaymentTypeAsync(paymentTypeModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddPaymentTypeAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "PaymentTypesService AddPaymentTypeAsync() method does not work properly.";

            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var paymentTypeModel = new PaymentType
            {
                Name = "Test",
            };

            // Act
            await paymentTypesService.AddPaymentTypeAsync(paymentTypeModel);
            var actualResult = paymentTypeRepository.All().First();
            var expectedResult = paymentTypeModel;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task AddPaymentTypeAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var paymentTypeModel = new PaymentType
            {
                Name = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.AddPaymentTypeAsync(paymentTypeModel);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentTypesService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            var paymentType = context.PaymentTypes.First();

            var model = new EditPaymentTypeViewModel
            {
                Id = paymentType.Id,
                Name = "Test-1-Edited",
            };

            // Act
            var result = await paymentTypesService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            var model = new EditPaymentTypeViewModel
            {
                Id = nonExistentId,
                Name = "Test-1-Edited",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            var paymentType = context.PaymentTypes.First();

            var model = new EditPaymentTypeViewModel
            {
                Id = paymentType.Id,
                Name = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessagePrefix = "PaymentTypeService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            var paymentType = context.PaymentTypes.First();

            var model = new EditPaymentTypeViewModel
            {
                Id = paymentType.Id,
                Name = "Test-1-Edited",
            };

            // Act
            await paymentTypesService.EditAsync(model);
            var actualResult = paymentTypeRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentTypeService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypesAsync(context);

            var paymentTypeId = paymentTypeRepository.All().First().Id;

            // Act
            var result = await paymentTypesService.DeleteByIdAsync(paymentTypeId);

            // Assert
            Assert.True(result, errorMessage + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "PaymentTypesService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypesAsync(context);

            var paymentTypeId = paymentTypeRepository.All().First().Id;

            // Act
            var paymentTypesCount = paymentTypeRepository.All().Count();
            await paymentTypesService.DeleteByIdAsync(paymentTypeId);
            var actualResult = paymentTypeRepository.All().Count();
            var expectedResult = paymentTypesCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Payment types count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetPaymentTypeByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentTypesService GetPaymentTypeByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            var paymentTypeId = paymentTypeRepository.All().First().Id;

            // Act
            var actualResult = await paymentTypesService.GetPaymentTypeByIdAsync(paymentTypeId);
            var expectedResult = await paymentTypeRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == paymentTypeId);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetPaymentTypeByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.GetPaymentTypeByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllPaymentTypesCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentTypesService GetAllPaymentTypesCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypesAsync(context);

            // Act
            var actualResult = await paymentTypesService.GetAllPaymentTypesCountAsync();
            var expectedResult = paymentTypeRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage + " " + "PaymentTypesService GetAllRoomTypesCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentTypesService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypesAsync(context);

            var paymentTypeId = paymentTypeRepository.All().First().Id;

            // Act
            var actualResult = await paymentTypesService.GetViewModelByIdAsync<EditPaymentTypeViewModel>(paymentTypeId);
            var expectedResult = new EditPaymentTypeViewModel
            {
                Id = paymentTypeId,
                Name = paymentTypeRepository.All().First().Name,
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
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.GetViewModelByIdAsync<EditPaymentTypeViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllPaymentTypesAsyn_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentTypesService GetAllPaymentTypesAsyn() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypesAsync(context);

            // Act
            var actualResult = paymentTypesService.GetAllPaymentTypes<DetailsPaymentTypeViewModel>().ToList();
            var expectedResult = new DetailsPaymentTypeViewModel[]
            {
                new DetailsPaymentTypeViewModel
                {
                     Id = paymentTypeRepository.All().First().Id,
                     Name = paymentTypeRepository.All().First().Name,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].Name == actualResult[0].Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetAllPaymentTypesAsyn_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            // Act
            var actualResult = paymentTypesService.GetAllPaymentTypes<DetailsPaymentTypeViewModel>().ToList();
            var expectedResult = new DetailsPaymentTypeViewModel[]
            {
                new DetailsPaymentTypeViewModel
                {
                     Id = paymentTypeRepository.All().First().Id,
                     Name = paymentTypeRepository.All().First().Name,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task CreateAllAsync_ShouldShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "PaymentTypesService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypesAsync(context);

            // Act
            var result = await paymentTypesService.CreateAllAsync(new string[] { "Test-1", "Test-2", "Test-3" });

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task CreateAllAsync_ShouldReturnCorrectCount()
        {
            var errorMessage = "PaymentTypesService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            // Act
            var paymentTypesCount = paymentTypeRepository.All().Count();
            await paymentTypesService.CreateAllAsync(new string[] { "Test-1" });
            var actualResult = paymentTypeRepository.All().Count();
            var expectedResult = paymentTypesCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage + " " + "Payment types count is not reduced.");
        }

        [Fact]
        public async Task CreateAllAsync_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentTypesService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            // Act
            await paymentTypesService.CreateAllAsync(new string[] { "Test-1" });
            var actualResult = new RoomType { Name = "Test-1" };
            var expectedResult = paymentTypeRepository.All().First();

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessage + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetPaymentTypeByNamec_WithExistentName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentTypesService GetPaymentTypeByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);
            var seeder = new PaymentTypesServiceTestsSeeder();
            await seeder.SeedPaymentTypeAsync(context);

            var paymentTypeName = paymentTypeRepository.All().First().Name;

            // Act
            var actualResult =await paymentTypesService.GetPaymentTypeByNameAsync(paymentTypeName);
            var expectedResult = await paymentTypeRepository
                .All()
                .SingleOrDefaultAsync(x => x.Name == paymentTypeName);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
        }

        [Fact]
        public async Task GetPaymentTypeByName_WithNonExistentName_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            var nonExistentName = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.GetPaymentTypeByNameAsync(nonExistentName);
            });
        }

        [Fact]
        public async Task GetPaymentTypeByName_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentTypeRepository = new EfDeletableEntityRepository<PaymentType>(context);
            var paymentTypesService = this.GetPaymentTypesService(paymentTypeRepository);

            string name = null;

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentTypesService.GetPaymentTypeByNameAsync(name);
            });
        }

        private PaymentTypesService GetPaymentTypesService(EfDeletableEntityRepository<PaymentType> paymentTypeRepository)
        {
            var paymentTypesService = new PaymentTypesService(paymentTypeRepository);

            return paymentTypesService;
        }
    }
}
