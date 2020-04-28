namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.SpecialOffers;
    using Microsoft.AspNetCore.Mvc;

    public class SpecialOffersController : AdministrationController
    {
        private readonly ISpecialOffersService specialOffersService;
        private readonly IHotelsService hotelsService;

        public SpecialOffersController(ISpecialOffersService specialOffersService, IHotelsService hotelsService)
        {
            this.specialOffersService = specialOffersService;
            this.hotelsService = hotelsService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int specialOffersCount = await this.specialOffersService.GetAllSpecialOffersCountAsync();

            var pagesCount = (int)Math.Ceiling(specialOffersCount / (decimal)perPage);

            var specialOffers = this.specialOffersService
               .GetAllSpecialOffers<DetailsSpecialOfferViewModel>()
               .Skip(perPage * (page - 1))
               .Take(perPage);

            var model = new AllSpecialOffersViewModel
            {
                SpecialOffers = specialOffers,
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
        public async Task<IActionResult> Add(AddSpecialOfferInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var hotel = this.hotelsService.GetHotelByName("Hotel Boryana");

            model.HotelDataId = hotel.Id;

            SpecialOffer specialOffer = AutoMapperConfig.MapperInstance.Map<SpecialOffer>(model);

            await this.specialOffersService.AddSpecialOfferAsync(specialOffer);

            return this.Redirect($"/Administration/SpecialOffers/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var specialOfferToEdit = await this.specialOffersService.GetViewModelByIdAsync<EditSpecialOfferViewModel>(id);

            return this.View(specialOfferToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSpecialOfferViewModel specialOfferEditView)
        {
            await this.specialOffersService.EditAsync(specialOfferEditView);

            return this.Redirect($"/Administration/SpecialOffers/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var specialOfferToDelete = await this.specialOffersService.GetViewModelByIdAsync<DeleteSpecialOfferViewModel>(id);

            return this.View(specialOfferToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteSpecialOfferViewModel deleteViewModel)
        {
            var id = deleteViewModel.Id;

            await this.specialOffersService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/SpecialOffers/All");
        }
    }
}
