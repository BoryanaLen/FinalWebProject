namespace Hotel.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.RoomTypes;

    public interface IRoomTypesService
    {
        Task<bool> AddRoomTypeAsync(RoomType roomType);

        Task<bool> EditAsync(EditRoomTypeViewModel roomTypeEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<RoomType> GetRoomTypeByIdAsync(string id);

        Task<int> GetAllRoomTypesCountAsync();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        IEnumerable<T> GetAllRoomTypes<T>(int? count = null);

        RoomType GetRoomTypeByName(string name);
    }
}
