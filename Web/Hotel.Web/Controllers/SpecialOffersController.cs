namespace Hotel.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Services.Data;
    using Hotel.Web.ViewModels.SpecialOffers;
    using Microsoft.AspNetCore.Mvc;

    public class SpecialOffersController : BaseController
    {
        private readonly ISpecialOffersService specialOffersService;

        public SpecialOffersController(ISpecialOffersService specialOffersService)
        {
            this.specialOffersService = specialOffersService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int offersCount = await this.specialOffersService.GetAllSpecialOffersCountAsync();

            var pagesCount = (int)Math.Ceiling(offersCount / (decimal)perPage);

            var offers = this.specialOffersService
               .GetAllSpecialOffers<DetailsSpecialOfferViewModel>()
               .OrderBy(x => x.Id);

            var model = new AllSpecialOffersViewModel
            {
                SpecialOffers = offers.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var offer = await this.specialOffersService.GetViewModelByIdAsync<DetailsSpecialOfferViewModel>(id);

            return this.View(offer);
        }
    }
}
