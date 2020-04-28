namespace Hotel.Web.ViewModels.ReservationStatuses
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DetailsReservationStatusViewModel : IMapFrom<ReservationStatus>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
