namespace Hotel.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;
    using Hotel.Data.Repositories;
    using Hotel.Services.Data.Tests.Common;
    using Hotel.Services.Data.Tests.Common.Seeders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class HotelsServiceTests
    {
        [Fact]
        public async Task AddRHotelAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "HotelsService AddRHotelAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var hotelRepository = new EfDeletableEntityRepository<HotelData>(context);
            var hotelsService = this.GetHotelsService(hotelRepository);

            var hotel = new HotelData
            {
                Name = "Test",
                Address = "Bulgaria",
                UniqueIdentifier = "123456789",
                Owner = "Owner",
                Manager = "Manager",
                PhoneNumber = "123456789",
            };

            // Act
            var result = await hotelsService.AddRHotelAsync(hotel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddRHotelAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "HotelsService AddRHotelAsync() method does not work properly.";

            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var hotelRepository = new EfDeletableEntityRepository<HotelData>(context);
            var hotelsService = this.GetHotelsService(hotelRepository);

            var hotel = new HotelData
            {
                Name = "Test",
                Address = "Bulgaria",
                UniqueIdentifier = "123456789",
                Owner = "Owner",
                Manager = "Manager",
                PhoneNumber = "123456789",
            };

            // Act
            await hotelsService.AddRHotelAsync(hotel);
            var actualResult = hotelRepository.All().First();
            var expectedResult = hotel;

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Address == actualResult.Address, errorMessagePrefix + " " + "Address is not returned properly.");
            Assert.True(expectedResult.UniqueIdentifier == actualResult.UniqueIdentifier, errorMessagePrefix + " " + "UniqueIdentifier is not returned properly.");
            Assert.True(expectedResult.Owner == actualResult.Owner, errorMessagePrefix + " " + "Owner is not returned properly.");
            Assert.True(expectedResult.Manager == actualResult.Manager, errorMessagePrefix + " " + "Manager is not returned properly.");
            Assert.True(expectedResult.PhoneNumber == actualResult.PhoneNumber, errorMessagePrefix + " " + "Phone number is not returned properly.");
        }

        [Fact]
        public async Task AddRHotelAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var hotelRepository = new EfDeletableEntityRepository<HotelData>(context);
            var hotelsService = this.GetHotelsService(hotelRepository);

            var hotel = new HotelData
            {
                Name = null,
                Address = "Bulgaria",
                UniqueIdentifier = null,
                Owner = "Owner",
                Manager = "Manager",
                PhoneNumber = "123456789",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await hotelsService.AddRHotelAsync(hotel);
            });
        }

        [Fact]
        public async Task GetHotelTypeByNamec_WithExistentName_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "HotelsService GetHotelByName() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var hotelRepository = new EfDeletableEntityRepository<HotelData>(context);
            var hotelsService = this.GetHotelsService(hotelRepository);
            var seeder = new HotelsServiceTestsSeeder();
            await seeder.SeedHotelTypeAsync(context);

            var hotelName = hotelRepository.All().First().Name;

            // Act
            var actualResult = hotelsService.GetHotelByName(hotelName);
            var expectedResult = await hotelRepository
                .All()
                .SingleOrDefaultAsync(x => x.Name == hotelName);

            // Assert
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Address == actualResult.Address, errorMessagePrefix + " " + "Address is not returned properly.");
            Assert.True(expectedResult.UniqueIdentifier == actualResult.UniqueIdentifier, errorMessagePrefix + " " + "UniqueIdentifier is not returned properly.");
            Assert.True(expectedResult.Owner == actualResult.Owner, errorMessagePrefix + " " + "Owner is not returned properly.");
            Assert.True(expectedResult.Manager == actualResult.Manager, errorMessagePrefix + " " + "Manager is not returned properly.");
            Assert.True(expectedResult.PhoneNumber == actualResult.PhoneNumber, errorMessagePrefix + " " + "Phone number is not returned properly.");
        }

        [Fact]
        public async Task GetHotelByName_WithNonExistentName_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var hotelRepository = new EfDeletableEntityRepository<HotelData>(context);
            var hotelsService = this.GetHotelsService(hotelRepository);

            var nonExistentName = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                hotelsService.GetHotelByName(nonExistentName);
            });
        }

        [Fact]
        public async Task GetPaymentTypeByName_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var hotelRepository = new EfDeletableEntityRepository<HotelData>(context);
            var hotelsService = this.GetHotelsService(hotelRepository);

            string name = null;

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                hotelsService.GetHotelByName(name);
            });
        }


        private HotelsService GetHotelsService(EfDeletableEntityRepository<HotelData> hotelRepository)
        {
            var hotelService = new HotelsService(hotelRepository);

            return hotelService;
        }
    }
}
