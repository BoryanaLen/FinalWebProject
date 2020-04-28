namespace Hotel.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using Hotel.Web.ViewModels.Footer;
    using Hotel.Web.ViewModels.SpecialOffers;

    public class HomeViewModel
    {
        public FooterViewModel FooterViewModel { get; set; }

        public IEnumerable<DetailsSpecialOfferViewModel> SpecialOffers { get; set; }
    }
}
