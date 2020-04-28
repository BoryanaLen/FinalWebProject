namespace Hotel.Web.ViewModels.SpecialOffers
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DetailsSpecialOfferViewModel : IMapFrom<SpecialOffer>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent { get; set; }
    }
}
