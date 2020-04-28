namespace Hotel.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Data.Repositories;
    using Hotel.Services.Data.Tests.Common;
    using Hotel.Services.Data.Tests.Common.Seeders;
    using Hotel.Web.ViewModels.Payments;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PaymentsServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentsService AddPaymentAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentModel = new Payment
            {
                DateOfPayment = DateTime.Now,
                Amount = 300,
                PaymentTypeId = context.PaymentTypes.First().Id,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            // Act
            var result = await paymentsService.AddPaymentAsync(paymentModel);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            var errorMessage = "PaymentsService AddPaymentAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentModel = new Payment
            {
                DateOfPayment = DateTime.Now.Date,
                Amount = 300,
                PaymentTypeId = context.PaymentTypes.First().Id,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            // Act
            await paymentsService.AddPaymentAsync(paymentModel);
            var actualResult = paymentRepository.All().First();
            var expectedResult = paymentModel;

            // Assert
            Assert.True(expectedResult.DateOfPayment == actualResult.DateOfPayment, errorMessage + " " + "Date of payment is not returned properly.");
            Assert.True(expectedResult.Amount == actualResult.Amount, errorMessage + " " + "Amount is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessage + " " + "payment type id is not returned properly.");
        }

        [Fact]
        public async Task AddAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentModel = new Payment
            {
                DateOfPayment = DateTime.Now,
                Amount = 300,
                PaymentTypeId = null,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentsService.AddPaymentAsync(paymentModel);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentsService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var payment = context.Payments.First();

            var model = new EditPaymentViewModel
            {
                Id = payment.Id,
                DateOfPayment = DateTime.Now.Date,
                Amount = 300,
                PaymentTypeId = context.PaymentTypes.First().Id,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            // Act
            var result = await paymentsService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessage + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);

            var nonExistentId = Guid.NewGuid().ToString();

            var model = new EditPaymentViewModel
            {
                Id = nonExistentId,
                DateOfPayment = DateTime.Now.Date,
                Amount = 300,
                PaymentTypeId = context.PaymentTypes.First().Id,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentsService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var payment = context.Payments.First();

            var model = new EditPaymentViewModel
            {
                Id = payment.Id,
                DateOfPayment = DateTime.Now.Date,
                Amount = 300,
                PaymentTypeId = null,
                ReservationPayments = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentsService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessage = "PaymentsService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var payment = context.Payments.First();

            var model = new EditPaymentViewModel
            {
                Id = payment.Id,
                DateOfPayment = DateTime.Now.Date,
                Amount = 300,
                PaymentTypeId = context.PaymentTypes.First().Id,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            // Act
            await paymentsService.EditAsync(model);
            var actualResult = paymentRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.DateOfPayment == actualResult.DateOfPayment, errorMessage + " " + "Date of payment is not returned properly.");
            Assert.True(expectedResult.Amount == actualResult.Amount, errorMessage + " " + "Amount is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessage + " " + "payment type id is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentsService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentsAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentId = paymentRepository.All().First().Id;

            // Act
            var result = await paymentsService.DeleteByIdAsync(paymentId);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldSuccessfullyDelete()
        {
            var errorMessagePrefix = "PaymentsService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentsAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentId = paymentRepository.All().First().Id;

            // Act
            var paymentsCount = paymentRepository.All().Count();
            await paymentsService.DeleteByIdAsync(paymentId);
            var actualResult = paymentRepository.All().Count();
            var expectedResult = paymentsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Payments count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentsService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetPaymentByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentsService GetPaymentByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentId = paymentRepository.All().First().Id;

            // Act
            var actualResult = await paymentsService.GetPaymentByIdAsync(paymentId);
            var expectedResult = await paymentRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == paymentId);

            // Assert
            Assert.True(expectedResult.DateOfPayment == actualResult.DateOfPayment, errorMessage + " " + "Date of payment is not returned properly.");
            Assert.True(expectedResult.Amount == actualResult.Amount, errorMessage + " " + "Amount is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessage + " " + "payment type id is not returned properly.");
        }

        [Fact]
        public async Task GetPaymentByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentsService.GetPaymentByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllPaymentsCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "PaymentsService GetAllPaymentsCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            // Act
            var actualResult = await paymentsService.GetAllPaymentsCountAsync();
            var expectedResult = paymentRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "RoomTypesService GetAllRoomTypesCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentsService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var paymentId = paymentRepository.All().First().Id;

            // Act
            var actualResult = await paymentsService.GetViewModelByIdAsync<EditPaymentViewModel>(paymentId);
            var expectedResult = new EditPaymentViewModel
            {
                Id = paymentId,
                DateOfPayment = paymentRepository.All().First().DateOfPayment,
                Amount = paymentRepository.All().First().Amount,
                PaymentTypeId = paymentRepository.All().First().PaymentTypeId,
            };

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessage + " " + "Id is not returned properly.");
            Assert.True(expectedResult.DateOfPayment == actualResult.DateOfPayment, errorMessage + " " + "Date of payment is not returned properly.");
            Assert.True(expectedResult.Amount == actualResult.Amount, errorMessage + " " + "Amount is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessage + " " + "payment type id is not returned properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await paymentsService.GetViewModelByIdAsync<EditPaymentViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllPaymentsViewModels_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentsService GetAllPayments() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            // Act
            var actualResult = paymentsService.GetAllPayments<DetailsPaymentViewModel>().ToList();
            var expectedResult = new DetailsPaymentViewModel[]
            {
                new DetailsPaymentViewModel
                {
                     Id = paymentRepository.All().First().Id,
                     DateOfPayment = paymentRepository.All().First().DateOfPayment,
                     Amount = paymentRepository.All().First().Amount,
                     PaymentTypeId = paymentRepository.All().First().PaymentTypeId,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessage + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].DateOfPayment == actualResult[0].DateOfPayment, errorMessage + " " + "Date of payment is not returned properly.");
            Assert.True(expectedResult[0].Amount == actualResult[0].Amount, errorMessage + " " + "Amount is not returned properly.");
            Assert.True(expectedResult[0].PaymentTypeId == actualResult[0].PaymentTypeId, errorMessage + " " + "payment type id is not returned properly.");
        }

        [Fact]
        public async Task GetAllPaymentsViewModels_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            // Act
            var actualResult = paymentsService.GetAllPayments<EditPaymentViewModel>().ToList();
            var expectedResult = new EditPaymentViewModel[]
            {
                new EditPaymentViewModel
                {
                     Id = paymentRepository.All().First().Id,
                     DateOfPayment = paymentRepository.All().First().DateOfPayment,
                     Amount = paymentRepository.All().First().Amount,
                     PaymentTypeId = paymentRepository.All().First().PaymentTypeId,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task GetAllPayments_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentsService GetAllPayments() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            // Act
            var actualResult = paymentsService.GetAllPayments().ToList();
            var expectedResult = new Payment[]
            {
                new Payment
                {
                     Id = paymentRepository.All().First().Id,
                     DateOfPayment = paymentRepository.All().First().DateOfPayment,
                     Amount = paymentRepository.All().First().Amount,
                     PaymentTypeId = paymentRepository.All().First().PaymentTypeId,
                },
            };

            Assert.True(expectedResult[0].Id == actualResult[0].Id, errorMessage + " " + "Id is not returned properly.");
            Assert.True(expectedResult[0].DateOfPayment == actualResult[0].DateOfPayment, errorMessage + " " + "Date of payment is not returned properly.");
            Assert.True(expectedResult[0].Amount == actualResult[0].Amount, errorMessage + " " + "Amount is not returned properly.");
            Assert.True(expectedResult[0].PaymentTypeId == actualResult[0].PaymentTypeId, errorMessage + " " + "payment type id is not returned properly.");
        }

        [Fact]
        public async Task GetAllPayments_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            // Act
            var actualResult = paymentsService.GetAllPayments().ToList();
            var expectedResult = new Payment[]
            {
                new Payment
                {
                     Id = paymentRepository.All().First().Id,
                     DateOfPayment = paymentRepository.All().First().DateOfPayment,
                     Amount = paymentRepository.All().First().Amount,
                     PaymentTypeId = paymentRepository.All().First().PaymentTypeId,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task GetAllPaymentsForReservation_ShouldReturnCorrectResult()
        {
            var errorMessage = "PaymentsService GetAllPaymentsForReservation() method does not work properly.";

            // Arrange
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var reservation = context.Reservations.First();

            // Act
            var actualResult = paymentsService.GetAllPaymentsForReservation(reservation.Id).ToList();
            var expectedResult = new string[] { paymentRepository.All().First().ReservationPayments.First().PaymentId };

            Assert.True(expectedResult[0] == actualResult[0], errorMessage + " " + "Id is not returned properly.");
        }

        [Fact]
        public async Task GetAllPaymentsForReservation_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var seeder = new PaymentsServiceTestsSeeder();
            await seeder.SeedPaymentAsync(context);
            var paymentRepository = new EfDeletableEntityRepository<Payment>(context);
            var paymentsService = this.GetPaymentsService(paymentRepository);

            var reservation = context.Reservations.First();

            // Act
            var actualResult = paymentsService.GetAllPaymentsForReservation(reservation.Id).ToList();
            var expectedResult = 1;

            Assert.Equal(expectedResult, actualResult.Count());
        }

        private PaymentsService GetPaymentsService(EfDeletableEntityRepository<Payment> paymentRepository)
        {
            var paymentsService = new PaymentsService(
                paymentRepository);

            return paymentsService;
        }
    }
}
