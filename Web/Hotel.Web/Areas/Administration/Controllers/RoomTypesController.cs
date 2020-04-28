namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.AspNetCore.Mvc;

    public class RoomTypesController : AdministrationController
    {
        private readonly IRoomTypesService roomTypesService;
        private readonly ICloudinaryService cloudinaryService;

        public RoomTypesController(
            IRoomTypesService roomTypesService,
            ICloudinaryService cloudinaryService)
        {
            this.roomTypesService = roomTypesService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int roomTypesCount = await this.roomTypesService.GetAllRoomTypesCountAsync();

            var pagesCount = (int)Math.Ceiling(roomTypesCount / (decimal)perPage);

            var roomTypes = this.roomTypesService
               .GetAllRoomTypes<DetailsRoomTypeViewModel>()
               .OrderBy(x => x.Name)
               .Skip(perPage * (page - 1))
               .Take(perPage);

            var model = new AllRoomTypesViewModel
            {
                RoomTypes = roomTypes,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomTypeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var photoUrl = await this.cloudinaryService.UploadPhotoAsync(
               model.RoomImage,
               $"Room - {model.Name}",
               "Hotel_room_types_photos");

            model.Image = photoUrl;

            RoomType roomType = AutoMapperConfig.MapperInstance.Map<RoomType>(model);

            await this.roomTypesService.AddRoomTypeAsync(roomType);

            return this.Redirect($"/Administration/RoomTypes/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var roomTypeToEdit = await this.roomTypesService.GetViewModelByIdAsync<EditRoomTypeViewModel>(id);

            return this.View(roomTypeToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoomTypeViewModel roomTypeEditView)
        {
            var roomType = await this.roomTypesService.GetRoomTypeByIdAsync(roomTypeEditView.Id);

            if (roomTypeEditView.RoomImage != null)
            {
                var newImageUrl = await this.cloudinaryService.UploadPhotoAsync(
                roomTypeEditView.RoomImage,
                $"Room - {roomTypeEditView.Name}",
                "Hotel_room_types_photos");

                roomTypeEditView.Image = newImageUrl;

                var fileType = roomTypeEditView.RoomImage.ContentType.Split('/')[1];

                if (!this.IsImageTypeValid(fileType))
                {
                    return this.View();
                }
            }
            else
            {
                roomTypeEditView.Image = roomType.Image;
            }

            await this.roomTypesService.EditAsync(roomTypeEditView);

            return this.Redirect($"/Administration/RoomTypes/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var roomTypeToDelete = await this.roomTypesService.GetViewModelByIdAsync<DeleteRoomTypeViewModel>(id);

            return this.View(roomTypeToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRoomTypeViewModel deleteViewModel)
        {
            var id = deleteViewModel.Id;

            await this.roomTypesService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/RoomTypes/All");
        }
    }
}
