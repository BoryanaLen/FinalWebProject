namespace Hotel.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services.Data;
    using Hotel.Web.ViewModels.Rooms;
    using Microsoft.AspNetCore.Mvc;

    public class ReservedRoomsViewComponent : ViewComponent
    {
        private readonly IReservationsService reservationsService;
        private readonly IRoomsService roomsService;
        private readonly IRoomTypesService roomTypesService;

        public ReservedRoomsViewComponent(
            IReservationsService reservationsService,
            IRoomsService roomsService,
            IRoomTypesService roomTypesService)
        {
            this.reservationsService = reservationsService;
            this.roomsService = roomsService;
            this.roomTypesService = roomTypesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string checkIn, string checkOut)
        {
            var items = await this.GetRoomsAsync(checkIn, checkOut);

            return this.View(items);
        }

        private async Task<List<AvailableRoomViewModel>> GetRoomsAsync(string checkIn, string checkOut)
        {
            DateTime startDate = DateTime.Parse(checkIn).AddHours(14);
            DateTime endDate = DateTime.Parse(checkOut).AddHours(12);

            var reservedRoomsId = this.reservationsService
                .GetAllReservedRoomsId(startDate, endDate)
                .ToList();

            var allAvailableRooms = this.roomsService
                .GetAllRooms()
                .Where(x => !reservedRoomsId.Any(x2 => x2 == x.Id))
                .ToList();

            var allAvailableRoomModels = new List<AvailableRoomViewModel>();

            foreach (var room in allAvailableRooms)
            {
                var roomType = await this.roomTypesService.GetRoomTypeByIdAsync(room.RoomTypeId);

                var modelRoom = new AvailableRoomViewModel
                {
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    RoomRoomTypeId = roomType.Id,
                    RoomRoomTypePrice = roomType.Price,
                    RoomRoomTypeCapacityAdults = roomType.CapacityAdults,
                    RoomRoomTypeCapacityKids = roomType.CapacityKids,
                    RoomRoomTypeImage = roomType.Image,
                    RoomRoomTypeName = roomType.Name,
                    Description = room.Description,
                };

                allAvailableRoomModels.Add(modelRoom);
            }

            return allAvailableRoomModels.ToList();
        }
    }
}
