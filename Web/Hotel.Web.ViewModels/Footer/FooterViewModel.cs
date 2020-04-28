namespace Hotel.Web.ViewModels.Footer
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class FooterViewModel : IMapFrom<HotelData>
    {
        public string Manager { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
