namespace Hotel.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;
    using Hotel.Data.Repositories;
    using Hotel.Services.Data.Tests.Common;
    using Hotel.Services.Data.Tests.Common.Seeders;
    using Hotel.Web.ViewModels.SpecialOffers;
    using Xunit;

    public class SpecialOffersServicesTests
    {
        [Fact]
        public async Task AddSpecialOfferAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "SpecialOffersService AddSpecialOfferAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedHotelAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOffer = new SpecialOffer
            {
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act
            var result = await specialOffersService.AddSpecialOfferAsync(specialOffer);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddSpecialOfferAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "SpecialOffersService AddSpecialOfferAsync() method does not work properly.";

            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedHotelAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOffer = new SpecialOffer
            {
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act
            await specialOffersService.AddSpecialOfferAsync(specialOffer);
            var actualResult = specialOfferRepository.All().First();
            var expectedResult = specialOffer;

            // Assert
            Assert.True(expectedResult.Title == actualResult.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResult.Content == actualResult.Content, errorMessagePrefix + " " + "Content is not returned properly.");
            Assert.True(expectedResult.ShortContent == actualResult.ShortContent, errorMessagePrefix + " " + "Short content is not returned properly.");
            Assert.True(expectedResult.HotelDataId == actualResult.HotelDataId, errorMessagePrefix + " " + "Hotel is not returned properly.");
        }

        [Fact]
        public async Task AddSpecialOfferAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOfferAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOffer = new SpecialOffer
            {
                Title = null,
                Content = null,
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await specialOffersService.AddSpecialOfferAsync(specialOffer);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "SpecialOffersService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOfferAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOffer = context.SpecialOffers.First();

            var model = new EditSpecialOfferViewModel
            {
                Id = specialOffer.Id,
                Title = "Title-Edited",
                Content = "Content-Edited",
                ShortContent = "ShortContenr-Edited",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act
            var result = await specialOffersService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedHotelAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            var model = new EditSpecialOfferViewModel
            {
                Id = nonExistentId,
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await specialOffersService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOfferAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOffer = context.SpecialOffers.First();

            var model = new EditSpecialOfferViewModel
            {
                Id = specialOffer.Id,
                Title = null,
                Content = "Content1",
                ShortContent = null,
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await specialOffersService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessagePrefix = "SpecialOffersService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOfferAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOffer = context.SpecialOffers.First();

            var model = new EditSpecialOfferViewModel
            {
                Id = specialOffer.Id,
                Title = "Title1-Edited",
                Content = "Content1-Edited",
                ShortContent = "ShortContenr1-Edited",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Act
            await specialOffersService.EditAsync(model);
            var actualResult = specialOfferRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.Title == actualResult.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResult.Content == actualResult.Content, errorMessagePrefix + " " + "Content is not returned properly.");
            Assert.True(expectedResult.ShortContent == actualResult.ShortContent, errorMessagePrefix + " " + "Short content is not returned properly.");
            Assert.True(expectedResult.HotelDataId == actualResult.HotelDataId, errorMessagePrefix + " " + "Hotel is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "SpecialOffersService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOffersAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOfferId = specialOfferRepository.All().First().Id;

            // Act
            var result = await specialOffersService.DeleteByIdAsync(specialOfferId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "SpecialOffersService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOffersAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOfferId = specialOfferRepository.All().First().Id;

            // Act
            var specialOffersCount = specialOfferRepository.All().Count();
            await specialOffersService.DeleteByIdAsync(specialOfferId);
            var actualResult = specialOfferRepository.All().Count();
            var expectedResult = specialOffersCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Special offers count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await specialOffersService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllSpecialOffersCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "SpecialOffersService GetAllSpecialOffersCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOffersAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            // Act
            var actualResult = await specialOffersService.GetAllSpecialOffersCountAsync();
            var expectedResult = specialOfferRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "SpecialOffersService GetAllSpecialOffersCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "SpecialOffersService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOffersAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var specialOfferId = specialOfferRepository.All().First().Id;

            // Act
            var actualResult = await specialOffersService.GetViewModelByIdAsync<EditSpecialOfferViewModel>(specialOfferId);
            var expectedResult = new EditSpecialOfferViewModel
            {
                Id = specialOfferId,
                Title = specialOfferRepository.All().First().Title,
                Content = specialOfferRepository.All().First().Content,
                ShortContent = specialOfferRepository.All().First().ShortContent,
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Title == actualResult.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResult.Content == actualResult.Content, errorMessagePrefix + " " + "Content is not returned properly.");
            Assert.True(expectedResult.ShortContent == actualResult.ShortContent, errorMessagePrefix + " " + "Short content is not returned properly.");
            Assert.True(expectedResult.HotelDataId == actualResult.HotelDataId, errorMessagePrefix + " " + "Hotel is not returned properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await specialOffersService.GetViewModelByIdAsync<EditSpecialOfferViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllSpecialOffers_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "SpecialOffersService GetAllSpecialOffers() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOfferAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            // Act
            var actualResult = specialOffersService.GetAllSpecialOffers<DetailsSpecialOfferViewModel>().ToList();
            var expectedResult = new DetailsSpecialOfferViewModel[]
            {
                new DetailsSpecialOfferViewModel
                {
                    Id = specialOfferRepository.All().First().Id,
                    Title = specialOfferRepository.All().First().Title,
                    Content = specialOfferRepository.All().First().Content,
                    ShortContent = specialOfferRepository.All().First().ShortContent,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].Title == actualResult[0].Title, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult[0].Content == actualResult[0].Content, errorMessagePrefix + " " + "Content is not returned properly.");
            Assert.True(expectedResult[0].ShortContent == actualResult[0].ShortContent, errorMessagePrefix + " " + "ShortContent is not returned properly.");
        }

        [Fact]
        public async Task GetAllSpecialOffers_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedSpecialOfferAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            // Act
            var actualResult = specialOffersService.GetAllSpecialOffers<DetailsSpecialOfferViewModel>().ToList();
            var expectedResult = new DetailsSpecialOfferViewModel[]
            {
                new DetailsSpecialOfferViewModel
                {
                    Id = specialOfferRepository.All().First().Id,
                    Title = specialOfferRepository.All().First().Title,
                    Content = specialOfferRepository.All().First().Content,
                    ShortContent = specialOfferRepository.All().First().ShortContent,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task CreateAllAsync_ShouldShouldSuccessfullyCreate()
        {
            var errorMessagePrefix = "SpecialOffersService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedHotelAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            // Act
            var specialOffer = new SpecialOffer
            {
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };
            var result = await specialOffersService.CreateAllAsync(new List<SpecialOffer> { specialOffer });

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task CreateAllAsync_ShouldReturnCorrectCount()
        {
            var errorMessage = "SpecialOffersService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedHotelAsync(context);
            await seeder.SeedSpecialOffersAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            // Act
            var specialOffer = new SpecialOffer
            {
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };
            var specialOffersCount = specialOfferRepository.All().Count();
            await specialOffersService.CreateAllAsync(new List<SpecialOffer> { specialOffer });
            var actualResult = specialOfferRepository.All().Count();
            var expectedResult = specialOffersCount + 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessage + " " + "Special offers count is not reduced.");
        }

        [Fact]
        public async Task CreateAllAsync_ShouldReturnCorrectResult()
        {
            var errorMessage = "SpecialOffersService CreateAllAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new SpecialOffersServiceTestsSeeder();
            await seeder.SeedHotelAsync(context);
            var specialOfferRepository = new EfDeletableEntityRepository<SpecialOffer>(context);
            var specialOffersService = this.GetSpecialOffersService(specialOfferRepository, context);

            // Act
            var specialOffer = new SpecialOffer
            {
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            await specialOffersService.CreateAllAsync(new List<SpecialOffer> { specialOffer });

            var actualResult = specialOfferRepository.All().First();
            var expectedResult = specialOffer;

            // Assert
            Assert.True(expectedResult.Title == actualResult.Title, errorMessage + " " + "Title is not returned properly.");
            Assert.True(expectedResult.Content == actualResult.Content, errorMessage + " " + "Content is not returned properly.");
            Assert.True(expectedResult.ShortContent == actualResult.ShortContent, errorMessage + " " + "Short content is not returned properly.");
            Assert.True(expectedResult.HotelDataId == actualResult.HotelDataId, errorMessage + " " + "Hotel is not returned properly.");
        }

        private SpecialOffersService GetSpecialOffersService(EfDeletableEntityRepository<SpecialOffer> specialOfferRepository, HotelDbContext context)
        {
            var specialOffersService = new SpecialOffersService(specialOfferRepository);

            return specialOffersService;
        }
    }
}
