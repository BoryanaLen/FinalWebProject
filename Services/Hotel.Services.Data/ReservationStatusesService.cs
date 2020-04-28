namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.ReservationStatuses;
    using Microsoft.EntityFrameworkCore;

    public class ReservationStatusesService : IReservationStatusesService
    {
        private readonly IDeletableEntityRepository<ReservationStatus> reservationStatusesRepository;

        public ReservationStatusesService(IDeletableEntityRepository<ReservationStatus> reservationStatusesRepository)
        {
            this.reservationStatusesRepository = reservationStatusesRepository;
        }

        public async Task<bool> AddReservationStatusAsync(ReservationStatus reservationStatus)
        {
            if (reservationStatus.Name == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            await this.reservationStatusesRepository.AddAsync(reservationStatus);

            var result = await this.reservationStatusesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditReservationStatusViewModel reservationStatusEditViewModel)
        {
            var reservationStatus = this.reservationStatusesRepository.All().FirstOrDefault(r => r.Id == reservationStatusEditViewModel.Id);

            if (reservationStatus == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidReservationStatusIdErrorMessage, reservationStatusEditViewModel.Id)));
            }

            if (reservationStatusEditViewModel.Name == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            reservationStatus.Name = reservationStatusEditViewModel.Name;

            this.reservationStatusesRepository.Update(reservationStatus);

            var result = await this.reservationStatusesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var reservationStatus = await this.reservationStatusesRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (reservationStatus == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationStatusIdErrorMessage, id));
            }

            reservationStatus.IsDeleted = true;

            this.reservationStatusesRepository.Update(reservationStatus);

            var result = await this.reservationStatusesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<ReservationStatus> GetReservationStatusByIdAsync(string id)
        {
            var reservationStatus = await this.reservationStatusesRepository.GetByIdWithDeletedAsync(id);

            if (reservationStatus == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationStatusIdErrorMessage, id));
            }

            return reservationStatus;
        }

        public async Task<int> GetAllReservationStatusesCountAsync()
        {
            var reservationStatuses = await this.reservationStatusesRepository.All()
                .ToArrayAsync();
            return reservationStatuses.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var reservationStatus = await this.reservationStatusesRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (reservationStatus == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationStatusIdErrorMessage, id));
            }

            return reservationStatus;
        }

        public IEnumerable<T> GetAllReservationStatuses<T>(int? count = null)
        {
            IQueryable<ReservationStatus> query =
                 this.reservationStatusesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<bool> CreateAllAsync(string[] types)
        {
            foreach (var reservationStatus in types)
            {
                var model = new ReservationStatus
                {
                    Name = reservationStatus,
                };

                await this.reservationStatusesRepository.AddAsync(model);
            }

            var result = await this.reservationStatusesRepository.SaveChangesAsync();

            return result > 0;
        }

        public ReservationStatus GetReserVationStatusByName(string name)
        {
            var reservationStatus = this.reservationStatusesRepository
              .AllAsNoTracking()
              .Where(x => x.Name == name)
              .FirstOrDefault();

            if (reservationStatus == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidReservationStatusNameErrorMessage, name));
            }

            return reservationStatus;
        }
    }
}
