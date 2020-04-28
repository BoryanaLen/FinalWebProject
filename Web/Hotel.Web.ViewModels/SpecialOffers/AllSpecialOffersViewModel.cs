namespace Hotel.Web.ViewModels.SpecialOffers
{
    using System.Collections.Generic;

    public class AllSpecialOffersViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsSpecialOfferViewModel> SpecialOffers { get; set; }
    }
}
