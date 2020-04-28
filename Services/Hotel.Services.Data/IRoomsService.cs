namespace Hotel.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.Rooms;

    public interface IRoomsService
    {
        Task<bool> AddRoomAsync(Room room);

        Task<bool> EditAsync(EditRoomViewModel roomEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<Room> GetRoomByIdAsync(string id);

        Task<int> GetAllRoomsCountAsync();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        IQueryable<T> GetAllRooms<T>(int? count = null);

        Room GetRoomByRoomTypeName(string name);

        IEnumerable<Room> GetAllRooms();
    }
}
