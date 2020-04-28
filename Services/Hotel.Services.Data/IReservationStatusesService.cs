namespace Hotel.Services.Data
{
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.ReservationStatuses;

    public interface IReservationStatusesService
    {
        Task<bool> AddReservationStatusAsync(ReservationStatus reservationStatus);

        Task<bool> EditAsync(EditReservationStatusViewModel reservationStatusEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<ReservationStatus> GetReservationStatusByIdAsync(string id);

        Task<int> GetAllReservationStatusesCountAsync();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        Task<bool> CreateAllAsync(string[] types);

        ReservationStatus GetReserVationStatusByName(string name);
    }
}
