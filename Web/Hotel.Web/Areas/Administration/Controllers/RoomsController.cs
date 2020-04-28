namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Rooms;
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.AspNetCore.Mvc;

    public class RoomsController : AdministrationController
    {
        private readonly IRoomsService roomsService;
        private readonly IRoomTypesService roomTypesService;

        public RoomsController(
            IRoomsService roomsService,
            IRoomTypesService roomTypesService)
        {
            this.roomsService = roomsService;
            this.roomTypesService = roomTypesService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var roomTypes = this.roomTypesService
               .GetAllRoomTypes<RoomTypeDropDownViewModel>();

            var model = new AddRoomInputModel()
            {
                ListOfRoomTypes = roomTypes,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.ListOfRoomTypes = this.roomTypesService
                    .GetAllRoomTypes<RoomTypeDropDownViewModel>();

                return this.View(model);
            }

            Room room = AutoMapperConfig.MapperInstance.Map<Room>(model);

            await this.roomsService.AddRoomAsync(room);

            return this.Redirect($"/Administration/Rooms/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var roomToEdit = await this.roomsService.GetViewModelByIdAsync<EditRoomViewModel>(id);

            var roomTypes = this.roomTypesService
              .GetAllRoomTypes<RoomTypeDropDownViewModel>();

            roomToEdit.ListOfRoomTypes = roomTypes;

            return this.View(roomToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoomViewModel roomEditView)
        {
            await this.roomsService.EditAsync(roomEditView);

            return this.Redirect($"/Administration/Rooms/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var roomToDelete = await this.roomsService.GetViewModelByIdAsync<DeleteRoomViewModel>(id);

            return this.View(roomToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRoomViewModel deleteViewModel)
        {
            var id = deleteViewModel.Id;

            await this.roomsService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/Rooms/All");
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int roomsCount = await this.roomsService.GetAllRoomsCountAsync();

            var pagesCount = (int)Math.Ceiling(roomsCount / (decimal)perPage);

            var rooms = this.roomsService
               .GetAllRooms<DetailsRoomViewModel>()
               .OrderByDescending(x => x.RoomNumber)
               .Skip(perPage * (page - 1))
               .Take(perPage);

            var model = new AllRoomsViewModel
            {
                Rooms = rooms.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }
    }
}
