namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.Administration.Dashboard;
    using Hotel.Web.ViewModels.Reservations;

    public interface IReservationsService
    {
        Task<bool> AddReservationAsync(Reservation reservation);

        Task<bool> EditAsync(EditReservationViewModel reservationEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<int> GetAllReservationsCountAsync();

        IQueryable<T> GetAllReservations<T>(int? count = null);

        IEnumerable<string> GetAllReservedRoomsId(DateTime checkIn, DateTime checkOut);

        Task<Reservation> GetReservationByIdAsync(string id);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        int GetReservedRooms();

        int GetRoomsArrivals();

        int GetRoomsDeparture();

        int GetAllOccupiedRooms();

        IEnumerable<Reservation> GetAllReservationsList();

        IEnumerable<ColumnChartViewModel> IncomesForCurrentYear();

        Task<bool> SaveChangesForReservationAsync(Reservation reservation);

        Task<bool> ConfirmByIdAsync(string reservationId, string statusId);
    }
}
