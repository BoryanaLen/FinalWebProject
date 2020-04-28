namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Administration.Dashboard;
    using Hotel.Web.ViewModels.Reservations;
    using Microsoft.EntityFrameworkCore;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task<bool> AddReservationAsync(Reservation reservation)
        {
            if (reservation.UserId == null ||
               reservation.StartDate == null ||
               reservation.EndDate == null ||
               reservation.PaymentTypeId == null ||

               reservation.ReservationStatusId == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.reservationRepository.AddAsync(reservation);

            var result = await this.reservationRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditReservationViewModel reservationEditViewModel)
        {
            var reservation = this.reservationRepository.All().FirstOrDefault(r => r.Id == reservationEditViewModel.Id);

            if (reservation == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidReservationIdErrorMessage, reservationEditViewModel.Id)));
            }

            if (reservationEditViewModel.UserId == null ||
               reservationEditViewModel.StartDate == null ||
               reservationEditViewModel.EndDate == null ||
               reservationEditViewModel.PaymentTypeId == null ||
               reservationEditViewModel.ReservationStatusId == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            reservation.StartDate = reservationEditViewModel.StartDate;
            reservation.EndDate = reservationEditViewModel.EndDate;
            reservation.Adults = reservationEditViewModel.Adults;
            reservation.Kids = reservationEditViewModel.Kids;
            reservation.ReservationStatusId = reservationEditViewModel.ReservationStatusId;
            reservation.UserId = reservationEditViewModel.UserId;

            this.reservationRepository.Update(reservation);

            var result = await this.reservationRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var reservation = await this.reservationRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (reservation == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationIdErrorMessage, id));
            }

            reservation.IsDeleted = true;

            this.reservationRepository.Update(reservation);

            var result = await this.reservationRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<int> GetAllReservationsCountAsync()
        {
            var reservations = await this.reservationRepository.All()
                .ToArrayAsync();

            return reservations.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var reservation = await this.reservationRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (reservation == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationIdErrorMessage, id));
            }

            return reservation;
        }

        public IQueryable<T> GetAllReservations<T>(int? count = null)
        {
            IQueryable<Reservation> query =
                this.reservationRepository.All().OrderBy(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public IEnumerable<Reservation> GetAllReservationsList()
        {
            var reservations =
                this.reservationRepository.All().OrderBy(x => x.CreatedOn);

            return reservations;
        }

        public async Task<Reservation> GetReservationByIdAsync(string id)
        {
            var reservation = await this.reservationRepository.GetByIdWithDeletedAsync(id);

            if (reservation == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationIdErrorMessage, id));
            }

            return reservation;
        }

        public IEnumerable<string> GetAllReservedRoomsId(DateTime checkIn, DateTime checkOut)
        {
            var roomsId = this.reservationRepository.AllAsNoTracking()
                .Where(x => (x.StartDate >= checkIn && x.StartDate <= checkOut) || (x.EndDate >= checkIn && x.EndDate <= checkOut))
                .SelectMany(x => x.ReservationRooms)
                .Select(x => x.RoomId)
                .Distinct()
                .ToArray();

            return roomsId;
        }

        public int GetReservedRooms()
        {
            int currentInHouse = this.reservationRepository
                .AllAsNoTracking()
                .Where(x => x.StartDate < DateTime.Today && x.EndDate >= DateTime.Today)
                .SelectMany(x => x.ReservationRooms)
                .Count();

            return currentInHouse;
        }

        public int GetRoomsArrivals()
        {
            int currentInHouse = this.reservationRepository
                .AllAsNoTracking()
                .Where(x => x.StartDate.Date == DateTime.Today.Date)
                .SelectMany(x => x.ReservationRooms)
                .Count();

            return currentInHouse;
        }

        public int GetRoomsDeparture()
        {
            int currentInHouse = this.reservationRepository
               .AllAsNoTracking()
               .Where(x => x.EndDate.Date == DateTime.Today.Date)
               .SelectMany(x => x.ReservationRooms)
               .Count();

            return currentInHouse;
        }

        public int GetAllOccupiedRooms()
        {
            var roomsCount = this.reservationRepository
                .AllAsNoTracking()
                .Where(x => x.StartDate <= DateTime.Now.Date && x.EndDate > DateTime.Now.Date)
                .SelectMany(x => x.ReservationRooms)
                .Distinct()
                .Count();

            return roomsCount;
        }

        public IEnumerable<ColumnChartViewModel> IncomesForCurrentYear()
        {
            var incomes = this.reservationRepository
               .AllAsNoTracking()
               .Where(x => x.StartDate.Year == DateTime.Now.Year)
               .OrderBy(x => x.StartDate)
               .GroupBy(gp => new { gp.StartDate.Month })
               .Select(x => new ColumnChartViewModel
               {
                   Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(x.Key.Month),
                   TotalAmount = (int)x.Sum(y => y.TotalAmount),
               }).ToList();

            return incomes;
        }

        public async Task<bool> SaveChangesForReservationAsync(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage)));
            }

            this.reservationRepository.Update(reservation);

            var result = await this.reservationRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ConfirmByIdAsync(string reservationId, string statusId)
        {
            var reservation = await this.reservationRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == reservationId);

            if (reservation == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationIdErrorMessage, reservationId));
            }

            reservation.ReservationStatusId = statusId;

            this.reservationRepository.Update(reservation);

            var result = await this.reservationRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
