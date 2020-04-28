namespace Hotel.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.SpecialOffers;

    public interface ISpecialOffersService
    {
        Task<bool> AddSpecialOfferAsync(SpecialOffer offer);

        Task<bool> EditAsync(EditSpecialOfferViewModel roomTypeEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<int> GetAllSpecialOffersCountAsync();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        IQueryable<T> GetAllSpecialOffers<T>(int? count = null);

        Task<bool> CreateAllAsync(IEnumerable<SpecialOffer> offers);
    }
}
