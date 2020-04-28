namespace Hotel.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;
    using Hotel.Data.Repositories;
    using Hotel.Services.Data.Tests.Common;
    using Hotel.Services.Data.Tests.Common.Seeders;
    using Hotel.Web.ViewModels.Administration.Dashboard;
    using Hotel.Web.ViewModels.Reservations;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ReservationsServiceTests
    {
        [Fact]
        public async Task AddReservationAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService  AddReservationAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation
            {
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            // Act
            var result = await reservationsService.AddReservationAsync(reservation);

            // Assert
            Assert.True(result, errorMessagePrefix + " " + "Returns false.");
        }

        [Fact]
        public async Task AddReservationAsync_WithCorrectData_ShouldSuccessfullyCreate()
        {
            MapperInitializer.InitializeMapper();
            var errorMessagePrefix = "ReservationsService  AddReservationAsync() method does not work properly.";
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation
            {
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            // Act
            await reservationsService.AddReservationAsync(reservation);
            var actualResult = reservationRepository.All().First();
            var expectedResult = reservation;

            // Assert
            Assert.True(expectedResult.UserId == actualResult.UserId, errorMessagePrefix + " " + "User is not returned properly.");
            Assert.True(expectedResult.StartDate == actualResult.StartDate, errorMessagePrefix + " " + "Start date is not returned properly.");
            Assert.True(expectedResult.EndDate == actualResult.EndDate, errorMessagePrefix + " " + "End date is not returned properly.");
            Assert.True(expectedResult.Adults == actualResult.Adults, errorMessagePrefix + " " + "Adults is not returned properly.");
            Assert.True(expectedResult.Kids == actualResult.Kids, errorMessagePrefix + " " + "Kids is not returned properly.");
            Assert.True(expectedResult.TotalAmount == actualResult.TotalAmount, errorMessagePrefix + " " + "Total amount is not returned properly.");
            Assert.True(expectedResult.ReservationStatusId == actualResult.ReservationStatusId, errorMessagePrefix + " " + "Reservation status Id is not returned properly.");
            Assert.True(expectedResult.PricePerDay == actualResult.PricePerDay, errorMessagePrefix + " " + "Price per day is not returned properly.");
            Assert.True(expectedResult.AdvancedPayment == actualResult.AdvancedPayment, errorMessagePrefix + " " + "Advanced payment is not returned properly.");
            Assert.True(expectedResult.TotalDays == actualResult.TotalDays, errorMessagePrefix + " " + "Total days is not returned properly.");
            Assert.True(expectedResult.ReservationRooms.First().RoomId == actualResult.ReservationRooms.First().RoomId, errorMessagePrefix + " " + "First room id in ReservationRooms status is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessagePrefix + " " + "Payment type Id is not returned properly.");
        }

        [Fact]
        public async Task AddReservationAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation
            {
                UserId = null,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = null,
                PaymentTypeId = null,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationsService.AddReservationAsync(reservation);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldReturnCorrectResult()
        {
            var errorMessage = "ReservationsService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservation = context.Reservations.First();

            var model = new EditReservationViewModel
            {
                Id = reservation.Id,
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                TotalDays = 4,
                AdvancedPayment = 300,
                RoomId = context.Rooms.First().Id,
            };

            // Act
            var result = await reservationsService.EditAsync(model);

            // Assert
            Assert.True(result, errorMessage + " " + "Returns false.");
        }

        [Fact]
        public async Task EditAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var nonExistentId = Guid.NewGuid().ToString();

            var model = new EditReservationViewModel
            {
                Id = nonExistentId,
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                TotalDays = 4,
                AdvancedPayment = 300,
                RoomId = context.Rooms.First().Id,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationsService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithIncorrectProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservation = context.Reservations.First();

            var model = new EditReservationViewModel
            {
                Id = reservation.Id,
                UserId = null,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = null,
                PaymentTypeId = null,
                TotalAmount = 1000,
                TotalDays = 4,
                AdvancedPayment = 300,
                RoomId = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationsService.EditAsync(model);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var errorMessagePrefix = "ReservationsService EditAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservation = context.Reservations.First();

            var model = new EditReservationViewModel
            {
                Id = reservation.Id,
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                TotalDays = 4,
                AdvancedPayment = 300,
                RoomId = context.Rooms.First().Id,
            };

            // Act
            await reservationsService.EditAsync(model);
            var actualResult = reservationRepository.All().First();
            var expectedResult = model;

            // Assert
            Assert.True(expectedResult.UserId == actualResult.UserId, errorMessagePrefix + " " + "User is not returned properly.");
            Assert.True(expectedResult.StartDate == actualResult.StartDate, errorMessagePrefix + " " + "Start date is not returned properly.");
            Assert.True(expectedResult.EndDate == actualResult.EndDate, errorMessagePrefix + " " + "End date is not returned properly.");
            Assert.True(expectedResult.Adults == actualResult.Adults, errorMessagePrefix + " " + "Adults is not returned properly.");
            Assert.True(expectedResult.Kids == actualResult.Kids, errorMessagePrefix + " " + "Kids is not returned properly.");
            Assert.True(expectedResult.TotalAmount == actualResult.TotalAmount, errorMessagePrefix + " " + "Total amount is not returned properly.");
            Assert.True(expectedResult.ReservationStatusId == actualResult.ReservationStatusId, errorMessagePrefix + " " + "Reservation status Id is not returned properly.");
            Assert.True(expectedResult.AdvancedPayment == actualResult.AdvancedPayment, errorMessagePrefix + " " + "Advanced payment is not returned properly.");
            Assert.True(expectedResult.TotalDays == actualResult.TotalDays, errorMessagePrefix + " " + "Total days is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessagePrefix + " " + "Payment type Id is not returned properly.");
            Assert.True(expectedResult.ReservationStatusId == actualResult.ReservationStatusId, errorMessagePrefix + " " + "Reservation status is not returned properly.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService DeleteByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservationId = context.Reservations.First().Id;

            // Act
            var result = await reservationsService.DeleteByIdAsync(reservationId);

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
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservationId = context.Reservations.First().Id;

            // Act
            var reservationsCount = reservationRepository.All().Count();
            await reservationsService.DeleteByIdAsync(reservationId);
            var actualResult = reservationRepository.All().Count();
            var expectedResult = reservationsCount - 1;

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "Reservation ststuses count is not reduced.");
        }

        [Fact]
        public async Task DeleteByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationsService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllReservationsCountAsync_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService GetAllReservationsCountAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();

            await seeder.SeedReservationForGetReservedRoomsAsync(context);

            // Act
            var actualResult = await reservationsService.GetAllReservationsCountAsync();
            var expectedResult = reservationRepository.All().Count();

            // Assert
            Assert.True(actualResult == expectedResult, errorMessagePrefix + " " + "ReservationsService GetAllReservationsCountAsync() method does not work properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService GetViewModelByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservationId = context.Reservations.First().Id;

            // Act
            var actualResult = await reservationsService.GetViewModelByIdAsync<EditReservationViewModel>(reservationId);
            var expectedResult = new EditReservationViewModel
            {
                Id = reservationId,
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                TotalDays = 4,
                AdvancedPayment = 300,
                RoomId = context.Rooms.First().Id,
            };

            // Assert
            Assert.True(expectedResult.UserId == actualResult.UserId, errorMessagePrefix + " " + "User is not returned properly.");
            Assert.True(expectedResult.StartDate == actualResult.StartDate, errorMessagePrefix + " " + "Start date is not returned properly.");
            Assert.True(expectedResult.EndDate == actualResult.EndDate, errorMessagePrefix + " " + "End date is not returned properly.");
            Assert.True(expectedResult.Adults == actualResult.Adults, errorMessagePrefix + " " + "Adults is not returned properly.");
            Assert.True(expectedResult.Kids == actualResult.Kids, errorMessagePrefix + " " + "Kids is not returned properly.");
            Assert.True(expectedResult.TotalAmount == actualResult.TotalAmount, errorMessagePrefix + " " + "Total amount is not returned properly.");
            Assert.True(expectedResult.ReservationStatusId == actualResult.ReservationStatusId, errorMessagePrefix + " " + "Reservation status is not returned properly.");
        }

        [Fact]
        public async Task GetViewModelByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationsService.GetViewModelByIdAsync<EditReservationViewModel>(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService GetAllReservations() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            // Act
            var actualResult = reservationsService.GetAllReservations<AddReservationViewModel>().ToList();
            var expectedResult = new AddReservationViewModel[]
            {
                new AddReservationViewModel
                {
                    StartDate = new DateTime(2020, 4, 4),
                    EndDate = new DateTime(2020, 4, 8),
                    Adults = 2,
                    Kids = 1,
                    ReservationStatusId = context.ReservationStatuses.First().Id,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                },
            };

            Assert.True(expectedResult[0].StartDate == actualResult[0].StartDate, errorMessagePrefix + " " + "Start date is not returned properly.");
            Assert.True(expectedResult[0].EndDate == actualResult[0].EndDate, errorMessagePrefix + " " + "End date is not returned properly.");
            Assert.True(expectedResult[0].Adults == actualResult[0].Adults, errorMessagePrefix + " " + "Adults is not returned properly.");
            Assert.True(expectedResult[0].Kids == actualResult[0].Kids, errorMessagePrefix + " " + "Kids is not returned properly.");            
            Assert.True(expectedResult[0].ReservationStatusId == actualResult[0].ReservationStatusId, errorMessagePrefix + " " + "Reservation status is not returned properly.");
            Assert.True(expectedResult[0].PaymentTypeId == actualResult[0].PaymentTypeId, errorMessagePrefix + " " + "Payment type id is not returned properly.");
        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            // Act
            var actualResult = reservationsService.GetAllReservations<AddReservationViewModel>().ToList();
            var expectedResult = new AddReservationViewModel[]
            {
                 new AddReservationViewModel
                {
                     StartDate = new DateTime(2020, 4, 4),
                     EndDate = new DateTime(2020, 4, 8),
                     Adults = 2,
                     Kids = 1,
                     ReservationStatusId = context.ReservationStatuses.First().Id,
                     PaymentTypeId = context.PaymentTypes.First().Id,
                },
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task GetReservationByIdAsync_WithExistentId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService GetReservationByIdAsync() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            var reservationId = reservationRepository.All().First().Id;

            // Act
            var actualResult = await reservationsService.GetReservationByIdAsync(reservationId);
            var expectedResult = await reservationRepository
                .All()
                .SingleOrDefaultAsync(x => x.Id == reservationId);

            // Assert
            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.UserId == actualResult.UserId, errorMessagePrefix + " " + "User is not returned properly.");
            Assert.True(expectedResult.StartDate == actualResult.StartDate, errorMessagePrefix + " " + "Start date is not returned properly.");
            Assert.True(expectedResult.EndDate == actualResult.EndDate, errorMessagePrefix + " " + "End date is not returned properly.");
            Assert.True(expectedResult.Adults == actualResult.Adults, errorMessagePrefix + " " + "Adults is not returned properly.");
            Assert.True(expectedResult.Kids == actualResult.Kids, errorMessagePrefix + " " + "Kids is not returned properly.");
            Assert.True(expectedResult.TotalAmount == actualResult.TotalAmount, errorMessagePrefix + " " + "Total amount is not returned properly.");
            Assert.True(expectedResult.PaymentTypeId == actualResult.PaymentTypeId, errorMessagePrefix + " " + "Payment type id is not returned properly.");
            Assert.True(expectedResult.Kids == actualResult.Kids, errorMessagePrefix + " " + "Kids is not returned properly.");            
            Assert.True(expectedResult.TotalAmount == actualResult.TotalAmount, errorMessagePrefix + " " + "Total amount is not returned properly.");
            Assert.True(expectedResult.ReservationStatusId == actualResult.ReservationStatusId, errorMessagePrefix + " " + "Reservation status is not returned properly.");
        }

        [Fact]
        public async Task GetReservationByIdAsync_WithNonExistentId_ShouldThrowArgumentNullException()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);

            var nonExistentId = Guid.NewGuid().ToString();

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await reservationsService.GetReservationByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task GetAllReservedRoomsId_ShouldReturnCorrectResult()
        {
            var errorMessagePrefix = "ReservationsService GetAllReservedRooms() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            // Act
            var actualResult = reservationsService.GetAllReservedRoomsId(new DateTime(2020, 4, 4), new DateTime(2020, 4, 8)).ToList();
            var expectedResult = new string[] { context.Rooms.First().Id, };

            Assert.True(expectedResult[0] == actualResult[0], errorMessagePrefix + " " + "Id is not returned properly.");
        }

        [Fact]
        public async Task GetAllReservedRoomsId_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            // Act
            var actualResult = reservationsService.GetAllReservedRoomsId(new DateTime(2020, 4, 4), new DateTime(2020, 4, 8)).ToList();
            var expectedResult = new Room[]
            {
                context.Rooms.First(),
            };

            Assert.Equal(expectedResult.Length, actualResult.Count());
        }

        [Fact]
        public async Task GetReservedRooms_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationForGetReservedRoomsAsync(context);

            // Act
            var actualResult = reservationsService.GetReservedRooms();
            var expectedResult = context.Rooms.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetRoomsArrivals_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationForGetRoomsArrivalsAsync(context);

            // Act
            var actualResult = reservationsService.GetRoomsArrivals();
            var expectedResult = context.Rooms.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetRoomsDeparture_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationForGetRoomsDeparture(context);

            // Act
            var actualResult = reservationsService.GetRoomsDeparture();
            var expectedResult = context.Rooms.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetAllOccupiedRooms_ShouldReturnCorrectCount()
        {
            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationForGetReservedRoomsAsync(context);

            // Act
            var actualResult = reservationsService.GetAllOccupiedRooms();
            var expectedResult = context.Rooms.Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task IncomesForCurrentYear_ShouldReturnCorrectCount()
        {
            var errorMessagePrefix = "ReservationsService IncomesForCurrentYear() method does not work properly.";

            // Arrange
            MapperInitializer.InitializeMapper();
            var context = HotelDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = this.GetReservationService(reservationRepository, context);
            var seeder = new ReservationsServiceTestsSeeder();
            await seeder.SeedReservationAsync(context);

            // Act
            var actualResult = reservationsService.IncomesForCurrentYear().ToList();
            var reservation = context.Reservations.First();
            var expectedResult = new List<ColumnChartViewModel>
            {
                new ColumnChartViewModel
                {
                   Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(reservation.StartDate.Month),
                   TotalAmount = (int)reservation.TotalAmount,
                },
            };

            Assert.True(expectedResult[0].Month == actualResult[0].Month, errorMessagePrefix + " " + "Month is not returned properly.");
            Assert.True(expectedResult[0].TotalAmount == actualResult[0].TotalAmount, errorMessagePrefix + " " + "Total amount is not returned properly.");
        }

        private ReservationsService GetReservationService(EfDeletableEntityRepository<Reservation> reservationRepository, HotelDbContext context)
        {
            var reservationsService = new ReservationsService(reservationRepository);

            return reservationsService;
        }
    }
}
